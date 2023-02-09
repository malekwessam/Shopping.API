using Application.API.Models;
using Application.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.API.Repository.Implement
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext DbContext;
        public ProductRepository(ApplicationContext DbContext)
        {
            this.DbContext = DbContext;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            DbContext.Product.Add(product);
            await DbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteProductAsync(int productId)
        {
            var productToRemove = await DbContext.Product.FindAsync(productId);
            DbContext.Remove(productToRemove);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Product GetProduct(int productId)
        {
            return this.DbContext.Product.Find(productId);
        }

        public Task<Product> GetProductAsync(int productId)
        {
            return this.DbContext.Product.FindAsync(productId).AsTask();
        }

        public Task<Product> GetProductByNameAsync(string name)
        {
            return DbContext.Product.Include(i=>i.ProductImage).FirstOrDefaultAsync(f=>f.ProductName.ToLower()==name.ToLower());
        }

        public List<Product> GetProducts(int noOfProducts=100)
        {
            var products = this.DbContext.Product.AsNoTracking().Include(i=>i.ProductImage).OrderByDescending(x => x.CreatedDate)
                .Take(noOfProducts).ToList();
            return products;
        }

        public Task<List<Product>> GetProductsAsync(int noOfProducts=100)
        {
            var products = DbContext.Product.AsNoTracking().Include(i=>i.ProductImage).OrderByDescending(x => x.CreatedDate)
                 .Take(noOfProducts).ToListAsync();
            return products;
        }

        public Task<Product> GetProductByNameAsync(string name, int id)
        {
            return DbContext.Product.AsNoTracking().FirstOrDefaultAsync(f => f.ProductName.ToLower() == name.ToLower()&&f.Id!=id);
        }

        public async Task<Product> UpdateProductAsync(Product product)
        {
            DbContext.Product.Update(product);
            await DbContext.SaveChangesAsync();
            return product;
        }

        public async Task<ProductImage> CreateProductImageAsync(ProductImage productImage)
        {
            var product = await GetProductAsync(Convert.ToInt32(productImage.ProductId));
            product.ProductImage = new List<ProductImage>() { productImage };

            await DbContext.SaveChangesAsync();
            return productImage;
        }
        public Task<Product> GetProductAndImagesAsync(int productId)
        {
            return DbContext.Product.AsNoTracking().Include(i => i.ProductImage).FirstOrDefaultAsync(f => f.Id == productId);
        }

      
    }
}
