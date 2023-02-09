using Application.API.Models;
using Application.API.Repository.Abstruct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Implement
{
    public class CategoryService:ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public Task<Category> CreateCategoryAsync(Category category)
        {
            return categoryRepository.CreateCategoryAsync(category);
        }

        public async Task<CategoryImage> CreateCategoryImageAsync(byte[] fileBytes, short categoryId, string mimeType)
        {
            CategoryImage categoryImage = new CategoryImage()
            {
                CategoryId = categoryId,
                Image = fileBytes,
                Mime = mimeType


            };
            return await categoryRepository.CreateCategoryImageAsync(categoryImage);
        }

        public Task<bool> DeleteCategoryAsync(short categoryId)
        {
            return categoryRepository.DeleteCategoryAsync(categoryId);
        }

        public async Task<Category> GetCategoryAndImagesAsync(short categoryId)
        {
            return await categoryRepository.GetCategoryAndImagesAsync(categoryId);
        }

        public Task<Category> GetCategoryAsync(short categoryId)
        {
            return categoryRepository.GetCategoryAsync(categoryId);
        }

        public Task<List<Category>> GetCategorysAsync()
        {
            return categoryRepository.GetCategorysAsync();
        }

        public async Task<bool> IsCategoryExistAsync(string name)
        {
            var category = await categoryRepository.GetCategoryAsync(name);
            return category != null;
        }

        public Task<Category> UpdateCategoryAsync(Category category)
        {
            return categoryRepository.UpdateCategoryAsync(category);
        }
       
    }
}
