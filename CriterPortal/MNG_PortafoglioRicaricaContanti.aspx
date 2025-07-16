<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MNG_PortafoglioRicaricaContanti.aspx.cs" Inherits="MNG_PortafoglioRicaricaContanti" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICARICA CON CONTANTI PORTAFOGLIO AZIENDA
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1} {2}" BorderBottom-BorderStyle="None"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="CodiceSoggetto" Caption="Codice" Width="50" />
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Azienda" Width="200" />
                                <dx:ListBoxColumn FieldName="IndirizzoSoggetto" Caption="Indirizzo" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator
                            ID="rfvASPxComboBox1"
                            ValidationGroup="vgContanti"
                            ForeColor="Red"
                            Display="Dynamic"
                            runat="server"
                            InitialValue=""
                            ErrorMessage="Azienda: campo obbligatorio"
                            ControlToValidate="ASPxComboBox1">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloImporto" AssociatedControlID="txtImportoContanti" Text="Importo (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtImportoContanti" ValidationGroup="vgContanti" CssClass="txtClass" runat="server" Width="100" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtImportoContanti" ForeColor="Red" runat="server" ValidationGroup="vgContanti" EnableClientScript="true"
                            ErrorMessage="Importo contanti: campo obbligatorio"
                            ControlToValidate="txtImportoContanti">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revtxtImportoContanti" ValidationGroup="vgContanti" ControlToValidate="txtImportoContanti"
                            runat="server" ErrorMessage="Importo contanti: campo non valido" ValidationExpression="[-+]?[0-9]*\,?[0-9]+"
                            ForeColor="Red">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataVersamento" AssociatedControlID="txtDataVersamento" Text="Data versamento (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox ID="txtDataVersamento" runat="server" CssClass="txtClass" ValidationGroup="vgContanti" Width="90" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtDataVersamento" ForeColor="Red" runat="server" ValidationGroup="vgContanti" EnableClientScript="true"
                            ErrorMessage="Data versamento: campo obbligatorio"
                            ControlToValidate="txtDataVersamento">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator
                            ID="revtxtDataVersamento" ValidationGroup="vgContanti" ControlToValidate="txtDataVersamento" Display="Dynamic" ForeColor="Red" ErrorMessage="Data versamento: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="cvDataVersamento" Display="Dynamic" ForeColor="Red" EnableClientScript="true" 
                            ValidationGroup="vgContanti" Operator="GreaterThanEqual" type="Date" 
                            ControlToValidate="txtDataVersamento" ErrorMessage="Data versamento: deve essere non precedente agli ultimi 30 giorni." runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDisattivaControlloDate" AssociatedControlID="chkDisableValidationDate" Text="Disabilita validazione date" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBox ID="chkDisableValidationDate" runat="server" AutoPostBack="True" OnCheckedChanged="chkDisableValidationDate_OnCheckedChanged" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnContanti" runat="server" Text="RICARICA PORTAFOGLIO" Width="250" CausesValidation="True" ValidationGroup="vgContanti" OnClick="btnContanti_Click" CssClass="buttonClass" OnClientClick="javascript:return confirm('Confermi di ricaricare il portafoglio tramite i contanti?');" />
                        <asp:ValidationSummary ID="vsDatiContanti" ValidationGroup="vgContanti" runat="server" CssClass="testoerr" ShowMessageBox="True"
                             ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>