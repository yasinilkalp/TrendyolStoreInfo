using Microsoft.Playwright;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrendyolStoreInfo.Models;

namespace TrendyolStoreInfo.Services
{
    public class TrendyolScrapingService : ITrendyolScrapingService
    {
        #region Trendyol Store Info

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

        #endregion

        #region Trendyol Commission

        public async Task<IEnumerable<TrendyolCommission>> GetCommissions(string Email, string Password)
        {
            using IPlaywright playwright = await Playwright.CreateAsync();
            await using IBrowser browser = await playwright.Chromium.LaunchAsync(new() { Headless = false });
            IPage page = await browser.NewPageAsync();

            await LoginTrendyol(page, Email, Password);

            await page.GotoAsync("https://partner.trendyol.com/incentive");

            await page.SelectOptionAsync("div.change-size select", "50");

            await Task.Delay(1000);

            bool dataControl = true;
            List<TrendyolCommission> data = new();
            while (dataControl)
            {
                data.AddRange(await ReadTable(page));
                dataControl = await NextPage(page);
            }

            return data;
        }

        private async Task LoginTrendyol(IPage page, string Email, string Password)
        {
            await page.GotoAsync("https://partner.trendyol.com/account/login");
            await page.Locator("div.email-phone input").FillAsync(Email);
            await page.Locator("div.password input").FillAsync(Password);
            await page.Locator("button[type=submit]").ClickAsync();
            await Task.Delay(2000);
        }

        private async Task<List<TrendyolCommission>> ReadTable(IPage page)
        {
            List<TrendyolCommission> data = new();
            int RowCount = await page.Locator("div.g-table table tbody").Locator("tr").CountAsync();
            for (int i = 0; i < RowCount; i++)
            {
                var td = page.Locator("div.g-table table tbody").Locator("tr").Nth(i).Locator("td");
                data.Add(new TrendyolCommission()
                {
                    MainCategory = await td.Nth(0).InnerTextAsync(),
                    SubCategory = await td.Nth(1).InnerTextAsync(),
                    Commission = decimal.Parse((await td.Nth(2).InnerTextAsync()).Replace("% ", ""))
                });
            }
            return data;
        }

        private async Task<bool> NextPage(IPage page)
        {
            string CurrentPage = await page
                .Locator("div.pagination")
                .Locator("div.g-button-group")
                .Locator("button.-secondary")
                .InnerTextAsync();

            var NextButton = page.GetByRole(AriaRole.Button, new() { Name = (int.Parse(CurrentPage) + 1).ToString() });

            try
            {
                await NextButton.ClickAsync();
                await Task.Delay(100);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}