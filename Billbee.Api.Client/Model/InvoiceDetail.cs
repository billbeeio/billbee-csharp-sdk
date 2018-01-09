using System;
using System.Collections.Generic;
using Billbee.Api.Client.Enums;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Detail information for an invoice
    /// </summary>
    public class InvoiceDetail
    {
        /// <summary>
        /// Number that is printed as invoice number on the invoice
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Defines, wether this is an invoice or a creditnote
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Invoice address last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Invoice address first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Invoice address company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Customer number of the addressed customer
        /// </summary>
        public int CustomerNumber { get; set; }

        /// <summary>
        /// If applicable, the debtor number of the customer
        /// </summary>
        public int DebtorNumber { get; set; }

        /// <summary>
        /// Date of the invoice
        /// </summary>
        public DateTime InvoiceDate { get; set; }

        /// <summary>
        /// Total net value of this invoice
        /// </summary>
        public decimal TotalNet { get; set; }

        /// <summary>
        /// Definition of the currency this invoice ist based upon
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Total gross value of this invoice
        /// </summary>
        public decimal TotalGross { get; set; }

        /// <summary>
        /// Type id of payment, the order was or is planned to be payed
        /// </summary>
        public byte? PaymentTypeId { get; set; }

        /// <summary>
        /// The number of the corresponding order
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// If applicable, the id of the payment transaction
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// Contact e-mail address of the customer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The name of the shop, the original order came from
        /// </summary>
        public string ShopName { get; set; }

        /// <summary>
        /// All positions, that belong to this invoice <<see cref="InvoicePosition"/>
        /// </summary>
        public List<InvoicePosition> Positions { get; set; }

        /// <summary>
        /// Date date, when this order was payed. If null, the order has not been payed for yet.
        /// </summary>
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// The vat mode, that is applicable for this invoice
        /// </summary>
        public VatModeEnum VatMode { get; set; }

        /// <summary>
        /// The unique internal id of this invoice
        /// </summary>
        public int BillbeeId { get; set; }
    }
}
