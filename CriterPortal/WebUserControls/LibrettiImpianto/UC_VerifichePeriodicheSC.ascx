<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VerifichePeriodicheSC.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_UC_VerifichePeriodicheSC" %>
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
                            <dx:ASPxLabel runat="server" Text="Generatore :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
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
                            <asp:Label runat="server" Text="Temperatura esterna (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaEsterna") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Temperatura mandata primario (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaMandataPrimario") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Temperatura ritorno primario (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaRitornoPrimario") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Temperatura mandata secondario (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaMandataSecondario") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Temperatura ritorno secondario (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaRitornoSecondario") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Portata fluido primario (m3/h) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("PortataFluidoPrimario") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Potenza termica nominale totale (kW) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("PotenzaTermica")  %>' />  
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Potenza compatibile con i dati di progetto :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%#DataUtilityCore.UtilityApp.ValueSiNo(Eval("PotenzaCompatibileProgetto").ToString()) %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Stato delle coibentazioni idoneo :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%#DataUtilityCore.UtilityApp.ValueSiNo(Eval("CoibentazioniIdonee").ToString()) %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Dispositifi di regolazione e controllo :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%#DataUtilityCore.UtilityApp.ValueSiNo(Eval("AssenzaTrafilamenti").ToString()) %>' />
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
