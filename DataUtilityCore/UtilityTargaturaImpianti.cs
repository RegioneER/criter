using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting.BarCode;
using System.Web;
using System.Configuration;
using DataLayer;

namespace DataUtilityCore
{
    public class UtilityTargaturaImpianti
    {
        public static object GetCodiceTargaturaImpianto(int? iDSoggetto, int? iDSoggettoDerived, int? numeroTargature)
        {
            ObjectParameter codiceLottoOut = new ObjectParameter("codiceLottoOut", typeof(String));

            using (var ctx = new CriterDataModel())
            {
                //var db = DataLayer.Common.ApplicationContext.Current.Context;
                var sp = ctx.sp_SetCodiceTargatura(iDSoggetto, iDSoggettoDerived, numeroTargature, codiceLottoOut);

                return codiceLottoOut.Value + "-" + DateTime.Now.Year.ToString();
            }
        }

        public static string GetSqlValoriTargatureFilter(object iDSoggetto, object iDSoggettoDerived, object codiceLotto, object statoTargaturaCodiciImpianto, object codiceTargaturaImpianto)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ");
            strSql.Append(" * ");
            strSql.Append(" FROM V_LIM_TargatureImpianti ");
            strSql.Append(" WHERE 1=1");
            strSql.Append(" AND (fAttivoLibretto=1 OR fAttivoLibretto IS NULL) ");

            if ((iDSoggetto != "") && (iDSoggetto != "-1") && (iDSoggetto != null))
            {
                strSql.Append(" AND IDSoggetto=" + iDSoggetto);
            }

            if ((iDSoggettoDerived != "") && (iDSoggettoDerived != "-1") && (iDSoggettoDerived != null))
            {
                strSql.Append(" AND IDSoggettoDerived=" + iDSoggettoDerived);
            }

            if (!string.IsNullOrEmpty(Convert.ToString(codiceLotto)))//  (codiceLotto.ToString() != "") && (codiceLotto.ToString() != "0") && (codiceLotto != null))
            {
                string[] codiceLottoArray = codiceLotto.ToString().Split(new char[] { '-' });
                strSql.Append(" AND CodiceLotto = ");
                strSql.Append("'");
                strSql.Append(codiceLottoArray[0].ToString());
                strSql.Append("'");

                if (codiceLottoArray.GetLength(0) > 1)
                {
                    strSql.Append(" AND Anno = ");
                    strSql.Append("'");
                    strSql.Append(codiceLottoArray[1].ToString());
                    strSql.Append("'");
                }              
            }

            if ((codiceTargaturaImpianto!=null) && (codiceTargaturaImpianto.ToString() != ""))
            {
                strSql.Append(" AND CodiceTargatura = ");
                strSql.Append("'");
                strSql.Append(codiceTargaturaImpianto);
                strSql.Append("'");
            }

            switch (statoTargaturaCodiciImpianto.ToString())
            {
                case "0": //Visualizza tutti i codici targatura

                    break;
                case "1": //Visualizza i codici targatura liberi
                    strSql.Append(" AND IDLibrettoImpianto IS NULL ");
                    break;
                case "2": //Visualizza i codici targatura legati ad un libretto d'impianto
                    strSql.Append(" AND IDLibrettoImpianto IS NOT NULL ");
                    break;
            }

            strSql.Append(" ORDER BY DataInserimento DESC");
            return UtilityApp.SanitizeInput(strSql.ToString(), SanitizeType.Select);
        }

        public static string[] GetCodiciTargaturaImpiantoFromLotto(int? iDSoggetto, int? iDSoggettoDerived, object codiceLotto)
        {
            List<string> valoriCodiciTargature = new List<string>();

            //var db = DataLayer.Common.ApplicationContext.Current.Context;
            using (var ctx = new CriterDataModel())
            {
                string select = GetSqlValoriTargatureFilter(iDSoggetto, iDSoggettoDerived, codiceLotto, "0", null);
                var targhe = ctx.V_LIM_TargatureImpianti.SqlQuery(select).ToList();
                foreach (var targature in targhe)
                {
                    valoriCodiciTargature.Add(targature.CodiceTargatura);
                }
            }

            return valoriCodiciTargature.ToArray<string>();
        }

        public static void GetBarCodeUrl(string barcodenumber, string filename)
        {
            string newfile = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["UploadTargatureImpianti"].ToString() + "/" + filename + ".png");

            XRBarCode barcode = new XRBarCode();
            barcode.Symbology = new QRCodeGenerator();
            barcode.Text = barcodenumber;
            barcode.Width = 500;
            barcode.Height = 500;
            barcode.ShowText = false;
            barcode.AutoModule = true;
            barcode.BackColor = System.Drawing.Color.White;
            //barcode.BackColor = System.Drawing.ColorTranslator.FromHtml("#f5f2f2");
            //((Code128Generator)barcode.Symbology).CharacterSet = Code128Charset.CharsetB;
            ((QRCodeGenerator) barcode.Symbology).CompactionMode = QRCodeCompactionMode.Byte;
            ((QRCodeGenerator) barcode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator) barcode.Symbology).Version = QRCodeVersion.Version1;
            XtraReport report = new XtraReport();
            report.Bands.Add(new DetailBand());
            report.Bands[0].Controls.Add(barcode);
            report.CreateDocument();
            report.ExportToImage(newfile, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static bool CheckCodiceTargaturaImpianto(string CodiceTargaturaImpianto)
        {
            bool fExist = false;

            if (!string.IsNullOrEmpty(CodiceTargaturaImpianto))
            {
                using (var ctx = new CriterDataModel())
                {
                    fExist = ctx.LIM_TargatureImpianti.Where(c => c.CodiceTargatura == CodiceTargaturaImpianto).Any();
                }
            }
            
            return fExist;
        }
    }
}