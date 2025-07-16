<%@ Page Title="Criter - Ricerca libretti di impianti" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_LibrettiImpiantiSearch.aspx.cs" Inherits="LIM_LibrettiImpiantiSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>RICERCA LIBRETTI IMPIANTI</h2>
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
                        <asp:RequiredFieldValidator ID="rfvASPxComboBox1" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage=""
                            ControlToValidate="ASPxComboBox1">&nbsp;*</asp:RequiredFieldValidator>
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
                        <asp:RequiredFieldValidator ID="rfvASPxComboBox2" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Operatore/Addetto: campo obbligatorio"
                            ControlToValidate="ASPxComboBox2">&nbsp;*</asp:RequiredFieldValidator>
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

                <asp:TableRow runat="server" ID="rowCodicePod">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodicePod" AssociatedControlID="txtCodicePod" Text="Codice POD" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodicePod" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowCodicePdr">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodicePdr" AssociatedControlID="txtCodicePdr" Text="Codice PDR" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodicePdr" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowMatricolaGeneratore">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblMatricolaGeneratore" AssociatedControlID="txtMatricolaGeneratore" Text="Matricola generatore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblTipologieGeneratori" runat="server" TabIndex="1" RepeatDirection="Horizontal" RepeatColumns="4" CssClass="radiobuttonlistClass" />
                        <asp:TextBox runat="server" ID="txtMatricolaGeneratore" Width="200" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIndirizzoImpianto" AssociatedControlID="txtIndirizzoImpianto" Text="Indirizzo/Civico" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtIndirizzoImpianto" Width="200" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                        <asp:TextBox runat="server" ID="txtCivicoImpianto" Width="100" TabIndex="1" MaxLength="20" CssClass="txtClass" />
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

                <asp:TableRow runat="server" ID="rowResponsabileCfPIva">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCfPIvaResponsabile" AssociatedControlID="txtCfPIvaResponsabile" Text="C.F./P.IVA Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCfPIvaResponsabile" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowDataRegistrazione">
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

                <asp:TableRow runat="server" ID="rowGeneratoriDismessi">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloGeneratoriDismessi" AssociatedControlID="rblStatoLibrettoImpianto" Text="Visualizza libretti con generatori dismessi" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBox runat="server" ID="chkGeneratoriDsmessi" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowStatoLibretto">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloStatoLibretto" AssociatedControlID="rblStatoLibrettoImpianto" Text="Stato libretti impianti" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoLibrettoImpianto" runat="server" TabIndex="1" RepeatDirection="Vertical" RepeatColumns="2" CssClass="radiobuttonlistClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="LIM_LibrettiImpiantiSearch_btnProcess" TabIndex="1" runat="server" CssClass="buttonClass" Width="200"
                            OnClick="LIM_LibrettiImpiantiSearch_btnProcess_Click" Text="RICERCA LIBRETTI IMPIANTI" />&nbsp;
                        <asp:Button ID="LIM_LibrettiImpianti_btnAdd" runat="server" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="LIM_LibrettiImpianti_btnAdd_Click" Text="NUOVO LIBRETTO IMPIANTO" />
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
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDLibrettoImpianto"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle HorizontalAlign="Left" CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoManutentore" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoAzienda" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDStatoLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceTargatura" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Libretti di Impianti">
                                    <ItemTemplate>
                                        <asp:Table ID="tblInfoLibretti" Width="700" runat="server">
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Azienda:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" Width="600">
                                                    <asp:Label ID="lblSoggettoAzienda" runat="server" Text='<%#Eval("SoggettoAzienda") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Operatore/Addetto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" ColumnSpan="4" Width="600">
                                                    <asp:Label ID="lblSoggettoManutentore" runat="server" Text='<%#Eval("SoggettoManutentore") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            
                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Codice targatura:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                    <asp:HyperLink runat="server" ID="lnkLibretto" Target="_blank" Text='<%# Eval("CodiceTargatura") %>' />
                                                    <%# Eval("NumeroRevisione") is System.DBNull  ? "" : " - Rev " + Eval("NumeroRevisione")  %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Stato libretto:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblStatoLibrettoImpianto" runat="server" Text='<%# Eval("StatoLibrettoImpianto") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Comune:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                    <asp:Label ID="lblCodiceCatastale" runat="server" Text='<%#Eval("CodiceCatastale") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Indirizzo:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblIndirizzo" runat="server" Text='<%#Eval("Indirizzo") %>' />&nbsp;
                                                    <%#Eval("Civico") %>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Responsabile:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                    <asp:Label ID="lblResponsabile" runat="server" Text='<%#Eval("NomeResponsabile") %>' />&nbsp;
                                                    <%#Eval("CognomeResponsabile") %>&nbsp;<%#Eval("RagioneSocialeResponsabile") %>
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>C.F./ P.Iva:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblCfPIvaResponsabile" runat="server" Text='<%#Eval("CodiceFiscaleResponsabile") %>' /><%#Eval("PartitaIvaResponsabile") %>
                                                </asp:TableCell>
                                            </asp:TableRow>

                                            <asp:TableRow Width="700">
                                                <asp:TableCell Width="100">
                                                        <b>Numero PDR:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="330" ColumnSpan="3">
                                                    <asp:Label ID="lblPdr" runat="server" Text='<%#Eval("NumeroPDR") %>' />
                                                </asp:TableCell>
                                                <asp:TableCell Width="100">
                                                        <b>Numero POD:&nbsp;</b>
                                                </asp:TableCell>
                                                <asp:TableCell HorizontalAlign="Left" Width="130">
                                                    <asp:Label ID="lblPod" runat="server" Text='<%#Eval("NumeroPOD") %>' />
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                        <asp:Image runat="server" ID="imgBarcode" Width="30px" Height="30px" CssClass="imgBarcode" 
                                            ToolTip="QrCode Codice targatura" AlternateText="QrCode Codice targatura" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgEdit" ToolTip="Modifica libretto impianto" AlternateText="Modifica libretto impianto" ImageUrl="~/images/Buttons/edit.png"
                                            OnCommand="RowCommand" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") %>' TabIndex="1" />
                                        <asp:ImageButton runat="server" ID="ImgView" ToolTip="Visualizza dati libretto impianto" AlternateText="Visualizza dati libretto impianto" ImageUrl="~/images/Buttons/view.png"
                                            OnCommand="RowCommand" CommandName="View" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") %>' TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgDelete" AlternateText="Cancella" ToolTip="Cancella"
                                            OnCommand="RowCommand" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto")  +","+ DataBinder.Eval(Container.DataItem,"fAttivo").ToString() %>' TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgRevision" ToolTip="Visualizza versioni precedenti libretto impianto" AlternateText="Visualizza versioni precedenti libretto impianto" ImageUrl="~/images/Buttons/revision.png"
                                            OnCommand="RowCommand" CommandName="Revision" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") +","+ DataBinder.Eval(Container.DataItem,"IDTargaturaImpianto") %>'  TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf libretto impianto" AlternateText="Visualizza pdf libretto impianto" ImageUrl="~/images/Buttons/pdf.png"
                                            OnCommand="RowCommand" CommandName="Pdf" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") %> ' TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NumeroRevisione" Visible="false" ReadOnly="True" />
                            </Columns>
                            <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                        </asp:DataGrid>
                    </asp:TableCell>
                </asp:TableRow>

            </asp:Table>
            <!-- -->
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