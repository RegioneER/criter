<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_Sanzioni.aspx.cs" Inherits="VER_Sanzioni" %>
<%@ Register Src="~/WebUserControls/Sanzioni/UCAccertamentiSanzioni.ascx" TagPrefix="ucAccertamentiSanzioni" TagName="UCAccertamentiSanzioni" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table Width="900" ID="tblInfoGenerali" runat="server">
                <%--<asp:TableRow>
                    <asp:TableCell ColumnSpan="2" HorizontalAlign="Left" CssClass="riempimento1">
						<h2>INFORMAZIONI GENERALI NOTIFICHE SANZIONI</h2>
                        
                        <asp:Label runat="server" ID="lblIDStatoAccertamentoSanzione" Visible="false" />
                        <asp:Label ID="lblIDTipoAccertamento" Visible="false" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>--%>
                
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <ucAccertamentiSanzioni:UCAccertamentiSanzioni ID="UCSanzione" runat="server" />
                    </asp:TableCell>
                </asp:TableRow>

               <%-- <asp:TableRow>
                    <asp:TableCell Width="900" ColumnSpan="2" CssClass="riempimento">
                       <uc1:UCRaccomandate runat="server" ID="UCRaccomandate" />
                    </asp:TableCell>
                </asp:TableRow>--%>

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