using Newtonsoft.Json;
using System;

namespace FeiniuBus.Test.Model
{
    public class ChargeHostory
    {
        [JsonProperty("adcode")]
        public string Adcode { get; set; }

        [JsonProperty("provider_id")]
        public string ProviderId { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("real")]
        public int Real { get; set; }

        [JsonProperty("discount")]
        public int Discount { get; set; }

        [JsonProperty("provider_privilege")]
        public int ProviderPrivilege { get; set; }

        [JsonProperty("charge_id")]
        public long ChargeId { get; set; }

        [JsonProperty("create_time")]
        public DateTime CreateTime { get; set; }

        [JsonProperty("succeed")]
        public bool Succeed { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("refund_amount")]
        public int RefundAmount { get; set; }

        [JsonProperty("order_id")]
        public string OrderId { get; set; }

        [JsonProperty("creator")]
        public long Creator { get; set; }

    }
}
