using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
