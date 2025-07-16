<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VerifichePeriodicheGT.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_UC_VerifichePeriodicheGT" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCBolliniView.ascx" TagPrefix="uc1" TagName="UCBolliniView" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Table runat="server" BackColor="#ffffff" Width="100%">
    <asp:TableRow>
        <asp:TableCell>
            <dx:ASPxGridView ID="DataGrid"
                ClientInstanceName="DataGrid"
                EnableCallbackAnimation="true"
                runat="server"
                KeyFieldName="Id"
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
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Data Controllo :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%#Eval("DataControllo", "{0:d}") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Numero modulo :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("ModuloTermico") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Portata termica effettiva (kW) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("PotenzaTermicaEffettiva") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Temperatura fumi (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaFumi") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Temperaratura aria comburente (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TemperaraturaComburente") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="O2 (%) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("O2") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="CO2 (%) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("Co2") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Indice di Bachahach :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("bacharach1") + "/"+ Eval("bacharach2")  +"/"+ Eval("bacharach3")  %>' />  
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="CO fumi secchi (ppm v/v) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("COFumiSecchi") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Portata combustibile (m3/h oppure kg/h)) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("PortataCombustibile") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="CO nei fumi secchi e senz'aria (ppm v/v) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("CoCorretto") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Rendimento di combustione (%) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("RendimentoCombustione") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Rispetta l'indice di Bacharach :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%#DataUtilityCore.UtilityApp.ToSiNo(Eval("RispettaIndiceBacharach").ToString()) %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="CO fumi secchi e senz'aria <= 1000 ppm v/v :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%#DataUtilityCore.UtilityApp.ToSiNo(Eval("COFumiSecchiNoAria1000").ToString()) %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Rendimento minimo di legge (%) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("RendimentoMinimo") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Rendimendo supera il minimo :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# DataUtilityCore.UtilityApp.ToSiNo(Eval("RendimentoSupMinimo").ToString()) %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <uc1:UCBolliniView runat="server" ID="UCSBolliniView" IDRapportoControlloTecnico='<%# (long)Eval("Id") %>' />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn>
                        <DataItemTemplate>
                            <asp:ImageButton ID="ImgPdf" runat="server" BorderStyle="None" ImageUrl="~/images/Buttons/pdf.png" ToolTip="Visualizza PDF Rapporto di controllo tecnico" />                            
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>

            <dx:ASPxPopupControl ID="WindowRapporti"
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
                ClientInstanceName="WindowRapporti"
                EnableHierarchyRecreation="true"
                AllowDragging="True">
                <ContentStyle Paddings-Padding="0" />
                <ClientSideEvents Shown="WindowRapporti" />
            </dx:ASPxPopupControl>
            <script type="text/javascript">
                function OpenPopupWindowRapporti(element, key)
                {
                    var url = 'RCT_RapportiControlloViewer.aspx?IDRapportoControlloTecnico=' + key;
                    WindowRapporti.SetContentUrl(url)
                    WindowRapporti.Show();
                }
            </script>

        </asp:TableCell>
    </asp:TableRow>
</asp:Table>



