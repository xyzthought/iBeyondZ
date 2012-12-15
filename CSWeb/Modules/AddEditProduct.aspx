<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditProduct.aspx.cs" Inherits="Modules_AddEditProduct" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product</title>
    <style type="text/css">
    .scroll_checkboxes
    { 
        height: 60px;
        width: 265px;
        padding: 5px;
        overflow: auto;
        border: 1px solid #ccc;
    }
    
     .FormText
    {
       /* FONT-SIZE: 11px;
        FONT-FAMILY: tahoma,sans-serif*/
         border: 1px solid #DADADA;
    height: 26px;
    line-height: 24px;
    padding: 2px;
   /* width: 354px;*/
    }
</style>
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
            var margin = ($('#txtMargin').val()) / 100;
           
            var bp = $('#txtBuyingPrice').val();
            var bpm = bp * (($('#txtMargin').val()) / 100);
            var tax = parseFloat($('#txtTax').val() / 100)+ (1);
            var val = (parseFloat(bp) + parseFloat(bpm))*tax;
            $('#txtSellingPrice').val(val.toFixed(2));
            $('#hdnSellingPrice').val(val.toFixed(2));
            
        }

        function checkAlpaNumeric(obj) {
            if (obj.value.match(/[^a-zA-Z0-9 ]/g)) {
                obj.value = obj.value.replace(/[^a-zA-Z0-9 ]/g, '');
            }
        }
    
        var color = 'White';

        function changeColor(obj) {
            var rowObject = getParentRow(obj);
            var parentTable = document.getElementById("<%=chkSize.ClientID%>");
            if (color == '') {
                color = getRowColor();
            }
            if (obj.checked) {
                rowObject.style.backgroundColor = '#A3B1D8';
            }
            else {
                rowObject.style.backgroundColor = color;
                color = 'White';
            }

            // private method
            function getRowColor() {
                if (rowObject.style.backgroundColor == 'White') return parentTable.style.backgroundColor;
                else return rowObject.style.backgroundColor;
            }

        }

        // This method returns the parent row of the object

        function getParentRow(obj) {
            do {
                obj = obj.parentElement;
            }
            while (obj.tagName != "TR")
            return obj;
        }


        function TurnCheckBoixGridView(id) {
            var frm = document.forms[0];

            for (i = 0; i < frm.elements.length; i++) {
                if (frm.elements[i].type == "checkbox" && frm.elements[i].id.indexOf("<%= chkSize.ClientID %>") == 0) {
                    frm.elements[i].checked = document.getElementById(id).checked;
                }
            }
        }

        function SelectAll(id) {

            var parentTable = document.getElementById("<%=chkSize.ClientID%>");
            var color

            if (document.getElementById(id).checked) {
                color = '#A3B1D8'
            }
            else {
                color = 'White'
            }

            for (i = 0; i < parentTable.rows.length; i++) {
                parentTable.rows[i].style.backgroundColor = color;
            }
            TurnCheckBoixGridView(id);

        }


        function calculateMarginPrice() {

            var OSP = $('#hdnSellingPrice').val();
            var CSP = $('#txtSellingPrice').val();
            var OMargin = $('#txtMargin').val();

            //alert(OSP + '-' + CSP + '-' + OMargin);
            $('#hdnSellingPrice').val($('#txtSellingPrice').val());
            
            var CMargin = (OMargin / OSP) * CSP;

            
            $('#txtMargin').val(CMargin.toFixed(2));
            //alert(CMargin);
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
                                <asp:TextBox ID="txtDescription" runat="server" Rows="3" TextMode="MultiLine" class="txtInv"></asp:TextBox>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <%--<div style="float: left; width: 135px;">
                                Manufacturer<span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbManufacturer" runat="server" CssClass="txtUpl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="ReqtxtMan" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" InitialValue="--Select--" ControlToValidate="cmbManufacturer"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>--%>
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
                            <div style="float: left; width: 135px;">
                                Season
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="ddlSeason" runat="server" CssClass="txtUpl" Style="width: 280px!important">
                                </asp:DropDownList>
                            </div>
                            <div style="float: left; padding-left: 8px;">
                                <span class="btn5">
                                    <asp:LinkButton ID="lnkSeason" runat="server" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow5','dvInnerWindow4',0)"><span class="AddNewData"></span>Add</asp:LinkButton>
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
                            <div class="scroll_checkboxes">
                                <asp:CheckBoxList ID="chkSize" CssClass="FormText" runat="server" RepeatDirection="Vertical" RepeatColumns="1"
                                    CellPadding="15" CellSpacing="5" BorderWidth="0">
                                </asp:CheckBoxList>
                                </div>
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
                                    onblur="extractNumber(this,-1,false);calculateSellingPrice();" Style="width: 160px!important"></asp:TextBox>
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
                            <asp:HiddenField ID="hdnSellingPrice" ClientIDMode="Static" runat="server" />
                                <asp:TextBox ID="txtSellingPrice" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);calculateMarginPrice()"
                                    runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="float: left; width: 135px;">
                                Bar Code <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="txtCred" MaxLength="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqBarcode" runat="server" ErrorMessage="*"
                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtBarcode" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                                        AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                                        SortColumn="SizeID" DataKeyNames="SizeID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                        SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ShowFooter="true" EmptyDataText="No Record Found"
                                                        OnRowCancelingEdit="gvSize_RowCancelingEdit" OnRowCommand="gvSize_RowCommand"
                                                        OnRowDataBound="gvSize_RowDataBound" OnRowDeleting="gvSize_RowDeleting" OnRowEditing="gvSize_RowEditing"
                                                        OnRowUpdating="gvSize_RowUpdating" 
                                                        onpageindexchanging="gvSize_PageIndexChanging">
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
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblSizeBarCode" runat="server" Text="SizeBarCode" /></b>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSizeID1" CssClass="txtMasterData" runat="server" Visible="false"
                                                                            Text="0" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSizeName1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDSi" ID="ReqtxtSize" runat="server"
                                                                            ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeName1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSizeBarCode1" onblur="checkAlpaNumeric(this);" onkeyup="checkAlpaNumeric(this);" MaxLength="3" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDSi" ID="RequiredFieldValidator1" runat="server"
                                                                            ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeBarCode1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewDSi" />
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
                                                                    <asp:TextBox ID="txtSizeIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SizeID") %>'
                                                                        Visible="false" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtSizeID" CssClass="txtMasterData" Text="0" runat="server" Visible="false" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size" SortExpression="SizeName">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("SizeName") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSizeNameE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SizeName") %>' />
                                                                    <asp:RequiredFieldValidator ValidationGroup="NewDSiE" ID="ReqtxtSizeE" runat="server"
                                                                        ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeNameE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtSizeName"  CssClass="txtMasterData" runat="server" />
                                                                    <asp:RequiredFieldValidator ValidationGroup="NewDSi" ID="ReqtxtSize" runat="server"
                                                                        ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size Bar Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSizeBarCode" runat="server" Text='<%# Eval("SizeBarCode") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSizeBarCodeE" MaxLength="3" onblur="checkAlpaNumeric(this);" onkeyup="checkAlpaNumeric(this);" CssClass="txtMasterData" runat="server" Text='<%# Eval("SizeBarCode") %>' />
                                                                    <asp:RequiredFieldValidator ValidationGroup="NewDSiE" ID="ReqtxtSizeBarE" runat="server"
                                                                        ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeBarCodeE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtSizeBarCode" onblur="checkAlpaNumeric(this);" onkeyup="checkAlpaNumeric(this);" MaxLength="3" CssClass="txtMasterData" runat="server" />
                                                                    <asp:RequiredFieldValidator ValidationGroup="NewDSi" ID="ReqtxtSizeBarcode" runat="server"
                                                                        ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeBarCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    
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
                                                                        ToolTip="Save" ValidationGroup="NewDSiE" />
                                                                    <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                        runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                </EditItemTemplate>
                                                                <FooterStyle />
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                        CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewDSi" />
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
                                                        AllowPaging="True" AllowSorting="True" Width="100%" PageSize="50" GridLines="None"
                                                        CssClass="gvStyle" SortColumn="CategoryID" DataKeyNames="CategoryID" SortOrder="Ascending"
                                                        SortAscImageUrl="~/Images/GridViewCtrl/asc.png" SortDescImageUrl="~/Images/GridViewCtrl/dsc.png"
                                                        ExportTemplatePath="~/Reports/Templates/" ExcelHeaderRow="8" StartRow="10" StartColumn="2"
                                                        DBColumn="" MaxLevel="1" SheetNumber="1" CurrentDateRow="6" CurrentDateCol="3"
                                                        StartDateRow="4" StartDateCol="3" EndDateRow="5" EndDateCol="3" ShowFooter="true"
                                                        EmptyDataText="No Record Found" OnRowCancelingEdit="grvCategory_RowCancelingEdit"
                                                        OnRowCommand="grvCategory_RowCommand" OnRowDataBound="grvCategory_RowDataBound"
                                                        OnRowDeleting="grvCategory_RowDeleting" OnRowEditing="grvCategory_RowEditing"
                                                        OnRowUpdating="grvCategory_RowUpdating" 
                                                        onpageindexchanging="grvCategory_PageIndexChanging">
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
                                                                        <asp:TextBox ID="txtCategoryID" CssClass="txtMasterData" runat="server" Visible="false"
                                                                            Text="0" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCategoryName1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDC" ID="ReqtxtCat1" runat="server"
                                                                            ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCategoryName1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewDC" />
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
                                                                    <asp:TextBox ID="txtCategoryIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("CategoryID") %>'
                                                                        Visible="false" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtCategoryID" CssClass="txtMasterData" Text="0" runat="server"
                                                                        Visible="false" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtCategoryNameE" CssClass="txtMasterData" runat="server" Text='<%# Eval("CategoryName") %>' />
                                                                    <asp:RequiredFieldValidator ValidationGroup="NewDCE" ID="ReqtxtCatE" runat="server"
                                                                        ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCategoryNameE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtCategoryName" CssClass="txtMasterData" runat="server" />
                                                                    <asp:RequiredFieldValidator ValidationGroup="NewDC" ID="ReqtxtCat" runat="server"
                                                                        ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCategoryName" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                                                        ToolTip="Save" ValidationGroup="NewDCE" />
                                                                    <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                        runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                </EditItemTemplate>
                                                                <FooterStyle />
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                        CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewDC" />
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
                                                            AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                                            SortColumn="BrandID" DataKeyNames="BrandID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                                            EndDateCol="3" ShowFooter="true" EmptyDataText="No Record Found" OnRowCancelingEdit="grvBrand_RowCancelingEdit"
                                                            OnRowCommand="grvBrand_RowCommand" OnRowDataBound="grvBrand_RowDataBound" OnRowDeleting="grvBrand_RowDeleting"
                                                            OnRowEditing="grvBrand_RowEditing" OnRowUpdating="grvBrand_RowUpdating" 
                                                            onpageindexchanging="grvBrand_PageIndexChanging">
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
                                                                            <asp:TextBox ID="txtBrandID1" CssClass="txtMasterData" runat="server" Visible="false"
                                                                                Text="0" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBrand1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                            <asp:RequiredFieldValidator ValidationGroup="NewD" ID="ReqtxtBrand1" runat="server"
                                                                                ErrorMessage="*" ForeColor="Red" ControlToValidate="txtBrand1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                                CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
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
                                                                        <asp:TextBox ID="txtBrandIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("BrandID") %>'
                                                                            Visible="false" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtBrandID" CssClass="txtMasterData" Text="0" runat="server" Visible="false" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("BrandName") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBrandE" CssClass="txtMasterData" runat="server" Text='<%# Eval("BrandName") %>' />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewD" ID="ReqtxtBrandE" runat="server"
                                                                            ErrorMessage="*" ForeColor="Red" ControlToValidate="txtBrandE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtBrand" CssClass="txtMasterData" runat="server" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewD" ID="ReqtxtBrand" runat="server"
                                                                            ErrorMessage="*" ForeColor="Red" ControlToValidate="txtBrand" Display="Dynamic"></asp:RequiredFieldValidator>
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

                <div id="ModalWindow5" style="display: none" clientidmode="Static">
                    <div class="mainModalAddEdit" id="Div4">
                        <div class="topM">
                            <h1>
                                <span id="Span4">Manage Season</span><a onclick="return CloseAddDiv('ModalWindow5');"
                                    id="A4" title="Close"> </a>
                            </h1>
                        </div>
                        <div id="Div5" class="MidM">
                            <div class="addNew" id="Div7">
                                <div id="Div8">
                                    <div id="dvInnerWindow4" class="modalContent">
                                        <fieldset>
                                            <div class="grid_container">
                                                <ctrl:CustomGridView ID="grvSeason" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                            AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                                            SortColumn="SeasonID" DataKeyNames="SeasonID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                                            EndDateCol="3" ShowFooter="true" EmptyDataText="No Record Found" OnRowCancelingEdit="grvSeason_RowCancelingEdit"
                                                            OnRowCommand="grvSeason_RowCommand" 
                                                            OnRowDataBound="grvSeason_RowDataBound" OnRowDeleting="grvSeason_RowDeleting"
                                                            OnRowEditing="grvSeason_RowEditing" OnRowUpdating="grvSeason_RowUpdating" 
                                                            onpageindexchanging="grvSeason_PageIndexChanging">
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
                                                                            <asp:TextBox ID="txtSeasonID1" CssClass="txtMasterData" runat="server" Visible="false"
                                                                                Text="0" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtSeason1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                            <asp:RequiredFieldValidator ValidationGroup="NewDS" ID="ReqtxtSeason1" runat="server"
                                                                                ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSeason1" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                                CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewDS" />
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
                                                                        <asp:TextBox ID="txtSeasonIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SeasonID") %>'
                                                                            Visible="false" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtSeasonID" CssClass="txtMasterData" Text="0" runat="server" Visible="false" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Season">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeason" runat="server" Text='<%# Eval("SeasonName") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtSeasonE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SeasonName") %>' />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDSE" ID="ReqtxtSeasoE" runat="server"
                                                                            ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSeasonE" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtSeason" CssClass="txtMasterData" runat="server" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDS" ID="ReqtxtSeason" runat="server"
                                                                            ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSeason" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                                                            ToolTip="Save" ValidationGroup="NewDSE" />
                                                                        <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                            runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                    </EditItemTemplate>
                                                                    <FooterStyle />
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewDS" />
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

<script type="text/javascript">

    $(document).ready(function () {

        $('#hdnSellingPrice').val($('#txtSellingPrice').val());
    });
</script>
                                                                                                                                                                                                                                                                                                                                                                             