using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Linq;
using Z.EntityFramework.Plus;

public partial class VER_ProgrammaIspezioneRegoleControlli : System.Web.UI.Page
{
    UserInfo userInfo = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected string calculate
    {
        get
        {
            return (string)Request.QueryString["calculate"];
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDefaultFilters();
            VisibleHiddenCalculate();

            if (UtilityVerifiche.GetDataRegoleControlli() != null)
            {
                lblDataUltimaAnalisi.Text = UtilityVerifiche.GetDataRegoleControlli().ToString();
            }
             
            if (!string.IsNullOrEmpty(calculate))
            {
                GetDataRegoleControlli();
            }
        }
    }

    public void VisibleHiddenCalculate()
    {
        if (!string.IsNullOrEmpty(calculate))
        {
            txtPotenzaR1.Enabled = false;
            txtPotenzaR2.Enabled = false;
            txtPotenzaR3.Enabled = false;
            txtPotenzaR4Da.Enabled = false;
            txtPotenzaR4A.Enabled = false;
            txtPotenzaR5.Enabled = false;

            //ddeCombustibileR1.Enabled = false;
            //ddeCombustibileR2.Enabled = false;
            //ddeCombustibileR3.Enabled = false;
            //ddeCombustibileR4.Enabled = false;
            //ddeCombustibileR5.Enabled = false;

            //ddeTipiGeneratoriR1.Enabled = false;
            //ddeTipiGeneratoriR2.Enabled = false;
            //ddeTipiGeneratoriR3.Enabled = false;
            //ddeTipiGeneratoriR4.Enabled = true;
            //ddeTipiGeneratoriR5.Enabled = true;
        }
        else
        {
            rowAggiornaRisultati.Visible = false;
            tblRisultatiRegoleControlli.Visible = false;

            chkRegolaR1.Visible = false;
            chkRegolaR2.Visible = false;
            chkRegolaR3.Visible = false;
            chkRegolaR4.Visible = false;
            chkRegolaR5.Visible = false;

            txtRegolaR1Percentage.Visible = false;
            txtRegolaR2Percentage.Visible = false;
            txtRegolaR3Percentage.Visible = false;
            txtRegolaR4Percentage.Visible = false;
            txtRegolaR5Percentage.Visible = false;
        }
    }

    public void BindDefaultFilters()
    {
        // R1-R5 - IDTipologiaGruppiTermici IN (1, 2) -- Gruppi termici singoli, Gruppi termici modulari
        //
        // R1, Default - IDTipologiaCombustibile  IN (2, 3, 4, 5, 12, 15) -- Gas naturale, Gpl extra rete, Gpl, Gasolio, Kerosene, Olio Combustibile
        HelpBindDefaultFilters((ASPxListBox)ddeCombustibileR1.FindControl("listBox"), new List<string>() { "2", "3", "4", "5", "12", "15" },
                               (ASPxListBox)ddeTipiGeneratoriR1.FindControl("listBox"), new List<string>() { "1", "2"});
        txtPotenzaR1.Text = "10";
        // R2, Default - IDTipologiaCombustibile  IN (2, 3, 15) -- Gas naturale, Gpl extra rete, Gpl
        HelpBindDefaultFilters((ASPxListBox)ddeCombustibileR2.FindControl("listBox"), new List<string>() { "2", "3", "15" },
                               (ASPxListBox)ddeTipiGeneratoriR2.FindControl("listBox"), new List<string>() { "1", "2" });
        txtPotenzaR2.Text = "100";
        // R3, Default - IDTipologiaCombustibile  IN (4, 5, 12) -- Olio combustibile, Gasolio, Kerosene
        HelpBindDefaultFilters((ASPxListBox)ddeCombustibileR3.FindControl("listBox"), new List<string>() { "4", "5", "12" },
                               (ASPxListBox)ddeTipiGeneratoriR3.FindControl("listBox"), new List<string>() { "1", "2" });
        txtPotenzaR3.Text = "100";
        // R4, Default - IDTipologiaCombustibile  IN (4, 5, 12) -- Olio combustibile, Gasolio, Kerosene
        HelpBindDefaultFilters((ASPxListBox)ddeCombustibileR4.FindControl("listBox"), new List<string>() { "4", "5", "12" },
                               (ASPxListBox)ddeTipiGeneratoriR4.FindControl("listBox"), new List<string>() { "1", "2" });
        txtPotenzaR4Da.Text = "20";
        txtPotenzaR4A.Text = "100";
        // R5 , Default - IDTipologiaCombustibile  IN (2, 3, 4, 5, 12, 15) -- Gas naturale, Gpl extra rete, Gpl, Gasolio, Kerosene, Olio Combustibile
        HelpBindDefaultFilters((ASPxListBox)ddeCombustibileR5.FindControl("listBox"), new List<string>() { "2", "3", "4", "5", "12", "15" },
                               (ASPxListBox)ddeTipiGeneratoriR5.FindControl("listBox"), new List<string>() { "1", "2" });
        txtPotenzaR5.Text = "10";

    }

    public void HelpBindDefaultFilters(ASPxListBox lb, List<string> DefaultCombistile, ASPxListBox lbG, List<string> DefaultTipiGeneratori)
    {
        if (lb != null)
        {
            lb.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaCombustibile(null);  
            lb.TextField = "TipologiaCombustibile";
            lb.ValueField = "IDTipologiaCombustibile";
            lb.DataBind();
            if (lb.Items != null)
            {
                foreach (string combustibile in DefaultCombistile)
                {
                    ListEditItem item = lb.Items.FindByValue(combustibile);
                    if (item != null)
                        item.Selected = true;
                }
            }
        }
        if (lbG != null)
        {            
            lbG.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaGruppiTermici(null);
            lbG.TextField = "TipologiaGruppiTermici";
            lbG.ValueField = "IDTipologiaGruppiTermici";
            lbG.DataBind();
            if (lbG.Items != null)
            {
                foreach (string tipoGeneratore in DefaultTipiGeneratori)
                {
                    ListEditItem item = lbG.Items.FindByValue(tipoGeneratore);
                    if (item != null)
                        item.Selected = true;
                }
            }
        }
    }
    
    public List<int?> GetCombustibile(ASPxListBox lb)
    {
        List<int?> CombustibileR = new List<int?>();
        if (lb != null)
            foreach (ListEditItem item in lb.SelectedItems) { CombustibileR.Add(int.Parse(item.Value.ToString())); }

        return CombustibileR;
    }

    public List<int> GetTipiGeneratori(ASPxListBox lb)
    {
        List<int> TipiGeneratoriR = new List<int>();
        if (lb != null)
            foreach (ListEditItem item in lb.SelectedItems) { TipiGeneratoriR.Add(int.Parse(item.Value.ToString())); }

        return TipiGeneratoriR;
    }

    public void GetDataRegoleControlli()
    {
        object[] getVal = new object[7];
        getVal = UtilityVerifiche.GetGeneratoriProgrammaIspezioneRegoleControlli(
            false, null, 
            false, null, 
            false, null, 
            false, null, 
            false, null);

        lblRegolaR1Count.Text = getVal[0].ToString();
        lblRegolaR2Count.Text = getVal[1].ToString();
        lblRegolaR3Count.Text = getVal[2].ToString();
        lblRegolaR4Count.Text = getVal[3].ToString();
        lblRegolaR5Count.Text = getVal[4].ToString();
        lblCountGeneratoriDaInviare.Text = getVal[5].ToString();
        lblImportoGeneratoriDaInviare.Text = string.Format("€ {0:0.##}", decimal.Parse(getVal[6].ToString()));

        if (int.Parse(getVal[5].ToString()) > 0)
        {
            btnInserGeneratoriInProgrammaIspezione.Visible = true;
        }
        else
        {
            btnInserGeneratoriInProgrammaIspezione.Visible = false;
        }
    }
    
    protected void chkRegolaR1_CheckedChanged(object sender, EventArgs e)
    {
        txtRegolaR1Percentage.Enabled = chkRegolaR1.Checked;
        if (chkRegolaR1.Checked)
        {
            txtRegolaR1Percentage.Focus();
        }
        else
        {
            txtRegolaR1Percentage.Text = string.Empty;
        }
    }

    protected void chkRegolaR2_CheckedChanged(object sender, EventArgs e)
    {
        txtRegolaR2Percentage.Enabled = chkRegolaR2.Checked;
        if (chkRegolaR2.Checked)
        {
            txtRegolaR2Percentage.Focus();
        }
        else
        {
            txtRegolaR2Percentage.Text = string.Empty;
        }
    }

    protected void chkRegolaR3_CheckedChanged(object sender, EventArgs e)
    {
        txtRegolaR3Percentage.Enabled = chkRegolaR3.Checked;
        if (chkRegolaR3.Checked)
        {
            txtRegolaR3Percentage.Focus();
        }
        else
        {
            txtRegolaR3Percentage.Text = string.Empty;
        }
    }

    protected void chkRegolaR4_CheckedChanged(object sender, EventArgs e)
    {
        txtRegolaR4Percentage.Enabled = chkRegolaR4.Checked;
        if (chkRegolaR4.Checked)
        {
            txtRegolaR4Percentage.Focus();
        }
        else
        {
            txtRegolaR4Percentage.Text = string.Empty;
        }
    }

    protected void chkRegolaR5_CheckedChanged(object sender, EventArgs e)
    {
        txtRegolaR5Percentage.Enabled = chkRegolaR5.Checked;
        if (chkRegolaR5.Checked)
        {
            txtRegolaR5Percentage.Focus();
        }
        else
        {
            txtRegolaR5Percentage.Text = string.Empty;
        }
    }
    
    protected void btnAggiornaRisultati_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            object[] getVal = new object[6];

            var a = ddeCombustibileR1.Text;

            int? regolaR1Percentage = null;
            if (!string.IsNullOrEmpty(txtRegolaR1Percentage.Text))
            {
                regolaR1Percentage = int.Parse(txtRegolaR1Percentage.Text);
            }

            int? regolaR2Percentage = null;
            if (!string.IsNullOrEmpty(txtRegolaR2Percentage.Text))
            {
                regolaR2Percentage = int.Parse(txtRegolaR2Percentage.Text);
            }

            int? regolaR3Percentage = null;
            if (!string.IsNullOrEmpty(txtRegolaR3Percentage.Text))
            {
                regolaR3Percentage = int.Parse(txtRegolaR3Percentage.Text);
            }

            int? regolaR4Percentage = null;
            if (!string.IsNullOrEmpty(txtRegolaR4Percentage.Text))
            {
                regolaR4Percentage = int.Parse(txtRegolaR4Percentage.Text);
            }

            int? regolaR5Percentage = null;
            if (!string.IsNullOrEmpty(txtRegolaR5Percentage.Text))
            {
                regolaR5Percentage = int.Parse(txtRegolaR5Percentage.Text);
            }
            
            getVal = UtilityVerifiche.GetGeneratoriProgrammaIspezioneRegoleControlli(
                chkRegolaR1.Checked, regolaR1Percentage,
                chkRegolaR2.Checked, regolaR2Percentage, 
                chkRegolaR3.Checked, regolaR3Percentage, 
                chkRegolaR4.Checked, regolaR4Percentage, 
                chkRegolaR5.Checked, regolaR5Percentage);

            lblRegolaR1Count.Text = getVal[0].ToString();
            lblRegolaR2Count.Text = getVal[1].ToString();
            lblRegolaR3Count.Text = getVal[2].ToString();
            lblRegolaR4Count.Text = getVal[3].ToString();
            lblRegolaR5Count.Text = getVal[4].ToString();
            lblCountGeneratoriDaInviare.Text = getVal[5].ToString();
            lblImportoGeneratoriDaInviare.Text = string.Format("€ {0:0.##}", decimal.Parse(getVal[6].ToString()));

            if (int.Parse(getVal[5].ToString()) > 0)
            {
                btnInserGeneratoriInProgrammaIspezione.Visible = true;
            }
            else
            {
                btnInserGeneratoriInProgrammaIspezione.Visible = false;
            }
        }
    }

    protected void btnInserGeneratoriInProgrammaIspezione_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            int iDProgrammaIspezione = UtilityVerifiche.GetIDProgrammaIspezioneAttivo();
            UtilityVerifiche.InsertGeneratoriProgrammaIspezioneRegoleControlli(iDProgrammaIspezione);

            Response.Redirect("~/VER_ProgrammaIspezione.aspx");
        }
    }

    protected void ServerErrorCustomValidator_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
    {
        //System.Web.UI.WebControls.CustomValidator val = source as System.Web.UI.WebControls.CustomValidator;

        if (string.IsNullOrEmpty(txtPotenzaR1.Text) ||
            string.IsNullOrEmpty(txtPotenzaR2.Text) ||
            string.IsNullOrEmpty(txtPotenzaR3.Text) ||
            string.IsNullOrEmpty(txtPotenzaR4Da.Text) ||
            string.IsNullOrEmpty(txtPotenzaR4A.Text) ||
            string.IsNullOrEmpty(txtPotenzaR5.Text) ||
            string.IsNullOrEmpty(ddeCombustibileR1.Text) ||
            string.IsNullOrEmpty(ddeCombustibileR2.Text) ||
            string.IsNullOrEmpty(ddeCombustibileR3.Text) ||
            string.IsNullOrEmpty(ddeCombustibileR4.Text) ||
            string.IsNullOrEmpty(ddeCombustibileR5.Text) ||
            string.IsNullOrEmpty(ddeTipiGeneratoriR1.Text) ||
            string.IsNullOrEmpty(ddeTipiGeneratoriR2.Text) ||
            string.IsNullOrEmpty(ddeTipiGeneratoriR3.Text) ||
            string.IsNullOrEmpty(ddeTipiGeneratoriR4.Text) ||
            string.IsNullOrEmpty(ddeTipiGeneratoriR5.Text))
            args.IsValid = false;
    }

    protected void btnNuovaAnalisiDati_Click(object sender, EventArgs e)
    {
        UtilityVerifiche.SetRegoleControlli(
            decimal.Parse(txtPotenzaR1.Text), GetCombustibile((ASPxListBox)ddeCombustibileR1.FindControl("listBox")), GetTipiGeneratori((ASPxListBox)ddeTipiGeneratoriR1.FindControl("listBox")),
            decimal.Parse(txtPotenzaR2.Text), GetCombustibile((ASPxListBox)ddeCombustibileR2.FindControl("listBox")), GetTipiGeneratori((ASPxListBox)ddeTipiGeneratoriR2.FindControl("listBox")),
            decimal.Parse(txtPotenzaR3.Text), GetCombustibile((ASPxListBox)ddeCombustibileR3.FindControl("listBox")), GetTipiGeneratori((ASPxListBox)ddeTipiGeneratoriR3.FindControl("listBox")),
            decimal.Parse(txtPotenzaR4Da.Text), decimal.Parse(txtPotenzaR4A.Text), GetCombustibile((ASPxListBox)ddeCombustibileR4.FindControl("listBox")), GetTipiGeneratori((ASPxListBox)ddeTipiGeneratoriR4.FindControl("listBox")),
            decimal.Parse(txtPotenzaR5.Text), GetCombustibile((ASPxListBox)ddeCombustibileR5.FindControl("listBox")), GetTipiGeneratori((ASPxListBox)ddeTipiGeneratoriR5.FindControl("listBox")));


        Response.Redirect("~/VER_ProgrammaIspezioneRegoleControlli.aspx?calculate=true");
    }

    protected void btnAnalisiDatiEsistente_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VER_ProgrammaIspezioneRegoleControlli.aspx?calculate=true");
    }


    protected void btnProgrammaIspezione_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/VER_ProgrammaIspezione.aspx");
    }
}