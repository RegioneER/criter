<%@ Page Title="" Language="C#" MasterPageFile="~/BootstrapItalia.master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentDisplay" runat="Server">
    <!--LOGIN FORM-->
    <!--CONTENT-->
    <asp:Login ID="LoginControl" runat="server" DisplayRememberMe="false" OnAuthenticate="LoginCtrl_Authenticate">
        <LayoutTemplate>
            <div class="container login-container">
                <div class="login-box">
                    <!-- HEADER -->
                    <div class="text-center mb-4">
                        <asp:Image runat="server" ID="imgCriter" CssClass="img-fluid" ImageUrl="~/images/LogoCriter.png" AlternateText="Logo Criter" ToolTip="Logo Criter" ImageAlign="AbsMiddle" />
                    </div>
                    <div class="text-center mb-3">
                        <asp:Label runat="server" ID="Login_lblTitoloAccesso" AssociatedControlID="UserName" Text="Digita Username e Password per accedere alla tua area riservata." />
                    </div>
                    <div class="text-center text-danger mb-3">
                        <asp:Label runat="server" ID="Login_lblPasswordScaduta" CssClass="text-danger" Visible="false" AssociatedControlID="Password" />
                    </div>
                    <!-- LOGIN FORM -->
                    <div>
                        <div class="form-group">
                            <!-- USERNAME -->
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <asp:Image runat="server" ID="imgUser" AlternateText="User Image" ToolTip="User Image" ImageUrl="~/images/buttons/user-icon.png" />
                                        <asp:Label runat="server" ID="lblTitoloUsername" Font-Bold="true" AssociatedControlID="UserName" Text="Username" />
                                    </span>
                                </div>
                                <asp:TextBox ID="UserName" runat="server" TabIndex="1" CssClass="form-control" ValidationGroup="LoginControl" Width="400px" ToolTip="UserName" />
                            </div>
                            <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UserName"
                                ErrorMessage="Username è un campo obbligatorio" ValidationGroup="LoginControl" Display="None" />
                        </div>
                        <div class="form-group">
                            <!-- PASSWORD -->
                            <div class="input-group mb-3">
                                <div class="input-group-prepend">
                                    <span class="input-group-text">
                                        <asp:Image runat="server" ID="imgPassword" AlternateText="Password Image" ToolTip="Password Image" ImageUrl="~/images/buttons/pass-icon.png" />
                                        <asp:Label runat="server" ID="lblTitoloPassword" Font-Bold="true" AssociatedControlID="Password" Text="Password" />
                                    </span>
                                </div>
                                <asp:TextBox ID="Password" TextMode="password" TabIndex="2" ValidationGroup="LoginControl" runat="server" Width="400px" CssClass="form-control" ToolTip="Password" />
                            </div>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password"
                                ErrorMessage="Password è un campo obbligatorio" ValidationGroup="LoginControl" Display="None" />
                        </div>
                        <!-- LOGIN BUTTON -->
                        <div class="form-group text-center">
                            <asp:Button runat="server" ID="btnlogin" CommandName="Login" TabIndex="3" Text="Accedi al sistema" ValidationGroup="LoginControl" CssClass="buttonClass" />
                        </div>
                        <div class="form-group text-center">
                            <asp:ImageButton runat="server" ID="btnloginSpid" TabIndex="4" ToolTip="Entra con Spid" AlternateText="Entra con Spid" onmouseover="this.src='images/spid-button-link-link.png'" onmouseout="this.src='images/spid-button-link-hover.png'" ImageUrl="~/images/spid-button-link-hover.png" BorderStyle="None" CausesValidation="false" OnClick="btnloginSpid_Click" />
                        </div>
                        <div class="text-center text-danger">
                            <asp:Label ID="FailureText" runat="server" Font-Bold="true" EnableViewState="False" />
                        </div>
                        <!-- LINKS -->
                        <div class="text-center">
                            <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="True" ValidationGroup="LoginControl"
                                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
                            <asp:Panel runat="server" ID="pnlLoginFuction">
                                <div class="row">
                                    <div class="col">
                                        <asp:LinkButton ID="Login_lnkPasswordRecovery" OnClick="Login_lnkPasswordRecovery_OnClick" CausesValidation="false" Text="Problemi di accesso all'account?" runat="server" class="btn btn-link" />
                                    </div>
                                </div>
                                <div class="row mt-2">
                                    <div class="col">
                                        <asp:LinkButton ID="Login_lnkIscrizione" OnClick="Login_lnkIscrizione_OnClick" CausesValidation="false" Text="Non hai ancora un account Criter? Iscriviti ora" runat="server" class="btn btn-link" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <asp:Panel ID="pnlPassword" runat="server" Visible="False">
        <div class="text-center">
            <!--TITLE-->
            <div>
                <asp:Image runat="server" ID="imgCriter" ImageUrl="~/images/LogoCriter.png" AlternateText="Logo Criter" ToolTip="Logo Criter" ImageAlign="AbsMiddle" />
                <br />
                <br />
            </div>
            <!--END TITLE-->
            <!--DESCRIPTION-->
            <span>
                <asp:Label runat="server" ID="lblPasswordScaduta" Visible="false" CssClass="riempimento8" />
            </span>
        </div>
        <div class="row">
            <div class="col-md-6 col-md-offset-3 text-center">
                <div class="form-group">
                    <div class="input-group" style="width: 100% !important">
                        <asp:Label runat="server" ID="lblTitoloNewPassword" Font-Bold="true" AssociatedControlID="txtNewPassword" Text="Nuova password:" />&nbsp;
                            <asp:TextBox ID="txtNewPassword" TabIndex="1" TextMode="password" ValidationGroup="vsChangePassword" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ValidationGroup="vsChangePassword" ControlToValidate="txtNewPassword"
                            ErrorMessage="Inserire la nuova password" Display="None" /><br />
                    </div>
                </div>
                <div class="form-group">
                    <div class="input-group" style="width: 100% !important">
                        <asp:Label runat="server" ID="lblTitoloConfirmNewPassword" Font-Bold="true" AssociatedControlID="TxtConfirmNewPassword" Text="Conferma Password:" />&nbsp;
                        <asp:TextBox ID="TxtConfirmNewPassword" TabIndex="1" TextMode="password" ValidationGroup="vsChangePassword" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator ID="rfvConfirmNewPassword" runat="server" ValidationGroup="vsChangePassword" ControlToValidate="TxtConfirmNewPassword"
                            ErrorMessage="Confermare la nuova Password" Display="None" />
                    </div>
                </div>
            </div>
        </div>
        <div class="text-center text-danger">
            <asp:Label ID="lblErroreCambioPassword" runat="server" Font-Bold="true" />
        </div>

        <!--FOOTER-->
        <div class="text-center">
            <asp:Button runat="server" ID="btnChangePassword" TabIndex="1" Text="CAMBIA PASSWORD"
                OnClick="btnCambiaPassword_Click" Width="200" ValidationGroup="vsChangePassword" CssClass="buttonClass" />
            <asp:ValidationSummary ID="vSummary" runat="server" ShowMessageBox="True" ValidationGroup="vsChangePassword"
                ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:" />
        </div>
        <!--END FOOTER-->
    </asp:Panel>
    <!--END CONTENT-->
    <!--END LOGIN FORM-->
</asp:Content>
