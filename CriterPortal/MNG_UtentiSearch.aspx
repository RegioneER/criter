<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="MNG_UtentiSearch.aspx.cs" Inherits="MNG_UtentiSearch" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="contentDisplay">
    <asp:UpdatePanel runat="server" ID="Update1">
        <ContentTemplate>
            <asp:Table runat="server" CssClass="TableClass" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        <asp:Label runat="server" Text="RICERCA UTENTI"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="Gruppo" />
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:DropDownList runat="server" ID="ddlUserRole" CssClass="selectClass" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="Username"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtUserName" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="Soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtSoggetto" Width="300" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_AnagraficaSearch_lblTitoloCodiceSoggetto" Text="Codice soggetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceSoggetto" TabIndex="1" Width="200" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="Utenza attiva (si/no)"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkUtenzaAttiva" AutoPostBack="true" Checked="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass=" riempimento2">
                        <asp:Label runat="server" Text="Utenza Bloccata" />
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblBloccati">
                            <asp:ListItem Text="Visualizza tutti gli utenti" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Visualizza solo gli utenti non bloccati" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Visualizza solo utenti bloccati" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass=" riempimento2">
                        <asp:Label runat="server" Text="Tipo di iscrizione" />
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblTipoIscrizione">
                            <asp:ListItem Text="Visualizza tutti gli utenti" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Visualizza solo gli utenti iscritti con Firma Digitale" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Visualizza solo utenti iscritti con Spid" Value="2"></asp:ListItem>
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicercaUtenti" Text="RICERCA UTENTI" OnClick="btnRicercaUtenti_Click"
                            CssClass="buttonClass" Width="200px" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid Width="100%" runat="server" ID="dgUtenti" AllowPaging="true" AutoGenerateColumns="false" CssClass="Grid"
                            PageSize="10" OnItemDataBound="dgUtenti_ItemDataBound" CellSpacing="1" CellPadding="3"
                            OnPageIndexChanged="dgUtenti_PageIndexChanged">
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                            <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" ForeColor="Black" />
                            <ItemStyle CssClass="GridItem" HorizontalAlign="Center" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDUtente" Visible="false" />
                                <asp:BoundColumn DataField="IDSoggetto" Visible="false" />
                                <asp:BoundColumn DataField="fAttivoUser" Visible="false" />
                                <asp:TemplateColumn HeaderText="Tipo">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgSpid" ImageUrl='<%# DataUtilityCore.UtilityApp.ToImageSpid(bool.Parse(DataBinder.Eval(Container.DataItem,"fSpid").ToString())) %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Soggetto" ItemStyle-Font-Size="XX-Small" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUtente" runat="server" Text='<%# Bind("Utente") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Username" ItemStyle-Font-Size="XX-Small" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        Username:<asp:Label ID="lblUsername" runat="server" Text='<%# Bind("Username") %>' /><br />
                                        Password:<asp:Label ID="lblPassword" runat="server" Text='<%# Bind("Password") %>' /><br /><br />
                                        <asp:Label ID="lblKeyApi" runat="server" ForeColor="Green" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Ruolo" HeaderText="Gruppo" ItemStyle-Font-Size="XX-Small" />
                                <asp:TemplateColumn HeaderText="Attivo">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgAttivo" ImageUrl='<%# DataUtilityCore.UtilityApp.ToImage(bool.Parse(DataBinder.Eval(Container.DataItem,"fAttivoUser").ToString())) %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DataUltimaModificaPassword" ItemStyle-Font-Size="XX-Small" DataFormatString="{0:d}" HeaderText="Data ult. mod."></asp:BoundColumn>
                                <asp:BoundColumn DataField="DataScadenzaPassword" ItemStyle-Font-Size="XX-Small" DataFormatString="{0:d}" HeaderText="Data scad. pass."></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Bloccato" ItemStyle-Font-Size="XX-Small">
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgBloccato" ImageUrl='<%# DataUtilityCore.UtilityApp.ToImage(bool.Parse(DataBinder.Eval(Container.DataItem,"fBloccato").ToString())) %>' /><br />
                                        Nr. tentativi falliti&nbsp;<asp:Label runat="server" ID="lblNrTentativiFalliti" Text='<%# Bind("NrTentativiFalliti") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="btnEdit" AlternateText="Modifica Utente" ToolTip="Modifica Utente" OnClick="btnEdit_Click" ImageUrl="~/images/Buttons/edit.png" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="btnAttiva" AlternateText="Attiva Utente ed invia email" OnClientClick="javascript:return confirm('Confermi di inviare la email di attivazione?');" ToolTip="Attiva Utente ed invia email" OnClick="btnAttiva_Click" ImageUrl="~/images/Buttons/email.png" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="KeyApi" Visible="false" ReadOnly="True" />
                            </Columns>
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
