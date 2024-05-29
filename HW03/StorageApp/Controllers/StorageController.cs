using Microsoft.AspNetCore.Mvc;
using StorageApp.DB;
using Storage = StorageApp.Model.Storage;

namespace StorageApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StorageController : ControllerBase
    {
        [HttpPost(template: "addstorage")]
        public ActionResult AddStorage(string name)
        {
            try
            {
                using (var ctx = new StorageContext())
                {
                    if (ctx.Storages.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        ctx.Storages.Add(new Storage { Name = name });
                        ctx.SaveChanges();
                    }
                }
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet(template: "getstorages")]
        public ActionResult<IEnumerable<Storage>> GetStorages()
        {
            try
            {
                using (var ctx = new StorageContext())
                {
                    var list = ctx.Storages.Select(x => new Storage { Id = x.Id, Name = x.Name}).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "deletestorage")]
        public ActionResult DeleteStorage(string name)
        {
            try
            {
                using (var ctx = new StorageContext())
                {
                    var storage = ctx.Storages.FirstOrDefault(x => x.Name == name);
                    if (storage != null)
                    {
                        ctx.Storages.Remove(storage);
                        ctx.SaveChanges();
                        return Ok();
                    }
                    return BadRequest();
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
