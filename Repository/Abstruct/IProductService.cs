using Application.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Abstruct
{
    public interface IProductService
    {
        Product GetProduct(int productId);
        List<Product> GetProducts(int noOfProducts=100 );

        Task<Product>GetProductAsync(int productId);
        Task<List<Product>> GetProductsAsync(int noOfProducts=100 );
        Task<Product>CreateProductAsync(Product product);
        Task<Product> UpdateProductAsync(Product product);
        Task<bool>DeleteProductAsync(int productId);
        Task<bool> IsProductNameExistAsync(string name);
        Task <bool>IsProductExistAsync(string name, int id);
        Task<ProductImage> CreateProductImageAsync(byte[] fileBytes,int productId,string mimeType);
        Task<Product> GetProductAndImagesAsync(int productId);
    }
}
