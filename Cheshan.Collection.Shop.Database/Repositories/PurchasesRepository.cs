using Cheshan.Collection.Shop.Database.Abstract;
using Cheshan.Collection.Shop.Database.Database;
using Cheshan.Collection.Shop.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cheshan.Collection.Shop.Database.Repositories
{
    public class PurchasesRepository : IPurchasesRepository
    {
        private readonly DataContext _context;


        private readonly TimeSpan _purchasesThresold;

        public PurchasesRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _purchasesThresold = TimeSpan.FromDays(1);
        }

        public async Task CompletePurchaseAsync(Guid purchaseId, bool purchaseComplited)
        {
            var purchase = await _context.Purchases.Include(x => x.PaymentLinksWithPurchase).Include(x => x.PurchasedProducts).FirstOrDefaultAsync(x => x.Id == purchaseId);

            if (purchase == null)
            {
                throw new ArgumentException("Purchase with such id was not found");
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
            purchase.Created = DateTime.UtcNow;
            await _context.AddAsync(purchase);
            await _context.SaveChangesAsync();
        }

        public async Task<PurchaseEntity?> GetByPaymentIdAsync(Guid paymentId)
        {
            var purchase = await _context.Purchases.Include(x => x.PaymentLinksWithPurchase).FirstOrDefaultAsync(x => x.PaymentLinksWithPurchase.FirstOrDefault(x => x.Id == paymentId) != null);

            if (purchase != null)
            {
                return purchase;
            }
            else
            {
                return null;
            }
        }

        public async Task<PurchaseEntity?> GetByIdAsync(Guid purchaseId)
        {
            var purchase = await _context.Purchases.Include(x => x.PaymentLinksWithPurchase).Include(x => x.PurchasedProducts).FirstOrDefaultAsync(x => x.Id == purchaseId);

            return purchase;
        }

        public async Task<IEnumerable<PurchaseEntity>> GetIncompleteRecent()
        {
            try
            {
                var recentIncomplete = await _context.Purchases.Include(x => x.PaymentLinksWithPurchase)
                                                               .Include(x => x.PurchasedProducts)
                                                               .Where(x => x.Created > (DateTime.UtcNow - _purchasesThresold) &&
                                                                           x.IsComplited == false &&
                                                                           x.PaymentType != "cash")
                                                               .ToListAsync();

                return recentIncomplete;
            }
            catch (Exception ex)
            {
                var oshibka = ex.Message;
                return Array.Empty<PurchaseEntity>();
            }
        }
    }
}
