using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using ProductApp.DB;
using ProductApp.DTO;
using ProductApp.Model;
using ProductApp.Repo.Abstraction;

namespace ProductApp.Repo
{
    public class ProductGroupRepo : IProductGroupRepo
    {
        private readonly IMapper _mapper;
        private IMemoryCache _memoryCache;
        private ProductContext _context;
        public ProductGroupRepo(IMapper mapper, IMemoryCache memoryCache, ProductContext context)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _context = context;
        }

        public int AddProductGroup(ProductGroupDTORequest productGroupDTORequest)
        {
            using (_context)
            {
                var entityProductGroup = _context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(productGroupDTORequest.Name.ToLower()));
                if (entityProductGroup == null)
                {
                    var entity = _mapper.Map<ProductGroup>(productGroupDTORequest);
                    _context.ProductGroups.Add(entity);
                    _context.SaveChanges();
                    _memoryCache.Remove("productGroups");
                    return entity.Id;
                }
                else
                {
                    throw new Exception("ProductGroup already exist");
                }
            }
        }

        public IEnumerable<ProductGroupDTOResponse> GetProductGroups()
        {
            if (_memoryCache.TryGetValue("productGroups", out IEnumerable<ProductGroupDTOResponse> productGroupsCache))
            {
                return productGroupsCache;
            }
            using (_context)
            {
                var productGroups = _context.ProductGroups.Select(_mapper.Map<ProductGroupDTOResponse>).ToList();
                _memoryCache.Set("productGroups", productGroups, TimeSpan.FromMinutes(30));
                return productGroups;
            }
        }

        public ProductGroupDTOResponse DeleteProductGroup(int id)
        {
            using (_context)
            {

                var entityProductGroup = _context.ProductGroups.FirstOrDefault(x => x.Id == id);
                if (entityProductGroup != null)
                {
                    _context.ProductGroups.Remove(entityProductGroup);
                    _context.SaveChanges();
                    _memoryCache.Remove("productGroups");
                    return _mapper.Map<ProductGroupDTOResponse>(entityProductGroup);
                }
                else
                {
                    throw new Exception("ProductGroup not found");
                }
            }
        }
    }
}
