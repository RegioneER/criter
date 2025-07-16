<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_Menu.ascx.cs" Inherits="WebUserControls_WUC_Menu" %>

  
    <asp:Repeater ID="rptMenu" runat="server" OnItemDataBound="rptMenu_OnItemDataBound">
        <HeaderTemplate>
            <nav id="dawgdrops"> 
            <ul class="nav-menu">
        </HeaderTemplate>
        <ItemTemplate>
            <li class="nav-item">
                <asp:HyperLink ID="hlprvMenu" runat="server" NavigateUrl='<%# Eval("PageUrl", "{0}") %>' Text='<%# Eval("Menu") %>' TabIndex="2" />
                <asp:Repeater runat="server" ID="rptMenuSotto" OnItemDataBound="rptMenuSotto_ItemDataBound">
                    <HeaderTemplate>
                        <div class="sub-nav"><ul class="sub-nav-group">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <asp:HyperLink ID="hlMenuSotto" runat="server" NavigateUrl='<%# Eval("PageUrl", "~/{0}") %>' Text='<%# Eval("Menu") %>' TabIndex="2"></asp:HyperLink>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                        </ul></div>
                    </FooterTemplate>
                </asp:Repeater>
            </li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
            </nav>
        </FooterTemplate>
    </asp:Repeater>
