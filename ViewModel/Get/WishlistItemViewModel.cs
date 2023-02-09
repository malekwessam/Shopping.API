using Application.API.Validation;
using System.ComponentModel.DataAnnotations;

namespace Application.API.ViewModel.Get
{
    public class WishlistItemViewModel: AbstractValidatableObject
    {
        public long Id { get; set; }
        public int ProductId { get; set; }
        [MaxLength(200)]
        public string OwnerADObjectId { get; set; } = "Admin";
    }
}
