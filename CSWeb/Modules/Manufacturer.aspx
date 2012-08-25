<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manufacturer.aspx.cs" Inherits="Modules_Manufacturer" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <!--Header start-->
    <div>
        <uc1:Header ID="Header1" runat="server" />
    </div>
    <!--Header End-->
    <!--Body Start-->
    <div class="wrapper">
        <div class="main-table-wrapper">
            <div class="breadcrmbLeft">
                <input type="hidden" name="ctl00$ContentPlaceHolder1$currentReportName" id="currentReportName" />
                <div class="searchBox">
                    <asp:TextBox ID="txtSearch" value="Search" runat="server" class="searchBoxTxt" onkeypress="return SetDefaultButton(event,1);"
                        onfocus="if (this.value==&#39;Search&#39;) this.value=&#39;&#39;" onblur="if (this.value==&#39;&#39;) this.value=&#39;Search&#39;" />
                    <a id="lnkBtnSearch" class="searchBoxBtn" href="#"></a>
                    <div class="clear">
                    </div>
                </div>
                <div id="ContentPlaceHolder1_dvAddReport" class="fl">
                    <span class="btn5"><asp:LinkButton id="lnkAddNew" href="#" runat="server"><span class="AddNewData"></span>Add Data 1</asp:LinkButton></span>
                </div>
                <div id="ContentPlaceHolder1_dvCloneOrgReport" class="fl">
                    <span class="btn5"><asp:LinkButton id="lnkAddNew2" href="#" runat="server"><span class="AddNewData"></span>Add Data 2</asp:LinkButton> </span>
                </div>
                <div class="reports">
                    Manufacturer
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
            <div id="updMain">
                <div id="dvgridcontainer" class="grid_container">
                    &nbsp;
                </div>
            </div>
        </div>
        <div class="push">
        </div>
    </div>
    <!--Body End-->
    <!--Footer Start-->
    <div>
        <uc2:Footer ID="Footer1" runat="server" />
    </div>
    <!--Footer Start-->
    </form>
</body>
</html>

