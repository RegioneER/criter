<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LIM_LibrettiImpiantiDismissione.aspx.cs" Inherits="LIM_LibrettiImpiantiDismissione" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://wwwservizi.regione.emilia-romagna.it/includes/TemplatesER/styles.css" />
    <link rel="stylesheet" href="https://cm.regione.emilia-romagna.it/energia/er_base.css" />
    <link rel="stylesheet" href="https://cm.regione.emilia-romagna.it/energia/er_colors.css" />
    <link rel="stylesheet" href="https://cm.regione.emilia-romagna.it/energia/++resource++rer.aree_tematiche.stylesheets/aree_tematiche.css" />
    <link rel="stylesheet" href="https://cm.regione.emilia-romagna.it/energia/er_energia.css" />
    <link rel="stylesheet" href="https://cm.regione.emilia-romagna.it/energia/ploneCustom.css" />
    <link id="Link1" rel="stylesheet" runat="server" href="~/Content/StyleCustom.css" media="screen" />
    <link id="Link2" rel="shortcut icon" runat="server" type="image/x-icon" href="http://energia.regione.emilia-romagna.it/favicon.ico" />
    <link id="Link3" rel="apple-touch-icon" runat="server" href="http://energia.regione.emilia-romagna.it/touch_icon.png" />
    <link rel="search" href="http://energia.regione.emilia-romagna.it/@@search" title="Cerca nel sito" />

    <script type="text/javascript" src="Scripts/jquery-3.1.1.js"></script>
    <script type="text/javascript" src="Scripts/jquery-accessibleMegaMenu.js"></script>
    <script type="text/javascript" src="Scripts/jquery.hoverIntent.minified.js"></script>
    <script type="text/javascript" src="Scripts/jquery.dhtmlwindow.js"></script>
    <script type="text/javascript" src="Scripts/jquery.MultiFile.js"></script>
    <script type="text/javascript" src="Scripts/DisableButton.js"></script>
    <script type="text/javascript" src="Scripts/SiCommonUtility.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="smContent" EnableScriptGlobalization="true" AsyncPostBackTimeout="360000"
            EnablePartialRendering="true" EnablePageMethods="true" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Table Width="900" ID="tblDismissioni" runat="server">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell HorizontalAlign="Left" CssClass="riempimento1">
                            LISTA GENERATORI
                        </asp:TableHeaderCell>
                    </asp:TableHeaderRow>

                    <asp:TableRow>
                        <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                            In questa sezione viene descritto come comunicare la dismissione del generatore dell’impianto termico.
                            Si specifica che tale comunicazione è obbligatoria per gli impianti termici posti nella condizione di non poter funzionare, quali ad esempio gli impianti che sono stati scollegati dalla rete di distribuzione dell’energia o a serbatoi di combustibili o comunque privi di approvvigionamento. Ai sensi della normativa vigente la comunicazione di dismissione è un onere posto a capo del responsabile di impianto: a tal fine, il responsabile o, ove delegato, il terzo responsabile può accedere al sistema informativo CRITER ed operare le funzioni ivi previste limitatamente ai dati ed all’impianto di propria competenza. Se debitamente incaricati, all'aggiornamento di tali dati possono altresì provvedere gli operatori (installatori e manutentori).<br /><br />

                            <b>Per effettuare la richiesta di dismissione del generatore/i seguire i seguenti punti:</b><br />
                             <ul>
                                 <li>1) facendo riferimento al generatore per il quale si vuole si vuole comunicare la dismissione selezionare il pulsante “Richiesta dismissione” scaricando il documento di richiesta di dismissione del generatore in formato .pdf;</li>
                                 <li>2) procedere alla compilazione del documento. Scansionare il documento completo degli allegati obbligatori richiesti (non sono ammesse foto del documento);</li>
                                 <li>3) inviare il documento scansionato completo di allegati obbligatori richiesti al seguente indirizzo PEC: mailto:criter.art-er@pec.it o in alternativa in assenza di indirizzo PEC è possibile inviare il documento al seguente indirizzo di mail ordinaria: mailto:criter@art-er.it con il seguente oggetto: COMUNICAZIONE DISMISSIONE GENERATORE (specificare il codice targatura impianto).</li>
                                 <li>4) una volta inviata la richiesta l’Organismo di accreditamento ed ispezione provvederà a dismettere il/i generatore/i oggetto della comunicazione pervenuta.</li>
                             </ul>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server" ID="rowNoResult" Visible="false">
                        <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                            <asp:Label runat="server" ID="lblNoResult" Text="NESSUN GENERATORE PRESENTE DA DISATTIVARE" CssClass="GridLabelNoCount" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow runat="server" ID="rowGT" Visible="false">
                        <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                            <asp:DataGrid ID="gridGT" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                                CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                                AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoGruppoTermico"
                                OnItemDataBound="gridGT_ItemDataBound">
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridItem" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDLibrettoImpiantoGruppoTermico" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Gruppi Termici">
                                        <ItemTemplate>
                                            <asp:Table ID="tblInfo" Width="780" runat="server">
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Tipologia:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="5">
                                                   <%#Eval("Tipologia") %> <%#Eval("AnalisiFumoPrevisteNr", " - Previste {0} analisi fumi") %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780" runat="server" ID="rowDismesso" CssClass="riempimento4">
                                                    <asp:TableCell ColumnSpan="6" Font-Bold="true">
                                                   Generatore dismesso&nbsp;in data&nbsp;<%# String.Format("{0:dd/MM/yyyy}", Eval("DataDismesso")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="Dismetti">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelezioneGT" AutoPostBack="true" OnCheckedChanged="chkSelezioneGT_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="fDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="DataDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDUtenteDismesso" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160" HeaderText="Scarica documento">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="imgExportPdfLetteraDismissione" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="Richiesta dismissione" ToolTip="Richiesta dismissione generatore" Target="_blank" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                            </asp:DataGrid>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" ID="rowGF" Visible="false">
                        <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                            <asp:DataGrid ID="gridGF" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                                CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                                AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoMacchinaFrigorifera"
                                OnItemDataBound="gridGF_ItemDataBound">
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridItem" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDLibrettoImpiantoMacchinaFrigorifera" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Gruppi Macchine Frigo">
                                        <ItemTemplate>
                                            <asp:Table ID="tblInfo" Width="780" runat="server">
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Tipologia:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="5">
                                                   <%#Eval("Tipologia") %> <%#Eval("NumCircuiti", " - {0} circuiti") %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780" runat="server" ID="rowDismesso" CssClass="riempimento4">
                                                    <asp:TableCell ColumnSpan="6" Font-Bold="true">
                                                    Generatore dismesso&nbsp;in data&nbsp;<%# String.Format("{0:dd/MM/yyyy}", Eval("DataDismesso")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="Dismetti">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelezioneGF" AutoPostBack="true" OnCheckedChanged="chkSelezioneGF_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="fDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="DataDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDUtenteDismesso" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160" HeaderText="Scarica documento">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="imgExportPdfLetteraDismissione" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="Richiesta dismissione" ToolTip="Richiesta dismissione generatore" Target="_blank" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                            </asp:DataGrid>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" ID="rowSC" Visible="false">
                        <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                            <asp:DataGrid ID="gridSC" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                                CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                                AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoScambiatoreCalore"
                                OnItemDataBound="gridSC_ItemDataBound">
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridItem" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDLibrettoImpiantoScambiatoreCalore" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Scambiatori di Calore">
                                        <ItemTemplate>
                                            <asp:Table ID="tblInfo" Width="780" runat="server">
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780" runat="server" ID="rowDismesso" CssClass="riempimento4">
                                                    <asp:TableCell ColumnSpan="6" Font-Bold="true">
                                                   Generatore dismesso&nbsp;in data&nbsp;<%# String.Format("{0:dd/MM/yyyy}", Eval("DataDismesso")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="Dismetti">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelezioneSC" AutoPostBack="true" OnCheckedChanged="chkSelezioneSC_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="fDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="DataDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDUtenteDismesso" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160" HeaderText="Scarica documento">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="imgExportPdfLetteraDismissione" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="Richiesta dismissione" ToolTip="Richiesta dismissione generatore" Target="_blank" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                            </asp:DataGrid>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" ID="rowCG" Visible="false">
                        <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                            <asp:DataGrid ID="gridCG" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                                CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="False" AllowPaging="False"
                                AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpiantoCogeneratore"
                                OnItemDataBound="gridCG_ItemDataBound">
                                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                                <ItemStyle CssClass="GridItem" />
                                <Columns>
                                    <asp:BoundColumn DataField="IDLibrettoImpiantoCogeneratore" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="Prefisso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="CodiceProgressivo" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Cogeneratori">
                                        <ItemTemplate>
                                            <asp:Table ID="tblInfo" Width="780" runat="server">
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Gruppo termico:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                    <%#Eval("Prefisso") %>  <%#Eval("CodiceProgressivo") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Data installazione:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="80">
                                                   <%# String.Format("{0:dd/MM/yyyy}", Eval("DataInstallazione")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Fabbricante:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Fabbricante") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Modello:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Modello") %>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                     <b>Matricola:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell Width="120">
                                                   <%#Eval("Matricola") %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780">
                                                    <asp:TableCell Width="120">
                                                     <b>Tipologia:&nbsp;</b>
                                                    </asp:TableCell>
                                                    <asp:TableCell ColumnSpan="5">
                                                   <%#Eval("Tipologia") %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                                <asp:TableRow Width="780" runat="server" ID="rowDismesso" CssClass="riempimento4">
                                                    <asp:TableCell ColumnSpan="6" Font-Bold="true">
                                                   Generatore dismesso&nbsp;in data&nbsp;<%# String.Format("{0:dd/MM/yyyy}", Eval("DataDismesso")) %>
                                                    </asp:TableCell>
                                                </asp:TableRow>
                                            </asp:Table>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50" HeaderText="Dismetti">
                                        <ItemTemplate>
                                            <asp:CheckBox runat="server" ID="chkSelezioneCG" AutoPostBack="true" OnCheckedChanged="chkSelezioneCG_CheckedChanged" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="fDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="DataDismesso" Visible="false" ReadOnly="True" />
                                    <asp:BoundColumn DataField="IDUtenteDismesso" Visible="false" ReadOnly="True" />
                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="160" HeaderText="Scarica documento">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="imgExportPdfLetteraDismissione" runat="server" CssClass="lnkLink" Font-Bold="true" Font-Underline="true" Text="Richiesta dismissione" ToolTip="Richiesta dismissione generatore" Target="_blank" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                            </asp:DataGrid>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
            <ProgressTemplate>
                <div>
                    <img alt="" src="images/loader.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
