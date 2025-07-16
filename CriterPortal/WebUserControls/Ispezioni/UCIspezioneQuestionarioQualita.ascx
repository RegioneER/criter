<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCIspezioneQuestionarioQualita.ascx.cs" Inherits="WebUserControls_Ispezioni_UCIspezioneQuestionarioQualita" %>

<asp:Table Width="890" ID="tblInfoGenerali" runat="server">
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Questionari qualità ispezioni</h3></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
        <asp:TableCell ColumnSpan="2">
            <asp:Label runat="server" ID="lblIDIspezioneVisita" Visible="false" />
            <asp:Label runat="server" ID="lblIDIspezioneSelected" Visible="false" />
            <dx:ASPxGridView ID="DataGrid"
                ClientInstanceName="DataGrid"
                EnableCallbackAnimation="true"
                runat="server"
                KeyFieldName="IDIspezione"
                Width="100%"
                
                Font-Size="Smaller"


                Border-BorderStyle="None"
                BorderBottom-BorderStyle="None"
                BorderLeft-BorderStyle="None"
                BorderRight-BorderStyle="None"
                BorderTop-BorderStyle="None"

                Styles-AlternatingRow-BackColor="#ffedad"
                Styles-Row-CssClass="GridItem"
                    
                Styles-Header-Font-Bold="true"
                Styles-Table-BackColor="#ffcc3d"
                Styles-Header-BackColor="#ffcc3d"
                Styles-EmptyDataRow-BackColor="#ffffff"

                OnHtmlRowCreated="DataGrid_HtmlRowCreated">
                <SettingsPager PageSize="20" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="IDIspezioneDocumento" VisibleIndex="0" Visible="false" />
                    <dx:GridViewDataTextColumn VisibleIndex="1" Caption="Codice ispezione"  HeaderStyle-HorizontalAlign="Left">
                        <DataItemTemplate>
                            <br />
                            <dx:ASPxLabel runat="server" Text='<%# Eval("CodiceIspezione") %>' Font-Bold="true" ForeColor="#000000" />
                            <br />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataCheckColumn FieldName="IsDefinitivo" Caption="Definitivo?" Width="50px" VisibleIndex="2" UnboundType="Boolean" />
                    <dx:GridViewDataTextColumn FieldName="DataUltimaModifica" VisibleIndex="3" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy HH:mm:ss"  Width="190px" Caption="Data Ultima Modifica" />
                    <dx:GridViewDataColumn FieldName="UtenteUltimaModifica" VisibleIndex="4" Width="300px" Caption="Utente" />
                    <dx:GridViewDataTextColumn Width="30" VisibleIndex="5">
                        <DataItemTemplate>
                            <asp:ImageButton ID="ImgPdf" runat="server" BorderStyle="None" ImageUrl="~/images/Buttons/detailsSmall.png" ToolTip="Visualizza questionario qualità ispezione" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>

            <dx:ASPxPopupControl ID="WindowQuestionario"
                runat="server"
                Modal="true"
                CloseAction="CloseButton"
                CloseAnimationType="Fade"
                HeaderText=""
                PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter"
                Width="990px"
                Height="800px"
                MinWidth="990px"
                MinHeight="800px"
                ClientInstanceName="WindowQuestionario"
                EnableHierarchyRecreation="true"
                AllowDragging="True">
                <ContentStyle Paddings-Padding="0" />
                <ClientSideEvents Shown="WindowQuestionario" CloseUp="function(s, e) { window.location.reload(true); }" />
            </dx:ASPxPopupControl>
            <script type="text/javascript">
                function OpenPopupWindowQuestionario(element, key) {
                    var url = 'VER_IspezioneQuestionarioQualita.aspx?IDIspezione=' + key;
                    WindowQuestionario.SetContentUrl(url)
                    WindowQuestionario.Show();
                }
            </script>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
