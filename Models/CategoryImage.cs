using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Application.API.Models
{
    public partial class CategoryImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Mime { get; set; }
        public short? CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
