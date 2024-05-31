using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductInStorageApp.DB;
using ProductInStorageApp.Dto;
using ProductInStorageApp.Model;


public class ProductInStorageRepo : IProductInStorageRepo
{
    private IMapper _mapper;
    private ProductInStorageContext _context;

    public ProductInStorageRepo(IMapper mapper, ProductInStorageContext context)
	{
        _mapper = mapper;
        _context = context;
    }

    public void ReturnProduct(int productId)
    {
        using (_context)
        {
        var product = _context.ProductInStorages.First(x => x.ProductId == productId);

        _context.ProductInStorages.Remove(product);

        _context.SaveChanges();
        }
    }

    public void TakeProduct(ProductInStorageDto record)
    {
        using (_context)
        {
        _context.Add(_mapper.Map<ProductInStorage>(record));
        _context.SaveChanges();
        }        
    }

    public IEnumerable<int?> ListProductsOfStorage(int storageId)
    {
        using (_context)
        {
        return _context.ProductInStorages.Where(x => x.StorageId == storageId).Select(x => x.ProductId).ToList();
        }
    }
    public IEnumerable<int?> GetStoragesWithProducts()
    {
        using (_context)
        {
            return _context.ProductInStorages.Select(x => x.StorageId).Distinct().ToList();
        }
    }

    public IEnumerable<ProductInStorageDto> GetProductsInStorages()
    {
        using (_context)
        {
            return _context.ProductInStorages.Select(_mapper.Map<ProductInStorageDto>).ToList();
        }
    }
}

