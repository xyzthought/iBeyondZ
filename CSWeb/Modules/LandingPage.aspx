<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="Modules_LandingPage" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Clothing Shop |</title>
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
                <div>
                    <div style="width: 48%;float:left">
                        <div id="dvgridcontainer" class="grid_container">
                            <div id="Div1">
                                <div id="Div2" class="grid_container">
                                    <div style="margin: 0px auto; padding: 0px; text-align: center;">
                                        <div id="divMess" runat="server" visible="false">
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <br style="clear: both" />
                                    <div style="color: #626262; font-size: 16px; font-weight: bold; padding-bottom: 5px;">
                                        Top 10 Selling Products</div>
                                    <div class="grid_container">
                                        <ctrl:CustomGridView ID="gvGridTopSellingProduct" EmptyDataText="<span class='noDataSelected'>No Data Available</span>"
                                            runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True"
                                            Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle" SortColumn="ProductName"
                                            DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                            EndDateCol="3" OnSorting="gvGrid_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product" SortExpression="ProductName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' ToolTip='<%# Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size" SortExpression="SizeName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSizeName" runat="server" Text='<%# Eval("SizeName") %>' ToolTip='<%# Eval("SizeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Quantity" SortExpression="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' ToolTip='<%# Eval("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Price" SortExpression="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%#String.Format("{0:C}", Eval("Price")) %>'
                                                            ToolTip='<%# Eval("Price") %>'></asp:Label>
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
                    </div>
                    <div style="width: 48%;float:right">
                        <div id="Div3" class="grid_container">
                            <div id="Div4">
                                <div id="Div5" class="grid_container">
                                    <div style="margin: 0px auto; padding: 0px; text-align: center;">
                                        <div id="div6" runat="server" visible="false">
                                            <asp:Label ID="Label1" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <br style="clear: both" />
                                    <div style="color: #626262; font-size: 16px; font-weight: bold; padding-bottom: 5px;">
                                        Last 7 Days | Top 10 Selling Products</div>
                                    <div class="grid_container">
                                        <ctrl:CustomGridView ID="L7DaysTop10" EmptyDataText="<span class='noDataSelected'>No Data Available</span>"
                                            runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True"
                                            Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle" SortColumn="ProductName"
                                            DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                            EndDateCol="3" OnSorting="gvGrid_Sorting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Product" SortExpression="ProductName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' ToolTip='<%# Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size" SortExpression="SizeName">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSizeName" runat="server" Text='<%# Eval("SizeName") %>' ToolTip='<%# Eval("SizeName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Quantity" SortExpression="Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' ToolTip='<%# Eval("Quantity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Price" SortExpression="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%#String.Format("{0:C}", Eval("Price")) %>'
                                                            ToolTip='<%# Eval("Price") %>'></asp:Label>
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
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div>
                    <div>
                    </div>
                    <div>
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
