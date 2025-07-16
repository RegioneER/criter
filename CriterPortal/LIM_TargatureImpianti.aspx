<%@ Page Title="Prenotazione codici targatura impianti" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LIM_TargatureImpianti.aspx.cs" Inherits="LIM_TargatureImpianti" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                              PRENOTAZIONE CODICI TARGATURA IMPIANTI
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloAzienda" AssociatedControlID="ASPxComboBox1" Text="Azienda (*)" />
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
                            <asp:requiredfieldvalidator ID="rfvASPxComboBox1" ValidationGroup="vgTargatura" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Azienda: campo obbligatorio"
								ControlToValidate="ASPxComboBox1">&nbsp;*</asp:requiredfieldvalidator>
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
                            <%--<asp:requiredfieldvalidator ID="rfvASPxComboBox2" ValidationGroup="vgTargatura" ForeColor="Red" Display="Dynamic" runat="server" InitialValue="" ErrorMessage="Operatore/Addetto: campo obbligatorio"
								ControlToValidate="ASPxComboBox2">&nbsp;*</asp:requiredfieldvalidator>--%>
                            <asp:Label ID="lblIDSoggettoDerived" runat="server" Visible="false" />
                            <asp:Label ID="lblSoggettoDerived" runat="server" Visible="false" Font-Bold="true" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="LIM_TargatureImpianti_lblTitoloNumeroTargature" AssociatedControlID="txtNumeroTargature" Text="Numero targature da prenotare (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtNumeroTargature" TabIndex="1" ValidationGroup="vgTargatura" Width="100" MaxLength="3" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator
                                ID="rfvNumeroTargature" ForeColor="Red" runat="server" ValidationGroup="vgTargatura" EnableClientScript="true" ErrorMessage="Numero targature da prenotare: campo obbligatorio"
                                ControlToValidate="txtNumeroTargature">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator
                                ID="revtxtNumeroTargature" ForeColor="Red" runat="server" ValidationGroup="vgTargatura" EnableClientScript="true" ErrorMessage="Numero targature da prenotare: inserire un numero intero" ControlToValidate="txtNumeroTargature"
                                ValidationExpression="^[0-9]*$">&nbsp;*</asp:RegularExpressionValidator>
                            <asp:RangeValidator 
                                ID="rvNumeroTargature" ForeColor="Red" runat="server" Type="Integer" ValidationGroup="vgTargatura" EnableClientScript="true"
                                MinimumValue="1" MaximumValue="100" ControlToValidate="txtNumeroTargature" 
                                ErrorMessage="Numero targature da prenotare: inserire un numero intero da 1 a 100" >&nbsp;*</asp:RangeValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                                        
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            <asp:Button ID="LIM_TargatureImpianti_btnProcess" runat="server" TabIndex="1" CssClass="buttonClass" Width="250"
                                OnClick="LIM_TargatureImpianti_btnProcess_Click" ValidationGroup="vgTargatura" Text="PRENOTA CODICI TARGATURA" />
                            <asp:ValidationSummary ID="LIM_TargatureImpianti_vschkPrenotaCodici" ValidationGroup="vgTargatura" runat="server" ShowMessageBox="True"
                                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:">
                            </asp:ValidationSummary>
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow runat="server" ID="rowCreateOk" Visible="false">
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            <asp:Label runat="server" ID="LIM_TargatureImpianti_lblCreateOk" ForeColor="Green" Font-Bold="true" Text="Codici targatura impianti creati con successo" />
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