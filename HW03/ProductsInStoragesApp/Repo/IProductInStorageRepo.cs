using ProductInStorageApp.Dto;

public interface IProductInStorageRepo
{
    public void TakeProduct(ProductInStorageDto record);
    public void ReturnProduct(int productId);
    public IEnumerable<int?> ListProducts(int storageId);
}


