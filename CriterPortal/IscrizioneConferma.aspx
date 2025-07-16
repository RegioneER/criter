<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IscrizioneConferma.aspx.cs" Inherits="IscrizioneConferma" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        window.history.forward();
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
             <asp:Table Width="900" ID="tblInfoAziendaIscrizioneOk" Visible="false" CssClass="TableClass" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblIscrizioneOk" Font-Bold="true" Text="Iscrizione al sistema Criter effettuata con successo" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

            <asp:Table Width="900" ID="tblConfirmIscrizione" Visible="true" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                            CONFERMA ISCRIZIONE AL SISTEMA CRITER
                            <asp:Label runat="server" ID="lblIDSoggetto" Visible="false" />
                            <asp:Label runat="server" ID="lblIDTipoSoggetto" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow> 
                <asp:TableRow runat="server" ID="rowFirmaDigitale" Visible="true">
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        Per completare la registrazione al Sistema Criter seguire i seguenti punti:<br />
                        <ul>
                            <li>scaricare, cliccando <asp:HyperLink ID="imgExportPdfIscrizione" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica la scheda di Iscrizione" Target="_blank" />, la scheda di iscrizione in formato .pdf contenete i dati inseriti durante la registrazione;</li>
                            <li>salvare il file .pdf sul proprio computer e provvedere a firmarlo digitalmente mediante il software in dotazione con il dispositivo di firma digitale;</li>
                            <li>caricare la scheda di iscrizione firmata digitalmente in formato .p7m selezionando il pulsante Scegli file</li>
                            <li>selezionare il pulsante CONFERMA ISCRIZIONE</li>
                        </ul>
                        <br />
                        <asp:FileUpload ID="UploadFileP7m" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                        <asp:RequiredFieldValidator ID="rfvUploadFileP7m" runat="server"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgAnagraficaUploadFile"
                            ErrorMessage="Documento di iscrizione firmato digitalmente .p7m: campo necessario"
                            ControlToValidate="UploadFileP7m">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadFileP7m" runat="server"
                            ErrorMessage="Documento di iscrizione firmato digitalmente .p7m non valido"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.p7m|.P7m|.p7M|.P7M)$" ValidationGroup="vgAnagraficaUploadFile"
                            ControlToValidate="UploadFileP7m"></asp:RegularExpressionValidator>
                        <br />
                        <asp:Label runat="server" ForeColor="Green" ID="lblCheckP7m" Font-Bold="true" />
                        <br /><br />
                        Nel caso in cui si desideri completare la registrazione in un secondo momento perché in assenza ad esempio del dispositivo di firma digitale clicca su questo pulsante&nbsp;&nbsp;<asp:ImageButton ID="imgInviaLinkIscrizione" OnClick="imgInviaLinkIscrizione_Click" Width="20" Height="20" ImageUrl="~/images/buttons/email.png" runat="server" AlternateText="Invia link per completare l'iscrizione successivamente" BorderStyle="None" ToolTip="Invia link per completare l'iscrizione successivamente" OnClientClick="javascript:return confirm('Confermi di voler ricevere il link per completare l\'iscrizione successivamente?');" TabIndex="1"/>.
                        Cliccando sul pulsante il sistema invierà una e-mail contenente un link all’indirizzo di posta elettronica indicato nel form di iscrizione.
                        Collegandosi al link riportato nella e-mail ricevuta l’utente verrà ri-direzionato su una pagina dove potrà nuovamente scaricare la scheda di iscrizione in formato .pdf, provvedere a firmarla digitalmente e caricarla portando a compimento la registrazione.
                        Altrimenti, portare a compimento la procedura sopra descritta seguendo i punti elencati.
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowSpid" Visible="false">
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        Per confermare la registrazione cliccare sul pulsante CONFERMA ISCRIZIONE<br /><br />
                        <asp:Image runat="server" ID="imgSpid" ImageUrl="~/images/spid-logo-b-lb.png" BorderStyle="None" AlternateText="Autenticato con SPID" ToolTip="Autenticato con SPID" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                         &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnConfermaIscrizione" runat="server" ValidationGroup="vgAnagraficaUploadFile" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnConfermaIscrizione_Click" Text="CONFERMA ISCRIZIONE" />
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="vgAnagraficaUploadFile" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                 </asp:TableRow>
            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConfermaIscrizione" />
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