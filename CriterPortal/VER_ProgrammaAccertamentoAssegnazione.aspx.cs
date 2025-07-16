using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class VER_ProgrammaAccertamentoAssegnazione : System.Web.UI.Page
{
    protected string IdAccertamentoPacchetto
    {
        get
        {
            return (string)Request.QueryString["IdAccertamentoPacchetto"];
        }
    }

    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region RICERCA ACCERTATORI
    protected void ASPxComboBoxAccertatore_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var Accertatori = (from a in ctx.COM_AnagraficaSoggetti
                               join u in ctx.COM_Utenti on a.IDSoggetto equals u.IDSoggetto
                               where (u.IDRuolo == 6) // Accertatore
                               select new
                               {
                                   u.IDUtente,
                                   Accertatore = a.Nome + " " + a.Cognome
                               }).ToList();

            comboBox.DataSource = Accertatori;
        }

        comboBox.DataBind();
    }

    protected void ASPxComboBoxAccertatore_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        using (CriterDataModel ctx = new CriterDataModel())
        {
            var Accertatori = (from a in ctx.COM_AnagraficaSoggetti
                               join u in ctx.COM_Utenti on a.IDSoggetto equals u.IDSoggetto
                               where (u.IDRuolo == 6 && u.IDSoggetto == value) // Accertatore
                               select new
                               {
                                   u.IDUtente,
                                   Accertatore = a.Nome + " " + a.Cognome
                               }).ToList();

            comboBox.DataSource = Accertatori;
        }
        comboBox.DataBind();
    }

    protected void ASPxComboBoxAccertatore_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox2();
    }

    protected void RefreshAspxComboBox2()
    {
        ASPxComboBoxAccertatore.SelectedIndex = -1;
        using (CriterDataModel ctx = new CriterDataModel())
        {
            var Accertatori = (from a in ctx.COM_AnagraficaSoggetti
                               join u in ctx.COM_Utenti on a.IDSoggetto equals u.IDSoggetto
                               where (u.IDRuolo == 6) // Accertatore
                               select new
                               {
                                   u.IDUtente,
                                   Accertatore = a.Nome + " " + a.Cognome
                               }).ToList();

            ASPxComboBoxAccertatore.DataSource = Accertatori;
        }
        ASPxComboBoxAccertatore.DataBind();
    }

    #endregion

    protected void btnAssegnaPacchetto_Click(object sender, EventArgs e)
    {
        if (ASPxComboBoxAccertatore.Value != null)
        {
            long IdAccertamentoPacchettoLong = long.Parse(IdAccertamentoPacchetto);

            int IdAccertatore = int.Parse(ASPxComboBoxAccertatore.Value.ToString());
            UtilityVerifiche.AssegnaPacchettoDeiAccertamenti(IdAccertamentoPacchettoLong, info.IDUtente, IdAccertatore);
            ASPxComboBoxAccertatore.Value = null;
            rowError.Visible = false;

            Session["RefreshParentPage"] = true;

            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "popup_Hide", "setInterval(function(){parent.window.WindowProgrammaAccertamentoAssegnazione.Hide(); window.parent.location.reload();},1);", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "popup_Hide", "setInterval(function(){parent.window.WindowProgrammaAccertamentoAssegnazione.Hide(); parent.window.WindowProgrammaAccertamentoAssegnazione.PerformCallback();},1);", true);
                       
        }
        else
        {
            rowError.Visible = true;
        }
    }
}