using ProductApp.DTO;

namespace ProductApp.Repo.Abstraction
{
    public interface IProductGroupRepo
    {
        public int AddProductGroup(ProductGroupDTORequest productGroupRequest);
        public IEnumerable<ProductGroupDTOResponse> GetProductGroups();
        public ProductGroupDTOResponse DeleteProductGroup(int id);
    }
}
