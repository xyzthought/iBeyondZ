<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="Modules_LandingPage" %>

<%@ Register src="../UserControls/Header.ascx" tagname="Header" tagprefix="uc1" %>

<%@ Register src="../UserControls/Footer.ascx" tagname="Footer" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clothing Shop |</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:Header ID="Header1" runat="server" />
    </div>
    <div>

    </div>
    <div>
        <uc2:Footer ID="Footer1" runat="server" />
    </div>
    </form>
</body>
</html>
