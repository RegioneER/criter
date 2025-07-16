<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VER_IspezioniRimandaAdIspettore.aspx.cs" Inherits="VER_IspezioniRimandaAdIspettore" %>

<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
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
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Table Width="900" ID="tblInfoNotificaOk" Visible="false" CssClass="TableClass" runat="server">
                        <asp:TableRow HorizontalAlign="Center">
                            <asp:TableCell Width="900" ColumnSpan="4" CssClass="riempimento5">
                                <asp:Label runat="server" ForeColor="Green" ID="lblNotificaOk" Font-Bold="true" Text="Notifica inviata con successo!" />
                            </asp:TableCell>
                        </asp:TableRow>
                    </asp:Table>


                    <asp:Table Width="900" ID="tblInfoGenerali" runat="server">
                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
			                    <h2>NOTIFICA RIAPERTURA ISPEZIONE</h2>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell Width="290" CssClass="riempimento2">
                                <asp:Label runat="server" ID="lblTitoloNotificaAdIspettore" AssociatedControlID="txtNotificaAdIspettore" Text="Notifica ad Ispettore" />
                            </asp:TableCell>
                            <asp:TableCell Width="600" CssClass="riempimento">
                                <asp:TextBox Width="600" Height="200" ID="txtNotificaAdIspettore" CssClass="txtClass" ValidationGroup="vgIspezione"
                                    runat="server" TextMode="MultiLine" Rows="6" />
                                <asp:RequiredFieldValidator ID="rfvtxtNotificaAdIspettore" ValidationGroup="vgIspezione" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Notifica ad Ispettore: campo obbligatorio"
                                    ControlToValidate="txtNotificaAdIspettore">&nbsp;*</asp:RequiredFieldValidator>
                            </asp:TableCell>
                        </asp:TableRow>

                        <asp:TableRow>
                            <asp:TableCell ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento5">
                                <asp:Button ID="btnRimandaIspezioneAdIspettore" runat="server" ValidationGroup="vgIspezione" TabIndex="1" CssClass="buttonSmallClass" Width="230"
                                    OnClick="btnRimandaIspezioneAdIspettore_Click" OnClientClick="javascript:return confirm('Confermi di rimandare l\'Ispezione all\'ispettore ed inviare una notifica tramite email?');" Text="RIMANDA ISPEZIONE AD ISPETTORE" />

                                <asp:ValidationSummary ID="vsIspezione" ValidationGroup="vgIspezione" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
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
        </div>
    </form>
</body>
</html>
