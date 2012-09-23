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
                <div id="ContentPlaceHolder1_dvCloneOrgReport" class="fl">
                    <span class="btn5">
                        <%--<asp:LinkButton ID="lnkAddNew2" href="#" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow1','dvInnerWindow',0)"
                            runat="server"><span class="AddNewData"></span>Add Product</asp:LinkButton>--%>
                        <a href='AddEditProduct.aspx'>Add Product</a> </span>
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
                                <ctrl:CustomGridView ID="gvGrid" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                    AllowSorting="True" Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle"
                                    SortColumn="UserType" DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                    ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                    CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                    EndDateCol="3" OnPageIndexChanging="gvGrid_PageIndexChanging" OnRowCommand="gvGrid_RowCommand"
                                    OnRowDataBound="gvGrid_RowDataBound" OnSorting="gvGrid_Sorting" OnRowEditing="gvGrid_RowEditing">
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProductID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product" SortExpression="ProductName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' ToolTip='<%# Eval("ProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manufacturer" SortExpression="Manufacturer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManufacturer" runat="server" Text='<%# Eval("Manufacturer") %>'
                                                    ToolTip='<%# Eval("Manufacturer") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Category" SortExpression="CategoryName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' ToolTip='<%# Eval("CategoryName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bar Code" SortExpression="BarCode">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBarCode" runat="server" Text='<%# Eval("BarCode") %>' ToolTip='<%# Eval("BarCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                    OnClientClick="return ClearFormFields();" CausesValidation="False" CommandArgument='<%# Eval("ProductID") %>'> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                                    CommandArgument='<%# Eval("ProductID") %>' CausesValidation="False"> <img src="../Images/ico_delete.png" alt="Delete" /> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" CssClass="al" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" CssClass="alH" />
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
