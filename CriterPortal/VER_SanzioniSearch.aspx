<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_SanzioniSearch.aspx.cs" Inherits="VER_SanzioniSearch" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            
            <asp:Table runat="server" ID="tblAccertamenti" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA NOTIFICHE SANZIONI
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="row2">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row3">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceAccertamento" AssociatedControlID="txtCodiceTargatura" Text="Codice accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceAccertamento" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row4">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloStatoSanzione" AssociatedControlID="rblStatoSanzione" Text="Stato sanzione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoSanzione" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row6">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row7">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA NOTIFICHE SANZIONI" OnClick="btnRicerca_Click" 
                            CssClass="buttonClass" Width="300px" TabIndex="1" />&nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row8">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDAccertamento"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceSanzione" HeaderText="Cod.sanzione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="StatoAccertamentoSanzione" HeaderText="Stato sanzione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceAccertamento" HeaderText="Cod.accertamento" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceIspezione" HeaderText="Cod.ispezione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="Ispettore" HeaderText="Ispettore" Visible="true" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderText="Responsabile">
                                    <ItemTemplate>
                                        <%#Eval("NomeResponsabile") %>&nbsp;<%#Eval("CognomeResponsabile") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CodiceTargatura" HeaderText="Cod.targatura" Visible="true" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderText="Generatore">
                                    <ItemTemplate>
                                        <%#Eval("Prefisso") %>&nbsp;<%#Eval("CodiceProgressivo") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgView" ToolTip="Visualizza dettaglio Sanzione" AlternateText="Visualizza dettaglio Sanzione" 
                                            ImageUrl="~/images/Buttons/viewSmall.png" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
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