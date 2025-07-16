<%@ Page Title="Criter - Recupera credenziali dimenticate" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RecuperoCredenziali.aspx.cs" Inherits="RecuperoCredenziali" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <div id="loginwrapper">
                <div class="login-form">
                    <!--HEADER-->
                    <div class="header">
                        <!--TITLE-->
                        <div>
                            <asp:Image runat="server" ID="imgCriter" ImageUrl="~/images/LogoCriter.png" ImageAlign="AbsMiddle" AlternateText="Immagine logo CRITER" ToolTip="Immagine logo CRITER" />
                            <br /><br />
                        </div>
                        <!--END TITLE-->
                        <span>
                            <asp:Label runat="server" ID="Login_lblTitoloRecuperoCredenziali" CssClass="riempimento7" Text="Per reimpostare la password, immetti i dati richiesti" /><br />
                            <asp:RadioButtonList ID="rblTipoSoggetto" runat="server" TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="rblTipoSoggetto_SelectedIndexChanged" RepeatDirection="Vertical" BorderStyle="None" RepeatColumns="1" CssClass="radiobuttonlistClass">
                                <asp:ListItem Text="Impresa di Manutenzione" Value="1" Selected="True" />
                                <asp:ListItem Text="Operatore Addetto/Responsabile Tecnico" Value="2" />
                                <asp:ListItem Text="Distributore di Combustibile" Value="3" />
                                <asp:ListItem Text="Ente Locale" Value="4" />
                                <asp:ListItem Text="Società di Sviluppo Software" Value="5" />
                                <asp:ListItem Text="Ispettore" Value="6" />
                            </asp:RadioButtonList>
                        </span>
                    </div>
                    <!--END HEADER-->
                    <!--CONTENT-->
                    <div class="content" runat="server" id="pnlImpresa" visible="false">
                        <asp:Label runat="server" ID="lblCodFiscalePiva" Text="Cod. Fiscale o Partita Iva" AssociatedControlID="txtCodFiscalePiva" />
                        <asp:TextBox ID="txtCodFiscalePiva" runat="server" TabIndex="1" MaxLength="16" CssClass="input username" ValidationGroup="vgRecuperaCredenziali" Width="70%" />
                        <asp:RequiredFieldValidator ID="rfvCodFiscalePiva" runat="server" ControlToValidate="txtCodFiscalePiva"
                            ErrorMessage="Codice fiscale o Partita Iva: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                        <br /><br />
                        <asp:Label runat="server" ID="lbltxtCodiceSoggetto" Text="Codice Soggetto" AssociatedControlID="txtCodiceSoggetto" />
                        <asp:TextBox ID="txtCodiceSoggetto" TabIndex="1" MaxLength="11" ValidationGroup="vgRecuperaCredenziali" runat="server" CssClass="input username" Width="70%" ToolTip="Codice Soggetto" />
                        <asp:RequiredFieldValidator ID="rfvtxtCodiceSoggetto" runat="server" ControlToValidate="txtCodiceSoggetto"
                            ErrorMessage="Codice soggetto: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                    </div>
                    <div class="content" runat="server" id="pnlManutentore" visible="false">
                        <asp:Label runat="server" ID="lblCodFiscaleManutentore" Text="Codice Fiscale" AssociatedControlID="txtCodFiscaleManutentore" />
                        <asp:TextBox ID="txtCodFiscaleManutentore" runat="server" TabIndex="1" MaxLength="16" CssClass="input username" ValidationGroup="vgRecuperaCredenziali" Width="70%" />
                        <asp:RequiredFieldValidator ID="rfvCodFiscaleManutentore" runat="server" ControlToValidate="txtCodFiscaleManutentore"
                            ErrorMessage="Codice fiscale: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                        <br /><br />
                        <asp:Label runat="server" ID="lbltxtCodiceSoggettoManutentore" Text="Codice Soggetto" AssociatedControlID="txtCodiceSoggettoManutentore" />
                        <asp:TextBox ID="txtCodiceSoggettoManutentore" TabIndex="1" MaxLength="11" ValidationGroup="vgRecuperaCredenziali" runat="server" CssClass="input username" Width="70%" ToolTip="Codice Soggetto" />
                        <asp:RequiredFieldValidator ID="rfvtxtCodiceSoggettoManutentore" runat="server" ControlToValidate="txtCodiceSoggettoManutentore"
                            ErrorMessage="Codice soggetto: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                    </div>
                    <div class="content" runat="server" id="pnlDistributore" visible="false">
                        <asp:Label runat="server" ID="lblPartitaIvaDistributore" Text="Partita Iva" AssociatedControlID="txtPartitaIvaDistributore" />
                        <asp:TextBox ID="txtPartitaIvaDistributore" runat="server" TabIndex="1" MaxLength="16" CssClass="input username" ValidationGroup="vgRecuperaCredenziali" Width="70%" />
                        <asp:RequiredFieldValidator ID="rfvPartitaIvaDistributore" runat="server" ControlToValidate="txtPartitaIvaDistributore"
                            ErrorMessage="Partita Iva: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                        <br /><br />
                        <asp:Label runat="server" ID="lbltxtEmailDistributore" Text="Email di registrazione" AssociatedControlID="txtEmailDistributore" />
                        <asp:TextBox ID="txtEmailDistributore" TabIndex="1" ValidationGroup="vgRecuperaCredenziali" runat="server" CssClass="input username" Width="70%" ToolTip="Email" />
                        <asp:RequiredFieldValidator ID="rfvtxtEmailDistributore" runat="server" ControlToValidate="txtEmailDistributore"
                            ErrorMessage="Email: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                    </div>
                    <div class="content" runat="server" id="pnlEnteLocale" visible="false">
                        <asp:Label runat="server" ID="lblPartitaIvaEnteLocale" Text="Partita Iva" AssociatedControlID="txtPartitaIvaEnteLocale" />
                        <asp:TextBox ID="txtPartitaIvaEnteLocale" runat="server" TabIndex="1" MaxLength="16" CssClass="input username" ValidationGroup="vgRecuperaCredenziali" Width="70%" />
                        <asp:RequiredFieldValidator ID="rfvPartitaIvaEnteLocale" runat="server" ControlToValidate="txtPartitaIvaEnteLocale"
                            ErrorMessage="Partita Iva: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                        <br /><br />
                        <asp:Label runat="server" ID="lbltxtEmailEnteLocale" Text="Email di registrazione" AssociatedControlID="txtEmailEnteLocale" />
                        <asp:TextBox ID="txtEmailEnteLocale" TabIndex="1" ValidationGroup="vgRecuperaCredenziali" runat="server" CssClass="input username" Width="70%" ToolTip="Email" />
                        <asp:RequiredFieldValidator ID="rfvtxtEmailEnteLocale" runat="server" ControlToValidate="txtEmailEnteLocale"
                            ErrorMessage="Email: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                    </div>
                    <div class="content" runat="server" id="pnlSoftwareHouse" visible="false">
                        <asp:Label runat="server" ID="lblPartitaIvaSoftwareHouse" Text="Partita Iva" AssociatedControlID="txtPartitaIvaSoftwareHouse" />
                        <asp:TextBox ID="txtPartitaIvaSoftwareHouse" runat="server" TabIndex="1" MaxLength="16" CssClass="input username" ValidationGroup="vgRecuperaCredenziali" Width="70%" />
                        <asp:RequiredFieldValidator ID="rfvPartitaIvaSoftwareHouse" runat="server" ControlToValidate="txtPartitaIvaSoftwareHouse"
                            ErrorMessage="Partita Iva: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                        <br /><br />
                        <asp:Label runat="server" ID="lbltxtEmailSoftwareHouse" Text="Email di registrazione" AssociatedControlID="txtEmailSoftwareHouse" />
                        <asp:TextBox ID="txtEmailSoftwareHouse" TabIndex="1" ValidationGroup="vgRecuperaCredenziali" runat="server" CssClass="input username" Width="70%" ToolTip="Email" />
                        <asp:RequiredFieldValidator ID="rfvtxtEmailSoftwareHouse" runat="server" ControlToValidate="txtEmailSoftwareHouse"
                            ErrorMessage="Email: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                    </div>
                    <div class="content" runat="server" id="pnlIspettore" visible="false">
                        <asp:Label runat="server" ID="lblPartitaIvaIspettore" Text="Partita Iva" AssociatedControlID="txtPartitaIvaIspettore" />
                        <asp:TextBox ID="txtPartitaIvaIspettore" runat="server" TabIndex="1" MaxLength="16" CssClass="input username" ValidationGroup="vgRecuperaCredenziali" Width="70%" />
                        <asp:RequiredFieldValidator ID="rfvPartitaIvaIspettore" runat="server" ControlToValidate="txtPartitaIvaIspettore"
                            ErrorMessage="Partita Iva: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                        <br /><br />
                        <asp:Label runat="server" ID="lbltxtEmailIspettore" Text="Email di registrazione" AssociatedControlID="txtEmailIspettore" />
                        <asp:TextBox ID="txtEmailIspettore" TabIndex="1" ValidationGroup="vgRecuperaCredenziali" runat="server" CssClass="input username" Width="70%" ToolTip="Email" />
                        <asp:RequiredFieldValidator ID="rfvtxtEmailIspettore" runat="server" ControlToValidate="txtEmailIspettore"
                            ErrorMessage="Email: campo obbligatorio" ValidationGroup="vgRecuperaCredenziali" Display="None" />
                    </div>


                    <div class="failureClass">
                        <asp:Label ID="lblMessage" runat="server" />
                    </div>
                    <!--END CONTENT-->
                    <div id="footer" class="footer">
                        <asp:Button runat="server" ID="RecuperoCredenziali_btnRecovery" ValidationGroup="vgRecuperaCredenziali" OnClick="RecuperoCredenziali_btnRecovery_Click" Text="RECUPERA PASSWORD" Width="200" CssClass="buttonClass" />  
                        <asp:CustomValidator ID="cvRecuperoPassword" runat="server" ForeColor="Red" ValidationGroup="vgRecuperaCredenziali" EnableClientScript="true" OnServerValidate="ControllaRecuperoPassword" 
                                ErrorMessage="<br/>La corrispondenza tra dei dati inseriti non è stata trovata sul sistema Criter! Contatta l'assistenza" />
                        <asp:ValidationSummary ID="RecuperoCredenziali_vschkRecuperoCredenziali" ValidationGroup="vgRecuperaCredenziali" runat="server" ShowMessageBox="True" ShowSummary="False"
                            HeaderText="Attenzione controllare i seguenti campi:"></asp:ValidationSummary>
                    </div>
                </div>
            </div>
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

