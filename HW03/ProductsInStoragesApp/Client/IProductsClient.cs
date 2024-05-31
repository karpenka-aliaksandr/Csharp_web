using ProductInStorageApp.DTO;
using System;
namespace ProductsInStoragesApp.Client
{
	public interface IProductsClient
	{
        public Task<bool> Exists(int? id);
        public Task<IEnumerable<ProductDTOResponse>> GetProducts();
    }
}

