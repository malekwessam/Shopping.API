using Application.API.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.API.ViewModel.Get
{
    public class ProductViewModel: AbstractValidatableObject
    {
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MinLength(100)]
        public string Descriptions { get; set; }
        public DateTime AvailableSince { get; set; }
        public bool IsActive { get; set; }
        public short CategoryId { get; set; }
        public List<ProductImagesViewModel> ProductImagesViewModels { get; set; }
    }
}
