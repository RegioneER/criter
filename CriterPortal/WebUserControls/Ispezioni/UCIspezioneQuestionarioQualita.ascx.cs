using Bender.Reflection;
using DataLayer;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControls_Ispezioni_UCIspezioneQuestionarioQualita : System.Web.UI.UserControl
{
    public string IDIspezioneVisita
    {
        get { return lblIDIspezioneVisita.Text; }
        set
        {
            lblIDIspezioneVisita.Text = value;
        }
    }

    public string IDIspezioneSelected
    {
        get { return lblIDIspezioneSelected.Text; }
        set
        {
            lblIDIspezioneSelected.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDatiQuestionarioQualita();
        }
    }

    protected void GetDatiQuestionarioQualita()
    {
        using (var ctx = new CriterDataModel())
        {
            var IDVisita = int.Parse(IDIspezioneVisita);
            //var result = from v in ctx.VER_Ispezione
            //             .Include("V_VER_IspezioneQuestinarioQualita")
            //             where (v.IDIspezioneVisita == IDVisita)
            //             select v;

            //var result = (from VER_IspezioneQuestinarioQualita in ctx.VER_IspezioneQuestinarioQualita
            //              join VER_Ispezione in ctx.VER_Ispezione on VER_IspezioneQuestinarioQualita.IDIspezione equals VER_Ispezione.IDIspezione into VER_Ispezione_join
            //              from VER_Ispezione in VER_Ispezione_join.DefaultIfEmpty()
            //              where (VER_Ispezione.IDIspezioneVisita == IDVisita)
            //             select new
            //             {
            //                 IDIspezione = VER_Ispezione.IDIspezione,
            //                 CodiceIspezione = VER_Ispezione.CodiceIspezione,
            //                 IsDefinitivo = VER_IspezioneQuestinarioQualita.IsDefinitivo,
            //                 DataUltimaModifica = VER_IspezioneQuestinarioQualita.DataUltimaModifica,
            //                 //UtenteUltimaModifica = VER_IspezioneQuestinarioQualita.UtenteUltimaModifica
            //             }).ToList();

            var result = (from VER_Ispezione in ctx.VER_Ispezione
                          join V_VER_IspezioneQuestinarioQualita in ctx.V_VER_IspezioneQuestinarioQualita on VER_Ispezione.IDIspezione equals V_VER_IspezioneQuestinarioQualita.IDIspezione into V_VER_IspezioneQuestinarioQualita_join
                          from V_VER_IspezioneQuestinarioQualita in V_VER_IspezioneQuestinarioQualita_join.DefaultIfEmpty()
                          where (VER_Ispezione.IDIspezioneVisita == IDVisita)
                          select new
                          {
                             IDIspezione = VER_Ispezione.IDIspezione,
                             CodiceIspezione = VER_Ispezione.CodiceIspezione,
                             IsDefinitivo = V_VER_IspezioneQuestinarioQualita.IsDefinitivo == null ? false : V_VER_IspezioneQuestinarioQualita.IsDefinitivo,
                             DataUltimaModifica = (DateTime?)V_VER_IspezioneQuestinarioQualita.DataUltimaModifica,
                             UtenteUltimaModifica = V_VER_IspezioneQuestinarioQualita.UtenteUltimaModifica
                          }).ToList();


            //var QuestionariIspezioni = result.ToList();

            DataGrid.DataSource = result;
            DataGrid.DataBind();
        }
    }

    protected void DataGrid_HtmlRowCreated(object sender, DevExpress.Web.ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            //Image imgDocumentoCompilato = DataGrid.FindRowCellTemplateControl(e.VisibleIndex, null, "imgDocumentoCompilato") as Image;
            //imgDocumentoCompilato.ImageUrl = UtilityApp.ToImage(IsCompilato);

            int IDIspezione = int.Parse((e.GetValue("IDIspezione").ToString()));
            ImageButton ImgPdf = DataGrid.FindRowCellTemplateControl(e.VisibleIndex, null, "ImgPdf") as ImageButton;
            if (int.Parse(lblIDIspezioneSelected.Text) == IDIspezione)
            {
                ImgPdf.Visible = true;
                ImgPdf.Attributes.Add("onclick", "OpenPopupWindowQuestionario(this, " + IDIspezione + "); return false;");
            }
            else
            {
                ImgPdf.Visible = false;
            }
        }
    }






}