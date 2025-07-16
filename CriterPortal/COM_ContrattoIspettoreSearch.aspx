<%@ Page Title="Ricerca Contratto Ispettore" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COM_ContrattoIspettoreSearch.aspx.cs" Inherits="COM_ContrattoIspettoreSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->

            <asp:Table Width="900" ID="tblQualificaIspettore" runat="server" CssClass="TableClass">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        <h2>RICERCA CONTRATTO ISPETTORE</h2>
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
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell runat="server" Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblStatoContratto" AssociatedControlID="rblStatoContratto" Text="Stato Contratto" />
                    </asp:TableCell>

                    <asp:TableCell runat="server" Width="600" CssClass="riempimento">
                        <asp:RadioButtonList ID="rblStatoContratto" runat="server" RepeatColumns="1" RepeatLayout="Table" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataInizioContratto" Text="Data inizio contratto (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInizioDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataInizioDa" ControlToValidate="txtDataInizioDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataInizioAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataInizioAl" ControlToValidate="txtDataInizioAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloDataFine" Text="Data fine contratto (gg/mm/aaaa)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        da:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataFineDa" />
                        <asp:RegularExpressionValidator
                            ID="revDataFineDA" ControlToValidate="txtDataFineDa" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                        &nbsp;&nbsp;
						a:&nbsp;<asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtDataFineAl" />
                        <asp:RegularExpressionValidator
                            ID="revDataFineAl" ControlToValidate="txtDataFineAl" ForeColor="Red" ErrorMessage=""
                            runat="server" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([-./])(0[13578]|10|12)([-./])(\d{4}))|(([0][1-9]|[12][0-9]|30)([-./])(0[469]|11)([-./])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([-./])(02)([-./])(\d{4}))|((29)(\.|-|\/)(02)([-./])([02468][048]00))|((29)([-./])(02)([-./])([13579][26]00))|((29)([-./])(02)([-./])([0-9][0-9][0][48]))|((29)([-./])(02)([-./])([0-9][0-9][2468][048]))|((29)([-./])(02)([-./])([0-9][0-9][13579][26])))"
                            EnableClientScript="true">&nbsp;*</asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>


                 <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblNumeroIspezioniMax" Text="Numero ispezioni massime" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:TextBox CssClass="txtClass" Width="100" TabIndex="1" runat="server" ID="txtNumeroIspezioniMax" />
                    </asp:TableCell>
                </asp:TableRow>


                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAttivo" Text="Attivo ( SI / NO )" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:CheckBox ID="cbAttivo" runat="server" Checked="true" />
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="2">
                        <br />
                        <asp:Button ID="btnRicerca" TabIndex="1" runat="server" CssClass="buttonClass" Width="200"
                            OnClick="btnRicerca_Click" Text="RICERCA CONTRATTI" />&nbsp;                       
                        <asp:Button ID="btnNuovo" runat="server" TabIndex="1" CssClass="buttonClass" Width="200"
                            OnClick="btnNuovo_Click" Text="NUOVO CONTRATTO" />&nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <asp:Table runat="server" ID="tblDataGrid">
                            <asp:TableRow>
                                <asp:TableCell>


                                    <br />
                                    <asp:Label runat="server" ID="lblCount" Visible="false" />
                                    <asp:DataGrid ID="DataGrid" CssClass="Grid" Width="890px" GridLines="None" HorizontalAlign="Center"
                                        CellSpacing="1" CellPadding="3" UseAccessibleHeader="false" PageSize="10" AllowSorting="True" AllowPaging="True"
                                        AutoGenerateColumns="False" runat="server" DataKeyField="IDIspettore" OnItemDataBound="DataGrid_ItemDataBound"
                                        OnPageIndexChanged="DataGrid_PageChanger">
                                        <HeaderStyle CssClass="GridHeader" HorizontalAlign="Center" />
                                        <ItemStyle CssClass="GridItem" />
                                        <AlternatingItemStyle CssClass="GridAlternativeItem" />
                                        <Columns>
                                            <asp:BoundColumn DataField="IDIspettore" Visible="false" ReadOnly="True" />
                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Left" ItemStyle-Width="830" HeaderText="Lista Contratto Ispettore" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Table ID="tblInfoIspettore" Width="700" runat="server">
                                                        <asp:TableRow Width="700">
                                                            <asp:TableCell Width="100">
                                                        <b>Ispettore:&nbsp;</b>
                                                            </asp:TableCell>
                                                            <asp:TableCell HorizontalAlign="Left" Width="200">
                                                                <asp:Label ID="lblIspettore" runat="server" Text='<%#Eval("Nome") + "   " + Eval("Cognome") %>' />
                                                            </asp:TableCell>

                                                            <asp:TableCell Width="100">
                                                        <b>Stato&nbsp;Contratto:&nbsp;</b>
                                                            </asp:TableCell>
                                                            <asp:TableCell HorizontalAlign="Left" Width="200">
                                                                <asp:Label ID="lblSoggettoManutentore" runat="server" Text='<%#Eval("StatoContratto") %>' />
                                                            </asp:TableCell>
                                                        </asp:TableRow>

                                                        <asp:TableRow Width="700">
                                                            <asp:TableCell Width="100">
                                                        <b>Data&nbsp;Inizio&nbsp;Contratto:&nbsp;</b>
                                                            </asp:TableCell>
                                                            <asp:TableCell HorizontalAlign="Left" Width="200">
                                                                <asp:Label ID="lblDataInizio" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataInizioContratto")) %>' />
                                                            </asp:TableCell>


                                                            <asp:TableCell Width="100">
                                                        <b>Data&nbsp;Fine&nbsp;Contratto:&nbsp;</b>
                                                            </asp:TableCell>
                                                            <asp:TableCell HorizontalAlign="Left" Width="200">
                                                                <asp:Label ID="lblDataFine" runat="server" Text='<%# String.Format("{0:dd/MM/yyyy}", Eval("DataFineContratto")) %>' />
                                                            </asp:TableCell>
                                                        </asp:TableRow>



                                                        <asp:TableRow Width="700">
                                                            <asp:TableCell Width="100">
                                                        <b>Attivo:&nbsp;</b>
                                                            </asp:TableCell>
                                                            <asp:TableCell HorizontalAlign="Left" Width="100" ColumnSpan="1">
                                                                <asp:Image ID="imgAttivo" runat="server" ImageUrl='<%#DataUtilityCore.UtilityApp.ToImage(Convert.ToBoolean(Eval("fAttivo"))) %>' />
                                                            </asp:TableCell>

                                                            
                                                            <asp:TableCell Width="100">
                                                        <b>Numero&nbsp;Ispezioni&nbsp;Massime:&nbsp;</b>
                                                            </asp:TableCell>
                                                            <asp:TableCell HorizontalAlign="Left" Width="300">
                                                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("NumeroIspezioniMax") %>' />
                                                            </asp:TableCell>

                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>


                                            <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30" HeaderText="">
                                                <ItemTemplate>
                                                    <asp:ImageButton runat="server" ID="ImgEdit" ToolTip="Modifica Qualifica Ispettore" AlternateText="Modifica Qualifica Ispettore" ImageUrl="~/images/Buttons/edit.png" TabIndex="1"
                                                        OnCommand="RowCommand" CommandName="Edit" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"IDContrattoIspettore") +","+ DataBinder.Eval(Container.DataItem,"IDIspettore") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>

                                        </Columns>
                                        <PagerStyle Font-Bold="True" HorizontalAlign="Left" CssClass="ContainerPaging" Mode="NumericPages" />
                                    </asp:DataGrid>


                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
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