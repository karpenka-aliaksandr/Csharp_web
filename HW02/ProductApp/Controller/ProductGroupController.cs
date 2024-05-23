using Microsoft.AspNetCore.Mvc;
using ProductApp.DTO;
using ProductApp.Repo.Abstraction;

namespace ProductApp.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ProductGroupController : ControllerBase
    {
        private readonly IProductGroupRepo _repo;
        public ProductGroupController(IProductGroupRepo repo)
        {
            _repo = repo;
        }

        [HttpPost(template: "addproductgroup")]
        public ActionResult<int> AddProductGroup(ProductGroupDTORequest productGroupDTORequest)
        {
            try
            {
                var id = _repo.AddProductGroup(productGroupDTORequest);
                return id;
            }
            catch
            {
                return StatusCode(409);
            }
        }

        [HttpGet(template: "getproductgroups")]
        public ActionResult<IEnumerable<ProductGroupDTOResponse>> GetProductGroups()
        {
            try
            {
                var productGroups = _repo.GetProductGroups().ToList();
                return productGroups;
            }
            catch
            {
                return StatusCode(523, "База данных недоступна");
            }
        }

        [HttpDelete(template: "deleteproductgroup")]
        public ActionResult<ProductGroupDTOResponse> DeleteProductGroups(int id)
        {
            try
            {
                var productGroup = _repo.DeleteProductGroup(id);
                return productGroup;
            }
            catch
            {
                return BadRequest();
            }
        }


       

    }


}
