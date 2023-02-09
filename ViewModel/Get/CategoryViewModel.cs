using Application.API.Validation;
using System.Collections.Generic;

namespace Application.API.ViewModel.Get
{
    public class CategoryViewModel:AbstractValidatableObject
    {
        public short Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public List<CategoryImagesViewModel> CategoryImagesViewModels { get; set; }
    }
}
