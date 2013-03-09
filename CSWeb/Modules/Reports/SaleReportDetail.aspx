<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SaleReportDetail.aspx.cs"
    Inherits="Modules_SaleReportDetail" %>

<%@ Register Src="../../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
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
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" OnClick="lnkBtnSearch_Click"
                        CausesValidation="false"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dv3" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click">Refresh</asp:LinkButton>
                    </span>
                </div>
                <div id="Div1" class="fl">
                    <div class="demo" style="font-size: 9px!important;">
                        <p>
                            <input type="text" id="toDate" runat="server" class="txtCred" style="width: 80px!important" /></p>
                    </div>
                </div>
                <div id="dv2" class="fl">
                    <div class="demo" style="font-size: 9px!important;">
                        <p>
                            <input type="text" id="fromDate" runat="server" class="txtCred" style="width: 80px!important" /></p>
                    </div>
                </div>
                <div class="reports">
                    Sale Details
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
            <div id="updMain">
                <div id="dvgridcontainer" class="grid_container">
                    <div style="margin: 0px auto; padding: 0px; text-align: center;">
                        <div id="divMess" runat="server" visible="false">
                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                        </div>
                    </div>
                    <br style="clear: both" />
                    <div style="float: right; padding-bottom: 5px;padding-right:10px; font-weight: bolder">
                        [Cash Sale : €<span id='spCash' runat="server">0.00</span>]</div>
                    <div style="float: right; padding-bottom: 5px;;padding-right:10px; font-weight: bolder">
                        [Visa Sale : €<span id='spVisa' runat="server">0.00</span>]</div>
                    <div style="float: right; padding-bottom: 5px;;padding-right:10px; font-weight: bolder">
                        [BanContact Sale : €<span id='spBanContact' runat="server">0.00</span>]</div>
                    <br style="clear: both" />
                    <div class="grid_container">
                        <ctrl:CustomGridView ID="gvGrid" EmptyDataText="<span class='noDataSelected'>No Data Available</span>"
                            runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True"
                            Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle" SortColumn="FirstName"
                            DataKeyNames="SaleID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                            EndDateCol="3" OnPageIndexChanging="gvGrid_PageIndexChanging"
                            OnSorting="gvGrid_Sorting">
                            <Columns>
                                <asp:TemplateField HeaderText="Sale Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleOrder" runat="server" Text='<%# Eval("SaleOrder") %>' ToolTip='<%# Eval("SaleOrder") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer" SortExpression="FirstName">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCustomer" runat="server" Text='<%# Eval("FirstName")+" "+Eval("LastName") %>'
                                            ToolTip='<%# Eval("FirstName")+" "+Eval("LastName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sale Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleDate" runat="server" Text='<%# Eval("SaleDate","{0:dd-MMM-yyyy HH:mm}") %>'
                                            ToolTip='<%# Eval("SaleDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Detail">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductDetails" runat="server" Text='<%# Eval("ProductDetails") %>'
                                            ToolTip='<%# Eval("ProductDetails") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Price")) %>'
                                            ToolTip='<%# String.Format("{0:0.00}", Eval("Price")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Discount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Discount")) %>'
                                            ToolTip='<%# String.Format("{0:0.00}", Eval("Discount")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Discount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFDiscount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("FinalDiscount")) %>'
                                            ToolTip='<%# String.Format("{0:0.00}", Eval("FinalDiscount")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Payable">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFPAmount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("FinalPayableAmount")) %>'
                                            ToolTip='<%# String.Format("{0:0.00}", Eval("FinalPayableAmount")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ban Contact">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFPAmount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("BankAmount")) %>'
                                            ToolTip='<%# String.Format("{0:0.00}", Eval("BankAmount")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Visa">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFPAmount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("CCAmount")) %>'
                                            ToolTip='<%# String.Format("{0:0.00}", Eval("CCAmount")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cash">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFPAmount" runat="server" Text='<%# String.Format("{0:0.00}", Eval("Cash")) %>'
                                            ToolTip='<%# String.Format("{0:0.00}", Eval("Cash")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sale Made By" SortExpression="SaleMadeBy">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSaleMadeBy" runat="server" Text='<%# Eval("SaleMadeByName") %>'
                                            ToolTip='<%# Eval("SaleMadeByName") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                </asp:TemplateField>
                                
                            </Columns>
                            <RowStyle CssClass="tdData" />
                            <HeaderStyle CssClass="trHeader" />
                        </ctrl:CustomGridView>
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
<!--Date Picker-->
"
<script src="../../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="../../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<link href="../../Styles/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    $(function () {
        $("#fromDate").datepicker({
            showOn: "button",
            buttonImage: "../../images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0d"
        });
    });
    $(function () {
        $("#toDate").datepicker({
            showOn: "button",
            buttonImage: "../../images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0d"
        });
    });


</script>
<!--Date Picker-->
