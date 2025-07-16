<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConfermaIspezione.aspx.cs" Inherits="ConfermaIspezione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="js/DisableButton.js"></script>
    <script type="text/javascript">
        window.history.forward();
        
        function confermaAccettazione() {
            var res = confirm('Confermi di accettare di effettuare l\'ispezione?');
            if (res)
            {
                setTimeout("disableBtn('<%=btnConfermaVerifica.ClientID %>');", 500);                
            }
            return res;
        }

        function confermaRifiuto() {
            var res = confirm('Confermi di non accettare di effettuare l\'ispezione?');
            if (res)
            {
                setTimeout("disableBtn('<%=btnConfermaVerifica.ClientID %>');", 500);
            }
            return res;
        }

        function confermaRifiutoConflittoInteressi() {
            var res = confirm('Confermi di non accettare di effettuare l\'ispezione per conflitto di interessi?');
            if (res) {
                setTimeout("disableBtn('<%=btnConfermaVerifica.ClientID %>');", 500);
            }
            return res;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblDettagliVisistaIspettiva" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento1">
                         ACCETTAZIONE A SVOLGERE LA VISITA ISPETTIVA N. <asp:Label runat="server" ID="lblIDVisitaIspettiva" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center" ID="rowInfoVisitaOk" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblVisitaOk" Font-Bold="true" Text="Visita ispettiva confermata con successo" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center" ID="rowInfoVisitaRifiutata" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblVisitaRifiutata" Font-Bold="true" Text="Visita ispettiva rifiutata con successo" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center" ID="rowInfoAccessoNegato" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Red" ID="lblVerificaGiaAssegnata" Font-Bold="true" Text="La Visita ispettiva non è più disponibile" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow HorizontalAlign="Center" ID="rowInfoErroreRichiestaPagina" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Red" ID="lblErroreRichiesta" Font-Bold="true" Text="Errore nella richiesta della pagina" />
                    </asp:TableCell>
                </asp:TableRow>
               
                <asp:TableRow runat="server" ID="rowDettaglioVerifica0" Visible="false">
                    <asp:TableCell ColumnSpan="4" Width="900" CssClass="riempimento">
                        <asp:Label runat="server" ID="lblIspettore" /><br />
                        l'Organismo di Accreditamento Regionale richiede la sua disponibilità per una serie di ispezioni sugli impianti termici censiti all’interno del CRITER<br />
                        Gli impianti per i quali è richiesta la disponibilità sono i seguenti:<br /><br />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="1" UseAccessibleHeader="true" PageSize="100" AllowSorting="false" AllowPaging="false"
                            AutoGenerateColumns="False" Font-Size="XX-Small" runat="server" DataKeyField="IDIspezione">
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
                                <asp:TemplateColumn HeaderText="Tipologia generatore">
                                    <ItemTemplate>
                                        <%#Eval("Prefisso") %>&nbsp;<%#Eval("CodiceProgressivo") %> / 
                                        <%#Eval("TipologiaGruppiTermici") %>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="DataInstallazione" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data di Installazione" />
                                <asp:BoundColumn DataField="NomeAzienda" HeaderText="Libretto di impianto compilato da" />
                                <asp:BoundColumn DataField="NomeAzienda" HeaderText="Rapporto di controllo compilato da" />
                                <asp:BoundColumn DataField="CompensoIspezione" HeaderText="Compenso ispezione" />
                            </Columns>
                        </asp:DataGrid><br /><br />

                        Con riferimento al compenso indicato in tabella, si ricorda che l'ispezione non avvenuta per indisponibilità del responsabile di impianto viene remunerata con un importo di <b>euro 35</b>. 
                        <br /><br />
                        <asp:Label runat="server" ID="lblIspettore1" /><br />
                            1)	di essere disponibile a svolgere la verifica ispettiva nel quadro delle condizioni contrattuali, pertanto provvederò a sottoscrivere la relativa lettera di incarico che mi verrà inviata dall'Organismo di Ispezione,<br />
                            2)	di essere nelle condizioni di indipendenza ed imparzialità previste dall'art.21 comma 8 del Regolamento regionale n° 1/2017 e smi
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowDettaglioVerifica1" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowDettaglioVerifica2" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:RadioButtonList runat="server" RepeatDirection="Horizontal"  OnSelectedIndexChanged="TipoAccettazioneVerifica_OnSelectedIndexChanged" AutoPostBack="true" ID="rblSceltaAccettazione" >
                            <asp:ListItem Selected="True" Text="Accetto" Value="S" />
                            <asp:ListItem Text="Non accetto" Value="N" />
                            <asp:ListItem Text="Non accetto per conflitto di interesse" Value="NCI" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowDettaglioVerifica3" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowDettaglioVerifica4" runat="server" Visible="false">
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:ValidationSummary ID="vs" runat="server" ShowMessageBox="True" ShowSummary="False"
                            HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                        <asp:Button CssClass="buttonClass" Width="350" OnClick="SendConfermaVerifica_Click" runat="server" ID="btnConfermaVerifica" 
                            Text="Conferma accettazione ispezione" OnClientClick="javascript:return confermaAccettazione();" />
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