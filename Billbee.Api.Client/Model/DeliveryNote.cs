using System;

namespace Billbee.Api.Client.Model
{
    /// <summary>
    /// Contains information of a delivery note.
    /// </summary>
    public class DeliveryNote
    {
        /// <summary>
        /// User specific number of the order, that is used for this delivery note
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// Number of this delivery note
        /// </summary>
        public string DeliveryNoteNumber { get; set; }

        /// <summary>
        /// If requested, this field contains the delivery note as PDF file 
        /// </summary>
        public byte[] PDFData { get; set; }

        /// <summary>
        /// Date, on which the delivery note was issued
        /// </summary>
        public DateTime? DeliveryNoteDate { get; set; }

        /// <summary>
        /// Url to donwload the delivery note, when not self contained inside <<see cref="PDFData"/>
        /// </summary>
        public string PdfDownloadUrl { get; set; }
    }
}
