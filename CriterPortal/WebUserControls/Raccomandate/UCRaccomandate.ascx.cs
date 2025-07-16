using DataLayer;
using DataUtilityCore;
using DevExpress.Web;
using System;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControls_Sanzioni_UCRaccomandate : System.Web.UI.UserControl
{

    UserInfo info = SecurityManager.GetUserInfo(HttpContext.Current.User.Identity.Name, false);

    public string IDAccertamento
    {
        get { return lblIDAccertamento.Text; }
        set
        {
            lblIDAccertamento.Text = value;
        }
    }

    public string IDIspezione
    {
        get { return lblIDIspezione.Text; }
        set
        {
            lblIDIspezione.Text = value;
        }
    }

    public string IDRaccomandataType
    {
        get { return lblIDRaccomandataType.Text; }
        set
        {
            lblIDRaccomandataType.Text = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            GetDatiRaccomandate();
            PagePermission();
        }
        
    }

    protected void GetDatiRaccomandate()
    {
        switch (IDRaccomandataType)
        {
            case "1":
                //typeRaccomandata = "Accertamento";
               
                break;
            case "2":
                //typeRaccomandata = "Revoca accertamento";

                break;
            case "3":
                //typeRaccomandata = "Conferma pianificazione ispezione";

                break;
            case "4":
                //typeRaccomandata = "Annullamento pianificazione ispezione";

                break;
            case "5":
                //typeRaccomandata = "Ripianificazione ispezione";

                break;
            case "6":
                //typeRaccomandata = "Notifica Sanzione";
                                
                break;
            case "7":
                //typeRaccomandata = "Raccomandate libere";

                break;
        }


        using (var ctx = new CriterDataModel())
        {
            var raccomandate = ctx.V_COM_Raccomandate.AsQueryable();
            if (!string.IsNullOrEmpty(IDAccertamento))
            {
                long? iDAccertamento = long.Parse(IDAccertamento);
                raccomandate = raccomandate.Where(a => a.IDAccertamento == iDAccertamento);
            }
            else if (!string.IsNullOrEmpty(IDIspezione))
            {
                long? iDIspezione = long.Parse(IDIspezione);
                raccomandate = raccomandate.Where(a => a.IDIspezione == iDIspezione);
            }

            DataGrid.DataSource = raccomandate.OrderByDescending(r => r.CreatoIl).ToList();
            DataGrid.DataBind();
        }
    }

    protected void PagePermission()
    {
        string[] getVal = new string[4];
        getVal = SecurityManager.GetDatiPermission();

        switch (getVal[1])
        {
            case "1": //Amministratore Criter
            case "12": //Amministrazione
            case "9": //Segreteria Verifiche
                rowSaveNote.Visible = true;
                for (int i = 0; i < DataGrid.Items.Count; i++)
                {
                    TextBox txtNoteRaccomandata = (TextBox)DataGrid.Items[i].Cells[4].FindControl("txtNoteRaccomandata");
                    txtNoteRaccomandata.ReadOnly = false;
                }
                break;
            case "8": //Ispettore
                rowSaveNote.Visible = false;
                for (int i = 0; i < DataGrid.Items.Count; i++)
                {
                    TextBox txtNoteRaccomandata = (TextBox)DataGrid.Items[i].Cells[4].FindControl("txtNoteRaccomandata");
                    txtNoteRaccomandata.ReadOnly = true;
                }
                break;
            //case "14": //Coordinatore/Ispettore
            //    rowSaveNote.Visible = (DataGrid.Items.Count > 0) ? true : false;
            //    for (int i = 0; i < DataGrid.Items.Count; i++)
            //    {
            //        TextBox txtNoteRaccomandata = (TextBox)DataGrid.Items[i].Cells[4].FindControl("txtNoteRaccomandata");
            //        txtNoteRaccomandata.ReadOnly = false;
            //    }
            //    break;
        }
    }

    protected void btnSaveNote_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            for (int i = 0; i < DataGrid.Items.Count; i++)
            {
                TextBox txtNoteRaccomandata = (TextBox)DataGrid.Items[i].Cells[4].FindControl("txtNoteRaccomandata");
                var iDRaccomandata = DataGrid.Items[i].Cells[0].Text;

                UtilityPosteItaliane.SaveNoteRaccomandata(iDRaccomandata, txtNoteRaccomandata.Text);
            }

            GetDatiRaccomandate();
        }
    }


    protected void DataGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            ImageButton ImgPdf = (ImageButton)(e.Item.Cells[4].FindControl("ImgPdf"));
            ImgPdf.Attributes.Add("onclick", "OpenPopupWindowRaccomandate(this, '" + e.Item.Cells[0].Text + "'); return false;");
        }
    }
}