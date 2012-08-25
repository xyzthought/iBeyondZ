<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_Header" %>
<!--All Script tag should come here-->
<script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
<!--All CSS should come here-->
<link href="../Styles/InnerStyle.css" rel="stylesheet" type="text/css" />
<link href="../Styles/Header.css" rel="stylesheet" type="text/css" />
<div class="header">
    <div class="header-innerWrap">
        <div id="modalOverlayHeader" class="overlayHeader" style="z-index: 3000;">
        </div>
        <div class="modelwindowHeader" id="dvHeaderLoading" style="height: 100px; width: 100px;
            margin: -50px 0 0 -50px; display: none;">
            <img alt="Progress" src="../Images/SaveProgress.gif" title="Progress...please wait" />
        </div>
        <div style="float: left;">
            <div class="menu ddsmoothmenu" id="smoothmenu1">
                <ul>
                </ul>
                <div class="clear">
                </div>
                <ul>
                    <li id="mnuli1"><a href='../Modules/Landingpage.aspx'>Home</a></li>
                    <li id="mnuli6"><a href="../Modules/PlatformUser.aspx">Platform User</a>
                        <%--<ul id="HeaderControl_ulDashboard" class="ulDashboard" style="width: 245px">
                            <li><a title="ddd" href="#" style="display: inline-block; width: 225px;">ddd</a></li><li>
                        </ul>--%>
                    </li>
                    <li id="mnuli7"><a href='../Modules/Manufacturer.aspx'>Manufacturer</a></li>
                    <li id="mnuli8"><a href='../Modules/Product.aspx'>Product</a></li>
                    <li id="mnuli9"><a href='../Modules/Sale.aspx'>Sale</a></li>
                    <li id="mnuli10"><a href='../Modules/Report.aspx'>Report</a></li>
                    <li id="mnuli5"><a href="#nogo" onclick="changeCss();OpenMyAccount()">My Account</a></li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="Headermenu">
            <div class="wlcm">
                <span class="wlcmNote">Welcome <span id="lblUserName" runat="server"></span><span
                    id="spnUserType" runat="server"></span><span>| <b><a onclick="return OpenChangePassword();"
                        id="HeaderControl_LinkButton1" href="#" style="color: #CCCCCC; text-decoration: none">
                        Change Password</a></b></span> <span>| <b>
                            <asp:LinkButton ID="lnkLogout" runat="server" OnClick="lnkLogout_Click" Style="color: #CCCCCC;
                                text-decoration: none">Logout</asp:LinkButton></b></span> </span>
            </div>
        </div>
        <div class="logo" style="display: none;">
            <img id="HeaderControl_imgLogo" src="/ApplicationData/OrgLogo/634437894119684509.gif"
                alt="Logo" />
        </div>
        <div class="clear">
        </div>
    </div>
    <iframe id="ifSession" height="1px" width="1px" src="" style="display: none"></iframe>
</div>
<asp:ScriptManager ID="ScriptManager1"   runat="server"></asp:ScriptManager>