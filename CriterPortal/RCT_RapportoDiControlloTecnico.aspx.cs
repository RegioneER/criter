using Criter.Rapporti;
using DataLayer;
using DataUtilityCore;
using DataUtilityCore.Enum;
using DevExpress.Web;
using EncryptionQS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RCT_RapportoDiControlloTecnico : System.Web.UI.Page
{
    #region DataContext

    private CriterDataModel _CurrentDataContext;

    public CriterDataModel CurrentDataContext
    {
        get
        {
            if (_CurrentDataContext == null)
            {
                _CurrentDataContext = DataLayer.Common.ApplicationContext.Current.Context;
            }
            return _CurrentDataContext;
        }
    }

    #endregion

    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    protected void AddValidationError(string group, string msg)
    {
        this.Validators.Add(new ValidationError(group, msg));
    }

    protected void AddValidationErrors(string group, List<string> errorList)
    {
        foreach (var error in errorList)
        {
            this.Validators.Add(new ValidationError(group, error));
        }
    }

    protected void ClearValidationErrors(string group)
    {
        var validators = this.Validators.OfType<ValidationError>().Where(c => c.ValidationGroup == group).ToArray();
        foreach (var v in validators)
        {
            this.Validators.Remove(v);
            v.Dispose();
        }
    }

    protected string IDRapportoControlloTecnico
    {
        get
        {
            if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
            {
                #region Encrypt on
                QueryString qs = QueryString.FromCurrent();
                QueryString qsdec = Encryption.DecryptQueryString(qs);

                try
                {
                    if (qsdec.Count > 0)
                    {
                        if (qsdec[0] != null)
                        {
                            return (string)qsdec[0];
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
                #endregion
            }
            return "";
        }
    }

    protected string IDTipologiaRct
    {
        get
        {
            if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
            {
                #region Encrypt on
                QueryString qs = QueryString.FromCurrent();
                QueryString qsdec = Encryption.DecryptQueryString(qs);

                try
                {
                    if (qsdec.Count > 0)
                    {
                        if (qsdec[1] != null)
                        {
                            return (string)qsdec[1];
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
                #endregion
            }
            return "";
        }
    }

    protected string iDSoggetto
    {
        get
        {
            if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
            {
                #region Encrypt on
                QueryString qs = QueryString.FromCurrent();
                QueryString qsdec = Encryption.DecryptQueryString(qs);

                try
                {
                    if (qsdec.Count > 0)
                    {
                        if (qsdec[2] != null)
                        {
                            return (string)qsdec[2];
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
                #endregion
            }
            return "";
        }
    }

    protected string iDSoggettoDerived
    {
        get
        {
            if (ConfigurationManager.AppSettings["fEncryptQueryString"] == "on")
            {
                #region Encrypt on
                QueryString qs = QueryString.FromCurrent();
                QueryString qsdec = Encryption.DecryptQueryString(qs);

                try
                {
                    if (qsdec.Count > 0)
                    {
                        if (qsdec[3] != null)
                        {
                            return (string)qsdec[3];
                        }
                        else
                        {
                            return "";
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
                #endregion
            }
            return "";
        }
    }

    #region User Control Dinamycs
    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni0
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni0") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni1
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni1") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni2
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni2") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni3
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni3") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni4
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni4") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni5
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni5") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni6
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni6") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni7
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni7") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni8
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni8") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni9
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni9") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni10
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni10") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni11
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni11") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni12
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni12") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni13
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni13") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni14
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni14") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni15
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni15") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni16
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni16") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni17
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni17") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni18
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni18") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni19
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni19") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni20
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni20") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni21
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni21") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni22
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni22") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni23
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni23") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni24
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni24") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni25
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni25") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni26
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni26") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni27
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni27") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni28
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni28") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni29
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni29") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni30
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni30") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni31
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni31") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni32
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni32") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni33
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni33") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni34
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni34") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni35
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni35") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni36
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni36") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni37
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni37") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni38
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni38") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni39
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni39") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni40
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni40") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni41
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni41") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni42
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni42") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    protected WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni UCRaccomandazioniPrescrizioni47
    {
        get
        {
            return MainFormView.FindControl("UCRaccomandazioniPrescrizioni47") as WebUserControls_RapportiControlloTecnico_UCRaccomandazioniPrescrizioni;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            UCFirmaDigitale.IDRapportoControlloTecnico = IDRapportoControlloTecnico;
        }
        
        btnViewRapportoControllo.Attributes.Add("onclick", "var winRapporto=dhtmlwindow.open('InfoRapporto_" + IDRapportoControlloTecnico + "', 'iframe', 'RCT_RapportiControlloViewer.aspx?IDRapportoControlloTecnico=" + IDRapportoControlloTecnico + "', 'Rapporto_" + IDRapportoControlloTecnico + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); "); //winApe.hide();
        btnViewDefinitivoRct.Attributes.Add("onclick", "var winRapporto=dhtmlwindow.open('InfoRapporto_" + IDRapportoControlloTecnico + "', 'iframe', 'RCT_RapportiControlloViewer.aspx?IDRapportoControlloTecnico=" + IDRapportoControlloTecnico + "', 'Rapporto_" + IDRapportoControlloTecnico + "', 'width=800px,height=600px,resize=1,scrolling=1,center=1'); "); //winApe.hide();
    }

    protected void cbTipologiaTrattamentoAcqua(int? iDPresel, CheckBoxList cbTrattamentoAcqua)
    {
        cbTrattamentoAcqua.Items.Clear();

        cbTrattamentoAcqua.DataValueField = "IDTipologiaTrattamentoAcqua";
        cbTrattamentoAcqua.DataTextField = "TipologiaTrattamentoAcqua";
        cbTrattamentoAcqua.DataSource = LoadDropDownList.LoadDropDownList_SYS_TipologiaTrattamentoAcqua(iDPresel);
        cbTrattamentoAcqua.DataBind();
    }

    protected void cbTipologiaCheckList(int? iDPresel, int? iDTipologiaRct, CheckBoxList cblCheckList)
    {
        cblCheckList.Items.Clear();

        cblCheckList.DataValueField = "IDCheckList";
        cblCheckList.DataTextField = "TestoCheckList";
        cblCheckList.DataSource = LoadDropDownList.LoadDropDownList_V_SYS_CheckList(iDPresel, iDTipologiaRct);
        cblCheckList.DataBind();
    }

    protected void ddAddetti(object iDPresel, object iDSoggetto, object iDSoggettoDerived)
    {
        cmbAddetti.Text = "";
        cmbAddetti.DataSource = LoadDropDownList.ASPxComboBox_COM_AnagraficaSoggetti(false, "1", "", iDSoggetto.ToString(), string.Format("%{0}%", ""), (1).ToString(), (100 + 1).ToString());
        cmbAddetti.DataBind();
    }

    public object GetRapporto()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        int iDRapportoControlloTecnico = int.Parse(IDRapportoControlloTecnico);
        int iDTipologiaRct = int.Parse(IDTipologiaRct);

        object rapporto = null;
        switch (iDTipologiaRct)
        {
            case 1:
                rapporto = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoGT.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                var rapGT = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoGT.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                if ((rapGT.IDStatoRapportoDiControllo == 2) || (rapGT.IDStatoRapportoDiControllo == 3) || (rapGT.IDStatoRapportoDiControllo == 4))
                {
                    btnViewDefinitivoRct.Visible = true;
                    UtilityApp.SiDisableAllControls(MainFormView);
                }
                if (getVal[1] == "1" && rapGT.IDStatoRapportoDiControllo == 2)
                {
                    btnAnnullaRapportoControlloTecnico.Visible = true;
                }
                if (rapGT.IDStatoRapportoDiControllo == 3)
                {
                    btnAnnullaRapportoDiControlloInAttesaFirma.Visible = true;
                }
                break;
            case 2:
                rapporto = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoGF.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                var rapGF = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoGF.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                if ((rapGF.IDStatoRapportoDiControllo == 2) || (rapGF.IDStatoRapportoDiControllo == 3) || (rapGF.IDStatoRapportoDiControllo == 4))
                {
                    btnViewDefinitivoRct.Visible = true;
                    UtilityApp.SiDisableAllControls(MainFormView);
                }
                if (getVal[1] == "1" && rapGF.IDStatoRapportoDiControllo == 2)
                {
                    btnAnnullaRapportoControlloTecnico.Visible = true;
                }
                if (rapGF.IDStatoRapportoDiControllo == 3)
                {
                    btnAnnullaRapportoDiControlloInAttesaFirma.Visible = true;
                }
                break;
            case 3:
                rapporto = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoSC.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                var rapSC = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoSC.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                if ((rapSC.IDStatoRapportoDiControllo == 2) || (rapSC.IDStatoRapportoDiControllo == 3) || (rapSC.IDStatoRapportoDiControllo == 4))
                {
                    btnViewDefinitivoRct.Visible = true;
                    UtilityApp.SiDisableAllControls(MainFormView);
                }
                if (getVal[1] == "1" && rapSC.IDStatoRapportoDiControllo == 2)
                {
                    btnAnnullaRapportoControlloTecnico.Visible = true;
                }
                if (rapSC.IDStatoRapportoDiControllo == 3)
                {
                    btnAnnullaRapportoDiControlloInAttesaFirma.Visible = true;
                }
                break;
            case 4:
                rapporto = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoCG.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico);
                var rapCG = CurrentDataContext.V_RCT_RapportoDiControlloTecnicoCG.Where(c => c.IDRapportoControlloTecnico == iDRapportoControlloTecnico).FirstOrDefault();
                if ((rapCG.IDStatoRapportoDiControllo == 2) || (rapCG.IDStatoRapportoDiControllo == 3) || (rapCG.IDStatoRapportoDiControllo == 4))
                {
                    btnViewDefinitivoRct.Visible = true;
                    UtilityApp.SiDisableAllControls(MainFormView);
                }
                if (getVal[1] == "1" && rapCG.IDStatoRapportoDiControllo == 2)
                {
                    btnAnnullaRapportoControlloTecnico.Visible = true;
                }
                if (rapCG.IDStatoRapportoDiControllo == 3)
                {
                    btnAnnullaRapportoDiControlloInAttesaFirma.Visible = true;
                }
                break;
        }

        return rapporto;
    }
    
    public void GetDatiRapportoControlloTrattamentoAcquaInvernale(int iDRapportoControlloTecnico)
    {
        cbTipologiaTrattamentoAcqua(null, cblTipologiaTrattamentoAcquaInvernale);
        var result = UtilityRapportiControllo.GetValoriRapportoControlloTrattamentoAcquaInvernale(iDRapportoControlloTecnico);

        foreach (var row in result)
        {
            cblTipologiaTrattamentoAcquaInvernale.Items.FindByValue(row.IDTipologiaTrattamentoAcqua.ToString()).Selected = true;
        }

        VisibleTrattamentoAcquaInvernale();
    }

    public void GetDatiRapportoControlloTrattamentoAcquaAcs(int iDRapportoControlloTecnico)
    {
        cbTipologiaTrattamentoAcqua(null, cblTipologiaTrattamentoAcquaAcs);
        var result = UtilityRapportiControllo.GetValoriRapportoControlloTrattamentoAcquaAcs(iDRapportoControlloTecnico);

        foreach (var row in result)
        {
            cblTipologiaTrattamentoAcquaAcs.Items.FindByValue(row.IDTipologiaTrattamentoAcqua.ToString()).Selected = true;
        }

        VisibleTrattamentoAcquaAcs();
    }

    public void GetDatiRapportoControlloCheckList(int iDRapportoControlloTecnico)
    {
        int iDTipologiaRCT = int.Parse(IDTipologiaRct);
        cbTipologiaCheckList(null, iDTipologiaRCT, cblCheckList);
        var result = UtilityRapportiControllo.GetValoriRapportoControlloCheckList(iDRapportoControlloTecnico);

        foreach (var row in result)
        {
            cblCheckList.Items.FindByValue(row.IDCheckList.ToString()).Selected = true;
        }
    }

    #region Campi FormView
    
    protected Label lblDataInstallazione
    {
        get
        {
            return MainFormView.FindControl("lblDataInstallazione") as Label;
        }
    }

    protected RequiredFieldValidator rfvtxtDepressioneCanaleFumo
    {
        get
        {
            return MainFormView.FindControl("rfvtxtDepressioneCanaleFumo") as RequiredFieldValidator;
        }
    }
    
    protected Label lblTipologiaCombustibile
    {
        get
        {
            return MainFormView.FindControl("lblTipologiaCombustibile") as Label;
        }
    }

    protected Label lblIDTipologiaCombustibile
    {
        get
        {
            return MainFormView.FindControl("lblIDTipologiaCombustibile") as Label;
        }
    }

    protected TextBox txtPotenzaTermicaNominaleTotaleMax
    {
        get
        {
            return MainFormView.FindControl("txtPotenzaTermicaNominaleTotaleMax") as TextBox;
        }
    }

    protected TextBox txtDurezzaAcqua
    {
        get
        {
            return MainFormView.FindControl("txtDurezzaAcqua") as TextBox;
        }
    }

    protected Label lblImportoBolliniRichiesto
    {
        get
        {
            return MainFormView.FindControl("lblImportoBolliniRichiesto") as Label;
        }
    }

    protected RadioButtonList rblTipologiaControllo
    {
        get
        {
            return MainFormView.FindControl("rblTipologiaControllo") as RadioButtonList;
        }
    }

    protected RadioButtonList rblTrattamentoRiscaldamento
    {
        get
        {
            return MainFormView.FindControl("rblTrattamentoRiscaldamento") as RadioButtonList;
        }
    }

    protected RadioButtonList rblTrattamentoAcs
    {
        get
        {
            return MainFormView.FindControl("rblTrattamentoAcs") as RadioButtonList;
        }
    }

    protected RadioButtonList rblDichiarazioneConformita
    {
        get
        {
            return MainFormView.FindControl("rblDichiarazioneConformita") as RadioButtonList;
        }
    }

    protected RadioButtonList rblLibrettoImpiantoPresente
    {
        get
        {
            return MainFormView.FindControl("rblLibrettoImpiantoPresente") as RadioButtonList;
        }
    }

    protected RadioButtonList rblUsoManutenzioneGeneratore
    {
        get
        {
            return MainFormView.FindControl("rblUsoManutenzioneGeneratore") as RadioButtonList;
        }
    }

    protected RadioButtonList rblLibrettoImpiantoCompilato
    {
        get
        {
            return MainFormView.FindControl("rblLibrettoImpiantoCompilato") as RadioButtonList;
        }
    }

    protected ASPxComboBox cmbTipologiaGeneratoriTermici
    {
        get
        {
            return MainFormView.FindControl("cmbTipologiaGeneratoriTermici") as ASPxComboBox;
        }
    }

    protected Label lblIDSoggetto
    {
        get
        {
            return MainFormView.FindControl("lblIDSoggetto") as Label;
        }
    }

    protected Label lblIDSoggettoDerived
    {
        get
        {
            return MainFormView.FindControl("lblIDSoggettoDerived") as Label;
        }
    }

    protected TableRow rowTrattamentoRiscaldamento
    {
        get
        {
            return MainFormView.FindControl("rowTrattamentoRiscaldamento") as TableRow;
        }
    }
        
    protected TableRow rowTrattamentoACS
    {
        get
        {
            return MainFormView.FindControl("rowTrattamentoACS") as TableRow;
        }
    }

    protected Label lblPotenzaTermicaNominaleFocolare
    {
        get
        {
            return MainFormView.FindControl("lblPotenzaTermicaNominaleFocolare") as Label;
        }
    }

    protected TextBox txtPotenzaAssorbitaCombustibile
    {
        get
        {
            return MainFormView.FindControl("txtPotenzaAssorbitaCombustibile") as TextBox;
        }
    }

    protected TextBox txtPotenzaBypass
    {
        get
        {
            return MainFormView.FindControl("txtPotenzaBypass") as TextBox;
        }
    }

    protected CheckBox chkClimatizzazioneInvernale
    {
        get
        {
            return MainFormView.FindControl("chkClimatizzazioneInvernale") as CheckBox;
        }
    }

    protected CheckBox chkClimatizzazioneEstiva
    {
        get
        {
            return MainFormView.FindControl("chkClimatizzazioneEstiva") as CheckBox;
        }
    }

    protected CheckBox chkProduzioneACS
    {
        get
        {
            return MainFormView.FindControl("chkProduzioneACS") as CheckBox;
        }
    }

    protected RadioButton rbRaffrescamento
    {
        get
        {
            return MainFormView.FindControl("rbRaffrescamento") as RadioButton;
        }
    }

    protected RadioButton rbRiscaldamento
    {
        get
        {
            return MainFormView.FindControl("rbRiscaldamento") as RadioButton;
        }
    }

    protected ASPxComboBox cmbFluidoVettoreEntrata
    {
        get
        {
            return MainFormView.FindControl("cmbFluidoVettoreEntrata") as ASPxComboBox;
        }
    }

    protected ASPxComboBox cmbFluidoVettore
    {
        get
        {
            return MainFormView.FindControl("cmbFluidoVettore") as ASPxComboBox;
        }
    }

    protected RadioButton rblEvacuazioneForzata
    {
        get
        {
            return MainFormView.FindControl("rblEvacuazioneForzata") as RadioButton;
        }
    }

    protected RadioButton rblEvacuazioneNaturale
    {
        get
        {
            return MainFormView.FindControl("rblEvacuazioneNaturale") as RadioButton;
        }
    }

    protected TextBox txtDepressioneCanaleFumo
    {
        get
        {
            return MainFormView.FindControl("txtDepressioneCanaleFumo") as TextBox;
        }
    }

    protected TextBox txtOsservazioni
    {
        get
        {
            return MainFormView.FindControl("txtOsservazioni") as TextBox;
        }
    }

    protected TextBox txtRaccomandazioni
    {
        get
        {
            return MainFormView.FindControl("txtRaccomandazioni") as TextBox;
        }
    }

    protected TextBox txtPrescrizioni
    {
        get
        {
            return MainFormView.FindControl("txtPrescrizioni") as TextBox;
        }
    }

    protected RadioButtonList rblImpiantoFunzionante
    {
        get
        {
            return MainFormView.FindControl("rblImpiantoFunzionante") as RadioButtonList;
        }
    }

    protected TextBox txtDataManutenzioneConsigliata
    {
        get
        {
            return MainFormView.FindControl("txtDataManutenzioneConsigliata") as TextBox;
        }
    }

    protected TextBox txtDataControllo
    {
        get
        {
            return MainFormView.FindControl("txtDataControllo") as TextBox;
        }
    }

    protected ASPxTimeEdit txtOraArrivo
    {
        get
        {
            return MainFormView.FindControl("txtOraArrivo") as ASPxTimeEdit;
        }
    }

    protected ASPxTimeEdit txtOraPartenza
    {
        get
        {
            return MainFormView.FindControl("txtOraPartenza") as ASPxTimeEdit;
        }
    }

    protected CheckBoxList cblTipologiaTrattamentoAcquaInvernale
    {
        get
        {
            return MainFormView.FindControl("cblTipologiaTrattamentoAcquaInvernale") as CheckBoxList;
        }
    }

    protected CheckBoxList cblTipologiaTrattamentoAcquaAcs
    {
        get
        {
            return MainFormView.FindControl("cblTipologiaTrattamentoAcquaAcs") as CheckBoxList;
        }
    }

    protected CheckBoxList cblCheckList
    {
        get
        {
            return MainFormView.FindControl("cblCheckList") as CheckBoxList;
        }
    }

    protected Label lblPotenzaTermicaNominale
    {
        get
        {
            return MainFormView.FindControl("lblPotenzaTermicaNominale") as Label;
        }
    }

    protected Button btnViewRapportoControllo
    {
        get
        {
            return MainFormView.FindControl("btnViewRapportoControllo") as Button;
        }
    }

    protected RCT_UC_UCBolliniSelector UCBolliniSelector
    {
        get
        {
            return MainFormView.FindControl("UCBolliniSelector") as RCT_UC_UCBolliniSelector;
        }
    }

    protected UCFirmaDigitaleView UCFirmaDigitale
    {
        get
        {
            return MainFormView.FindControl("UCFirmaDigitale") as UCFirmaDigitaleView;
        }
    }
    
    protected TextBox txtAltroFluidoTermoVettoreEntrata
    {
        get
        {
            return MainFormView.FindControl("txtAltroFluidoTermoVettoreEntrata") as TextBox;
        }
    }

    protected Panel pnlFluidoVettoreEntrataAltro
    {
        get
        {
            return MainFormView.FindControl("pnlFluidoVettoreEntrataAltro") as Panel;
        }
    }

    protected TextBox txtAltroFluidoTermoVettoreUscita
    {
        get
        {
            return MainFormView.FindControl("txtAltroFluidoTermoVettoreUscita") as TextBox;
        }
    }

    protected Panel pnlFluidoVettoreUscitaAltro
    {
        get
        {
            return MainFormView.FindControl("pnlFluidoVettoreUscitaAltro") as Panel;
        }
    }

    protected ASPxComboBox cmbTipologiaDistribuzione
    {
        get
        {
            return MainFormView.FindControl("cmbTipologiaDistribuzione") as ASPxComboBox;
        }
    }

    protected Panel pnlTipologiaSistemaDistribuzioneAltro
    {
        get
        {
            return MainFormView.FindControl("pnlTipologiaSistemaDistribuzioneAltro") as Panel;
        }
    }

    protected TextBox txtAltroTipologiaSistemaDistribuzione
    {
        get
        {
            return MainFormView.FindControl("txtAltroTipologiaSistemaDistribuzione") as TextBox;
        }
    }

    protected ASPxComboBox cmbTipologiaContabilizzazione
    {
        get
        {
            return MainFormView.FindControl("cmbTipologiaContabilizzazione") as ASPxComboBox;
        }
    }

    protected CustomValidator cvRaccomandazioniPrescrizioni
    {
        get
        {
            return MainFormView.FindControl("cvRaccomandazioniPrescrizioni") as CustomValidator;
        }
    }

    protected TableRow rowTipoDistribuzione
    {
        get
        {
            return MainFormView.FindControl("rowTipoDistribuzione") as TableRow;
        }
    }

    protected TableRow rowContabilizzazione
    {
        get
        {
            return MainFormView.FindControl("rowContabilizzazione") as TableRow;
        }
    }

    protected TableRow rowTipoContabilizzazione
    {
        get
        {
            return MainFormView.FindControl("rowTipoContabilizzazione") as TableRow;
        }
    }

    protected TableRow rowTermoregolazione
    {
        get
        {
            return MainFormView.FindControl("rowTermoregolazione") as TableRow;
        }
    }

    protected TableRow rowCorrettoFunzionamentoContabilizzazione
    {
        get
        {
            return MainFormView.FindControl("rowCorrettoFunzionamentoContabilizzazione") as TableRow;
        }
    }

    protected TableRow rowDepressioneCanaleFumo
    {
        get
        {
            return MainFormView.FindControl("rowDepressioneCanaleFumo") as TableRow;
        }
    }

    protected TableRow rowTitoloSistemiTermoregolazione
    {
        get
        {
            return MainFormView.FindControl("rowTitoloSistemiTermoregolazione") as TableRow;
        }
    }

    protected ASPxComboBox cmbAddetti
    {
        get
        {
            return MainFormView.FindControl("cmbAddetti") as ASPxComboBox;
        }
    }

    protected Label lblGuidInteroImpianto
    {
        get
        {
            return MainFormView.FindControl("lblGuidInteroImpianto") as Label;
        }
    }


    #endregion

    protected void MainFormView_ItemCreated(object sender, EventArgs e)
    {
        int iDTipologiaRct = int.Parse(IDTipologiaRct);
        SetVisibleTipologiaRct(iDTipologiaRct);
    }

    protected void MainFormView_OnDataBound(object sender, EventArgs e)
    {
        int iDTipologiaRct = int.Parse(IDTipologiaRct);
        if (iDTipologiaRct == 3)
        {
            object IDTipologiaFluidoTermoVettoreEntrata = DataBinder.Eval(MainFormView.DataItem, "IDTipologiaFluidoTermoVettoreEntrata");
            VisibleHiddenFluidoVettoreEntrata(IDTipologiaFluidoTermoVettoreEntrata);

            object IDTipologiaFluidoTermoVettoreUscita = DataBinder.Eval(MainFormView.DataItem, "IDTipologiaFluidoTermoVettoreEntrata");
            VisibleHiddenFluidoVettoreUscita(IDTipologiaFluidoTermoVettoreUscita);

            object IDTipologiaSistemaDistribuzione = DataBinder.Eval(MainFormView.DataItem, "IDTipologiaSistemaDistribuzione");
            VisibleHiddenTipologiaSistemaDistribuzione(IDTipologiaSistemaDistribuzione);
        }

        if (iDTipologiaRct == 1)
        { 
            if (rblTipologiaControllo.SelectedValue == "1")
            {
                rfvtxtDepressioneCanaleFumo.Enabled = true;
                txtDepressioneCanaleFumo.CssClass = "txtClass_o";
            }
            else if (rblTipologiaControllo.SelectedValue == "2")
            {
                rfvtxtDepressioneCanaleFumo.Enabled = false;
                txtDepressioneCanaleFumo.CssClass = "txtClass";
            }
        }

        RCT_UC_Checkbox chkLocaleInstallazioneIdoneo = (RCT_UC_Checkbox)MainFormView.FindControl("chkLocaleInstallazioneIdoneo");
        RCT_UC_Checkbox chkGeneratoriIdonei = (RCT_UC_Checkbox)MainFormView.FindControl("chkGeneratoriIdonei");
        EnabledDisabledInstallazione(chkLocaleInstallazioneIdoneo, chkGeneratoriIdonei);

        UserControl UCBolliniSelector = (UserControl)MainFormView.FindControl("UCBolliniSelector");

        ddAddetti(null, iDSoggetto, null);

        GetDatiRapportoControlloTrattamentoAcquaInvernale(int.Parse(IDRapportoControlloTecnico));
        GetDatiRapportoControlloTrattamentoAcquaAcs(int.Parse(IDRapportoControlloTecnico));
        GetDatiRapportoControlloCheckList(int.Parse(IDRapportoControlloTecnico));

        AggiornaImportoBolliniRichiesto();
    }

    public void SetVisibleTipologiaRct(int iDTipologiaRct)
    {
        Label lblModelloRapportoControllo = (Label)MainFormView.FindControl("lblModelloRapportoControllo");
        Label lblModello = (Label)MainFormView.FindControl("lblModello");
        Label lblTrattamentoRiscaldamento = (Label)MainFormView.FindControl("lblTrattamentoRiscaldamento");
        TableRow rowTrattamentoTipoAcs = MainFormView.FindControl("rowTrattamentoTipoAcs") as TableRow;
        Label lblTrattamentoAcs = MainFormView.FindControl("lblTrattamentoAcs") as Label;
        Label lblLocaleInstallazioneIdoneo = MainFormView.FindControl("lblLocaleInstallazioneIdoneo") as Label;
        Label lblGeneratoriIdonei = MainFormView.FindControl("lblGeneratoriIdonei") as Label;
        TableRow rowGeneratoriIdonei = MainFormView.FindControl("rowGeneratoriIdonei") as TableRow;
        TableRow rowApertureLibere = MainFormView.FindControl("rowApertureLibere") as TableRow;
        Label lblApertureLibere = MainFormView.FindControl("lblApertureLibere") as Label;
        TableRow rowDimensioniApertureAdeguate = MainFormView.FindControl("rowDimensioniApertureAdeguate") as TableRow;
        Label lblDimensioniApertureAdeguate = MainFormView.FindControl("lblDimensioniApertureAdeguate") as Label;
        TableRow rowScarichiIdonei = MainFormView.FindControl("rowScarichiIdonei") as TableRow;
        Label lblScarichiIdonei = MainFormView.FindControl("lblScarichiIdonei") as Label;
        TableRow rowRegolazioneTemperaturaAmbiente = MainFormView.FindControl("rowRegolazioneTemperaturaAmbiente") as TableRow;
        //TableRow rowAssenzaPerditeCombustibile = MainFormView.FindControl("rowAssenzaPerditeCombustibile") as TableRow;
        Label lblAssenzaPerditeCombustibile = MainFormView.FindControl("lblAssenzaPerditeCombustibile") as Label;
        //TableRow rowTenutaImpiantoIdraulico = MainFormView.FindControl("rowTenutaImpiantoIdraulico") as TableRow;
        Label lblTenutaImpiantoIdraulico = MainFormView.FindControl("lblTenutaImpiantoIdraulico") as Label;
        TableRow rowLineeElettricheIdonee = MainFormView.FindControl("rowLineeElettricheIdonee") as TableRow;
        Label lblLineeElettricheIdonee = MainFormView.FindControl("lblLineeElettricheIdonee") as Label;
        TableRow rowCoibentazioniIdonee = MainFormView.FindControl("rowCoibentazioniIdonee") as TableRow;
        TableRow rowStatoCoibentazioniIdonee = MainFormView.FindControl("rowStatoCoibentazioniIdonee") as TableRow;
        Label lblCoibentazioniIdonee = MainFormView.FindControl("lblCoibentazioniIdonee") as Label;
        TableRow rowCapsulaInsonorizzataIdonea = MainFormView.FindControl("rowCapsulaInsonorizzataIdonea") as TableRow;
        TableRow rowTenutaCircuitoOlioIdonea = MainFormView.FindControl("rowTenutaCircuitoOlioIdonea") as TableRow;
        TableRow rowFunzionalitàScambiatoreSeparazione = MainFormView.FindControl("rowFunzionalitàScambiatoreSeparazione") as TableRow;
        Label lblTitoloSezioneE = MainFormView.FindControl("lblTitoloSezioneE") as Label;
        TableRow rowTipoGruppiTermici = MainFormView.FindControl("rowTipoGruppiTermici") as TableRow;
        //TableRow rowTipologiaGeneratoriTermici = MainFormView.FindControl("rowTipologiaGeneratoriTermici") as TableRow;
        //TableRow rowPotenzaTermicaNominaleFocolare = MainFormView.FindControl("rowPotenzaTermicaNominaleFocolare") as TableRow;
        TableRow rowPotenzaFrigorifera = MainFormView.FindControl("rowPotenzaFrigorifera") as TableRow;
        Label lblTitoloPotenzaTermicaNominale = MainFormView.FindControl("lblTitoloPotenzaTermicaNominale") as Label;
        CheckBox chkClimatizzazioneEstiva = MainFormView.FindControl("chkClimatizzazioneEstiva") as CheckBox;
        TableRow rowTipologiaCombustile = MainFormView.FindControl("rowTipologiaCombustile") as TableRow;
        Label lblTitoloCombustile = MainFormView.FindControl("lblTitoloCombustile") as Label;
        TableRow rowNCircuitiTotali = MainFormView.FindControl("rowNCircuitiTotali") as TableRow;
        TableRow rowProvaEseguita = MainFormView.FindControl("rowProvaEseguita") as TableRow;
        TableRow rowTipologiaMacchineFrigorifere = MainFormView.FindControl("rowTipologiaMacchineFrigorifere") as TableRow;
        TableRow rowTipologiaCogeneratore = MainFormView.FindControl("rowTipologiaCogeneratore") as TableRow;
        TableRow rowFluidoVettoreEntrata = MainFormView.FindControl("rowFluidoVettoreEntrata") as TableRow;
        TableRow rowFluidoVettoreUscita = MainFormView.FindControl("rowFluidoVettoreUscita") as TableRow;
        TableRow rowPotenzaElettricaMorsetti = MainFormView.FindControl("rowPotenzaElettricaMorsetti") as TableRow;
        TableRow rowPotenzaAssorbitaCombustibile = MainFormView.FindControl("rowPotenzaAssorbitaCombustibile") as TableRow;
        TableRow rowPotenzaBypass = MainFormView.FindControl("rowPotenzaBypass") as TableRow;
        TableRow rowEvacuazioneFumi = MainFormView.FindControl("rowEvacuazioneFumi") as TableRow;
        //TableRow rowDepressioneCanaleFumo = MainFormView.FindControl("rowDepressioneCanaleFumo") as TableRow;

        TableRow rowDispositiviComandoRegolazione = MainFormView.FindControl("rowDispositiviComandoRegolazione") as TableRow;
        TableRow rowDispositiviSicurezza = MainFormView.FindControl("rowDispositiviSicurezza") as TableRow;
        TableRow rowValvolaSicurezzaSovrappressione = MainFormView.FindControl("rowValvolaSicurezzaSovrappressione") as TableRow;
        TableRow rowScambiatoreFumiPulito = MainFormView.FindControl("rowScambiatoreFumiPulito") as TableRow;
        TableRow rowRiflussoProdottiCombustione = MainFormView.FindControl("rowRiflussoProdottiCombustione") as TableRow;
        //TableRow rowConformitaUNI10389 = MainFormView.FindControl("rowConformitaUNI10389") as TableRow;

        TableRow rowAssenzaPerditeRefrigerante = MainFormView.FindControl("rowAssenzaPerditeRefrigerante") as TableRow;
        TableRow rowFiltriPuliti = MainFormView.FindControl("rowFiltriPuliti") as TableRow;
        TableRow rowLeakDetector = MainFormView.FindControl("rowLeakDetector") as TableRow;
        TableRow rowScambiatoriLiberi = MainFormView.FindControl("rowScambiatoriLiberi") as TableRow;
        TableRow rowParametriTermodinamici = MainFormView.FindControl("rowParametriTermodinamici") as TableRow;

        TableRow rowPotenzaCompatibileProgetto = MainFormView.FindControl("rowPotenzaCompatibileProgetto") as TableRow;
        TableRow rowAssenzaTrafilamenti = MainFormView.FindControl("rowAssenzaTrafilamenti") as TableRow;

        TableRow rowVerificaEnergeticaGT = MainFormView.FindControl("rowVerificaEnergeticaGT") as TableRow;
        TableRow rowVerificaEnergeticaGF = MainFormView.FindControl("rowVerificaEnergeticaGF") as TableRow;
        TableRow rowVerificaEnergeticaSC = MainFormView.FindControl("rowVerificaEnergeticaSC") as TableRow;
        TableRow rowVerificaEnergeticaCG = MainFormView.FindControl("rowVerificaEnergeticaCG") as TableRow;

        switch (iDTipologiaRct)
        {
            case 1:
                lblModelloRapportoControllo.Text = "RAPPORTO DI CONTROLLO TECNICO TIPO 1 (GRUPPI TERMICI)";
                lblTrattamentoRiscaldamento.Text = "Trattamento in riscaldamento";
                rowTrattamentoTipoAcs.Visible = true;
                lblTrattamentoAcs.Text = "Trattamento in ACS";
                lblLocaleInstallazioneIdoneo.Text = "Per installazione interna: in locale idoneo";
                lblApertureLibere.Text = "Aperture di ventilazione/aerazione libere da ostruzioni";
                lblDimensioniApertureAdeguate.Text = "Adeguate dimensioni aperture di ventilazione/aerazione";
                lblScarichiIdonei.Text = "Canale da fumo o condotti di scarico idonei (esame visivo)";
                lblTenutaImpiantoIdraulico.Text = "Idonea tenuta dell'impianto interno e raccordi con il generatore";
                lblAssenzaPerditeCombustibile.Text = "Assenza di perdite di combustibile liquido";
                rowLineeElettricheIdonee.Visible = false;
                rowCoibentazioniIdonee.Visible = false;
                rowStatoCoibentazioniIdonee.Visible = false;
                rowCapsulaInsonorizzataIdonea.Visible = false;
                rowTenutaCircuitoOlioIdonea.Visible = false;
                rowFunzionalitàScambiatoreSeparazione.Visible = false;
                lblTitoloSezioneE.Text = "E. CONTROLLO E VERIFICA ENERGETICA DEL GRUPPO TERMICO";
                lblModello.Text = "Gruppo termico";
                lblTitoloPotenzaTermicaNominale.Text = "Potenza termica nominale utile (kW)";
                chkClimatizzazioneEstiva.Visible = false;
                lblTitoloCombustile.Text = "Combustibile";
                rowPotenzaFrigorifera.Visible = false;
                rowNCircuitiTotali.Visible = false;
                rowProvaEseguita.Visible = false;
                rowTipologiaMacchineFrigorifere.Visible = false;
                rowTipologiaCogeneratore.Visible = false;
                rowFluidoVettoreEntrata.Visible = false;
                rowFluidoVettoreUscita.Visible = false;
                rowPotenzaElettricaMorsetti.Visible = false;
                rowPotenzaAssorbitaCombustibile.Visible = false;
                rowPotenzaBypass.Visible = false;
                rowAssenzaPerditeRefrigerante.Visible = false;
                rowFiltriPuliti.Visible = false;
                rowLeakDetector.Visible = false;
                rowScambiatoriLiberi.Visible = false;
                rowParametriTermodinamici.Visible = false;
                rowPotenzaCompatibileProgetto.Visible = false;
                rowAssenzaTrafilamenti.Visible = false;
                rowVerificaEnergeticaGF.Visible = false;
                rowVerificaEnergeticaSC.Visible = false;
                rowVerificaEnergeticaCG.Visible = false;
                //rowDepressioneCanaleFumo.Visible = true;

                if (UtilityRapportiControllo.GetfUnitaImmobiliare(int.Parse(IDRapportoControlloTecnico)))
                {
                    rowTitoloSistemiTermoregolazione.Visible = false;
                    rowTipoDistribuzione.Visible = false;
                    rowContabilizzazione.Visible = false;
                    rowTipoContabilizzazione.Visible = false;
                    rowTermoregolazione.Visible = false;
                    rowCorrettoFunzionamentoContabilizzazione.Visible = false;
                }
                else
                {
                    rowTitoloSistemiTermoregolazione.Visible = true;
                    rowTipoDistribuzione.Visible = true;
                    rowContabilizzazione.Visible = true;
                    rowTipoContabilizzazione.Visible = true;
                    rowTermoregolazione.Visible = true;
                    rowCorrettoFunzionamentoContabilizzazione.Visible = true;
                }
                break;
            case 2:
                lblModelloRapportoControllo.Text = "RAPPORTO DI CONTROLLO TECNICO TIPO 2 (GRUPPI FRIGO)";
                lblTrattamentoRiscaldamento.Text = "Trattamento";
                rowTrattamentoTipoAcs.Visible = false;
                lblLocaleInstallazioneIdoneo.Text = "Locale di installazione idoneo";
                rowGeneratoriIdonei.Visible = false;
                lblApertureLibere.Text = "Aperture di ventilazione libere da ostruzioni";
                lblDimensioniApertureAdeguate.Text = "Dimensioni aperture di ventilazione adeguate";
                rowScarichiIdonei.Visible = false;
                rowRegolazioneTemperaturaAmbiente.Visible = false;
                //rowAssenzaPerditeCombustibile.Visible = false;
                //rowTenutaImpiantoIdraulico.Visible = false;
                lblLineeElettricheIdonee.Text = "Linee elettriche idonee";
                lblCoibentazioniIdonee.Text = "Coibentazioni idonee";
                rowStatoCoibentazioniIdonee.Visible = false;
                rowCapsulaInsonorizzataIdonea.Visible = false;
                rowTenutaCircuitoOlioIdonea.Visible = false;
                rowFunzionalitàScambiatoreSeparazione.Visible = false;
                lblTitoloSezioneE.Text = "E. CONTROLLO E VERIFICA ENERGETICA DEL GRUPPO FRIGO";
                lblModello.Text = "Gruppo frigo";
                rowTipoGruppiTermici.Visible = false;
                //rowTipologiaGeneratoriTermici.Visible = false;
                //rowPotenzaTermicaNominaleFocolare.Visible = false;
                lblTitoloPotenzaTermicaNominale.Text = "Potenza termica nominale in riscaldamento (kW)";
                rowTipologiaCombustile.Visible = false;
                rowTipologiaCogeneratore.Visible = false;
                rowFluidoVettoreEntrata.Visible = false;
                rowFluidoVettoreUscita.Visible = false;
                rowPotenzaElettricaMorsetti.Visible = false;
                rowPotenzaAssorbitaCombustibile.Visible = false;
                rowPotenzaBypass.Visible = false;
                rowEvacuazioneFumi.Visible = false;
                //rowDepressioneCanaleFumo.Visible = false;
                rowDispositiviComandoRegolazione.Visible = false;
                rowDispositiviSicurezza.Visible = false;
                rowValvolaSicurezzaSovrappressione.Visible = false;
                rowScambiatoreFumiPulito.Visible = false;
                rowRiflussoProdottiCombustione.Visible = false;
                //rowConformitaUNI10389.Visible = false;
                rowPotenzaCompatibileProgetto.Visible = false;
                rowAssenzaTrafilamenti.Visible = false;
                rowVerificaEnergeticaGT.Visible = false;
                rowVerificaEnergeticaSC.Visible = false;
                rowVerificaEnergeticaCG.Visible = false;
                break;
            case 3:
                lblModelloRapportoControllo.Text = "RAPPORTO DI CONTROLLO TECNICO TIPO 3 (SCAMBIATORI)";
                lblTrattamentoRiscaldamento.Text = "Trattamento in riscaldamento";
                lblTrattamentoAcs.Text = "Trattamento in ACS";
                lblLocaleInstallazioneIdoneo.Text = "Luogo di installazione idoneo";
                rowGeneratoriIdonei.Visible = false;
                rowApertureLibere.Visible = false;
                rowDimensioniApertureAdeguate.Visible = false;
                rowScarichiIdonei.Visible = false;
                rowRegolazioneTemperaturaAmbiente.Visible = false;
                //rowAssenzaPerditeCombustibile.Visible = false;
                lblTenutaImpiantoIdraulico.Text = "Assenza perdite dal circuito idraulico";
                lblLineeElettricheIdonee.Text = "Linee elettriche idonee";
                lblCoibentazioniIdonee.Text = "Stato delle coibentazioni idonee";
                rowCapsulaInsonorizzataIdonea.Visible = false;
                rowTenutaCircuitoOlioIdonea.Visible = false;
                rowFunzionalitàScambiatoreSeparazione.Visible = false;
                lblTitoloSezioneE.Text = "E. CONTROLLO E VERIFICA ENERGETICA DELLO SCAMBIATORE";
                lblModello.Text = "Scambiatore";
                rowTipoGruppiTermici.Visible = false;
                //rowTipologiaGeneratoriTermici.Visible = false;
                //rowPotenzaTermicaNominaleFocolare.Visible = false;
                lblTitoloPotenzaTermicaNominale.Text = "Potenza termica nominale (kW)";
                rowTipologiaCombustile.Visible = false;
                rowPotenzaFrigorifera.Visible = false;
                rowNCircuitiTotali.Visible = false;
                rowProvaEseguita.Visible = false;
                rowTipologiaMacchineFrigorifere.Visible = false;
                rowTipologiaCogeneratore.Visible = false;
                rowPotenzaElettricaMorsetti.Visible = false;
                rowPotenzaAssorbitaCombustibile.Visible = false;
                rowPotenzaBypass.Visible = false;
                rowEvacuazioneFumi.Visible = false;
                //rowDepressioneCanaleFumo.Visible = false;
                rowDispositiviComandoRegolazione.Visible = false;
                rowDispositiviSicurezza.Visible = false;
                rowValvolaSicurezzaSovrappressione.Visible = false;
                rowScambiatoreFumiPulito.Visible = false;
                rowRiflussoProdottiCombustione.Visible = false;
                //rowConformitaUNI10389.Visible = false;
                rowAssenzaPerditeRefrigerante.Visible = false;
                rowFiltriPuliti.Visible = false;
                rowLeakDetector.Visible = false;
                rowScambiatoriLiberi.Visible = false;
                rowParametriTermodinamici.Visible = false;
                rowVerificaEnergeticaGT.Visible = false;
                rowVerificaEnergeticaGF.Visible = false;
                rowVerificaEnergeticaCG.Visible = false;
                break;
            case 4:
                lblModelloRapportoControllo.Text = "RAPPORTO DI CONTROLLO TECNICO TIPO 4 (COGENERATORI)";
                lblTrattamentoRiscaldamento.Text = "Trattamento";
                rowTrattamentoTipoAcs.Visible = false;
                lblLocaleInstallazioneIdoneo.Text = "Luogo di installazione idoneo (esame visivo)";
                rowGeneratoriIdonei.Visible = false;
                lblApertureLibere.Text = "Aperture di ventilazione libere da ostruzioni (esame visivo)";
                lblDimensioniApertureAdeguate.Text = "Adeguate dimensioni aperture di ventilazione (esame visivo)";
                lblScarichiIdonei.Text = "Camino e canale da fumo idonei (esame visivo)";
                rowRegolazioneTemperaturaAmbiente.Visible = false;
                lblAssenzaPerditeCombustibile.Text = "Tenuta circuito alimentazione combustibile idonea";
                lblTenutaImpiantoIdraulico.Text = "Tenuta circuito idraulico idonea";
                lblLineeElettricheIdonee.Text = "Linee elettriche e cablaggi idonei (esame visivo)";
                rowCoibentazioniIdonee.Visible = false;
                lblTitoloSezioneE.Text = "E. CONTROLLO E VERIFICA ENERGETICA DEL COGENERATORE";
                lblModello.Text = "Cogeneratore";
                rowTipoGruppiTermici.Visible = false;
                //rowTipologiaGeneratoriTermici.Visible = false;
                //rowPotenzaTermicaNominaleFocolare.Visible = false;
                rowStatoCoibentazioniIdonee.Visible = false;
                lblTitoloPotenzaTermicaNominale.Text = "Potenza termica nominale (massimo recupero) (kW)";
                lblTitoloCombustile.Text = "Alimentazione";
                rowPotenzaFrigorifera.Visible = false;
                rowNCircuitiTotali.Visible = false;
                rowProvaEseguita.Visible = false;
                rowTipologiaMacchineFrigorifere.Visible = false;
                rowFluidoVettoreEntrata.Visible = false;
                rowEvacuazioneFumi.Visible = false;
                //rowDepressioneCanaleFumo.Visible = false;
                rowDispositiviComandoRegolazione.Visible = false;
                rowDispositiviSicurezza.Visible = false;
                rowValvolaSicurezzaSovrappressione.Visible = false;
                rowScambiatoreFumiPulito.Visible = false;
                rowRiflussoProdottiCombustione.Visible = false;
                //rowConformitaUNI10389.Visible = false;
                rowAssenzaPerditeRefrigerante.Visible = false;
                rowFiltriPuliti.Visible = false;
                rowLeakDetector.Visible = false;
                rowScambiatoriLiberi.Visible = false;
                rowParametriTermodinamici.Visible = false;
                rowPotenzaCompatibileProgetto.Visible = false;
                rowAssenzaTrafilamenti.Visible = false;
                rowVerificaEnergeticaGT.Visible = false;
                rowVerificaEnergeticaGF.Visible = false;
                rowVerificaEnergeticaSC.Visible = false;
                break;
        }

    }
    
    protected void rblTipologiaControllo_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        RadioButtonList rblTipologiaControllo = (RadioButtonList)sender;
        AggiornaImportoBolliniRichiesto();

        MainFormView.UpdateItem(false);

        QueryString qs = new QueryString();
        qs.Add("IDRapportoControlloTecnico", IDRapportoControlloTecnico);
        qs.Add("IDTipologiaRCT", IDTipologiaRct);
        qs.Add("IDSoggetto", iDSoggetto);
        qs.Add("IDSoggettoDerived", iDSoggettoDerived);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);
        string url = "RCT_RapportoDiControlloTecnico.aspx";

        url += qsEncrypted.ToString();
        Response.Redirect(url);
    }

    #region Bollini
    
    private void AggiornaImportoBolliniRichiesto()
    {
        if (rblTipologiaControllo.SelectedValue == "2")
        {
            lblImportoBolliniRichiesto.Text = "Nessun bollino calore pulito richiesto per i rapporti di controllo Funzionali";
            //UCBolliniSelector_Selezioneterminata(this, null);
            UCBolliniSelector.Visible = false;
        }
        else
        {
            int iDTipologiaRct = int.Parse(IDTipologiaRct);
            var result = UtilityBollini.GetImportoRichiesto(long.Parse(IDRapportoControlloTecnico), lblPotenzaTermicaNominale.Text, txtDataControllo.Text, iDTipologiaRct);

            UCBolliniSelector.ImportoRichiesto = result.Item2;
            if (result.Item1)
            {
                if (UCBolliniSelector.ImportoRichiesto > 0)
                {
                    UCBolliniSelector.Visible = true;
                    lblImportoBolliniRichiesto.Text = result.Item3;// string.Format("Il costo per emettere questo rapporto di controllo è pari a {0} €.", string.Format("{0:#.00}", UCBolliniSelector.ImportoRichiesto));
                }
                else
                {
                    UCBolliniSelector.Visible = false;
                    lblImportoBolliniRichiesto.Text = result.Item3;// string.Format("Il costo per emettere questo rapporto di controllo è pari a {0} €.", string.Format("{0:#.00}", UCBolliniSelector.ImportoRichiesto));
                }
            }
            else
            {
                UCBolliniSelector.Visible = false;
                lblImportoBolliniRichiesto.Text = result.Item3;
            }
        }
    }

    protected void UCBolliniSelector_Selezioneterminata(object sender, EventArgs e)
    {
        MainFormView.UpdateItem(true);

        this.ClearValidationErrors("ValidationGroupBollini");
        //Verifica il numero di bollini (ma non che non siano già stati usati) ed escludo il caso di Intero Impianto
        if (!UtilityBollini.VerificaBollini(CurrentDataContext, int.Parse(IDRapportoControlloTecnico),
                UCBolliniSelector.ImportoTotaleSelezionati) && string.IsNullOrEmpty(lblGuidInteroImpianto.Text))
        {

            this.AddValidationError("ValidationGroupBollini", "L'importo totale dei bollini Calore Pulito selezionati non è corretto.");
        }
        else
        {
            var errori = new List<string>();
            bool fValid = AggiornaBolliniSuRct(errori);
            if (fValid)
            {
                QueryString qs = new QueryString();
                qs.Add("IDRapportoControlloTecnico", IDRapportoControlloTecnico);
                qs.Add("IDTipologiaRCT", IDTipologiaRct);
                qs.Add("IDSoggetto", iDSoggetto);
                qs.Add("IDSoggettoDerived", iDSoggettoDerived);
                QueryString qsEncrypted = Encryption.EncryptQueryString(qs);
                string url = "RCT_RapportoDiControlloTecnico.aspx";

                url += qsEncrypted.ToString();
                Response.Redirect(url);
            }
            else
            {
                this.AddValidationErrors("ValidationGroupBollini", errori);
            }
        }

        UCBolliniSelector.SelectedValues.Clear();
    }

    private CriterAPI.DataSource.ICriterDataSource ds = new CriterAPI.DataSource.EFCriterDataSource();

    protected bool AggiornaBolliniSuRct(List<string> errori)
    {
        int iDRapportoControlloTecnico = int.Parse(IDRapportoControlloTecnico);
        //Repeatable read per mettere il lock quando faccio la select 
        using (var transaction = CurrentDataContext.Database.BeginTransaction()) //BeginTransaction(System.Data.IsolationLevel.RepeatableRead)
        {
            try
            {
                RCT_RapportoDiControlloTecnicoBase rapporto = CurrentDataContext.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);
                rapporto.IDStatoRapportoDiControllo = 3;

                int iDTipologiaRct = int.Parse(IDTipologiaRct);
                object json = null;
                switch (iDTipologiaRct)
                {
                    case 1: //Gruppi Termici
                        //json = UtilityRapportiControllo.GetJsonRapporti("api/GetRapportoTecnico_GT_ByID", iDRapportoControlloTecnico);
                        POMJ_RapportoControlloTecnico_GT rapportotecnico_GT = ds.GetRapportoTecnico_GT_ByID(iDRapportoControlloTecnico);
                        json = JsonConvert.SerializeObject(rapportotecnico_GT);
                        break;
                    case 2: //Gruppi Frigo
                        //json = UtilityRapportiControllo.GetJsonRapporti("api/GetRapportoTecnico_GF_ByID", iDRapportoControlloTecnico);
                        POMJ_RapportoControlloTecnico_GF rapportotecnico_GF = ds.GetRapportoTecnico_GF_ByID(iDRapportoControlloTecnico);
                        json = JsonConvert.SerializeObject(rapportotecnico_GF);
                        break;
                    case 3: //Scambiatori
                        //json = UtilityRapportiControllo.GetJsonRapporti("api/GetRapportoTecnico_SC_ByID", iDRapportoControlloTecnico);
                        POMJ_RapportoControlloTecnico_SC rapportotecnico_SC = ds.GetRapportoTecnico_SC_ByID(iDRapportoControlloTecnico);
                        json = JsonConvert.SerializeObject(rapportotecnico_SC);
                        break;
                    case 4://Cogeneratori
                        //json = UtilityRapportiControllo.GetJsonRapporti("api/GetRapportoTecnico_CG_ByID", iDRapportoControlloTecnico);
                        POMJ_RapportoControlloTecnico_CG rapportotecnico_CG = ds.GetRapportoTecnico_CG_ByID(iDRapportoControlloTecnico);
                        json = JsonConvert.SerializeObject(rapportotecnico_CG);
                        break;
                }

                if (json != null)
                {
                    rapporto.JsonFormat = json.ToString();
                }

                var idBolliniSelezionati = UCBolliniSelector.SelectedValues.Distinct();

                int iDSoggetto = int.Parse(lblIDSoggetto.Text);
                int iDSoggettoDerived = int.Parse(lblIDSoggettoDerived.Text);
                var bolliniUtilizzabili = UtilityBollini.GetBolliniUtilizzabili(iDSoggetto, iDSoggettoDerived, true);
                foreach (long idBollino in idBolliniSelezionati)
                {
                    RCT_BollinoCalorePulito bollinoCorrente = CurrentDataContext.RCT_BollinoCalorePulito.Where(b => b.IDBollinoCalorePulito == idBollino).FirstOrDefault();

                    //Verifico che il bollino esista e che non sia associato con alcun rapporto di controllo
                    if (bollinoCorrente != null && bollinoCorrente.IDRapportoControlloTecnico == null && bolliniUtilizzabili.Any(c => c.IDBollinoCalorePulito == idBollino))
                    {
                        bollinoCorrente.IDRapportoControlloTecnico = int.Parse(IDRapportoControlloTecnico);
                        bollinoCorrente.DataOraUtilizzo = DateTime.Now;
                        bollinoCorrente.IDSoggettoUtilizzatore = iDSoggetto;
                    }
                    else
                    {
                        errori.Add("Bollino Calore pulito " + bollinoCorrente.CodiceBollino.ToString().ToUpper() + " già utilizzato per un altro rapporto di controllo tecnico");
                        transaction.Rollback();
                        CurrentDataContext.Entry(rapporto).Reload();
                        return false;
                    }
                }
                CurrentDataContext.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                errori.Add(e.Message);
                transaction.Rollback();
                return false;
            }
        }
    }
    
    #endregion

    protected void cmbFluidoVettoreEntrata_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFluidoVettoreEntrata(cmbFluidoVettoreEntrata.Value);
    }

    protected void VisibleHiddenFluidoVettoreEntrata(object IDTipologiaFluidoTermoVettoreEntrata)
    {
        if (IDTipologiaFluidoTermoVettoreEntrata != null)
        {
            if (int.Parse(IDTipologiaFluidoTermoVettoreEntrata.ToString()) == 1)
            {
                pnlFluidoVettoreEntrataAltro.Visible = true;
            }
            else
            {
                pnlFluidoVettoreEntrataAltro.Visible = false;
                txtAltroFluidoTermoVettoreEntrata.Text = "";
            }
        }
        else
        {
            pnlFluidoVettoreEntrataAltro.Visible = false;
            txtAltroFluidoTermoVettoreEntrata.Text = "";
        }
    }

    protected void cmbFluidoVettore_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenFluidoVettoreUscita(cmbFluidoVettore.Value);
    }

    protected void VisibleHiddenFluidoVettoreUscita(object IDTipologiaFluidoTermoVettoreUscita)
    {
        if (IDTipologiaFluidoTermoVettoreUscita != null)
        {
            if (int.Parse(IDTipologiaFluidoTermoVettoreUscita.ToString()) == 1)
            {
                pnlFluidoVettoreUscitaAltro.Visible = true;
            }
            else
            {
                pnlFluidoVettoreUscitaAltro.Visible = false;
                txtAltroFluidoTermoVettoreUscita.Text = "";
            }
        }
        else
        {
            pnlFluidoVettoreUscitaAltro.Visible = false;
            txtAltroFluidoTermoVettoreUscita.Text = "";
        }
    }

    protected void cmbTipologiaSistemaDistribuzione_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleHiddenTipologiaSistemaDistribuzione(cmbTipologiaDistribuzione.Value);
    }

    protected void VisibleHiddenTipologiaSistemaDistribuzione(object IDTipologiaSistemaDistribuzione)
    {
        if (IDTipologiaSistemaDistribuzione != null)
        {
            if (int.Parse(IDTipologiaSistemaDistribuzione.ToString()) == 1)
            {
                pnlTipologiaSistemaDistribuzioneAltro.Visible = true;
            }
            else
            {
                pnlTipologiaSistemaDistribuzioneAltro.Visible = false;
                txtAltroTipologiaSistemaDistribuzione.Text = "";
            }
        }
        else
        {
            pnlTipologiaSistemaDistribuzioneAltro.Visible = false;
            txtAltroTipologiaSistemaDistribuzione.Text = "";
        }
    }

    #region Updates
    public void UpdateCheckList(int iDRapportoControlloTecnico)
    {
        List<string> valoriCheckList = new List<string>();
        foreach (ListItem item in cblCheckList.Items)
        {
            if (item.Selected)
            {
                valoriCheckList.Add(item.Value);
            }
        }
        UtilityRapportiControllo.SaveInsertDeleteDatiCheckList(iDRapportoControlloTecnico, valoriCheckList.ToArray<string>());
    }

    public void UpdateTipologiaTrattamentoAcquaInvernale(int iDRapportoControlloTecnico)
    {
        List<string> valoriTrattamentoAcqua = new List<string>();
        foreach (ListItem item in cblTipologiaTrattamentoAcquaInvernale.Items)
        {
            if (item.Selected)
            {
                valoriTrattamentoAcqua.Add(item.Value);
            }
        }
        UtilityRapportiControllo.SaveInsertDeleteDatiTrattamentoAcquaInvernale(iDRapportoControlloTecnico, valoriTrattamentoAcqua.ToArray<string>());
    }

    public void UpdateTipologiaTrattamentoAcquaAcs(int iDRapportoControlloTecnico)
    {
        List<string> valoriTrattamentoAcqua = new List<string>();
        foreach (ListItem item in cblTipologiaTrattamentoAcquaAcs.Items)
        {
            if (item.Selected)
            {
                valoriTrattamentoAcqua.Add(item.Value);
            }
        }
        UtilityRapportiControllo.SaveInsertDeleteDatiTrattamentoAcquaAcs(iDRapportoControlloTecnico, valoriTrattamentoAcqua.ToArray<string>());
    }

    public void UpdateRapporto()
    {
        #region Salvataggio rapporto di controllo
        int iDRapportoControlloTecnico = int.Parse(IDRapportoControlloTecnico);
        var rapportoBase = CurrentDataContext.RCT_RapportoDiControlloTecnicoBase.Find(iDRapportoControlloTecnico);
        UpdateTipologiaTrattamentoAcquaInvernale(iDRapportoControlloTecnico);
        UpdateTipologiaTrattamentoAcquaAcs(iDRapportoControlloTecnico);
        UpdateCheckList(iDRapportoControlloTecnico);

        switch (rapportoBase.IDTipologiaRCT)
        {
            case (int)RCT_TipoRapportoDiControlloTecnico.GT:
                var rapportoGt = CurrentDataContext.RCT_RapportoDiControlloTecnicoGT.Find(iDRapportoControlloTecnico);
                UpdateBase(rapportoBase);
                UpdateGt(rapportoGt);
                break;
            case (int)RCT_TipoRapportoDiControlloTecnico.GF:
                var rapportoGf = CurrentDataContext.RCT_RapportoDiControlloTecnicoGF.Find(iDRapportoControlloTecnico);
                UpdateBase(rapportoBase);
                UpdateGf(rapportoGf);
                break;
            case (int)RCT_TipoRapportoDiControlloTecnico.SC:
                var rapportoSc = CurrentDataContext.RCT_RapportoDiControlloTecnicoSC.Find(iDRapportoControlloTecnico);
                UpdateBase(rapportoBase);
                UpdateSc(rapportoSc);
                break;
            case (int)RCT_TipoRapportoDiControlloTecnico.CG:
                var rapportoCg = CurrentDataContext.RCT_RapportoDiControlloTecnicoCG.Find(iDRapportoControlloTecnico);
                UpdateBase(rapportoBase);
                UpdateCg(rapportoCg);
                break;
        }

        if (ModelState.IsValid)
        {
            try
            {
                CurrentDataContext.SaveChanges();
                UtilityRapportiControllo.SetFieldsRaccomandazioniPrescrizioni(int.Parse(IDRapportoControlloTecnico));
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion
    }

    public void UpdateBase(RCT_RapportoDiControlloTecnicoBase rapporto)
    {
        int? iDSoggettoDerived = null;
        if (!string.IsNullOrEmpty(lblIDSoggettoDerived.Text))
        {
            iDSoggettoDerived = int.Parse(lblIDSoggettoDerived.Text);
        }
        else if (cmbAddetti.Value != null)
        {
            iDSoggettoDerived = int.Parse(cmbAddetti.Value.ToString());
        }

        if (iDSoggettoDerived != null)
        {
            rapporto.IDSoggettoDerived = iDSoggettoDerived;
        }
        //rapporto.PotenzaTermicaNominale = decimal.Parse(txtPotenzaTermicaNominaleTotaleMax.Text);
        //rapporto.Indirizzo = null;
        //rapporto.Civico = null;
        //rapporto.Palazzo = null;
        //rapporto.Scala = null;
        //rapporto.Interno = null;
        //rapporto.IDCodiceCatastale = null;
        //rapporto.IDTipologiaRCT = null;
        //rapporto.IDLibrettoImpianto = null;
        //rapporto.IDStatoRapportoDiControllo = null;
        rapporto.IDTipologiaControllo = int.Parse(rblTipologiaControllo.SelectedItem.Value);
        //rapporto.IDTipologiaResponsabile = null;
        //rapporto.NomeResponsabile = null;
        //rapporto.CognomeResponsabile = null;
        //rapporto.CodiceFiscaleResponsabile = null;
        //rapporto.RagioneSocialeResponsabile = null;
        //rapporto.PartitaIVAResponsabile = null;
        //rapporto.IndirizzoResponsabile = null;
        //rapporto.CivicoResponsabile = null;
        //rapporto.IDComuneResponsabile = null;
        //rapporto.IDProvinciaResponsabile = null;
        //rapporto.RagioneSocialeTerzoResponsabile = null;
        //rapporto.PartitaIVATerzoResponsabile = null;
        //rapporto.IndirizzoTerzoResponsabile = null;
        //rapporto.CivicoTerzoResponsabile = null;
        //rapporto.IDComuneTerzoResponsabile = null;
        //rapporto.IDProvinciaTerzoResponsabile = null;
        //rapporto.RagioneSocialeImpresaManutentrice = null;
        //rapporto.PartitaIVAImpresaManutentrice = null;
        //rapporto.IndirizzoImpresaManutentrice = null;
        //rapporto.CivicoImpresaManutentrice = null;
        //rapporto.IDComuneImpresaManutentrice = null;
        //rapporto.IDProvinciaImpresaManutentrice = null;
        if (!string.IsNullOrEmpty(txtPotenzaTermicaNominaleTotaleMax.Text))
        {
            rapporto.PotenzaTermicaNominaleTotaleMax = decimal.Parse(txtPotenzaTermicaNominaleTotaleMax.Text);
        }
        else
        {
            rapporto.PotenzaTermicaNominaleTotaleMax = null;
        }

        rapporto.fDichiarazioneConformita = bool.Parse(rblDichiarazioneConformita.SelectedItem.Value);
        rapporto.fLibrettoImpiantoPresente = bool.Parse(rblLibrettoImpiantoPresente.SelectedItem.Value);
        rapporto.fUsoManutenzioneGeneratore = bool.Parse(rblUsoManutenzioneGeneratore.SelectedItem.Value);
        rapporto.fLibrettoImpiantoCompilato = bool.Parse(rblLibrettoImpiantoCompilato.SelectedItem.Value);
        if (!string.IsNullOrEmpty(txtDurezzaAcqua.Text))
        {
            rapporto.DurezzaAcqua = decimal.Parse(txtDurezzaAcqua.Text);
        }
        else
        {
            rapporto.DurezzaAcqua = null;
        }
        rapporto.TrattamentoRiscaldamento = int.Parse(rblTrattamentoRiscaldamento.SelectedItem.Value);
        rapporto.TrattamentoACS = int.Parse(rblTrattamentoAcs.SelectedItem.Value);
        rapporto.LocaleInstallazioneIdoneo = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkLocaleInstallazioneIdoneo")).Value;
        rapporto.DimensioniApertureAdeguate = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkDimensioniApertureAdeguate")).Value;
        rapporto.ApertureLibere = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkApertureLibere")).Value;
        rapporto.LineeElettricheIdonee = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkLineeElettricheIdonee")).Value;
        rapporto.ScarichiIdonei = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkScarichiIdonei")).Value;
        rapporto.AssenzaPerditeCombustibile = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeCombustibile")).Value;
        rapporto.CoibentazioniIdonee = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkCoibentazioniIdonee")).Value;
        rapporto.TenutaImpiantoIdraulico = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkTenutaImpiantoIdraulico")).Value;
        rapporto.fClimatizzazioneInvernale = chkClimatizzazioneInvernale.Checked;
        rapporto.fProduzioneACS = chkProduzioneACS.Checked;
        rapporto.fClimatizzazioneEstiva = chkClimatizzazioneEstiva.Checked;
        rapporto.StatoCoibentazioniIdonee = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkStatoCoibentazioniIdonee")).Value;
        if (!string.IsNullOrEmpty(txtOsservazioni.Text))
        {
            rapporto.Osservazioni = txtOsservazioni.Text;
        }
        else
        {
            rapporto.Osservazioni = null;
        }
        if (!string.IsNullOrEmpty(txtRaccomandazioni.Text))
        {
            rapporto.Raccomandazioni = txtRaccomandazioni.Text;
        }
        else
        {
            rapporto.Raccomandazioni = null;
        }
        if (!string.IsNullOrEmpty(txtPrescrizioni.Text))
        {
            rapporto.Prescrizioni = txtPrescrizioni.Text;
        }
        else
        {
            rapporto.Prescrizioni = null;
        }
        rapporto.fImpiantoFunzionante = bool.Parse(rblImpiantoFunzionante.SelectedItem.Value);
        if (!string.IsNullOrEmpty(txtDataManutenzioneConsigliata.Text))
        {
            rapporto.DataManutenzioneConsigliata = DateTime.Parse(txtDataManutenzioneConsigliata.Text);
        }
        else
        {
            rapporto.DataManutenzioneConsigliata = null;
        }
        if (!string.IsNullOrEmpty(txtDataControllo.Text))
        {
            rapporto.DataControllo = DateTime.Parse(txtDataControllo.Text);
        }
        else
        {
            rapporto.DataControllo = null;
        }
        if (!string.IsNullOrEmpty(txtOraArrivo.Text))
        {
            rapporto.OraArrivo = DateTime.Parse(txtOraArrivo.Text);
        }
        else
        {
            rapporto.OraArrivo = DateTime.Now;
        }
        if (!string.IsNullOrEmpty(txtOraPartenza.Text))
        {
            rapporto.OraPartenza = DateTime.Parse(txtOraPartenza.Text);
        }
        else
        {
            rapporto.OraPartenza = DateTime.Now;
        }

        if (cmbTipologiaDistribuzione.Value != null)
        {
            rapporto.IDTipologiaSistemaDistribuzione = int.Parse(cmbTipologiaDistribuzione.Value.ToString());
        }
        else
        {
            rapporto.IDTipologiaSistemaDistribuzione = null;
        }

        if (string.IsNullOrEmpty(txtAltroTipologiaSistemaDistribuzione.Text))
        {
            rapporto.AltroTipologiaSistemaDistribuzione = null;
        }
        else
        {
            rapporto.AltroTipologiaSistemaDistribuzione = txtAltroTipologiaSistemaDistribuzione.Text;
        }

        rapporto.Contabilizzazione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkContabilizzazione")).Value;

        if (cmbTipologiaContabilizzazione.Value != null)
        {
            rapporto.IDTipologiaContabilizzazione = int.Parse(cmbTipologiaContabilizzazione.Value.ToString());
        }
        else
        {
            rapporto.IDTipologiaContabilizzazione = null;
        }

        rapporto.Termoregolazione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkTermoregolazione")).Value;
        rapporto.CorrettoFunzionamentoContabilizzazione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkCorrettoFunzionamentoContabilizzazione")).Value;
    }

    public void UpdateGt(RCT_RapportoDiControlloTecnicoGT rapporto)
    {
        rapporto.GeneratoriIdonei = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkGeneratoriIdonei")).Value;
        rapporto.RegolazioneTemperaturaAmbiente = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkRegolazioneTemperaturaAmbiente")).Value;
        if (cmbTipologiaGeneratoriTermici.Value != null)
        {
            rapporto.IdTipologiaGeneratoriTermici = int.Parse(cmbTipologiaGeneratoriTermici.Value.ToString());
        }
        else
        {
            rapporto.IdTipologiaGeneratoriTermici = null;
        }
        if (!string.IsNullOrEmpty(lblPotenzaTermicaNominaleFocolare.Text))
        {
            rapporto.PotenzaTermicaNominaleFocolare = decimal.Parse(lblPotenzaTermicaNominaleFocolare.Text);
        }
        rapporto.EvacuazioneForzata = rblEvacuazioneForzata.Checked;
        rapporto.EvacuazioneNaturale = rblEvacuazioneNaturale.Checked;
        if (!string.IsNullOrEmpty(txtDepressioneCanaleFumo.Text))
        {
            rapporto.DepressioneCanaleFumo = decimal.Parse(txtDepressioneCanaleFumo.Text);
        }
        else
        {
            rapporto.DepressioneCanaleFumo = null;
        }

        rapporto.DispositiviComandoRegolazione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviComandoRegolazione")).Value;
        rapporto.DispositiviSicurezza = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviSicurezza")).Value;
        rapporto.ValvolaSicurezzaSovrappressione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkValvolaSicurezzaSovrappressione")).Value;
        rapporto.ScambiatoreFumiPulito = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoreFumiPulito")).Value;
        rapporto.RiflussoProdottiCombustione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkRiflussoProdottiCombustione")).Value;
        rapporto.ConformitaUNI10389 = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkConformitàUNI10389")).Value;

        RCT_UC_UCVerificaEnergeticaGT RCT_UC_UCVerificaEnergeticaGT = ((RCT_UC_UCVerificaEnergeticaGT)MainFormView.FindControl("UCVerificaEnergeticaGT"));
        decimal temperaturaFumi = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtTemperaturaFumi")).Text, out temperaturaFumi))
        {
            rapporto.TemperaturaFumi = temperaturaFumi;
        }
        else
        {
            rapporto.TemperaturaFumi = null;
        }
        decimal temperaturaComburente = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtTemperaturaComburente")).Text, out temperaturaComburente))
        {
            rapporto.TemperaraturaComburente = temperaturaComburente;
        }
        else
        {
            rapporto.TemperaraturaComburente = null;
        }
        decimal o2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtO2")).Text, out o2))
        {
            rapporto.O2 = o2;
        }
        else
        {
            rapporto.O2 = null;
        }
        decimal co2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCO2")).Text, out co2))
        {
            rapporto.Co2 = co2;
        }
        else
        {
            rapporto.Co2 = null;
        }
        decimal rendimentoCombustione = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtRendimentoCombustione")).Text, out rendimentoCombustione))
        {
            rapporto.RendimentoCombustione = rendimentoCombustione;
        }
        else
        {
            rapporto.RendimentoCombustione = null;
        }
                
        decimal? rendimentoMinimo = UtilityRapportiControllo.GetRendimentoMinimoDiLegge(
                                                                   cmbTipologiaGeneratoriTermici.Value, 
                                                                    UtilityApp.ParseNullableDatetime(lblDataInstallazione.Text), 
                                                                    UtilityApp.ParseNullableDecimal(lblPotenzaTermicaNominale.Text));
        if (rendimentoMinimo != null)
        {
            ((Label)RCT_UC_UCVerificaEnergeticaGT.FindControl("lblRendimentoMinimo")).Text = rendimentoMinimo.ToString();
            rapporto.RendimentoMinimo = rendimentoMinimo;
        }
        else
        {
            ((Label)RCT_UC_UCVerificaEnergeticaGT.FindControl("lblRendimentoMinimo")).Text = string.Empty;
            rapporto.RendimentoMinimo = null;
        }
                
        decimal bacharach1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach1")).Text, out bacharach1))
        {
            rapporto.bacharach1 = bacharach1;
        }
        else
        {
            rapporto.bacharach1 = null;
        }
        decimal bacharach2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach2")).Text, out bacharach2))
        {
            rapporto.bacharach2 = bacharach2;
        }
        else
        {
            rapporto.bacharach2 = null;
        }
        decimal bacharach3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach3")).Text, out bacharach3))
        {
            rapporto.bacharach3 = bacharach3;
        }
        else
        {
            rapporto.bacharach3 = null;
        }
        decimal coCorretto = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCoCorretto")).Text, out coCorretto))
        {
            rapporto.CoCorretto = coCorretto;
        }
        else
        {
            rapporto.CoCorretto = null;
        }
        if (!string.IsNullOrEmpty(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtModuloTermico")).Text))
        {
            rapporto.ModuloTermico = ((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtModuloTermico")).Text;
        }
        else
        {
            rapporto.ModuloTermico = null;
        }
        //rapporto.IDLIM_LibrettiImpiantiGruppitermici = null;
        decimal cOFumiSecchi = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCoFumiSecchi")).Text, out cOFumiSecchi))
        {
            rapporto.COFumiSecchi = cOFumiSecchi;
        }
        else
        {
            rapporto.COFumiSecchi = null;
        }
        decimal portataCombustibile = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtPortataCombustibile")).Text, out portataCombustibile))
        {
            rapporto.PortataCombustibile = portataCombustibile;
        }
        else
        {
            rapporto.PortataCombustibile = null;
        }
        RCT_UC_Checkbox chkRispettaIndiceBacharach = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkRispettaIndiceBacharach"));
        rapporto.RispettaIndiceBacharach = UtilityApp.ToBoolean(chkRispettaIndiceBacharach.Value.ToString());
        RCT_UC_Checkbox chkCOFumiSecchiNoAria1000 = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkCOFumiSecchiNoAria1000"));
        rapporto.COFumiSecchiNoAria1000 = UtilityApp.ToBoolean(chkCOFumiSecchiNoAria1000.Value.ToString());
        RCT_UC_Checkbox chkRendimentoSupMinimo = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkRendimentoSupMinimo"));
        rapporto.RendimentoSupMinimo = UtilityApp.ToBoolean(chkRendimentoSupMinimo.Value.ToString());
        decimal potenzaTermicaEffettiva = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtPotenzaTermicaEffettiva")).Text, out potenzaTermicaEffettiva))
        {
            rapporto.PotenzaTermicaEffettiva = potenzaTermicaEffettiva;
        }
        else
        {
            rapporto.PotenzaTermicaEffettiva = null;
        }
    }

    public void UpdateGf(RCT_RapportoDiControlloTecnicoGF rapporto)
    {
        rapporto.ProvaRaffrescamento = rbRaffrescamento.Checked;
        rapporto.ProvaRiscaldamento = rbRiscaldamento.Checked;
        rapporto.AssenzaPerditeRefrigerante = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeRefrigerante")).Value;
        rapporto.FiltriPuliti = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkFiltriPuliti")).Value;
        rapporto.LeakDetector = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkLeakDetector")).Value;
        rapporto.ScambiatoriLiberi = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoriLiberi")).Value;
        rapporto.ParametriTermodinamici = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkParametriTermodinamici")).Value;

        RCT_UC_UCVerificaEnergeticaGF RCT_UC_UCVerificaEnergeticaGF = ((RCT_UC_UCVerificaEnergeticaGF)MainFormView.FindControl("UCVerificaEnergeticaGF"));
        decimal temperaturaSurriscaldamento = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtSurriscaldamento")).Text, out temperaturaSurriscaldamento))
        {
            rapporto.TemperaturaSurriscaldamento = temperaturaSurriscaldamento;
        }
        else
        {
            rapporto.TemperaturaSurriscaldamento = null;
        }
        decimal temperaturaSottoraffreddamento = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtSottoraffreddamento")).Text, out temperaturaSottoraffreddamento))
        {
            rapporto.TemperaturaSottoraffreddamento = temperaturaSottoraffreddamento;
        }
        else
        {
            rapporto.TemperaturaSottoraffreddamento = null;
        }
        decimal temperaturaCondensazione = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTemperaturaCondensazione")).Text, out temperaturaCondensazione))
        {
            rapporto.TemperaturaCondensazione = temperaturaCondensazione;
        }
        else
        {
            rapporto.TemperaturaCondensazione = null;
        }
        decimal temperaturaEvaporazione = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTemperaturaEvaporazione")).Text, out temperaturaEvaporazione))
        {
            rapporto.TemperaturaEvaporazione = temperaturaEvaporazione;
        }
        else
        {
            rapporto.TemperaturaEvaporazione = null;
        }
        decimal tInEst = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTinEst")).Text, out tInEst))
        {
            rapporto.TInglatoEst = tInEst;
        }
        else
        {
            rapporto.TInglatoEst = null;
        }
        decimal tOutEst = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtToutEst")).Text, out tOutEst))
        {
            rapporto.TUscLatoEst = tOutEst;
        }
        else
        {
            rapporto.TUscLatoEst = null;
        }
        decimal tInUtenze = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTinUtenze")).Text, out tInUtenze))
        {
            rapporto.TIngLatoUtenze = tInUtenze;
        }
        else
        {
            rapporto.TIngLatoUtenze = null;
        }
        decimal tOutUtenze = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtToutUtenze")).Text, out tOutUtenze))
        {
            rapporto.TUscLatoUtenze = tOutUtenze;
        }
        else
        {
            rapporto.TUscLatoUtenze = null;
        }
        decimal potenzaAssorbita = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtPotenzaAssorbita")).Text, out potenzaAssorbita))
        {
            rapporto.PotenzaAssorbita = potenzaAssorbita;
        }
        else
        {
            rapporto.PotenzaAssorbita = null;
        }
        decimal tUscitaFluido = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTUscitaFluido")).Text, out tUscitaFluido))
        {
            rapporto.TUscitaFluido = tUscitaFluido;
        }
        else
        {
            rapporto.TUscitaFluido = null;
        }
        decimal tBulboUmidoAria = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTBulboUmidoAria")).Text, out tBulboUmidoAria))
        {
            rapporto.TBulboUmidoAria = tBulboUmidoAria;
        }
        else
        {
            rapporto.TBulboUmidoAria = null;
        }
        decimal tIngressoLatoEsterno = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTIngressoLatoEsterno")).Text, out tIngressoLatoEsterno))
        {
            rapporto.TIngressoLatoEsterno = tIngressoLatoEsterno;
        }
        else
        {
            rapporto.TIngressoLatoEsterno = null;
        }
        decimal tUscitaLatoEsterno = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTUscitaLatoEsterno")).Text, out tUscitaLatoEsterno))
        {
            rapporto.TUscitaLatoEsterno = tUscitaLatoEsterno;
        }
        else
        {
            rapporto.TUscitaLatoEsterno = null;
        }
        decimal tIngressoLatoMacchina = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTIngressoLatoMacchina")).Text, out tIngressoLatoMacchina))
        {
            rapporto.TIngressoLatoMacchina = tIngressoLatoMacchina;
        }
        else
        {
            rapporto.TIngressoLatoMacchina = null;
        }
        decimal tUscitaLatoMacchina = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtTUscitaLatoMacchina")).Text, out tUscitaLatoMacchina))
        {
            rapporto.TUscitaLatoMacchina = tUscitaLatoMacchina;
        }
        else
        {
            rapporto.TUscitaLatoMacchina = null;
        }
        decimal NCircuito = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaGF.FindControl("txtNCircuito")).Text, out NCircuito))
        {
            rapporto.NCircuiti = NCircuito;
        }
        else
        {
            rapporto.NCircuiti = null;
        }
        //rapporto.Potenzafrigorifera = null;
        //rapporto.IdLIM_LibrettiImpiantiMacchineFrigorifere = null;
        //rapporto.IDTipologiemacchineFrigorifere = null;
        //rapporto.NCircuiti = null;
    }

    public void UpdateSc(RCT_RapportoDiControlloTecnicoSC rapporto)
    {
        if (cmbFluidoVettoreEntrata.Value != null)
        {
            rapporto.IDTipologiaFluidoTermoVettoreEntrata = int.Parse(cmbFluidoVettoreEntrata.Value.ToString());
        }
        else
        {
            rapporto.IDTipologiaFluidoTermoVettoreEntrata = null;
        }
        if (string.IsNullOrEmpty(txtAltroFluidoTermoVettoreEntrata.Text))
        {
            rapporto.AltroTipologiaFluidoTermoVettoreEntrata = null;
        }
        else
        {
            rapporto.AltroTipologiaFluidoTermoVettoreEntrata = txtAltroFluidoTermoVettoreEntrata.Text;
        }

        if (cmbFluidoVettore.Value != null)
        {
            rapporto.IDTipologiaFluidoTermoVettoreUscita = int.Parse(cmbFluidoVettore.Value.ToString());
        }
        else
        {
            rapporto.IDTipologiaFluidoTermoVettoreUscita = null;
        }
        if (string.IsNullOrEmpty(txtAltroFluidoTermoVettoreUscita.Text))
        {
            rapporto.AltroTipologiaFluidoTermoVettoreUscita = null;
        }
        else
        {
            rapporto.AltroTipologiaFluidoTermoVettoreUscita = txtAltroFluidoTermoVettoreUscita.Text;
        }

        rapporto.PotenzaCompatibileProgetto = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkPotenzaCompatibileProgetto")).Value;
        rapporto.AssenzaTrafilamenti = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaTrafilamenti")).Value;

        RCT_UC_UCVerificaEnergeticaSC RCT_UC_UCVerificaEnergeticaSC = ((RCT_UC_UCVerificaEnergeticaSC)MainFormView.FindControl("UCVerificaEnergeticaSC"));

        decimal temperaturaEsterna = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaSC.FindControl("txtTemperaturaEsterna")).Text, out temperaturaEsterna))
        {
            rapporto.TemperaturaEsterna = temperaturaEsterna;
        }
        else
        {
            rapporto.TemperaturaEsterna = null;
        }
        decimal temperaturaMandataPrimario = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaSC.FindControl("txtTemperaturaMandataPrimario")).Text, out temperaturaMandataPrimario))
        {
            rapporto.TemperaturaMandataPrimario = temperaturaMandataPrimario;
        }
        else
        {
            rapporto.TemperaturaMandataPrimario = null;
        }
        decimal termperaturaRitornoPrimario = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaSC.FindControl("txtTemperaturaRitornoPrimario")).Text, out termperaturaRitornoPrimario))
        {
            rapporto.TemperaturaRitornoPrimario = termperaturaRitornoPrimario;
        }
        else
        {
            rapporto.TemperaturaRitornoPrimario = null;
        }
        decimal portataFluidoPrimario = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaSC.FindControl("txtPortataFluidoPrimario")).Text, out portataFluidoPrimario))
        {
            rapporto.PortataFluidoPrimario = portataFluidoPrimario;
        }
        else
        {
            rapporto.PortataFluidoPrimario = null;
        }
        decimal temperaturaMandataSecondario = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaSC.FindControl("txtTemperaturamandataSecondario")).Text, out temperaturaMandataSecondario))
        {
            rapporto.TemperaturaMandataSecondario = temperaturaMandataSecondario;
        }
        else
        {
            rapporto.TemperaturaMandataSecondario = null;
        }
        decimal temperaturaRitornoSecondario = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaSC.FindControl("txtTemperaturaRitornoSecondario")).Text, out temperaturaRitornoSecondario))
        {
            rapporto.TemperaturaRitornoSecondario = temperaturaRitornoSecondario;
        }
        else
        {
            rapporto.TemperaturaRitornoSecondario = null;
        }
        decimal potenzaTermica = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaSC.FindControl("txtPotenzaTermica")).Text, out potenzaTermica))
        {
            rapporto.PotenzaTermica = potenzaTermica;
        }
        else
        {
            rapporto.PotenzaTermica = null;
        }
                
        //rapporto.IdLIM_LibrettiImpiantiScambaitoriCalore = null;
        //rapporto.DispositiviComandoRegolazione = null;
    }

    public void UpdateCg(RCT_RapportoDiControlloTecnicoCG rapporto)
    {
        rapporto.CapsulaInsonorizzataIdonea = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkCapsulaInsonorizzazioneIdonea")).Value;
        rapporto.TenutaCircuitoOlioIdonea = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkTenutaCircuitoOlioIdonea")).Value;
        rapporto.FunzionalitàScambiatoreSeparazione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkFunzionalitaScambiatoreSeparazione")).Value;
        if (!string.IsNullOrEmpty(txtPotenzaAssorbitaCombustibile.Text))
        {
            rapporto.PotenzaAssorbitaCombustibile = decimal.Parse(txtPotenzaAssorbitaCombustibile.Text);
        }
        else
        {
            rapporto.PotenzaAssorbitaCombustibile = null;
        }
        if (!string.IsNullOrEmpty(txtPotenzaBypass.Text))
        {
            rapporto.PotenzaByPass = decimal.Parse(txtPotenzaBypass.Text);
        }
        else
        {
            rapporto.PotenzaByPass = null;
        }

        RCT_UC_UCVerificaEnergeticaCG RCT_UC_UCVerificaEnergeticaCG = ((RCT_UC_UCVerificaEnergeticaCG)MainFormView.FindControl("UCVerificaEnergeticaCG"));

        decimal temperaturaAriaComburente = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtTemperaturaAriaComburente")).Text, out temperaturaAriaComburente))
        {
            rapporto.TemperaturaAriaComburente = temperaturaAriaComburente;
        }
        else
        {
            rapporto.TemperaturaAriaComburente = null;
        }
        decimal temperaturaAcquaIngresso = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtTemperaturaAcquaIngresso")).Text, out temperaturaAcquaIngresso))
        {
            rapporto.TemperaturaAcquaIngresso = temperaturaAcquaIngresso;
        }
        else
        {
            rapporto.TemperaturaAcquaIngresso = null;
        }
        decimal temperaturaAcquaUscita = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtTemperaturaAcquaUscita")).Text, out temperaturaAcquaUscita))
        {
            rapporto.TemperaturaAcquauscita = temperaturaAcquaUscita;
        }
        else
        {
            rapporto.TemperaturaAcquauscita = null;
        }
        decimal potenzaAiMorsetti = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtPotenzaaiMorsetti")).Text, out potenzaAiMorsetti))
        {
            rapporto.PotenzaAiMorsetti = potenzaAiMorsetti;
        }
        else
        {
            rapporto.PotenzaAiMorsetti = null;
        }
        decimal temperaturaAcquaMotore = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtTemperaturaAcquaMotore")).Text, out temperaturaAcquaMotore))
        {
            rapporto.TemperaturaAcquaMotore = temperaturaAcquaMotore;
        }
        else
        {
            rapporto.TemperaturaAcquaMotore = null;
        }
        decimal temperaturaFumiMonte = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtTemperaturaFumiMonte")).Text, out temperaturaFumiMonte))
        {
            rapporto.TemperaturaFumiMonte = temperaturaFumiMonte;
        }
        else
        {
            rapporto.TemperaturaFumiMonte = null;
        }
        decimal temperaturaFumiValle = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtTemperaturaFumiValle")).Text, out temperaturaFumiValle))
        {
            rapporto.TemperaturaFumiValle = temperaturaFumiValle;
        }
        else
        {
            rapporto.TemperaturaFumiValle = null;
        }
        decimal emissioneMonossido = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtEmissioneMonossido")).Text, out emissioneMonossido))
        {
            rapporto.EmissioneMonossido = emissioneMonossido;
        }
        else
        {
            rapporto.EmissioneMonossido = null;
        }
        decimal sovrafrequenzaSogliaInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovrafrequenzaSogliaInterv1")).Text, out sovrafrequenzaSogliaInterv1))
        {
            rapporto.SovrafrequenzaSogliaInterv1 = sovrafrequenzaSogliaInterv1;
        }
        else
        {
            rapporto.SovrafrequenzaSogliaInterv1 = null;
        }
        decimal SovrafrequenzaSogliaInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovrafrequenzaSogliaInterv2")).Text, out SovrafrequenzaSogliaInterv2))
        {
            rapporto.SovrafrequenzaSogliaInterv2 = SovrafrequenzaSogliaInterv2;
        }
        else
        {
            rapporto.SovrafrequenzaSogliaInterv2 = null;
        }
        decimal SovrafrequenzaSogliaInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovrafrequenzaSogliaInterv3")).Text, out SovrafrequenzaSogliaInterv3))
        {
            rapporto.SovrafrequenzaSogliaInterv3 = SovrafrequenzaSogliaInterv3;
        }
        else
        {
            rapporto.SovrafrequenzaSogliaInterv3 = null;
        }
        decimal SovrafrequenzaTempoInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovrafrequenzaTempoInterv1")).Text, out SovrafrequenzaTempoInterv1))
        {
            rapporto.SovrafrequenzaTempoInterv1 = SovrafrequenzaTempoInterv1;
        }
        else
        {
            rapporto.SovrafrequenzaTempoInterv1 = null;
        }
        decimal SovrafrequenzaTempoInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovrafrequenzaTempoInterv2")).Text, out SovrafrequenzaTempoInterv2))
        {
            rapporto.SovrafrequenzaTempoInterv2 = SovrafrequenzaTempoInterv2;
        }
        else
        {
            rapporto.SovrafrequenzaTempoInterv2 = null;
        }
        decimal SovrafrequenzaTempoInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovrafrequenzaTempoInterv3")).Text, out SovrafrequenzaTempoInterv3))
        {
            rapporto.SovrafrequenzaTempoInterv3 = SovrafrequenzaTempoInterv3;
        }
        else
        {
            rapporto.SovrafrequenzaTempoInterv3 = null;
        }
        decimal SottofrequenzaSogliaInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottofrequenzaSogliaInterv1")).Text, out SottofrequenzaSogliaInterv1))
        {
            rapporto.SottofrequenzaSogliaInterv1 = SottofrequenzaSogliaInterv1;
        }
        else
        {
            rapporto.SottofrequenzaSogliaInterv1 = null;
        }
        decimal SottofrequenzaSogliaInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottofrequenzaSogliaInterv2")).Text, out SottofrequenzaSogliaInterv2))
        {
            rapporto.SottofrequenzaSogliaInterv2 = SottofrequenzaSogliaInterv2;
        }
        else
        {
            rapporto.SottofrequenzaSogliaInterv2 = null;
        }
        decimal SottofrequenzaSogliaInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottofrequenzaSogliaInterv3")).Text, out SottofrequenzaSogliaInterv3))
        {
            rapporto.SottofrequenzaSogliaInterv3 = SottofrequenzaSogliaInterv3;
        }
        else
        {
            rapporto.SottofrequenzaSogliaInterv3 = null;
        }
        decimal SottofrequenzaTempoInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottofrequenzaTempoInterv1")).Text, out SottofrequenzaTempoInterv1))
        {
            rapporto.SottofrequenzaTempoInterv1 = SottofrequenzaTempoInterv1;
        }
        else
        {
            rapporto.SottofrequenzaTempoInterv1 = null;
        }
        decimal SottofrequenzaTempoInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottofrequenzaTempoInterv2")).Text, out SottofrequenzaTempoInterv2))
        {
            rapporto.SottofrequenzaTempoInterv2 = SottofrequenzaTempoInterv2;
        }
        else
        {
            rapporto.SottofrequenzaTempoInterv2 = null;
        }
        decimal SottofrequenzaTempoInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottofrequenzaTempoInterv3")).Text, out SottofrequenzaTempoInterv3))
        {
            rapporto.SottofrequenzaTempoInterv3 = SottofrequenzaTempoInterv3;
        }
        else
        {
            rapporto.SottofrequenzaTempoInterv3 = null;
        }
        decimal SovratensioneSogliaInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovratensioneSogliaInterv1")).Text, out SovratensioneSogliaInterv1))
        {
            rapporto.SovratensioneSogliaInterv1 = SovratensioneSogliaInterv1;
        }
        else
        {
            rapporto.SovratensioneSogliaInterv1 = null;
        }
        decimal SovratensioneSogliaInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovratensioneSogliaInterv2")).Text, out SovratensioneSogliaInterv2))
        {
            rapporto.SovratensioneSogliaInterv2 = SovratensioneSogliaInterv2;
        }
        else
        {
            rapporto.SovratensioneSogliaInterv2 = null;
        }
        decimal SovratensioneSogliaInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovratensioneSogliaInterv3")).Text, out SovratensioneSogliaInterv3))
        {
            rapporto.SovratensioneSogliaInterv3 = SovratensioneSogliaInterv3;
        }
        else
        {
            rapporto.SovratensioneSogliaInterv3 = null;
        }
        decimal SovratensioneTempoInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovratensioneTempoInterv1")).Text, out SovratensioneTempoInterv1))
        {
            rapporto.SovratensioneTempoInterv1 = SovratensioneTempoInterv1;
        }
        else
        {
            rapporto.SovratensioneTempoInterv1 = null;
        }
        decimal SovratensioneTempoInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovratensioneTempoInterv2")).Text, out SovratensioneTempoInterv2))
        {
            rapporto.SovratensioneTempoInterv2 = SovratensioneTempoInterv2;
        }
        else
        {
            rapporto.SovratensioneTempoInterv2 = null;
        }
        decimal SovratensioneTempoInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSovratensioneTempoInterv3")).Text, out SovratensioneTempoInterv3))
        {
            rapporto.SovratensioneTempoInterv3 = SovratensioneTempoInterv3;
        }
        else
        {
            rapporto.SovratensioneTempoInterv3 = null;
        }
        decimal SottotensioneSogliaInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottotensioneSogliaInterv1")).Text, out SottotensioneSogliaInterv1))
        {
            rapporto.SottotensioneSogliaInterv1 = SottotensioneSogliaInterv1;
        }
        else
        {
            rapporto.SottotensioneSogliaInterv1 = null;
        }
        decimal SottotensioneSogliaInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottotensioneSogliaInterv2")).Text, out SottotensioneSogliaInterv2))
        {
            rapporto.SottotensioneSogliaInterv2 = SottotensioneSogliaInterv2;
        }
        else
        {
            rapporto.SottotensioneSogliaInterv2 = null;
        }
        decimal SottotensioneSogliaInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottotensioneSogliaInterv3")).Text, out SottotensioneSogliaInterv3))
        {
            rapporto.SottotensioneSogliaInterv3 = SottotensioneSogliaInterv3;
        }
        else
        {
            rapporto.SottotensioneSogliaInterv3 = null;
        }
        decimal SottotensioneTempoInterv1 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottotensioneTempoInterv1")).Text, out SottotensioneTempoInterv1))
        {
            rapporto.SottotensioneTempoInterv1 = SottotensioneTempoInterv1;
        }
        else
        {
            rapporto.SottotensioneTempoInterv1 = null;
        }
        decimal SottotensioneTempoInterv2 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottotensioneTempoInterv2")).Text, out SottotensioneTempoInterv2))
        {
            rapporto.SottotensioneTempoInterv2 = SottotensioneTempoInterv2;
        }
        else
        {
            rapporto.SottotensioneTempoInterv2 = null;
        }
        decimal SottotensioneTempoInterv3 = 0;
        if (decimal.TryParse(((TextBox)RCT_UC_UCVerificaEnergeticaCG.FindControl("txtSottotensioneTempoInterv3")).Text, out SottotensioneTempoInterv3))
        {
            rapporto.SottotensioneTempoInterv3 = SottotensioneTempoInterv3;
        }
        else
        {
            rapporto.SottotensioneTempoInterv3 = null;
        }

        //rapporto.PotenzaElettricaMorsetti = null;
        //rapporto.PotenzaMassimoRecupero = null;
        //rapporto.IdLIM_LibrettiImpiantiCogeneratori = null;
        //rapporto.IDTipologiaFluidoTermoVettore = null;
        //rapporto.AltroFluidoTermoVettore = null;
        //rapporto.IDTipologiaCogeneratore = null;
    }

    #endregion

    protected void customValidatorRaccomandazioniPrescrizioni(Object sender, ServerValidateEventArgs e)
    {
        string message = "Attenzione:<br/><br/>";

        string iDTipologiaControllo = rblTipologiaControllo.SelectedItem.Value;

        #region Controlli Raccomadanzioni/Prescrizioni
        bool fUnitaImmbiliare = UtilityRapportiControllo.GetfUnitaImmobiliare(int.Parse(IDRapportoControlloTecnico));
        bool c01 = true;
        if ((rblDichiarazioneConformita.SelectedIndex != 0) && (txtRaccomandazioni.Text == string.Empty))
        {
            c01 = false;
            message += "Sezione B - Dichiarazione di conformità assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        }

        bool c02 = true;
        if ((rblUsoManutenzioneGeneratore.SelectedIndex != 0) && (txtRaccomandazioni.Text == string.Empty))
        {
            c02 = false;
            message += "Sezione B - Libretti uso/manutenzione generatore assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        }

        bool c03 = true;
        if ((rblLibrettoImpiantoCompilato.SelectedIndex != 0) && (txtRaccomandazioni.Text == string.Empty))
        {
            c03 = false;
            message += "Sezione B - Libretto impianto non compilato in tutte le sue parti: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        }

        bool c04 = true;
        if ((rblTrattamentoRiscaldamento.SelectedIndex == 1) && (txtRaccomandazioni.Text == string.Empty))
        {
            c04 = false;
            message += "Sezione C - Trattamento in riscaldamento assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        }

        bool c05 = true;
        if ((rblTrattamentoAcs.SelectedIndex == 1) && (txtRaccomandazioni.Text == string.Empty))
        {
            c05 = false;
            message += "Sezione C - Trattamento Acs assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' <br /><br />";
        }

        bool c06 = true;
        int LocaleInstallazioneIdoneo = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkLocaleInstallazioneIdoneo")).Value;
        if ((LocaleInstallazioneIdoneo == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
        {
            c06 = false;
            if (IDTipologiaRct == "1")
            {
                message += "Sezione D - Installazione interna in locale non idoneo: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
            }
            else if ((IDTipologiaRct == "2") || (IDTipologiaRct == "3") || (IDTipologiaRct == "4"))
            {
                message += "Sezione D - Locale di installazione non idoneo: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c06bis = true;
        if (IDTipologiaRct == "4")
        {
            int CapsulaInsonorizzazioneIdonea = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkCapsulaInsonorizzazioneIdonea")).Value;
            if ((CapsulaInsonorizzazioneIdonea == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c06bis = false;
                message += "Sezione D - Capsula insonorizzante non idonea (esame visivo): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c07 = true;
        if (IDTipologiaRct == "1")
        {
            int GeneratoriIdonei = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkGeneratoriIdonei")).Value;
            if ((GeneratoriIdonei == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c07 = false;
                message += "Sezione D - Installazione esterna generatori non idonei: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
            }
        }
        
        bool c07bis = true;
        if (IDTipologiaRct == "4")
        {
            int TenutaCircuitoOlioIdonea = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTenutaCircuitoOlioIdonea")).Value;
            if ((TenutaCircuitoOlioIdonea == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c07bis = false;
                message += "Sezione D - Tenuta circuito olio non idonea: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c08 = true;
        if ((IDTipologiaRct == "1") || (IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
        {
            int DimensioniApertureAdeguate = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDimensioniApertureAdeguate")).Value;
            if ((DimensioniApertureAdeguate == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c08 = false;
                if (IDTipologiaRct == "1")
                {
                    message += "Sezione D - Non Adeguate dimensioni aperture di ventilazione/aerazione: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
                }
                else if ((IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
                {
                    message += "Sezione D - Non Adeguate dimensioni aperture di ventilazione/aerazione: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
                }
            }
        }
        
        bool c08bis = true;
        if (IDTipologiaRct == "4")
        {
            int FunzionalitaScambiatoreSeparazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkFunzionalitaScambiatoreSeparazione")).Value;
            if ((FunzionalitaScambiatoreSeparazione == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c08bis = false;
                message += "Sezione D - Funzionalità dello scambiatore di calore di separazione fra unità cogenerativa e impianto edificio (se presente) non idonea: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c09 = true;
        if ((IDTipologiaRct == "1") || (IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
        {
            int ApertureLibere = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkApertureLibere")).Value;
            if ((ApertureLibere == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c09 = false;
                if (IDTipologiaRct == "1")
                {
                    message += "Sezione D - Aperture di ventilazione/aerazione non libere da ostruzioni: è necessario indicare una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
                }
                else if ((IDTipologiaRct == "2") || (IDTipologiaRct == "4"))
                {
                    message += "Sezione D - Aperture di ventilazione/aerazione non libere da ostruzioni: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
                }
            }
        }

        bool c10 = true;
        if ((IDTipologiaRct == "1") || (IDTipologiaRct == "4"))
        {
            int ScarichiIdonei = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScarichiIdonei")).Value;
            if ((ScarichiIdonei == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c10 = false;
                if (IDTipologiaRct == "1")
                {
                    message += "Sezione D - Canale da fumo o condotti di scarico non idonei (esame visivo): è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
                }
                else if (IDTipologiaRct == "4")
                {
                    message += "Sezione D - Canale da fumo o condotti di scarico non idonei (esame visivo): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
                }
            }
        }

        bool c11 = true;
        if (IDTipologiaRct == "1")
        {
            int RegolazioneTemperaturaAmbiente = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkRegolazioneTemperaturaAmbiente")).Value;
            if ((RegolazioneTemperaturaAmbiente == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c11 = false;
                message += "Sezione D - Sistema di regolazione temperatura ambiente non funzionante: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
            }
        }

        bool c12 = true;
        if (IDTipologiaRct == "1")
        {
            int AssenzaPerditeCombustibile = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeCombustibile")).Value;
            if ((AssenzaPerditeCombustibile == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c12 = false;
                message += "Sezione D - Presenza di perdite di combustibile liquido: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
            }
        }

        bool c13 = true;
        if ((IDTipologiaRct == "1") || (IDTipologiaRct == "3") || (IDTipologiaRct == "4"))
        {
            int TenutaImpiantoIdraulico = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTenutaImpiantoIdraulico")).Value;
            if ((TenutaImpiantoIdraulico == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c13 = false;
                if (IDTipologiaRct == "1")
                {
                    message += "Sezione D - Non idonea tenuta dell'impianto interno e raccordi con il generatore: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
                }
                else if (IDTipologiaRct == "3")
                {
                    message += "Sezione D - Presenza di perdite circuito idraulico: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
                }
                else if (IDTipologiaRct == "4")
                {
                    message += "Sezione D - Tenuta circuito alimentazione non idoena: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
                }
            }
        }

        bool c13bis = true;
        if ((IDTipologiaRct == "2") || (IDTipologiaRct == "3") || (IDTipologiaRct == "4"))
        {
            int LineeElettricheIdonee = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkLineeElettricheIdonee")).Value;
            if ((LineeElettricheIdonee == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c13bis = false;
                message += "Sezione D - Linee elettriche non idonee: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }
        
        bool c14 = true;
        if (IDTipologiaRct == "1")
        {
            int DispositiviComandoRegolazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviComandoRegolazione")).Value;
            if ((DispositiviComandoRegolazione == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c14 = false;
                message += "Sezione E - Dispositivi di comando e regolazione non funzionanti correttamente: è necessario indicare una Raccomandazione o una Prescrizione selezionandola dall'elenco visualizzato o inserendo manualmente negli appositi campi 'Raccomadanzioni' o 'Prescrizioni'<br /><br />";
            }
        }

        bool c14bis = true;
        if ((IDTipologiaRct == "2") || (IDTipologiaRct == "3"))
        {
            int CoibentazioniIdonee = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkCoibentazioniIdonee")).Value;
            if ((CoibentazioniIdonee == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c14bis = false;
                message += "Sezione D - Coibentazioni idonee non idonee: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c15 = true;
        if (IDTipologiaRct == "1")
        {
            int DispositiviSicurezza = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviSicurezza")).Value;
            if ((DispositiviSicurezza == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c15 = false;
                message += "Sezione E - Dispositivi di sicurezza manomessi e/o cortocircuitati: è necessario indicare una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Prescrizione'<br /><br />";
            }
        }

        bool c15bis = true;
        if (IDTipologiaRct == "2")
        {
            int AssenzaPerditeRefrigerante = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeRefrigerante")).Value;
            if ((AssenzaPerditeRefrigerante == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c15bis = false;
                message += "Sezione E - Presenza di perdite di gas refrigerante: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c16 = true;
        if (IDTipologiaRct == "1")
        {
            int ValvolaSicurezzaSovrappressione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkValvolaSicurezzaSovrappressione")).Value;
            if ((ValvolaSicurezzaSovrappressione == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c16 = false;
                message += "Sezione E - Valvola di sicurezza alla sovrappressione a scarico libero assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
            }
        }

        bool c16bis = true;
        if (IDTipologiaRct == "2")
        {
            int FiltriPuliti = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkFiltriPuliti")).Value;
            if ((FiltriPuliti == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c16bis = false;
                message += "Sezione E - Filtri non puliti: è necessario indicare una Raccomandazione o una Prescrzione<br /><br />";
            }
        }

        bool c17 = true;
        if (IDTipologiaRct == "1")
        {
            int ScambiatoreFumiPulito = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoreFumiPulito")).Value;
            if ((ScambiatoreFumiPulito == 0) && (txtRaccomandazioni.Text == string.Empty))
            {
                c17 = false;
                message += "Sezione E - Non controllato e pulito lo scambiatore lato fumi: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
            }
        }

        bool c17bis = true;
        if (IDTipologiaRct == "2")
        {
            int LeakDetector = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkLeakDetector")).Value;
            if ((LeakDetector == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c17bis = false;
                message += "Sezione E - Assenza di apparecchiatura automatica rilevazione diretta fughe refrigerante (leak detector): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c18 = true;
        if (IDTipologiaRct == "1")
        {
            int RiflussoProdottiCombustione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkRiflussoProdottiCombustione")).Value;
            if ((RiflussoProdottiCombustione == 1) && (txtPrescrizioni.Text == string.Empty))
            {
                c18 = false;
                message += "Sezione E - Presenza riflusso dei prodotti della combustione: è necessario indicare una Prescrizione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Prescrizione'<br /><br />";
            }
        }

        bool c18bis = true;
        if (IDTipologiaRct == "2")
        {
            int ScambiatoriLiberi = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoriLiberi")).Value;
            if ((ScambiatoriLiberi == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c18bis = false;
                message += "Sezione E - Scambiatori di calore non puliti e non liberi da incrostazioni: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c18bisbis = true;
        if (IDTipologiaRct == "3")
        {
            int PotenzaCompatibileProgetto = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkPotenzaCompatibileProgetto")).Value;
            if ((PotenzaCompatibileProgetto == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c18bisbis = false;
                message += "Sezione E - Potenza non compatibile con i dati di progetto: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }
                
        bool c19 = true;
        int Contabilizzazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkContabilizzazione")).Value;
        if ((Contabilizzazione == 0) && (!fUnitaImmbiliare) && (txtRaccomandazioni.Text == string.Empty))
        {
            c19 = false;
            message += "Sezione G - Contabilizzazione assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        }

        bool c19bis = true;
        if (IDTipologiaRct == "2")
        {
            int ParametriTermodinamici = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkParametriTermodinamici")).Value;
            if ((ParametriTermodinamici == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c19bis = false;
                message += "Sezione E - Assenza di apparecchiatura automatica rilevazione indiretta fughe refrigerante (parametri termodinamici): è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }

        bool c19bisbis = true;
        if (IDTipologiaRct == "3")
        {
            int AssenzaTrafilamenti = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaTrafilamenti")).Value;
            if ((AssenzaTrafilamenti == 0) && (txtRaccomandazioni.Text == string.Empty && txtPrescrizioni.Text == string.Empty))
            {
                c19bisbis = false;
                message += "Sezione E - Dispositivi di regolazione e controllo non funzionanti e presenza di trafilamenti sulla valvola di regolazione: è necessario indicare una Raccomandazione o una Prescrizione<br /><br />";
            }
        }
                
        bool c20 = true;
        int Termoregolazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTermoregolazione")).Value;
        if ((Termoregolazione == 0) && (!fUnitaImmbiliare) && (txtRaccomandazioni.Text == string.Empty))
        {
            c20 = false;
            message += "Sezione G - Termoregolazione assente: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        }

        bool c21 = true;
        int CorrettoFunzionamentoContabilizzazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkCorrettoFunzionamentoContabilizzazione")).Value;
        if ((Termoregolazione == 0) && (!fUnitaImmbiliare) && (txtRaccomandazioni.Text == string.Empty))
        {
            c21 = false;
            message += "Sezione G - Non corretto funzionamento dei sistemi di contabilizzazione e termoregolazione: è necessario indicare una Raccomandazione selezionandola dall'elenco visualizzato o inserendola manualmente nell'apposito campo 'Raccomadanzioni'<br /><br />";
        }

        #endregion

        #region Controlli Campi
        decimal DurezzaAcqua;
        try
        {
            DurezzaAcqua = decimal.Parse(txtDurezzaAcqua.Text);
        }
        catch (Exception)
        {
            DurezzaAcqua = -1;
        }
        
        RCT_UC_UCVerificaEnergeticaGT RCT_UC_UCVerificaEnergeticaGT = ((RCT_UC_UCVerificaEnergeticaGT)MainFormView.FindControl("UCVerificaEnergeticaGT"));
        decimal TemperaturaFumi;
        try
        {
            TemperaturaFumi = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtTemperaturaFumi")).Text);
        }
        catch (Exception)
        {
            TemperaturaFumi = -1;
        }

        decimal TemperaturaComburente;
        try
        {
            TemperaturaComburente = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtTemperaturaComburente")).Text);
        }
        catch (Exception)
        {
            TemperaturaComburente = -1;
        }

        decimal O2;
        try
        {
            O2 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtO2")).Text);
        }
        catch (Exception)
        {
            O2 = -1;
        }

        decimal CO2;
        try
        {
            CO2 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCO2")).Text);
        }
        catch (Exception)
        {
            CO2 = -1;
        }

        decimal CoFumiSecchi;
        try
        {
            CoFumiSecchi = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCoFumiSecchi")).Text);
        }
        catch (Exception)
        {
            CoFumiSecchi = -1;
        }

        decimal CoCorretto;
        try
        {
            CoCorretto = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtCoCorretto")).Text);
        }
        catch (Exception)
        {
            CoCorretto = -1;
        }

        decimal PotenzaTermicaEffettiva;
        try
        {
            PotenzaTermicaEffettiva = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtPotenzaTermicaEffettiva")).Text);
        }
        catch (Exception)
        {
            PotenzaTermicaEffettiva = -1;
        }

        decimal RendimentoCombustione;
        try
        {
            RendimentoCombustione = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtRendimentoCombustione")).Text);
        }
        catch (Exception)
        {
            RendimentoCombustione = -1;
        }
        
        decimal RendimentoMinimo;
        try
        {
            RendimentoMinimo = decimal.Parse(((Label)RCT_UC_UCVerificaEnergeticaGT.FindControl("lblRendimentoMinimo")).Text);
        }
        catch (Exception)
        {
            RendimentoMinimo = -1;
        }

        decimal Bacharach1;
        try
        {
            Bacharach1 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach1")).Text);
        }
        catch (Exception)
        {
            Bacharach1 = -1;
        }

        decimal Bacharach2;
        try
        {
            Bacharach2 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach2")).Text);
        }
        catch (Exception)
        {
            Bacharach2 = -1;
        }

        decimal Bacharach3;
        try
        {
            Bacharach3 = decimal.Parse(((TextBox)RCT_UC_UCVerificaEnergeticaGT.FindControl("txtBacharach3")).Text);
        }
        catch (Exception)
        {
            Bacharach3 = -1;
        }


        decimal PotenzaTermicaNominaleFocolare;
        try
        {
            PotenzaTermicaNominaleFocolare = decimal.Parse(lblPotenzaTermicaNominaleFocolare.Text);
        }
        catch (Exception)
        {
            PotenzaTermicaNominaleFocolare = -1;
        }

        bool Val01 = true;
        bool Val02 = true;
        bool Val03 = true;
        bool Val04 = true;
        bool Val05 = true;
        bool Val06 = true;
        bool Val07 = true;
        bool Val08 = true;
        bool Val09 = true;
        bool Val10 = true;
        bool Val10bis = true;
        bool Val11 = true;
        bool Val12 = true;
        bool Val13 = true;
        bool Val14 = true;
        bool Val15 = true;

        if (IDTipologiaRct == "1")
        {
            if (DurezzaAcqua != -1)
            {
                if (!((DurezzaAcqua >= 0) && (DurezzaAcqua <= 100.00m)))
                {
                    Val01 = false;
                    message += "Sezione C – Durezza totale dell'acqua: range valori ammessi 0 <= 100 °F (gradi francesi)<br /><br />";
                }
            }

            if (TemperaturaFumi != -1)
            {
                if (!((TemperaturaFumi >= 0) && (TemperaturaFumi <= 250.00m)))
                {
                    Val02 = false;
                    message += "Sezione E – Temperatura fumi: range valori ammessi 0 <= 250 °C<br /><br />";
                }
            }

            if (TemperaturaComburente != -1)
            {
                if (!((TemperaturaComburente >= 0) && (TemperaturaComburente <= 200.00m)))
                {
                    Val03 = false;
                    message += "Sezione E – Temperatura aria comburente: range valori ammessi 0 <= 200 °C<br /><br />";
                }
            }

            if (O2 != -1)
            {
                if (!((O2 >= 0) && (O2 <= 20.9m)))
                {
                    Val04 = false;
                    message += "Sezione E – O2: range valori ammessi 0 <= 20,9 %<br /><br />";
                }
            }

            if ((CO2 != -1) && (lblIDTipologiaCombustibile.Text !="1"))
            {
                switch (lblIDTipologiaCombustibile.Text)
                {
                    case "2": //Gas naturale
                        if (!((CO2 >= 0) && (CO2 <= 12.00m)))
                        {
                            Val05 = false;
                            message += "Sezione E – CO2: range valori ammessi per il combustibile "+ lblTipologiaCombustibile.Text + " 0 <= 12 %<br /><br />";
                        }
                        break;
                    case "3": //Gpl
                        if (!((CO2 >= 0) && (CO2 <= 13.90m)))
                        {
                            Val05 = false;
                            message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 13,9 %<br /><br />";
                        }
                        break;
                    case "4": //Gasolio
                        if (!((CO2 >= 0) && (CO2 <= 15.10m)))
                        {
                            Val05 = false;
                            message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 15,1 %<br /><br />";
                        }
                        break;
                    case "5": //Olio combustibile
                        if (!((CO2 >= 0) && (CO2 <= 15.70m)))
                        {
                            Val05 = false;
                            message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 15,7 %<br /><br />";
                        }
                        break;
                    case "6": //Pellet
                    case "7": //Legna
                    case "8": //Cippato
                    case "9": //Bricchette
                        if (!((CO2 >= 0) && (CO2 <= 20.10m)))
                        {
                            Val05 = false;
                            message += "Sezione E – CO2: range valori ammessi per il combustibile " + lblTipologiaCombustibile.Text + " 0 <= 20,1 %<br /><br />";
                        }
                        break;
                }
            }

            if ((CoCorretto != -1) && (RendimentoCombustione !=-1) && (RendimentoMinimo!=-1) && (lblIDTipologiaCombustibile.Text != "1"))
            {
                //TODO: da controllare
                //int conformitaUni10389 = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkConformitàUNI10389")).Value;
                //switch (lblIDTipologiaCombustibile.Text)
                //{
                //    case "2": //Gas naturale
                //    case "3": //Gpl
                //        if (conformitaUni10389 == 1 && ((CoCorretto > 1000) || (RendimentoCombustione < RendimentoMinimo)))
                //        {
                //            Val06 = false;
                //            message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul No solo se non vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br /><br />";
                //        }
                //        else if (conformitaUni10389 == 0 && ((CoCorretto <= 1000) || (RendimentoCombustione >= RendimentoMinimo)))
                //        {
                //            Val06 = false;
                //            message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul Si solo se vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br /><br />";
                //        }
                //        break;
                //    case "4": //Gasolio
                //        var fCheckbacharach2 = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 2);
                //        if (conformitaUni10389 == 1)
                //        {
                //            if ((CoCorretto > 1000) || (RendimentoCombustione < RendimentoMinimo) || (!fCheckbacharach2))
                //            {
                //                Val06 = false;
                //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul No solo se non vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori del campo Bacharach devono essere ≤ 2<br /><br />";
                //            }
                //        }
                //        else if (conformitaUni10389 == 0)
                //        {
                //            if ((CoCorretto <= 1000) || (RendimentoCombustione >= RendimentoMinimo) || (fCheckbacharach2))
                //            {
                //                Val06 = false;
                //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul Si solo se vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori del campo Bacharach devono essere ≤ 2<br /><br />";
                //            }
                //        }
                //        break;
                //    case "5": //Olio combustibile
                //        var fCheckbacharach6 = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 6);
                //        if (conformitaUni10389 == 1)
                //        {
                //            if ((CoCorretto > 1000) || (RendimentoCombustione < RendimentoMinimo) || (!fCheckbacharach6))
                //            {
                //                Val06 = false;
                //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul No solo se non vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori di Bacharach devono essere ≤ 6<br /><br />";
                //            }
                //        }
                //        else if (conformitaUni10389 == 0)
                //        {
                //            if ((CoCorretto <= 1000) || (RendimentoCombustione >= RendimentoMinimo) || (fCheckbacharach6))
                //            {
                //                Val06 = false;
                //                message += "Sezione E – Risultati controllo, secondo UNI 10389-1, conformi alla legge per il combustibile " + lblTipologiaCombustibile.Text.ToLower() + ": il campo deve essere impostato sul Si solo se vengono soddisfatte le seguenti condizioni:<br/>1) il campo CO corretto deve risultare ≤ 1000<br/>2) il Rendimento combustione deve risultare >= del Rendimento minimo di legge<br/>3) almeno due dei valori di Bacharach devono essere ≤ 6<br /><br />";
                //            }
                //        }
                //        break;
                //}
            }
            
            if ((CoCorretto != -1) && (CoFumiSecchi != -1))
            {
                if (!(CoCorretto >= CoFumiSecchi))
                {
                    Val07 = false;
                    message += "Sezione E – CO corretto (ppm): il valore inserito non può essere minore del valore di CO fumi secchi<br /><br />";
                }
            }
            
            if ((PotenzaTermicaEffettiva != -1) && (PotenzaTermicaNominaleFocolare != -1))
            {
                decimal PotenzaTermicaNominaleFocolare50 = (PotenzaTermicaNominaleFocolare * 0.5m);
                decimal PotenzaTermicaNominaleFocolare120 = (PotenzaTermicaNominaleFocolare * 1.20m);

                if (!((PotenzaTermicaEffettiva > PotenzaTermicaNominaleFocolare50) && (PotenzaTermicaEffettiva < PotenzaTermicaNominaleFocolare120)))
                {
                    Val08 = false;
                    message += "Sezione E – La Potenza termica effettiva deve avere un range di valori ammessi tra il 50% della Potenza termica max al focolare e il 120% della Potenza termica max al focolare<br /><br />";
                }
            }
            
            if (RendimentoCombustione != -1)
            {
                if (!((RendimentoCombustione >= 0) && (RendimentoCombustione <= 150.00m)))
                {
                    Val09 = false;
                    message += "Sezione E – Rendimento di combustione: range valori ammessi 0 <= 150<br /><br />";
                }
            }
            
            if ((Bacharach1 != -1) && (Bacharach2 != -1) && (Bacharach3 != -1))
            {
                var result = false;
                RCT_UC_Checkbox chkRispettaIndiceBacharach = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkRispettaIndiceBacharach"));
                bool fRispettaIndiceBacharach = UtilityApp.ToBoolean(chkRispettaIndiceBacharach.Value.ToString());

                switch (lblIDTipologiaCombustibile.Text)
                {
                    case "4": //Gasolio
                        result = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 2);
                        if ((!result) && (fRispettaIndiceBacharach))
                        {
                            Val13 = false;
                            message += "Sezione E – Rispetta l'indice di Bacharach: il campo deve essere impostato sul Si solo se due dei tre valori di Bacharach inseriti sono ≤ 2  per il combustibile " + lblTipologiaCombustibile.Text + "<br /><br/>";
                        }
                        break;
                    case "5": //Olio combustibile
                        result = UtilityApp.AreNumbersTheSame(new[] { Bacharach1, Bacharach2, Bacharach3 }, 2, 6);
                        if ((!result) && (fRispettaIndiceBacharach))
                        {
                            Val13 = false;
                            message += "Sezione E – Rispetta l'indice di Bacharach: il campo deve essere impostato sul Si solo se due dei tre valori di Bacharach inseriti sono ≤ 6  per il combustibile " + lblTipologiaCombustibile.Text + "<br /><br/>";
                        }
                        break;
                }
            }

            if ((CoCorretto != -1))
            {
                RCT_UC_Checkbox chkCOFumiSecchiNoAria1000 = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkCOFumiSecchiNoAria1000"));
                bool fCOFumiSecchiNoAria1000 = UtilityApp.ToBoolean(chkCOFumiSecchiNoAria1000.Value.ToString());
                if (!(CoCorretto <= 1000) && (fCOFumiSecchiNoAria1000))
                {
                    Val14 = false;
                    message += "Sezione E – CO corretto <= 1.000 ppm v/v: in base al valore di CO corretto (ppm) il campo deve essere impostato su No<br /><br />";
                }
                else if (!(CoCorretto >= 1000) && (!fCOFumiSecchiNoAria1000))
                {
                    Val14 = false;
                    message += "Sezione E – CO corretto <= 1.000 ppm v/v: in base al valore di CO corretto (ppm) il campo deve essere impostato su Si<br /><br />";
                }
            }

            if ((RendimentoCombustione != -1) && (RendimentoMinimo != -1))
            {
                RCT_UC_Checkbox chkRendimentoSupMinimo = ((RCT_UC_Checkbox)RCT_UC_UCVerificaEnergeticaGT.FindControl("chkRendimentoSupMinimo"));
                bool fRendimentoSupMinimo = UtilityApp.ToBoolean(chkRendimentoSupMinimo.Value.ToString());
                if (!(RendimentoCombustione >= RendimentoMinimo) && (fRendimentoSupMinimo))
                {
                    Val15 = false;
                    message += "Sezione E – Rendimento >= rendimento minimo: in base al valore del rendimento minimo il campo deve essere impostato su No<br /><br />";
                }
                else if (!(RendimentoCombustione <= RendimentoMinimo) && (!fRendimentoSupMinimo))
                {
                    Val15 = false;
                    message += "Sezione E – Rendimento >= rendimento minimo: in base al valore del rendimento minimo il campo deve essere impostato su Si<br /><br />";
                }
            }

            if (!string.IsNullOrEmpty(txtPrescrizioni.Text) && (bool.Parse(rblImpiantoFunzionante.SelectedItem.Value)))
            {
                Val12 = false;
                message += "In presenza di una prescrizione l’impianto non può funzionare. E’ necessario pertanto selezionare l’opzione “No” del relativo campo<br /><br />";
            }
        }

        if ((txtDataManutenzioneConsigliata.Text != "") && (txtDataControllo.Text != ""))
        {
            DateTime dtManutenzione = DateTime.Parse(txtDataManutenzioneConsigliata.Text);
            DateTime dtControllo = DateTime.Parse(txtDataControllo.Text);
            if (!(dtManutenzione > dtControllo))
            {
                Val10 = false;
                message += "La data intervento manutentivo raccomandata deve essere maggiore della data del presente controllo<br /><br />";
            }
            if (!(dtControllo <= DateTime.Now))
            {
                Val10bis = false;
                message += "La data del presente controllo non deve essere maggiore della data odierna<br /><br />";
            }
        }

        if ((txtOraArrivo.DateTime != null) && (txtOraPartenza.DateTime != null))
        {
            DateTime dtOraArrivo = txtOraArrivo.DateTime;
            DateTime dtOraPartenza = txtOraPartenza.DateTime;
            if (!(dtOraPartenza > dtOraArrivo))
            {
                Val11 = false;
                message += "L'orario di arrivo presso l'impianto deve essere inferiore all'orario di partenza presso l'impianto<br /><br />";
            }
        }
        #endregion

        #region Controlli NC
        bool Nc01 = true;
        int? fScarichiIdonei = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkScarichiIdonei")).Value;
        if (fScarichiIdonei != null)
        {
            if ((fScarichiIdonei == -1) && (IDTipologiaRct == "1"))
            {
                Nc01 = false;
                message += "Sezione D - Canale da fumo o condotti di scarico idonei (esame visivo): l'opzione Nc non può essere selezionata<br /><br />";
            }
        }

        bool Nc02 = true;
        int? fAssenzaPerditeCombustibile = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeCombustibile")).Value;
        if (fAssenzaPerditeCombustibile != null)
        {
            if ((fAssenzaPerditeCombustibile == -1) && (IDTipologiaRct=="1") && ((lblIDTipologiaCombustibile.Text =="4") || (lblIDTipologiaCombustibile.Text == "5")))
            {
                Nc02 = false;
                message += "Sezione D - Assenza di perdite di combustibile liquido: l'opzione Nc non può essere selezionata<br /><br />";
            }
        }

        bool Nc03 = true;
        int? fDispositiviComandoRegolazione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviComandoRegolazione")).Value;
        if (fDispositiviComandoRegolazione != null)
        {
            if ((fDispositiviComandoRegolazione == -1) && (IDTipologiaRct == "1"))
            {
                Nc03 = false;
                message += "Sezione E - Dispositivi di comando e regolazione funzionanti correttamente: l'opzione Nc non può essere selezionata<br /><br />";
            }
        }

        bool Nc04 = true;
        int? fDispositiviSicurezza = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviSicurezza")).Value;
        if (fDispositiviSicurezza != null)
        {
            if ((fDispositiviSicurezza == -1) && (IDTipologiaRct == "1"))
            {
                Nc04 = false;
                message += "Sezione E - Dispositivi di sicurezza non manomessi e/o cortocircuitati: l'opzione Nc non può essere selezionata<br /><br />";
            }
        }

        bool Nc05 = true;
        //int? fValvolaSicurezzaSovrappressione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkValvolaSicurezzaSovrappressione")).Value;
        //if (fValvolaSicurezzaSovrappressione != null)
        //{
        //    if ((fValvolaSicurezzaSovrappressione == -1) && (IDTipologiaRct == "1"))
        //    {
        //        Nc05 = false;
        //        message += "Sezione E - Valvola di sicurezza alla sovrappressione a scarico libero: l'opzione Nc non può essere selezionata<br /><br />";
        //    }
        //}

        bool Nc06 = true;
        int? fScambiatoreFumiPulito = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoreFumiPulito")).Value;
        if (fScambiatoreFumiPulito != null)
        {
            if ((fScambiatoreFumiPulito == -1) && (IDTipologiaRct == "1"))
            {
                Nc06 = false;
                message += "Sezione E - Controllato e pulito lo scambiatore lato fumi: l'opzione Nc non può essere selezionata<br /><br />";
            }
        }

        bool Nc07 = true;
        int? fRiflussoProdottiCombustione = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkRiflussoProdottiCombustione")).Value;
        if (fRiflussoProdottiCombustione != null)
        {
            if ((fRiflussoProdottiCombustione == -1) && (IDTipologiaRct == "1"))
            {
                Nc07 = false;
                message += "Sezione E - Presenza riflusso dei prodotti della combustione: l'opzione Nc non può essere selezionata<br /><br />";
            }
        }

        bool Nc08 = true;
        int? fConformitàUNI10389 = (int?)((RCT_UC_Checkbox)MainFormView.FindControl("chkConformitàUNI10389")).Value;
        if (fConformitàUNI10389 != null)
        {
            if ((fConformitàUNI10389 == -1) && (iDTipologiaControllo=="1") && ((lblIDTipologiaCombustibile.Text =="2") || (lblIDTipologiaCombustibile.Text == "3") || (lblIDTipologiaCombustibile.Text == "4") || (lblIDTipologiaCombustibile.Text == "5")))
            {
                Nc08 = false;
                message += "Sezione E - Risultati controllo, secondo UNI 10389-1, conformi alla legge: l'opzione Nc non può essere selezionata<br /><br />";
            }
        }
        
        #endregion

        if (c01 && c02 && c03 && c04 && c05 && c06 && c06bis && c07 && c07bis && c08 && c08bis && c09 && c10 && c11 && c12 && c13 && c13bis && c14 && c14bis && c15 && c15bis && c16 && c16bis && c17 && c17bis && c18 && c18bis && c18bisbis && c19 && c19bis && c19bisbis && c20 && c21
            & Val01 && Val02 && Val03 && Val04 && Val05 && Val06 && Val07 && Val08 && Val09 && Val10 && Val10bis && Val11 && Val12 && Val13 && Val14 && Val15
            & Nc01 && Nc02 && Nc03 && Nc04 && Nc05 && Nc06 && Nc07 && Nc08
            )
        {
            e.IsValid = true;
        }
        else
        {
            e.IsValid = false;
            cvRaccomandazioniPrescrizioni.ErrorMessage = message;
        }
    }
    
    protected void btnSaveRapportoDiControlloDefinitivo_Click(object sender, EventArgs e)
    {
        var btn = sender as Button;
        this.CausesValidation = btn != null && btn.CausesValidation;

        if (Page.IsValid)
        {
            UCBolliniSelector_Selezioneterminata(this, null);
        }
    }

    protected void btnAnnullaRapportoDiControlloInAttesaFirma_Click(object sender, EventArgs e)
    {
        UtilityRapportiControllo.AnnullaRapportoControlloInAttesaDiFirma(int.Parse(IDRapportoControlloTecnico));

        QueryString qs = new QueryString();
        qs.Add("IDRapportoControlloTecnico", IDRapportoControlloTecnico);
        qs.Add("IDTipologiaRCT", IDTipologiaRct);
        qs.Add("IDSoggetto", iDSoggetto);
        qs.Add("IDSoggettoDerived", iDSoggettoDerived);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "RCT_RapportoDiControlloTecnico.aspx";
        url += qsEncrypted.ToString();

        Response.Redirect(url);
    }

    protected void btnSaveRapportoDiControllo_OnClick(object sender, EventArgs e)
    {
        //Salvo ma con validazione
        var btn = sender as Button;
        this.CausesValidation = btn != null && btn.CausesValidation;

        if (Page.IsValid)
        {
            MainFormView.UpdateItem(true);
        }
    }

    protected void btnAnnullaRapportoControlloTecnico_Click(object sender, EventArgs e)
    {
        UtilityRapportiControllo.AnnullaRapportoControllo(int.Parse(IDRapportoControlloTecnico));

        QueryString qs = new QueryString();
        qs.Add("IDRapportoControlloTecnico", IDRapportoControlloTecnico);
        qs.Add("IDTipologiaRCT", IDTipologiaRct);
        qs.Add("IDSoggetto", iDSoggetto);
        qs.Add("IDSoggettoDerived", iDSoggettoDerived);
        QueryString qsEncrypted = Encryption.EncryptQueryString(qs);

        string url = "RCT_RapportoDiControlloTecnico.aspx";
        url += qsEncrypted.ToString();

        Response.Redirect(url);
    }

    private bool CausesValidation
    {
        get;
        set;
    }
    
    protected void rblDichiarazioneConformita_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (rblDichiarazioneConformita.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni0.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni0.Visible = true;
                }
                break;
            case 2:
                if (rblDichiarazioneConformita.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni1.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni1.Visible = true;
                }
                break;
            case 3:
                if (rblDichiarazioneConformita.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni2.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni2.Visible = true;
                }
                break;
            case 4:
                if (rblDichiarazioneConformita.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni3.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni3.Visible = true;
                }
                break;
        }
    }

    protected void rblUsoManutenzioneGeneratore_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (rblUsoManutenzioneGeneratore.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni4.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni4.Visible = true;
                }
                break;
            case 2:
                if (rblUsoManutenzioneGeneratore.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni5.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni5.Visible = true;
                }
                break;
            case 3:
                if (rblUsoManutenzioneGeneratore.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni6.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni6.Visible = true;
                }
                break;
            case 4:
                if (rblUsoManutenzioneGeneratore.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni7.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni7.Visible = true;
                }
                break;
        }
    }

    protected void rblLibrettoImpiantoCompilato_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (rblLibrettoImpiantoCompilato.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni8.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni8.Visible = true;
                }
                break;
            case 2:
                if (rblLibrettoImpiantoCompilato.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni9.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni9.Visible = true;
                }
                break;
            case 3:
                if (rblLibrettoImpiantoCompilato.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni10.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni10.Visible = true;
                }
                break;
            case 4:
                if (rblLibrettoImpiantoCompilato.SelectedIndex == 0)
                {
                    UCRaccomandazioniPrescrizioni11.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni11.Visible = true;
                }
                break;
        }
    }

    protected void rblTrattamentoRiscaldamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTrattamentoAcquaInvernale();

        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (rblTrattamentoRiscaldamento.SelectedIndex != 1)
                {
                    UCRaccomandazioniPrescrizioni12.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni12.Visible = true;
                }
                break;
            case 2:

                break;
            case 3:
                if (rblTrattamentoRiscaldamento.SelectedIndex != 1)
                {
                    UCRaccomandazioniPrescrizioni13.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni13.Visible = true;
                }
                break;
            case 4:
                if (rblTrattamentoRiscaldamento.SelectedIndex != 1)
                {
                    UCRaccomandazioniPrescrizioni14.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni14.Visible = true;
                }
                break;
        }
    }

    protected void VisibleTrattamentoAcquaInvernale()
    {
        if (rblTrattamentoRiscaldamento.SelectedValue == "1")
        {
            rowTrattamentoRiscaldamento.Visible = true;
        }
        else
        {
            rowTrattamentoRiscaldamento.Visible = false;
            cblTipologiaTrattamentoAcquaInvernale.ClearSelection();
        }
    }

    protected void VisibleTrattamentoAcquaAcs()
    {
        if (rblTrattamentoAcs.SelectedValue == "1")
        {
            rowTrattamentoACS.Visible = true;
        }
        else
        {
            rowTrattamentoACS.Visible = false;
            cblTipologiaTrattamentoAcquaAcs.ClearSelection();
        }
    }

    protected void rblTrattamentoAcs_SelectedIndexChanged(object sender, EventArgs e)
    {
        VisibleTrattamentoAcquaAcs();

        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (rblTrattamentoAcs.SelectedIndex != 1)
                {
                    UCRaccomandazioniPrescrizioni15.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni15.Visible = true;
                }
                break;
            case 2:

                break;
            case 3:
                if (rblTrattamentoAcs.SelectedIndex != 1)
                {
                    UCRaccomandazioniPrescrizioni16.Visible = false;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni16.Visible = true;
                }
                break;
            case 4:

                break;
        }
    }

    protected void chkLocaleInstallazioneIdoneo_CheckedChanged(object sender, EventArgs e)
    {
        RCT_UC_Checkbox chkLocaleInstallazioneIdoneo = (RCT_UC_Checkbox)MainFormView.FindControl("chkLocaleInstallazioneIdoneo");
        RCT_UC_Checkbox chkGeneratoriIdonei = (RCT_UC_Checkbox)MainFormView.FindControl("chkGeneratoriIdonei");
        EnabledDisabledInstallazione(chkLocaleInstallazioneIdoneo, chkGeneratoriIdonei);

        LocaleInstallazioneIdoneo();
        GeneratoriIdonei();
    }

    protected void LocaleInstallazioneIdoneo()
    {
        int LocaleInstallazioneIdoneo = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkLocaleInstallazioneIdoneo")).Value;
        if ((LocaleInstallazioneIdoneo == 0) && (IDTipologiaRct=="1"))
        {
            UCRaccomandazioniPrescrizioni17.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni17.Visible = false;
        }
    }

    protected void chkGeneratoriIdonei_CheckedChanged(object sender, EventArgs e)
    {
        RCT_UC_Checkbox chkLocaleInstallazioneIdoneo = (RCT_UC_Checkbox)MainFormView.FindControl("chkLocaleInstallazioneIdoneo");
        RCT_UC_Checkbox chkGeneratoriIdonei = (RCT_UC_Checkbox)MainFormView.FindControl("chkGeneratoriIdonei");
        EnabledDisabledInstallazione(chkLocaleInstallazioneIdoneo, chkGeneratoriIdonei);

        GeneratoriIdonei();
        LocaleInstallazioneIdoneo();
    }

    protected void GeneratoriIdonei()
    {
        int GeneratoriIdonei = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkGeneratoriIdonei")).Value;
        if ((GeneratoriIdonei == 0) && (IDTipologiaRct == "1"))
        {
            UCRaccomandazioniPrescrizioni18.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni18.Visible = false;
        }
    }

    protected void EnabledDisabledInstallazione(RCT_UC_Checkbox chkLocaleInstallazioneIdoneo, RCT_UC_Checkbox chkGeneratoriIdonei)
    {
        if (
            ((chkLocaleInstallazioneIdoneo.Value == EnumStatoSiNoNc.Si) || (chkLocaleInstallazioneIdoneo.Value == EnumStatoSiNoNc.No))
            &&
            ((chkGeneratoriIdonei.Value != EnumStatoSiNoNc.Si) || (chkGeneratoriIdonei.Value != EnumStatoSiNoNc.No))
            )
        {
            chkGeneratoriIdonei.Value = EnumStatoSiNoNc.NonClassificabile;
        }
        else if (
                ((chkGeneratoriIdonei.Value == EnumStatoSiNoNc.Si) || (chkGeneratoriIdonei.Value == EnumStatoSiNoNc.No))
                &&
                ((chkLocaleInstallazioneIdoneo.Value != EnumStatoSiNoNc.Si) || (chkLocaleInstallazioneIdoneo.Value != EnumStatoSiNoNc.No))
                )
        {
            chkLocaleInstallazioneIdoneo.Value = EnumStatoSiNoNc.NonClassificabile;
        }
    }

    protected void chkDimensioniApertureAdeguate_CheckedChanged(object sender, EventArgs e)
    {
        int DimensioniApertureAdeguate = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDimensioniApertureAdeguate")).Value;
        if ((DimensioniApertureAdeguate == 0) && (IDTipologiaRct=="1"))
        {
            UCRaccomandazioniPrescrizioni19.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni19.Visible = false;
        }
    }

    protected void chkApertureLibere_CheckedChanged(object sender, EventArgs e)
    {
        int ApertureLibere = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkApertureLibere")).Value;
        if ((ApertureLibere == 0) && (IDTipologiaRct=="1"))
        {
            UCRaccomandazioniPrescrizioni20.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni20.Visible = false;
        }
    }

    protected void chkScarichiIdonei_CheckedChanged(object sender, EventArgs e)
    {
        int ScarichiIdonei = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScarichiIdonei")).Value;
        if ((ScarichiIdonei == 0) && (IDTipologiaRct=="1"))
        {
            UCRaccomandazioniPrescrizioni21.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni21.Visible = false;
        }
    }

    protected void chkRegolazioneTemperaturaAmbiente_CheckedChanged(object sender, EventArgs e)
    {
        int RegolazioneTemperaturaAmbiente = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkRegolazioneTemperaturaAmbiente")).Value;
        if (RegolazioneTemperaturaAmbiente == 0)
        {
            UCRaccomandazioniPrescrizioni22.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni22.Visible = false;
        }
    }

    protected void chkAssenzaPerditeCombustibile_CheckedChanged(object sender, EventArgs e)
    {
        int AssenzaPerditeCombustibile = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkAssenzaPerditeCombustibile")).Value;
        if (AssenzaPerditeCombustibile == 0)
        {
            UCRaccomandazioniPrescrizioni23.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni23.Visible = false;
        }
    }

    protected void chkTenutaImpiantoIdraulico_CheckedChanged(object sender, EventArgs e)
    {
        int TenutaImpiantoIdraulico = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTenutaImpiantoIdraulico")).Value;
        if ((TenutaImpiantoIdraulico == 0) && (IDTipologiaRct=="1"))
        {
            UCRaccomandazioniPrescrizioni24.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni24.Visible = false;
        }
    }
        
    protected void txtDepressioneCanaleFumo_TextChanged(object sender, EventArgs e)
    {
        EvacuazioneFumi();
        MainFormView.UpdateItem(false);
    }

    protected void EvacuazioneFumi()
    {
        decimal DepressioneCanaleFumo;
        try
        {
            DepressioneCanaleFumo = decimal.Parse(txtDepressioneCanaleFumo.Text);
        }
        catch (Exception)
        {
            DepressioneCanaleFumo = -1;
        }

        if (DepressioneCanaleFumo != -1)
        {
            if (DepressioneCanaleFumo < 3)
            {
                UCRaccomandazioniPrescrizioni25.Visible = true;
            }
            else
            {
                UCRaccomandazioniPrescrizioni25.Visible = false;
            }
        }
    }

    protected void chkDispositiviComandoRegolazione_CheckedChanged(object sender, EventArgs e)
    {
        int DispositiviComandoRegolazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviComandoRegolazione")).Value;
        if (DispositiviComandoRegolazione == 0)
        {
            UCRaccomandazioniPrescrizioni26.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni26.Visible = false;
        }
    }

    protected void chkDispositiviSicurezza_CheckedChanged(object sender, EventArgs e)
    {
        int DispositiviSicurezza = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkDispositiviSicurezza")).Value;
        if (DispositiviSicurezza == 0)
        {
            UCRaccomandazioniPrescrizioni27.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni27.Visible = false;
        }
    }

    protected void chkValvolaSicurezzaSovrappressione_CheckedChanged(object sender, EventArgs e)
    {
        int ValvolaSicurezzaSovrappressione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkValvolaSicurezzaSovrappressione")).Value;
        if (ValvolaSicurezzaSovrappressione == 0)
        {
            UCRaccomandazioniPrescrizioni28.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni28.Visible = false;
        }
    }

    protected void chkScambiatoreFumiPulito_CheckedChanged(object sender, EventArgs e)
    {
        int ScambiatoreFumiPulito = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkScambiatoreFumiPulito")).Value;
        if (ScambiatoreFumiPulito == 0)
        {
            UCRaccomandazioniPrescrizioni29.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni29.Visible = false;
        }
    }

    protected void chkRiflussoProdottiCombustione_CheckedChanged(object sender, EventArgs e)
    {
        int RiflussoProdottiCombustione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkRiflussoProdottiCombustione")).Value;
        if (RiflussoProdottiCombustione == 1)
        {
            UCRaccomandazioniPrescrizioni30.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni30.Visible = false;
        }
    }

    protected void chkContabilizzazione_CheckedChanged(object sender, EventArgs e)
    {
        int Contabilizzazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkContabilizzazione")).Value;
        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (Contabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni31.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni31.Visible = false;
                }
                break;
            case 2:
                if (Contabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni32.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni32.Visible = false;
                }
                break;
            case 3:
                if (Contabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni33.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni33.Visible = false;
                }
                break;
            case 4:
                if (Contabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni34.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni34.Visible = false;
                }
                break;
        }
    }

    protected void chkTermoregolazione_CheckedChanged(object sender, EventArgs e)
    {
        int Termoregolazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkTermoregolazione")).Value;
        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (Termoregolazione == 0)
                {
                    UCRaccomandazioniPrescrizioni35.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni35.Visible = false;
                }
                break;
            case 2:
                if (Termoregolazione == 0)
                {
                    UCRaccomandazioniPrescrizioni36.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni36.Visible = false;
                }
                break;
            case 3:
                if (Termoregolazione == 0)
                {
                    UCRaccomandazioniPrescrizioni37.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni37.Visible = false;
                }
                break;
            case 4:
                if (Termoregolazione == 0)
                {
                    UCRaccomandazioniPrescrizioni38.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni38.Visible = false;
                }
                break;
        }
    }

    protected void chkCorrettoFunzionamentoContabilizzazione_CheckedChanged(object sender, EventArgs e)
    {
        int CorrettoFunzionamentoContabilizzazione = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkCorrettoFunzionamentoContabilizzazione")).Value;
        switch (int.Parse(IDTipologiaRct))
        {
            case 1:
                if (CorrettoFunzionamentoContabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni39.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni39.Visible = false;
                }
                break;
            case 2:
                if (CorrettoFunzionamentoContabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni40.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni40.Visible = false;
                }
                break;
            case 3:
                if (CorrettoFunzionamentoContabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni41.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni41.Visible = false;
                }
                break;
            case 4:
                if (CorrettoFunzionamentoContabilizzazione == 0)
                {
                    UCRaccomandazioniPrescrizioni42.Visible = true;
                }
                else
                {
                    UCRaccomandazioniPrescrizioni42.Visible = false;
                }
                break;
        }
    }
    
    protected void rblEvacuazioneNaturaleForzata_CheckedChanged(object sender, EventArgs e)
    {
        string IDTipoCombustibile = lblIDTipologiaCombustibile.Text;
        if ((IDTipologiaRct == "1") && (rblEvacuazioneNaturale.Checked) && ((IDTipoCombustibile == "2" || IDTipoCombustibile == "3")))
        {
            rowDepressioneCanaleFumo.Visible = true;
        }
        else
        {
            rowDepressioneCanaleFumo.Visible = false;
            txtDepressioneCanaleFumo.Text = string.Empty;
        }
        //if (rblEvacuazioneNaturale.Checked)
        //{
        //    rowDepressioneCanaleFumo.Visible = true;
        //}
        //else
        //{
        //    rowDepressioneCanaleFumo.Visible = false;
        //}
    }


    protected void chkConformitàUNI10389_CheckedChanged(object sender, EventArgs e)
    {
        int ConformitàUNI10389 = (int)((RCT_UC_Checkbox)MainFormView.FindControl("chkConformitàUNI10389")).Value;
        if (ConformitàUNI10389 == 0)
        {
            UCRaccomandazioniPrescrizioni47.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni47.Visible = false;
        }
    }
}