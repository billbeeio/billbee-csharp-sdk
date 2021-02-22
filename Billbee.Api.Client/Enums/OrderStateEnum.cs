namespace Billbee.Api.Client.Enums
{
    public enum OrderStateEnum
    {
        /// <summary>
        /// Ordered
        /// </summary>
        Bestellt = 1,

        /// <summary>
        /// Confirmed
        /// </summary>
        Bestaetigt = 2,

        /// <summary>
        /// Payment Received
        /// </summary>
        Zahlung_erhalten = 3,

        /// <summary>
        /// sent
        /// </summary>
        Versendet = 4,

        /// <summary>
        /// complaints
        /// </summary>
        Reklamation = 5,

        /// <summary>
        /// deleted
        /// </summary>
        Geloescht = 6,

        /// <summary>
        /// completed
        /// </summary>
        Abgeschlossen = 7,

        /// <summary>
        /// cancelled
        /// </summary>
        Storniert = 8,

        /// <summary>
        /// archived
        /// </summary>
        Archiviert = 9,

        /// <summary>
        /// rated
        /// </summary>
        Rated = 10,

        /// <summary>
        /// warning
        /// </summary>
        Mahnung = 11,

        /// <summary>
        /// warning2
        /// </summary>
        Mahnung2 = 12,

        /// <summary>
        /// packed
        /// </summary>
        Gepackt = 13,

        /// <summary>
        /// Offered 
        /// </summary>
        Angeboten = 14,

        /// <summary>
        /// reminder
        /// </summary>
        Zahlungserinnerung = 15,

        /// <summary>
        /// In sending process
        /// </summary>
        Im_Fulfillment = 16
    }
}
