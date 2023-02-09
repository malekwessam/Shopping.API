using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Application.API.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductImage = new HashSet<ProductImage>();
            WishlistItem = new HashSet<WishlistItem>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime AvailableSince { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public int NumberOfQuantity { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Modifiedby { get; set; }
        public short? CategoryId { get; set; }
        public int? ProductOwnerId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ProductOwner ProductOwner { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<WishlistItem> WishlistItem { get; set; }
    }
}
