<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VER_ProgrammaAccertamentoDetails.aspx.cs" Inherits="VER_ProgrammaAccertamentoDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link id="Link1" rel="stylesheet" runat="server" href="~/Content/StyleCustom.css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxGridView ID="GridAccertamenti" runat="server" AutoGenerateColumns="false" KeyFieldName="IDAccertamento" 
                    Width="100%" Styles-Cell-HorizontalAlign="Center"
                    OnHtmlRowCreated="GridAccertamenti_HtmlRowCreated"
                    OnPageIndexChanged="GridAccertamenti_PageIndexChanged"
                    ClientInstanceName="GridAccertamenti">
                    <Styles AlternatingRow-CssClass="GridAlternativeItem" Header-HorizontalAlign="Center"
                        Row-CssClass="GridItem"
                        Header-CssClass="GridHeader" />
                    <SettingsBehavior AllowFocusedRow="false"
                        ProcessFocusedRowChangedOnServer="false"
                        AllowSort="false" />
                    <SettingsPager PageSize="20" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="IDAccertamentoProgramma" Visible="false" />
                        <dx:GridViewDataColumn Caption="Codice accertamento">
                            <DataItemTemplate>
                                <asp:HyperLink runat="server" ID="lnkAccertamento" />
                            </DataItemTemplate>
                        </dx:GridViewDataColumn>
                        <dx:GridViewDataColumn FieldName="CodiceTargatura" />
                    </Columns>
                </dx:ASPxGridView>
        </div>
    </form>
</body>
</html>