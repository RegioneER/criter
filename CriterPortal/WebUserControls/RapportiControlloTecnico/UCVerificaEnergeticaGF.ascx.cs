using System;


public partial class RCT_UC_UCVerificaEnergeticaGF : RCT_UC_UCVerificaEnergeticaBase
{
    public decimal? Surriscaldamento
    {
        get 
        {
            return NullableDecimalGet(txtSurriscaldamento);
        }
        set 
        {
            NullableDecimalSet(txtSurriscaldamento,value);
        }
    }

    public decimal? Sottoraffredamento
    {
        get
        {
            return NullableDecimalGet(txtSottoraffreddamento);
        }
        set
        {
            NullableDecimalSet(txtSottoraffreddamento, value);
        }
    }

    public decimal? TemperaturaCondensazione
    {
        get
        {
            return NullableDecimalGet(txtTemperaturaCondensazione);
        }
        set
        {
            NullableDecimalSet(txtTemperaturaCondensazione, value);
        }
    }

    public decimal? TemperaturaEvaporazione
    {
        get
        {
            return NullableDecimalGet(txtTemperaturaEvaporazione);
        }
        set
        {
            NullableDecimalSet(txtTemperaturaEvaporazione, value);
        }
    }

    public decimal? TInEst
    {
        get
        {
            return NullableDecimalGet(txtTinEst);
        }
        set
        {
            NullableDecimalSet(txtTinEst, value);
        }
    }

    public decimal? TOutEst
    {
        get
        {
            return NullableDecimalGet(txtToutEst);
        }
        set
        {
            NullableDecimalSet(txtToutEst, value);
        }
    }

    public decimal? TInUtenze
    {
        get
        {
            return NullableDecimalGet(txtTinUtenze);
        }
        set
        {
            NullableDecimalSet(txtTinUtenze, value);
        }
    }

    public decimal? TOutUtenze
    {
        get
        {
            return NullableDecimalGet(txtToutUtenze);
        }
        set
        {
            NullableDecimalSet(txtToutUtenze, value);
        }
    }

    public decimal? NCircuito
    {
        get
        {
            return NullableDecimalGet(txtNCircuito);
        }
        set
        {
            NullableDecimalSet(txtNCircuito, value);
        }
    }
    
    public decimal? PotenzaAssorbita
    {
        get
        {
            return NullableDecimalGet(txtPotenzaAssorbita);
        }
        set
        {
            NullableDecimalSet(txtPotenzaAssorbita, value);
        }
    }
    public decimal? TUscitaFluido
    {
        get
        {
            return NullableDecimalGet(txtTUscitaFluido);
        }
        set
        {
            NullableDecimalSet(txtTUscitaFluido, value);
        }
    }
    public decimal? TBulboUmidoAria
    {
        get
        {
            return NullableDecimalGet(txtTBulboUmidoAria);
        }
        set
        {
            NullableDecimalSet(txtTBulboUmidoAria, value);
        }
    }
    public decimal? TIngressoLatoEsterno
    {
        get
        {
            return NullableDecimalGet(txtTIngressoLatoEsterno);
        }
        set
        {
            NullableDecimalSet(txtTIngressoLatoEsterno, value);
        }
    }
    public decimal? TUscitaLatoEsterno
    {
        get
        {
            return NullableDecimalGet(txtTUscitaLatoEsterno);
        }
        set
        {
            NullableDecimalSet(txtTUscitaLatoEsterno, value);
        }
    }
    public decimal? TIngressoLatoMacchina
    {
        get
        {
            return NullableDecimalGet(txtTIngressoLatoMacchina);
        }
        set
        {
            NullableDecimalSet(txtTIngressoLatoMacchina, value);
        }
    }
    public decimal? TUscitaLatoMacchina
    {
        get
        {
            return NullableDecimalGet(txtTUscitaLatoMacchina);
        }
        set
        {
            NullableDecimalSet(txtTUscitaLatoMacchina, value);
        }
    }

    

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}