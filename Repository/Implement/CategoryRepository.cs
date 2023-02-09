using Application.API.Models;
using Application.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Implement
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly ApplicationContext DbContext;
        public CategoryRepository(ApplicationContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<Category> CreateCategoryAsync(Category category)
        {
            DbContext.Category.Add(category);
            await DbContext.SaveChangesAsync();
            return category;
        }

        public async Task<CategoryImage> CreateCategoryImageAsync(CategoryImage categoryImage)
        {
            var category = await GetCategoryAsync((short)Convert.ToUInt32(categoryImage.CategoryId));
            category.CategoryImage = new List<CategoryImage>() { categoryImage };

            await DbContext.SaveChangesAsync();
            return categoryImage;
        }

        public async Task<bool> DeleteCategoryAsync(short categoryId)
        {
            // this will return entity and that is tracked
            var categoryToRemove = await DbContext.Category.FindAsync(categoryId);
            DbContext.Category.Remove(categoryToRemove);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Task<Category> GetCategoryAndImagesAsync(short categoryId)
        {
            return DbContext.Category.AsNoTracking().Include(i => i.CategoryImage).FirstOrDefaultAsync(f => f.Id == categoryId);
        }

        public Task<Category> GetCategoryAsync(short categoryId)
        {
            return this.DbContext.Category.FindAsync(categoryId).AsTask();
        }

        public Task<Category> GetCategoryAsync(string name)
        {
            return this.DbContext.Category.FirstOrDefaultAsync(f => f.CategoryName.ToLower() == name.ToLower());
        }

        public Task<List<Category>> GetCategorysAsync()
        {
            return this.DbContext.Category.ToListAsync();
        }

        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            DbContext.Category.Update(category);
            await DbContext.SaveChangesAsync();
            return category;
        }
       
    }
}
