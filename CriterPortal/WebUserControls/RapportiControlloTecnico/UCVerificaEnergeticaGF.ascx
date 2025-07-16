<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCVerificaEnergeticaGF.ascx.cs" Inherits="RCT_UC_UCVerificaEnergeticaGF" %>

<asp:Label runat="server" ID="lblIDTipologiaControllo" Visible="false" />
<asp:Table ID="tblGT" runat="server" Width="100%">
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="N° Circuito" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtNCircuito" />
            <asp:RegularExpressionValidator ID="revtxtNCircuito" ControlToValidate="txtNCircuito"
				ErrorMessage="N° Circuito: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        <asp:TableCell>
            <asp:Label runat="server" Text="Surriscaldamento (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSurriscaldamento" />
            <asp:RegularExpressionValidator ID="revtxtSurriscaldamento" ControlToValidate="txtSurriscaldamento"
				ErrorMessage="Surriscaldamento: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Sottoraffreddamento (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtSottoraffreddamento" />
            <asp:RegularExpressionValidator ID="revtxtSottoraffreddamento" ControlToValidate="txtSottoraffreddamento"
				ErrorMessage="Sottoraffreddamento: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>        
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura Condensazione (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTemperaturaCondensazione" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaCondensazione" ControlToValidate="txtTemperaturaCondensazione"
				ErrorMessage="Temperatura Condensazione: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura Evaporazione (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTemperaturaEvaporazione" />
            <asp:RegularExpressionValidator ID="revtxtTemperaturaEvaporazione" ControlToValidate="txtTemperaturaEvaporazione"
				ErrorMessage="Temperatura Evaporazione: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>        
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura Ingresso lato est (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTinEst" />
            <asp:RegularExpressionValidator ID="revtxtTinEst" ControlToValidate="txtTinEst"
				ErrorMessage="Temperatura Ingresso lato est: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura uscita lato est (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtToutEst" />
            <asp:RegularExpressionValidator ID="revtxtToutEst" ControlToValidate="txtToutEst"
				ErrorMessage="Temperatura uscita lato est: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>        
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura ingresso lato utenze (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTinUtenze" />
            <asp:RegularExpressionValidator ID="revtxtTinUtenze" ControlToValidate="txtTinUtenze"
				ErrorMessage="Temperatura ingresso lato utenze: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>

    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="Temperatura uscita lato utenze (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtToutUtenze" />
            <asp:RegularExpressionValidator ID="revtxtToutUtenze" ControlToValidate="txtToutUtenze"
				ErrorMessage="Temperatura uscita lato utenze: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>        
        <asp:TableCell>
            <asp:Label runat="server" Text="Potenza assorbita (kW)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtPotenzaAssorbita" />
            <asp:RegularExpressionValidator ID="revtxtPotenzaAssorbita" ControlToValidate="txtPotenzaAssorbita"
				ErrorMessage="Potenza assorbita: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell ColumnSpan="4" VerticalAlign="Middle" Height="40">
            Se usata torre di raffreddamento o raffreddatore a fluido
        </asp:TableCell>
    </asp:TableRow>
        
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="T uscita fluido (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTUscitaFluido" />
            <asp:RegularExpressionValidator ID="revtxtTUscitaFluido" ControlToValidate="txtTUscitaFluido"
				ErrorMessage="T uscita fluido: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        
        <asp:TableCell>
            <asp:Label runat="server" Text="T bulbo umido aria (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTBulboUmidoAria" />
            <asp:RegularExpressionValidator ID="revtxtTBulboUmidoAria" ControlToValidate="txtTBulboUmidoAria"
				ErrorMessage="T bulbo umido aria: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell ColumnSpan="4" VerticalAlign="Middle" Height="40">
            Se usato scambiatore di calore intermedio
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="T ingresso lato esterno (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTIngressoLatoEsterno" />
            <asp:RegularExpressionValidator ID="revtxtTIngressoLatoEsterno" ControlToValidate="txtTIngressoLatoEsterno"
				ErrorMessage="T ingresso lato esterno: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        
        <asp:TableCell>
            <asp:Label runat="server" Text="T uscita lato esterno (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTUscitaLatoEsterno" />
            <asp:RegularExpressionValidator ID="revtxtTUscitaLatoEsterno" ControlToValidate="txtTUscitaLatoEsterno"
				ErrorMessage="T uscita lato esterno: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell>
            <asp:Label runat="server" Text="T ingresso lato macchina (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTIngressoLatoMacchina" />
            <asp:RegularExpressionValidator ID="revtxtTIngressoLatoMacchina" ControlToValidate="txtTIngressoLatoMacchina"
				ErrorMessage="T ingresso lato macchina: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
        
        <asp:TableCell>
            <asp:Label runat="server" Text="T uscita lato macchina (°C)" />
        </asp:TableCell>
        <asp:TableCell>
            <asp:TextBox runat="server" CssClass="txtClass" ID="txtTUscitaLatoMacchina" />
            <asp:RegularExpressionValidator ID="revtxtTUscitaLatoMacchina" ControlToValidate="txtTUscitaLatoMacchina"
				ErrorMessage="T uscita lato macchina: inserire un valore numerico eventualmente con separatore decimale la virgola" runat="server"
				ValidationExpression="^([0-9]*\,?[0-9]+|[0-9]+\,?[0-9]*)?$" ForeColor="Red" 
				EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
        </asp:TableCell>
    </asp:TableRow>   
</asp:Table>