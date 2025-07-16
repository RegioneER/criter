<%@ Page Title="Criter - Ricerca rapporti di controllo tecnico" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RCT_RapportoDiControlloTecnicoSearch.aspx.cs" Inherits="RCT_RCT_RapportoDiControlloTecnicoSearch" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Src="~/WebUserControls/RapportiControlloTecnico/UCBolliniView.ascx" TagPrefix="uc1" TagName="UCBolliniView" %>

<asp:Content runat="server" ID="HeadContent" ContentPlaceHolderID="head"></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="contentDisplay" ID="Content">
    <asp:UpdatePanel runat="server" ID="panel1">
        <ContentTemplate>
            <asp:Table runat="server" Width="100%">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA RAPPORTI DI CONTROLLO TECNICO
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowAzienda">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                            AutoPostBack="true"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.Int32"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnSelectedIndexChanged="ASPxComboBox_OnSelectedIndexChanged"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1} {2}"
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
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                        <asp:Label ID="lblApiKey" runat="server" Visible="false" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowManutentore">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPersona" AssociatedControlID="ASPxComboBox2" Text="Operatore/Addetto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Theme="Default" TabIndex="1"
                            TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32"
                            Width="350px" DropDownWidth="300px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblIDSoggettoDerived" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggettoDerived" runat="server" Visible="false" Font-Bold="true" />
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
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloDatiCatastali" AssociatedControlID="ASPxComboBox3" Text="Dati Catastali" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:Table ID="tblCodiceCatastale" Width="500" runat="server">
                            <asp:TableRow>
                                <asp:TableCell Width="150" HorizontalAlign="Left">
                                        Comune
                                </asp:TableCell>
                                <asp:TableCell Width="350">
                                    <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" Theme="Default" TabIndex="1"
                                        EnableCallbackMode="true"
                                        CallbackPageSize="30"
                                        IncrementalFilteringMode="Contains"
                                        ValueType="System.String"
                                        ValueField="IDCodiceCatastale"
                                        OnItemsRequestedByFilterCondition="ASPxComboBox3_OnItemsRequestedByFilterCondition"
                                        OnItemRequestedByValue="ASPxComboBox3_OnItemRequestedByValue"
                                        OnButtonClick="ASPxComboBox3_ButtonClick"
                                        TextFormatString="{0}"
                                        Width="250px"
                                        AllowMouseWheel="true">
                                        <ItemStyle Border-BorderStyle="None" />
                                        <Buttons>
                                            <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                                        </Buttons>
                                        <Columns>
                                            <dx:ListBoxColumn FieldName="Comune" Caption="Comune" Width="250" />
                                        </Columns>
                                    </dx:ASPxComboBox>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="150" HorizontalAlign="Left">
                                        Foglio
                                </asp:TableCell>
                                <asp:TableCell Width="350">
                                    <asp:TextBox ID="txtFoglio" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="150">
                                            Mappale
                                </asp:TableCell>
                                <asp:TableCell Width="350">
                                    <asp:TextBox ID="txtMappale" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="150">
                                            Subalterno
                                </asp:TableCell>
                                <asp:TableCell Width="350">
                                    <asp:TextBox ID="txtSubalterno" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="150">
                                            Identificativo
                                </asp:TableCell>
                                <asp:TableCell Width="350">
                                    <asp:TextBox ID="txtIdentificativo" runat="server" TabIndex="1" CssClass="txtClass" Width="50" />
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowResponsabile">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblResponsabile" AssociatedControlID="txtResponsabile" Text="Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtResponsabile" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowStatoRapporto" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="RCT_RapportoDiControllo_lblTitoloStato" AssociatedControlID="rblStatoRapportoDiControllo" Text="Stato rapporti di controllo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoRapportoDiControllo" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTipologieRapportiDiControllo" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="RCT_RapportoDiControllo_lblTipologieRapportiDiControllo" AssociatedControlID="rblTipologieRapportoDiControllo" Text="Tipologie rapporti di controllo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologieRapportoDiControllo" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowTipologieControllo" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="RCT_RapportoDiControllo_lblTipologieControllo" AssociatedControlID="rblTipologieControllo" Text="Tipologie di controllo" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologieControllo" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDataRegistrazione" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataRegistrazione" Text="Data registrazione (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataRegistrazioneDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataRegistrazioneDa" ControlToValidate="txtDataRegistrazioneDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataRegistrazioneAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataRegistrazioneAl" ControlToValidate="txtDataRegistrazioneAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDataFirma" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataFirma" Text="Data firma rapporto (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataFirmaDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataFirmaDa" ControlToValidate="txtDataFirmaDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataFirmaAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataFirmaAl" ControlToValidate="txtDataFirmaAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowDataEsecuzioneVerifica" runat="server">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataEsecuzioneVerifica" Text="Data esecuzione verifica (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataEsecuzioneVerificaDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataEsecuzioneVerificaDa" ControlToValidate="txtDataEsecuzioneVerificaDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataEsecuzioneVerificaAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataEsecuzioneVerificaAl" ControlToValidate="txtDataEsecuzioneVerificaAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow ID="rowCriticitaFilter" runat="server" Visible="false">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCriticita" AssociatedControlID="cblCriticita" Text="Criticità" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBoxList runat="server" ID="cblCriticita" RepeatColumns="1" TabIndex="1" CssClass="checkboxlistClass" RepeatLayout="Table">
                            <asp:ListItem Text="Visualizza gli RCT con Osservazioni" Value="0" />
                            <asp:ListItem Text="Visualizza gli RCT con Raccomandazioni" Value="1" />
                            <asp:ListItem Text="Visualizza gli RCT con Prescrizioni" Value="2" />
                            <asp:ListItem Text="Visualizza gli RCT con Impianto non può funzionare" Value="3" />
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
                        <asp:Button runat="server" ID="btnRicerca" Text="RICERCA RAPPORTI DI CONTROLLO" OnClick="btnRicerca_Click"
                            CssClass="buttonClass" Width="300px" TabIndex="1" />&nbsp;
                         <asp:Button runat="server" ID="btnNuovo" Text="NUOVO RAPPORTO DI CONTROLLO" OnClick="btnNuovo_Click"
                             CssClass="buttonClass" Width="300px" TabIndex="1" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" PageSize="10" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDRapportoControlloTecnico"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDRapportoControlloTecnico" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoManutentore" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoAzienda" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoRapportoDiControllo" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceTargatura" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Rapporti di Controllo Tecnico">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfoRapporti" Width="700" runat="server">
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Azienda:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="230">
                                                    <asp:Label ID="lblSoggettoAzienda" runat="server" Text='<%#Eval("SoggettoAzienda") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Operatore/Addetto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" Width="230">
                                                    <asp:Label ID="lblSoggettoManutentore" runat="server" Text='<%#Eval("SoggettoManutentore") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Responsabile:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="5">
                                                    <asp:Label ID="lblNomeResponsabile" runat="server" Text='<%#Eval("NomeResponsabile") %>' />&nbsp;
                                                    <asp:Label ID="lbCognomeResponsabile" runat="server" Text='<%#Eval("CognomeResponsabile") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Codice targatura:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330">
                                                    <asp:HyperLink runat="server" ID="lnkRapportoControllo" Target="_blank" Text='<%# Eval("CodiceTargatura") %>' />
                                                   <%-- <asp:Label ID="lblCodiceTargatura" runat="server" Text='<%# Eval("CodiceTargatura") %> ' />--%>
                                                    <%# Eval("NumeroRevisione") is System.DBNull  ? "" : " - Rev " + Eval("NumeroRevisione")  %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                    <b>Generatore:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblGeneratorePrefisso" runat="server" Text='<%#Eval("Prefisso") %>' />
                                                    <asp:Label ID="lblGeneratoreCodiceProgressivo" runat="server" Text='<%#Eval("CodiceProgressivo") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Data&nbsp;controllo:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblDataControllo" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataControllo")) %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Stato&nbsp;rapporto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330">
                                                    <asp:Label ID="lblStatoLibrettoImpianto" runat="server" Text='<%# Eval("StatoRapportoDiControllo") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                    <b>Tipologia&nbsp;rapporto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblTipologiaRapporto" runat="server" Text='<%# Eval("DescrizioneRCT") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Tipo&nbsp;controllo:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblTipoRapportoControllo" runat="server" Text='<%# Eval("TipologiaControllo") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Data&nbsp;firma:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330">
                                                        <asp:Label ID="lblDataFirma" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataFirma")) %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                    &nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                     &nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                    &nbsp;
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    &nbsp;
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="500">
                                                <asp:TableCell ColumnSpan="6">
                                                        <uc1:UCBolliniView runat="server" ID="UCSBolliniView" 
                                                            IDRapportoControlloTecnico='<%# (long)Eval("IDRapportoControlloTecnico") %>'
                                                         />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                 <asp:TemplateColumn >
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgBarcode" Width="30px" Height="30px" CssClass="imgBarcode" 
                                            ToolTip="QrCode Codice targatura" AlternateText="QrCode Codice targatura" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgEdit" ToolTip="Modifica rapporto di controllo" AlternateText="Modifica rapporto di controllo" ImageUrl="~/images/Buttons/edit.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDRapportoControlloTecnico") +","+ DataBinder.Eval(Container.DataItem,"IDTipologiaRCT") +","+ DataBinder.Eval(Container.DataItem,"IDSoggettoAzienda") +","+ DataBinder.Eval(Container.DataItem,"IDSoggettoManutentore") %>' />
                                        <asp:ImageButton runat="server" ID="ImgView" ToolTip="Visualizza dati rapporto di controllo" AlternateText="Visualizza dati rapporto di controllo" ImageUrl="~/images/Buttons/view.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDRapportoControlloTecnico") +","+ DataBinder.Eval(Container.DataItem,"IDTipologiaRCT") +","+ DataBinder.Eval(Container.DataItem,"IDSoggettoAzienda") +","+ DataBinder.Eval(Container.DataItem,"IDSoggettoManutentore") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgDelete" ToolTip="Cancella il rapporto di controllo" AlternateText="Cancella il rapporto di controllo" ImageUrl="~/images/Buttons/delete.png" OnClientClick="javascript:return confirm('Confermi la cancellazione del rapporto di controllo tecnico?')" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDRapportoControlloTecnico") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf rapporto di controllo" AlternateText="Visualizza pdf rapporto di controllo" ImageUrl="~/images/Buttons/pdf.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="Pdf" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDRapportoControlloTecnico") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="guidInteroImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTipologiaRCT" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
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
