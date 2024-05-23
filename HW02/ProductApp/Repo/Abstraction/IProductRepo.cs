using ProductApp.DTO;

namespace ProductApp.Repo.Abstraction
{
    public interface IProductRepo
    {
        public int AddProduct(ProductDTORequest productDTORequest);
        public IEnumerable<ProductDTOResponse> GetProducts();
        public ProductDTOResponse DeleteProduct(int id);

    }
}
