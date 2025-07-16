<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_LoginView.ascx.cs" Inherits="WebUserControls_WUC_LoginView" %>

<asp:LoginView ID="LoginViewer" runat="server">
    <AnonymousTemplate>
        <!-- -->
                
        <!-- -->
    </AnonymousTemplate>
    <LoggedInTemplate>
        <asp:Table runat="server" ID="tblLogin">
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Image runat="server" ID="imgUserIcon" ImageUrl="~/images/user-icon.png" ImageAlign="AbsMiddle" ToolTip="Immagine user" AlternateText="Immagine user" />&nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:LoginName ID="LoginName" runat="server" />
                    &nbsp;&nbsp;
                    [&nbsp;<asp:LoginStatus ID="LoginStatus" OnLoggingOut="LoginStatus_LoggingOut" LogoutText="Esci" LogoutAction="RedirectToLoginPage" runat="server" />&nbsp;]
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    &nbsp;
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label runat="server" ID="lblCodiceSoggetto" ForeColor="Green" />
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </LoggedInTemplate>
</asp:LoginView>