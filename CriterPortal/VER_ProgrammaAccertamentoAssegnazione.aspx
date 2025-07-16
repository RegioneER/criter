<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VER_ProgrammaAccertamentoAssegnazione.aspx.cs" Inherits="VER_ProgrammaAccertamentoAssegnazione" %>

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
</head>
<body class="template-er_portletpage_view_notitle portaltype-erportletpage site-energia section-homepage" dir="ltr">
    <form id="form1" runat="server">
        <div>
            <asp:Table runat="server" ID="Detail" CssClass="TableClass" HorizontalAlign="Center" Width="100%" Height="84%" Visible="true">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        <asp:Label runat="server" ID="lblPacchetto" Text="ASSEGNAZIONE PACCHETTO" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Left" CssClass="riempimento1">
                        <asp:Label runat="server" ID="Accertatore" Text="Accertatore:" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <dx:ASPxComboBox ID="ASPxComboBoxAccertatore" runat="server" Theme="Default" TabIndex="1"
                            AutoPostBack="false"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.Int32"
                            ValueField="IDUtente"
                            OnItemsRequestedByFilterCondition="ASPxComboBoxAccertatore_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBoxAccertatore_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBoxAccertatore_ButtonClick"
                            TextFormatString="{0}"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Accertatore" Caption="Accertatore" Width="50" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <br />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow ID="rowError" Visible="false">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center">
                        <asp:Label runat="server" ID="lblError" ForeColor="Red" Font-Bold="true" Text="Accertatore - campo obbligatorio" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell HorizontalAlign="Center" ColumnSpan="2">
                        <asp:Button ID="btnAssegnaPacchetto" OnClick="btnAssegnaPacchetto_Click" CssClass="buttonClass" Width="250" Text="ASSEGNAZIONE PACCHETTO" runat="server" OnClientClick="if (!confirm('Confermi di voler assegnare il pacchetto?')) return false;" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </div>
    </form>
</body>
</html>