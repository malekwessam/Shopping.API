using Application.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.API.Repository.Abstruct
{
    public interface IWishlistRepository
    {
        Task<List<WishlistItem>> GetWishlistItemsAsync(string adObjName = "Admin");
        Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem);
        Task<bool> IsWishlistItemExistAsync(long wishlistItemId);
        Task<bool> DeleteWishlistItemAsync(long id);
        Task<bool> IsWishlistItemExistAsync(string ownerADObjectId, int productId);
        Task<WishlistItem> GetWishlistItemAsync(string adObjName, int productId);
    }
}
