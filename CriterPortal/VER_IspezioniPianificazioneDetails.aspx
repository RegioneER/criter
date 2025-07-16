<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_IspezioniPianificazioneDetails.aspx.cs" Inherits="VER_IspezioniPianificazioneDetails" %>
<%@ Register Src="~/WebUserControls/WUC_GoogleAutosuggest.ascx" TagPrefix="uc1" TagName="UCGoogleAutosuggest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:Table runat="server" Width="100%">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                 PIANIFICAZIONE VISITA ISPETTIVA
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="300" CssClass="riempimento2">
                <asp:Label runat="server" ID="VER_Accertamenti_lblResponsabile" AssociatedControlID="lblResponsabile" Text="Responsabile" />
            </asp:TableCell>
            <asp:TableCell Width="600" CssClass="riempimento">
                <asp:Label ID="lblResponsabile" Font-Bold="true" runat="server" /><br /><br />
                <uc1:UCGoogleAutosuggest runat="server" ID="UCGoogleAutosuggest" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="290" CssClass="riempimento2">
                <asp:Label runat="server" ID="lblTitoloDataIspezione" AssociatedControlID="txtDataIspezione" Text="Data ispezione (gg/mm/aaaa) (*)" />
            </asp:TableCell>
            <asp:TableCell Width="600" CssClass="riempimento">
                <asp:TextBox runat="server" ID="txtDataIspezione" ValidationGroup="vgPianificazione" Width="90" MaxLength="10" CssClass="txtClass_o" />
                <asp:Label runat="server" ID="lblDataIspezione" />
                <asp:RequiredFieldValidator ID="rfvDataIspezione" ValidationGroup="vgPianificazione" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Data ispezione: campo obbligatorio"
                    ControlToValidate="txtDataIspezione">&nbsp;*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator
                    ID="revDataIspezione" ValidationGroup="vgPianificazione" ControlToValidate="txtDataIspezione" Display="Dynamic" ForeColor="Red" ErrorMessage="Data ispezione: inserire la data nel formato gg/mm/aaaa"
                    runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                    EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                <asp:CustomValidator ID="cvDataIspezione" runat="server" EnableClientScript="true" Display="Dynamic"
                    OnServerValidate="ControllaDataIspezione"
                    CssClass="testoerr"
                    ValidationGroup="vgPianificazione"
                    ErrorMessage="<br/>Attenzione: la data di ispezione non può essere antecedente alla data di firma dell'incarico" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="290" CssClass="riempimento2">
                <asp:Label runat="server" ID="lblTitolotxtDataIspezione" AssociatedControlID="txtDataIspezione" Text="Fascia oraria (*)" />
            </asp:TableCell>
            <asp:TableCell Width="600" CssClass="riempimento">
                da:<asp:DropDownList ID="ddlOrarioDa" Width="130" TabIndex="1" ValidationGroup="vgPianificazione" runat="server" CssClass="selectClass_o" />
                <asp:RequiredFieldValidator ID="rfvddlOrarioDa" ValidationGroup="vgPianificazione" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Orario da: campo obbligatorio"
                    ControlToValidate="ddlOrarioDa">&nbsp;*</asp:RequiredFieldValidator>
                &nbsp;
                        a:<asp:DropDownList ID="ddlOrarioA" Width="130" TabIndex="1" ValidationGroup="vgPianificazione" runat="server" CssClass="selectClass_o" />
                <asp:RequiredFieldValidator ID="rfvddlOrarioA" ValidationGroup="vgPianificazione" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Orario a: campo obbligatorio"
                    ControlToValidate="ddlOrarioA">&nbsp;*</asp:RequiredFieldValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell Width="290" CssClass="riempimento2">
                <asp:Label runat="server" ID="lblTitoloOsservatoreIspezione" AssociatedControlID="txtOsservatoreIspezione" Text="Osservatore" />
            </asp:TableCell>
            <asp:TableCell Width="600" CssClass="riempimento">
                <asp:TextBox runat="server" ID="txtOsservatoreIspezione" Width="300" CssClass="txtClass" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="rowIspezionePagamento1" Visible="false">
            <asp:TableCell Width="290" CssClass="riempimento2">
                <asp:Label runat="server" ID="lblTitolochkApagamento" AssociatedControlID="chkIspezioneAPagamento" Text="Ispezione a pagamento" />
            </asp:TableCell>
            <asp:TableCell Width="600" CssClass="riempimento">
                <asp:CheckBox runat="server" ID="chkIspezioneAPagamento" AutoPostBack="true" OnCheckedChanged="chkIspezioneAPagamento_CheckedChanged" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="rowIspezionePagamento2" Visible="false">
            <asp:TableCell Width="290" CssClass="riempimento2">
                <asp:Label runat="server" ID="lblTitoloImportoIspezione" AssociatedControlID="txtImportoIspezione" Text="Importo ispezione € (*)" />
            </asp:TableCell>
            <asp:TableCell Width="600" CssClass="riempimento">
                <asp:TextBox runat="server" ID="txtImportoIspezione" ValidationGroup="vgPianificazione" Width="100" CssClass="txtClass_o" />
                <asp:RequiredFieldValidator
                    ID="rfvtxtImportoIspezione" ForeColor="Red" runat="server" ValidationGroup="vgPianificazione" EnableClientScript="true" ErrorMessage="Importo ispezione: campo obbligatorio"
                    ControlToValidate="txtImportoIspezione">&nbsp;*</asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revtxtImportoIspezione" ValidationGroup="vgPianificazione" EnableClientScript="true"
                    ControlToValidate="txtImportoIspezione"
                    ErrorMessage="Importo ispezione: inserire importo con la virgola" runat="server"
                    ValidationExpression="^-?[0-9]\d*(,\d+)?$">&nbsp;*</asp:RegularExpressionValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow runat="server" ID="rowStessaData" Visible="false">
            <asp:TableCell Width="290" CssClass="riempimento2">
                <asp:Label runat="server" ID="lblchkStessaData" AssociatedControlID="txtDataIspezione" Text="Pianifica stessa data per tutte le ispezioni" />
            </asp:TableCell>
            <asp:TableCell Width="600" CssClass="riempimento">
                <asp:CheckBox runat="server" ID="chkStessaDataIspezione" />
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
               <asp:CustomValidator ID="cvNormalizeAddress" Enabled="true" runat="server"
                        Display="Dynamic" ValidationGroup="vgPianificazione" EnableClientScript="True" CssClass="testoerr"
                        OnServerValidate="ControllaNormalizeAddress"></asp:CustomValidator>
            </asp:TableCell>
        </asp:TableRow>

        <asp:TableRow>
            <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                <asp:Button runat="server" ID="btnAnnullaPianificazione" Text="ANNULLA" CausesValidation="false" 
                    TabIndex="1" CssClass="buttonClass" OnClick="btnAnnullaPianificazione_Click" Width="200px" />&nbsp;
                <asp:Button runat="server" ID="btnSalvaPianificazione" Text="SALVA PIANIFICAZIONE" OnClick="btnSalvaPianificazione_Click"
                    CssClass="buttonClass" Width="250px" ValidationGroup="vgPianificazione" TabIndex="1" />

                <asp:ValidationSummary ID="vsPianificazione" ValidationGroup="vgPianificazione" runat="server" ShowMessageBox="True"
                    ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</asp:Content>
