using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using EntityDataSourceChangingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceChangingEventArgs;
using EntityDataSourceSelectingEventArgs = Microsoft.AspNet.EntityDataSource.EntityDataSourceSelectingEventArgs;

public partial class RCT_BollinoCalorePulito : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            PagePermission();
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
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "2", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "2", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox_OnSelectedIndexChanged(object sender, EventArgs e)
    {
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
        ASPxComboBox2.SelectedIndex = -1;
        ASPxComboBox2.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "0", string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        ASPxComboBox2.DataBind();
    }
    #endregion

    #region RICERCA PERSONE AZIENDA
    protected void ASPxComboBox2_OnItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
    {
        ASPxComboBox comboBox = (ASPxComboBox) source;
        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", "", string.Format("%{0}%", e.Filter), (e.BeginIndex + 1).ToString(), (e.EndIndex + 1).ToString());
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_OnItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
    {
        long value = 0;
        if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
            return;
        ASPxComboBox comboBox = (ASPxComboBox) source;

        comboBox.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggettiRequestedByValue(false, "1", e.Value.ToString(), "");
        comboBox.DataBind();
    }

    protected void ASPxComboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        ASPxGridViewBollini.DataBind();
    }

    #endregion

    #region New release
    protected void dsBollini_Selecting(object sender, Microsoft.AspNet.EntityDataSource.EntityDataSourceSelectingEventArgs e)
    {
        if (ASPxComboBox1.Value != null && ASPxComboBox2.Value != null)
        {
            e.DataSource.WhereParameters["IDSoggetto"].DefaultValue = ASPxComboBox1.Value.ToString();
            e.DataSource.WhereParameters["IDSoggettoDerived"].DefaultValue = ASPxComboBox2.Value.ToString();
        }
    }

    protected void ASPxGridViewBollini_DataBound(object sender, EventArgs e)
    {
        ASPxGridView grid = sender as ASPxGridView;
        using (var ctx = new CriterDataModel())
        {
            for (int i = 0; i < grid.VisibleRowCount; i++)
            {
                var IDBollinoCalorePulito = Convert.ToInt32(grid.GetRowValues(i, new string[] { "IDBollinoCalorePulito" }));

                int iDSoggettoDerived = 0;
                if (ASPxComboBox2.Value != null)
                {
                    iDSoggettoDerived = Convert.ToInt32(ASPxComboBox2.Value.ToString());
                    var bollini = ctx.RCT_BollinoCalorePulito.Where(a => a.IDSoggettoDerived == iDSoggettoDerived && a.IDBollinoCalorePulito == IDBollinoCalorePulito).FirstOrDefault();
                    if (bollini != null)
                    {
                        grid.Selection.SelectRow(i);                        
                    }
                }
            }
        }
    }

    protected void ASPxGridViewBollini_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "UpdateSelectedBollini")
        {
            List<long> valoriBollini = ASPxGridViewBollini.GetSelectedFieldValues("IDBollinoCalorePulito").Cast<long>().ToList();
            
            UtilityBollini.SaveInsertDeleteDatiAssegnazioneBollini(int.Parse(ASPxComboBox2.Value.ToString()), valoriBollini.ToArray<long>());
            ASPxGridViewBollini.DataBind();
        }
    }

    protected void dsBolliniModel_ContextDisposing(object sender, Microsoft.AspNet.EntityDataSource.EntityDataSourceContextDisposingEventArgs e)
    {
        
    }

    protected void dsBolliniModel_ContextCreating(object sender, Microsoft.AspNet.EntityDataSource.EntityDataSourceContextCreatingEventArgs e)
    {
        
    }

    #endregion




}