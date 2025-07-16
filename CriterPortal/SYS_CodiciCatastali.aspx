<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SYS_CodiciCatastali.aspx.cs" Inherits="SYS_CodiciCatastali" Title="Gestione Codici Catastali" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentDisplay" runat="Server">
    <div class="ContenutoDisplay">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!-- -->
                <asp:Table Width="900" ID="tblInfoRicerca" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                            <h2>RICERCA CODICI CATASTALI</h2>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="lblTitoloComune" AssociatedControlID="txtComuneRicerca" Text="Comune" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtComuneRicerca" Width="300" TabIndex="1" CssClass="txtClass" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            <asp:Button ID="btnSearch" TabIndex="1" runat="server" CssClass="buttonClass" Width="200" Text="RICERCA" />&nbsp;
                            <asp:Button ID="btnnew" CssClass="buttonClass" OnClick="Nuovo_Click" Text="NUOVO" Width="200" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            &nbsp;
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                
                <asp:GridView ID="GridViewCodiciCatastali" AllowSorting="True" AllowPaging="True"
                    runat="server" DataSourceID="SqlDataSourceCodiciCatastali" ShowFooter="False"
                    DataKeyNames="IDCodiceCatastale" Width="100%" CellSpacing="0" CellPadding="3"
                    AutoGenerateColumns="False" OnRowCommand="GridViewCodiciCatastali_RowCommand"
                    UseAccessibleHeader="true" CssClass="Grid" GridLines="Horizontal">
                    <HeaderStyle CssClass="GridHeader" />
                    <AlternatingRowStyle CssClass="GridAlternativeItem" />
                    <RowStyle CssClass="GridItem" />
                    <PagerStyle CssClass="ContainerPaging" Font-Bold="true" />
                    <Columns>
                        <asp:BoundField ReadOnly="True" Visible="False" DataField="IDCodiceCatastale" SortExpression="IDCodiceCatastale" />
                        <asp:BoundField HeaderText="Codice" DataField="CodiceCatastale" SortExpression="CodiceCatastale" ItemStyle-Width="80px" />
                        <asp:BoundField HeaderText="Comune" DataField="Comune" SortExpression="Comune" ItemStyle-Width="250px" />
                        <asp:TemplateField HeaderText="Provincia" SortExpression="Provincia" ItemStyle-Width="100px">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlProvincia" CssClass="myinput" DataSourceID="ProvinciaDataSource" DataTextField="Provincia" DataValueField="IDProvincia" SelectedValue='<%#Bind("IDProvincia") %>' runat="server" Visible="true" AutoPostBack="false">
                                    <asp:ListItem Value="0">-- Selezionare --</asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbProvincia" Text='<%# Bind("Provincia") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="EmailPec" DataField="EmailPec" SortExpression="EmailPec" ItemStyle-Width="120px" />
                        <asp:BoundField HeaderText="Cap" DataField="Cap" SortExpression="Cap" ItemStyle-Width="60px" />
                        <asp:CheckBoxField HeaderText="Attivo" SortExpression="FAttivo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DataField="FAttivo" />
                        <asp:CommandField ShowEditButton="true" ButtonType="Image"
                            UpdateImageUrl="~/images/buttons/saveSmall.png"
                            EditImageUrl="~/images/buttons/editSmall.png"
                            CancelImageUrl="~/images/buttons/undoSmall.png" />
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmpty" runat="server" Text="Non sono presenti elementi." />
                    </EmptyDataTemplate>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceCodiciCatastali" runat="server"
                    SelectCommand="SELECT [IDCodiceCatastale], [CodiceCatastale], [Comune], [IDProvincia], [Provincia], [EmailPec], [Cap], [fattivo] FROM V_SYS_CodiciCatastali"
                    UpdateCommand="UPDATE [SYS_CodiciCatastali] SET [CodiceCatastale]=@CodiceCatastale, [Comune]=@Comune, [IDProvincia]=@IDProvincia, [EmailPec]=@EmailPec, [Cap]=@Cap, [fattivo]=@fattivo WHERE [IDCodiceCatastale]=@IDCodiceCatastale"
                    ConnectionString="<%$ ConnectionStrings:DBConnection %>"
                    FilterExpression="Comune LIKE '%{0}%'">
                    <InsertParameters>
                        <asp:Parameter Name="CodiceCatastale" Type="string" />
                        <asp:Parameter Name="Comune" Type="string" />
                        <asp:Parameter Name="IDProvincia" Type="int16" />
                        <asp:Parameter Name="EmailPec" Type="string" />
                        <asp:Parameter Name="Cap" Type="string" />
                        <asp:Parameter Name="fattivo" Type="Boolean" />
                    </InsertParameters>
                    <FilterParameters>
                        <asp:ControlParameter Name="Comune" ControlID="txtComuneRicerca" PropertyName="Text" />
                    </FilterParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="ProvinciaDataSource" runat="server"
                    SelectCommand="SELECT [IDProvincia], [Provincia], [fattivo] FROM [SYS_Province] WHERE [fattivo]=1"
                    ConnectionString="<%$ ConnectionStrings:DBConnection %>"></asp:SqlDataSource>

                <asp:Table Width="900" ID="tblInsertCodiceCatastale" Visible="false" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                                <h2>INSERIMENTO CODICE CATASTALE</h2>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label ID="lblCodiceCatastale" Text="Codice Catastale" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox ID="txtCodiceCatastale" runat="server" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvCodiceCatastale" runat="server" EnableClientScript="true"
                                ForeColor="Red" ErrorMessage="Codice Catastale: campo obbligatorio" ControlToValidate="txtCodiceCatastale">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label ID="lblComune" Text="Comune" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox ID="txtComune" runat="server" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvtxtComune" runat="server" EnableClientScript="true"
                                ForeColor="Red" ErrorMessage="Comune: campo obbligatorio" ControlToValidate="txtComune">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label ID="lblProvincia" Text="Provincia" runat="server" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:DropDownList ID="ddlProvincia" CssClass="selectClass_o" Width="200px" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvProvincia" runat="server"
                                ForeColor="Red" ErrorMessage="Provincia: campo obbligatorio"
                                ControlToValidate="ddlProvincia" InitialValue="0">&nbsp;*
                            </asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label ID="lblEmailPec" Text="Email Pec " runat="server" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox ID="txtEmailPec" runat="server" Width="200px" CssClass="txtClass" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label ID="lblCapComune" Text="Cap " runat="server" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox ID="txtCapComune" runat="server" Width="60" CssClass="txtClass_o" MaxLength="5" />
                            <asp:RequiredFieldValidator ID="rfvtxtCapComune" runat="server" EnableClientScript="true"
                                ForeColor="Red" ErrorMessage="Cap Comune: campo obbligatorio" ControlToValidate="txtCapComune">&nbsp;*</asp:RequiredFieldValidator>

                            <asp:RegularExpressionValidator
                                ID="revtxtCapSedeLegale" ForeColor="Red" runat="server" EnableClientScript="true" ErrorMessage="Cap comune: non valido" ControlToValidate="txtCapComune"
                                ValidationExpression="\d{5}">&nbsp;*</asp:RegularExpressionValidator>
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label ID="lblFattivo" Text="Attivo (Si/No) " runat="server" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:CheckBox ID="chkFattivo" Checked="true" runat="server" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <%--<asp:TableRow>
                            <asp:TableCell Width="300" CssClass="riempimento2">
                        
                            </asp:TableCell>
                            <asp:TableCell Width="600" CssClass="riempimento">
                        
                            </asp:TableCell>
                        </asp:TableRow>

                         <asp:TableRow>
                            <asp:TableCell Width="300" CssClass="riempimento2">
                        
                            </asp:TableCell>
                            <asp:TableCell Width="600" CssClass="riempimento">
                        
                            </asp:TableCell>
                        </asp:TableRow>--%>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            <asp:Button CssClass="buttonClass" OnClick="Insert_Click" Text="INSERISCI" runat="server" Width="200px" ID="btnview" />&nbsp;
                                <asp:Button CssClass="buttonClass" OnClick="Annulla_Click" Text="ANNULLA" CausesValidation="false" Width="200px" runat="server" ID="btnannulla" />
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
    </div>
</asp:Content>

