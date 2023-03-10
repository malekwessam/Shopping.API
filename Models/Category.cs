using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Application.API.Models
{
    public partial class Category
    {
        public Category()
        {
            CategoryImage = new HashSet<CategoryImage>();
            Product = new HashSet<Product>();
        }

        public short Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsAcTive { get; set; }

        public virtual ICollection<CategoryImage> CategoryImage { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
