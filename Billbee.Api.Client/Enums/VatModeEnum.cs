namespace Billbee.Api.Client.Enums
{
    public enum VatModeEnum : byte
    {
        DisplayVat = 0,
        NoVatKleinunternehmer = 1,
        NoVatInnergemeinschaftlicheLieferung = 2,
        NoVatAusfuhrDrittland = 3,
        NoVatDifferenzbesteuerung = 4,
        LastEnumEntry = 5
    }
}
