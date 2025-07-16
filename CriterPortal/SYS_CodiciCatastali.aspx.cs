using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using DataUtilityCore;

public partial class SYS_CodiciCatastali : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DdlProvincia();
        }
    }

    protected void GridViewCodiciCatastali_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == " Insert ")
        {
            TextBox txtCodiceCatastale = (TextBox)GridViewCodiciCatastali.FooterRow.FindControl("txtCodiceCatastale");
            TextBox txtComune = (TextBox)GridViewCodiciCatastali.FooterRow.FindControl("txtComune");
            DropDownList ddlProvincia = (DropDownList)GridViewCodiciCatastali.FooterRow.FindControl("ddlProvincia");
            CheckBox chkFattivo = (CheckBox)GridViewCodiciCatastali.FooterRow.FindControl("chkFattivo");
            SqlDataSourceCodiciCatastali.InsertParameters["CodiceCatastale"].DefaultValue = txtCodiceCatastale.Text;
            SqlDataSourceCodiciCatastali.InsertParameters["Comune"].DefaultValue = txtComune.Text;
            SqlDataSourceCodiciCatastali.InsertParameters["IDProvincia"].DefaultValue = ddlProvincia.SelectedValue;
            SqlDataSourceCodiciCatastali.InsertParameters["Fattivo"].DefaultValue = chkFattivo.Checked.ToString();
            SqlDataSourceCodiciCatastali.Insert();

        }


    }

    protected void DdlProvincia()
    {
        ddlProvincia.DataValueField = "IDProvincia";
        ddlProvincia.DataTextField = "Provincia";
        ddlProvincia.DataSource = LoadDropDownList.LoadDropDownList_SYS_Province(null, false);
        ddlProvincia.DataBind();

        ListItem myItem = new ListItem("-- Selezionare --", "0");
        ddlProvincia.Items.Insert(0, myItem);
        ddlProvincia.SelectedIndex = 0;
    }

    public void Nuovo_Click(object sender, EventArgs e)
    {
        tblInfoRicerca.Visible = false;
        tblInsertCodiceCatastale.Visible = true;
        GridViewCodiciCatastali.Visible = false;        
    }

    public void Annulla_Click(object sender, EventArgs e)
    {
        tblInfoRicerca.Visible = true;
        tblInsertCodiceCatastale.Visible = false;
        GridViewCodiciCatastali.Visible = true;
        
        txtCodiceCatastale.Text = "";
        txtComune.Text = "";
        ddlProvincia.SelectedIndex = 0;
        txtCapComune.Text = "";
        txtEmailPec.Text = "";
    }

    public void Insert_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            SqlConnection conn = BuildConnection.GetConn();
            SqlCommand myCommand = new SqlCommand(("INSERT INTO SYS_CodiciCatastali(CodiceCatastale, Comune, IDProvincia, EmailPec, Cap, fattivo) VALUES" + " (@CodiceCatastale, @Comune, @IDProvincia, @EmailPec, @Cap, @fattivo)"), conn);
            myCommand.Connection = conn;
            myCommand.Connection.Open();
            myCommand.Parameters.AddWithValue("@CodiceCatastale", this.txtCodiceCatastale.Text);
            myCommand.Parameters.AddWithValue("@Comune", this.txtComune.Text);
            myCommand.Parameters.AddWithValue("@IDProvincia", this.ddlProvincia.SelectedValue);
            myCommand.Parameters.AddWithValue("@EmailPec", this.txtEmailPec.Text);
            myCommand.Parameters.AddWithValue("@Cap", this.txtCapComune.Text);
            myCommand.Parameters.AddWithValue("@fattivo", this.chkFattivo.Checked);

            myCommand.ExecuteNonQuery();
            myCommand.Connection.Close();

            Response.Redirect("SYS_CodiciCatastali.aspx");
        }       
    }

}
