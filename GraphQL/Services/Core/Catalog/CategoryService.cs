using AutoMapper;
using GraphQL.Data.Entities;
using GraphQL.Data.Repositories.Category;
using GraphQL.Services.Core.IServices;

using static GraphQL.ViewModels.Catalog.Entities;

namespace GraphQL.Services.Core.Catalog
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Add(CategoryRequest request)
        {
            if (await _categoryRepository.GetOneAsync(x => x.CategoryName.Equals(request.CategoryName)) is not null) throw new BadHttpRequestException("Category name was existed");

            var item = _mapper.Map<CategoryRequest, Category>(request);
            var added = await _categoryRepository.AddAsync(item);
            return added;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var entity = await _categoryRepository.GetOneAsync(c => c.Id.Equals(id))
                         ?? throw new KeyNotFoundException($"Not found category with id {id}");

            await _categoryRepository.DeleteAsync(entity);
            return id;
        }

        public async Task<Category> Get(Guid id)
        {
            var entity = await _categoryRepository.GetOneAsync(c => c.Id.Equals(id));

            return entity;
        }

        public IQueryable<Category> GetAll()
        {
            return _categoryRepository.GetAsync();
        }

        public async Task<Category> Update(CategoryRequest request, Guid id)
        {
            var entity = await _categoryRepository.GetOneAsync(c => c.Id.Equals(id))
                         ?? throw new KeyNotFoundException($"Not found category with id {id}");

            entity = _mapper.Map(request, entity);
            var update = await _categoryRepository.UpdateAsync(entity);

            return update;
        }
    }
}