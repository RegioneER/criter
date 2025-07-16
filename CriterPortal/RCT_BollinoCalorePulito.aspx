<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RCT_BollinoCalorePulito.aspx.cs" Inherits="RCT_BollinoCalorePulito" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function gridBollini_SelectionChanged(s, e) {
            gridBollini.PerformCallback("UpdateSelectedBollini");          
        }

        function ShowRowsCount(s, e) {
            lblTotaleBolliniAssegnati.SetText(gridBollini.GetSelectedRowCount());
        }

        function OnGridViewEndCallback() {
            ShowRowsCount();
        }

        function OnGridViewInit() {
            ShowRowsCount();
        }

        function OnUnselectAllRowsLinkClick() {
            gridBollini.UnselectRows();
            gridBollini.PerformCallback("UpdateSelectedBollini");    
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                              ASSEGNAZIONE BOLLINI CALORE PULITO
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="RCT_BollinoCalorePulito_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda (*)" />
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
                            <asp:requiredfieldvalidator ID="rfvASPxComboBox1" ValidationGroup="vgBollini" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Azienda: campo obbligatorio"
								ControlToValidate="ASPxComboBox1">&nbsp;*</asp:requiredfieldvalidator>
                            <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                            <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="LIM_LibrettiImpianti_lblTitoloPersona" AssociatedControlID="ASPxComboBox2" Text="Operatore/Addetto (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <dx:ASPxComboBox ID="ASPxComboBox2" runat="server" Theme="Default" TabIndex="1"
                                TextField="Soggetto" ValueField="IDSoggetto" ValueType="System.Int32"
                                Width="350px" AutoPostBack="true" OnSelectedIndexChanged="ASPxComboBox2_SelectedIndexChanged" 
                                DropDownWidth="350px">
                                <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                            </dx:ASPxComboBox>
                            <asp:requiredfieldvalidator ID="rfvASPxComboBox2" ValidationGroup="vgBollini" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Operatore/Addetto: campo obbligatorio"
								ControlToValidate="ASPxComboBox2">&nbsp;*</asp:requiredfieldvalidator>
                            <asp:Label ID="lblIDSoggettoDerived" runat="server" Visible="false" />
                            <asp:Label ID="lblSoggettoDerived" runat="server" Visible="false" Font-Bold="true" />
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="RCT_BollinoCalorePulito_lblTitoloTotaleBolliniAssegnati" AssociatedControlID="lblTotaleBolliniAssegnati" Text="Bollini calore pulito assegnati" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                             <dx:ASPxLabel ID="lblTotaleBolliniAssegnati" ClientInstanceName="lblTotaleBolliniAssegnati" Font-Bold="true"  runat="server" Text="" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="RCT_BollinoCalorePulito_lblTitoloNumeroBollini" AssociatedControlID="ASPxGridViewBollini" Text="Bollini disponibili da assegnare" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <dx:ASPxGridView ID="ASPxGridViewBollini"
                                ClientInstanceName="gridBollini" runat="server" DataSourceID="dsBollini"
                                KeyFieldName="IDBollinoCalorePulito" Width="100%"  EnableRowsCache="False"
                                OnDataBound="ASPxGridViewBollini_DataBound"
                                OnCustomCallback="ASPxGridViewBollini_CustomCallback">
                                <SettingsBehavior AllowGroup="false" AllowDragDrop="false" />
                                <%--<Settings VerticalScrollBarMode="Visible" VerticalScrollBarStyle="VirtualSmooth" VerticalScrollableHeight="600" />--%>
                                    <Columns>
                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True" VisibleIndex="0" Width="20">
                                            <HeaderTemplate>
                                                <dx:ASPxCheckBox ID="SelectAllCheckBox" runat="server" ToolTip="Selezione/deseleziona tutti i bollini in questa pagina"
                                                    ClientSideEvents-CheckedChanged="function(s, e) { gridBollini.SelectAllRowsOnPage(s.GetChecked()); }" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </dx:GridViewCommandColumn>
                                         <dx:GridViewDataColumn FieldName="CodiceBollino" VisibleIndex="1" Settings-AllowAutoFilterTextInputTimer="True" />
                                        <dx:GridViewDataTextColumn FieldName="CostoBollino" VisibleIndex="2" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                            <PropertiesTextEdit DisplayFormatString="{0:N2} €" />
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                <SettingsPager PageSize="20" />
                                <SettingsSearchPanel Visible="true" ColumnNames="CodiceBollino" AllowTextInputTimer="True" Delay="500" />
                                <ClientSideEvents SelectionChanged="gridBollini_SelectionChanged" Init="OnGridViewInit" EndCallback="OnGridViewEndCallback" />
                            </dx:ASPxGridView>
                            <ef:EntityDataSource ID="dsBollini" runat="server"
                                ConnectionString="name=CriterDataModel" 
                                ContextTypeName="DataLayer.CriterDataModel"
                                DefaultContainerName="CriterDataModel"
                                EnableFlattening="False"
                                OnContextCreating="dsBolliniModel_ContextCreating"
                                OnContextDisposing="dsBolliniModel_ContextDisposing"
                                OnSelecting="dsBollini_Selecting"
                                EntitySetName="V_RCT_BollinoCalorePulito"
                                Where="it.IDRapportoControlloTecnico IS NULL AND it.fBollinoUtilizzato=false AND it.fAttivo=true AND it.IDSoggetto=@IDSoggetto AND (it.IDSoggettoDerived=@IDSoggettoDerived OR it.IDSoggettoDerived IS NULL) "
                                OrderBy="it.CodiceBollino DESC">
                                <WhereParameters>
                                    <asp:parameter name="IDSoggetto" type="Int32" defaultvalue="0" />
                                    <asp:parameter name="IDSoggettoDerived" type="Int32" defaultvalue="0" />
                                </WhereParameters>
                            </ef:EntityDataSource>
                            
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