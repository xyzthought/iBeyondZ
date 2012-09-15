<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditProduct.aspx.cs" Inherits="Modules_AddEditProduct" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Product</title>
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
                <div class="reports">
                    Home | Top Information
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
            <div id="updMain">
             <div id="MidM2" class="MidM">
                                <div class="addNew" id="addNew2">
                <table>
                    <tr>
                        <td>
                            Product Name
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Description
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                    <td>Manufacturer</td>
                    <td>
                        <asp:DropDownList ID="cmbManufacturer" runat="server">
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                    <td>Category</td>
                    <td>
                        <asp:DropDownList ID="cmbCategory" runat="server">
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr>
                    <td>Size</td>
                    <td>
                        <asp:CheckBoxList ID="chkSize" runat="server">
                        </asp:CheckBoxList>
                    </td>
                    </tr>
                    <tr>
                    <td>Buying Price</td>
                    <td>
                        <asp:TextBox ID="txtBuyingPrice" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>Tax</td>
                    <td>
                        <asp:TextBox ID="txtTax" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>TaMarginx</td>
                    <td>
                        <asp:TextBox ID="txtMargin" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>Selling Price</td>
                    <td>
                        <asp:TextBox ID="txtSellingPrice" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>Bar Code</td>
                    <td>
                        <asp:TextBox ID="txtBarcode" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                    <td>
                        <asp:LinkButton ID="lnkSave" runat="server" onclick="lnkSave_Click">Save</asp:LinkButton></td>
                    <td>
                        <asp:LinkButton ID="lnkCancel" runat="server">Cancel</asp:LinkButton></td>
                    </tr>
                </table>
                </div>
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
