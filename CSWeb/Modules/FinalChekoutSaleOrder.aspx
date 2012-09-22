﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalChekoutSaleOrder.aspx.cs"
    Inherits="Modules_FinalChekoutSaleOrder" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function CalculatePay() {

            try {
                var tp = $('#<%=lblTotalAmount.ClientID%>').html().replace(' ', '').replace('.', '').replace(',', '').replace(/\u20ac/g, '');
                var dis = $("#txtDiscount").val();
                if (dis != "" && tp != "") {
                    DisVal = tp - dis;

                    $('#<%=lblTotalPay.ClientID%>').html(DisVal).formatCurrency({
                        decimalSymbol: ',',
                        digitGroupSymbol: '.',
                        dropDecimals: false,
                        groupDigits: true,
                        region: 'fr-BE',
                        colorize: true,
                        symbol: '€ '
                    });



                    //$('#<%=lblTotalAmount.ClientID%>').formatCurrency({ colorize: true, region: 'fr-BE' });
                }
            } catch (e) {
                alert(e);
            }
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
                <div class="reports">
                    <asp:Label ID="lblHeader" runat="server"></asp:Label>
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
                    <div class="grid_container">
                        <asp:HiddenField runat="server" ID="hdnCustData" ClientIDMode="Static" />
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
                                        <asp:TemplateField HeaderText="Bar Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBarCode" runat="server" Text='<%# Eval("BarCode") %>' ToolTip='<%# Eval("BarCode") %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSize" runat="server" Text='<%# Eval("SizeName") %>' ToolTip='<%# Eval("SizeName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' ToolTip='<%# Eval("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUnit" runat="server" Text='<%# String.Format("{0:C}",Eval("Unit")) %>'
                                                    ToolTip='<%# String.Format("{0:C}",Eval("Unit")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" Text='<%# String.Format("{0:C}",Eval("Price")) %>'
                                                    ToolTip='<%# String.Format("{0:C}",Eval("Price")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                    CausesValidation="False" CommandArgument='<%# Eval("ProductID") %>'> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>--%>
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
                        <!--End Product Deatil Grid-->
                        <div style="clear: both">
                        </div>
                        <!--Outer Div-->
                        <div class="accPend">
                            <!--Start Add/Edit product-->
                            <div class="smallDivLeft">
                                <div class="acceptedCont">
                                    <div id="MidM2">
                                        <fieldset class="fieldAddEdit">
                                            <div class="inner" style="margin: 0 5px 24px!important;">
                                                <div class="mandet">
                                                    <span id="lblMessage">* Fields are mandatory</span></div>
                                                <div class="errorMsg">
                                                    <span id="lblError" runat="server"></span>
                                                </div>
                                                <div>
                                                    Customer Name :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt">
                                                    <div class="demo">
                                                        <asp:TextBox ID="Customer" runat="server" ClientIDMode="Static" CssClass="txtCred" />
                                                        <asp:ImageButton src="../Images/refresh.png" ID="btnRegresh" runat="server" OnClick="btnRegresh_Click"
                                                            CausesValidation="false" />
                                                        <asp:HiddenField ID="Customerid" runat="server" ClientIDMode="Static" />
                                                        <p id="Customer-description">
                                                        </p>
                                                    </div>
                                                </div>
                                                <div style="float: left">
                                                    <div>
                                                        Address :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqtxtAddress" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                            ForeColor="Red" ControlToValidate="txtAddress" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div style="float: left">
                                                    <div style="margin: 0!important">
                                                        ZIP :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtZIP" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtZIP" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div style="float: left; padding-left: 18px">
                                                    <div style="margin: 0!important">
                                                        City :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCity" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div style="clear: both">
                                                </div>
                                                <div style="float: left">
                                                    <div style="margin: 0!important">
                                                        Country :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtCountry" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCountry" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div style="float: left; padding-left: 18px">
                                                    <div style="margin: 0!important">
                                                        Phone :<span class="mandet2"></span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtPhone" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div style="clear: both">
                                                </div>
                                                <div>
                                                    Email :<span class="mandet2"></span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtCred"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegEmail" runat="server" ErrorMessage="Invalid Email"
                                                        Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID"
                                                        ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"></asp:RegularExpressionValidator>
                                                </div>
                                                <div style="clear: both">
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>

                                <!--Payment Button-->
                                <div class="acceptedCont" style="margin-top:9px!important">
                                    <div id="Div3">
                                        <fieldset class="fieldAddEdit" >
                                            <div class="inner">
                                                
                                                <div>
                                                    <span class="btn5" style="float: right; padding-top: 2px; padding-right: 5px;">
                                                            <asp:LinkButton ID="lnkFinalCheckout" runat="server" CausesValidation="False">Final Checkout</asp:LinkButton>
                                                        </span>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                                <!--Payment Button-->
                            </div>
                            </div>
                            <!--End Add/Edit product-->
                            <!--Start Payment Deatils-->
                            <div class="smallDivRight">
                                <div class="pendingCont">
                                    <div id="Div1">
                                        <fieldset class="fieldAddEdit">
                                            <div class="innerSummary">
                                                <div class="errorMsg">
                                                    <span id="Span2" runat="server"></span>
                                                </div>
                                                <div>
                                                    <div class="accpendNew" style="width: 49%; float: left">
                                                        Payment Details</div>
                                                    <div class="accpendNew" style="width: 46%; float: right">
                                                        <span class="btn5" style="float: right; padding-top: 2px; padding-right: 5px;">
                                                            <asp:LinkButton ID="lnkBack" runat="server" CausesValidation="False" OnClick="lnkBack_Click">Back</asp:LinkButton>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div style="clear: both">
                                                </div>
                                                <div style="width: 100px; float: left; padding-left: 5px; font-weight: bold">
                                                    Total Amount</div>
                                                <div style="width: 100px; float: left;">
                                                    <asp:Label ID="lblTotalAmount" runat="server" CssClass="txtCred"></asp:Label></div>
                                                <div style="clear: both">
                                                </div>
                                                <div style="width: 100px; float: left; padding-left: 5px; font-weight: bold">
                                                    Discount</div>
                                                <div style="width: 100px; float: left;">
                                                    €&nbsp;
                                                    <asp:Label ID="txtDiscount" runat="server" CssClass="txtCred" Style="width: 100px!important"></asp:Label>
                                                </div>
                                                <div style="width: 100px; float: right; padding-left: 5px; margin-top: -18px; font-weight: bold">
                                                    Total Pay</div>
                                                <div style="width: 100px; float: right; margin-bottom: 22px">
                                                    <asp:Label ID="lblTotalPay" runat="server" CssClass="txtCred"></asp:Label></div>
                                                <div style="clear: both">
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--End Payment Deatils-->
                        <!--Card Panel-->
                        <div class="smallDivRight" style="margin-top: 5px!important">
                            <div class="pendingCont">
                                <div id="Div2">
                                    <fieldset class="fieldAddEdit">
                                        <div class="innerSummary">
                                            <div class="errorMsg">
                                                <span id="Span1" runat="server"></span>
                                            </div>
                                            <div>
                                                <div class="accpendNew">
                                                    Payment Method</div>
                                            </div>
                                            <div style="clear: both">
                                            </div>
                                            <div style="float: left">
                                                <div>
                                                    Mode of Payment :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="TextBox1" runat="server" CssClass="txtCred"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtAddress" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div style="float: left">
                                                <div style="margin: 0!important">
                                                    Credit/Debit Card # :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="txtCreditCard" runat="server" MaxLength="16" CssClass="txtCred"
                                                        Style="width: 160px!important"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCountry" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div style="float: left; padding-left: 18px">
                                                <div style="margin: 0!important">
                                                    CVV Number :<span class="mandet2"></span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="txtCVV" runat="server" MaxLength="4" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div style="clear: both">
                                            </div>
                                            <div style="float: left">
                                                <div style="margin: 0!important">
                                                    Expiry on [DD - YYYY] :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="txtMonth" runat="server" MaxLength="2" CssClass="txtCred" Style="width: 60px!important"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtMonth" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RVmonth" runat="server" ControlToValidate="txtMonth" MinimumValue="1"
                                                        MaximumValue="12" ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" Display="Dynamic"></asp:RangeValidator>
                                                    <asp:TextBox ID="txtYear" runat="server" MaxLength="4" CssClass="txtCred" Style="width: 60px!important"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtYear" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtYear"
                                                        MinimumValue="2012" MaximumValue="2016" ErrorMessage="*" Font-Size="X-Small"
                                                        ForeColor="Red" Display="Dynamic"></asp:RangeValidator>
                                                </div>
                                            </div>
                                            <div style="float: left; padding-left: 18px">
                                                <div style="margin: 0!important">
                                                    Amount Paid :<span class="mandet2"></span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="txtAmountPaid" runat="server" CssClass="txtCred" Style="width: 160px!important"
                                                        ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                        <!--End Card Panel-->
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
    #Customer-description
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
<script type="text/javascript">

    $(function () {
        var projects = [];
        var MyKeys = ["value", "label", "desc"];
        dimArrayValue = $("#hdnCustData").val()
        var DataArr = dimArrayValue.split("@@");

        for (i = 0; i < DataArr.length; i++) {
            var DataArr2 = DataArr[i].split("##");
            var obj = {};
            for (j = 0; j < DataArr2.length; j++) {
                obj[MyKeys[j]] = DataArr2[j];
            }
            projects.push(obj);
        }


        $("#Customer").autocomplete({
            minLength: 0,
            source: projects,
            focus: function (event, ui) {
                $("#Customer").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#Customer").val(ui.item.label);
                $("#Customerid").val(ui.item.value);
                $("#Customer-description").html(ui.item.desc);

                return false;
            }
        })
		.data("autocomplete")._renderItem = function (ul, item) {
		    return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<a>" + item.label + "<br>" + item.desc + "</a>")
				.appendTo(ul);
		};
    });
</script>
