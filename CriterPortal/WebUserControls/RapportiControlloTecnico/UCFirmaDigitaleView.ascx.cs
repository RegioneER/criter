using System;
using DataUtilityCore;

public partial class UCFirmaDigitaleView : System.Web.UI.UserControl
{
    private string _IDRapportoControlloTecnico = string.Empty;
    public string IDRapportoControlloTecnico
    {
        get { return _IDRapportoControlloTecnico; }
        set { _IDRapportoControlloTecnico = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetDataFirmaDigitale(IDRapportoControlloTecnico);
        }
    }

    public void GetDataFirmaDigitale(string IDRapportoControlloTecnico)
    {
        var result = UtilityRapportiControllo.GetValoriFirmaDigitale(int.Parse(IDRapportoControlloTecnico));
        if (result.Count > 0)
        {
            tblInfoFirmaDigitale.Visible = true;
            string InfoFirma = "";
            foreach (var row in result)
            {
                InfoFirma += "Rapporto di controllo tecnico firmato digitalmente in data " + row.DataFirma + "<br/>";
                if (row.SignerFullName != null)
                {
                    InfoFirma += "<br/>Firmatario:&nbsp;" + row.SignerFullName;
                }
                if (row.SignerAuthority != null)
                {
                    InfoFirma += "<br/>Autorit&agrave; di rilascio dispositivo digitale:&nbsp;" + row.SignerAuthority;
                }
                if (row.SignerSerialNumber != null)
                {
                    InfoFirma += "<br/>Numero seriale dispositivo:&nbsp;" + row.SignerSerialNumber;
                }
                if (row.DataFirma != null)
                {
                    InfoFirma += "<br/>Data firma:&nbsp;" + row.DataFirma;
                }
                lblInfoFirma.Text = InfoFirma;
            }
        }
        else
        {
            tblInfoFirmaDigitale.Visible = false;
        }
    }
}