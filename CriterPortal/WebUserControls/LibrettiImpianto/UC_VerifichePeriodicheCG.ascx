<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VerifichePeriodicheCG.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_UC_VerifichePeriodicheCG" %>
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
                OnHtmlRowCreated="DataGrid_HtmlRowCreated">
                <SettingsPager PageSize="20" Summary-Visible="false" ShowDefaultImages="false" ShowDisabledButtons="false" ShowNumericButtons="true" />
                <Columns>
                    <dx:GridViewDataTextColumn VisibleIndex="0">
                        <DataItemTemplate>
                            <asp:Table runat="server" Width="820" BorderColor="#ffffff">
                                <asp:TableRow>
                                    <asp:TableCell>
                            <dx:ASPxLabel runat="server" Text="Generatore :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <dx:ASPxLabel runat="server" Text='<%# Eval("Prefisso") + "   " +  Eval("CodiceProgressivo","{0:00}") %>' Font-Bold="true"/>
                            <br /><br />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Data Controlo :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%#Eval("DataControllo", "{0:d}") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Temperatura aria comburente (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaAriaComburente") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Temperatura acqua in ingresso (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaAcquaIngresso") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Temperatura acqua motore (molo m.c.i) (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaAcquaMotore") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Temperatura fumi a monte dello scambiatore di fumi (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaFumiMonte") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Potenza elettrica ai morsetti (kW) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("PotenzaAiMorsetti") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Sovrafrequenza: soglia intervento (Hz) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("SovrafrequenzaSogliaInterv1") + "/"+ Eval("SovrafrequenzaSogliaInterv2")  +"/"+ Eval("SovrafrequenzaSogliaInterv3")  %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Sovrafrequenza: tempo intervento (s) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("SovrafrequenzaTempoInterv1") + "/"+ Eval("SovrafrequenzaTempoInterv2")  +"/"+ Eval("SovrafrequenzaTempoInterv3")  %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Sottofrequenza: tempo intervento (s) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("SottofrequenzaTempoInterv1") + "/"+ Eval("SottofrequenzaTempoInterv2")  +"/"+ Eval("SottofrequenzaTempoInterv3")  %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Sovratensione: soglia intervento (V) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("SovratensioneSogliaInterv1") + "/"+ Eval("SovratensioneSogliaInterv2")  +"/"+ Eval("SovratensioneSogliaInterv3")  %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Sottotensione: soglia intervento (V) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("SottotensioneSogliaInterv1") + "/"+ Eval("SottotensioneSogliaInterv2")  +"/"+ Eval("SottotensioneSogliaInterv3")  %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Sottotensione: tempo intervento (s) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("SottotensioneTempoInterv1") + "/"+ Eval("SottotensioneTempoInterv2")  +"/"+ Eval("SottotensioneTempoInterv3")  %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Temperatura acqua in uscita (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaAcquauscita") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Temperatura fumi a valle dello scambiatore di fumi (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaFumiValle") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Sovratensione: tempo intervento (s) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("SovratensioneTempoInterv1") + "/"+ Eval("SovratensioneTempoInterv2")  +"/"+ Eval("SovratensioneTempoInterv3")  %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Sottofrequenza: soglia intervento (Hz) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("SottofrequenzaSogliaInterv1") + "/"+ Eval("SottofrequenzaSogliaInterv2")  +"/"+ Eval("SottofrequenzaSogliaInterv3")  %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Emissioni di monossido di carbonio CO (mg/Nm3 riportati al 5% di O2 nei fumi) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("EmissioneMonossido") %>' />            
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            <br />
                            <uc1:UCBolliniView runat="server" ID="UCSBolliniView" IDRapportoControlloTecnico='<%# (long)Eval("Id") %>' />
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn>
                        <DataItemTemplate>
                            <asp:ImageButton ID="ImgPdf" runat="server" BorderStyle="None" ImageUrl="~/images/Buttons/pdf.png" ToolTip="Visualizza pdf Rapporto di controllo tecnico" />
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

