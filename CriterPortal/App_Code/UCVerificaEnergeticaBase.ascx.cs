using System.Web.UI.WebControls;

public abstract class RCT_UC_UCVerificaEnergeticaBase : System.Web.UI.UserControl
{
    public decimal? NullableDecimalGet(TextBox txtBox)
    {
        decimal x;

        if(decimal.TryParse(txtBox.Text, out x))
            return x;

        return null;
    }

    public decimal? NullableLabelDecimalGet(Label lblBox)
    {
        decimal x;

        if (decimal.TryParse(lblBox.Text, out x))
            return x;

        return null;
    }

    public void NullableDecimalSet(TextBox txtBox, decimal? value)
    {
        txtBox.Text = (value != null) ? value.ToString() : string.Empty;
    }

    public void NullableLabelDecimalSet(Label lblBox, decimal? value)
    {
        lblBox.Text = (value != null) ? value.ToString() : string.Empty;
    }

    public decimal DecimalGet(TextBox txtBox)
    {
        decimal x;

        if(decimal.TryParse(txtBox.Text, out x))
            return x;

        return 0;
    }

    public decimal DecimalLabelGet(Label lblBox)
    {
        decimal x;

        if (decimal.TryParse(lblBox.Text, out x))
            return x;

        return 0;
    }

    public void DecimalSet(TextBox txtBox, decimal value)
    {
        txtBox.Text = value.ToString();
    }

    public void DecimalLabelSet(Label lblBox, decimal value)
    {
        lblBox.Text = value.ToString();
    }

    public int? NullableIntGet(TextBox txtBox)
    {
        int x;

        if (int.TryParse(txtBox.Text, out x))
            return x;

        return null;
    }

    public void NullableIntSet(TextBox txtBox, int? value)
    {
        txtBox.Text = (value != null) ? value.ToString() : string.Empty;
    }

    public int IntGet(TextBox txtBox)
    {
        int x;

        if(int.TryParse(txtBox.Text, out x))
            return x;

        return 0;
    }

    public void IntSet(TextBox txtBox, int value)
    {
        txtBox.Text = value.ToString();
    }
}
