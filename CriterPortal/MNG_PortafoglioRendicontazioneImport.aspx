<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MNG_PortafoglioRendicontazioneImport.aspx.cs" Inherits="MNG_PortafoglioRendicontazioneImport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoTestata" CssClass="TableClass" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        GESTIONE IMPORTAZIONE FILE DI RENDICONTAZIONE PAYER
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloFilePayer" AssociatedControlID="UploadFilePayer" Text="File di rendicontazione PayER (.zip) (*)" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <asp:FileUpload ID="UploadFilePayer" ToolTip="Scegli file" TabIndex="1" runat="server" Width="350px" />
                        <asp:RequiredFieldValidator ID="rfvUploadFilePayer" runat="server"
                            EnableClientScript="true" ForeColor="Red" SetFocusOnError="true" ValidationGroup="vgUploadFilePayer"
                            ErrorMessage="File di rendicontazione PayER .zip: campo necessario"
                            ControlToValidate="UploadFilePayer">&nbsp;*</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revUploadFilePayer" runat="server"
                            ErrorMessage="File di rendicontazione PayER .zip non valido"
                            EnableClientScript="true" CssClass="testoerr" SetFocusOnError="true"
                            ValidationExpression="^.+(.zip|.Zip|.zIp|.ziP|.ZIP)$" ValidationGroup="vgUploadFilePayer"
                            ControlToValidate="UploadFilePayer"></asp:RegularExpressionValidator>
                    </asp:TableCell>
                </asp:TableRow>
               
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="btnElaboraRendicontazionePayer" runat="server" Text="AVVIA ELABORAZIONE FILE PAYER" Width="250" 
                            CausesValidation="True" ValidationGroup="vgUploadFilePayer" 
                            OnClick="btnElaboraRendicontazionePayer_Click" CssClass="buttonClass" 
                            OnClientClick="javascript:return confirm('Confermi di importare il file di rendicontazione di PayER?');" />
                        <asp:ValidationSummary ID="vgUploadFilePayer" ValidationGroup="vgStorno" runat="server" CssClass="testoerr" ShowMessageBox="True"
                             ShowSummary="False" HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell ColumnSpan="4" HorizontalAlign="Left" CssClass="riempimento5">
                        <asp:Label runat="server" ID="lblMessage" Visible="false" ForeColor="Green" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <!-- -->
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnElaboraRendicontazionePayer" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>