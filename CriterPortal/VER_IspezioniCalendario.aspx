<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="VER_IspezioniCalendario.aspx.cs" Inherits="VER_IspezioniCalendario" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentDisplay" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <!-- -->
            <asp:Table ID="tbl" runat="server" Width="900" CssClass="TableClass">
                <asp:TableRow>
                    <asp:TableCell Width="300" CssClass="riempimento2">
                        <asp:Label runat="server" ID="lblTitoloIspettore" AssociatedControlID="cmbIspettore" Text="Ispettore" />
                    </asp:TableCell>
                    <asp:TableCell Width="600" CssClass="riempimento">
                        <dx:ASPxComboBox runat="server" ID="cmbIspettore" TabIndex="1"
                            Theme="Default"
                            AutoPostBack="true"
                            EnableCallbackMode="true"
                            CallbackPageSize="30"
                            IncrementalFilteringMode="Contains"
                            ValueType="System.String"
                            ValueField="IDSoggetto"
                            OnItemsRequestedByFilterCondition="cmbIspettore_OnItemsRequestedByFilterCondition"
                            OnItemRequestedByValue="cmbIspettore_OnItemRequestedByValue"
                            OnButtonClick="cmbIspettore_ButtonClick"
                            TextFormatString="{0}"
                            Width="450px"
                            AllowMouseWheel="true">
                            <ItemStyle Border-BorderStyle="None" />
                            <Buttons>
                                <dx:EditButton Text="x" Width="12px" Position="Right" ToolTip="Cancella selezione"></dx:EditButton>
                            </Buttons>
                            <Columns>
                                <dx:ListBoxColumn FieldName="Soggetto" Caption="Ispettore" Width="400" />
                            </Columns>
                        </dx:ASPxComboBox>
                        <asp:Label ID="lblIDSoggetto" runat="server" Visible="false" />
                        <asp:Label ID="lblSoggetto" runat="server" Visible="false" Font-Bold="true" />
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell ColumnSpan="2">
                        <%--<asp:calendar ID="ASPxCalendar1" runat="server"
				            OnDayCellCreated="ASPxCalendar1_DayCellCreated"
                            OnDayCellPrepared="ASPxCalendar1_DayCellPrepared"
                            Width="100%" BorderWidth="0px"
				            NextPrevFormat="FullMonth" ShowGridLines="True" DayNameFormat="Full">
				            <TitleStyle CssClass="riempimento2" Font-Bold="true" />
                            <DayHeaderStyle CssClass="riempimento2" Font-Bold="true" />
                            <DayStyle CssClass="riempimento4" />
                            <NextPrevStyle />
                            <TodayDayStyle BackColor="#ffffff" />
				            <SelectedDayStyle CssClass="riempimento2"></SelectedDayStyle>
				            <OtherMonthDayStyle></OtherMonthDayStyle>
				            <WeekendDayStyle BackColor="#f8f2dc"></WeekendDayStyle>
			            </asp:calendar>--%>



                        <dx:ASPxCalendar ID="ASPxCalendar1"
                            runat="server"
                            EnableMonthNavigation="true"
                            EnableYearNavigation="true"
                            Width="100%"
                            ClientInstanceName="clientCalendar"
                            OnDayCellCreated="ASPxCalendar1_DayCellCreated"
                            OnDayCellPrepared="ASPxCalendar1_DayCellPrepared"
                            DaySelectedStyle-BackColor="#ffcc3d"
                            ShowWeekNumbers="false"
                            HeaderStyle-BackColor="#ffcc3d"
                            HeaderStyle-ForeColor="#000000"
                            DayHeaderStyle-BackColor="#ffcc3d"
                            DayStyle-BackColor="#f5f2f2"
                            AutoPostBack="false"
                            ShowTodayButton="false"
                            ShowClearButton="false"
                            ReadOnly="true" 
                            DayOtherMonthStyle-BackColor="#ffcc3d"
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