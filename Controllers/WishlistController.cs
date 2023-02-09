using Application.API.Models;
using Application.API.Repository.Abstruct;
using Application.API.ViewModel.Create;
using Application.API.ViewModel.Get;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistItemService wishlistService;
        public WishlistController(IWishlistItemService wishlistService)
        {
            this.wishlistService = wishlistService;
        }
        [HttpGet("all", Name = "GetWishlists")]
        [ProducesResponseType(typeof(List<WishlistItemViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWishlistItemsAsync()
        {
            var adObjName = "Admin";
           

            var wishlists = await wishlistService.GetWishlistItemsAsync(adObjName);

            var wishlistsViewModel = wishlists.Select(s => new WishlistItemViewModel()
            {
                OwnerADObjectId = s.OwnerAdobject,
                ProductId =Convert.ToInt32(s.ProductId),
                Id = s.Id
            }).ToList();

            return Ok(wishlistsViewModel);
        }
        [HttpPost("", Name = "CreateWishlist")]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(ModelStateDictionary), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostWishlistAsync([FromBody] CreateWishlistItem createWishlistItem)
        {
           
            var wishListInDB = await wishlistService.GetWishlistItemAsync("Admin",
                createWishlistItem.ProductId);

            if (wishListInDB == null)
            {
                var entity = new WishlistItem()
                {
                    OwnerAdobject = "Admin",
                    ProductId = createWishlistItem.ProductId
                };

                var isSuccess = await wishlistService.CreateWishlistItemAsync(entity);
                return new CreatedAtRouteResult("GetWishlist",
                  new { id = entity.Id });
            }
            return new CreatedAtRouteResult("GetWishlist",
                   new { id = wishListInDB.Id });
        }
    }
}
