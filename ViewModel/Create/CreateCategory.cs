using Application.API.Repository.Abstruct;
using Application.API.ViewModel.Get;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Application.API.ViewModel.Create
{
    public class CreateCategory : CategoryViewModel
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
        CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var categoryService = validationContext.GetService<ICategoryService>();

            if (await categoryService.IsCategoryExistAsync(Name))
            {
                errors.Add(new ValidationResult($"Category name {Name} exist", new[] { nameof(Name) }));
            }

            return errors;

        }
    }
    }
