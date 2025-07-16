<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WUC_Tile.ascx.cs" Inherits="WebUserControls_WUC_Tile" %>

<div id="mainform">
<div class="button-holder">
    <asp:DataList ID="DLTile" runat="server" CssClass="TableClass" RepeatDirection="Horizontal" RepeatColumns="4">
        <ItemTemplate>
            <div id="DivTile" class='<%# Eval("TileCssClassDimension") %> <%# Eval("TileCssClassColor")%> shadow-blue floatleft margin-5'>
                <a href='<%# Eval("PageUrl")%>' id="lnkTile" class="gradiente">
                   
                    <div class="outer-grid">
                        <div style="width: 33%; display: inline-block; float: left; vertical-align: middle;">
                            <img id="imgTile" runat="server" src='<%# Eval("TileImage")%>' alt='<%# Eval("Menu")%>' style="margin-left: 10px;"/>
                        </div>
                        <div style="width: 66%; display: inline-block; float: left; vertical-align: middle;">
                            <span class="light-text"><%# Eval("Menu")%></span>
                        </div>
                    </div>
                    
                </a>
            </div>
        </ItemTemplate>
    </asp:DataList>

    <!-- Button 1 -->
    <%--<div class="tile-bt-2 solid-orange-2 shadow-blue floatleft margin-5">
        <a href="COM_AnagraficaSearch.aspx" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn1" alt="" />
            <span class="light-text">Prova titolo tiles</span>
        </a>
    </div>
    <!-- End Button 1 -->
    <!-- Button 2 -->
    <div class="tile-bt-2 solid-red-2 shadow-red floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn2" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 2 -->

    <!-- Button 3 -->
    <div class="tile-bt-2 solid-grass shadow-green floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn3" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 3 -->

    <!-- Button 4 -->
    <div class="tile-bt-2 solid-olive shadow-green floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn4" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 4 -->

    <!-- Button 5 -->
    <div class="tile-bt-2 solid-pink shadow-red floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn5" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 5 -->

    <!-- Button 6 -->
    <div class="tile-bt-2 solid-purple shadow-red floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn6" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 6 -->

    <!-- Button 7 -->
    <div class="tile-bt-2 solid-blue shadow-blue floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn7" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 7 -->

    <!-- Button 8 -->
    <div class="tile-bt-2 solid-blue-2 shadow-blue floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="btn8" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 8 -->

    <!-- Button 9 -->
    <div class="tile-bt-2 solid-grass shadow-blue floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="Img1" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 9 -->

    <!-- Button 10 -->
    <div class="tile-bt-2 solid-gold shadow-blue floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="Img2" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 10 -->

    <!-- Button 11 -->
    <div class="tile-bt-2 solid-olive shadow-blue floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="Img3" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Button 11 -->

    <!-- Button 12 -->
    <div class="tile-bt-2 solid-orange-3 shadow-blue floatleft margin-5">
        <a href="javascript:void(0);" class="gradiente">
            <img src="~/images/Tiles/MB_0000_calendar.png" runat="server" id="Img4" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>--%>
    <!-- End Button 12 -->

    <div class="clearspace"></div>
</div>

<%--<div class="button-holder">


    <!-- Small Button 1 -->
    <div class="tile-bt solid-blue-2 shadow-blue floatleft margin-5">
        <a href="#" class="gradient">
            <img src="images/m-big-char.png" alt="" />
            <span class="light-text">Tile Button</span>
        </a>
    </div>
    <!-- End Small Button 1 -->

    <!-- Small Button 2 -->
    <div class="tile-bt solid-darkblue shadow-blue floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 2 -->

    <!-- Small Button 3 -->
    <div class="tile-bt solid-violetred shadow-black floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 3 -->

    <!-- Small Button 4 -->
    <div class="tile-bt solid-purple shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 4 -->

    <!-- Small Button 5 -->
    <div class="tile-bt solid-orange shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 5 -->

    <!-- Small Button 6 -->
    <div class="tile-bt solid-coral shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 6 -->

    <!-- Small Button 7 -->
    <div class="tile-bt solid-green-2 shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 7 -->

    <!-- Small Button 8 -->
    <div class="tile-bt solid-yellow shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="dark-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 8 -->

    <!-- Small Button 9 -->
    <div class="tile-bt solid-darkgreen shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 9 -->

    <!-- Small Button 10 -->
    <div class="tile-bt solid-red shadow-red floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 10 -->

    <!-- Small Button 11 -->
    <div class="tile-bt solid-olive shadow-red floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 11 -->

    <!-- Small Button 12 -->
    <div class="tile-bt solid-darkred shadow-red floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 12 -->

    <!-- Small Button 13 -->
    <div class="tile-bt solid-red-2 shadow-red floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 13 -->

    <!-- Small Button 14 -->
    <div class="tile-bt solid-blue shadow-red floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="dark-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 14 -->

    <!-- Small Button 15 -->
    <div class="tile-bt solid-black shadow-black floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 15 -->

    <!-- Small Button 16 -->
    <div class="tile-bt solid-grass shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="dark-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 16 -->

    <!-- Small Button 17 -->
    <div class="tile-bt solid-pink shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 17 -->

    <!-- Small Button 18 -->
    <div class="tile-bt solid-orange-2 shadow-green floatleft margin-5">
        <a href="#" class="gradient">
            <img src="../images/Tiles/MB_0000_calendar.png" alt="" />
            <span class="light-text">Button Title</span>
        </a>
    </div>
    <!-- End Small Button 18 -->



    <div class="clearspace"></div>

</div>--%>
</div>
