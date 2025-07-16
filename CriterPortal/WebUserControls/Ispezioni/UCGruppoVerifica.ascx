<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCGruppoVerifica.ascx.cs" Inherits="WebUserControls_Ispezioni_UCGruppoVerifica" %>

<asp:Table ID="tblGruppoVerifica" runat="server" Width="100%" CssClass="TableClass">
    <asp:TableRow>
        <asp:TableCell>
            <asp:Button runat="server" ID="btnCreaGruppoVerificaAssente" Visible="false" CssClass="buttonSmallRedClass" Width="330" TabIndex="1" OnClick="btnCreaGruppoVerificaAssente_Click" 
                Text="CREA GRUPPO DI VERIFICA ASSENTE"  AlternateText="" ToolTip="CREA GRUPPO DI VERIFICA ASSENTE" 
                OnClientClick="if (!confirm('Confermi di voler creare il gruppo di verifica assente?. Confermi?')) return false;"
                 />
            &nbsp;&nbsp;
           <asp:Button runat="server" ID="btnCancellaIspezione" Visible="false" CssClass="buttonSmallRedClass" Width="330" TabIndex="1" OnClick="btnCancellaIspezione_Click" 
                Text="CANCELLA ISPEZIONE"  AlternateText="" ToolTip="CANCELLA ISPEZIONE" 
                OnClientClick="if (!confirm('Confermi di voler cancellare l\'Ispezione?. Confermi?')) return false;"
                 />
            <br /><br />
            <asp:Button ID="btnRimandaInRicercaIspettoreNoFirmaLAI" runat="server" Visible="false" TabIndex="1" CssClass="buttonSmallRedClass" Width="330"
                 OnClick="btnRimandaInRicercaIspettoreNoFirmaLAI_Click" OnClientClick="javascript:return confirm('Confermi di rimandare l\'Ispezione in ricerca ispettore per inadempienze dell\'ispettore a FIRMARE LA LAI?');" Text="INADEMPIENZA ISPETTORE A FIRMARE LA LAI" />
            &nbsp;&nbsp;
            <asp:Button ID="btnRimandaInRicercaIspettoreNoPianificazione" runat="server" Visible="false" TabIndex="1" CssClass="buttonSmallRedClass" Width="330"
                 OnClick="btnRimandaInRicercaIspettoreNoPianificazione_Click" OnClientClick="javascript:return confirm('Confermi di rimandare l\'Ispezione in ricerca ispettore per inadempienze dell\'ispettore a pianificare la verifica?');" Text="INADEMPIENZA ISPETTORE A PIANIFICARE" />
            &nbsp;&nbsp;
            <asp:Button ID="btnRimandaInRicercaIspettoreNoCorrettaPianificazione" runat="server" Visible="false" TabIndex="1" CssClass="buttonSmallRedClass" Width="330"
                 OnClick="btnRimandaInRicercaIspettoreNoCorrettaPianificazione_Click" OnClientClick="javascript:return confirm('Confermi di rimandare l\'Ispezione in ricerca ispettore per non corretta pianificazione dell\'ispettore?');" Text="NON CORRETTA PIANIFICAZIONE DELL'ISPETTORE" />
        </asp:TableCell>
    </asp:TableRow>
    
    <asp:TableRow>
        <asp:TableCell>
            <dx:ASPxGridView ID="GridGruppoVerifica"
                runat="server"
                AutoGenerateColumns="false"
                KeyFieldName="IDIspezioneGruppoVerifica"
                ClientInstanceName="GridGruppoVerifica"
                EnableCallBacks="false"
                Width="100%"
                OnHtmlRowCreated="GridGruppoVerifica_HtmlRowCreated"
                >
                <SettingsPager PageSize="100"
                    Summary-Visible="false"
                    ShowDefaultImages="false"
                    ShowDisabledButtons="false"
                    ShowNumericButtons="true" />
                <Styles AlternatingRow-CssClass="GridAlternativeItem"
                    Row-CssClass="GridItem"
                    Header-CssClass="GridHeader"
                    Cell-HorizontalAlign="Center"
                    Header-HorizontalAlign="Center" />
                <SettingsBehavior AllowSort="false" AllowFocusedRow="false"  />
                <Columns>
                    <dx:GridViewDataColumn FieldName="IDIspezioneGruppoVerifica" Visible="false" ReadOnly="true" />
                    <dx:GridViewDataColumn FieldName="IDIspettore" Visible="false" ReadOnly="true" />
                    <dx:GridViewDataColumn FieldName="IDStatoPianificazioneIspettore" Visible="false" ReadOnly="true" />
                    <dx:GridViewDataColumn Caption="Ispettore">
                        <DataItemTemplate>
                            <asp:Table ID="tblGruppoVerificaInfoIspettore" runat="server" Width="100%">
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblTitoloIspettore" runat="server" Font-Bold="true" ForeColor="#000000" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblIndirizzoIspettore" runat="server" ForeColor="#000000" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblTelefonoIspettore" runat="server" ForeColor="#000000" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblCellulareIspettore" runat="server" ForeColor="#000000" />
                                    </asp:TableCell>
                                </asp:TableRow>

                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="lblEmailIspettore" runat="server" ForeColor="#000000" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>

                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="NumeroVerificheEffettuate"  Caption="N. Isp"/>
                    <dx:GridViewDataColumn FieldName="Distanza"  Caption="Distanza (km)"/>
                   
                    <dx:GridViewDataColumn Caption="Pianificazione">
                        <DataItemTemplate>
                            <asp:Label ID="lblStatoPianificazioneIspettore" runat="server"  ForeColor="#000000"/>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataCheckColumn FieldName="fInGruppoVerifica" Caption="In Gruppo Verifica">
                        <DataItemTemplate>
                            <dx:ASPxImage runat="server" ID="imgfInGruppoVerifica" OnInit="imgfInGruppoVerifica_Init" />
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>
                    
                    <dx:GridViewDataColumn>
                        <DataItemTemplate>
                            <asp:ImageButton ID="imgSetIspettore" runat="server" ImageUrl="~/images/buttons/Pin.png" OnCommand="imgSetIspettore_Command" CommandName="Set" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneGruppoVerifica") +","+ DataBinder.Eval(Container.DataItem,"IDIspettore")%>'  OnClientClick="javascript:return confirm('Confermi l\'assegnazione manuale dell\'ispezione a questo ispettore? Sarà inviata la LAI e il materiale relativo all\'ispezione.');"/>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                </Columns>
            </dx:ASPxGridView>
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>