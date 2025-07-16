<%@ Page Language="C#" AutoEventWireup="true" Title="Gestione Portafoglio" MasterPageFile="~/MasterPage.master" CodeFile="MNG_Portafoglio.aspx.cs" Inherits="MNG_Portafoglio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelPortafoglio" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA MOVIMENTI PORTAFOGLIO AZIENDA
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1} {2}" BorderBottom-BorderStyle="None"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="CodiceSoggetto" Caption="Codice" Width="50" />
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Azienda" Width="200" />
                                <dx:ListBoxColumn FieldName="IndirizzoSoggetto" Caption="Indirizzo" Width="250" />
                            </Columns>
                            <ValidationSettings>
                                <RequiredField IsRequired="True" ErrorText="Selezionare l'azienda" />
                            </ValidationSettings>
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="MNG_Portafoglio_lblTitoloDataMovimento" Text="Data Movimento (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox TabIndex="1" runat="server" Width="90" MaxLength="10" CssClass="txtClass" ID="txtDataInizio" />
                        <asp:RegularExpressionValidator
                            ID="rfvtxtDataInizio" ControlToValidate="txtDataInizio" Display="Dynamic" ForeColor="Red" ErrorMessage="Data inizio movimento: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox TabIndex="1" runat="server" Width="90" MaxLength="10" CssClass="txtClass" ID="txtDataFine" />
                        <asp:RegularExpressionValidator
                            ID="rfvtxtDataFine" ControlToValidate="txtDataFine" Display="Dynamic" ForeColor="Red" ErrorMessage="Data fine movimento: inserire la data nel formato gg/mm/aaaa"
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIDMovimento" AssociatedControlID="txtIDMovimento" Text="Codice Movimento" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIDMovimento" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloTipoPortafoglioRicarica" AssociatedControlID="rblTipoPortafoglioRicarica" Text="Tipo di ricarica portafoglio" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipoPortafoglioRicarica" TabIndex="1" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Value="0" Text="Visualizza tutti i tipi di ricarica" Selected="True" />
                            <asp:ListItem Value="1" Text="Visualizza solo le ricariche tramite PayER" />
                            <asp:ListItem Value="2" Text="Visualizza solo le ricariche tramite Bonifico" />
                            <asp:ListItem Value="3" Text="Visualizza solo le ricariche tramite Contanti" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnReloadPortafoglio" runat="server" Text="RICERCA MOVIMENTI PORTAFOGLIO"
                            OnClick="btnLoadPortafoglio_Click" CssClass="buttonClass" Width="300" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" Visible="false" ID="rowSaldoPortafoglio">
                    <asp:TableCell Width="900" ColumnSpan="2" HorizontalAlign="Right" CssClass="riempimento">
                        <asp:Label runat="server" ID="MNG_Portafoglio_lblTitoloSaldoTotale" Text="Totale saldo portafoglio: " />
                        <b>&euro;</b>
                        <asp:Label ID="lblSaldoPortafoglio" runat="server" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:GridView ID="GridViewDataPortafoglio"
                            runat="server"
                            AllowPaging="True"
                            AllowSorting="True"
                            CurrentSortField="DataRegistrazione"
                            CurrentSortDir="ASC"
                            AutoGenerateColumns="False"
                            DataKeyNames="IDMovimento"
                            OnRowDataBound="GridViewDataPortafoglio_RowDataBound"
                            OnSorting="GridViewDataPortafoglio_Sorting"
                            PageSize="10"
                            OnPageIndexChanging="GridViewDataPortafoglio_PageIndexChanging"
                            ShowFooter="False"
                            CssClass="si_grid"
                            CellSpacing="0"
                            UseAccessibleHeader="true">
                            <Columns>
                                <asp:BoundField DataField="Utente" HeaderText="Azienda" ReadOnly="True" SortExpression="Utente" />
                                <asp:BoundField DataField="DataRegistrazione" HeaderText="Data Movimento" ReadOnly="True" SortExpression="DataRegistrazione" />
                                <asp:TemplateField HeaderText="Importo" InsertVisible="False" SortExpression="Importo">
                                    <ItemTemplate>
                                        <asp:Label ID="lblImporto" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />

                                    <FooterTemplate>
                                       <asp:Label ID="lblSaldoPortafoglio" runat="server" Font-Bold="true" />
                                    </FooterTemplate>
                                    <FooterStyle Wrap="False" HorizontalAlign="Right"/>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Descrizione" InsertVisible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDescrizione" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ricevuta" InsertVisible="False">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" Visible="False" ID="lnkRicevuta">Ricevuta</asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Esito" InsertVisible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEsito" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="IDMovimento" HeaderText="Codice Movimento" ReadOnly="True" SortExpression="IDMovimento">
                                    <ItemStyle CssClass="toUpper" />
                                </asp:BoundField>
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" />
                        </asp:GridView>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReloadPortafoglio" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>