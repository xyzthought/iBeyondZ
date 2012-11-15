<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Modules_Product" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Products</title>
    <script type="text/javascript">
        function ClearFormFields() {
         
            $('#txtProductName').val('');
            $('#txtDescription').val('');
            $('#txtBuyingPrice').val('');
            $('#txtTax').val('');
            $('#txtMargin').val('');
            $('#txtCity').val('');
            $('#txtSellingPrice').val('');
            $('#txtBarCode').val('');

            $('#lblError').empty();
        }
    </script>
    <style type="text/css">
    .gvStyle tr td, .gvStyle tr th{padding:5px;vertical-align:top;}
    </style>
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
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" 
                        ValidationGroup="abc" onclick="lnkBtnSearch_Click"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dvAddUser" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkAddNew" runat="server" 
                        onclick="lnkAddNew_Click"><span class="AddNewData"></span>Add Product</asp:LinkButton></span>
                </div>
                <div class="reports">
                    Manage Products
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
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
                                <div class="inner" style="width: 100%!important; margin: 10px auto; border: 1px solid #eee">
                                    <ctrl:CustomGridView ID="gvGrid" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                        AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                        SortColumn="ProductName" DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                        SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ShowFooter="false" EmptyDataText="No Record Found"
                                        OnRowCancelingEdit="gvGrid_RowCancelingEdit" OnRowCommand="gvGrid_RowCommand"
                                        OnRowDataBound="gvGrid_RowDataBound" OnRowDeleting="gvGrid_RowDeleting" OnRowEditing="gvGrid_RowEditing"
                                        OnRowUpdating="gvGrid_RowUpdating" 
                                        onpageindexchanging="gvGrid_PageIndexChanging" onsorting="gvGrid_Sorting">
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="ProductID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false" />
                                                </ItemTemplate>
                                                
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product" SortExpression="ProductName">
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           <%--<asp:TemplateField HeaderText="Manufacturer" SortExpression="Manufacturer">
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblManufacturer" runat="server" Text='<%# Eval("Manufacturer") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Brand" SortExpression="Brand">
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("Brand") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Season" SortExpression="Season">
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSeason" runat="server" Text='<%# Eval("Season") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BarCode">
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBarCode" runat="server" Text='<%# Eval("BarCode") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantities") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Stock">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStock" runat="server" Text='<%# Eval("Stock") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BuyingPrice">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBuyingPrice" runat="server" Text='<%# String.Format("{0:0.00}",Eval("BuyingPrice")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Margin">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMargin" runat="server" Text='<%# String.Format("{0:0.00}",Eval("Margin")) %>' />
                                                </ItemTemplate>
                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SellingPrice">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSellingPrice" runat="server" Text='<%# String.Format("{0:0.00}",Eval("SellingPrice")) %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle />
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                        CommandName="Edit" CommandArgument='<%# Eval("ProductID") %>' ToolTip="Edit" ValidationGroup="Edit" />
                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                        CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                </ItemTemplate>
                                              
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tdData" />
                                        <HeaderStyle CssClass="trHeader" />
                                    </ctrl:CustomGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br style="clear: both" />
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
