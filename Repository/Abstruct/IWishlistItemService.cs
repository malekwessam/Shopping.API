using Application.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Abstruct
{
    public interface IWishlistItemService
    {
        Task<List<WishlistItem>> GetWishlistItemsAsync(string adName);
        Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem);
        Task<bool> IsWishlistItemExistAsync(long wishlistItemId);
        Task<bool> IsWishlistItemExistAsync(string ownerADObjectId, int productId);
        Task<bool> DeleteWishlistItemAsync(long id);
        Task<WishlistItem> GetWishlistItemAsync(string adObjName, int productId);
    }
}
