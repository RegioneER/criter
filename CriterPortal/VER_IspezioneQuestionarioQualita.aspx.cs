using DataUtilityCore;
using System;
using System.Linq;
using System.Configuration;
using System.Net;
using System.Web;
using DataLayer;
using Org.BouncyCastle.Asn1.IsisMtt.X509;

public partial class VER_IspezioniQuestionarioQualita : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected string IDIspezione
    {
        get
        {
            return (string)Request.QueryString["IDIspezione"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UtilityVerifiche.CreateIfNotExistQuestionariIspezioni(long.Parse(IDIspezione), (int)userInfo.IDUtente);
            GetQuestionariQualita(long.Parse(IDIspezione));
            Calculate();
        }
    }

    public void GetQuestionariQualita(long IDIspezione)
    {
        using (var ctx = new CriterDataModel())
        {
            var q = (from a in ctx.VER_IspezioneQuestinarioQualita
                     join b in ctx.VER_Ispezione on a.IDIspezione equals b.IDIspezione
                     where (a.IDIspezione == IDIspezione)
                     select new
                     {
                         IDIspezione = a.IDIspezione,
                         IDStatoIspezione = b.IDStatoIspezione,
                         DataIspezione = b.DataIspezione,
                         CompensoIspezione = b.CompensoIspezione,
                         CodiceIspezione = b.CodiceIspezione,
                         IsIspezioneSvolta = b.IsIspezioneSvolta,


                         IndiceTempisticheConclusioneVerifica = a.IndiceTempisticheConclusioneVerifica,
                         IndiceCompletezzaRvi = a.IndiceCompletezzaRvi,
                         IndiceReclami = a.IndiceReclami,
                         
                         IspezioneNonEffettuataOption = a.IspezioneNonEffettuataOption,
                         IsIspezioneEffettuataNoAnalisi = a.IsIspezioneEffettuataNoAnalisi,
                         IsIspezioneEffettuataRitardoConsegnaRvi = a.IsIspezioneEffettuataRitardoConsegnaRvi,
                         IsIspezioneEffettuataRitardoComunicazione = a.IsIspezioneEffettuataRitardoComunicazione,
                         IsIspezioneEffettuataRitardoAppuntamento = a.IsIspezioneEffettuataRitardoAppuntamento,
                         IsIspezioneEffettuataMancataDocumentazione = a.IsIspezioneEffettuataMancataDocumentazione,

                         IsIspezioneNonEffettuataMAI1 = a.IsIspezioneNonEffettuataMAI1,
                         IsIspezioneNonEffettuataMAI2 = a.IsIspezioneNonEffettuataMAI2,
                                                  
                         CostoFinale = a.CostoFinale,
                         TrimestreFatturazione = a.TrimestreFatturazione,

                         IsDefinitivo = a.IsDefinitivo,
                         IDIspettore = b.IDIspettore
                     }).FirstOrDefault();

            if (q != null)
            {
                if (q.IDIspettore != null)
                {
                    var IDUtenteIspettore = int.Parse(DataUtilityCore.SecurityManager.GetUserIDUtenteDaIDSoggetto(q.IDIspettore.ToString()));

                    var IspezioneChiusaIspettore = ctx.VER_IspezioneStato.Where(a => a.IDIspezione == IDIspezione && a.IDStatoIspezione == 4 && a.IDUtenteUltimaModifica == IDUtenteIspettore).OrderBy(a => a.Data).FirstOrDefault();

                    int DayChiusuraIspezione = 0;
                    if (IspezioneChiusaIspettore != null)
                    {
                        lblDataIspezioneConclusa.Text = ((DateTime)IspezioneChiusaIspettore.Data).ToString("dd/MM/yyyy");
                        if (q.DataIspezione != null)
                        {
                            DayChiusuraIspezione = (IspezioneChiusaIspettore.Data - (DateTime)q.DataIspezione).Days;
                        }
                        lblGiorniChiusuraIspezione.Text = DayChiusuraIspezione.ToString();
                    }
                }
                
                lblDataIspezione.Text = q.DataIspezione != null ? ((DateTime)q.DataIspezione).ToString("dd/MM/yyyy") : string.Empty;
                lblCompensoIspezione.Text = q.CompensoIspezione.ToString();


                imgIspezioneSvolta.ImageUrl = UtilityApp.BooleanFlagToImage(q.IsIspezioneSvolta);
                lblIsIspezioneSvolta.Text = q.IsIspezioneSvolta.ToString();
                VisibleHiddenIspezioneSvolta(q.IsIspezioneSvolta);

                if (q.IndiceTempisticheConclusioneVerifica != null)
                {
                    rblIndiceTempisticheConclusioneVerifica.SelectedValue = q.IndiceTempisticheConclusioneVerifica.ToString();
                }
                if (q.IndiceCompletezzaRvi != null)
                {
                    rblIndiceCompletezzaRvi.SelectedValue = q.IndiceCompletezzaRvi.ToString();
                }
                if (q.IndiceReclami != null)
                {
                    rblIndiceReclami.SelectedValue = q.IndiceReclami.ToString();
                }
                if (q.IspezioneNonEffettuataOption != null)
                {
                    rblIspezioneNonEffettuata.SelectedValue = q.IspezioneNonEffettuataOption.ToString();
                }
                
                chkIsEffettuataNoAnalisi.Checked = q.IsIspezioneEffettuataNoAnalisi;
                chkIsRitardoConsegna.Checked = q.IsIspezioneEffettuataRitardoConsegnaRvi;
                chkIsRitardoComunicazione.Checked = q.IsIspezioneEffettuataRitardoComunicazione;
                chkIsRitardoAppuntamento.Checked = q.IsIspezioneEffettuataRitardoAppuntamento;
                chkIsMancataDocumentazione.Checked = q.IsIspezioneEffettuataMancataDocumentazione;

                chkIsNonEffettuataMai1.Checked = q.IsIspezioneNonEffettuataMAI1;
                chkIsNonEffettuataMai2.Checked = q.IsIspezioneNonEffettuataMAI2;


                lblCompensoFinale.Text = q.CostoFinale != null ? q.CostoFinale.ToString() : "0";

                if (q.TrimestreFatturazione != null)
                {
                    var SelectedItemTrimestre = ddlTrimestreFatturazione.Items.FindByValue(q.TrimestreFatturazione.ToString());
                    
                    ddlTrimestreFatturazione.SelectedItem = SelectedItemTrimestre;
                }
                
                lblIsDefinitivo.Text = q.IsDefinitivo.ToString();
                LogicIsDefinitivoBozza(q.IsDefinitivo);
            }
        }
    }

    protected void LogicIsDefinitivoBozza(bool IsDefinitivo)
    {
        if (IsDefinitivo)
        {
            UtilityApp.SiDisableAllControls(tblInfoQuestionario);
            btnSaveQuestionario.Visible = false;
            btnSaveDefinitivo.Visible = false;
            btnSaveBozza.Visible = true;
        }
        else
        {
            btnSaveQuestionario.Visible = true;
            btnSaveBozza.Visible = false;
            btnSaveDefinitivo.Visible = true;
        }


        //switch (IDStatoIspezione)
        //{
        //    case 1: //Ricerca Ispettore

        //        break;
        //    case 2: //Ispezione da Pianificare

        //        break;
        //    case 3://Ispezione Pianificata

        //        break;
        //    case 4: //Ispezione Conclusa da Ispettore

        //        break;
        //    case 7: //Ispezione Pianificata confermata

        //        break;
        //    case 8: //Annullata

        //        break;
        //    case 5: //Ispezione Conclusa da Coordinatore con accertamento
        //    case 9: //Ispezione Conclusa da Coordinatore senza accertamento
        //    case 10: //Ispezione Conclusa da Coordinatore con doppio mancato accesso (avviso non recapitato)
        //    case 11: //Ispezione Conclusa da Coordinatore con utente sconosciuto
                
        //}
    }

    public void VisibleHiddenIspezioneSvolta(bool IsIspezioneSvolta)
    {
        if (IsIspezioneSvolta)
        {
            rowIspezioneNonEffettuataTitle.Visible = false;
            rowIspezioneNonEffettuata.Visible = false;
            rowIspezioneEffettuataTitle.Visible = true;
            rowIspezioneEffettuata1.Visible = true;
            rowIspezioneEffettuata2.Visible = true;
            rowIspezioneEffettuata3.Visible = true;
            rowIspezioneEffettuata4.Visible = true;
            rowIspezioneEffettuata5.Visible = true;
        }
        else
        {
            rowIspezioneNonEffettuataTitle.Visible = true;
            rowIspezioneNonEffettuata.Visible = true;
            rowIspezioneEffettuataTitle.Visible = false;
            rowIspezioneEffettuata1.Visible = false;
            rowIspezioneEffettuata2.Visible = false;
            rowIspezioneEffettuata3.Visible = false;
            rowIspezioneEffettuata4.Visible = false;
            rowIspezioneEffettuata5.Visible = false;
        }
    }


    protected void Calculate()
    {
        decimal CompensoInizialeIspezione = decimal.Parse(lblCompensoIspezione.Text);
        decimal CostoIspezioneNonSvolta = 0;
        decimal CostoIspezioneSvolta = 0;

        decimal CostoFinale = 0;

        bool IsIspezioneSvolta = bool.Parse(lblIsIspezioneSvolta.Text);

        if (!IsIspezioneSvolta)
        {
            switch (rblIspezioneNonEffettuata.SelectedValue)
            {
                case "1": //Ispezione non effettuata
                    CostoIspezioneNonSvolta = 35m;
                    break;
                case "2": //Ispezione non effettuata per generatore disattivato con presenza modulo disattivazione
                    CostoIspezioneNonSvolta = 50m;
                    break;
                case "3"://Ispezione non effettuata per generatore disattivato con assenza modulo disattivazione
                    CostoIspezioneNonSvolta = 35m;
                    break;
                case "4": //Ispezione non effettuata per doppia MAI
                    CostoIspezioneNonSvolta = 0m;
                    break;
                default: //

                    break;
            }
        }
        else
        {
            decimal Percentage = 0;

            decimal PercentageIsEffettuataNoAnalisi = chkIsEffettuataNoAnalisi.Checked == true ? 20m : 0;
            decimal PercentageIsRitardoConsegna = chkIsRitardoConsegna.Checked == true ? 10m : 0;
            decimal PercentageIsRitardoComunicazione = chkIsRitardoComunicazione.Checked == true ? 30m : 0;
            decimal PercentageIsRitardoAppuntamento = chkIsRitardoAppuntamento.Checked == true ? 10m : 0;
            decimal PercentageIsMancataDocumentazione = chkIsMancataDocumentazione.Checked == true ? 10m : 0;

            Percentage = (PercentageIsEffettuataNoAnalisi + PercentageIsRitardoConsegna + PercentageIsRitardoComunicazione + PercentageIsRitardoAppuntamento + PercentageIsMancataDocumentazione);

            CostoIspezioneSvolta = CompensoInizialeIspezione - ((Percentage / 100) * CompensoInizialeIspezione);
        }

        decimal CostoIsNonEffettuataMai1 = chkIsNonEffettuataMai1.Checked == true ? 35m : 0;
        decimal CostoIsNonEffettuataMai2 = chkIsNonEffettuataMai2.Checked == true ? 35m : 0;

        CostoFinale = CostoIspezioneNonSvolta + CostoIspezioneSvolta + CostoIsNonEffettuataMai1 + CostoIsNonEffettuataMai2;

        lblCompensoFinale.Text = CostoFinale.ToString();
    }

    protected void InputChanged(object sender, EventArgs e)
    {
        Calculate();
    }


    protected void btnSaveQuestionario_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long iDIspezione = long.Parse(IDIspezione);
                        
            UtilityVerifiche.UpdateQuestionariIspezioni(iDIspezione,
                                                        UtilityApp.ParseNullableInt(rblIndiceTempisticheConclusioneVerifica.SelectedValue),
                                                        UtilityApp.ParseNullableInt(rblIndiceCompletezzaRvi.SelectedValue),
                                                        UtilityApp.ParseNullableInt(rblIndiceReclami.SelectedValue),
                                                        UtilityApp.ParseNullableInt(rblIspezioneNonEffettuata.SelectedValue),
                                                        chkIsEffettuataNoAnalisi.Checked,
                                                        chkIsRitardoConsegna.Checked,
                                                        chkIsRitardoComunicazione.Checked,
                                                        chkIsRitardoAppuntamento.Checked,
                                                        chkIsMancataDocumentazione.Checked,
                                                        chkIsNonEffettuataMai1.Checked,
                                                        chkIsNonEffettuataMai2.Checked,
                                                        decimal.Parse(lblCompensoFinale.Text),
                                                        UtilityApp.ParseNullableInt(ddlTrimestreFatturazione.Value != null ? ddlTrimestreFatturazione.Value.ToString() : string.Empty),
                                                        (int)userInfo.IDUtente
                                                        );

            GetQuestionariQualita(iDIspezione);
        }
    }


    protected void btnSaveDefinitivoBozza_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            long IDIspezioneLong = long.Parse(IDIspezione);

            UtilityVerifiche.UpdateQuestionariIspezioni(IDIspezioneLong,
                                                            UtilityApp.ParseNullableInt(rblIndiceTempisticheConclusioneVerifica.SelectedValue),
                                                            UtilityApp.ParseNullableInt(rblIndiceCompletezzaRvi.SelectedValue),
                                                            UtilityApp.ParseNullableInt(rblIndiceReclami.SelectedValue),
                                                            UtilityApp.ParseNullableInt(rblIspezioneNonEffettuata.SelectedValue),
                                                            chkIsEffettuataNoAnalisi.Checked,
                                                            chkIsRitardoConsegna.Checked,
                                                            chkIsRitardoComunicazione.Checked,
                                                            chkIsRitardoAppuntamento.Checked,
                                                            chkIsMancataDocumentazione.Checked,
                                                            chkIsNonEffettuataMai1.Checked,
                                                            chkIsNonEffettuataMai2.Checked,
                                                            decimal.Parse(lblCompensoFinale.Text),
                                                            UtilityApp.ParseNullableInt(ddlTrimestreFatturazione.Value != null ? ddlTrimestreFatturazione.Value.ToString() : string.Empty),
                                                            (int)userInfo.IDUtente
                                                            );

            bool IsDefinitivo = bool.Parse(lblIsDefinitivo.Text);

            UtilityVerifiche.SetQuestionarioBozzaDefinitivo(IDIspezioneLong, !IsDefinitivo);

            Response.Redirect("VER_IspezioneQuestionarioQualita.aspx?IDIspezione=" + IDIspezioneLong);
        }
    }
}