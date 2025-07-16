<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" MaintainScrollPositionOnPostback="true" CodeFile="COM_Questionario.aspx.cs" Inherits="COM_Questionario" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Questionario</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Table runat="server" Width="650">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        QUESTIONARIO
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento5">
                        Sig./Sig.ra <asp:Label runat="server" Font-Bold="true" ID="lblResponsabile" /> , buongiorno/buonasera, la chiamo dalla Regione Emilia Romagna servizio Criter (Catasto Regionale degli Impianti Termici).
                        Ci risulta che in data <asp:Label runat="server" Font-Bold="true" ID="lblDataControllo" /> è stato effettuato il controllo di efficienza energetica del suo impianto di riscaldamento dalla ditta <asp:Label runat="server" Font-Bold="true" ID="lblImpresaManutentrice" /> … ha 3/4 minuti da dedicarmi per qualche veloce domanda per capire se i manutentori le hanno fornito tutte le informazioni necessarie per essere a norma di legge…?
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblRisposteDate" ForeColor="Green" Font-Bold="true" /><br />

                        <asp:DataGrid ID="dgQuestionario" CssClass="Grid" Width="900px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="10" AllowSorting="False" AllowPaging="False"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDQuestionarioDomanda"
                            OnItemDataBound="dgQuestionario_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDQuestionarioDomanda" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDQuestionario" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="550" HeaderText="DOMANDE">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDomanda" runat="server" Text='<%# Eval("Domanda") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="220" HeaderText="RISPOSTE">
                                    <ItemTemplate>
                                         <asp:RadioButtonList runat="server" ValidationGroup="vgQuestionario" ID="rblRisposte" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="180" HeaderText="NOTE">
                                    <ItemTemplate>
                                         <asp:TextBox runat="server" ID="txtNoteRisposte" Text='<%# Eval("Note") %>' TextMode="MultiLine" Rows="4" Height="100" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <%--<asp:Button runat="server" ID="btnSaveQuestionario" Text="Salva dati Questionario" OnClick="btnSaveQuestionario_Click" />--%>
                        <dx:ASPxButton runat="server" ID="btnSaveQuestionario" Visible="false" Text="Salva dati Questionario"
                            CausesValidation="false" ClientInstanceName="btnSaveQuestionario" OnClick="btnSaveQuestionario_Click" />&nbsp;
                        <dx:ASPxButton runat="server" ID="btnCloseQuestionarioNonCompilato" Visible="false" Text="Nessuna risposta fornita"
                            CausesValidation="false" ClientInstanceName="btnCloseQuestionarioNonCompilato" OnClick="btnCloseQuestionarioNonCompilato_Click" />&nbsp;
                        <dx:ASPxButton runat="server" ID="btnCloseQuestionario" Visible="false" Text="Salva e Chiudi Questionario"
                                    CausesValidation="true" ClientInstanceName="btnCloseQuestionario" OnClick="btnCloseQuestionario_Click" />
                                   <%-- <ClientSideEvents Init="function(s,e){ s.SetEnabled(true); }"
                                        Click="function(s,e){ 
                                            if(ASPxClientEdit.ValidateGroup(null)) 
                                            { 
                                                window.setTimeout(function()
                                                { 
                                                   LoadingPanel1.Show(); 
                                                   s.SetText('Attendere, salvataggio in corso...'); 
                                                   s.SetEnabled(false); 
                                                },1) 
                                            } 
                                        }" />
                                </dx:ASPxButton>
                                <dx:ASPxLoadingPanel ID="lp1" runat="server" Text="Attendere, salvataggio in corso..."
                                    ClientInstanceName="LoadingPanel1">
                                </dx:ASPxLoadingPanel>--%>
                    </asp:TableCell>
                </asp:TableRow>
                <%--<asp:TableRow>
                    <asp:TableCell ColumnSpan="4" CssClass="riempimento5">
                       <asp:CustomValidator ID="cvRisposteDomande" runat="server" ForeColor="Red" ValidationGroup="vgQuestionario" EnableClientScript="true" OnServerValidate="ControllaRisposteDate"
                            ErrorMessage="Partita Iva non valida<br/>" />
                    </asp:TableCell>
                </asp:TableRow>--%>
             </asp:Table>
        </div>
    </form>
</body>
</html>
