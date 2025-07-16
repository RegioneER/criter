<%@ Page Title="Gestione Report" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="COM_Reports.aspx.cs" Inherits="COM_Reports" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Table Width="900" ID="tblInfoTestata" runat="server">
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
                        GESTIONE REPORTS CRITER
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowDownloadEnti" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento">
                        Per scaricare i dati fai click&nbsp;
                        <asp:HyperLink ID="imgExportXlsEnti" runat="server" CssClass="lnkLink" Font-Underline="true" Text="qui" ToolTip="Scarica i dati" Target="_blank" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow runat="server" ID="rowListReports">
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="COM_Report_lblTitoloReport" AssociatedControlID="ddlReport" Text="Report" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox ID="ddlReport" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReport_SelectedIndexChanged" Theme="Default" TabIndex="1"
                            TextField="ReportDescription" ValueField="IDReport"
                            Width="370px" DropDownWidth="370px">
                            <ItemStyle Border-BorderStyle="None" CssClass="selectClass" />
                        </dx:ASPxComboBox>
                        <asp:RequiredFieldValidator ID="rfvddlReport" runat="server" 
                            ForeColor="Red"
                            Display="Dynamic"
                            ValidationGroup="vgReport" 
                            InitialValue="0" 
                            ErrorMessage="Report: campo obbligatorio"
                            ControlToValidate="ddlReport">&nbsp;*</asp:RequiredFieldValidator>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowListReports1">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                            &nbsp;
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowListReports2">
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Center" CssClass="riempimento5">
                        <asp:Button ID="COM_Reports_btnProcess" runat="server" CssClass="buttonClass" Width="200" ValidationGroup="vgReport" OnClick="COM_Reports_btnProcess_Click" Text="VISUALIZZA REPORT" />
                        <asp:ValidationSummary ID="ANAG_Iscrizione_vschkIscrizione" runat="server" ValidationGroup="vgReport" ShowMessageBox="True" ShowSummary="False"
                            HeaderText="Attenzione, ricontrollare i seguenti campi:"></asp:ValidationSummary>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow runat="server" ID="rowViewReport" Visible="false">
                    <asp:TableCell ColumnSpan="2" CssClass="riempimento5">
                        <br />
                        <rsweb:ReportViewer ID="ReportViewer1" AsyncRendering="false"
                            runat="server" Font-Names="Arial" style="display:table; overflow-y:auto !important;" 
                            Font-Size="7pt" Height="800px"
                            Width="100%" ProcessingMode="Remote" KeepSessionAlive="false"
                            ShowWaitControlCancelLink="true" WaitMessageFont-Names="Arial"
                            WaitMessageFont-Size="10pt" BackColor="#ECE9D8" SplitterBackColor="#ECE9D8"
                            ZoomPercent="90" EnableTheming="true" ExportContentDisposition="AlwaysAttachment" PageCountMode="Actual" WaitControlDisplayAfter="10">
                        </rsweb:ReportViewer>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <img alt="Caricamento" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>