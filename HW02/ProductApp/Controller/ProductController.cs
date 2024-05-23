using Microsoft.AspNetCore.Mvc;
using ProductApp.DTO;
using ProductApp.Repo.Abstraction;

namespace ProductApp.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepo _repo;
        public ProductController(IProductRepo repo)
        {
            _repo = repo;
        }

        [HttpPost(template: "addproduct")]
        public ActionResult<int> AddProduct(ProductDTORequest productDTORequest)
        {
            try
            {
                var id = _repo.AddProduct(productDTORequest);
                return id;
            }
            catch
            {
                return StatusCode(409);
            }
        }

        [HttpGet(template: "getproducts")]
        public ActionResult<IEnumerable<ProductDTOResponse>> GetProducts()
        {
            try
            {
                var products = _repo.GetProducts().ToList();
                return products;
            }
            catch
            {
                return StatusCode(523, "База данных недоступна");
            }
        }

        [HttpDelete(template: "deleteproduct")]
        public ActionResult<ProductDTOResponse> DeleteProducts(int id)
        {
            try
            {
                var product = _repo.DeleteProduct(id);
                return product;
            }
            catch
            {
                return BadRequest();
            }
        }


       

    }


}
