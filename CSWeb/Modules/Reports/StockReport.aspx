<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockReport.aspx.cs" Inherits="Modules_Reports_StockReport" %>

<%@ Register Src="../../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
       
    </script>
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
                <div class="searchBox">
                    <asp:TextBox ID="txtSearch" value="Search" runat="server" class="searchBoxTxt" onkeypress="return SetDefaultButton(event,1);"
                        onfocus="if (this.value==&#39;Search&#39;) this.value=&#39;&#39;" onblur="if (this.value==&#39;&#39;) this.value=&#39;Search&#39;" />
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" OnClick="lnkBtnSearch_Click"
                        CausesValidation="False"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dv3" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click">Refresh</asp:LinkButton>
                    </span>
                </div>
                <div id="Div1" class="fl">
                    <div class="demo" style="font-size: 9px!important;margin-top:-5px;">
                        <p>
                            <input type="text" id="toDate" runat="server" class="txtCred" style="width: 80px!important" />
                            <asp:RequiredFieldValidator ID="reqDate" ControlToValidate="toDate" ErrorMessage="*"
                                Font-Size="X-Small" ForeColor="Red" runat="server"></asp:RequiredFieldValidator></p>
                    </div>
                </div>
                <div class="reports">
                    Stock Report
                </div>
                <div class="clear">
                </div>
            </div>
            <asp:UpdatePanel ID="updPanel" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                <ContentTemplate>
                    <div id="updMain">
                        <div id="dvgridcontainer" class="grid_container">
                            <div style="margin: 0px auto; padding: 0px; text-align: center;">
                                <div id="divMess" runat="server" visible="false">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <br style="clear: both" />
                            <div class="grid_container">
                                <ctrl:CustomGridView ID="gvGrid" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                    AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                    SortColumn="ProductName" DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ShowFooter="false" EmptyDataText="No Record Found"
                                    OnRowDataBound="gvGrid_RowDataBound" OnPageIndexChanging="gvGrid_PageIndexChanging"
                                    OnSorting="gvGrid_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProductID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product" SortExpression="ProductName">
                                            <ItemStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' ToolTip='Description : <br /><%#  Eval("Description") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand" SortExpression="Brand">
                                            <ItemStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("Brand") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Season" SortExpression="Season">
                                            <ItemStyle VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSeason" runat="server" Text='<%# Eval("Season") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BarCode">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBarCode" runat="server" Text='<%# Eval("BarCode") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" Visible="false">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantities") %>' />
                                                <div id="dvQtyDetails" style="display: none" runat="server">
                                                    <%# Eval("QuantityDetails") %></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' Style="width: 100px !important;
                                                    background-color: #eee !important; cursor: pointer; margin: 3px 13px 10px" />
                                                <div id="dvStockDetails" style="display: none" runat="server">
                                                    <%# Eval("StockDetails") %></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock Detail">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblStockDetails" runat="server" Text='<%# Eval("StockDetails") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buying Price">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblBuyingPrice" runat="server" Text='<%# String.Format("{0:0.00}",Eval("BuyingPrice")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VAT(%)">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblTax" runat="server" Text='<%# String.Format("{0:0.00}",Eval("Tax")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin(%)">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblMargin" runat="server" Text='<%# String.Format("{0:0.00}",Eval("Margin")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Selling Price">
                                            <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="lblSellingPrice" runat="server" Text='<%# String.Format("{0:0.00}",Eval("SellingPrice")) %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="tdData" />
                                    <HeaderStyle CssClass="trHeader" />
                                </ctrl:CustomGridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
