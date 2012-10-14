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
                                    AllowSorting="True" Width="50%" PageSize="20" GridLines="None" CssClass="gvStyle"
                                    SortColumn="ProductName" DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ShowFooter="true" EmptyDataText="No Record Found"
                                    OnRowCancelingEdit="gvGrid_RowCancelingEdit" OnRowCommand="gvGrid_RowCommand"
                                    OnRowDataBound="gvGrid_RowDataBound" OnRowDeleting="gvGrid_RowDeleting" OnRowEditing="gvGrid_RowEditing"
                                    OnRowUpdating="gvGrid_RowUpdating">
                                    <EmptyDataTemplate>
                                        <table cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <th>
                                                    <b>
                                                        <asp:Label ID="lblProductID" runat="server" Text="ProductID" Visible="false" /></b>
                                                </th>
                                                <th>
                                                    <b>
                                                        <asp:Label ID="lblProductName" runat="server" Text="ProductName" /></b>
                                                </th>
                                                <th>
                                                    <b>
                                                        <asp:Label ID="lblDescription" runat="server" Text="Description" /></b>
                                                </th>
                                                <th>
                                                    <b>
                                                        <asp:Label ID="lblMargin" runat="server" Text="Margin" /></b>
                                                </th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtProductID1" CssClass="txtBox" runat="server" Visible="false"
                                                        Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtProductName" CssClass="txtBox" runat="server" Visible="true" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDescription" CssClass="txtBox" runat="server" Visible="true"
                                                        Rows="3" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMargin" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);"
                                                        CssClass="txtBox" runat="server" Visible="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:TemplateField HeaderText="ProductID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProductIDE" CssClass="txtBox" runat="server" Text='<%# Eval("ProductID") %>'
                                                    Visible="false" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtProductID" CssClass="txtBox" Text="0" runat="server" Visible="false" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProductNameE" CssClass="txtBox" runat="server" Text='<%# Eval("ProductName") %>' />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtProductName" CssClass="txtBox" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescriptionE" CssClass="txtBox" runat="server" Text='<%# Eval("Description") %>'
                                                    Rows="3" TextMode="MultiLine" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDescription" CssClass="txtBox" runat="server" Rows="3" TextMode="MultiLine" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Margin">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMargin" runat="server" Text='<%# Eval("Margin") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMarginE" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);"
                                                    CssClass="txtBox" runat="server" Text='<%# Eval("Margin") %>' />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMargin" CssClass="txtBox" runat="server" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <HeaderStyle />
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                    CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                    CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                    ToolTip="Save" ValidationGroup="ProductFamilyEditRow" />
                                                <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                    runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                            </EditItemTemplate>
                                            <FooterStyle />
                                            <FooterTemplate>
                                                <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                    CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="tdData" />
                                    <HeaderStyle CssClass="trHeader" />
                                </ctrl:CustomGridView>
                                <div style="width: 50%; float: right">
                                    <div id="divSize">
                                        <div>
                                            <h1>
                                                <span id="Span1">Manage Size</span>
                                            </h1>
                                        </div>
                                        <div id="updDataSource1">
                                            <fieldset>
                                                <div class="grid_container">
                                                    <ctrl:CustomGridView ID="gvSize" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                        AllowSorting="True" Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle"
                                                        SortColumn="SizeID" DataKeyNames="SizeID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                        SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ShowFooter="true" EmptyDataText="No Record Found"
                                                        OnRowCancelingEdit="gvSize_RowCancelingEdit" OnRowCommand="gvSize_RowCommand"
                                                        OnRowDataBound="gvSize_RowDataBound" OnRowDeleting="gvSize_RowDeleting" OnRowEditing="gvSize_RowEditing"
                                                        OnRowUpdating="gvSize_RowUpdating">
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblSizeID" runat="server" Text="SizeID" Visible="false" /></b>
                                                                    </th>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblSizeName" runat="server" Text="SizeName" /></b>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSizeID1" CssClass="txtBox" runat="server" Visible="false" Text="0" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSizeName" CssClass="txtBox" runat="server" Visible="true" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SizeID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSizeID" runat="server" Text='<%# Eval("SizeID") %>' Visible="false" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSizeIDE" CssClass="txtBox" runat="server" Text='<%# Eval("SizeID") %>'
                                                                        Visible="false" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtSizeID" CssClass="txtBox" Text="0" runat="server" Visible="false" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("SizeName") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSizeNameE" CssClass="txtBox" runat="server" Text='<%# Eval("SizeName") %>' />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtSizeName" CssClass="txtBox" runat="server" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                                        CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                                        CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                                        ToolTip="Save" ValidationGroup="ProductFamilyEditRow" />
                                                                    <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                        runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                </EditItemTemplate>
                                                                <FooterStyle />
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                        CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="tdData" />
                                                        <HeaderStyle CssClass="trHeader" />
                                                    </ctrl:CustomGridView>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div id="divCategory">
                                        <div>
                                            <h1>
                                                <span id="Span2">Manage Category</span>
                                            </h1>
                                        </div>
                                        <div id="updDataSource2">
                                            <fieldset>
                                                <div class="grid_container">
                                                    <ctrl:CustomGridView ID="grvCategory" runat="server" AutoGenerateColumns="false"
                                                        AllowPaging="True" AllowSorting="True" Width="100%" PageSize="20" GridLines="None"
                                                        CssClass="gvStyle" SortColumn="CategoryID" DataKeyNames="CategoryID" SortOrder="Ascending"
                                                        SortAscImageUrl="~/Images/GridViewCtrl/asc.png" SortDescImageUrl="~/Images/GridViewCtrl/dsc.png"
                                                        ExportTemplatePath="~/Reports/Templates/" ExcelHeaderRow="8" StartRow="10" StartColumn="2"
                                                        DBColumn="" MaxLevel="1" SheetNumber="1" CurrentDateRow="6" CurrentDateCol="3"
                                                        StartDateRow="4" StartDateCol="3" EndDateRow="5" EndDateCol="3" ShowFooter="true"
                                                        EmptyDataText="No Record Found" OnRowCancelingEdit="grvCategory_RowCancelingEdit"
                                                        OnRowCommand="grvCategory_RowCommand" OnRowDataBound="grvCategory_RowDataBound"
                                                        OnRowDeleting="grvCategory_RowDeleting" OnRowEditing="grvCategory_RowEditing"
                                                        OnRowUpdating="grvCategory_RowUpdating">
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblCategoryID" runat="server" Text="CategoryID" Visible="false" /></b>
                                                                    </th>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblCategoryName" runat="server" Text="CategoryName" /></b>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCategoryID" CssClass="txtBox" runat="server" Visible="false"
                                                                            Text="0" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCategoryName" CssClass="txtBox" runat="server" Visible="true" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="CategoryID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CategoryID") %>' Visible="false" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtCategoryIDE" CssClass="txtBox" runat="server" Text='<%# Eval("CategoryID") %>'
                                                                        Visible="false" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtCategoryID" CssClass="txtBox" Text="0" runat="server" Visible="false" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtCategoryNameE" CssClass="txtBox" runat="server" Text='<%# Eval("CategoryName") %>' />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtCategoryName" CssClass="txtBox" runat="server" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                                        CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                                        CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                                        ToolTip="Save" ValidationGroup="ProductFamilyEditRow" />
                                                                    <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                        runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                </EditItemTemplate>
                                                                <FooterStyle />
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                        CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="tdData" />
                                                        <HeaderStyle CssClass="trHeader" />
                                                    </ctrl:CustomGridView>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div id="divSeason">
                                        <div>
                                            Manage Season
                                        </div>
                                        <div id="updDataSource3">
                                              <div id="Div1">
                                                <fieldset>
                                                    <div class="grid_container">
                                                        <ctrl:CustomGridView ID="grvSeason" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                            AllowSorting="True" Width="100%" PageSeason="20" GridLines="None" CssClass="gvStyle"
                                                            SortColumn="SeasonID" DataKeyNames="SeasonID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                                            EndDateCol="3" ShowFooter="true" EmptyDataText="No Record Found" OnRowCancelingEdit="grvSeason_RowCancelingEdit"
                                                            OnRowCommand="grvSeason_RowCommand" OnRowDataBound="grvSeason_RowDataBound" OnRowDeleting="grvSeason_RowDeleting"
                                                            OnRowEditing="grvSeason_RowEditing" OnRowUpdating="grvSeason_RowUpdating">
                                                            <EmptyDataTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblSeasonID" runat="server" Text="SeasonID" Visible="false" /></b>
                                                                        </th>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblSeason" runat="server" Text="Season" /></b>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtSeasonID1" CssClass="txtBox" runat="server" Visible="false" Text="0" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtSeason" CssClass="txtBox" runat="server" Visible="true" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SeasonID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeasonID" runat="server" Text='<%# Eval("SeasonID") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtSeasonIDE" CssClass="txtBox" runat="server" Text='<%# Eval("SeasonID") %>'
                                                                            Visible="false" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtSeasonID" CssClass="txtBox" Text="0" runat="server" Visible="false" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Season">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeason" runat="server" Text='<%# Eval("SeasonName") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtSeasonE" CssClass="txtBox" runat="server" Text='<%# Eval("SeasonName") %>' />

                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtSeason" CssClass="txtBox" runat="server" />
                                                                        <asp:RequiredFieldValidator ID="rqdtxtSeason" ControlToValidate="txtSeason" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <HeaderStyle />
                                                                    <ItemStyle />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                                            CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                                        <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                                            CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                                            ToolTip="Save" ValidationGroup="ProductFamilyEditRow" />
                                                                        <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                            runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                    </EditItemTemplate>
                                                                    <FooterStyle />
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="tdData" />
                                                            <HeaderStyle CssClass="trHeader" />
                                                        </ctrl:CustomGridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divBrand">
                                        <div>
                                            Manage Brand
                                        </div>
                                        <div id="Div6">
                                            <div id="dvInnerWindow3">
                                                <fieldset>
                                                    <div class="grid_container">
                                                        <ctrl:CustomGridView ID="grvBrand" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                            AllowSorting="True" Width="100%" PageBrand="20" GridLines="None" CssClass="gvStyle"
                                                            SortColumn="BrandID" DataKeyNames="BrandID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                                            EndDateCol="3" ShowFooter="true" EmptyDataText="No Record Found" OnRowCancelingEdit="grvBrand_RowCancelingEdit"
                                                            OnRowCommand="grvBrand_RowCommand" OnRowDataBound="grvBrand_RowDataBound" OnRowDeleting="grvBrand_RowDeleting"
                                                            OnRowEditing="grvBrand_RowEditing" OnRowUpdating="grvBrand_RowUpdating">
                                                            <EmptyDataTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblBrandID" runat="server" Text="BrandID" Visible="false" /></b>
                                                                        </th>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblBrand" runat="server" Text="Brand" /></b>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBrandID1" CssClass="txtBox" runat="server" Visible="false" Text="0" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBrand" CssClass="txtBox" runat="server" Visible="true" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BrandID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrandID" runat="server" Text='<%# Eval("BrandID") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBrandIDE" CssClass="txtBox" runat="server" Text='<%# Eval("BrandID") %>'
                                                                            Visible="false" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtBrandID" CssClass="txtBox" Text="0" runat="server" Visible="false" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("BrandName") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBrandE" CssClass="txtBox" runat="server" Text='<%# Eval("BrandName") %>' />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtBrand" CssClass="txtBox" runat="server" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <HeaderStyle />
                                                                    <ItemStyle />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                                            CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                                        <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                                            CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                                            ToolTip="Save" ValidationGroup="ProductFamilyEditRow" />
                                                                        <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                            runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                    </EditItemTemplate>
                                                                    <FooterStyle />
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="tdData" />
                                                            <HeaderStyle CssClass="trHeader" />
                                                        </ctrl:CustomGridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    
                                </div>
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
