<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditProduct.aspx.cs" Inherits="Modules_AddEditProduct" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product</title>
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

        function calculateSellingPrice() {
            if ($('#txtBuyingPrice').val() == '') {
                $('#txtBuyingPrice').val('0');
            }

            if ($('#txtMargin').val() == '') {
                $('#txtMargin').val('0');
            }
            var val = $('#txtBuyingPrice').val() * $('#txtMargin').val()
            $('#txtSellingPrice').val(val);
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
                    Add Product
                </div>
                <div class="clear">
                </div>
            </div>
            <div class="acceptedCont" style="border:0!important">
                <span id="ContentPlaceHolder1_lblMsg"></span>
                <div id="updMain">
                    <fieldset class="fieldAddEdit">
                        <div class="inner" style="width: 500px!important; margin: 10px auto; padding-left: 26px;
                            padding-right: 26px; border: 1px solid #eee">
                            <div class="mandet">
                                <span id="lblMessage">* Fields are mandatory</span></div>
                            <div class="errorMsg">
                                <span id="lblError" runat="server"></span>
                            </div>
                            <div style="float: left; width: 135px;">
                                Product Name <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="txtCred"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtProductName" runat="server" ErrorMessage="*"
                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtProductName" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Description
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtDescription" runat="server" Rows="5" TextMode="MultiLine" class="txtInv"></asp:TextBox>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Manufacturer<span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbManufacturer" runat="server" CssClass="txtUpl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ReqtxtMan" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" InitialValue="--Select--" ControlToValidate="cmbManufacturer"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Brand
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbBrand" runat="server" CssClass="txtUpl" Style="width: 280px!important">
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 8px;">
                                <span class="btn5">
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow4','dvInnerWindow3',0)"><span class="AddNewData"></span>Add</asp:LinkButton>
                                </span>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Category
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbCategory" runat="server" CssClass="txtUpl" Style="width: 280px!important">
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 8px;">
                                <span class="btn5">
                                    <asp:LinkButton ID="lnkCategory" runat="server" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow3','dvInnerWindow2',0)"><span class="AddNewData"></span>Add</asp:LinkButton>
                                </span>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Size
                            </div>
                            <div style="float: left; width: 278px;">
                                <asp:CheckBoxList ID="chkSize" runat="server" RepeatDirection="Horizontal" RepeatColumns="3"
                                    CellPadding="5" CellSpacing="5">
                                </asp:CheckBoxList>
                            </div>
                            <div style="float: left; padding-left: 8px;">
                                <span class="btn5">
                                    <asp:LinkButton ID="lnkAddSize" runat="server" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow2','dvInnerWindow1',0)"><span class="AddNewData"></span>Add</asp:LinkButton>
                                </span>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Buying Price
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtBuyingPrice" runat="server" onkeyup="extractNumber(this,-1,false);"
                                    onblur="extractNumber(this,-1,false);calculateSellingPrice();" CssClass="txtCred"
                                    Style="width: 160px!important"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtBuyingPrice" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="float: left; width: 135px;">
                                Tax
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtTax" runat="server" CssClass="txtCred" onkeyup="extractNumber(this,-1,false);"
                                    onblur="extractNumber(this,-1,false);" Style="width: 160px!important"></asp:TextBox></td>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCountry" Display="Dynamic"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="float: left; width: 135px;">
                                Margin <span class="mandet2"></span>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);calculateSellingPrice();"
                                    ID="txtMargin" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="float: left; width: 135px;">
                                Selling Price <span class="mandet2"></span>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtSellingPrice" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);"
                                    runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="float: left; width: 135px;">
                                Bar Code <span class="mandet2"></span>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="txtCred"></asp:TextBox>
                            </div>
                            <div class="btn-wrapper4">
                                <span class="btn">
                                    <asp:LinkButton ID="lnkBtnSaveDS" runat="server" OnClick="lnkBtnSaveDS_Click">Save</asp:LinkButton></span>
                                <span class="btn">
                                    <asp:LinkButton ID="lnkCancel" runat="server" CausesValidation="false" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                                </span>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div id="ModalWindow2" style="display: none" clientidmode="Static">
                    <div class="mainModalAddEdit" id="mainModalAddDataSource1">
                        <div class="topM">
                            <h1>
                                <span id="Span1">Manage Size</span><a onclick="return CloseAddDiv('ModalWindow2');"
                                    id="A1" title="Close"> </a>
                            </h1>
                        </div>
                        <div id="MidM3" class="MidM">
                            <div class="addNew" id="addNew3">
                                <div id="updDataSource1">
                                    <div id="dvInnerWindow1" class="modalContent">
                                        <fieldset>
                                            <div class="grid_container">
                                                <ctrl:CustomGridView ID="gvSize" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                    AllowSorting="True" Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle"
                                                    SortColumn="SizeID" DataKeyNames="SizeID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                                    ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                                    CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                                    EndDateCol="3" ShowFooter="true" EmptyDataText="No Record Found" OnRowCancelingEdit="gvSize_RowCancelingEdit"
                                                    OnRowCommand="gvSize_RowCommand" OnRowDataBound="gvSize_RowDataBound" OnRowDeleting="gvSize_RowDeleting"
                                                    OnRowEditing="gvSize_RowEditing" OnRowUpdating="gvSize_RowUpdating">
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
                            </div>
                        </div>
                        <div class="bottomM">
                        </div>
                    </div>
                </div>
                <div id="ModalWindow3" style="display: none" clientidmode="Static">
                    <div class="mainModalAddEdit" id="Div2">
                        <div class="topM">
                            <h1>
                                <span id="Span2">Manage Category</span><a onclick="return CloseAddDiv('ModalWindow3');"
                                    id="A2" title="Close"> </a>
                            </h1>
                        </div>
                        <div id="MidM4" class="MidM">
                            <div class="addNew" id="addNew4">
                                <div id="updDataSource2">
                                    <div id="dvInnerWindow2" class="modalContent">
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
                            </div>
                        </div>
                        <div class="bottomM">
                        </div>
                    </div>
                </div>
                <div id="ModalWindow4" style="display: none" clientidmode="Static">
                    <div class="mainModalAddEdit" id="Div3">
                        <div class="topM">
                            <h1>
                                <span id="Span3">Manage Brand</span><a onclick="return CloseAddDiv('ModalWindow4');"
                                    id="A3" title="Close"> </a>
                            </h1>
                        </div>
                        <div id="MidM5" class="MidM">
                            <div class="addNew" id="addNew5">
                                <div id="Div6">
                                    <div id="dvInnerWindow3" class="modalContent">
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
                        <div class="bottomM">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="push">
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
