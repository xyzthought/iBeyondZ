<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clothing Shop |</title>
    <link href="Styles/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="aspNetHidden">
        </div>
        <div class="wrapper" style="background: #fff !important;">
            <div class="loginHeader">
                <div class="wrapperInnner">
                    <%-- <img src="#">--%></div>
            </div>
            <div class="shiftingContainer">
                <h1>
                    Clothing Shop ERP <span id="lblLocation" class="currentLocationTxt"></span>
                </h1>
                <div id="pnlLogin" class="loginShadow">
                    <div class="loginContainer">
                        <div id="pnlLocation">
                            <label>
                                &nbsp;
                            </label>
                        </div>
                        <div class="clear">
                        </div>
                        <label>
                            Username</label>
                        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        <span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required"
                                Display="Dynamic" ControlToValidate="txtUserName" CssClass="errorMsg"></asp:RequiredFieldValidator></span>
                        <label>
                            Password</label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                        <span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required"
                                Display="Dynamic" ControlToValidate="txtPassword" CssClass="errorMsg"></asp:RequiredFieldValidator></span>
                        <label id="errMsg" runat="server">
                            </label>
                        <p>
                            <asp:Button ID="btnLogin" class="btnLogin" runat="server" OnClick="btnLogin_Click" />
                        </p>
                    </div>
                </div>
            </div>
            <div class="push">
            </div>
        </div>
        <div class="footerContainer">
            <div class="footerInner">
                <span class="copyright">System maintained by <a href="www.ibeyondz.com" target="_blank"
                    style="color: #8F8F8F;">iBeyondz, Imagine behind Imagination</a></span>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
