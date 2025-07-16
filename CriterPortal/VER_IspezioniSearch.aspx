<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_IspezioniSearch.aspx.cs" Inherits="VER_IspezioniSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            <asp:Table runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA ISPEZIONI
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIspettore" AssociatedControlID="cmbIspettore" Text="Ispettore" />
                    </asp:TableCell>
                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbIspettore" TabIndex="1"
                            Theme="Default"
                            AutoPostBack="false"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="cmbIspettore_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="cmbIspettore_OnItemRequestedByValue"
                            OnButtonClick="cmbIspettore_ButtonClick"
                            TextFormatString="{0}"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Ispettore" Width="400" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" ForeColor="Green" />                   
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow runat="server" ID="rowProgrammaIspezione" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloProgrammaIspezione" AssociatedControlID="ASPxComboBox2" Text="Programma Ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Theme="Default" TabIndex="1"
                            AutoPostBack="false"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.Int32"
                            ValueField="IDProgrammaIspezione"
                            OnItemsRequestedByFilterCondition="ASPxComboBox2_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox2_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox2_ButtonClick"
                            TextFormatString="{0}"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Descrizione" Caption="Programma Ispezioni" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceVisitaIspettiva" AssociatedControlID="txtIDISpezioneVisita" Text="Codice visita ispettiva" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIDISpezioneVisita" Width="200" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceIspezione" AssociatedControlID="txtCodiceIspezione" Text="Codice ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceIspezione" Width="150" TabIndex="1" MaxLength="10" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloStatoIspezione" AssociatedControlID="rblStatoIspezione" Text="Stato ispezione" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoIspezione" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataIspezione" Text="Data ispezione (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataIspezioneDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataIspezioneDa" ControlToValidate="txtDataIspezioneDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataIspezioneAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataIspezioneAl" ControlToValidate="txtDataIspezioneAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIspezioniNonSvolte" AssociatedControlID="rblIspezioniNonSvolte" Text="Situazione ispezioni non svolte" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList runat="server" ID="rblIspezioniNonSvolte" TabIndex="1" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Text="Visualizza tutte le ispezioni" Value="0" Selected="True" />
                            <asp:ListItem Text="Visualizza tutte le ispezioni non svolte la prima volta" Value="1" />
                            <asp:ListItem Text="Visualizza tutte le ispezioni non svolte la seconda volta" Value="2" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowCriticitaFilter" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCriticita" AssociatedControlID="cblCriticita" Text="Criticità" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList runat="server" ID="cblCriticita" RepeatColumns="1" TabIndex="1" CssClass="checkboxlistClass" RepeatLayout="Table">
                            <asp:ListItem Text="Visualizza gli RVI con Osservazioni" Value="0" />
                            <asp:ListItem Text="Visualizza gli RVI con Raccomandazioni" Value="1" />
                            <asp:ListItem Text="Visualizza gli RVI con Prescrizioni" Value="2" />
                            <asp:ListItem Text="Visualizza gli RVI con Impianto non può funzionare" Value="3" />
                        </asp:CheckBoxList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA ISPEZIONI" OnClick="btnRicerca_Click"
                            CssClass="buttonClass" Width="250px" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Font-Bold="true" Visible="false" />

                        <dx:ASPxGridView ID="gridIspezioniNellaVisita" ClientInstanceName="gridIspezioniNellaVisita" runat="server"
                            KeyFieldName="IDIspezione" 
                            AutoGenerateColumns="False"
                            EnableCallBacks="False" 
                            
                            OnHtmlRowCreated="gridIspezioniNellaVisita_HtmlRowCreated"
                            Width="850px" Font-Size="6.3"
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
                            >
                            <SettingsPager ShowDefaultImages="false" PageSize="10" ShowDisabledButtons="false" Summary-Visible="false" ShowEmptyDataRows="false" ShowNumericButtons="true" />
                            <SettingsText EmptyDataRow="Nessuna ispezione trovata" />
                            <SettingsBehavior AllowSort="true" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="IDIspezione" VisibleIndex="0" Visible="false" />
                                <dx:GridViewDataColumn FieldName="Descrizione" VisibleIndex="1" Caption="Programma" />
                                <dx:GridViewDataColumn FieldName="IDIspezioneVisita" VisibleIndex="2" Caption="Codice Visita" CellStyle-HorizontalAlign="Center" />
                                <dx:GridViewDataColumn FieldName="CodiceIspezione" VisibleIndex="3" />
                                <dx:GridViewDataColumn FieldName="CodiceTargatura" VisibleIndex="4" />
                                <dx:GridViewDataTextColumn VisibleIndex="5" FieldName="TipologiaCombustibile" Caption="Dati impianto">
                                    <DataItemTemplate>
                                        Potenza:&nbsp;<%# Eval("PotenzaTermicaUtileNominaleKw")%><br />
                                        Combustibile:&nbsp;<%# Eval("TipologiaCombustibile") %><br />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="6" FieldName="StatoIspezione" Caption="Stato Ispezione" />
                                <dx:GridViewDataColumn FieldName="Ispettore" VisibleIndex="7" />
                                <dx:GridViewDataColumn FieldName="TipoIspezione" VisibleIndex="8" />
                                <dx:GridViewDataCheckColumn FieldName="fIspezioneNonSvolta" Caption="Isp. non svolta" VisibleIndex="9" UnboundType="Boolean">  
                                    <PropertiesCheckEdit AllowGrayed="false" ValueChecked="true" ValueUnchecked="false" ValueType="System.Boolean"></PropertiesCheckEdit>  
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataCheckColumn FieldName="fIspezioneNonSvolta2" Caption="Isp. non svolta 2" VisibleIndex="10" UnboundType="Boolean">  
                                    <PropertiesCheckEdit AllowGrayed="false" ValueChecked="true" ValueUnchecked="false" ValueType="System.Boolean"></PropertiesCheckEdit>  
                                </dx:GridViewDataCheckColumn>
                                <dx:GridViewDataTextColumn VisibleIndex="11" FieldName="DataIspezione" Caption="Data/Orario Ispezione">
                                    <DataItemTemplate>
                                        <%# String.Format("{0:dd/MM/yyyy}", Eval("DataIspezione"))%><br /> <%# Eval("OrarioDa")%> - <%# Eval("OrarioA") %>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn UnboundType="String" Caption="" VisibleIndex="12">
                                    <DataItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgView" Visible="false" ImageUrl="~/images/Buttons/editSmall.png" TabIndex="1"
                                                ToolTip="Visualizza ispezione" OnCommand="Ispezioni_Command" 
                                                CommandName="View" 
                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezione") +","+ DataBinder.Eval(Container.DataItem,"IDIspezioneVisita") %>'  />   
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                                 <dx:GridViewDataTextColumn UnboundType="String" Caption="" VisibleIndex="13">
                                    <DataItemTemplate>
                                        <asp:ImageButton runat="server" ID="imgAnnullaIspezione" Visible="false" ImageUrl="~/images/Buttons/deleteSmall.png" TabIndex="1"
                                            ToolTip="Annulla ispezione" OnCommand="Ispezioni_Command" 
                                            OnClientClick="javascript:return confirm('Confermi di annullare l\'ispezione?Effettuando questa operazione verrà inviata una email pec all\'ispettore ed inviata una raccomandata al responsabile impianto.Confermi?');" 
                                            CommandName="AnnullaIspezione" 
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezione") +","+ DataBinder.Eval(Container.DataItem,"IDIspezioneVisita") %>'  />

                                        <asp:ImageButton runat="server" ID="imgFirmaLetteraIncarico" Visible="false" 
                                            ImageUrl="~/images/Buttons/FirmaSmall.png" TabIndex="1"
                                            ToolTip="Firma lettera di Incarico" 
                                            CommandName="FirmaLetteraIncarico" OnCommand="Ispezioni_Command"
                                            CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneVisita") %>' />

                                        <asp:ImageButton runat="server" ID="imgPianificazione" 
                                             ImageUrl="~/images/Buttons/CalendarioSmall.png" 
                                             TabIndex="1" ToolTip="Pianifica verifica ispettiva"  
                                             CommandName="PianificazioneVisita" OnCommand="Ispezioni_Command"
                                             CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDIspezioneVisita") %>' />
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                        </dx:ASPxGridView>

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