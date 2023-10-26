using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class PurchasesRepository : IPurchasesRepository
    {
        private readonly DataContext _context;

        public PurchasesRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CompletePurchaseAsync(Guid purchaseId, bool purchaseComplited)
        {
            var purchase = _context.Purchases.FirstOrDefault(x => x.Id == purchaseId);

            if (purchase == null)
            {
                throw new Exception("purchase with such id was not found");
            }

            purchase.IsComplited = purchaseComplited;

            await UpdatePurchaseAsync(purchase);
        }

        public async Task UpdatePurchaseAsync(PurchaseEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task CreatePurchaseAsync(PurchaseEntity purchase)
        {
            purchase.IsComplited = false;

            await _context.AddAsync(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task<PurchaseEntity?> GetByPaymentIdAsync(Guid paymentId)
        {
            var purchase = await _context.Purchases.Include(x=> x.PaymentLinksWithPurchase).FirstOrDefaultAsync(x => x.PaymentLinksWithPurchase.FirstOrDefault(x => x.Id == paymentId) != null);

            if (purchase != null)
            {
                return purchase;
            }
            else
            {
                return null;
            }
        }
    }
}
