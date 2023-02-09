using Application.API.Models;
using Application.API.Repository.Abstruct;
using Application.API.Repository.Implement;
using Application.API.ViewModel.Create;
using Application.API.ViewModel.Get;
using Application.API.ViewModel.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet("", Name = "GetCategorys")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<CategoryViewModel>), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get()
        {
            var categorys = await categoryService.GetCategorysAsync();
            var models = categorys.Select(category => new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.CategoryName,
                
                
                IsActive = category.IsAcTive,
               
                CategoryImagesViewModels = category.CategoryImage.Any() ? category.CategoryImage.Select(s => new CategoryImagesViewModel()
                {
                    Id = s.Id,
                    Mime = s.Mime,
                    CategoryId = (short)Convert.ToInt16(s.CategoryId),
                    Base64Image = Convert.ToBase64String(s.Image)

                }).ToList() : new List<CategoryImagesViewModel>()

            }).ToList();
            return Ok(models);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CategoryViewModel), StatusCodes.Status200OK)]
        public async Task<ActionResult> Get(short id)
        {
            var category = await categoryService.GetCategoryAndImagesAsync(id);
            if (category == null)
                return NotFound();

            var model = new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.CategoryName,
                
                
                IsActive = category.IsAcTive,
                
                CategoryImagesViewModels = category.CategoryImage.Any() ? category.CategoryImage.Select(s => new CategoryImagesViewModel()
                {
                    Id = s.Id,
                    Mime = s.Mime,
                    CategoryId = Convert.ToInt16(s.CategoryId),
                    Base64Image = Convert.ToBase64String(s.Image)

                }).ToList() : new List<CategoryImagesViewModel>()
            };
            return Ok(model);
        }

        // POST api/<CategoryController>
        [HttpPost("", Name = "CreateCategory")]
        public async Task<ActionResult> Post([FromBody] CreateCategory createCategory)
        {
            var entityToAdd = new Category()
            {

                CategoryName = createCategory.Name,
                
              
                IsAcTive = createCategory.IsActive

            };
            
            var createdProduct = await categoryService.CreateCategoryAsync(entityToAdd);
            return new CreatedAtRouteResult("Get", new { Id = createCategory.Id });
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}", Name = "UpdateCategory")]
        public async Task<ActionResult> Put(short id, [FromBody] UpdateCategory updateCategory)
        {
            var entityToUpdate = await categoryService.GetCategoryAsync(updateCategory.Id);


            entityToUpdate.Id = updateCategory.Id;
            entityToUpdate.CategoryName = updateCategory.Name;
           
            entityToUpdate.IsAcTive = updateCategory.IsActive;




            var updatedCategory = await categoryService.UpdateCategoryAsync(entityToUpdate);
            return Ok();
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(short id)
        {
            var category = await categoryService.GetCategoryAsync(id);
            if (category == null)
                return NotFound();

            var isSuccess = await categoryService.DeleteCategoryAsync(id);
            return Ok();
        }
        [HttpPost("upload/{id}", Name = "UploadCategoryImage")]
        public async Task<IActionResult> UploadCategoryImageAsync(IFormFile file, [FromRoute] short id)
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

            var categoryImage = await categoryService.CreateCategoryImageAsync(fileBytes, id, file.ContentType);
            return Ok(categoryImage.Id);
        }
        private bool IsValidFile(IFormFile file)
        {
            List<string> validFormats = new List<string>() { ".jpg", ".png", ".svg", ".jpeg" };
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return validFormats.Contains(extension);
        }
    }
}
