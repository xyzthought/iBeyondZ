<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header.ascx.cs" Inherits="UserControls_Header" %>
<!--All Script tag should come here-->
<script src='<%=Page.ResolveUrl("~/Scripts/jquery-1.8.0.min.js")%>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Scripts/common.js")%>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Scripts/jquery-formatcurrency.js")%>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Scripts/jquery.ui.core.js")%>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Scripts/jquery.ui.widget.js")%>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Scripts/jquery.ui.position.js")%>' type="text/javascript"></script>
<script src='<%=Page.ResolveUrl("~/Scripts/jquery.ui.autocomplete.js")%>' type="text/javascript"></script>
<link href='<%=Page.ResolveUrl("~/Styles/jquery.ui.all.css")%>' rel="stylesheet" type="text/css" />
<link href='<%=Page.ResolveUrl("~/Styles/jquery.ui.autocomplete.css")%>' rel="stylesheet" type="text/css" />
<!--All CSS should come here-->
<link href='<%=Page.ResolveUrl("~/Styles/InnerStyle.css")%>' rel="stylesheet" type="text/css" />
<link href='<%=Page.ResolveUrl("~/Styles/Header.css")%>' rel="stylesheet" type="text/css" />
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="header">
    <div class="header-innerWrap">
        <div id="modalOverlayHeader" class="overlayHeader" style="z-index: 3000;">
        </div>
        <div class="modelwindowHeader" id="dvHeaderLoading" style="height: 100px; width: 100px;
            margin: -50px 0 0 -50px; display: none;">
            <img alt="Progress" src='<%=Page.ResolveUrl("~/Images/SaveProgress.gif")%>' title="Progress...please wait" />
        </div>
        <div style="float: left;">
            <div class="menu ddsmoothmenu" id="smoothmenu1">
                <ul>
                </ul>
                <div class="clear">
                </div>
                <ul>
                    <li id="mnuli1" runat="server"><a href='<%=Page.ResolveUrl("~/Modules/Landingpage.aspx")%>'>Home</a></li>
                    <li id="mnuli2" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/PlatformUser.aspx")%>'>
                        Platform User</a> </li>
                    <li id="mnuli3" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/Manufacturer.aspx")%>'>
                        Manufacturer</a></li>
                    <li id="mnuli4" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/Product.aspx")%>'>
                        Product</a></li>
                    <li id="mnuli5" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/BuyingInterface.aspx")%>'>
                        Purchase</a></li>
                    <li id="mnuli6" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/Sale.aspx")%>'>
                        Sale</a></li>
                    <li id="mnuli7" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/MasterData.aspx")%>'>
                        Master</a></li>
                    <li id="mnuli8" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/BarcodePrint.aspx")%>'>
                        Barcode</a></li>
                    <li id="mnuli9" runat="server" style="display: none"><a href='<%=Page.ResolveUrl("~/Modules/ManageCustomer.aspx")%>'>
                        Customer</a></li>
                    <li id="mnuli10" runat="server">
                        <asp:LinkButton ID="lnkMyAccount" runat="server" Style="color: #CCCCCC; text-decoration: none"
                            CausesValidation="False" OnClick="lnkMyAccount_Click">My Account</asp:LinkButton></li>
                    <li id="mnuli11" runat="server" style="text-align: left;width:120px;" clientidmode="Static">
                        <a href='<%=Page.ResolveUrl("~/Modules/Reports/StockReport.aspx")%>'>Reports</a>
                        <ul class="ulDashboard" id="ulSMenu" style="top: 35px; visibility: visible; left: 0px;
                            display: none;">
                            <li class="ulDashboard" style="z-index:100;position:relative; background-color: Black; text-align: left;width:120px;"><a style="width: 100px;cursor:pointer"
                                href='<%=Page.ResolveUrl("~/Modules/Reports/StockReport.aspx")%>'>Stock</a></li>
                            <li class="ulDashboard" style="z-index:100;position:relative;background-color: Black; text-align: left;width:120px;"><a style="width: 100px;cursor:pointer"
                                href='<%=Page.ResolveUrl("~/Modules/Reports/SaleReport.aspx")%>'>Item Sold</a></li>
                            <li class="ulDashboard" style="z-index:100;position:relative;background-color: Black; text-align: left;width:120px;"><a style="width: 100px;cursor:pointer"
                                href='<%=Page.ResolveUrl("~/Modules/Reports/SaleReportDetail.aspx")%>'>Sale Detail</a></li>
                        </ul>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </div>
        </div>
        <div class="Headermenu">
            <div class="wlcm">
                <span class="wlcmNote">Welcome <span id="lblUserName" runat="server"></span><span
                    id="spnUserType" runat="server"></span><span>|&nbsp;&nbsp;<asp:LinkButton ID="lnkAddNew"
                        href="#" runat="server" OnClientClick="ShowModalDiv('divChangePassword','dvInnerWindow',0)"
                        Style="color: #CCCCCC; text-decoration: none;">Change Password</asp:LinkButton>&nbsp;&nbsp;|</span></span><span>
                            <asp:LinkButton ID="lnkLogout" runat="server" Style="color: #CCCCCC; text-decoration: none"
                                CausesValidation="False" OnClick="lnkLogout_Click">Logout</asp:LinkButton></span>
            </div>
        </div>
        <div class="logo" style="display: none;">
        </div>
        <div class="clear">
        </div>
    </div>
    <iframe id="ifSession" height="1px" width="1px" src="" style="display: none"></iframe>
</div>
<div id="divChangePassword" style="display: none; z-index: 1000" clientidmode="Static">
    <div class="mainModalAddEdit" id="mainModalAddDataSource">
        <div class="topM">
            <h1>
                <span id="spTitle">Change Password</span><a onclick="return CloseAddDiv('divChangePassword');"
                    id="lnkCloseAddDiv" title="Close"> </a>
            </h1>
        </div>
        <div id="MidM2" class="MidM">
            <div class="addNew" id="addNew2">
                <div id="updDataSource">
                    <div id="dvInnerWindow" class="modalContent">
                        <fieldset class="fieldAddEdit">
                            <div class="inner">
                                <div class="mandet">
                                    <span id="lblMessage">* Fields are mandatory</span></div>
                                <div class="errorMsg">
                                    <span id="lblError" runat="server"></span>
                                </div>
                                <div>
                                    Old Password :<span class="mandet2">* </span>
                                </div>
                                <div class="alt">
                                    <asp:TextBox ID="txtOldPassword" runat="server" CssClass="txtCred" MaxLength="50"
                                        TextMode="Password" ValidationGroup="CP"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtOldPassword" Display="Dynamic"
                                        ValidationGroup="CP"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    New Password :<span class="mandet2">* </span>
                                </div>
                                <div class="alt" style="margin-bottom: 5px;">
                                    <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txtCred" MaxLength="50"
                                        TextMode="Password" ValidationGroup="CP"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtFirstName" runat="server" ErrorMessage="*"
                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtNewPassword" Display="Dynamic"
                                        ValidationGroup="CP"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    Confirm Password :<span class="mandet2">* </span>
                                </div>
                                <div class="alt" style="margin-bottom: 5px;">
                                    <asp:TextBox ID="txtCPassword" runat="server" CssClass="txtCred" MaxLength="50" TextMode="Password"
                                        ValidationGroup="CP"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtSecondName" runat="server" ErrorMessage="*"
                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCPassword" Display="Dynamic"
                                        ValidationGroup="CP"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ControlToValidate="txtCPassword" ControlToCompare="txtNewPassword"
                                        ID="reqCom" runat="server" ErrorMessage="Mismatching..." Font-Size="X-Small"
                                        ForeColor="Red" Display="Dynamic" ValidationGroup="CP"></asp:CompareValidator>
                                </div>
                                <div class="btn-wrapper4">
                                    <span class="btn">
                                        <asp:LinkButton ID="lnkBtnSaveDS" runat="server" OnClick="lnkBtnSaveDS_Click" ValidationGroup="CP">Save</asp:LinkButton></span>
                                    <span class="btn">
                                        <asp:LinkButton ID="lnkCancel" runat="server" OnClientClick="return CloseAddDiv('divChangePassword');">Cancel</asp:LinkButton>
                                    </span>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottomM">
        </div>
    </div>
</div>
<div id="divMyAccount" style="display: none; z-index: 1000" clientidmode="Static">
    <div class="mainModalAddEdit" id="Div2">
        <div class="topM">
            <h1>
                <span id="Span1">My Account</span><a onclick="return CloseAddDiv('divMyAccount');"
                    id="A1" title="Close"> </a>
            </h1>
        </div>
        <div id="Div3" class="MidM">
            <div class="addNew" id="Div4">
                <div id="Div5">
                    <div id="Div6" class="modalContent">
                        <fieldset class="fieldAddEdit">
                            <div class="inner">
                                <div class="mandet">
                                    <span id="Span2">* Fields are mandatory</span></div>
                                <div class="errorMsg">
                                    <span id="MyAccountError" runat="server"></span>
                                </div>
                                <div>
                                    First Name :<span class="mandet2">* </span>
                                </div>
                                <div class="alt">
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtCred" MaxLength="50" ValidationGroup="MA"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtFirstName" Display="Dynamic"
                                        ValidationGroup="MA"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    Last Name :<span class="mandet2">* </span>
                                </div>
                                <div class="alt" style="margin-bottom: 5px;">
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="txtCred" MaxLength="50" ValidationGroup="MA"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtLastName" Display="Dynamic"
                                        ValidationGroup="MA"></asp:RequiredFieldValidator>
                                </div>
                                <div>
                                    Email ID :<span class="mandet2">* </span>
                                </div>
                                <div class="alt" style="margin-bottom: 5px;">
                                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtCred" MaxLength="50" ValidationGroup="MA"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtEmailID" Display="Dynamic"
                                        ValidationGroup="MA"></asp:RequiredFieldValidator>
                                    <br />
                                    <asp:RegularExpressionValidator ID="RegEmail" runat="server" ErrorMessage="Invalid Email"
                                        Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID"
                                        ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"
                                        ValidationGroup="MA"></asp:RegularExpressionValidator>
                                </div>
                                <div class="btn-wrapper4">
                                    <span class="btn">
                                        <asp:LinkButton ID="lnkSaveMyAccount" runat="server" OnClick="lnkSaveMyAccount_Click"
                                            ValidationGroup="MA">Save</asp:LinkButton></span> <span class="btn">
                                                <asp:LinkButton ID="lnkMCCancel" runat="server" OnClientClick="return CloseAddDiv('divMyAccount');">Cancel</asp:LinkButton>
                                            </span>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
        <div class="bottomM">
        </div>
    </div>
</div>
<script type="text/javascript">
    $(document).ready(function () {

        $('#mnuli11').hover(
         function () {
             $('#mnuli11').css('background-color', 'Black');
             $('#mnuli11').css('text-align', 'left');
             //$('div').removeClass('wrapper');
             $('#ulSMenu').show();
         }
     );
         //smoothmenu1
         $('li').not('#mnuli11').hover(
            function () {
                $('#mnuli11').css('background-color', '');
                //$('div').addClass('wrapper');
             $('#ulSMenu').hide();
         }
     );

    });
</script>
