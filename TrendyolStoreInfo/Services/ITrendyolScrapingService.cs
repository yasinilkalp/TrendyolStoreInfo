using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolStoreInfo.Models;

namespace TrendyolStoreInfo.Services
{
    public interface ITrendyolScrapingService
    {
        #region Trendyol Store Info

        Task<TrendyolStore> GetStore(string SellerId);
        Task<IEnumerable<TrendyolStore>> GetStores(string[] SellerIds);

        #endregion

        #region Trendyol Commission

        Task<IEnumerable<TrendyolCommission>> GetCommissions(string Email, string Password);

        #endregion

        #region Trendyol Claim Reasons

        Task<IEnumerable<TrendyolClaimReason>> GetClaimReasons();

        #endregion

        #region Trendyol Cargo Providers

        Task<IEnumerable<TrendyolCargoProvider>> GetCargoProviders();

        #endregion
    }
}
