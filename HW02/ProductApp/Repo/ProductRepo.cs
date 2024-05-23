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
        public ProductRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }
        public int AddProduct(ProductDTORequest productDTORequest)
        {
            using (var context = new ProductContext())
            {

                var product = context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(productDTORequest.Name.ToLower()));
                if (product == null)
                {
                    var entity = _mapper.Map<Product>(productDTORequest);
                    context.Products.Add(entity);
                    context.SaveChanges();
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
            using (var context = new ProductContext())
            {
                var products = context.Products.Select(_mapper.Map<ProductDTOResponse>).ToList();
                _memoryCache.Set("products", products, TimeSpan.FromMinutes(30));
                return products;
            }
        }

        public ProductDTOResponse DeleteProduct(int id)
        {
            using (var context = new ProductContext())
            {

                var entityProduct = context.Products.FirstOrDefault(x => x.Id == id);
                if (entityProduct != null)
                {
                    context.Products.Remove(entityProduct);
                    context.SaveChanges();
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
