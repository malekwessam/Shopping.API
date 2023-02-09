using Application.API.Repository.Abstruct;
using Application.API.ViewModel.Get;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace Application.API.ViewModel.Create
{
    public class CreateWishlistItem: WishlistItemViewModel
    {
        public override async Task<IEnumerable<ValidationResult>> ValidateAsync(ValidationContext validationContext,
    CancellationToken cancellation)
        {
            var errors = new List<ValidationResult>();
            var wishlistService = validationContext.GetService<IWishlistItemService>();

            if (await wishlistService.IsWishlistItemExistAsync(OwnerADObjectId, ProductId))
            {
                errors.Add(new ValidationResult($"Product id {ProductId} exist for owner {OwnerADObjectId}", new[] { nameof(ProductId) }));
            }

            return errors;

        }
    }
}
