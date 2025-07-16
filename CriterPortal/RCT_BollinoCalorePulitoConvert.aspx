<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="RCT_BollinoCalorePulitoConvert.aspx.cs" Inherits="RCT_BollinoCalorePulitoConvert" %>
<%@ Register Src="~/WebUserControls/WUC_Progress.ascx" TagPrefix="uc1" TagName="WebUSUpdateProgress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        CONVERSIONE BOLLINI CALORE PULITO
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                        Con tale funzionalità a partire dal 1° ottobre 2022 l'utente impresa potrà "convertire" i bollini con il <b>vecchio importo</b> non ancora utilizzati (non utilizzati dall’impresa perché non associati a rapporti di controllo) in bollini con il <b>nuovo importo</b>.
                        La conversione prevederà un aumento del numero bollini mantenendo però lo stesso valore economico totale degli stessi.
                        <br /><br />
                        Dopo tale conversione non sarà possibile ripristinare i <b>vecchi bollini</b>.
                        <br /><br />
                        Si specifica che a partire dal 1° gennaio 2023 tutti i precedenti bollini con il vecchio importo che non sono stati utilizzati e per i quali non è stata richiesta la conversione (presenti sull'applicativo CRITER e non utilizzati) saranno convertiti automaticamente in bollini con il nuovo importo.
                        <br /><br />
                        Per maggiori informazioni sulle modalità di conversione fare riferimento al manuale messo a disposizione.
                        <br /><br />
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
                                <dx:ListEditItem Text="Visualizza tutti Bollini calore pulito" Value="20000" Selected="true" />
                                <dx:ListEditItem Text="Visualizza 50 Bollini calore pulito per pagina" Value="50"  />
                                <dx:ListEditItem Text="Visualizza 100 Bollini calore pulito per pagina" Value="100" />
                                <dx:ListEditItem Text="Visualizza 150 Bollini calore pulito per pagina" Value="150" />
                                <dx:ListEditItem Text="Visualizza 200 Bollini calore pulito per pagina" Value="200" />
                            </Items>
                        </dx:ASPxComboBox>
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

                <asp:TableRow ID="rowConvertiBollini" runat="server" Visible="false">
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento5">
                        <asp:Table runat="server" ID="tblConvertiBollini" CssClass="TableClassNoBorder">
                            <asp:TableRow>
                                <asp:TableCell Width="600" CssClass="riempimento">
                                    Alla fine del processo di conversione le verrà assegnato un nuovo lotto contenente  
                                    <asp:Label runat="server" ID="lblTotaleBolliniConvert" Font-Bold="true" />&nbsp;bollini calore pulito e non le sarà addebitato alcun importo.
                                    <asp:Label runat="server" ID="lblFeedBackConversioneBollini" CssClass="error" />
                                </asp:TableCell>
                                <asp:TableCell Width="250px" CssClass="riempimento" HorizontalAlign="Center">
                                    <asp:Button runat="server" ID="btnConvertiBollini" CssClass="buttonClass" OnClick="btnConvertiBollini_Click" Text="CONVERTI BOLLINI CALORE PULITO" OnClientClick="javascript:return confirm('Confermi di avviare la procedura di conversione dei bollini calore pulito?');"  Width="300px" />
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
                                                    <b>Codice bollino:&nbsp;</b><%#Eval("CodiceBollino") %>&nbsp;-&nbsp;<b>Lotto n.&nbsp;:</b><%#Eval("IDLottoBolliniCalorePulito").ToString() %><br />
                                                        <b>Costo bollino:&nbsp;</b><%# string.Format("{0:N2} €", Eval("CostoBollino")) %>
                                                        <br />
                                                </td>
                                                <td width="100">
                                                    <asp:Image runat="server" ID="imgBarcode" Width="70px" Height="70px" CssClass="imgBarcode" ToolTip="Barcode Codice Bollino" AlternateText="Barcode Codice Bollino" />
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
    <uc1:WebUSUpdateProgress runat="server" id="WebUSUpdateProgress" />
    <%--<asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>--%>
</asp:Content>