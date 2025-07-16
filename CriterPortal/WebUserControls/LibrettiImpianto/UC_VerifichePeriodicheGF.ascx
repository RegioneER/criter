<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UC_VerifichePeriodicheGF.ascx.cs" Inherits="WebUserControls_LibrettiImpianto_UC_VerifichePeriodicheGF" %>
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
                            <asp:Label runat="server" Text="Numreo circuito :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("NCircuiti") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Assenza perdite refrigerante :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%#DataUtilityCore.UtilityApp.ValueSiNo(Eval("AssenzaPerditeRefrigerante").ToString()) %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Prova riscaldamento :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# DataUtilityCore.UtilityApp.ToSiNo(Eval("ProvaRaffrescamento").ToString()) %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Sottoraffreddamento (K) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaSottoraffreddamento") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T Condensazione (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%#(Eval("TemperaturaCondensazione")) %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="T Evaporazione (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TemperaturaEvaporazione") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T sorgente ingresso lato esterno (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TInglatoEst") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T sorgente uscita lato esterno (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("TUscLatoEst") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="T ingresso fluido utenze (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TIngLatoUtenze") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T uscita fluido utenze (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TUscLatoUtenze") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T uscita fluido (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("TUscitaFluido") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="T bulbo umido aria (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TBulboUmidoAria") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T ingresso fluido sorgente esterna (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TIngressoLatoEsterno") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T uscita fluido sorgente esterna (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("TUscitaLatoEsterno") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="T ingresso fluido alla macchina (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%# Eval("TIngressoLatoMacchina") %>'  />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="T uscita fluido alla macchina (°C) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/>
                            <asp:Label runat="server" Text='<%# Eval("TUscitaLatoMacchina") %>' />
                                    </asp:TableCell>
                                    <asp:TableCell>
                            <asp:Label runat="server" Text="Potenza assorbita (kW) :&nbsp;"  Font-Bold="true" ForeColor="#000000"/> 
                            <asp:Label runat="server" Text='<%# Eval("PotenzaAssorbita") %>' />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell Height="20">
                            <asp:Label runat="server" Text="Filtri puliti :&nbsp;"  Font-Bold="true" ForeColor="#000000" />
                            <asp:Label runat="server" Text='<%#DataUtilityCore.UtilityApp.ValueSiNo(Eval("FiltriPuliti").ToString()) %>'  />
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
