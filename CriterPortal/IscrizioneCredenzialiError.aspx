<%@ Page Title="Criter - Errore conferma credenziali" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IscrizioneCredenzialiError.aspx.cs" Inherits="IscrizioneCredenzialiError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Table Width="900" ID="tblInfoConfermaCredenziali" CssClass="TableClass" runat="server">
                    <asp:TableRow HorizontalAlign="Center">
                        <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
                            <asp:Label runat="server" ForeColor="Red" ID="lblIscrizioneOk" Font-Bold="true" Text="Attenzione l'operazione richiesta è stata già effettuata!" />
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

