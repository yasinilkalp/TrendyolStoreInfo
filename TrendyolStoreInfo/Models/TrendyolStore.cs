﻿namespace TrendyolStoreInfo.Models
{
    public class TrendyolStore
    {

        public TrendyolStore(string sellerId, string storeName, string storeImage, string score, string registerTime, string location, string productCount, string deliveryTimeToCargo, string questionAnswerRate, string storeUrl)
        {
            SellerId = sellerId;
            StoreName = storeName;
            StoreImage = storeImage;
            Score = score;
            RegisterTime = registerTime;
            Location = location;
            ProductCount = productCount;
            DeliveryTimeToCargo = deliveryTimeToCargo;
            QuestionAnswerRate = questionAnswerRate;
            StoreUrl = storeUrl;
        }

        public string SellerId { get; set; }
        public string StoreName { get; set; } 
        public string StoreImage { get; set; }
        public string Score { get; set; }
        public string RegisterTime { get; set; }
        public string Location { get; set; }
        public string ProductCount { get; set; }
        public string DeliveryTimeToCargo { get; set; }
        public string QuestionAnswerRate { get; set; }
        public string StoreUrl { get; set; }
    }
}
