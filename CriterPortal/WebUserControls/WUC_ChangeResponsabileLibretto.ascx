<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_ChangeResponsabileLibretto.ascx.cs" Inherits="WebUserControls_WUC_ChangeResponsabileLibretto" %>



<asp:Table Width="600" ID="tblInfoTestata" runat="server">
    <asp:TableRow>
        <asp:TableCell Width="600" ColumnSpan="2" CssClass="riempimento">
            <asp:Label runat="server" ID="lblIDLibrettoImpianto" Visible="false" />
            <asp:Label runat="server" ID="lblIDTargaturaImpianto" Visible="false" />
            <asp:Label runat="server" ID="lblIDAccertamento" Visible="false" />

            <dx:ASPxGridView ID="gridResponsabiliLibretti" ClientInstanceName="grid" runat="server"
                KeyFieldName="IDLibrettoImpianto"
                Width="600" OnSelectionChanged="gridResponsabiliLibretti_SelectionChanged"
                EnableCallBacks="False" EnableCallbackCompression="False">
                <Columns>
                    <dx:GridViewCommandColumn ShowSelectCheckbox="true" CellStyle-HorizontalAlign="Left" VisibleIndex="0" Caption="" />
                    <dx:GridViewDataColumn FieldName="IDLibrettoImpianto" VisibleIndex="1" Visible="false" CellStyle-HorizontalAlign="Center" />
                    <dx:GridViewDataColumn FieldName="Responsabile" VisibleIndex="2" />
                    <dx:GridViewDataColumn FieldName="IndirizzoResponsabile" VisibleIndex="3" />
                    <dx:GridViewDataColumn FieldName="fAttivo" Caption="Libretto Attivo?" VisibleIndex="4" CellStyle-HorizontalAlign="Center" />
                </Columns>
                <SettingsBehavior ProcessSelectionChangedOnServer="True" AllowSelectSingleRowOnly="true" />
                <SettingsPager EnableAdaptivity="true" PageSize="100" Visible="true" />
            </dx:ASPxGridView>

            <asp:Label runat="server" ID="lblIDSoggettiSelezionati" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>