using Market.DB;
using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        [HttpPost(template: "addgroup")]
        public ActionResult AddGroup(string name, string? description)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.ProductGroups.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        ctx.ProductGroups.Add(new ProductGroup { Name = name, Description = description });
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
        [HttpGet(template: "getgroups")]
        public ActionResult<IEnumerable<ProductGroup>> GetGroups()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.ProductGroups.Select(x => new ProductGroup { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "deletegroup")]
        public ActionResult DeleteGroup(string name)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var productGroup = ctx.ProductGroups.FirstOrDefault(x => x.Name == name);
                    if (productGroup != null)
                    {
                        ctx.ProductGroups.Remove(productGroup);
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
