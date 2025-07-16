<%@ Page Title="Codici targatura impianti" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_TargatureImpiantiSearch.aspx.cs" Inherits="LIM_TargatureImpiantiSearch" %>
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
                        RICERCA E STAMPA CODICI TARGATURA IMPIANTI
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Theme="Default" TabIndex="1"
                            AutoPostBack="true"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnSelectedIndexChanged="ASPxComboBox_OnSelectedIndexChanged"
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
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="rfvASPxComboBox1" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Azienda: campo obbligatorio"
                            ControlToValidate="ASPxComboBox1">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
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
                        <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloCodiciLotto" AssociatedControlID="ddlCodiciLotto" Text="Codici lotto targature" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ddlCodiciLotto" runat="server" Theme="Default" TabIndex="1"
                            TextField="DescrizioneLotto" ValueField="CodiceLotto"
                            Width="370px" DropDownWidth="370px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceTargatura" AssociatedControlID="txtCodiceTargatura" Text="Codice targatura impianto" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceTargatura" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloPageSize" AssociatedControlID="ASPxComboBox3" Text="Visualizzazione risultati per pagina" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ASPxComboBox3_SelectedIndexChanged" Theme="Default" TabIndex="1"
                            Width="300px" DropDownWidth="300px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                            <Items>
                                <dx:ListEditItem Text="Visualizza tutte Targature impianti" Value="10000" />
                                <dx:ListEditItem Text="Visualizza 12 Targature impianti per pagina" Value="12" Selected="true" />
                                <dx:ListEditItem Text="Visualizza 24 Targature impianti per pagina" Value="24" />
                                <dx:ListEditItem Text="Visualizza 36 Targature impianti per pagina" Value="36" />
                                <dx:ListEditItem Text="Visualizza 48 Targature impianti per pagina" Value="48" />
                            </Items>
                        </dx:ASPxComboBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloStatoLibretto" AssociatedControlID="rblStatoTargaturaCodiciImpianto" Text="Stato codici targature impianti" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoTargaturaCodiciImpianto" TabIndex="1" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Value="0" Text="Visualizza tutti i codici targatura" Selected="True" />
                            <asp:ListItem Value="1" Text="Visualizza solo i codici targatura liberi" />
                            <asp:ListItem Value="2" Text="Visualizza solo i codici targatura legati ad un libretto d'impianto" />
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
                        <asp:Button ID="LIM_TargatureImpiantiSearch_btnProcess" runat="server" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="LIM_TargatureImpiantiSearch_btnProcess_Click" Text="RICERCA CODICI TARGATURA" />                   
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow ID="rowStampa" runat="server" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Table runat="server" ID="tblStampa" CssClass="TableClassNoBorder">
                            <asp:TableRow>
                                <asp:TableCell Width="600" CssClass="riempimento">
                                    <dx:ASPxRadioButtonList ID="rblFormatoStampa" runat="server" 
                                        RepeatColumns="3" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rblFormatoStampa_SelectedIndexChanged" >
                                        <Items>
                                            <dx:ListEditItem Value="0" Text="Formato A4" Selected="true" ImageUrl="~/images/buttons/imgFormatoA4.png" />
                                            <dx:ListEditItem Value="1" Text="Formato A7" ImageUrl="~/images/buttons/imgFormatoA7.png" />
                                            <dx:ListEditItem Value="2" Text="Formato Etichetta" ImageUrl="~/images/buttons/imgFormatoEtichetta.png" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </asp:TableCell>
                                <asp:TableCell Width="300" CssClass="riempimento" HorizontalAlign="Center">
                                    <asp:Button runat="server" ID="btnStampa" CssClass="buttonClass" Text="STAMPA CODICI TARGATURA"
                                        Width="250px" TabIndex="1" />
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                                    &nbsp;
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
                
                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblCount" Visible="false" />
                        <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                            CellSpacing="1" CellPadding="3" UseAccessibleHeader="true" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDTargaturaImpianto"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDTargaturaImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDLibrettoImpianto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggetto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoDerived" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceTargatura" Visible="false" ReadOnly="True" />
                                <%--<asp:BoundColumn DataField="CodiceRandom" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="Anno" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteInserimento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="DataInserimento" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDUtenteUltimaModifica" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="DataUltimaModifica" Visible="false" ReadOnly="True" />--%>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="800" HeaderText="Elenco Codici Targatura Impianto">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td width="700">
                                                    <b>Azienda:&nbsp;</b><%#Eval("Soggetto") %>&nbsp;<%#Eval("IndirizzoSoggetto") %><br />
                                                    <br />
                                                    <b>Operatore/Addetto:&nbsp;</b><%#Eval("SoggettoManutentore") %><br />
                                                    <br />
                                                    <b>Codice targatura impianto:&nbsp;</b><%#Eval("CodiceTargatura") %>&nbsp;-&nbsp;Lotto n.<b><%#Eval("CodiceLotto").ToString() %></b><br />
                                                </td>
                                                <td width="100">
                                                    <asp:Image runat="server" ID="imgBarcode" Width="70px" Height="70px" CssClass="imgBarcode" ToolTip="Barcode Codice Targatura" AlternateText="Barcode Codice Targatura" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="800" colspan="2">
                                                    <asp:Label runat="server" ID="lblLibrettoImpianto" Visible="false" Text="Codice Targatura legato ad un libretto di impianto" ForeColor="Green" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" >
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezioneAll" AutoPostBack="true" OnCheckedChanged="chkSelezioneAll_CheckedChanged" ToolTip="Seleziona tutti i codici targatura per la stampa" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" AutoPostBack="true" OnCheckedChanged="chkSelezione_CheckedChanged" TabIndex="1"/>
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
