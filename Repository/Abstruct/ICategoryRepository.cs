using Application.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Abstruct
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(string name);
        
        Task<Category> GetCategoryAsync(short categoryId);
        Task<List<Category>> GetCategorysAsync();
        Task<Category> CreateCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(short categoryId);
        Task<CategoryImage> CreateCategoryImageAsync(CategoryImage categoryImage);
        Task<Category> GetCategoryAndImagesAsync(short categoryId);
    }
}
