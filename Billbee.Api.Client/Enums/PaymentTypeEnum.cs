using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billbee.Api.Client.Enums
{
    public enum PaymentTypeEnum
    {
        Bankueberweisung = 1,
        Nachnahme = 2,
        PayPal = 3,
        Barzahlung = 4,
        Gutschein = 6,
        Sofortüberweisung = 19,
        MoneyOrder = 20, // etsy
        Check = 21, // etsy ck
        Andere = 22,
        Lastschrift = 23,
        Moneybookers = 24,
        KLARNA = 25,
        Rechnung = 26,
        Moneybookers_Kreditkarte = 27,
        Moneybookers_Lastschrift = 28,
        BILLPAY_Rechnung = 29,
        BILLPAY_Lastschrift = 30,
        Kreditkarte = 31,
        Maestro = 32,
        iDEAL = 33,
        EPS = 34,
        P24 = 35,
        ClickAndBuy = 36,
        GiroPay = 37,
        Novalnet_Lastschrift = 38,
        KLARNA_PartPayment = 39,
        iPayment_CC = 40,
        Billsafe = 41,
        Testbestellung = 42,
        WireCard_Kreditkarte = 43,
        AmazonPayments = 44,
        Secupay_Kreditkarte = 45,
        Secupay_Lastschrift = 46,
        WireCard_Lastschrift = 47,
        EC = 48,
        Paymill_Kreditkarte = 49,
        Novalnet_Kreditkarte = 50,
        Novalnet_Rechnung = 51,
        Novalnet_PayPal = 52,
        Paymill = 53,
        Rechnung_PayPal = 54,
        Selekkt = 55,
        Avocadostore = 56,
        DirectCheckout = 57,
        Rakuten = 58,
        Vorkasse = 59,
        Kommissionsabrechnung = 60,
        Amazon_Marketplace = 61,
        Amazon_Payments_Advanced = 62,
        Stripe = 63,
        BILLPAY_PayLater = 64,
        PostFinance = 65,
        iZettle = 66,
        SumUp = 67,
        payleven = 68,
        atalanda = 69,
        Saferpay_Kreditkarte = 70,
        WireCard_PayPal = 71,
        Micropayment = 72,
        Ratenkauf = 73,
        Wayfair = 74, // baranski
        MangoPay_PayPal = 75,
        MangoPay_Sofortueberweisung = 76,
        MangoPay_Kreditkarte = 77,
        MangoPay_iDeal = 78,
        PayPal_Express = 79,
        PayPal_Lastschrift = 80,
        PayPal_Kreditkarte = 81,
        Wish = 82,
        Bancontact_Mister_Cash = 83,
        Belfius_Direct_Net = 84,
        KBC_CBC_Betaalknop = 85
    }
}
