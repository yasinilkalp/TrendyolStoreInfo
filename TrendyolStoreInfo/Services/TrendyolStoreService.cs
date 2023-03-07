using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolStoreInfo.Models;

namespace TrendyolStoreInfo.Services
{
    public class TrendyolStoreService : ITrendyolStoreService
    {
        public async Task<TrendyolStore> GetStore(string SellerId)
        {
            using IPlaywright playwright = await Playwright.CreateAsync();
            await using IBrowser browser = await playwright.Chromium.LaunchAsync();
            IPage page = await browser.NewPageAsync();

            return await GetTrendyolStoreAsync(SellerId, page);
        }

        public async Task<IEnumerable<TrendyolStore>> GetStores(string[] SellerIds)
        {
            using IPlaywright playwright = await Playwright.CreateAsync();
            await using IBrowser browser = await playwright.Chromium.LaunchAsync();
            IPage page = await browser.NewPageAsync();
            List<TrendyolStore> response = new();
            foreach (var Id in SellerIds)
            {
                response.Add(await GetTrendyolStoreAsync(Id, page));
            }
            return response;
        }

        private async Task<TrendyolStore> GetTrendyolStoreAsync(string SellerId, IPage page)
        {
            await page.GotoAsync("https://www.trendyol.com/magaza/profil/x-m-" + SellerId);
            string StoreName = await GetLocator(page, ".seller-store__name");
            string StoreIcon = await page.Locator(".seller-icon").GetAttributeAsync("src");
            string Score = await GetLocator(page, ".score-actual");
            string RegisterTime = await GetLocator(page, ".seller-info-container__wrapper__text-container__value>>nth=0");
            string Location = await GetLocator(page, ".seller-info-container__wrapper__text-container__value>>nth=1");
            string ProductCount = await GetLocator(page, ".seller-info-container__wrapper__text-container__value>>nth=2");
            string DeliveryTimeToCargo = await GetLocator(page, ".seller-metrics-container__wrapper>>nth=0");
            string QuestionAnswerRate = await GetLocator(page, ".seller-metrics-container__wrapper>>nth=1");

            TrendyolStore response = new(SellerId, StoreName, StoreIcon, Score, RegisterTime, Location, ProductCount,
                DeliveryTimeToCargo.Replace("Kargoya Teslim Süresi\n", "").Replace("Ortalama Kargolama Süresi\n", ""),
                QuestionAnswerRate.Replace("Soru Cevaplama Oranı\n", ""),
                "https://www.trendyol.com/magaza/x-m-" + SellerId);

            return response;
        }

        private async Task<string> GetLocator(IPage page, string selector)
        {
            return await page.Locator(selector).InnerTextAsync();
        }
    }
}
