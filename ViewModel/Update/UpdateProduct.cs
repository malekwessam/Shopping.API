using Application.API.Repository.Abstruct;
using Application.API.ViewModel.Create;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Application.API.ViewModel.Update
{
    public class UpdateProduct : CreateProduct
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(
           ValidationContext validationContext,
           CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();

            var productService = validationContext.GetService<IProductService>();
           

            var productEntity = await productService.GetProductAsync(Id);

            if (productEntity == null)
            {
                errors.Add(new ValidationResult($"No such product id {Id} exist", new[] { nameof(Id) }));
            }

            var productExist = await productService.IsProductExistAsync(Name, Id);
           
            if (productExist)
            {
                errors.Add(new ValidationResult($"Product with name {Name} exist, provide a different name", new[] { nameof(Name) }));
            }
            



            return errors;
        }
    }
}
