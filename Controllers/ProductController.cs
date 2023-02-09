using Application.API.Models;
using Application.API.Repository.Abstruct;
using Application.API.Repository.Implement;
using Application.API.ViewModel.Create;
using Application.API.ViewModel.Get;
using Application.API.ViewModel.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }
        // GET: api/<ProductController>

        [HttpGet("", Name = "GetProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<ProductViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            var products = await productService.GetProductsAsync();
            var models =products.Select(product => new ProductViewModel()
            {
                Id= product.Id,
                Name=product.ProductName,
                AvailableSince = product.AvailableSince,
                CategoryId = Convert.ToInt16(product.CategoryId),
                IsActive= product.IsActive,
                Descriptions=product.ProductDescription,
                ProductImagesViewModels = product.ProductImage.Any() ? product.ProductImage.Select(s => new ProductImagesViewModel()
                {
                    Id = s.Id,
                    Mime = s.Mime,
                    ProductId = Convert.ToInt16(s.ProductId),
                    Base64Image = Convert.ToBase64String(s.Image)

                }).ToList() : new List<ProductImagesViewModel>()

            }).ToList();
            return Ok(models);
        }

        // GET api/<ProductController>/5

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProductViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(int id)
        {
            var product=    await productService.GetProductAndImagesAsync(id);   
            if(product==null)
                return NotFound();

            var model = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.ProductName,
                AvailableSince = product.AvailableSince,
                CategoryId = Convert.ToInt16(product.CategoryId),
                IsActive = product.IsActive,
                Descriptions = product.ProductDescription,
                ProductImagesViewModels = product.ProductImage.Any() ? product.ProductImage.Select(s => new ProductImagesViewModel()
                {
                    Id = s.Id,
                    Mime= s.Mime,
                    ProductId= Convert.ToInt16(s.ProductId),
                    Base64Image= Convert.ToBase64String(s.Image)

                }).ToList() : new List<ProductImagesViewModel>()
            };
            return Ok(model);
            
        }

        // POST api/<ProductController>
        [HttpPost("", Name = "CreateProduct")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post([FromBody] CreateProduct createProduct)
        {
            var entityToAdd = new Product()
            {

                ProductName = createProduct.Name,
                AvailableSince = createProduct.AvailableSince,
                CategoryId = createProduct.CategoryId,
                CreatedDate = DateTime.Now,
                ProductDescription = createProduct.Descriptions,
                IsActive = createProduct.IsActive

            };
            entityToAdd.ProductOwner = new ProductOwner() { OwnerAdobject = "Admin", OwnerName = "Admin" };
            var createdProduct =await productService.CreateProductAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new { Id = createProduct.Id });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}", Name = "UpdateProduct")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateProduct updateProduct)
        {
            var entityToUpdate = await productService.GetProductAsync(updateProduct.Id);


            entityToUpdate.Id= updateProduct.Id;
            entityToUpdate.ProductName = updateProduct.Name;
            entityToUpdate.AvailableSince = updateProduct.AvailableSince;
            entityToUpdate.CategoryId = updateProduct.CategoryId;
            entityToUpdate.ModifiedDate = DateTime.Now;
            entityToUpdate.Modifiedby = "Admin";
            entityToUpdate.ProductDescription = updateProduct.Descriptions;
            entityToUpdate.IsActive = updateProduct.IsActive;

        


        var updatedProduct = await productService.UpdateProductAsync(entityToUpdate);
            return Ok();
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await productService.GetProductAsync(id);
            if (product == null)
                return NotFound();

            var isSuccess = await productService.DeleteProductAsync(id);
            return Ok();
        }

        [HttpPost("upload/{id}", Name = "UploadProductImage")]
        public async Task<IActionResult> UploadProductImageAsync(IFormFile file, [FromRoute] int id)
        {
            if (!IsValidFile(file))
            {
                return BadRequest(new { message = "Invalid file extensions" });
            }

            byte[] fileBytes = null;
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                fileBytes = stream.ToArray();
            }

            var productImage = await productService.CreateProductImageAsync(fileBytes, id,  file.ContentType);
            return Ok(productImage.Id);
        }
        private bool IsValidFile(IFormFile file)
        {
            List<string> validFormats = new List<string>() { ".jpg", ".png", ".svg", ".jpeg" };
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return validFormats.Contains(extension);
        }

    }
}
