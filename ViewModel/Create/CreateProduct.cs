using Application.API.ViewModel.Get;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Application.API.Repository.Abstruct;
using Microsoft.Extensions.DependencyInjection;

namespace Application.API.ViewModel.Create
{
    public class CreateProduct : ProductViewModel
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
       CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var categoryService = validationContext.GetService<ICategoryService>();
            var productService = validationContext.GetService<IProductService>();

            var category = await categoryService.GetCategoryAsync(CategoryId);
            var isProductNameExist = await productService.IsProductNameExistAsync(Name);
            if (isProductNameExist)
            {
                errors.Add(new
                   ValidationResult($"Product with name {Name} exist, provide a different name", new[] { nameof(Name) }));
            }
            if (category == null)
            {
                errors.Add(new ValidationResult($"Category id {CategoryId} doesn't exist", new[] { nameof(CategoryId) }));
            }

            return errors;
        }
    }
}
