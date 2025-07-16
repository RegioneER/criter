<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCVerificaEnergeticaSC.ascx.cs" Inherits="RCT_UC_UCVerificaEnergeticaSC" %>

<asp:Label runat="server" ID="lblIDTipologiaControllo" Visible="false" />
<asp:Table ID="tblSC" runat="server" Width="100%">
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura esterna (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTemperaturaEsterna" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaEsterna" ControlToValidate="txtTemperaturaEsterna"
				ErrorMessage="Temperatura esterna: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura mandata primario (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTemperaturaMandataPrimario" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaMandataPrimario" ControlToValidate="txtTemperaturaMandataPrimario"
				ErrorMessage="Temperatura mandata primario: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura ritorno primario (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTemperaturaRitornoPrimario" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaRitornoPrimario" ControlToValidate="txtTemperaturaRitornoPrimario"
				ErrorMessage="Temperatura ritorno primario: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        
        <asp:TableCell>
            <asp:Label runat="server" Text="Portata fluido primario (m3/h)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtPortataFluidoPrimario" />
            <asp:RegularExpressionValidator ID="revtxtPortataFluidoPrimario" ControlToValidate="txtPortataFluidoPrimario"
				ErrorMessage="Portata fluido primario: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell>
        <asp:Label runat="server" Text="Temperatura mandata secondario (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTemperaturamandataSecondario" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturamandataSecondario" ControlToValidate="txtTemperaturamandataSecondario"
				ErrorMessage="Temperatura mandata secondario: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura ritorno secondario (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTemperaturaRitornoSecondario" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaRitornoSecondario" ControlToValidate="txtTemperaturaRitornoSecondario"
				ErrorMessage="Temperatura ritorno secondario: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Potenza termica (kW)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtPotenzaTermica" />
            <asp:RegularExpressionValidator ID="revtxtPotenzaTermica" ControlToValidate="txtPotenzaTermica"
				ErrorMessage="Potenza termica: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        <asp:TableCell>
            &nbsp;
        </asp:TableCell>
        <asp:TableCell>
            &nbsp;
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>