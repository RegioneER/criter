<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UCBolliniView.ascx.cs" Inherits="WebUserControls_RapportiControlloTecnico_UCBolliniView" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<dx:ASPxGridView ID="gridBollini" ClientInstanceName="grid" runat="server"
    KeyFieldName="IDBollinoCalorePulito"
    Width="100%" Font-Size="Smaller"
    Border-BorderStyle="None"
    BorderBottom-BorderStyle="None"
    BorderLeft-BorderStyle="None"
    BorderRight-BorderStyle="None"
    BorderTop-BorderStyle="None"
    EnableCallBacks="False"
    EnableCallbackCompression="False"
    Styles-AlternatingRow-BackColor="#ffedad"
    Styles-Row-CssClass="GridItem"
    Styles-Header-HorizontalAlign="Center"
    Styles-Header-Font-Bold="true"
    Styles-Table-BackColor="#ffcc3d"
    Styles-Header-BackColor="#ffcc3d"
    Styles-EmptyDataRow-BackColor="#ffffff">
    <SettingsPager ShowDefaultImages="false" ShowDisabledButtons="false" Summary-Visible="false" />
    <SettingsText EmptyDataRow="Nessun bollino calore pulito" />
    <Columns>
        <dx:GridViewDataColumn FieldName="IDBollinoCalorePulito" VisibleIndex="1" Visible="false" />
        <dx:GridViewDataColumn FieldName="CodiceBollino" Caption="Codice bollino calore pulito" VisibleIndex="2"  />
        <dx:GridViewDataTextColumn FieldName="CostoBollino" CellStyle-HorizontalAlign="Center" Caption="Costo bollino calore pulito" VisibleIndex="3">
            <PropertiesTextEdit DisplayFormatString="{0:N2} €" />
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataColumn VisibleIndex="4" Caption="QRCode">
            <DataItemTemplate>
                <center>
                <asp:Image runat="server" ID="imgBarcodeBollini" Width="45px" Height="45px"
                    ImageUrl='<%# "~/" + ConfigurationManager.AppSettings["UploadBolliniCalorePulito"].ToString() + "/" + Eval("CodiceBollino") + ".png" %>' 
                    ToolTip="Barcode Codice bollino calore pulito" />
                    </center>                
            </DataItemTemplate>
        </dx:GridViewDataColumn>
    </Columns>
    <SettingsPager PageSize="5" />
</dx:ASPxGridView>

<asp:Label runat="server" ID="lblIDRapportoControlloTecnico" Visible="false" />





