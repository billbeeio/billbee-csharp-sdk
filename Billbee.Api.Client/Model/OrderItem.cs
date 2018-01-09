using System.Collections.Generic;

namespace Billbee.Api.Client.Model
{
    public class OrderItem
    {
        /// <summary>
        /// Id der Einzeltransaktion. Wird nur von Ebay benötigt, um zusammengefasste Bestellungen zu erkennen  OR  Id of the individual transaction. Only required by Ebay to detect aggregated orders
        /// </summary>
        public string TransactionId { get; set; }

        public OrderItemProduct Product { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TaxAmount { get; set; }
        public byte? TaxIndex { get; set; }

        /// <summary>
        /// Sets the discount in percent
        /// </summary>
        public decimal Discount { get; set; }

        public List<OrderItemAttribute> Attributes { get; set; }
        public bool GetPriceFromArticleIfAny { get; set; }

        /// <summary>
        /// Legt fest, ob es sich bei dieser Position um einen Gutschein handelt, der als umsatzsteuerfreie Zahlung interpretiert wird OR Determines if it is a coupon, which is interpreted as tax-free payment
        /// </summary>
        public bool IsCoupon { get; set; }

        public string ShippingProfileId { get; set; }

        public override string ToString()
        {
            return $"Q:{Quantity} TP:{TotalPrice} Tax:{TaxIndex} Discount:{Discount}";
        }

        /// <summary>
        /// If true, the import of this order won't adjust the stock level at billbee.
        /// </summary>
        /// <remarks>This is used for amazon refunds</remarks>
        public bool DontAdjustStock { get; set; }

        public int? BillbeeId { get; set; }
        public bool IsDiscount { get; set; }
    }
}
