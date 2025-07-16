using DataUtilityCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RCT_UC_Checkbox : UserControl
{

    public event EventHandler CheckedChanged;

    private EnumStatoSiNoNc? valueField;
    private bool disableNc;
    private bool disableNa;

    private bool disableEnabledNc;
    private bool disableEnabledNa;

    public bool DisableEnabledNC
    {
        get { return disableEnabledNc; }
        set { disableEnabledNc = value; }
    }

    public bool DisableEnabledNA
    {
        get { return disableEnabledNa; }
        set { disableEnabledNa = value; }
    }

    public EnumStatoSiNoNc? Value
    {
        get
        {
            GetRadioValue();
            return valueField;
        }
        set
        {
            valueField = value;
            UpdateRadio();
        }
    }

    public bool DisableNC
    {
        get { return disableNc; }
        set { disableNc = value; }
    }

    public bool DisableNA
    {
        get { return disableNa; }
        set { disableNa = value; }
    }

    public bool AutoPostBack { get; set; }
        
    public string CaptionForNa { get; set; }
    public string CaptionForNc { get; set; }
    public string CaptionForYes { get; set; }
    public string CaptionForNo { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        radioNc.Visible = !disableNc;
        radioNa.Visible = !disableNa;

        radioNc.Enabled = !disableEnabledNc;
        radioNa.Enabled = !disableEnabledNa;

        radioNa.CheckedChanged += radio_CheckedChanged;
        radioNc.CheckedChanged += radio_CheckedChanged;
        radioYes.CheckedChanged += radio_CheckedChanged;
        radioNo.CheckedChanged += radio_CheckedChanged;

        radioNa.AutoPostBack = AutoPostBack;
        radioNc.AutoPostBack = AutoPostBack;
        radioYes.AutoPostBack = AutoPostBack;
        radioNo.AutoPostBack = AutoPostBack;
        
        if (!string.IsNullOrEmpty(CaptionForNa))
            radioNa.Text = CaptionForNa;
        if (!string.IsNullOrEmpty(CaptionForNc))
            radioNc.Text = CaptionForNc;
        if (!string.IsNullOrEmpty(CaptionForYes))
            radioYes.Text = CaptionForYes;
        if (!string.IsNullOrEmpty(CaptionForNo))
            radioNo.Text = CaptionForNo;
    }
    
    void radio_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckedChanged != null)
            CheckedChanged(this, EventArgs.Empty);
    }

    private  void UpdateRadio()
    {
        radioNa.Checked = !disableNa && valueField.HasValue && valueField.Value == EnumStatoSiNoNc.NonApplicabile;
        radioNc.Checked = !disableNc && valueField.HasValue && valueField.Value == EnumStatoSiNoNc.NonClassificabile;
        radioNo.Checked = valueField.HasValue && valueField.Value == EnumStatoSiNoNc.No;
        radioYes.Checked = valueField.HasValue && valueField.Value == EnumStatoSiNoNc.Si;
    }

    private void GetRadioValue()
    {
        if (radioNa.Checked)
            valueField = EnumStatoSiNoNc.NonApplicabile;
        else if (radioNc.Checked)
            valueField = EnumStatoSiNoNc.NonClassificabile;
        else if (radioNo.Checked)
            valueField = EnumStatoSiNoNc.No;
        else if (radioYes.Checked)
            valueField = EnumStatoSiNoNc.Si;
        else
            valueField =  null;
    }

}