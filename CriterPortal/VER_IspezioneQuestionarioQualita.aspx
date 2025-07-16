<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VER_IspezioneQuestionarioQualita.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="VER_IspezioniQuestionarioQualita" %>

<html lang="it" xml:lang="it" xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <!--INIZIO HEADER-->
    <title>Regione Emilia-Romagna</title>
    <link rel="stylesheet" href="https://wwwservizi.regione.emilia-romagna.it/includes/TemplatesER/styles.css" />
    <link id="Link1" rel="stylesheet" runat="server" href="~/Content/StyleCustom.css" media="screen" />
    <link id="Link2" rel="shortcut icon" runat="server" type="image/x-icon" href="https://www.regione.emilia-romagna.it/favicon.ico" />
    <script type="text/javascript" src="Scripts/jquery-3.1.1.js"></script>
    <script type="text/javascript" src="Scripts/jquery-accessibleMegaMenu.js"></script>
    <script type="text/javascript" src="Scripts/jquery.hoverIntent.minified.js"></script>
    <script type="text/javascript" src="Scripts/jquery.dhtmlwindow.js"></script>
    <script type="text/javascript" src="Scripts/jquery.MultiFile.js"></script>
    <script type="text/javascript" src="Scripts/DisableButton.js"></script>
    <script type="text/javascript" src="Scripts/SiCommonUtility.js"></script>
</head>
<body>
    <form runat="server" id="MainFormView">
        <asp:Table Width="950" ID="tblInfoQuestionario" runat="server">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
		            <h2>QUESTIONARIO QUALITA' ISPEZIONE</h2>
                    <asp:Label ID="lblIsDefinitivo" runat="server" Visible="false" />
                    <asp:Label ID="lblIsIspezioneSvolta" runat="server" Visible="false" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="290" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_DataIspezione" AssociatedControlID="lblDataIspezione" Text="Data ispezione" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:Label ID="lblDataIspezione" runat="server" Font-Bold="true" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="290" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_DataIspezioneConclusa" AssociatedControlID="lblDataIspezioneConclusa" Text="Data ispezione conclusa" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:Label ID="lblDataIspezioneConclusa" runat="server" Font-Bold="true" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="290" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_GiorniChiusuraIspezione" AssociatedControlID="lblGiorniChiusuraIspezione" Text="Giorni chiusura ispezione" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:Label ID="lblGiorniChiusuraIspezione" runat="server" Font-Bold="true" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IspezioneSvolta" AssociatedControlID="imgIspezioneSvolta" Text="Ispezione effettuata" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:ImageButton runat="server" ID="imgIspezioneSvolta" BorderStyle="None" TabIndex="1" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_CompensoIspezione" AssociatedControlID="lblCompensoIspezione" Text="Compenso ispezione" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:Label ID="lblCompensoIspezione" runat="server" Font-Bold="true" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
			        <h3>DATI QUALITA'</h3>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="300" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IndiceTempisticheConclusioneVerifica" Text="Indice della Tempistica di registrazione e conclusione della verifica ispettiva" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:RadioButtonList ID="rblIndiceTempisticheConclusioneVerifica" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                        <asp:ListItem Text="3 punti (invio entro 8 giorni)" Value="3" />
                        <asp:ListItem Text="2 punti (invio tra 8 e 10 giorni)" Value="2" />
                        <asp:ListItem Text="1 punto (invio oltre i 10 giorni)" Value="1" />
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="300" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IndiceCompletezzaRvi" Text="Indice di Completezza del Rapporto di Verifica Ispettiva" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:RadioButtonList ID="rblIndiceCompletezzaRvi" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                        <asp:ListItem Text="3 punti (RVI è redatto in maniera corretta, le non conformità sono elencate e descritte in maniera chiara e sintetica)" Value="3" />
                        <asp:ListItem Text="2 punti (RVI è redatto in maniera corretta, ma sono assenti alcune informazioni o alcuni aspetti non sono chiari o comprensibili)" Value="2" />
                        <asp:ListItem Text="1 punto (RVI è redatto in maniera confusa, o troppo sintetica o troppo prolissa, se non è possibile comprendere quali sono le non conformità o se queste non sono coerenti al disposto normativo o non supportate da immagini)" Value="1" />
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="300" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IndiceReclami" Text="Indice dei Reclami ricevuti" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:RadioButtonList ID="rblIndiceReclami" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                        <asp:ListItem Text="Il reclamo è fondato ed è inerente una violazione al codice di comportamento ed imparzialità ed assenza di ulteriori interessi dell’ispettore; Il reclamo è fondato ed inerente a situazioni che possono rappresentare un reato" Value="1" />
                        <asp:ListItem Text="Il reclamo non è infondato ma attiene alla sfera di competenze dell’ispettore (qualità dell’operato)" Value="2" />
                        <asp:ListItem Text="Il reclamo è infondato, o non riguarda l’operato di un ispettore (ad esempio un problema di procedure informatiche), in questo caso non si procede con nessuna ulteriore azione" Value="3" />
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="rowIspezioneNonEffettuataTitle">
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
	                <h3>DATI RENDICONTAZIONE ISPEZIONE NON EFFETTUATA</h3>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="rowIspezioneNonEffettuata">
                <asp:TableCell Width="300" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IspezioneNonEffettuata" Text="Ispezione non effettuata" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:RadioButtonList ID="rblIspezioneNonEffettuata" runat="server" AutoPostBack="true" OnSelectedIndexChanged="InputChanged" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                        <asp:ListItem Text="Ispezione non effettuata" Value="1" />
                        <asp:ListItem Text="Ispezione non effettuata per generatore disattivato con presenza modulo disattivazione" Value="2" />
                        <asp:ListItem Text="Ispezione non effettuata per generatore disattivato con assenza modulo disattivazione" Value="3" />
                        <asp:ListItem Text="Ispezione non effettuata per doppia MAI" Value="4" />
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="rowIspezioneEffettuataTitle">
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                    <h3>DATI RENDICONTAZIONE ISPEZIONE EFFETTUATA</h3>
                </asp:TableCell>
            </asp:TableRow>
            
            <asp:TableRow runat="server" ID="rowIspezioneEffettuata1">
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IsEffettuataNoAnalisi" AssociatedControlID="chkIsEffettuataNoAnalisi" Text="Ispezione effettuata senza analisi di combustione per cause non imputabili all’ispettore: decurtazione del 20%" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:CheckBox ID="chkIsEffettuataNoAnalisi" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="InputChanged" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="rowIspezioneEffettuata2">
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IsRitardoConsegna" AssociatedControlID="chkIsRitardoConsegna" Text="Ritardo nella consegna telematica dei rapporti ispettivi oltre i termini previsti nel Regolamento Regionale n. 1/2017: decurtazione del 10%" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:CheckBox ID="chkIsRitardoConsegna" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="InputChanged" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="rowIspezioneEffettuata3">
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IsRitardoComunicazione" AssociatedControlID="chkIsRitardoComunicazione" Text="Ritardo nella comunicazione (da rendersi entro 24 ore) relativa ad impianti pericolosi per i quali si diffida dall’utilizzo: decurtazione del 30%" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:CheckBox ID="chkIsRitardoComunicazione" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="InputChanged" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="rowIspezioneEffettuata4">
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IsRitardoAppuntamento" AssociatedControlID="chkIsRitardoAppuntamento" Text="Grave ed ingiustificato ritardo da parte dell’ispettore all’appuntamento prefissato, accertato comportamento scorretto nei confronti degli utenti o indisciplina durante lo svolgimento delle proprie mansioni: decurtazione del 10%" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:CheckBox ID="chkIsRitardoAppuntamento" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="InputChanged" />
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow runat="server" ID="rowIspezioneEffettuata5">
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IsMancataDocumentazione" AssociatedControlID="chkIsMancataDocumentazione" Text="Mancata allegazione della documentazione (ricevuta) emessa dallo strumento utilizzato per l’analisi di rendimento e/o tiraggio (laddove applicabile): decurtazione del 10%" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:CheckBox ID="chkIsMancataDocumentazione" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="InputChanged" />
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                    <h3>DATI RENDICONTAZIONE MANCATI ACCESSI ISPETTIVI</h3>
                </asp:TableCell>
            </asp:TableRow>


            <asp:TableRow>
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IsNonEffettuataMai1" AssociatedControlID="chkIsNonEffettuataMai1" Text="Ispezione non effettuata a causa dell’indisponibilità all’accesso nel luogo di ispezione (MAI1)" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:CheckBox ID="chkIsNonEffettuataMai1" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="InputChanged" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="225" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_IsNonEffettuataMai2" AssociatedControlID="chkIsNonEffettuataMai2" Text="Ispezione non effettuata a causa dell’indisponibilità all’accesso nel luogo di ispezione (MAI2)" />
                </asp:TableCell>
                <asp:TableCell Width="675" ColumnSpan="3" CssClass="riempimento">
                    <asp:CheckBox ID="chkIsNonEffettuataMai2" TabIndex="1" runat="server" AutoPostBack="true" CssClass="checkboxClass" OnCheckedChanged="InputChanged" />
                </asp:TableCell>
            </asp:TableRow>
                        

            <asp:TableRow>
                <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                    <h3>DATI RENDICONTAZIONE FINALE</h3>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell Width="290" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_CompensoFinale" AssociatedControlID="lblCompensoFinale" Text="Compenso finale ispezione" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <asp:Label ID="lblCompensoFinale" runat="server" Font-Bold="true" ForeColor="Green" />
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell Width="290" CssClass="riempimento2">
                    <asp:Label runat="server" ID="Label_ddlTrimestreFatturazione" AssociatedControlID="ddlTrimestreFatturazione" Text="Periodo fatturazione" />
                </asp:TableCell>
                <asp:TableCell Width="600" CssClass="riempimento">
                    <dx:ASPxComboBox ID="ddlTrimestreFatturazione" runat="server" Width="200px">
                        <Items>
                            <dx:ListEditItem Text="Trimestre Luglio-Agosto-Settembre" Value="1" />
                            <dx:ListEditItem Text="Trimestre Ottobre-Novembre-Dicembre" Value="2" />
                            <dx:ListEditItem Text="Trimestre Gennaio-Febbraio-Marzo" Value="3" />
                            <dx:ListEditItem Text="Trimestre Aprile-Maggio-Giugno" Value="4" />
                        </Items>
                    </dx:ASPxComboBox>
                    <asp:RequiredFieldValidator ID="rfvddlTrimestreFatturazione" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Periodo fatturazione: campo obbligatorio"
                        ControlToValidate="ddlTrimestreFatturazione">&nbsp;Campo Obbligatorio</asp:RequiredFieldValidator>
                </asp:TableCell>
            </asp:TableRow>

            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                      &nbsp;
                </asp:TableCell>
            </asp:TableRow>

            
        </asp:Table>

        <asp:Table Width="950" ID="Table1" runat="server">
            <asp:TableRow>
                <asp:TableCell ColumnSpan="4" HorizontalAlign="Center" CssClass="riempimento5">
                    <asp:Button ID="btnSaveDefinitivo" runat="server" TabIndex="1" CssClass="buttonClass" Width="330"
                        OnClick="btnSaveDefinitivoBozza_Click" Text="RENDI QUESTIONARIO DEFINITIVO" OnClientClick="javascript:return confirm('Confermi di rendere il questionario definitivo?');" />&nbsp;
                    <asp:Button ID="btnSaveBozza" runat="server" TabIndex="1" CssClass="buttonClass" Width="330"
                        OnClick="btnSaveDefinitivoBozza_Click" Text="RENDI QUESTIONARIO IN BOZZA" OnClientClick="javascript:return confirm('Confermi di rendere il questionario in bozza?');" />&nbsp;
                    <asp:Button ID="btnSaveQuestionario" runat="server" TabIndex="1" CssClass="buttonClass" Width="330"
                        OnClick="btnSaveQuestionario_Click" Text="SALVA DATI QUESTIONARIO" />

                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

    </form>
    <!-- -->
</body>
</html>

