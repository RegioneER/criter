<%@ Page Title="Portafoglio Azienda" Language="C#" AutoEventWireup="true" CodeFile="MNG_PortafoglioSaldo.aspx.cs" Inherits="MNG_PortafoglioSaldo"  MasterPageFile="~/MasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblPortafoglio" CssClass="TableClass" runat="server" >
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        SALDO PORTAFOGLIO AZIENDA
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAziende" AssociatedControlID="cmbAziende" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento" HorizontalAlign="Left">
                        <dx:ASPxComboBox runat="server" ID="cmbAziende" TabIndex="1"
                            Theme="Default"
                            AutoPostBack="true"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1} {2}" 
                            BorderBottom-BorderStyle="None"
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
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="riempimento1" Width="300">
                        <asp:Label runat="server" ID="lblSaldoPortafoglio" Text="Saldo portafoglio" Font-Bold="false" />
                    </asp:TableCell>
                    <asp:TableCell CssClass="riempimento">
                        <asp:Label runat="server" ID="SaldoPortafoglio"/>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblNumeroBolliniDisponbili" Text="Numero bollini disponibili"  Font-Bold="false"/>
                    </asp:TableCell>
                    <asp:TableCell CssClass="riempimento">
                        <asp:Label runat="server" ID="NumeroBolliniDisponibili" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblNumeroBolliniutulizzati" Text="Numero bollini utilizzati"  Font-Bold="false"/>
                    </asp:TableCell>
                    <asp:TableCell CssClass="riempimento">
                        <asp:Label runat="server" ID="NumeroBolliniUtilizzati" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>         
            <!-- -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <br />
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    </asp:Content>
