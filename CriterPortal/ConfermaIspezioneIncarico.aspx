<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConfermaIspezioneIncarico.aspx.cs" Inherits="ConfermaIspezioneIncarico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                         CONFERMA INCARICO TECNICO
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center" ID="rowInfoIncaricoOk" Visible="false" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblIncaricoOk" Font-Bold="true" Text="Incarico confermato con successo" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow HorizontalAlign="Center" ID="rowInfoIncaricoGiaConfermato" Visible="false" runat="server">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Red" ID="lblIncaricoGiaConfermato" Font-Bold="true" Text="Incarico già confermato" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowDettaglioIspezioni" Visible="false">
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <b>Premesso che:</b>
                        <br/><br/>
                        la Società “in house” Art-er Scpa svolge le funzioni di Organismo Regionale di Accreditamento di cui al comma 2 dell’art. 25-ter della L.R. n. 26 del 2004, come specificato dal RR 1/2017 e smi , ivi compresa la realizzazione dei controlli in campo per la verifica della sicurezza ed efficienza degli Impianti Termici;
                        <br/><br/>
                        <asp:Label runat="server" ID="lblIspettore" /> di seguito indicato come ISPETTORE, risulta iscritto nell’elenco dei soggetti qualificati per l’esecuzione delle attività di ispezione di cui al RR 1/2017 art 21
                        <br/><br/>
                        l'Ispettore ha sottoscritto con Art-ER S.cons.p.a. un contratto di prestazione di servizi e consulenza per la
                        esecuzione di attività ispettive finalizzate alla verifica dei criteri di sicurezza ed efficienza degli impianti termici presenti sul territorio della Regione Emilia-Romagna.
                        <br/><br/>
                        tale incarico prevede che l’assegnazione della specifica ispezione da realizzare si concretizzi mediante accettazione da parte dell'Ispettore della relativa segnalazione che gli viene inviata dall’Organismo Regionale di Accreditamento, tramite apposita funzionalità telematica di cui è stato provvisto l’applicativo CRITER
                        <br/><br/>
                        che l'Ispettore ha accettato di eseguire l'ispezione assegnata tramite apposita segnalazione all'Organismo di Accreditamento nelle modalità sopra indicate
                        <br/><br/>
                        <b>Tutto ciò premesso</b>
                        <br/><br/>
                        Art-ER S.cons.p.a. – Organismo Regionale di Accreditamento conferma l'assegnazione delle ispezioni da eseguirsi con le modalità contenute nel contratto quadro di cui all’art 19 del RR 1/2017 e del Disciplinare tecnico delle ispezioni sui seguenti impianti termici:
                        <br/><br/>
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" AllowSorting="false" AllowPaging="false"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDIspezione">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDIspezione" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceIspezione" HeaderText="Codice Ispezione" />
                                <asp:TemplateColumn HeaderText="Responsabile Impianto">
                                    <ItemTemplate>
                                        <%#Eval("NomeResponsabile") %>&nbsp;<%#Eval("CognomeResponsabile") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Terzo Responsabile Impianto">
                                    <ItemTemplate>
                                        <%#Eval("RagioneSocialeTerzoResponsabile") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Indirizzo Impianto">
                                    <ItemTemplate>
                                        <%#Eval("Indirizzo") %>&nbsp;<%#Eval("Civico") %>&nbsp;<%#Eval("Comune") %>&nbsp;(<%#Eval("SiglaProvincia") %>)
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="PotenzaTermicaUtileNominaleKw" HeaderText="Potenza di impianto (kW)" />
                                <asp:BoundColumn DataField="TipologiaCombustibile" HeaderText="Combustibile" />
                                <asp:BoundColumn DataField="DataInstallazione" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data di Installazione" />
                                <asp:BoundColumn DataField="NomeAzienda" HeaderText="Libretto di impianto compilato da" />
                                <asp:BoundColumn DataField="NomeAzienda" HeaderText="Rapporto di controllo compilato da" />
                            </Columns>
                        </asp:DataGrid>
                        <br /><br />
                        <b>
                        L'Ispettore dichiara di non essere nelle condizioni di incompatibilità previste dalla vigente normativa per
                        l’esecuzione dell’ispezione. In particolare, al fine di assicurare indipendenza e imparzialità di giudizio,
                        dichiara l'assenza di conflitto di interessi, tra l'altro espressa attraverso il non coinvolgimento diretto o
                        indiretto nel processo di manutenzione, installazione, progettazione e certificazione dell’impianto termico sottoposto a ispezione, o con i produttori dei materiali e dei componenti in esso incorporati.
                        </b><br/><br/>
                        L'ispettore si impegna inoltre a mantenere la massima riservatezza e a non divulgare, per nessuna ragione,
                        le informazioni concernenti l’Art-ER, l’attività di controllo sulla sicurezza e l’efficienza energetica degli impianti termici, ed ogni altra informazione che lo stesso potrà acquisire nel corso dello svolgimento dell'ispezione.
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowFirmaDigitale" Visible="false">
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        Per confermare l'incarico seguire i seguenti punti:<br />
                        <ul>
                            <li>1) scaricare, cliccando <asp:HyperLink ID="imgExportPdfLetteraIncarico" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="qui" ToolTip="Scarica la lettera di incarico" Target="_blank" />, la lettera di incarico in formato .pdf contenete i dati delle verifiche ispettive;</li>
                            <li>2) salvare il file .pdf sul proprio computer e provvedere a firmarlo digitalmente mediante il software in dotazione con il dispositivo di firma digitale;</li>
                            <li>3) caricare la lettera di incarico firmata digitalmente in formato .p7m selezionando il pulsante Scegli file</li>
                            <li>4) cliccare il pulsante CONFERMA INCARICO TECNICO</li>
                        </ul>
                        <br />
                        <asp:FileUpload ID="UploadFileP7m" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                        <asp:RequiredFieldValidator ID="rfvUploadFileP7m" runat="server"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgIncaricoUploadFile"
                            ErrorMessage="Lettera di Incarico firmata digitalmente .p7m: campo necessario"
                            ControlToValidate="UploadFileP7m">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadFileP7m" runat="server"
                            ErrorMessage="Lettera di incarico firmata digitalmente .p7m non valida"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.p7m|.P7m|.p7M|.P7M)$" ValidationGroup="vgIncaricoUploadFile"
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
                        <asp:Button ID="btnConfermaIncarico" runat="server" ValidationGroup="vgIncaricoUploadFile" TabIndex="1" CssClass="buttonClass" Width="250"
                            OnClick="btnConfermaIncarico_Click" Text="CONFERMA INCARICO TECNICO"
                            OnClientClick="javascript:return confirm('Confermi di firmare l\' incarico tecnico?');"/>
                        <asp:ValidationSummary ID="ValidationSummary1" ValidationGroup="vgIncaricoUploadFile" runat="server" ShowMessageBox="True"
                            ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                 </asp:TableRow>
            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnConfermaIncarico" />
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