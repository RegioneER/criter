<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCVerificaEnergeticaGT.ascx.cs" Inherits="RCT_UC_UCVerificaEnergeticaGT" %>
<%@ Register TagPrefix="uc1" TagName="UCCheckbox" Src="~/WebUserControls/RapportiControlloTecnico/UCCheckbox.ascx" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCRaccomandazioniPrescrizioni.ascx" TagPrefix="uc1" TagName="UCRaccomandazioniPrescrizioni" %>

<asp:Label runat="server" ID="lblIDTipologiaControllo" Visible="false" />
<asp:Table ID="tblGT" runat="server" Width="100%">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="Modulo Termico" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtModuloTermico" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtModuloTermico" ControlToValidate="txtModuloTermico"
				ErrorMessage="Modulo Termico: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>

        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="Temperatura fumi (°C)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtTemperaturaFumi" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaFumi" ControlToValidate="txtTemperaturaFumi"
				ErrorMessage="Temperatura Fumi: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" ValidationGroup="vgRapportoDiControllo"
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaFumi" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura Fumi (°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaFumi">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="Temperatura aria comburente (°C)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtTemperaturaComburente" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaComburente" ControlToValidate="txtTemperaturaComburente"
				ErrorMessage="Temperatura Aria Comburente: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtTemperaturaComburente" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Temperatura Aria Comburente (°C): campo obbligatorio"
                ControlToValidate="txtTemperaturaComburente">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="O2 (%)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtO2" runat="server" CssClass="txtClass" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtO2" ControlToValidate="txtO2"
				ErrorMessage="O2 (%): inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtO2" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - O2 (%): campo obbligatorio"
                ControlToValidate="txtO2">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="CO2 (%)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtCO2" runat="server" CssClass="txtClass" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtCO2" ControlToValidate="txtCO2"
				ErrorMessage="CO2 (%): inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtCO2" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - CO2 (%): campo obbligatorio"
                ControlToValidate="txtCO2">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1" Visible='<%# int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 4 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 5 %>'>
           <asp:Label runat="server" Text="Bacharach" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1" Visible='<%# int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 4 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 5 %>'>
            <asp:TextBox ID="txtBacharach1" CssClass="txtClass" runat="server" MaxLength="5" Width="50" />
            / 
            <asp:TextBox ID="txtBacharach2" CssClass="txtClass" runat="server" MaxLength="5" Width="50" />
            /
            <asp:TextBox ID="txtBacharach3" CssClass="txtClass" runat="server" MaxLength="5" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtBacharach1" ControlToValidate="txtBacharach1"
				ErrorMessage="Bacharach 1: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtBacharach1" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Bacharach 1: campo obbligatorio"
                ControlToValidate="txtBacharach1">&nbsp;*</asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="revtxtBacharach2" ControlToValidate="txtBacharach2"
				ErrorMessage="Bacharach 2: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtBacharach2" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Bacharach 2: campo obbligatorio"
                ControlToValidate="txtBacharach2">&nbsp;*</asp:RequiredFieldValidator>

            <asp:RegularExpressionValidator ID="revtxtBacharach3" ControlToValidate="txtBacharach3"
				ErrorMessage="Bacharach 3: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtBacharach3" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Bacharach 3: campo obbligatorio"
                ControlToValidate="txtBacharach3">&nbsp;*</asp:RequiredFieldValidator>

        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="CO fumi secchi (ppm)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtCoFumiSecchi" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtCoFumiSecchi" ControlToValidate="txtCoFumiSecchi"
				ErrorMessage="CO fumi secchi: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtCoFumiSecchi" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - CO fumi secchi (ppm): campo obbligatorio"
                ControlToValidate="txtCoFumiSecchi">&nbsp;*</asp:RequiredFieldValidator>

        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="CO corretto (ppm)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtCoCorretto" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtCoCorretto" ControlToValidate="txtCoCorretto"
				ErrorMessage="CO corretto: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtCoCorretto" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - CO corretto (ppm): campo obbligatorio"
                ControlToValidate="txtCoCorretto">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="Portata combustibile (m3/h)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtPortataCombustibile" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtPortataCombustibile" ControlToValidate="txtPortataCombustibile"
				ErrorMessage="Portata combustibile: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:Label runat="server" Text="Potenza termica effettiva (kW)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtPotenzaTermicaEffettiva" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtPotenzaTermicaEffettiva" ControlToValidate="txtPotenzaTermicaEffettiva"
				ErrorMessage="Potenza termica effettiva: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="rfvtxtPotenzaTermicaEffettiva" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Potenza termica effettiva (kW): campo obbligatorio"
                ControlToValidate="txtPotenzaTermicaEffettiva">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell ColumnSpan="1">
           <asp:Label runat="server" Text="Rendimento di combustione (%)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:TextBox ID="txtRendimentoCombustione" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtRendimentoCombustione" ControlToValidate="txtRendimentoCombustione"
				ErrorMessage="Rendimento di combustione: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtRendimentoCombustione" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Rendimento di combustione (%): campo obbligatorio"
                ControlToValidate="txtRendimentoCombustione">&nbsp;*</asp:RequiredFieldValidator>
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:Label runat="server" Text="Rendimento minimo di legge (%)" />
        </asp:TableCell>
        <asp:TableCell ColumnSpan="1">
            <asp:Label runat="server" ID="lblRendimentoMinimo" />
            <%--<asp:TextBox ID="txtRendimentoMinimo" CssClass="txtClass" runat="server" Width="50" />
            <asp:RegularExpressionValidator ID="revtxtRendimentoMinimo" ControlToValidate="txtRendimentoMinimo"
				ErrorMessage="Rendimento minimo di legge: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
            <asp:RequiredFieldValidator ID="rfvtxtRendimentoMinimo" runat="server" ValidationGroup="vgRapportoDiControllo" ForeColor="Red"
                Display="Dynamic" EnableClientScript="true"
                InitialValue="" ErrorMessage="Sezione E - Rendimento minimo di legge (%): campo obbligatorio"
                ControlToValidate="txtRendimentoMinimo">&nbsp;*</asp:RequiredFieldValidator>--%>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
<br />
<table>
    <tr runat="server" id="rowIndiceBacharach" Visible='<%# int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 4 || int.Parse(Eval("IDTipologiaCombustibile").ToString()) == 5 %>'>
        <td>Rispetta l'indice di Bacharach</td>
        <td>
            <uc1:UCCheckbox ID="chkRispettaIndiceBacharach" DisableNC="true" AutoPostBack="true" 
                OnCheckedChanged="chkRispettaIndiceBacharach_CheckedChanged" DisableNA="true" runat="server" />
            <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='20'
                iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server" 
                ID="UCRaccomandazioniPrescrizioni1"
                visible='<%# (int)Eval("IDTipologiaRCT") == 1 && bool.Parse(Eval("RispettaIndiceBacharach").ToString()) == false %>' />
        </td>
    </tr>
    <tr>
        <td>CO corretto <= 1.000 ppm v/v </td>
        <td>
            <uc1:UCCheckbox ID="chkCOFumiSecchiNoAria1000" DisableNC="true" DisableNA="true" AutoPostBack="true" 
                OnCheckedChanged="chkCOFumiSecchiNoAria1000_CheckedChanged" runat="server" />
            <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='21' 
                iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server" 
                ID="UCRaccomandazioniPrescrizioni2"
                visible='<%# (int)Eval("IDTipologiaRCT") == 1 && bool.Parse(Eval("COFumiSecchiNoAria1000").ToString()) == false %>' />
        </td>
    </tr>
    <tr>
        <td>Rendimento >= rendimento minimo </td>
        <td>
            <uc1:UCCheckbox ID="chkRendimentoSupMinimo" DisableNC="true" DisableNA="true" runat="server" 
                AutoPostBack="true" OnCheckedChanged="chkRendimentoSupMinimo_CheckedChanged"  />
            <uc1:UCRaccomandazioniPrescrizioni iDTipologiaRaccomandazionePrescrizioneRct='22' 
                iDRapportoControlloTecnico='<%# Eval("iDRapportoControlloTecnico").ToString() %>' runat="server" 
                ID="UCRaccomandazioniPrescrizioni3"
                visible='<%# (int)Eval("IDTipologiaRCT") == 1 && bool.Parse(Eval("RendimentoSupMinimo").ToString()) == false %>' />
        </td>
    </tr>
</table>