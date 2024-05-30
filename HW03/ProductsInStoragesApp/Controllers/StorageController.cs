using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProductInStorageApp.Dto;
using ProductsInStoragesApp.Client;

namespace StorageApp.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductInStorageController : ControllerBase
    {
        private IProductInStorageRepo _repo;
        private IProductsClient _productsClient;
        private IStoragesClient _storagesClient;
        public ProductInStorageController(IProductInStorageRepo repo, IProductsClient productsClient, IStoragesClient storagesClient)
        {
            _repo = repo;
            _productsClient = productsClient;
            _storagesClient = storagesClient;
        }

        [HttpPost(template: "TakeProduct")]
        public async Task<TakeProductResultDto> TakeProductAsync(ProductInStorageDto record)
        {

            var storageExistsTask = _storagesClient.Exists(record.StorageId);
            var productExistsTask = _productsClient.Exists(record.ProductId);

            var storageExists = await storageExistsTask;
            var productExists = await productExistsTask;

            if (storageExists && productExists)
            {
                try
                {
                    _repo.TakeProduct(record);
                    return new TakeProductResultDto { Success = true };
                }
                catch (Exception e)
                {
                    if (e is DbUpdateException && e.InnerException is PostgresException && e?.InnerException?.Message?.Contains("duplicate") == true)
                    {
                        return new TakeProductResultDto { Error = "Такой продукт уже в хранилище" };
                    }
                    throw;
                }

            }
            else
            {

                if (!productExists)
                    return new TakeProductResultDto { Error = "Продукт не найден!" };
                else
                    return new TakeProductResultDto { Error = "Хранилище не найдено!" };
            }
        }

        [HttpPost(template: "ReturnProduct")]
        public ActionResult ReturnProduct(int productId)
        {
            _repo.ReturnProduct(productId);
            return Ok();
        }

        [HttpGet(template: "ListProducts")]
        public ActionResult<IEnumerable<int>> ListProducts(int storageId)
        {
            return Ok(_repo.ListProducts(storageId));
        }
    }
}
