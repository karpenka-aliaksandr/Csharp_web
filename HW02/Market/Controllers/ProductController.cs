using Market.DB;
using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost(template: "addproduct")]
        public ActionResult AddProduct(string name, string? description)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    if (ctx.Products.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        ctx.Products.Add(new Product { Name = name, Description = description });
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
        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var list = ctx.Products.Select(x => new Product { Id = x.Id, Name = x.Name, Description = x.Description }).ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete(template: "deleteproduct")]
        public ActionResult DeleteProduct(string name)
        {
            try
            {
                using (var ctx = new ProductContext())
                {
                    var product = ctx.Products.FirstOrDefault(x => x.Name == name);
                    if (product != null)
                    {
                        ctx.Products.Remove(product);
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
