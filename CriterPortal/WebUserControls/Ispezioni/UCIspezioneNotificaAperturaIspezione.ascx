<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCIspezioneNotificaAperturaIspezione.ascx.cs" Inherits="WebUserControls_Ispezioni_UCIspezioneNotificaAperturaIspezione" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Table Width="890" ID="tblInfoGenerali" runat="server">
     <asp:TableRow>
         <asp:TableCell ColumnSpan="2" CssClass="riempimento1"><h3>Notifiche riapertura Ispezione</h3></asp:TableCell>
     </asp:TableRow>

    <asp:TableRow>
       <asp:TableCell ColumnSpan="2">
            <dx:ASPxGridView ID="gridIspezioniNotifiche" ClientInstanceName="gridIspezioniNotifiche" runat="server"
                KeyFieldName="IDNotificaRiaperturaIspezione" 
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
                Styles-EmptyDataRow-BackColor="#ffffff"
                >
                <SettingsPager ShowDefaultImages="false" PageSize="10" ShowDisabledButtons="false" Summary-Visible="false" />
                <SettingsText EmptyDataRow="Nessuna notifica di riapertura ispezione" />
                <SettingsBehavior AllowSort="false" />
                <Columns>
                    <dx:GridViewDataColumn FieldName="IDNotificaRiaperturaIspezione" VisibleIndex="0" Visible="false" />
                    <dx:GridViewDataTextColumn FieldName="DataNotifica" VisibleIndex="1" PropertiesTextEdit-DisplayFormatString="dd/MM/yyyy HH:mm:ss"  Width="190px" Caption="Data Notifica" />
                        
                    <dx:GridViewDataColumn FieldName="Utente" VisibleIndex="2" Width="300px" Caption="Utente" />
                    <dx:GridViewDataColumn FieldName="NotificaAdIspettore" Width="600" VisibleIndex="3" Caption="Notifica" />
                </Columns>
            </dx:ASPxGridView>
            <asp:Label runat="server" ID="lblIDIspezione" Visible="false" />
       </asp:TableCell>
    </asp:TableRow>
</asp:Table>