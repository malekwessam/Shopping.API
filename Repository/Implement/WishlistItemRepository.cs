using Application.API.Models;
using Application.API.Repository.Abstruct;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.API.Repository.Implement
{
    public class WishlistRepository : IWishlistRepository
    {
        private readonly ApplicationContext DbContext;
        public WishlistRepository(ApplicationContext DbContext)
        {
            this.DbContext = DbContext;
        }
        public async Task<WishlistItem> CreateWishlistItemAsync(WishlistItem wishlistItem)
        {
            DbContext.WishlistItem.Add(wishlistItem);
            await DbContext.SaveChangesAsync();
            return wishlistItem;
        }

        public async Task<bool> DeleteWishlistItemAsync(long id)
        {
            var entityToDelete = await DbContext.WishlistItem.FindAsync(id);
            DbContext.WishlistItem.Remove(entityToDelete);
            return await DbContext.SaveChangesAsync() > 0;
        }

        public Task<WishlistItem> GetWishlistItemAsync(string adObjName, int productId)
        {
            return DbContext.WishlistItem.FirstOrDefaultAsync(f => f.ProductId == productId
            && f.OwnerAdobject == adObjName);
        }

        public Task<List<WishlistItem>> GetWishlistItemsAsync(string adObjName = "Admin")
        {
            return DbContext.WishlistItem.Where(w => w.OwnerAdobject == adObjName).ToListAsync();
        }

        public async Task<bool> IsWishlistItemExistAsync(long wishlistItemId)
        {
            var entity = await DbContext.WishlistItem.FindAsync(wishlistItemId);
            return entity != null;
        }

        public async  Task<bool> IsWishlistItemExistAsync(string ownerADObjectId, int productId)
        {
            var wishlistItem = await DbContext.WishlistItem.FirstOrDefaultAsync(f => f.ProductId == productId && f.OwnerAdobject == ownerADObjectId);
            return wishlistItem != null;
        }
    }
}
