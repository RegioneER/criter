<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_IspezioniPianificazione.aspx.cs" Inherits="VER_IspezioniPianificazione" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.2" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
     <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
             <asp:Table runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        PIANIFICAZIONE VISITA ISPETTIVA
                    </asp:TableCell>
                </asp:TableRow>

                 <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        <dx:ASPxGridView ID="gridIspezioniNellaVisita" ClientInstanceName="gridIspezioniNellaVisita" runat="server"
                            KeyFieldName="IDIspezione" 
                            Width="890px" Font-Size="6"
                            Border-BorderStyle="None"
                            BorderBottom-BorderStyle="None"
                            BorderLeft-BorderStyle="None"
                            BorderRight-BorderStyle="None"
                            BorderTop-BorderStyle="None"
                            
                            Styles-AlternatingRow-BackColor="#ffedad"
                            Styles-Row-CssClass="GridItem"
                            Styles-Header-HorizontalAlign="Center"
                            Styles-Header-Font-Bold="true"
                            Styles-Table-BackColor="#ffcc3d"
                            Styles-Header-BackColor="#ffcc3d"
                            Styles-EmptyDataRow-BackColor="#ffffff"
                            OnHtmlRowCreated="gridIspezioniNellaVisita_HtmlRowCreated"  
                            >
                            <SettingsPager ShowDefaultImages="false" PageSize="10" ShowDisabledButtons="false" Summary-Visible="false" />
                            <SettingsText EmptyDataRow="Nessuna ispezione nella visita ispettiva" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="IDIspezione" VisibleIndex="0" Visible="false" />
                                <dx:GridViewDataColumn FieldName="IDIspezioneVisita" VisibleIndex="1" Visible="false" />
                                <dx:GridViewDataColumn FieldName="IDOrarioDa" VisibleIndex="2" Visible="false" />
                                <dx:GridViewDataColumn FieldName="IDOrarioA" VisibleIndex="3" Visible="false" />
                                <dx:GridViewDataColumn FieldName="CodiceIspezione" VisibleIndex="4" />
                                <dx:GridViewDataColumn FieldName="StatoIspezione" VisibleIndex="5" /> 
                                <dx:GridViewDataColumn FieldName="Ispettore" VisibleIndex="6" />
                                <dx:GridViewDataTextColumn VisibleIndex="7" CellStyle-HorizontalAlign="Left" Caption="Responsabile">
                                    <DataItemTemplate>
                                        <%# Eval("NomeResponsabile")%> <%# Eval("CognomeResponsabile")%> <%# Eval("RagioneSocialeResponsabile")%>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="8" Caption="Indirizzo Impianto">
                                    <DataItemTemplate>
                                        <%# Eval("Indirizzo")%>, <%# Eval("Civico")%> <%# Eval("Comune")%> (<%# Eval("SiglaProvincia")%>) 
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="9" Caption="Dati impianto">
                                    <DataItemTemplate>
                                        Potenza:&nbsp;<%# Eval("PotenzaTermicaUtileNominaleKw")%><br />
                                        Combustibile:&nbsp;<%# Eval("TipologiaCombustibile") %> 
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                
                                <dx:GridViewDataCheckColumn FieldName="fIspezioneNonSvolta" Caption="Isp. non svolta" VisibleIndex="10" UnboundType="Boolean">  
                                    <PropertiesCheckEdit AllowGrayed="false" ValueChecked="true" ValueUnchecked="false" ValueType="System.Boolean"></PropertiesCheckEdit>  
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="fIspezioneRipianificata" Caption="Isp. ripianificata" VisibleIndex="11" UnboundType="Boolean">  
                                    <PropertiesCheckEdit AllowGrayed="false" ValueChecked="true" ValueUnchecked="false" ValueType="System.Boolean"></PropertiesCheckEdit>  
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="12" Caption="Data/Orario Ispezione">
                                    <DataItemTemplate>
                                        <%# String.Format("{0:dd/MM/yyyy}", Eval("DataIspezione"))%><br /> <%# Eval("OrarioDa")%> - <%# Eval("OrarioA") %> 
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataColumn VisibleIndex="13">
                                    <DataItemTemplate>
                                        <asp:ImageButton runat="server" ImageUrl="~/images/Buttons/EditSmall.png" Visible="false" ID="ImgPianificazione"
                                            OnCommand="imgConfirm_Command" 
                                            CommandName="EditPianificazione"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezione") +","+ DataBinder.Eval(Container.DataItem,"IDIspezioneVisita")+","+ DataBinder.Eval(Container.DataItem,"fIspezioneRipianificata")+","+ DataBinder.Eval(Container.DataItem,"CodiceIspezione")+","+ DataBinder.Eval(Container.DataItem,"fTerzoResponsabile") %>'/>
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="14">
                                    <DataItemTemplate>
                                        <asp:ImageButton ID="ImgConferma" runat="server" Visible="false" OnCommand="imgConfirm_Command" 
                                            CommandName="ConfermaPianificazione" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezione") +","+ DataBinder.Eval(Container.DataItem,"IDIspezioneVisita")+","+ DataBinder.Eval(Container.DataItem,"fIspezioneRipianificata")+","+ DataBinder.Eval(Container.DataItem,"CodiceIspezione")+","+ DataBinder.Eval(Container.DataItem,"fTerzoResponsabile") %>'
                                            ImageUrl="~/images/Buttons/saveSmall.png" 
                                            OnClientClick="javascript:return confirm('Confermi la pianificazione della verifica? Effettuando questa operazione l\'ispezione cambierà di stato in confermata e verrà inviata una raccomandata tramite Poste Italiane al Responsabile di impianto.Confermi?');" />

                                        <asp:ImageButton ID="ImgConfermaRipianificazione" runat="server" Visible="false" OnCommand="imgConfirm_Command" 
                                            CommandName="ConfermaRipianificazione" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezione") +","+ DataBinder.Eval(Container.DataItem,"IDIspezioneVisita")+","+ DataBinder.Eval(Container.DataItem,"fIspezioneRipianificata")+","+ DataBinder.Eval(Container.DataItem,"CodiceIspezione")+","+ DataBinder.Eval(Container.DataItem,"fTerzoResponsabile") %>'
                                            ImageUrl="~/images/Buttons/UndoSmall.png" 
                                            OnClientClick="javascript:return confirm('Confermi la ripianificazione della verifica? Effettuando questa operazione l\'ispezione cambierà di stato in da pianificare.Confermi?');" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn VisibleIndex="15">
                                    <DataItemTemplate>
                                        <asp:ImageButton runat="server" ImageUrl="~/images/Buttons/PdfSmall.png" Visible="false" ID="ImgPianificazioneDocuments" />
                                    </DataItemTemplate>
                                </dx:GridViewDataColumn>
                                <dx:GridViewDataColumn FieldName="fIspezioneNonSvolta" VisibleIndex="16" Visible="false" />
                                <dx:GridViewDataColumn FieldName="fIspezioneNonSvolta2" VisibleIndex="17" Visible="false" />
                             </Columns>
                        </dx:ASPxGridView>
                        <%--<dx:ASPxPopupControl ID="WindowsPianificazione" runat="server" CloseAction="OuterMouseClick"
                            Modal="True" AccessibilityCompliant="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                            ClientInstanceName="WindowsPianificazione" HeaderText="Pianificazione verifica ispettiva"
                            AllowDragging="True" PopupAnimationType="Fade" Width="905px" Height="600px" AllowResize="True">
                            <ClientSideEvents CloseUp="function(s, e) 
                                { 
                                    window.location.reload(true);
                                }" />
                        </dx:ASPxPopupControl>
                        <script type="text/javascript">
                            function OpenPopupWindows(element, key)
                            {
                                var url = 'VER_IspezioniPianificazioneDetails.aspx?IDIspezione=' + key;
                                WindowsPianificazione.SetContentUrl(url)
                                WindowsPianificazione.Show();
                            }
                        </script>--%>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                 </asp:TableRow>
                 <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnIspezioni" Text="TORNA ALLA LISTA ISPEZIONI" OnClick="btnIspezioni_Click"
                            CssClass="buttonClass" Width="250px" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
         </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>