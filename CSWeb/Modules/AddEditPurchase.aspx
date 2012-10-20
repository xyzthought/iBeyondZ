<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditPurchase.aspx.cs" Inherits="Modules_AddEditPurchase" %>
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

            if ($('#txtTax').val() == '') {
                $('#txtTax').val('0');
            }

            var TaxonPurchase = parseFloat($('#txtBuyingPrice').val()) * parseFloat($('#txtTax').val()) / 100;
            var val = parseFloat($('#txtBuyingPrice').val()) + TaxonPurchase + parseFloat($('#txtMargin').val());
            $('#txtSellingPrice').val(val);
        }
        /*
        function ValidateZones(source, args) {
		    var chlZones = document.getElementByc('');
		    var chkLista = chlZones.getElementsByTagName("input");
		    for (var i = 0; i < chkLista.length; i++) {
			    if (chkLista[i].checked) {
			        args.IsValid = true;
			        alert("True");
				    return;
			    }
            }
            alert("False");
		    args.IsValid = false;
	}*/
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
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
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
                                Purchase Date <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <input type="text" ID="txtDateOfPurchase" name="txtDateOfPurchase" runat="server" class="txtCred" style="width:160px!important" />
                                <asp:RequiredFieldValidator ID="reqtxtDateOfPurchase" runat="server" ValidationGroup="frm" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" ControlToValidate="txtDateOfPurchase" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="txtProductPurchaseID" runat="server" Visible="false" />
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Product <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="txtUpl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqcmbProduct" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" InitialValue="-1" ControlToValidate="cmbProduct"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Bar Code <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtBarcode" runat="server" CssClass="txtCred"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtBarcode" runat="server" ErrorMessage="*"
                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtBarcode" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both">
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
                                Brand <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbBrand" runat="server" CssClass="txtUpl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqcmbBrand" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" InitialValue="--Select--" ControlToValidate="cmbBrand"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Category <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbCategory" runat="server" CssClass="txtUpl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqcmbCategory" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" InitialValue="--Select--" ControlToValidate="cmbCategory"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Season <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:DropDownList ID="cmbSeason" runat="server" CssClass="txtUpl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqcmbSeason" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" InitialValue="-1" ControlToValidate="cmbSeason" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Size <span class="mandet2">* </span>
                            </div>
                            <div style="float: left; width: 278px;">
                                <asp:DropDownList ID="cmbSizes" runat="server" CssClass="txtUpl">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="reqcmbSizes" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                    ForeColor="Red" InitialValue="-1" ControlToValidate="cmbSizes" Display="Dynamic"></asp:RequiredFieldValidator>
                                <%--<asp:RadioButtonList ID="chkSize" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" >
                                </asp:RadioButtonList--%>

                            </div>
                            <div style="clear: both;">
                            </div>
                            <div style="float: left; width: 135px;">
                                Buying Price <span class="mandet2">* </span>
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtBuyingPrice"  runat="server" onkeyup="extractNumber(this,-1,false);"
                                    onblur="extractNumber(this,-1,false);calculateSellingPrice();" CssClass="txtCred"
                                    Style="width: 160px!important"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtBuyingPrice" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="float: left; width: 135px;">
                                Tax (%)
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtTax" runat="server" CssClass="txtCred" onkeyup="extractNumber(this,-1,false);" Text="21"
                                    onblur="extractNumber(this,-1,false);calculateSellingPrice()" Style="width: 160px!important;" MaxLength="2"></asp:TextBox>
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
                            <div style="display:none" > <!-- float: left; width: 135px;"> -->
                                Selling Price <span class="mandet2"></span>
                            </div>
                            <div style="display:none" > <!--"float: left;">-->
                                <asp:TextBox ID="txtSellingPrice" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);"
                                    runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                            </div>
                            <div style="clear: both">
                            </div>
                            <div style="float: left; width: 135px;">
                                Quantity
                            </div>
                            <div style="float: left;">
                                <asp:TextBox ID="txtQuantity" runat="server" Text="1" CssClass="txtCred" onkeyup="extractNumber(this,0,false);" onblur="extractNumber(this,0,false);" Style="width: 160px!important;"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqtxtQuantity" runat="server" ValidationGroup="frm" ErrorMessage="*" Font-Size="X-Small"
                                ForeColor="Red" ControlToValidate="txtQuantity" Display="Dynamic"></asp:RequiredFieldValidator>
                            </div>
                            <div style="clear: both">
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
<!--Date Picker-->
<script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<link href="../Styles/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
<link href="../Styles/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
  
    $(function () {
        $("#txtDateOfPurchase").datepicker({
            showOn: "button",
            buttonImage: "../images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0d"
        });
    });
    function ChangeDatePicker() {
        $("#txtDateOfPurchase").datepicker({
            showOn: "button",
            buttonImage: "../images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0d"
        });
    }

    

</script>
<!--Date Picker-->
