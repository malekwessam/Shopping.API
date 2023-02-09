using Application.API.Models;
using Application.API.Repository.Abstruct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Implement
{
    public class WishlistItemService : IWishlistItemService
    {
        private readonly IWishlistRepository wishlistRepository;

        public WishlistItemService(IWishlistRepository wishlistRepository)
        {
            this.wishlistRepository = wishlistRepository;
        }
        public Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem)
        {
            return wishlistRepository.CreateWishlistItemAsync(wishlistItem);
        }

        public Task<bool> DeleteWishlistItemAsync(long id)
        {
            return wishlistRepository.DeleteWishlistItemAsync(id);
        }

        public Task<WishlistItem> GetWishlistItemAsync(string adObjName, int productId)
        {
            return wishlistRepository.GetWishlistItemAsync(adObjName, productId);
        }

        public Task<List<WishlistItem>> GetWishlistItemsAsync(string adName)
        {
            return wishlistRepository.GetWishlistItemsAsync(adName);
        }

        public Task<bool> IsWishlistItemExistAsync(long wishlistItemId)
        {
            return wishlistRepository.IsWishlistItemExistAsync(wishlistItemId);
        }

        public Task<bool> IsWishlistItemExistAsync(string ownerADObjectId, int productId)
        {
            return wishlistRepository.IsWishlistItemExistAsync(ownerADObjectId, productId);
        }
    }
}
