using System;

public partial class RCT_UC_UCVerificaEnergeticaSC : RCT_UC_UCVerificaEnergeticaBase
{
    public decimal TermperaturaEsterna
    {
        get
        {
            return DecimalGet(txtTemperaturaEsterna);
        }
        set
        {
            DecimalSet(txtTemperaturaEsterna,value);
        }
    }
    public decimal TermperaturaMandataPrimario
    {
        get
        {
            return DecimalGet(txtTemperaturaMandataPrimario);
        }
        set
        {
            DecimalSet(txtTemperaturaMandataPrimario, value);
        }
    }

    public decimal TermperaturaRitornoPrimario
    {
        get
        {
            return DecimalGet(txtTemperaturaRitornoPrimario);
        }
        set
        {
            DecimalSet(txtTemperaturaRitornoPrimario, value);
        }
    }

    public decimal PotenzaTermica
    {
        get
        {
            return DecimalGet(txtPotenzaTermica);
        }
        set
        {
            DecimalSet(txtPotenzaTermica, value);
        }
    }

    public decimal PortataFluidoPrimario
    {
        get
        {
            return DecimalGet(txtPortataFluidoPrimario);
        }
        set
        {
            DecimalSet(txtPortataFluidoPrimario, value);
        }
    }

    public decimal TermperaturaMandataSecondario
    {
        get
        {
            return DecimalGet(txtTemperaturamandataSecondario);
        }
        set
        {
            DecimalSet(txtTemperaturamandataSecondario, value);
        }
    }

    public decimal TermperaturaRitornoSecondario
    {
        get
        {
            return DecimalGet(txtTemperaturaRitornoSecondario);
        }
        set
        {
            DecimalSet(txtTemperaturaRitornoSecondario, value);
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {

    }
}