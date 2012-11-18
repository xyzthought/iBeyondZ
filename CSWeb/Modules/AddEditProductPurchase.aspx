<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditProductPurchase.aspx.cs"
    Inherits="Modules_AddEditProductPurchase" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Product Purchase</title>
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
                <div class="reports">
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
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
                                <!--Outer Div-->
                                <div class="accPend">
                                    <div style="float: left; width: 135px;">
                                        Manufacturer<span class="mandet2">* </span>
                                    </div>
                                    <div style="float: left;">
                                        <asp:DropDownList ID="cmbManufacturer" runat="server" CssClass="txtUpl" Style="width: 315px !important">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ReqtxtMan" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                            ForeColor="Red" InitialValue="--Select--" ControlToValidate="cmbManufacturer"
                                            Display="Dynamic" ValidationGroup="mainFrm"></asp:RequiredFieldValidator>
                                    </div>
                                    <div style="float: left; width: 135px; padding-left: 10px">
                                        Purchase Date <span class="mandet2">* </span>
                                    </div>
                                    <div style="float: left; padding-bottom: 10px;">
                                        <input type="text" id="txtDateOfPurchase" name="txtDateOfPurchase" runat="server"
                                            class="txtCred" readonly="readonly" style="width: 160px!important" />
                                        <asp:RequiredFieldValidator ID="reqtxtDateOfPurchase" runat="server" ErrorMessage="*"
                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtDateOfPurchase" Display="Dynamic"
                                            ValidationGroup="mainFrm"></asp:RequiredFieldValidator>
                                        <asp:HiddenField ID="txtPurchaseID" runat="server" Visible="false" />
                                    </div>
                                    <div style="clear: both">
                                    </div>
                                    <!--Start Add/Edit product-->
                                    <div class="smallDivLeft">
                                        <div class="acceptedCont">
                                            <div id="MidM2">
                                                <fieldset class="fieldAddEdit">
                                                    <div class="inner" style="height: 130px;">
                                                        <asp:HiddenField ID="hdnByBarCode" runat="server" ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hdnByProductName" runat="server" ClientIDMode="Static" />
                                                        <div class="errorMsg">
                                                            <span id="lblError" runat="server"></span>
                                                        </div>
                                                        <div class="mandet">
                                                            <span id="lblMessage">* Fields are mandatory</span>
                                                        </div>
                                                        <div style="background: url('../Images/dot.png') repeat-x scroll center bottom #FFFFFF;
                                                            padding-bottom: 6px;">
                                                            <strong>Search By</strong>&nbsp;<input type="radio" id="rdoPName" name="rdoSelection"
                                                                checked="checked" value="Product Name" onchange="ChangeMe(2)" />&nbsp;Product
                                                            Name<input type="radio" id="rdoBarCode" name="rdoSelection" value="Bar Code" onchange="ChangeMe(1)" />&nbsp;Bar
                                                            Code
                                                        </div>
                                                        <div>
                                                            Product <span id="spType">Bar Code</span> :<span class="mandet2">* </span>
                                                        </div>
                                                        <div class="alt" style="float: left!important; width: 280px;">
                                                            <asp:TextBox ID="txtProductBarCode" runat="server" CssClass="txtCred" ClientIDMode="Static"
                                                                Style="width: 275px!important;"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="required"
                                                                Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtProductBarCode" Display="Dynamic"
                                                                ValidationGroup="barCode"></asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="Productid" runat="server" ClientIDMode="Static" />
                                                            <p id="Product-description" style="margin-top: 5px;">
                                                            </p>
                                                        </div>
                                                        <div style="float: right; margin-top: 3px;">
                                                            <span class="btn5">
                                                                <asp:LinkButton ID="lnkAddMore" runat="server" OnClick="lnkAddMore_Click" CausesValidation="true"
                                                                    ValidationGroup="barCode"><span class="AddNewData"></span>Add</asp:LinkButton>
                                                            </span>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    <!--End Add/Edit product-->
                                    <!--Start Purchase Deatils-->
                                    <div class="smallDivRight">
                                        <div class="pendingCont">
                                            <div id="Div1">
                                                <fieldset class="fieldAddEdit">
                                                    <div class="innerSummary">
                                                        <div class="errorMsg">
                                                            <span id="Span2" runat="server"></span>
                                                        </div>
                                                        <div>
                                                            <div class="accpendNew">
                                                                Product Purchase Details</div>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div>
                                                            <div style="width: 100px; float: left; padding-left: 5px; font-weight: bold">
                                                                Total Buying Price</div>
                                                            <div style="width: 100px; float: left;">
                                                                <asp:Label ID="lblTotalAmount" runat="server" CssClass="lblAmt" Text="0.00"></asp:Label>&nbsp;€</div>
                                                            <div style="float: right; margin-bottom: 19px;">
                                                                <span class="btn5">
                                                                    <asp:LinkButton ID="lnkSave" runat="server" ValidationGroup="mainFrm" OnClick="lnkSave_Click"><span class="AddCheckoutData"></span>Save</asp:LinkButton>
                                                                </span>
                                                            </div>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div style="width: 100px; float: left; padding-left: 5px; font-weight: bold">
                                                            Total Quantity</div>
                                                        <div style="width: 100px; float: left;">
                                                            <asp:Label ID="lblTotalQuantity" runat="server" CssClass="lblAmt" Text="0"></asp:Label>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--End Payment Deatils-->
                                <div style="clear: both">
                                </div>
                                <!-- Start Product Deatil Grid-->
                                <div class="saleDetail">
                                    <div style="margin: 5px; font-weight: bold; background-color: #AAAAAA; height: 20px;
                                        padding-left: 5px; padding-top: 5px;">
                                        Product List</div>
                                    <div style="margin: 5px;">
                                        <ctrl:CustomGridView ID="gvGrid" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                            AllowSorting="True" Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle"
                                            SortColumn="UserType" DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                            EndDateCol="3" OnRowDataBound="gvGrid_RowDataBound" OnRowCommand="gvGrid_RowCommand"
                                            OnRowEditing="gvGrid_RowEditing" OnRowDeleting="gvGrid_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ProductID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' ToolTip='<%# Eval("ProductName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bar Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBarCode" runat="server" Text='<%# Eval("BarCode") %>' ToolTip='<%# Eval("BarCode") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size-Quantity">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSizeQty" runat="server" Text='<%# Eval("SizeQty") %>' ToolTip='<%# Eval("SizeQty") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Buying Price €">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBuyingPrice" runat="server" Text='<%# String.Format("{0:0.00}", Eval("BuyingPrice")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tax (%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTax" runat="server" Text='<%# Eval("Tax") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Margin (%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMargin" runat="server" Text='<%# Eval("Margin") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Selling Price €">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%# String.Format("{0:0.00}",Eval("SellingPrice")) %>'
                                                            ToolTip='<%# String.Format("{0:0.00}",Eval("SellingPrice")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                            Visible="false" CausesValidation="False" CommandArgument='<%# Eval("ProductID") %>'> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                                            CommandArgument='<%# Eval("ProductID") %>' CausesValidation="False"> <img src="../Images/ico_delete.png" alt="Delete" style="vertical-align:middle;"/> </asp:LinkButton>
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
                                <!--End Product Deatil Grid-->
                            </div>
                        </div>
                    </div>
                    <!--Add/Edit Product Purchase -->
                    <div id="ModalWindow1" style="display: none" clientidmode="Static">
                        <div class="mainModalAddEdit" id="mainModalAddDataSource">
                            <div class="topM">
                                <h1>
                                    <span id="spTitle">Add/Edit Purchase</span><a onclick="return CloseAddDiv('ModalWindow1');"
                                        id="lnkCloseAddDiv" title="Close"> </a>
                                </h1>
                            </div>
                            <div id="Div2" class="MidM">
                                <div class="addNew" id="addNew2">
                                    <div id="updDataSource">
                                        <div id="dvInnerWindow" class="modalContent">
                                            <fieldset class="fieldAddEdit">
                                                <div class="inner">
                                                    <div class="mandet">
                                                        <span id="Span1">* Fields are mandatory</span></div>
                                                    <div class="errorMsg">
                                                        <span id="Span3" runat="server"></span>
                                                    </div>
                                                    <div>
                                                        Product Name:<span class="mandet2"></span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:Label runat="server" ID="lblProduct" CssClass="txtCred"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="htnProductID" ClientIDMode="Static" />
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div>
                                                        <div style="float: left">
                                                            <div>
                                                                Bar Code:<span class="mandet2"></span>
                                                            </div>
                                                            <div class="alt" style="margin-bottom: 5px;">
                                                                <asp:Label runat="server" ID="lblBarCode" CssClass="txtCred"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div style="float: right">
                                                            <div>
                                                                Buying Price <span class="mandet2">*</span>
                                                            </div>
                                                            <div class="alt" style="margin-bottom: 5px;">
                                                                <asp:TextBox ID="txtBuyingPrice" runat="server" onkeyup="extractNumber(this,-1,false);"
                                                                    onblur="extractNumber(this,-1,false);calculateSellingPrice();" CssClass="txtCred"
                                                                    Style="width: 100px!important; text-align: right"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                    ValidationGroup="frm" Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtBuyingPrice"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                <input type="button" value="Update Price" style="height: 30px" onclick="UpdateProduct()" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div>
                                                        <div style="float: left">
                                                            <div>
                                                                Tax (%) <span class="mandet2">*</span>
                                                            </div>
                                                            <div>
                                                                <asp:TextBox ID="txtTax" runat="server" CssClass="txtCred" onkeyup="extractNumber(this,-1,false);"
                                                                    Text="21" onblur="extractNumber(this,-1,false);calculateSellingPrice()" Style="width: 100px!important;
                                                                    text-align: right" MaxLength="2"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqtxtTax" runat="server" ErrorMessage="*" ValidationGroup="frm"
                                                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtTax" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div style="float: right">
                                                            <div>
                                                                Margin (%)<span class="mandet2">*</span>
                                                            </div>
                                                            <div>
                                                                <asp:TextBox onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);calculateSellingPrice();"
                                                                    ID="txtMargin" runat="server" CssClass="txtCred" Style="width: 100px!important;
                                                                    text-align: right"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqtxtMargin" runat="server" ErrorMessage="*" ValidationGroup="frm"
                                                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtMargin" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div style="float: left;padding-left:30px;">
                                                            <div>
                                                                Selling Price<span class="mandet2">*</span>
                                                            </div>
                                                            <div>
                                                                <asp:TextBox onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);calculateSellingPrice();"
                                                                    ID="txtSellingPrice" runat="server" CssClass="txtCred" Style="width: 100px!important;text-align:right"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="reqtxtSellingPrice" runat="server" ErrorMessage="*"
                                                                    ValidationGroup="frm" Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtSellingPrice"
                                                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <asp:PlaceHolder runat="server" ID="plhQty"></asp:PlaceHolder>
                                                    <div style="clear: both">
                                                    <div class="btn-wrapper4">
                                                        <span class="btn">
                                                            <asp:LinkButton ID="lnkBtnSaveDS" runat="server" OnClick="lnkBtnSaveDS_Click" ValidationGroup="frm">Save</asp:LinkButton></span>
                                                        <span class="btn">
                                                            <%--<asp:LinkButton ID="lnkCancel" runat="server" OnClientClick="return CloseAddDiv('ModalWindow1'); Populate(2);">Cancel</asp:LinkButton>--%>
                                                            <a href="javascript:void(0)" onclick="CloseAddDiv('ModalWindow1')">Cancel</a>
                                                            <%--<input type="button" value="Cancel" onclick="CloseAddDiv('ModalWindow1')" />--%>
                                                        </span>
                                                    </div>
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
                </ContentTemplate>
            </asp:UpdatePanel>
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
<style>
    #project-label
    {
        display: block;
        font-weight: bold;
        margin-bottom: 1em;
    }
    #project-icon
    {
        float: left;
        height: 32px;
        width: 32px;
    }
    #Product-description
    {
        margin: 0;
        padding: 0;
    }
    
    .ui-autocomplete
    {
        max-height: 200px;
        overflow-y: auto; /* prevent horizontal scrollbar */
        overflow-x: hidden; /* add padding to account for vertical scrollbar */
        padding-right: 20px;
    }
    /* IE 6 doesn't support max-height
	 * we use height instead, but this forces the menu to always be this tall
	 */
    * html .ui-autocomplete
    {
        height: 200px;
    }
</style>
<!--Date Picker-->
<script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<link href="../Styles/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
<link href="../Styles/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    function ChangeMe(callfrom) {

        Populate(callfrom);
    }

    //    $(function () {
    //        $("#txtDateOfPurchase").datepicker({
    //            showOn: "button",
    //            buttonImage: "../images/calendar.gif",
    //            buttonImageOnly: true,
    //            changeMonth: true,
    //            changeYear: true,
    //            maxDate: "+0d"
    //        });
    //    });

    function PopulateDTPicker() {
        $("#txtDateOfPurchase").datepicker({
            showOn: "button",
            buttonImage: "../images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0d"
        });
    }

    $(function () {
        Populate(2);
    });

    function Populate(callfrom) {
        PopulateDTPicker();
        var projects = [];
        var MyKeys = ["value", "label", "desc"];

        if (callfrom == "1") {
            $("#spType").html("Bar Code");
            dimArrayValue = $("#hdnByBarCode").val();
        }
        else if (callfrom == "2") {
            $("#spType").html("Name");
            dimArrayValue = $("#hdnByProductName").val();
        }

        var DataArr = dimArrayValue.split("@@");

        for (i = 0; i < DataArr.length; i++) {
            var DataArr2 = DataArr[i].split("##");
            var obj = {};
            for (j = 0; j < DataArr2.length; j++) {
                obj[MyKeys[j]] = DataArr2[j];
            }
            projects.push(obj);
        }


        $("#txtProductBarCode").autocomplete({
            minLength: 0,
            source: projects,
            focus: function (event, ui) {
                $("#txtProductBarCode").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtProductBarCode").val(ui.item.label);
                $("#Productid").val(ui.item.value);
                var desc = "<img src='../images/pdot.png' style='vertical-align:bottom' />&nbsp;" + ui.item.desc
                $("#Product-description").html(desc);
                $("#txtQuantity").val("1");
                return false;
            }
        })
		.data("autocomplete")._renderItem = function (ul, item) {
		    return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<a>" + item.label + "<br>" + item.desc + "</a>")
				.appendTo(ul);
		};
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
        var tax = parseFloat($('#txtTax').val() / 100) + (1);
        var val = (parseFloat(bp) + parseFloat(bpm)) * tax;
        $('#txtSellingPrice').val(val.toFixed(2));
    }

    function UpdateProduct() {
        var productID = $('#htnProductID').val();
        var buyingPrice = $('#txtBuyingPrice').val();
        var tax = $('#txtTax').val();
        var margin = $('#txtMargin').val();
        var sellingPrice = $('#txtSellingPrice').val();

        $.ajax({
            url: "../Handler/ProductHandler.ashx",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: { 'productID': productID, 'buyingPrice': buyingPrice, 'tax': tax, 'margin': margin, 'sellingPrice': sellingPrice },
            responseType: "json",
            success: OnComplete,
            error: OnFail
        });
        return false;
    }
    function OnComplete(result) {
        if (result.ReturnStatus > 0) {
            $('#Span3').html('Price updated successfully');
        }
        else {
            $('#Span3').html('Error in updating data');
        }
        //alert([result.ReturnStatus, result.ReturnMessage]);
    }
    function OnFail(result) {
        //alert(result);
    }
</script>
