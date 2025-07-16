using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using DataUtilityCore;
using DevExpress.Web;
using System.Configuration;
using EncryptionQS;

public partial class LIM_TargatureImpianti : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityManager.CheckPagePermissions(HttpContext.Current.User.Identity.Name, "LIM_TargatureImpianti.aspx");

        if (!Page.IsPostBack)
        {
            PagePermission();
            ASPxComboBox1.Focus();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
                ASPxComboBox1.Visible = true;
                ASPxComboBox2.Visible = true;
                lblSoggetto.Visible = false;
                lblSoggettoDerived.Visible = false;               
                break;
            case "2": //Amministratore azienda
                ASPxComboBox1.Value = getVal[0].ToString();
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = true;
                GetComboBoxFilterByIDAzienda();
                
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
            case "3": //Operatore/Addetto
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = false;
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = true;
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox2.Value = getVal[0].ToString();
                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggettoDerived.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[0].ToString());      
                break;
            case "10": //Responsabile tecnico
                ASPxComboBox1.Value = getVal[2].ToString();
                ASPxComboBox1.Visible = false;
                ASPxComboBox2.Visible = true;
                GetComboBoxFilterByIDAzienda();

                lblSoggetto.Text = SecurityManager.GetUserDescriptionSoggetto(getVal[2].ToString());
                lblSoggetto.Visible = true;
                lblSoggettoDerived.Visible = false;
                break;
        }
    }

    #region RICERCA AZIENDE
    protected void ASPxComboBox_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        //using (CriterDataModel ctx = new CriterDataModel())
        //{
        //    if (e.Value != null)
        //    {
        //        int IdSoggetto = int.Parse(e.Value.ToString());
        //        ASPxComboBox comboBox = (ASPxComboBox)source;
        //        comboBox.DataSource = ctx.COM_AnagraficaSoggetti.Where(a => a.IDSoggetto == IdSoggetto).ToList();
        //        comboBox.DataBind();
        //    }
        //}
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }
    
    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
//        ASPxComboBox1_ButtonClick(ASPxComboBox1, null);
        RefreshAspxComboBox();
        GetComboBoxFilterByIDAzienda();
    }

    protected void GetComboBoxFilterByIDAzienda()
    {
        if (ASPxComboBox1.Value != null)
        {
            ASPxComboBox2.Text = "";
            ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", ASPxComboBox1.Value.ToString(), string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
            ASPxComboBox2.DataBind();
        }
    }

    protected void ASPxComboBox1_ButtonClick(object source, ButtonEditClickEventArgs e)
    {
        RefreshAspxComboBox();
    }

    protected void RefreshAspxComboBox()
    {
        //ASPxComboBox1.SelectedIndex = -1;
        //ASPxComboBox1.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti("2", "", "", string.Format("%{0}%", ""), (0 + 1).ToString(), (50 + 1).ToString());
        //ASPxComboBox1.DataBind();

        ASPxComboBox2.SelectedIndex = -1;
        ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "0", string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        ASPxComboBox2.DataBind();
    }
    #endregion

    #region RICERCA PERSONE AZIENDA
    protected void ASPxComboBox2_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox)source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox)source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "1", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    #endregion

    protected void LIM_TargatureImpianti_btnProcess_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int? iDSoggetto = UtilityApp.ParseNullableInt(ASPxComboBox1.Value.ToString());
            int? iDSoggettoDerived = null;
            if (ASPxComboBox2.Value != null)
            {
                iDSoggettoDerived = UtilityApp.ParseNullableInt(ASPxComboBox2.Value.ToString());
            }
            int? numeroTargature = UtilityApp.ParseNullableInt(txtNumeroTargature.Text);
            object codiceLotto = UtilityTargaturaImpianti.GetCodiceTargaturaImpianto(iDSoggetto, iDSoggettoDerived, numeroTargature);
            
            string[] valoriCodiciTargaturaImpiantoFromLotto = UtilityTargaturaImpianti.GetCodiciTargaturaImpiantoFromLotto(iDSoggetto, iDSoggettoDerived, codiceLotto);
            
            for (int i = 0; i < valoriCodiciTargaturaImpiantoFromLotto.Length; i++)
            {
                UtilityTargaturaImpianti.GetBarCodeUrl(valoriCodiciTargaturaImpiantoFromLotto[i].ToString(), valoriCodiciTargaturaImpiantoFromLotto[i].ToString());
            }
            rowCreateOk.Visible = true;
            Logger.LogUser(TipoEvento.ModificaDati, UtilityApp.GetCurrentPageName(), HttpContext.Current.User.Identity.Name, HttpContext.Current.Request.UserHostAddress);

            QueryString qs = new QueryString();
            qs.Add("IDSoggetto", iDSoggetto.ToString());
            qs.Add("IDSoggettoDerived", iDSoggettoDerived.ToString());
            qs.Add("codiceLotto", codiceLotto.ToString());
            QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

            string url = "LIM_TargatureImpiantiSearch.aspx";
            url += qsEncrypted.ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "success", "setInterval(function(){location.href='" + url + "';},2000);", true);
        }
    }

    

}