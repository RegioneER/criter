<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_ResponsabiliImpiantoSearch.aspx.cs" Inherits="LIM_ResponsabiliImpiantoSearch" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        RICERCA LIBRETTI NOMINE TERZO RESPONSABILE
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.Int32"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
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
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodicePod" AssociatedControlID="txtCodicePod" Text="Codice POD" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodicePod" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloCodicePdr" AssociatedControlID="txtCodicePdr" Text="Codice PDR" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodicePdr" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
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

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblResponsabile" AssociatedControlID="txtResponsabile" Text="Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtResponsabile" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblCfPIvaResponsabile" AssociatedControlID="txtCfPIvaResponsabile" Text="C.F./P.IVA Responsabile" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCfPIvaResponsabile" Width="300" TabIndex="1" MaxLength="20" CssClass="txtClass" />
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
                            OnClick="LIM_LibrettiImpiantiSearch_btnProcess_Click" Text="RICERCA LIBRETTI IMPIANTI" />
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
                                                    <asp:Label ID="lblCodiceTargatura" runat="server" Text='<%# Eval("CodiceTargatura") %> ' />
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
                                                    <asp:Label ID="lblCfPIvaResponsabile" runat="server" Text='<%#Eval("CodiceFiscaleResponsabile") %>' />
                                                    <%#Eval("PartitaIvaResponsabile") %>
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
                                
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgPdf" ToolTip="Visualizza pdf libretto impianto" AlternateText="Visualizza pdf libretto impianto" ImageUrl="~/images/Buttons/pdf.png" TabIndex="1"
                                            OnCommand="RowCommand" CommandName="Pdf" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                    <ItemTemplate>
                                        <asp:ImageButton runat="server" ID="ImgRevocaIncarico" ToolTip="Revoca incarico terzo responsabile sul libretto impianto" AlternateText="Revoca incarico terzo responsabile sul libretto impianto" ImageUrl="~/images/Buttons/delete.png" TabIndex="1" OnClientClick="javascript:return confirm('Confermi di revocare l\'incarico di terzo responsabile sul presente libretto di impianto?')"
                                            OnCommand="RowCommand" CommandName="RevocaIncarico" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDLibrettoImpianto") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
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