<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCVerificaEnergeticaCG.ascx.cs" Inherits="RCT_UC_UCVerificaEnergeticaCG" %>

<asp:Label runat="server" ID="lblIDTipologiaControllo" Visible="false" />
<asp:Table ID="tblCG" runat="server" Width="100%">
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Potenza ai morsetti del generatore (kW)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox ID="txtPotenzaaiMorsetti" runat="server" Width="80" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revtxtPotenzaaiMorsetti" ControlToValidate="txtPotenzaaiMorsetti"
				ErrorMessage="Potenza ai morsetti del generatore: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtPotenzaaiMorsetti" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Potenza ai morsetti del generatore (kW): campo obbligatorio"
                ControlToValidate="txtPotenzaaiMorsetti">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura aria comburente (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox ID="txtTemperaturaAriaComburente" runat="server" Width="80" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaAriaComburente" ControlToValidate="txtTemperaturaAriaComburente"
				ErrorMessage="Temperatura aria comburente: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaAriaComburente" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura aria comburente (°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaAriaComburente">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>
   
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura acqua in uscita (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox ID="txtTemperaturaAcquaUscita" runat="server" Width="80" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaAcquaUscita" ControlToValidate="txtTemperaturaAcquaUscita"
				ErrorMessage="Temperatura acqua in uscita: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaAcquaUscita" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura acqua in uscita (°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaAcquaUscita">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>        
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura acqua in ingresso (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox ID="txtTemperaturaAcquaIngresso" runat="server" Width="80" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaAcquaIngresso" ControlToValidate="txtTemperaturaAcquaIngresso"
				ErrorMessage="Temperatura acqua in ingresso: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaAcquaIngresso" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura acqua in ingresso (°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaAcquaIngresso">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura acqua motore  {solo m.c.i }(°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox ID="txtTemperaturaAcquaMotore" runat="server" Width="80" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaAcquaMotore" ControlToValidate="txtTemperaturaAcquaMotore"
				ErrorMessage="Temperatura acqua motore: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaAcquaMotore" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura acqua motore  {solo m.c.i }(°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaAcquaMotore">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
        
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura fumi a valle dello scambiatore (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" Width="80" CssClass="txtClass" ID="txtTemperaturaFumiValle" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaFumiValle" ControlToValidate="txtTemperaturaFumiValle"
				ErrorMessage="Temperatura fumi a valle dello scambiatore: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaFumiValle" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura fumi a valle dello scambiatore (°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaFumiValle">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>
   
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura fumi a monte dello scambiatore (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox ID="txtTemperaturaFumiMonte" runat="server" Width="80" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaFumiMonte" ControlToValidate="txtTemperaturaFumiMonte"
				ErrorMessage="Temperatura fumi a monte dello scambiatore: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaFumiMonte" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura fumi a monte dello scambiatore (°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaFumiMonte">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
        
        <asp:TableCell>
            <asp:Label runat="server" Text="Emissione di monossido di carbonio CO (riportato al 5% di O2 nei fumi)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox ID="txtEmissioneMonossido" runat="server" Width="80" CssClass="txtClass" />
            <asp:RegularExpressionValidator ID="revtxtEmissioneMonossido" ControlToValidate="txtEmissioneMonossido"
				ErrorMessage="Emissione di monossido di carbonio CO: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtEmissioneMonossido" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Emissione di monossido di carbonio CO (riportato al 5% di O2 nei fumi): campo obbligatorio"
                ControlToValidate="txtEmissioneMonossido">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell ColumnSpan="4" VerticalAlign="Middle" Height="40">
            Protezione di interfaccia con la rete elettrica Verifica per L1/L2/L3
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell >
            <asp:Label runat="server" Text="Sovrafrequenza soglia di interv. (Hz)" Width="120" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovrafrequenzaSogliaInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovrafrequenzaSogliaInterv1" ControlToValidate="txtSovrafrequenzaSogliaInterv1"
				ErrorMessage="Sovrafrequenza soglia di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovrafrequenzaSogliaInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovrafrequenzaSogliaInterv2" ControlToValidate="txtSovrafrequenzaSogliaInterv2"
				ErrorMessage="Sovrafrequenza soglia di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovrafrequenzaSogliaInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovrafrequenzaSogliaInterv3" ControlToValidate="txtSovrafrequenzaSogliaInterv3"
				ErrorMessage="Sovrafrequenza soglia di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sovrafrequenza tempo di interv. (s)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovrafrequenzaTempoInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovrafrequenzaTempoInterv1" ControlToValidate="txtSovrafrequenzaTempoInterv1"
				ErrorMessage="Sovrafrequenza tempo di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovrafrequenzaTempoInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovrafrequenzaTempoInterv2" ControlToValidate="txtSovrafrequenzaTempoInterv2"
				ErrorMessage="Sovrafrequenza tempo di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovrafrequenzaTempoInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovrafrequenzaTempoInterv3" ControlToValidate="txtSovrafrequenzaTempoInterv3"
				ErrorMessage="Sovrafrequenza tempo di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sottofrequenza soglia di interv. (Hz)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottofrequenzaSogliaInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottofrequenzaSogliaInterv1" ControlToValidate="txtSottofrequenzaSogliaInterv1"
				ErrorMessage="Sottofrequenza soglia di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottofrequenzaSogliaInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottofrequenzaSogliaInterv2" ControlToValidate="txtSottofrequenzaSogliaInterv2"
				ErrorMessage="Sottofrequenza soglia di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottofrequenzaSogliaInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottofrequenzaSogliaInterv3" ControlToValidate="txtSottofrequenzaSogliaInterv3"
				ErrorMessage="Sottofrequenza soglia di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sottofrequenza tempo di interv. (s)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottofrequenzaTempoInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottofrequenzaTempoInterv1" ControlToValidate="txtSottofrequenzaTempoInterv1"
				ErrorMessage="Sottofrequenza tempo di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottofrequenzaTempoInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottofrequenzaTempoInterv2" ControlToValidate="txtSottofrequenzaTempoInterv2"
				ErrorMessage="Sottofrequenza tempo di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottofrequenzaTempoInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottofrequenzaTempoInterv3" ControlToValidate="txtSottofrequenzaTempoInterv3"
				ErrorMessage="Sottofrequenza tempo di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sovratensione soglia di interv. (V)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovratensioneSogliaInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovratensioneSogliaInterv1" ControlToValidate="txtSovratensioneSogliaInterv1"
				ErrorMessage="Sovratensione soglia di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovratensioneSogliaInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovratensioneSogliaInterv2" ControlToValidate="txtSovratensioneSogliaInterv2"
				ErrorMessage="Sovratensione soglia di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovratensioneSogliaInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovratensioneSogliaInterv3" ControlToValidate="txtSovratensioneSogliaInterv3"
				ErrorMessage="Sovratensione soglia di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sovratensione tempo di interv. (s)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovratensioneTempoInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovratensioneTempoInterv1" ControlToValidate="txtSovratensioneTempoInterv1"
				ErrorMessage="Sovratensione tempo di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovratensioneTempoInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovratensioneTempoInterv2" ControlToValidate="txtSovratensioneTempoInterv2"
				ErrorMessage="Sovratensione tempo di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSovratensioneTempoInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSovratensioneTempoInterv3" ControlToValidate="txtSovratensioneTempoInterv3"
				ErrorMessage="Sovratensione tempo di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sottotensione soglia di interv. (V)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottotensioneSogliaInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottotensioneSogliaInterv1" ControlToValidate="txtSottotensioneSogliaInterv1"
				ErrorMessage="Sottotensione soglia di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottotensioneSogliaInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottotensioneSogliaInterv2" ControlToValidate="txtSottotensioneSogliaInterv2"
				ErrorMessage="Sottotensione soglia di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottotensioneSogliaInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottotensioneSogliaInterv3" ControlToValidate="txtSottotensioneSogliaInterv3"
				ErrorMessage="Sottotensione soglia di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sottotensione tempo di interv. (s)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="3">
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottotensioneTempoInterv1" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottotensioneTempoInterv1" ControlToValidate="txtSottotensioneTempoInterv1"
				ErrorMessage="Sottotensione tempo di interv. 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottotensioneTempoInterv2" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottotensioneTempoInterv2" ControlToValidate="txtSottotensioneTempoInterv2"
				ErrorMessage="Sottotensione tempo di interv. 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottotensioneTempoInterv3" Width="80" />
            <asp:RegularExpressionValidator ID="revtxtSottotensioneTempoInterv3" ControlToValidate="txtSottotensioneTempoInterv3"
				ErrorMessage="Sottotensione tempo di interv. 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true" ValidationGroup="vgRapportoDiControllo">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>