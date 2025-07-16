<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_SanzioniValidazione.aspx.cs" Inherits="VER_SanzioniValidazione" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoValidaOk" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblValidaOk" Font-Bold="true" Text="Sanzioni validate con successo!" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table runat="server" ID="tblAccertamenti" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        NOTIFICHE SANZIONI DA VALIDARE
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

                <asp:TableRow runat="server" ID="row6">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row7">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA NOTIFICHE SANZIONI" OnClick="btnRicerca_Click" 
                            CssClass="buttonClass" Width="300px" TabIndex="1" />&nbsp;
                        <asp:Button runat="server" ID="btnValidaSanzioni" Visible="false" Text="VALIDA SANZIONI SELEZIONATE" OnClick="btnValidaSanzioni_Click"
                            OnClientClick="javascript:return(confirm('Confermi la validazione delle Sanzioni selezionate?  Effettuando questa operazione la sanzione cambierà di stato in validata e verrà inviata un Atto Giudiziario tramite Poste Italiane al Responsabile di impianto. Confermi?'));"
                            CssClass="buttonClass" Width="350px" />
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
                                <asp:BoundColumn DataField="TipoAccertamento" HeaderText="Tipo Sanzione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceSanzione" HeaderText="Cod.sanzione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceAccertamento" HeaderText="Cod.accertamento" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceIspezione" HeaderText="Cod.ispezione" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="Ispettore" HeaderText="Ispettore" Visible="true" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceTargatura" HeaderText="Cod.targatura" Visible="true" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderText="Generatore">
                                    <ItemTemplate>
                                        <%#Eval("Prefisso") %>&nbsp;<%#Eval("CodiceProgressivo") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgView" ToolTip="Visualizza dettaglio" AlternateText="Visualizza dettaglio" 
                                            ImageUrl="~/images/Buttons/viewSmall.png" TabIndex="1"
                                             />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <%--<asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton OnCommand="RowCommand" ImageUrl="~/images/Buttons/saveSmall.png" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamento") %>' 
                                            CommandName="Validate"
                                            runat="server" ID="imgFlagAttivo" AlternateText="Valida Notifica Sanzione" 
                                            ToolTip="Valida Notifica Sanzione" OnClientClick="javascript:return confirm('Confermi di validare la notifica di sanzione?');" 
                                            TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>--%>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderStyle-HorizontalAlign="Center" HeaderText="Firma">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezioneAll" AutoPostBack="true" OnCheckedChanged="chkSelezioneAll_CheckedChanged" ToolTip="Seleziona tutte le sanzioni per la validazione" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" AutoPostBack="true" OnCheckedChanged="chkSelezione_CheckedChanged" TabIndex="1" />
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