using System;

namespace Billbee.Api.Client.Model
{
    public class OrderPayment
    {
        public long BillbeeId { get; set; }
        public string TransactionId { get; set; }
        public DateTime PayDate { get; set; }
        public short? PaymentType { get; set; }
        public string SourceTechnology { get; set; }
        public string SourceText { get; set; }
        public decimal PayValue { get; set; }
        public string Purpose { get; set; }
        public string Name { get; set; }
    }
}