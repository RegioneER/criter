<%@ Page Title="Bollini calore pulito" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RCT_BollinoCalorePulitoSearch.aspx.cs" Inherits="RCT_BollinoCalorePulitoSearch" %>
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
                        RICERCA E STAMPA BOLLINI CALORE PULITO
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
                            Width="350px"
                            DropDownWidth="350px">
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
                        <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloCodiciLotto" AssociatedControlID="ddlCodiciLotto" Text="Codici lotto bollini" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ddlCodiciLotto" runat="server" Theme="Default" TabIndex="1"
                            TextField="DescrizioneLotto" ValueField="IDLottoBolliniCalorePulito"
                            Width="370px" DropDownWidth="370px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloCodiceBollino" AssociatedControlID="txtCodiceBollino" Text="Codice bollino calore pulito" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox runat="server" ID="txtCodiceBollino" Width="300" TabIndex="1" MaxLength="36" CssClass="txtClass" />
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
                                <dx:ListEditItem Text="Visualizza tutti Bollini calore pulito" Value="10000" />
                                <dx:ListEditItem Text="Visualizza 12 Bollini calore pulito per pagina" Value="12" Selected="true" />
                                <dx:ListEditItem Text="Visualizza 24 Bollini calore pulito per pagina" Value="24" />
                                <dx:ListEditItem Text="Visualizza 36 Bollini calore pulito per pagina" Value="36" />
                                <dx:ListEditItem Text="Visualizza 48 Bollini calore pulito per pagina" Value="48" />
                            </Items>
                        </dx:ASPxComboBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloStatoBollini" AssociatedControlID="rblStatoBolliniCalorePulito" Text="Stato assegnazione bollini" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoBolliniCalorePulito" TabIndex="1" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Value="0" Text="Visualizza tutti i bollini" Selected="True" />
                            <asp:ListItem Value="1" Text="Visualizza solo i bollini non utilizzati in un rapporto di controllo" />
                            <asp:ListItem Value="2" Text="Visualizza solo i bollini utilizzati in un rapporto di controllo" />
                        </asp:RadioButtonList>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloBolliniNonAttivi" AssociatedControlID="rblStatoBolliniCalorePulitoNonAttivi" Text="Stato Bollini" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoBolliniCalorePulitoNonAttivi" TabIndex="1" runat="server" RepeatDirection="Vertical" RepeatColumns="1" CssClass="radiobuttonlistClass">
                            <asp:ListItem Value="0" Text="Visualizza tutti i bollini" Selected="True" />
                            <asp:ListItem Value="1" Text="Visualizza solo i bollini attivi" />
                            <asp:ListItem Value="2" Text="Visualizza solo i bollini non attivi" />
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
                        <asp:Button ID="RCT_BollinoCalorePulitoSearch_btnProcess" runat="server" TabIndex="1" CssClass="buttonClass" Width="300"
                            OnClick="RCT_BollinoCalorePulitoSearch_btnProcess_Click" Text="RICERCA BOLLINI CALORE PULITO" />
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
                                        RepeatColumns="3" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="rblFormatoStampa_SelectedIndexChanged">
                                        <Items>
                                            <dx:ListEditItem Value="0" Text="Formato A4" Selected="true" ImageUrl="~/images/buttons/imgFormatoA4.png" />
                                            <dx:ListEditItem Value="1" Text="Formato A7" ImageUrl="~/images/buttons/imgFormatoA7.png" />
                                            <dx:ListEditItem Value="2" Text="Formato Etichetta" ImageUrl="~/images/buttons/imgFormatoEtichetta.png" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </asp:TableCell>
                                <asp:TableCell Width="250px" CssClass="riempimento" HorizontalAlign="Center">
                                    <asp:Button runat="server" ID="btnStampa" CssClass="buttonClass"
                                        Text="STAMPA BOLLINI CALORE PULITO" Width="300px" />
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
                            AutoGenerateColumns="False" runat="server" DataKeyField="IDBollinoCalorePulito"
                            OnPageIndexChanged="DataGrid_PageChanger" OnItemDataBound="DataGrid_ItemDataBound">
                            <HeaderStyle CssClass="GridHeader" />
                            <ItemStyle CssClass="GridItem" />
                            <AlternatingItemStyle CssClass="GridAlternativeItem" />
                            <Columns>
                                <asp:BoundColumn DataField="IDBollinoCalorePulito" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDRapportoControlloTecnico" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggetto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoDerived" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceBollino" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="CodiceLotto" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="fBollinoUtilizzato" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDSoggettoUtilizzatore" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="DataOraUtilizzo" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDLottoBolliniCalorePulito" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="IDTipologiaRCT" Visible="false" ReadOnly="True" />
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="800" HeaderText="Elenco Codici Bollini Calore Pulito">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td width="700">
                                                    <b>Azienda:&nbsp;</b><%#Eval("Soggetto") %>&nbsp;<%#Eval("IndirizzoSoggetto") %><br />
                                                    <br />
                                                    <b>Operatore/Addetto:&nbsp;</b><%#Eval("SoggettoDerived") %><br />
                                                    <br />
                                                    <b>Codice bollino:&nbsp;</b>
                                                        <%#Eval("CodiceBollino") %>
                                                        &nbsp;-&nbsp;<b>Lotto n.:&nbsp;</b><%#Eval("IDLottoBolliniCalorePulito").ToString() %><br />
                                                        <b>Costo bollino:&nbsp;</b><%# string.Format("{0:N2} €", Eval("CostoBollino")) %>&nbsp;-&nbsp;
                                                        <b>Bollino attivo:&nbsp;</b><%# DataUtilityCore.UtilityApp.BooleanFlagToString(bool.Parse(Eval("fAttivo").ToString())) %>
                                                        <asp:Label runat="server" Font-Bold="true" Visible="false" ID="lblTitoloDataDisattivazione" Text=" - Data disattivazione: " /><%# string.Format("{0:dd/MM/yyyy}", Eval("DataDisattivazione")) %>
                                                        
                                                        <br />
                                                </td>
                                                <td width="100">
                                                    <asp:Image runat="server" ID="imgBarcode" Width="70px" Height="70px" CssClass="imgBarcode" ToolTip="Barcode Codice Bollino" AlternateText="Barcode Codice Bollino" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="800" colspan="2">
                                                    <asp:Label runat="server" ID="lblRapportoDiControllo" Visible="false" Text="Bollino calore pulito legato ad un Rapporto di controllo tecnico" ForeColor="Green" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezioneAll" AutoPostBack="true" OnCheckedChanged="chkSelezioneAll_CheckedChanged" ToolTip="Seleziona tutti i codici bollini per la stampa" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkSelezione" AutoPostBack="true" OnCheckedChanged="chkSelezione_CheckedChanged" TabIndex="1" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CostoBollino" Visible="false" ReadOnly="True" />
                                <asp:BoundColumn DataField="fAttivo" Visible="false" ReadOnly="True" />
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
