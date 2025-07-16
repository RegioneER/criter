<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COM_Password.aspx.cs" Inherits="COM_Password" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                              CAMBIA PASSWORD
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="COM_Password_lblTitoloPswCorrente" AssociatedControlID="txtPswCorrente" Text="Password corrente (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtPswCorrente" TabIndex="1" Width="200" TextMode="Password" CssClass="txtClass_o" /><asp:RequiredFieldValidator
                                ID="rfvPswCorrente" ForeColor="Red" runat="server" EnableClientScript="true" ErrorMessage="Password corrente: campo obbligatorio"
                                ControlToValidate="txtPswCorrente">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="COM_Password_lblTitoloPswNuova" AssociatedControlID="txtPswNuova" Text="Nuova password (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtPswNuova" TabIndex="1" Width="200" TextMode="Password" CssClass="txtClass_o" /><asp:RequiredFieldValidator
                                ID="rfvPswNuova" ForeColor="Red" runat="server" EnableClientScript="true" ErrorMessage="Nuova password: campo obbligatorio"
                                ControlToValidate="txtPswNuova">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPswNuova" 
                                ControlToValidate="txtPswNuova" ForeColor="Red" 
                                ErrorMessage="Password inserita in un formato non corretto: deve contenere   a)Almeno una lettera maiuscola                                                     b)Almeno una lettera minuscola                                                     c)Almeno un carattere speciale                                                            d)Deve essere lunga almeno 8 caratteri" runat="server" ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$"        >&nbsp;*</asp:RegularExpressionValidator>
                            <cc1:PasswordStrength ID="COM_Password_PasswordStrength" runat="server" TargetControlID="txtPswNuova"
                                DisplayPosition="BelowRight" StrengthIndicatorType="Text" PreferredPasswordLength="10"
                                PrefixText="Complessità della password:" TextCssClass="Complexpassword"
                                MinimumNumericCharacters="0" MinimumSymbolCharacters="0" RequiresUpperAndLowerCaseCharacters="false"
                                TextStrengthDescriptions="molto bassa;debole;media;robusta;eccellente"
                                StrengthStyles="ComplexpasswordBassa;ComplexpasswordDebole;ComplexpasswordMedia;ComplexpasswordRobusta;ComplexpasswordEccellente"
                                CalculationWeightings="50;15;15;20" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="300" CssClass="riempimento2">
                            <asp:Label runat="server" ID="COM_Password_lblTitoloPswNuovaConferma" AssociatedControlID="txtPswNuovaConferma" Text="Conferma nuova password (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="600" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtPswNuovaConferma" TabIndex="1" Width="200" TextMode="Password" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator ID="rfvPswNuovaConferma" ForeColor="Red" runat="server" EnableClientScript="true" ErrorMessage="Conferma nuova password: campo obbligatorio"
                                ControlToValidate="txtPswNuovaConferma">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="cvPasswordCompare" ForeColor="Red" runat="server" ControlToCompare="txtPswNuova"
                                ControlToValidate="txtPswNuovaConferma" Display="None" ErrorMessage="La Conferma della nuova Password deve essere uguale alla nuova Password" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            <asp:CustomValidator ID="COM_Password_cvCambioPassword" ValidateEmptyText="true" runat="server"
                                Display="None" OnServerValidate="ControlliPassword" />
                        </asp:TableCell>
                    </asp:TableRow>
                                        
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            <asp:Button ID="COM_Password_btnProcess" runat="server" CssClass="buttonClass" Width="200"
                                OnClick="btnProcess_Click" Text="CAMBIA PASSWORD" />
                            <asp:ValidationSummary ID="COM_Password_vschkCambiaPsw" runat="server" ShowMessageBox="True"
                                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:">
                            </asp:ValidationSummary>
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

