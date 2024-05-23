using AutoMapper;
using ProductApp.DB;
using ProductApp.DTO;
using ProductApp.Model;
using Microsoft.Extensions.Caching.Memory;
using ProductApp.Repo.Abstraction;

namespace ProductApp.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        private ProductContext _context;
        public ProductRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext context)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _context = context;

        }
        public int AddProduct(ProductDTORequest productDTORequest)
        {
            using (_context)
            {

                var product = _context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(productDTORequest.Name.ToLower()));
                if (product == null)
                {
                    var entity = _mapper.Map<Product>(productDTORequest);
                    _context.Products.Add(entity);
                    _context.SaveChanges();
                    _memoryCache.Remove("products");
                    return entity.Id;
                }
                else
                {
                    throw new Exception("Product already exist");
                }
            }
        }

        public IEnumerable<ProductDTOResponse> GetProducts()
        {
            if (_memoryCache.TryGetValue("products", out IEnumerable<ProductDTOResponse> productsCache))
            {
                return productsCache;
            }
            using (_context)
            {
                var products = _context.Products.Select(_mapper.Map<ProductDTOResponse>).ToList();
                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }

        public ProductDTOResponse DeleteProduct(int id)
        {
            using (_context)
            {

                var entityProduct = _context.Products.FirstOrDefault(x => x.Id == id);
                if (entityProduct != null)
                {
                    _context.Products.Remove(entityProduct);
                    _context.SaveChanges();
                    _memoryCache.Remove("products");
                    return _mapper.Map<ProductDTOResponse>(entityProduct);
                }
                else
                {
                    throw new Exception("Product not found");
                }
            }
        }
    }
}
