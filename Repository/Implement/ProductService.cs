using Application.API.Models;
using Application.API.Repository.Abstruct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Implement
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Task<Product> CreateProductAsync(Product product)
        {
            return productRepository.CreateProductAsync(product);

        }

        public async Task<ProductImage> CreateProductImageAsync(byte[] fileBytes, int productId, string mimeType)
        {
            ProductImage productImage = new ProductImage()
            {
                ProductId = productId,
                Image = fileBytes,
                Mime = mimeType
               
               
            };
            return await productRepository.CreateProductImageAsync(productImage);
        }

        public Task<bool> DeleteProductAsync(int productId)
        {
           return productRepository.DeleteProductAsync(productId);
        }

        public Product GetProduct(int productId)
        {
            return productRepository.GetProduct(productId);
        }

        public Task<Product> GetProductAndImagesAsync(int productId)
        {
           return productRepository.GetProductAndImagesAsync(productId);
        }

        public Task<Product> GetProductAsync(int productId)
        {
            return productRepository.GetProductAsync(productId);
        }

        public List<Product> GetProducts(int noOfProducts = 100)
        {
            return productRepository.GetProducts(noOfProducts);
        }

        public Task<List<Product>> GetProductsAsync(int noOfProducts = 100)
        {
            return productRepository.GetProductsAsync(noOfProducts);
        }

        public async Task<bool> IsProductExistAsync(string name, int id)
        {
            var product = await productRepository.GetProductByNameAsync(name, id);
            return product != null;
        }

        public async Task<bool> IsProductNameExistAsync(string name)
        {
            var product=await productRepository.GetProductByNameAsync(name);
            return product != null;
        }

        public Task<Product> UpdateProductAsync(Product product)
        {
            return productRepository.UpdateProductAsync(product);
        }
    }
}
