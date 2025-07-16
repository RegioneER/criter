using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataUtilityCore
{
    public class UtilityFirmaDigitale
    {
        public static void InsertFirma(long iDRapportoControlloTecnico, string iDSoggetto,
                                                 string rdpPdf, string signerQualifier, string signerName,
                                                 string signerSurname, string signerIdentifier, string signerFullName, 
                                                 string signerAuthority, string signerRdpAuthority, string signerSerialNumber)
        {
            using (CriterDataModel ctx = new CriterDataModel())
            {
                var firma = new RCT_FirmaDigitale();
                firma.IDRapportoControlloTecnico = iDRapportoControlloTecnico;
                firma.IDSoggetto = int.Parse(iDSoggetto);
                firma.DataFirma = DateTime.Now;
                firma.RdpPdf = rdpPdf;
                firma.SignerQualifier = signerQualifier;
                firma.SignerName = signerName;
                firma.SignerSurname = signerSurname;
                firma.SignerIdentifier = signerIdentifier;
                firma.SignerFullName = signerFullName;
                firma.SignerAuthority = signerAuthority;
                firma.SignerCertificationAuthority = signerRdpAuthority;
                firma.SignerSerialNumber = signerSerialNumber;
                ctx.RCT_FirmaDigitale.Add(firma);
                ctx.SaveChanges();
            }
        }


    }
}
