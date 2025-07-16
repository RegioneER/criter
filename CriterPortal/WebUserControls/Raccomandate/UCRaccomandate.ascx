<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCRaccomandate.ascx.cs" Inherits="WebUserControls_Sanzioni_UCRaccomandate" %>

<asp:Label runat="server" ID="lblIDAccertamento" Visible="false" />
<asp:Label runat="server" ID="lblIDIspezione" Visible="false" />
<asp:Label runat="server" ID="lblIDRaccomandataType" Visible="false" />

<asp:Table ID="tblInfoRaccomandate" Width="100%" runat="server">
    <asp:TableRow runat="server" ID="row8">
        <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
            <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="100" AllowSorting="false" AllowPaging="false"
                AutoGenerateColumns="False" OnItemDataBound="DataGrid_ItemDataBound" runat="server" DataKeyField="IDRichiesta">
                <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                <ItemStyle CssClass="GridItem" />
                <AlternatingItemStyle CssClass="GridAlternativeItem" />
                <Columns>
                    <asp:BoundColumn DataField="IDRichiesta" Visible="false" ReadOnly="True" />
                    <asp:BoundColumn DataField="IDRaccomandataType" Visible="false" ReadOnly="True" />
                    <asp:BoundColumn DataField="IDAccertamento" Visible="false" ReadOnly="True" />
                    <asp:BoundColumn DataField="IDIspezione" Visible="false" ReadOnly="True" />
                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Raccomandate">
                        <ItemTemplate>
                            <asp:Table ID="tblInfoRaccomandate" Width="100%" runat="server">
                                <asp:TableRow Width="700" runat="server" Visible='<%# Eval("fRaccomandataRecapitata") %>'>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="6" Width="500">
                                        <asp:Image runat="server" ID="LogoPoste" ImageUrl="~/images/logo-poste-italiane.png" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="100">
                                            <b>Servizio Poste:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Width="330">
                                        <asp:Label runat="server" ID="lblTipoRaccomandata" Text='<%# Eval("ServiceType") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="130">
                                         <b>Id raccomandata:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="400">
                                        <asp:Label ID="lblIDRichiesta" runat="server" Text='<%#Eval("IDRichiesta") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell Width="100">
                                                        <b>Data invio:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="100">
                                        <asp:Label ID="lblDataInvio" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("CreatoIl")) %>' />
                                    </asp:TableCell>
                                    <asp:TableCell Width="150">
                                                        <b>Tipo raccom.:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" Width="230">
                                        <asp:Label ID="lblRaccomandataType" runat="server" Text='<%#Eval("RaccomandataType") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="100">
                                                        <b>Esito spedizione:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Width="330">
                                        <asp:Label ID="lblEsito" runat="server" Text='<%# Eval("EsitoDescrizioneDepositoRaccomandata") %> ' />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="100">
                                         <b>Documento:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Width="330">
                                        <asp:ImageButton ID="ImgPdf" runat="server" BorderStyle="None" ImageUrl="~/images/Buttons/pdf.png" ToolTip="Visualizza PDF Raccomandata" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="100">
                                        <asp:Label ID="lblTitoloPratica" runat="server" Font-Bold="true" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Width="330">
                                        <asp:HyperLink runat="server" ID="lnkPratica" Target="_blank" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="100">
                                                        <b>N. raccomandata:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Width="330">
                                        <asp:Label ID="lblNumeroRaccomandata" runat="server" Text='<%# Eval("NumeroRaccomandata") %> ' />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="130">
                                                        <b>Codice Stato:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:Label ID="lblCodiceStatoRaccomandata" runat="server" Text='<%#Eval("CodiceStatoRaccomandata") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell Width="100">
                                                        <b>Stato:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:Label ID="lblStatoRaccomandata" runat="server" Text='<%#Eval("DescrizioneStatoRaccomandata") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell Width="100">
                                                        <b>Data ultimo agg.:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left">
                                        <asp:Label ID="lblDataAggiornamentoStato" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataStatoRaccomandata")) %>' />
                                    </asp:TableCell>
                                    
                                </asp:TableRow>

                                <asp:TableRow Width="700">
                                    <asp:TableCell Width="100">
                                            <b>Note:&nbsp;</b>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Left" ColumnSpan="5" Width="330">
                                        <%--<asp:Label runat="server" ID="txtNoteRaccomandata" Text='<%# Eval("Note") %>' />--%>
                                        <asp:TextBox runat="server" ID="txtNoteRaccomandata" Text='<%# Eval("Note") %>' TextMode="MultiLine" Rows="5" />
                                    </asp:TableCell>
                                </asp:TableRow>

                            </asp:Table>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
            </asp:DataGrid>

            <%--<dx:ASPxPopupControl ID="WindowRaccomandate"
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
                ClientInstanceName="WindowRaccomandate"
                EnableHierarchyRecreation="true"
                AllowDragging="True">
                <ContentStyle Paddings-Padding="0" />
                <ClientSideEvents Shown="WindowRaccomandate" />
            </dx:ASPxPopupControl>--%>
            <dx:ASPxPopupControl ID="WindowRaccomandate" runat="server" CloseAction="CloseButton" 
                Modal="True" AccessibilityCompliant="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                ClientInstanceName="WindowRaccomandate" HeaderText="Raccomandata"
                AllowDragging="True" PopupAnimationType="Fade" Width="950px" Height="700px" AllowResize="True">
            </dx:ASPxPopupControl>
            <script type="text/javascript">
                function OpenPopupWindowRaccomandate(element, key) {
                    var url = 'COM_RaccomandateViewer.aspx?IDRichiesta=' + key;
                    WindowRaccomandate.SetContentUrl(url)
                    WindowRaccomandate.Show();
                }
            </script>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow runat="server" ID="rowSaveNote" HorizontalAlign="Right" Visible="false" >
        <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
            <asp:Button runat="server" ID="btnSaveNote" Text="SALVA NOTE RACCOMANDATE" OnClick="btnSaveNote_Click" CssClass="buttonSmallClass" Width="200px" TabIndex="1" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>