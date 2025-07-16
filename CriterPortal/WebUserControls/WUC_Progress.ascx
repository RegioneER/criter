<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_Progress.ascx.cs" Inherits="WebUserControls_WUC_Progress" %>

<asp:UpdateProgress DynamicLayout="false" ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ffffff; opacity: 0.7;">

            <div style="border-width: 0px; position: fixed; padding: 50px; font-size: 36px; left: 37%; top: 40%;">

                <div style="color: #000000;">Caricamento in corso...</div>
                <div>
                    <asp:Image runat="server" ImageUrl="~/images/Loader.gif" />
                </div>
                <div style="color: #000000">Si prega di attendere</div>
            </div>
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>