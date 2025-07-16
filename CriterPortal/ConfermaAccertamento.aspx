<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConfermaAccertamento.aspx.cs" Inherits="ConfermaAccertamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        window.history.forward();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblConfermaAccertamentoOk" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblConfermaAccertamentoOk" Font-Bold="true" Text="Conferma accertamento inviato con successo" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Width="900" ID="tblConfermaAccertamentoGiaEffettuato" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Red" ID="lblConfermaAccertamentoGiaEffettuato" Font-Bold="true" Text="Conferma accertamento già effettuato o link non valido" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            
            <asp:Table Width="900" ID="tblConfirmaAccertamento" Visible="true" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                          CONFERMA ACCERTAMENTO
                    </asp:TableCell>
                </asp:TableRow> 
                                               
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                         <asp:TextBox Width="890" Height="200" ID="txtRispostaAccertamento" CssClass="txtClass_o" 
                             runat="server" TextMode="MultiLine" ValidationGroup="vgConfermaAccertamento" Rows="6" />
                        <asp:RequiredFieldValidator
                            ID="rfvtxtRispostaAccertamento" ForeColor="Red" runat="server" ValidationGroup="vgConfermaAccertamento" EnableClientScript="true" 
                            ErrorMessage="Conferma accertamento: campo obbligatorio"
                            ControlToValidate="txtRispostaAccertamento">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                         &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnConfermaAccertamento" runat="server" ValidationGroup="vgConfermaAccertamento" TabIndex="1" CssClass="buttonClass" Width="260"
                            OnClick="btnConfermaAccertamento_Click" Text="CONFERMA ACCERTAMENTO" />
                        <asp:ValidationSummary ID="vsConfermaAccertamento" ValidationGroup="vgConfermaAccertamento" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                 </asp:TableRow>
            </asp:Table>
            <!-- -->
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