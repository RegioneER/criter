using DataLayer;
using DataUtilityCore;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COM_Questionario : System.Web.UI.Page
{
    protected string IDRapportoControlloTecnico
    {
        get
        {
            return (string)Request.QueryString["IDRapportoControlloTecnico"];
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetQuestionario(IDRapportoControlloTecnico);
        }
    }

    protected void GetQuestionario(string iDRapportoControlloTecnico)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            long iD = long.Parse(iDRapportoControlloTecnico);
            int? iDQuestionario = UtilityQuestionari.CreateSaveQuestionari(iD);

            var domande = ctx.COM_QuestionariDomande.Where(a => a.IDQuestionario == iDQuestionario).OrderBy(a => a.IDQuestionarioDomanda).ToList();
            dgQuestionario.DataSource = domande;
            dgQuestionario.DataBind();

            int risposteDate = 0;
            foreach (var domanda in domande)
            {
                var risposte = ctx.COM_QuestionariRisposte.Where(a => a.IDQuestionarioDomanda == domanda.IDQuestionarioDomanda && a.fRisposta == true).FirstOrDefault();
                if (risposte != null)
                {
                    risposteDate++;
                }
            }
            lblRisposteDate.Text = "Hai risposto a " + risposteDate.ToString() + " domande su un totale di " + domande.Count.ToString();

            var questionario = ctx.V_COM_Questionari.Where(a => a.IDQuestionario == iDQuestionario).FirstOrDefault();
            #region Dati Rapporto/Libretto
            string responsabile = string.Empty;
            if (!string.IsNullOrEmpty(questionario.NomeResponsabile) && !string.IsNullOrEmpty(questionario.CognomeResponsabile))
            {
                responsabile = questionario.NomeResponsabile + " " + questionario.CognomeResponsabile;
            }
            else
            {
                responsabile = questionario.RagioneSocialeResponsabile;
            }

            lblResponsabile.Text = responsabile;
            lblDataControllo.Text = String.Format("{0:dd/MM/yyyy}", questionario.DataControllo);
            lblImpresaManutentrice.Text = questionario.SoggettoAzienda;
            #endregion
            
            if (risposteDate == domande.Count)
            {
                if (questionario.IDStatoQuestionario == 1)
                {
                    btnCloseQuestionario.Visible = true;
                    btnSaveQuestionario.Visible = false;
                    btnCloseQuestionarioNonCompilato.Visible = false;
                }
                else
                {
                    btnCloseQuestionario.Visible = false;
                    btnSaveQuestionario.Visible = false;
                    btnCloseQuestionarioNonCompilato.Visible = false;
                    dgQuestionario.Enabled = false;
                }
            }
            else
            {
                if (questionario.IDStatoQuestionario == 3)
                {
                    btnCloseQuestionario.Visible = false;
                    btnSaveQuestionario.Visible = false;
                    btnCloseQuestionarioNonCompilato.Visible = false;
                    dgQuestionario.Enabled = false;
                }
                else
                {
                    btnCloseQuestionario.Visible = false;
                    btnSaveQuestionario.Visible = true;
                    if (risposteDate == 0)
                    {
                        btnCloseQuestionarioNonCompilato.Visible = true;
                    }
                    else
                    {
                        btnCloseQuestionarioNonCompilato.Visible = false;
                    }
                }               
            }
        }
    }

    //public void ControllaRisposteDate(Object sender, ServerValidateEventArgs e)
    //{
    //    bool fPass = true;
    //    for (int i = 0; (i <= (dgQuestionario.Items.Count - 1)); i++)
    //    {
    //        DataGridItem item = dgQuestionario.Items[i];
    //        string iDQuestionarioRisposta = ((RadioButtonList)(item.Cells[3].FindControl("rblRisposte"))).SelectedValue;

    //        if (string.IsNullOrEmpty(iDQuestionarioRisposta))
    //        {
    //            break;
    //        }
    //    }
    //    e.IsValid = fPass;
    //    cvRisposteDomande.ErrorMessage = "Ci sono domande a cui non si ha dato una risposta!";
    //}

    protected void GetRapportoControllo(string iDRapportoControlloTecnico)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {

        }
    }

    protected void GetRisposte(RadioButtonList rblRisposte, int iDQuestionarioDomanda)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var risposte = ctx.COM_QuestionariRisposte.Where(a => a.IDQuestionarioDomanda == iDQuestionarioDomanda).ToList();
            rblRisposte.DataValueField = "IDQuestionarioRisposta";
            rblRisposte.DataTextField = "Risposta";
            rblRisposte.DataSource = risposte;
            rblRisposte.DataBind();

            foreach (var risposta in risposte)
            {
                if (risposta.fRisposta)
                {
                    rblRisposte.SelectedValue = risposta.IDQuestionarioRisposta.ToString();
                }
            }
        }
    }

    public void dgQuestionario_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int iDQuestionarioDomanda = int.Parse(e.Item.Cells[0].Text);
            RadioButtonList rblRisposte = (RadioButtonList)(e.Item.Cells[3].FindControl("rblRisposte"));
            GetRisposte(rblRisposte, iDQuestionarioDomanda);
        }
    }

    public void dgQuestionarioSave()
    {
        for (int i = 0; (i <= (dgQuestionario.Items.Count - 1)); i++)
        {
            DataGridItem item = dgQuestionario.Items[i];
            string iDQuestionarioDomanda = item.Cells[0].Text;
            string iDQuestionarioRisposta = ((RadioButtonList)(item.Cells[3].FindControl("rblRisposte"))).SelectedValue;
            string note = ((TextBox)(item.Cells[4].FindControl("txtNoteRisposte"))).Text;

            dgQuestionarioDomandeSave(iDQuestionarioDomanda, note);
            dgQuestionarioRisposteSave(iDQuestionarioDomanda, iDQuestionarioRisposta);
        }
    }

    public void dgQuestionarioDomandeSave(string iDQuestionarioDomanda, string note)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int IDD = int.Parse(iDQuestionarioDomanda);

            var domanda = ctx.COM_QuestionariDomande.Where(a => a.IDQuestionarioDomanda == IDD).FirstOrDefault();
            if (!string.IsNullOrEmpty(note))
            {
                domanda.Note = note;
            }
            else
            {
                domanda.Note = null;
            }
            ctx.SaveChanges();
        }
    }

    public void dgQuestionarioRisposteSave(string iDQuestionarioDomanda, string iDQuestionarioRisposta)
    {
        using (CriterDataModel ctx = new CriterDataModel())
        {
            int IDD = int.Parse(iDQuestionarioDomanda);

            var risposte = ctx.COM_QuestionariRisposte.Where(a => a.IDQuestionarioDomanda == IDD).ToList();
            foreach (var risposta in risposte)
            {
                if (iDQuestionarioRisposta != string.Empty)
                {
                    int IDR = int.Parse(iDQuestionarioRisposta);
                    if (risposta.IDQuestionarioRisposta == IDR)
                    {
                        risposta.fRisposta = true;
                        ctx.SaveChanges();
                    }
                    else
                    {
                        risposta.fRisposta = false;
                        ctx.SaveChanges();
                    }
                }
                else
                {
                    risposta.fRisposta = false;
                    ctx.SaveChanges();
                }
            }           
        }       
    }

    public void SaveQuestionarioAll()
    {
        dgQuestionarioSave();
        GetQuestionario(IDRapportoControlloTecnico);
    }
    
    protected void btnSaveQuestionario_Click(object sender, EventArgs e)
    {
        SaveQuestionarioAll();
    }

    protected void btnCloseQuestionario_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SaveQuestionarioAll();

            using (CriterDataModel ctx = new CriterDataModel())
            {
                long iD = long.Parse(IDRapportoControlloTecnico);
                int? iDQuestionario = UtilityQuestionari.CreateSaveQuestionari(iD);

                var questionario = ctx.COM_Questionari.Where(a => a.IDQuestionario == iDQuestionario).FirstOrDefault();
                questionario.IDStatoQuestionario = 2;
                ctx.SaveChanges();
            }

            GetQuestionario(IDRapportoControlloTecnico);
        }
    }

    protected void btnCloseQuestionarioNonCompilato_Click(object sender, EventArgs e)
    {
        SaveQuestionarioAll();

        using (CriterDataModel ctx = new CriterDataModel())
        {
            long iD = long.Parse(IDRapportoControlloTecnico);
            int? iDQuestionario = UtilityQuestionari.CreateSaveQuestionari(iD);

            var questionario = ctx.COM_Questionari.Where(a => a.IDQuestionario == iDQuestionario).FirstOrDefault();
            questionario.IDStatoQuestionario = 3;
            ctx.SaveChanges();
        }

        GetQuestionario(IDRapportoControlloTecnico);
    }

}