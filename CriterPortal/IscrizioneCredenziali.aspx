<%@ Page Title="Criter - Conferma credenziali" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IscrizioneCredenziali.aspx.cs" Inherits="IscrizioneCredenziali" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoCredenziali" CssClass="TableClass" runat="server">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left" ColumnSpan="2" CssClass="riempimento1">
                              CREDENZIALI DI ACCESSO AL SISTEMA CRITER
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                            L’accesso al "Sistema Criter" può avvenire solo previa autenticazione dell’utente. Il meccanismo di autenticazione utilizzato si basa sull’utilizzo di credenziali di accesso, in particolare di Username e Password.<br />
                            Per dotarsi delle credenziali di accesso al sistema è necessario:
                            <br />
                            <ul type="circle">
                                <li>compilare il campo "Username" inserendo una Username di propria scelta. La Username scelta deve essere lunga almeno 8 caratteri e può essere una combinazione di lettere maiuscole e minuscole, numeri e caratteri speciali.
                                <li>compilare il campo "Password" inserendo una password di propria scelta. E' bene evitare password facili da scoprire (la propria data di nascita, il nome dei propri figli, la targa dell’auto, ecc.). E’ bene anche non scegliere come password termini comuni. E’ pertanto consigliato di scegliere una password che contenga combinazioni di lettere maiuscole e minuscole e numeri. La password deve essere lunga almeno 8 caratteri, contenere almeno una lettera maiuscola, almeno una lettera minuscola e almeno un carattere speciale. 
                                <li>confermare le credenziali scelte cliccando sul bottone "Conferma Credenziali".
                            </ul>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="225" CssClass="riempimento2">
                            <asp:Label runat="server" ID="lblUsername" AssociatedControlID="txtUsername" Text="Username (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="675" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtUsername" Width="200" TabIndex="1" MaxLength="200" ValidationGroup="vgAnagrafica" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator
                                ID="rfvtxtAzienda" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Username: campo obbligatorio"
                                ControlToValidate="txtUsername">&nbsp;*</asp:RequiredFieldValidator>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell Width="225" CssClass="riempimento2">
                            <asp:Label runat="server" ID="lblPassword" AssociatedControlID="txtPassword" Text="Password (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="675" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtPassword" Width="200" TabIndex="1" ValidationGroup="vgAnagrafica" TextMode="Password" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator
                                ID="rfvPassword" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Password: campo obbligatorio"
                                ControlToValidate="txtPassword">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revPassword" ValidationGroup="vgAnagrafica"
                                ControlToValidate="txtPassword" ForeColor="Red" 
                                ErrorMessage="Password inserita in un formato non corretto: deve contenere   a)Almeno una lettera maiuscola                                                     b)Almeno una lettera minuscola                                                     c)Almeno un carattere speciale                                                            d)Deve essere lunga almeno 8 caratteri" runat="server" ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$"        >&nbsp;*</asp:RegularExpressionValidator>
                            <cc1:PasswordStrength ID="COM_Password_PasswordStrength" runat="server" TargetControlID="txtPassword"
                                DisplayPosition="BelowRight" StrengthIndicatorType="Text" PreferredPasswordLength="10"
                                PrefixText="Complessità della password:" TextCssClass="Complexpassword"
                                MinimumNumericCharacters="0" MinimumSymbolCharacters="0" RequiresUpperAndLowerCaseCharacters="false"
                                TextStrengthDescriptions="molto bassa;debole;media;robusta;eccellente"
                                StrengthStyles="ComplexpasswordBassa;ComplexpasswordDebole;ComplexpasswordMedia;ComplexpasswordRobusta;ComplexpasswordEccellente"
                                CalculationWeightings="50;15;15;20" />
                        </asp:TableCell>
                    </asp:TableRow>
                    
                    <asp:TableRow>
                        <asp:TableCell Width="225" CssClass="riempimento2">
                            <asp:Label runat="server" ID="lblConfermaPassword" AssociatedControlID="txtConfermaPassword" Text="Conferma Password (*)" />
                        </asp:TableCell>
                        <asp:TableCell Width="675" CssClass="riempimento">
                            <asp:TextBox runat="server" ID="txtConfermaPassword" Width="200" TabIndex="1" ValidationGroup="vgAnagrafica" TextMode="Password" CssClass="txtClass_o" />
                            <asp:RequiredFieldValidator
                                ID="rfvConfermaPassword" ForeColor="Red" runat="server" ValidationGroup="vgAnagrafica" EnableClientScript="true" ErrorMessage="Conferma Password: campo obbligatorio"
                                ControlToValidate="txtConfermaPassword">&nbsp;*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revConfermaPassword" ValidationGroup="vgAnagrafica"
                                ControlToValidate="txtConfermaPassword" ForeColor="Red" 
                                ErrorMessage="La conferma della Password inserita in un formato non corretto: deve contenere   a)Almeno una lettera maiuscola                                                     b)Almeno una lettera minuscola                                                     c)Almeno un carattere speciale                                                            d)Deve essere lunga almeno 8 caratteri" runat="server" ValidationExpression="(?=^.{8,}$)((?=.*\d)|(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z])(?!.*\s).*$"        >&nbsp;*</asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="cvPasswordCompare" ForeColor="Red" ValidationGroup="vgAnagrafica" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtConfermaPassword" Display="None" ErrorMessage="Le due password devono essere uguali" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
                            <asp:CustomValidator ID="cvUsernamePresente" runat="server" ForeColor="Red" ValidationGroup="vgAnagrafica" EnableClientScript="true" OnServerValidate="ControllaUsernamePresente" 
                                ErrorMessage="Username già presente nel sistema CRITER<br/>" />
                        </asp:TableCell>
                    </asp:TableRow>

                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            <asp:Button ID="btnProcess" runat="server" ValidationGroup="vgAnagrafica" TabIndex="1" CssClass="buttonClass" Width="200"
                                OnClick="btnProcess_Click" Text="CONFERMA CREDENZIALI" />
                            <asp:ValidationSummary ID="vsAnagrafica" ValidationGroup="vgAnagrafica" runat="server" ShowMessageBox="True"
                                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:">
                            </asp:ValidationSummary>
                        </asp:TableCell>
                    </asp:TableRow>
            </asp:Table>
            
            <asp:Table Width="900" ID="tblInfoConfermaCredenziali" CssClass="TableClass" Visible="false" runat="server">
                <asp:TableRow HorizontalAlign="Center">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
                        <asp:Label runat="server" ForeColor="Green" ID="lblIscrizioneOk" Font-Bold="true" Text="La registrazione al sistema CRITER è avvenuta con successo.<br/>L'utenza sarà attivata una volta terminati i controlli di funzionalità del sistema sulla procedura di registrazione." />
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

