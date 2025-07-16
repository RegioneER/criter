<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="MNG_Utenti.aspx.cs" Inherits="MNG_Utenti" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content runat="server" ContentPlaceHolderID="contentDisplay">
    <asp:UpdatePanel runat="server" ID="Upadate1">
        <ContentTemplate>
            <asp:Table runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento1">
                        <asp:Label runat="server" Text="DATI UTENTE"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="Gruppo" />
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:DropDownList runat="server" ID="ddlUserRole" CssClass="selectClass_o" />
                        <asp:RequiredFieldValidator ID="rfvddlUserRole" ValidationGroup="vgUtenti" ForeColor="Red" runat="server" InitialValue="0" ErrorMessage="Gruppo: campo obbligatorio"
                            ControlToValidate="ddlUserRole">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="UserName"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtUsername" CssClass="txtClass_o" />
                        <asp:CustomValidator ID="cvUsernamePresente" runat="server" ForeColor="Red" ValidationGroup="vgUtenti" EnableClientScript="true" OnServerValidate="ControllaUsernamePresente" 
                                ErrorMessage="Username già presente nel sistema Criter<br/>" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtUsername" ForeColor="Red" runat="server" ValidationGroup="vgUtenti" EnableClientScript="true" ErrorMessage="Username: campo obbligatorio"
                            ControlToValidate="txtUsername">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="Attivo (si/no)"></asp:Label>
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkAttivo" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="35%" CssClass="riempimento2">
                        <asp:Label runat="server" Text="Bloccato (si/no)" />
                    </asp:TableCell>
                    <asp:TableCell Width="65%" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkBloccato" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
							   &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnAnnulla" Text="ANNULLA" CausesValidation="false" CssClass="buttonClass" OnClick="btnAnnulla_Click" Width="200px" />&nbsp;
                        <asp:Button runat="server" ID="btnSalva" Text="SALVA" CssClass="buttonClass" OnClick="btnSalva_Click" Width="200px" />
                        <asp:ValidationSummary ID="vsUtenti" ValidationGroup="vgUtenti" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
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