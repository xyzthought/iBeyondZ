<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodePDF.aspx.cs" Inherits="Handler_BarcodePDF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <script type="text/javascript">
            document.write(window.opener.document.getElementById("divToPrint").innerHTML);
        </script>
    </div>
    </form>
</body>
</html>
