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
        public ProductGroupRepo(IMapper mapper, IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public int AddProductGroup(ProductGroupDTORequest productGroupDTORequest)
        {
            using (var context = new ProductContext())
            {
                var entityProductGroup = context.Products.FirstOrDefault(x => x.Name.ToLower().Equals(productGroupDTORequest.Name.ToLower()));
                if (entityProductGroup == null)
                {
                    var entity = _mapper.Map<ProductGroup>(productGroupDTORequest);
                    context.ProductGroups.Add(entity);
                    context.SaveChanges();
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
            using (var context = new ProductContext())
            {
                var productGroups = context.ProductGroups.Select(_mapper.Map<ProductGroupDTOResponse>).ToList();
                _memoryCache.Set("productGroups", productGroups, TimeSpan.FromMinutes(30));
                return productGroups;
            }
        }

        public ProductGroupDTOResponse DeleteProductGroup(int id)
        {
            using (var context = new ProductContext())
            {

                var entityProductGroup = context.ProductGroups.FirstOrDefault(x => x.Id == id);
                if (entityProductGroup != null)
                {
                    context.ProductGroups.Remove(entityProductGroup);
                    context.SaveChanges();
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
