<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCIspezioneDocumenti.ascx.cs" Inherits="WebUserControls_Ispezioni_UCIspezioneDocumenti" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<script type="text/javascript">
    function OnFileUploadComplete(s, e) {
        if (e.callbackData !== "")
        {
            gridIspezioniDocumenti.Refresh();
            //lblFileName.SetText(e.callbackData);
            //btnDeleteFile.SetVisible(true);
        }
    }
</script>


<dx:ASPxGridView ID="gridIspezioniDocumenti" ClientInstanceName="gridIspezioniDocumenti" runat="server"
    KeyFieldName="IDIspezioneDocumento" 
    AutoGenerateColumns="False"
    Width="890"
    Border-BorderStyle="None"
    BorderBottom-BorderStyle="None"
    BorderLeft-BorderStyle="None"
    BorderRight-BorderStyle="None"
    BorderTop-BorderStyle="None"
    EnableCallBacks="False"
    EnableCallbackCompression="False"
    Styles-AlternatingRow-BackColor="#ffedad"
    Styles-Row-CssClass="GridItem"
    Styles-Header-Font-Bold="true"
    Styles-Table-BackColor="#ffcc3d"
    Styles-Header-BackColor="#ffcc3d"
    Styles-EmptyDataRow-BackColor="#ffffff" OnHtmlRowCreated="gridIspezioniDocumenti_HtmlRowCreated"
    >
    <SettingsPager ShowDefaultImages="false" PageSize="10" ShowDisabledButtons="false" Summary-Visible="false" />
    <SettingsText EmptyDataRow="Nessun documento nella visita ispettiva" />
    <SettingsBehavior AllowSort="false" />
    <Columns>
        <dx:GridViewDataColumn FieldName="IDIspezioneDocumento" VisibleIndex="0" Visible="false" />
        <dx:GridViewDataColumn FieldName="IDTipoDocumentoIspezione" VisibleIndex="1" Visible="false" />
        <dx:GridViewDataColumn FieldName="TipoDocumentoIspezione" Width="350" VisibleIndex="2" Caption="Documento" />
        <dx:GridViewDataColumn FieldName="NomeDocumentoIspezione" VisibleIndex="3" Visible="false" /> 
        <dx:GridViewDataColumn FieldName="Estensione" VisibleIndex="4" Visible="false" />
        <dx:GridViewDataTextColumn VisibleIndex="6" Width="30" Caption="Download" CellStyle-HorizontalAlign="Center">
            <DataItemTemplate>
                <asp:ImageButton runat="server" ID="imgDocument" ImageUrl="~/images/Buttons/detailsSmall.png" TabIndex="1" ToolTip="Scarica documenti" />

                <asp:ImageButton runat="server" ID="imgDocumentRapportoIspezioneVuoto" ImageUrl="~/images/Buttons/detailsBookSmall.png" Visible="false" TabIndex="1" ToolTip="Scarica rapporto di ispezione vuoto" />
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="7" Width="30" Caption="Obbligatorio" CellStyle-HorizontalAlign="Center">
            <DataItemTemplate>
                <asp:Label runat="server" ID="lblDocumentoObbligatorio" />   
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn VisibleIndex="8" Width="30" Caption="Compilato" CellStyle-HorizontalAlign="Center">
            <DataItemTemplate>
                <asp:Image runat="server" ID="imgDocumentoCompilato" ToolTip="Compilato" />   
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn UnboundType="Object" VisibleIndex="9" Width="150" ToolTip="Carica documenti">
            <DataItemTemplate>
                <dx:ASPxUploadControl ID="uploadDocument" ShowProgressPanel="true" UploadMode="Standard" AutoStartUpload="true" FileUploadMode="OnPageLoad"
                    OnFileUploadComplete="uploadDocument_FileUploadComplete" runat="server" Theme="DevEx">
                    <ValidationSettings MaxFileSize="5000000" MaxFileSizeErrorText="Dimensione massima consentita del file 5 Mb" AllowedFileExtensions=".docx,.doc,.pdf" />
                  <ClientSideEvents FileUploadComplete="OnFileUploadComplete" />  
                </dx:ASPxUploadControl>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataColumn FieldName="NomeDocumento" Width="100" VisibleIndex="10" Caption="Link Documenti">
            <DataItemTemplate>
                <a runat="server" id="linkButtonDownloadDocumento" tooltip="Download documento"><%# Eval("NomeDocumento") %> - [Scarica]</a>
            </DataItemTemplate>
        </dx:GridViewDataColumn>
    </Columns>
</dx:ASPxGridView>

<asp:Label runat="server" ID="lblIDIspezione" Visible="false" />
<asp:Label runat="server" ID="lblIDIspezioneVisita" Visible="false" />
<asp:Label runat="server" ID="lblCodiceIspezione" Visible="false" />
