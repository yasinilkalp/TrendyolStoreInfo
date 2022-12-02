namespace TrendyolStoreInfo.Models
{
    public class TrendyolStore
    {

        public TrendyolStore(string sellerId, string storeName, string score, string location, string productCount, string deliveryTimeToCargo, string questionAnswerRate)
        {
            SellerId = sellerId;
            StoreName = storeName;
            Score = score;
            Location = location;
            ProductCount = productCount;
            DeliveryTimeToCargo = deliveryTimeToCargo;
            QuestionAnswerRate = questionAnswerRate;
        }

        public string SellerId { get; set; }
        public string StoreName { get; set; }
        public string Score { get; set; }
        public string Location { get; set; }
        public string ProductCount { get; set; }
        public string DeliveryTimeToCargo { get; set; }
        public string QuestionAnswerRate { get; set; }
    }
}
