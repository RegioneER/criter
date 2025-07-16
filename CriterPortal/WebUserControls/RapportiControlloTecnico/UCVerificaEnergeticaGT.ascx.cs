using DataUtilityCore;
using System;
using System.Web.UI.WebControls;

public partial class RCT_UC_UCVerificaEnergeticaGT : RCT_UC_UCVerificaEnergeticaBase
{
 
    public decimal? TemperaturaFumi
    {
        get
        {
            return NullableDecimalGet(txtTemperaturaFumi);
        }
        set
        {
            NullableDecimalSet(txtTemperaturaFumi, value);
        }
    }
    public decimal? TemperaturaComburente
    {
        get
        {
            return NullableDecimalGet(txtTemperaturaComburente);
        }
        set
        {
            NullableDecimalSet(txtTemperaturaComburente, value);
        }
    }
    public decimal? O2
    {
        get
        {
            return NullableDecimalGet(txtO2);
        }
        set
        {
            NullableDecimalSet(txtO2, value);
        }
    }
    public decimal? Co2
    {
        get
        {
            return NullableDecimalGet(txtCO2);
        }
        set
        {
            NullableDecimalSet(txtCO2, value);
        }
    }
    public decimal? RendimentoCombustione
    {
        get
        {
            return NullableDecimalGet(txtRendimentoCombustione);
        }
        set
        {
            NullableDecimalSet(txtRendimentoCombustione, value);
        }
    }
    public decimal? RendimentoMinimo
    {
        get
        {
            return NullableLabelDecimalGet(lblRendimentoMinimo);
        }
        set
        {
            NullableLabelDecimalSet(lblRendimentoMinimo, value);
        }
    }

    public decimal? Bacharach1
    {
        get
        {
            return NullableDecimalGet(txtBacharach1);
        }
        set
        {
            NullableDecimalSet(txtBacharach1, value);
        }
    }

    public decimal? Bacharach2
    {
        get
        {
            return NullableDecimalGet(txtBacharach2);
        }
        set
        {
            NullableDecimalSet(txtBacharach2, value);
        }
    }

    public decimal? Bacharach3
    {
        get
        {
            return NullableDecimalGet(txtBacharach3);
        }
        set
        {
            NullableDecimalSet(txtBacharach3, value);
        }
    }

    public decimal? CoCorretto
    {
        get
        {
            return NullableDecimalGet(txtCoCorretto);
        }
        set
        {
            NullableDecimalSet(txtCoCorretto, value);
        }
    }

    public string Modulotermico
    {
        get
        {
            return txtModuloTermico.Text;
        }
        set
        {
            txtModuloTermico.Text = value;
        }
    }

    public decimal? COFumiSecchi
    {
        get
        {
            return NullableDecimalGet(txtCoFumiSecchi);
        }
        set
        {
            NullableDecimalSet(txtCoFumiSecchi, value);
        }
    }

    public decimal? PortataCombustibile
    {
        get
        {
            return NullableDecimalGet(txtPortataCombustibile);
        }
        set
        {
            NullableDecimalSet(txtPortataCombustibile, value);
        }
    }
    
    public bool RispettaIndiceBacharach
    {
        get
        {
            if(chkRispettaIndiceBacharach.Value.HasValue)
                return ( chkRispettaIndiceBacharach.Value == EnumStatoSiNoNc.Si );

            return false;
        }
        set
        {
            chkRispettaIndiceBacharach.Value = ((bool)value ) ? EnumStatoSiNoNc.Si : EnumStatoSiNoNc.No;
        }
    }

    public bool COFumiSecchiNoAria1000
    {
        get
        {

            if(chkCOFumiSecchiNoAria1000.Value.HasValue)
                return ( chkCOFumiSecchiNoAria1000.Value == EnumStatoSiNoNc.Si );

            return false;
        }
        set
        {
            chkCOFumiSecchiNoAria1000.Value = ( (bool)value ) ? EnumStatoSiNoNc.Si : EnumStatoSiNoNc.No;
        }
    }

    public bool RendimentoSupMinimo
    {
        get
        {
            if(chkRendimentoSupMinimo.Value.HasValue)
                return ( chkRendimentoSupMinimo.Value == EnumStatoSiNoNc.Si );

            return false;
        }
        set
        {
            chkRendimentoSupMinimo.Value = ((bool)value ) ? EnumStatoSiNoNc.Si : EnumStatoSiNoNc.No;
        }
    }

    public decimal? PotenzaTermicaEffettiva
    {
        get
        {
            return NullableDecimalGet(txtPotenzaTermicaEffettiva);
        }
        set
        {
            NullableDecimalSet(txtPotenzaTermicaEffettiva, value);
        }
    }

    public string IDTipologiaControllo
    {
        get
        {
            return lblIDTipologiaControllo.Text;
        }
        set
        {
            lblIDTipologiaControllo.Text = value;
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public override void DataBind()
    {
        base.DataBind();
        SetRequiredFields(IDTipologiaControllo);
    }

    protected void SetRequiredFields(string IDTipologiaControllo)
    {
        if (IDTipologiaControllo == "1")
        {
            txtTemperaturaFumi.CssClass = "txtClass_o";
            rfvtxtTemperaturaFumi.Enabled = true;

            txtTemperaturaComburente.CssClass = "txtClass_o";
            rfvtxtTemperaturaComburente.Enabled = true;

            txtO2.CssClass = "txtClass_o";
            rfvtxtO2.Enabled = true;

            txtCO2.CssClass = "txtClass_o";
            rfvtxtCO2.Enabled = true;

            txtBacharach1.CssClass = "txtClass_o";
            rfvtxtBacharach1.Enabled = true;

            txtBacharach2.CssClass = "txtClass_o";
            rfvtxtBacharach2.Enabled = true;

            txtBacharach3.CssClass = "txtClass_o";
            rfvtxtBacharach3.Enabled = true;
            
            txtCoFumiSecchi.CssClass = "txtClass_o";
            rfvtxtCoFumiSecchi.Enabled = true;

            txtCoCorretto.CssClass = "txtClass_o";
            rfvtxtCoCorretto.Enabled = true;

            txtPotenzaTermicaEffettiva.CssClass = "txtClass_o";
            rfvtxtPotenzaTermicaEffettiva.Enabled = true;

            txtRendimentoCombustione.CssClass = "txtClass_o";
            rfvtxtRendimentoCombustione.Enabled = true;

            //txtRendimentoMinimo.CssClass = "txtClass_o";
            //rfvtxtRendimentoMinimo.Enabled = true;
        }
        else
        {
            txtTemperaturaFumi.CssClass = "txtClass";
            rfvtxtTemperaturaFumi.Enabled = false;

            txtTemperaturaComburente.CssClass = "txtClass";
            rfvtxtTemperaturaComburente.Enabled = false;

            txtO2.CssClass = "txtClass";
            rfvtxtO2.Enabled = false;

            txtCO2.CssClass = "txtClass";
            rfvtxtCO2.Enabled = false;

            txtBacharach1.CssClass = "txtClass";
            rfvtxtBacharach1.Enabled = false;

            txtBacharach2.CssClass = "txtClass";
            rfvtxtBacharach2.Enabled = false;

            txtBacharach3.CssClass = "txtClass";
            rfvtxtBacharach3.Enabled = false;

            txtCoFumiSecchi.CssClass = "txtClass";
            rfvtxtCoFumiSecchi.Enabled = false;

            txtCoCorretto.CssClass = "txtClass";
            rfvtxtCoCorretto.Enabled = false;

            txtPotenzaTermicaEffettiva.CssClass = "txtClass";
            rfvtxtPotenzaTermicaEffettiva.Enabled = false;

            txtRendimentoCombustione.CssClass = "txtClass";
            rfvtxtRendimentoCombustione.Enabled = false;

            //txtRendimentoMinimo.CssClass = "txtClass";
            //rfvtxtRendimentoMinimo.Enabled = false;
        }
    }
    
    protected void chkRispettaIndiceBacharach_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRispettaIndiceBacharach.Value == EnumStatoSiNoNc.No)
        {
            UCRaccomandazioniPrescrizioni1.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni1.Visible = false;
        }
    }
    
    protected void chkCOFumiSecchiNoAria1000_CheckedChanged(object sender, EventArgs e)
    {
        if (chkCOFumiSecchiNoAria1000.Value == EnumStatoSiNoNc.No)
        {
            UCRaccomandazioniPrescrizioni2.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni2.Visible = false;
        }
    }

    protected void chkRendimentoSupMinimo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkRendimentoSupMinimo.Value == EnumStatoSiNoNc.No)
        {
            UCRaccomandazioniPrescrizioni3.Visible = true;
        }
        else
        {
            UCRaccomandazioniPrescrizioni3.Visible = false;
        }
    }

}