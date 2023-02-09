using Application.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Abstruct
{
    public interface IProductRepository
    {
        Product GetProduct(int productId);
        List<Product> GetProducts(int noOfProducts);

        Task<Product> GetProductAsync(int productId);
        Task<List<Product>> GetProductsAsync(int noOfProducts);

        Task<Product> CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> GetProductByNameAsync(string name, int id);
        Task<ProductImage> CreateProductImageAsync(ProductImage productImage);
        Task<Product> GetProductAndImagesAsync(int productId);
        
    }
}
