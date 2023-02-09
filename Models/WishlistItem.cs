using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Application.API.Models
{
    public partial class WishlistItem
    {
        public long Id { get; set; }
        public int? ProductId { get; set; }
        public string OwnerAdobject { get; set; }

        public virtual Product Product { get; set; }
    }
}
