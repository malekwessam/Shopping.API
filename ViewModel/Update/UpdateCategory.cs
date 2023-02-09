using Application.API.Repository.Abstruct;
using Application.API.ViewModel.Create;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Application.API.Repository.Implement;

namespace Application.API.ViewModel.Update
{
    public class UpdateCategory:CreateCategory
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
        CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var categoryService = validationContext.GetService<ICategoryService>();

            var category = await categoryService.GetCategoryAsync(Id);

            if (category == null)
            {
                errors.Add(new ValidationResult($"Category id {Id} does not exist", new[] { nameof(Id) }));
            }
           


            return errors;

        }
    }
}
