<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CheckCriterPrivacy.aspx.cs" Inherits="CheckCriterPrivacy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
            <ContentTemplate>               
                  <asp:Table runat="server" ID="tblPrivacy" Width="100%">
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                    DESIGNAZIONE RESPONSABILE ESTERNO DEL TRATTAMENTO DEI DATI PERSONALI EX ART. 28 REG. UE 2016/679 NELL’AMBITO DELLA GESTIONE DEL CATASTO REGIONALE E DEL SISTEMA DI CONTROLLO DEGLI IMPIANTI TERMICI (CRITER)
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                    Nell’ambito delle funzioni conferite dalla Regione Emilia-Romagna ad ART-ER per la gestione del CRITER è ricompreso il ruolo di Responsabile del Trattamento dei dati ai sensi e per gli effetti dell’art.28 Reg. Ue 2016/679. In tale qualità, pertanto, ART-ER tratta i dati personali registrati tramite l’applicativo informatico CRITER – Catasto Regionale degli Impianti Termici, accessibile per via telematica all’indirizzo http://energia.regione.emilia-romagna.it/criter/catasto-impianti 
                                    Le funzioni di elaborazione e registrazione dei Libretti di Impianto e dei Rapporti di Controllo Tecnico sono svolte da imprese di installazione e manutenzione accreditati per poter operare nell’ambito del sistema CRITER. Nello svolgimento di tali funzioni, le imprese di installazione e manutenzione acquisiscono, trattano e trasferiscono nel sistema CRITER dati personali comuni degli Utenti – Responsabili di Impianto, dati di cui la Regione Emilia-Romagna risulta essere Titolare del Trattamento ed ART-ER risulta essere Responsabile del Trattamento. 
                                    Il trattamento dei dati personali degli Utenti finali del servizio da parte delle imprese di installazione e manutenzione accreditate da ART-ER, effettuato tramite l’applicativo informatico CRITER risulta necessario ai fini della registrazione obbligatoria ai sensi di legge dei Libretti di Impianto e dei Rapporti di Controllo Tecnico. 
                                    In conseguenza di tale sistema di relazioni, l’accreditamento da parte di ART-ER delle imprese di installazione e manutenzione comporta la individuazione e la nomina di queste ultime come sub-responsabili del trattamento dei dati personali comuni degli Utenti finali acquisiti nell’ambito del servizio di registrazione obbligatoria dei Libretti di Impianto e dei Rapporti di Controllo Tecnico nel CRITER. 
                                    <br /><br />
                                    Tentativi rimanenti prima che non sia più possibile inserire un nuovo RCT: <asp:Label runat="server" ID="lblCountRimanenti" Font-Bold="true" />
                                </asp:TableCell>
                            </asp:TableRow>
                            
                            <asp:TableRow runat="server" ID="rowSpid" Visible="false">
                                <asp:TableCell ColumnSpan="2" Width="900" CssClass="riempimento">
                                   <asp:Image runat="server" ID="imgSpid" ImageUrl="~/images/spid-logo-b-lb.png" BorderStyle="None" AlternateText="Autenticato con SPID" ToolTip="Autenticato con SPID" />
                                    <br /><br />
                                    E' necessario prendere visione del documento cliccando <asp:HyperLink ID="imgExportDocument1" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica il documento" Target="_blank" />.<br />
                                    Fare click sul pulsante FIRMA DOCUMENTO.
                                </asp:TableCell>
                            </asp:TableRow>

                            <asp:TableRow runat="server" ID="rowFirmaDigitale" Visible="false">
                                <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                                    Seguire i seguenti punti:<br />
                                    <ul>
                                        <li>scaricare il documento, cliccando
                                            <asp:HyperLink ID="imgExportDocument" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica il documento" Target="_blank" />;
                                        </li>
                                        <li>salvare il file .pdf sul proprio computer e provvedere a firmarlo digitalmente mediante il software in dotazione con il dispositivo di firma digitale;</li>
                                        <li>caricare il documento firmato digitalmente in formato .p7m selezionando il pulsante Scegli file</li>
                                        <li>selezionare il pulsante FIRMA DOCUMENTO</li>
                                    </ul>
                                    <br />
                                    <asp:FileUpload ID="UploadFileP7m" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                                    <asp:RequiredFieldValidator ID="rfvUploadFileP7m" runat="server" ValidationGroup="vgDocumentoUploadFile"
                                        EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" 
                                        ErrorMessage="Documento firmato digitalmente .p7m: campo necessario"
                                        ControlToValidate="UploadFileP7m">&nbsp;*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revUploadFileP7m" runat="server" ValidationGroup="vgDocumentoUploadFile"
                                        ErrorMessage="Documento firmato digitalmente .p7m non valido"
                                        EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                                        ValidationExpression="^.+(.p7m|.P7m|.p7M|.P7M)$" 
                                        ControlToValidate="UploadFileP7m" />
                                    <asp:Label runat="server" ForeColor="Green" ID="lblCheckP7m" Font-Bold="true" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento">
                                    <asp:Button runat="server" ID="btnAnnulla" Text="ANNULLA" OnClick="btnCancel_Click"
                                        CssClass="buttonClass" Width="350px" />
                                    &nbsp;
                                    <asp:Button runat="server" ID="btnFirma" ValidationGroup="vgDocumentoUploadFile" Text="FIRMA DOCUMENTO" OnClick="btnFirma_Click"
                                        OnClientClick="javascript:return(confirm('Confermi di voler inviare il documento firmato?'));"
                                        CssClass="buttonClass" Width="350px" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
            </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFirma" />
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

