using ProductInStorageApp.Dto;
using ProductInStorageApp.DTO;
using ProductsInStoragesApp.Client;

namespace ProductsInStoragesApp.GraphQLServices.Query
{
    public class Query
    {
        //public IEnumerable<ProductDTOResponse> GetProducts([Service] IProductsClient productsClient) => productsClient.GetProducts().Result;
        public IEnumerable<ProductInStorageDto> GetProductsInStorage([Service] IProductInStorageRepo productsInStorageRepo) => productsInStorageRepo.GetProductsInStorages();
    }
}
