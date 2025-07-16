<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VER_InterventiScadenziario.aspx.cs" Inherits="VER_InterventiScadenziario" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table ID="tbl" runat="server" Width="900" CssClass="TableClass">
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloAziende" AssociatedControlID="cmbAziende" Text="Azienda" />
                        <br />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">

                        <dx:ASPxComboBox runat="server" ID="cmbAziende" TabIndex="1"
                            Theme="Default"
                            AutoPostBack="true"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="ASPxComboBox_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="ASPxComboBox_OnItemRequestedByValue"
                            OnButtonClick="ASPxComboBox1_ButtonClick"
                            TextFormatString="{0} {1} {2}"
                            BorderBottom-BorderStyle="None"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="CodiceSoggetto" Caption="Codice" Width="50" />
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Azienda" Width="200" />
                                <dx:ListBoxColumn FieldName="IndirizzoSoggetto" Caption="Indirizzo" Width="250" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />

                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">

                        <br />
                        <dx:ASPxCalendar ID="ASPxCalendar1"
                            runat="server"
                            EnableMonthNavigation="true"
                            EnableYearNavigation="true"
                            Width="100%"
                            ClientInstanceName="clientCalendar"
                            OnDayCellCreated="ASPxCalendar1_DayCellCreated"
                            OnDayCellPrepared="ASPxCalendar1_DayCellPrepared"
                            DayHeaderStyle-Font-Bold="true"
                            HeaderStyle-Font-Bold="true"
                            ShowWeekNumbers="false"
                            HeaderStyle-BackColor="#ffcc3d"
                            HeaderStyle-ForeColor="#000000"
                            DayHeaderStyle-BackColor="#f5f2f2"
                            CssClass="TableClass"
                            AutoPostBack="false"
                            ShowTodayButton="false"
                            ShowClearButton="false"
                            ReadOnly="true" 
                           />

                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>


            <!-- -->
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div>
                <br />
                <img alt="" src="images/loader.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
