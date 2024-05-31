using ProductInStorageApp.Dto;

public interface IProductInStorageRepo
{
    public void TakeProduct(ProductInStorageDto record);
    public void ReturnProduct(int productId);
    public IEnumerable<int?> ListProductsOfStorage(int storageId);
    public IEnumerable<int?> GetStoragesWithProducts();
    public IEnumerable<ProductInStorageDto> GetProductsInStorages();


}


