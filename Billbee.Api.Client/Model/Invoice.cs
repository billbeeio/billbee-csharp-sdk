using System;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Short hand information for an invoice, including the invoice as PDF- Document.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// The number of the corresponding order
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Number that is printed as invoice number on the invoice
        /// </summary>
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Contains the PDF representation of the invoice, if it was requested.
        /// </summary>
        public byte[] PDFData { get; set; }

        /// <summary>
        /// Date of the invoice
        /// </summary>
        public DateTime? InvoiceDate { get; set; }

        /// <summary>
        /// Total gross value of this invoice
        /// </summary>
        public decimal TotalGross { get; set; }

        /// <summary>
        /// Total net value of this invoice
        /// </summary>
        public decimal TotalNet { get; set; }

        /// <summary>
        /// If the pdf file was not attached as byte[], it can be downloaded from this url
        /// </summary>
        public string PdfDownloadUrl { get; set; }
    }
}
