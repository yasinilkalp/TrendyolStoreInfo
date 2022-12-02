using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolStoreInfo.Models;

namespace TrendyolStoreInfo.Services
{
    public interface ITrendyolStoreService
    {
        Task<TrendyolStore> GetStore(string SellerId);
        Task<IEnumerable<TrendyolStore>> GetStores(string[] SellerIds);
    }
}
