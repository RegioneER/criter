<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConfermaIspezioneRVI.aspx.cs" Inherits="ConfermaIspezioneRVI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                         CONFERMA RAPPORTO DI VERIFICA ISPETTIVA
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center" ID="rowInfoIncaricoOk" Visible="false" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblRVIOk" Font-Bold="true" Text="Rapporto di verifica ispettiva confermato con successo" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowFirmaDigitale" Visible="false">
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        Per confermare il rapporto di verifica ispettiva seguire i seguenti punti:<br />
                        <ul>
                            <li>1) recuperare il rapporto di verifica ispettiva generato dal sistema a seguito della compilazione dei dati della verifica ispettiva in formato .pdf;</li>
                            <li>2) salvare il file .pdf sul proprio computer e provvedere a firmarlo digitalmente mediante il software in dotazione con l'idoneo dispositivo;</li>
                            <li>3) caricare il rapporto di verifica ispettiva firmato digitalmente in formato .p7m selezionando il pulsante Scegli file</li>
                            <li>4) cliccare il pulsante CONCLUDI ISPEZIONE ED INVIA A COORDINATORE</li>
                        </ul>
                        <br />
                        <asp:FileUpload ID="UploadFileP7m" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                        <asp:RequiredFieldValidator ID="rfvUploadFileP7m" runat="server"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgRVIUploadFile"
                            ErrorMessage="Rapporto di verifica ispettiva firmato digitalmente .p7m: campo necessario"
                            ControlToValidate="UploadFileP7m">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadFileP7m" runat="server"
                            ErrorMessage="Rapporto di verifica ispettiva firmato digitalmente .p7m non valida"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.p7m|.P7m|.p7M|.P7M)$" ValidationGroup="vgRVIUploadFile"
                            ControlToValidate="UploadFileP7m"></asp:RegularExpressionValidator>
                        <br />
                        <asp:Label runat="server" ForeColor="Green" ID="lblCheckP7m" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowFirmaDigitale1" Visible="false">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                         &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowFirmaDigitale2" Visible="false">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnAnnulla" runat="server" TabIndex="1" CssClass="buttonClass" Width="330"
                            OnClick="btnAnnulla_Click" Text="ANNULLA" />&nbsp;
                        <asp:Button ID="btnConfermaRVI" runat="server" TabIndex="1" CssClass="buttonClass" Width="330"
                            OnClick="btnConfermaRVI_Click" ValidationGroup="vgRVIUploadFile" 
                            OnClientClick="javascript:return confirm('Confermi di firmare il rapporto di verifica ispettiva e concludere l\'Ispezione?');" Text="CONCLUDI ISPEZIONE ED INVIA A COORDINATORE" />

                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="vgRVIUploadFile" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                 </asp:TableRow>
            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConfermaRVI" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>