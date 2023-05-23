using AutoMapper;
using GraphQL.Data.Entities;
using GraphQL.Data.Repositories.Product;
using GraphQL.Services.Core.IServices;
using static GraphQL.ViewModels.Catalog.Entities;

namespace GraphQL.Services.Core.Catalog
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryService _categoryService;

        public ProductService(IMapper mapper, IProductRepository productRepository, ICategoryService categoryService)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _categoryService = categoryService;
        }

        public async Task<Product> Get(Guid id)
        {
            var entity = await _productRepository.GetOneAsync(c => c.Id.Equals(id));

            return entity;
        }

        public IQueryable<Product> GetAll()
        {
            return _productRepository.GetAsync();
        }

        public async Task<Product> Add(ProductRequest request)
        {
            if (await _productRepository.GetOneAsync(x => x.ProductName.Equals(request.ProductName)) is not null) throw new BadHttpRequestException("Product name was existed");

            var item = _mapper.Map<ProductRequest, Product>(request);
            var added = await _productRepository.AddAsync(item);
            return added;
        }

        public async Task<Product> Update(ProductRequest request, Guid id)
        {
            var product = await _productRepository.GetOneAsync(c => c.Id.Equals(id))
                         ?? throw new KeyNotFoundException($"Not found product with id {id}");

            if (request.CategoryId is not null)
                _ = await _categoryService.Get((Guid)request.CategoryId) ?? throw new KeyNotFoundException($"Not found Category with id {request.CategoryId}");
                else
                request.CategoryId = product.CategoryId;

            if ((request.ProductName is not null && request.CategoryId != product.CategoryId
                && await _productRepository.GetOneAsync(
                    p => p.CategoryId.Equals(request.CategoryId)
                    && p.ProductName.ToLower().Equals(request.ProductName.ToLower())) is not null) ||
                    (request.ProductName is null && request.CategoryId != product.CategoryId
                    && await _productRepository.GetOneAsync(
                    p => p.CategoryId.Equals(request.CategoryId)
                    && p.ProductName.ToLower().Equals(product.ProductName.ToLower())) is not null))
                throw new BadHttpRequestException("Product name already exists");

            product = _mapper.Map(request, product);

            var update = await _productRepository.UpdateAsync(product);

            return update;
        }
        public async Task<Guid> Delete(Guid id)
        {
            var entity = await _productRepository.GetOneAsync(c => c.Id.Equals(id))
                         ?? throw new KeyNotFoundException($"Not found category with id {id}");

            await _productRepository.DeleteAsync(entity);
            return id;
        }
    }
}
