<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COM_Raccomandate.aspx.cs" Inherits="COM_Raccomandate" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/WebUserControls/WUC_Progress.ascx" TagPrefix="uc1" TagName="WebUSUpdateProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            
            <asp:Table runat="server" ID="tblAccertamenti" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA RACCOMANDATE/ATTI GIUDIZIARI POSTE ITALIANE
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTipoServizioRaccomandata" AssociatedControlID="rblTipoServizioRaccomandata" Text="Tipologia servizio" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblTipoServizioRaccomandata" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rblTipoServizioRaccomandata_SelectedIndexChanged" RepeatColumns="1" >
                            <asp:ListItem Text="ROL - Raccomandate online" Selected="True" Value="ROL" />
                            <asp:ListItem Text="AGOL - Atto Giudiziario" Value="AGOL" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowTipoRaccomandata">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTipoRaccomandata" AssociatedControlID="rblTipoRaccomandata" Text="Tipo raccomandata" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblTipoRaccomandata" RepeatColumns="2" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rblTipoRaccomandata_SelectedIndexChanged" >
                            <asp:ListItem Selected="True" Text="Tutti" Value="0" />
                            <asp:ListItem Text="Accertamento" Value="1" />
                            <asp:ListItem Text="Revoca accertamento" Value="2" />
                            <asp:ListItem Text="Conferma pianificazione ispezione" Value="3" />
                            <asp:ListItem Text="Annullamento pianificazione ispezione" Value="4" />
                            <asp:ListItem Text="Ripianificazione ispezione" Value="5" />
                            <asp:ListItem Text="Notifica sanzione" Value="6" />
                            <%--<asp:ListItem Text="Raccomandate libere" Value="7" />--%>
                            <asp:ListItem Text="Revoca sanzione" Value="8" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDescrizioneIDRichiesta" AssociatedControlID="txtIDRichiesta" Text="Id raccomandata" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIDRichiesta" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblDescrizioneNumeroRaccomandata" AssociatedControlID="txtNumeroRaccomandata" Text="Numero raccomandata" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtNumeroRaccomandata" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowEsitoDeposito">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloEsitoSpedizione" AssociatedControlID="rblTipoRaccomandata" Text="Esito deposito documento a Poste Italiane" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblEsitoSpedizione" Width="100%" RepeatColumns="3" >
                            <asp:ListItem Selected="True" Text="Tutti" Value="0" />
                            <asp:ListItem Text="Accettate da poste" Value="1" />
                            <asp:ListItem Text="Non accettate da poste" Value="2" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowStatoSpedizioneROL" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoSpedizioneROL" AssociatedControlID="rblStatoRaccomandataROL" Text="Stato spedizione ROL" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblStatoRaccomandataROL" Width="100%" RepeatColumns="1" >
                            <asp:ListItem Selected="True" Text="Tutti" Value="" />
                            <asp:ListItem Text="Codice stato 00: Accettata online" Value="accettata online" />
                            <asp:ListItem Text="Codice stato 01: Consegnato" Value="Consegnato" />
                            <asp:ListItem Text="Codice stato 02: Giacenza" Value="Giacenza" />
                            <asp:ListItem Text="Codice stato 03: In restituzione" Value="In restituzione" />
                            <asp:ListItem Text="Codice stato 04: Consegnato digitalmente" Value="Consegnato digitalmente" />
                            <asp:ListItem Text="Codice stato 99: In lavorazione" Value="In lavorazione" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowStatoSpedizioneAGOL" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoSpedizioneAGOL" AssociatedControlID="rblStatoRaccomandataAGOL" Text="Stato spedizione Atto Giudiziario" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblStatoRaccomandataAGOL" Width="100%" RepeatColumns="1" >
                            <asp:ListItem Selected="True" Text="Tutti" Value="" />
                            <asp:ListItem Text="L - Presa in carico Postel" Value="L" />
                            <asp:ListItem Text="Q - In postalizzazione" Value="Q" />
                            <asp:ListItem Text="S - Stampato" Value="S" />
                            <asp:ListItem Text="W - Errore negli esiti" Value="W" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row2" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodiceAccertamento">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceAccertamento" AssociatedControlID="txtCodiceAccertamento" Text="Codice accertamento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceAccertamento" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodiceIspezione">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceIspezione" AssociatedControlID="txtCodiceIspezione" Text="Codice ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceIspezione" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataInvio" Text="Data invio raccomandata (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInvioDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataInvioDa" ControlToValidate="txtDataInvioDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInvioAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataInvioAl" ControlToValidate="txtDataInvioAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataUltimoAggiornamento" Text="Data ultimo aggiornamento stato (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataUltimoAggiornamentoDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataUltimoAggiornamentoDa" ControlToValidate="txtDataUltimoAggiornamentoDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataUltimoAggiornamentoAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataUltimoAggiornamentoAl" ControlToValidate="txtDataUltimoAggiornamentoAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row6">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row7">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA RACCOMANDATE" OnClick="btnRicerca_Click" 
                            CssClass="buttonClass" Width="300px" TabIndex="1" />&nbsp;&nbsp;
                        <asp:Button runat="server" ID="btnSaveNote" Text="SALVA NOTE" OnClick="btnSaveNote_Click" 
                            CssClass="buttonClass" Width="300px" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="row8">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="10" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDRichiesta"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
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
                                                    <asp:TextBox runat="server" ID="txtNoteRaccomandata" Text='<%# Eval("Note") %>' TextMode="MultiLine" Rows="5" />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CodiceAccertamento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceIspezione" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDIspezioneVisita" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="RisultatoDescrizione" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgSendRaccomandata" ToolTip="Invia Raccomandata" 
                                            OnClientClick="javascript:return confirm('Confermi di inviare nuovamente la raccomandata?');" 
                                            AlternateText="Invia Raccomandata" ImageUrl="~/images/Buttons/send.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="InviaRaccomandata" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDAccertamento") +","+ DataBinder.Eval(Container.DataItem,"IDIspezione") +","+ DataBinder.Eval(Container.DataItem,"IDRaccomandataType") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="fEsitoDepositoRaccomandata" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <uc1:WebUSUpdateProgress runat="server" id="WebUSUpdateProgress" />
    <%--<asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>