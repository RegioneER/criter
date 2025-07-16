<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VerificheIspettive.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_UC_VerificheIspettive" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Table runat="server" BackColor="#ffffff" Width="100%">
    <asp:TableRow>
        <asp:TableCell>
            <dx:ASPxGridView ID="DataGrid"
                ClientInstanceName="DataGrid"
                EnableCallbackAnimation="true"
                runat="server"
                KeyFieldName="IDIspezione"
                Width="100%"
                ForeColor="#000000"
                Font-Size="Smaller"
                AutoGenerateColumns="false"
                OnHtmlRowCreated="DataGrid_HtmlRowCreated">
                <SettingsPager PageSize="20" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                <Columns>
                    <dx:GridViewDataTextColumn VisibleIndex="0">
                        <DataItemTemplate>
                            <asp:Table runat="server" Width="820" BorderColor="#ffffff">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <dx:ASPxLabel runat="server" Text="Generatore :&nbsp;"  Font-Bold="true" ForeColor="#000000"  Visible="true "/>
                                        <dx:ASPxLabel runat="server" Text='<%# Eval("Prefisso") + "   " +  Eval("CodiceProgressivo","{0:00}") %>'  Visible="true" Font-Bold="true"/>
                                        <br /><br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell  Height="20">
                                        <asp:Label runat="server" Text="Codice Ispezione :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                                        <asp:Label runat="server" Text='<%# Eval("CodiceIspezione") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Data Ispezione :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                                        <asp:Label runat="server" Text='<%#Eval("DataIspezione", "{0:d}") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                                        <asp:Label runat="server" Text="Ispettore :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                                        <asp:Label runat="server" Text='<%# Eval("Ispettore") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn>
                        <DataItemTemplate>
                            <asp:ImageButton ID="ImgPdf" runat="server" BorderStyle="None" ImageUrl="~/images/Buttons/pdf.png" ToolTip="Visualizza PDF Rapporto Verifica Ispettiva" />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>

            <dx:ASPxPopupControl ID="WindowRapportiRVI"
                runat="server"
                Modal="true"
                CloseAction="CloseButton"
                CloseAnimationType="Fade"
                HeaderText=""
                PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter"
                Width="905px"
                Height="600px"
                MinWidth="905px"
                MinHeight="600px"
                ClientInstanceName="WindowRapportiRVI"
                EnableHierarchyRecreation="true"
                AllowDragging="True">
                <ContentStyle Paddings-Padding="0" />
                <ClientSideEvents Shown="WindowRapportiRVI" />
            </dx:ASPxPopupControl>
            <script type="text/javascript">
                function OpenPopupWindowRapportiRVI(element, key) {
                    var url = 'VER_IspezioniViewerRVI.aspx?IDIspezione=' + key;
                    WindowRapportiRVI.SetContentUrl(url)
                    WindowRapportiRVI.Show();
                }
            </script>

        </asp:TableCell>
    </asp:TableRow>
</asp:Table>



