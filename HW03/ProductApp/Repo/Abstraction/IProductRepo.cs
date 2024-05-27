using ProductApp.DTO;

namespace ProductApp.Repo.Abstraction
{
    public interface IProductRepo
    {
        public int AddProduct(ProductDTORequest productDTORequest);
        public IEnumerable<ProductDTOResponse> GetProducts();
        public ProductDTOResponse DeleteProduct(int id);
        public bool CheckProduct(int Id);

        public int AddProductGroup(ProductGroupDTORequest productGroupRequest);
        public IEnumerable<ProductGroupDTOResponse> GetProductGroups();
        public ProductGroupDTOResponse DeleteProductGroup(int id);

    }
}
