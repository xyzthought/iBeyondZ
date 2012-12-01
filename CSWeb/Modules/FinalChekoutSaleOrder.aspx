<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FinalChekoutSaleOrder.aspx.cs"
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
                    $('#<%=lblTotalPay.ClientID%>').html(DisVal);
                    /* $('#<%=lblTotalPay.ClientID%>').html(DisVal).formatCurrency({
                    decimalSymbol: ',',
                    digitGroupSymbol: '.',
                    dropDecimals: false,
                    groupDigits: true,
                    region: 'fr-BE',
                    colorize: true,
                    symbol: '€ '
                    });*/



                    //$('#<%=lblTotalAmount.ClientID%>').formatCurrency({ colorize: true, region: 'fr-BE' });
                }
            } catch (e) {
                alert(e);
            }
        }

        function CalculateFinalPay() {
            var typeOfDiscount = $("#ddlFdiscount").val();
            var Discount = $("#txtFDiscount").val();
            var GrossPay = $("#lblTotalPay").html();
            var Calc = 0;
            if (typeOfDiscount == "%") {
                Calc = GrossPay - (GrossPay * Discount / 100);
            }
            else {
                Calc = GrossPay - Discount;
            }
            $("#lblFinalAmount").html(Calc.toFixed(2));
            $("#txtFinalAmount").val(Calc.toFixed(2));
        }

        function ResetCalculation() {
            var typeOfDiscount = $("#ddlFdiscount").val();
            $("#txtFDiscount").val("0.00");
            $("#lblFinalAmount").html($("#lblTotalPay").html());
            $("#txtFinalAmount").val($("#lblTotalPay").html());
            if (typeOfDiscount == "%") {
                $("#txtFDiscount").attr('maxlength', '2');
            }
            else {
                $("#txtFDiscount").attr('maxlength', '10');
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
                                                <asp:TemplateField HeaderText="ProductID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bar Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBarCode" runat="server" Text='<%# Eval("PBarCodeWithSize") %>'
                                                            ToolTip='<%# Eval("PBarCodeWithSize") %>'></asp:Label>
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
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUnit" runat="server" Text='<%# String.Format("{0:0.00}",Eval("Unit")) %>'
                                                            ToolTip='<%# String.Format("{0:0.00}",Eval("Unit")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Discount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPDiscount" Text='<%# Eval("PDiscount") %>' runat="server"></asp:Label>
                                                        <asp:Label ID="lblDiscType" runat="server" Text='<%# Eval("DiscountType") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="VAT(21%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVAT" runat="server" Text='<%# ShowValue(Eval("Unit").ToString(),Eval("Quantity").ToString(),Eval("Tax").ToString())%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%# String.Format("{0:0.00}",Eval("TPrice")) %>'
                                                            ToolTip='<%# String.Format("{0:0.00}",Eval("TPrice")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                                            CommandArgument='<%# Eval("ProductID") %>' CausesValidation="False"> <img src="../Images/ico_delete.png" alt="Delete" style="vertical-align:middle;" /> </asp:LinkButton>
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
                                    <div class="smallDivLeft" style="width: 415px!important">
                                        <div class="acceptedCont">
                                            <div id="MidM2">
                                                <fieldset class="fieldAddEdit">
                                                    <div class="inner" style="margin: 0 5px 0px!important;">
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
                                                                <asp:TextBox ID="Customer" runat="server" ClientIDMode="Static" CssClass="txtCred"
                                                                    Text="SOFISM" />
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
                                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="txtCred" Text="Van Iseghemlaan 40"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="ReqtxtAddress" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                                    ForeColor="Red" ControlToValidate="txtAddress" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div style="float: left">
                                                            <div style="margin: 0!important">
                                                                ZIP :<span class="mandet2">* </span>
                                                            </div>
                                                            <div class="alt" style="margin-bottom: 5px;">
                                                                <asp:TextBox ID="txtZIP" runat="server" CssClass="txtCred" Style="width: 160px!important"
                                                                    Text="8400"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtZIP" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; padding-left: 18px">
                                                            <div style="margin: 0!important">
                                                                City :<span class="mandet2">* </span>
                                                            </div>
                                                            <div class="alt" style="margin-bottom: 5px;">
                                                                <asp:TextBox ID="txtCity" runat="server" CssClass="txtCred" Style="width: 160px!important"
                                                                    Text=" Oostende"></asp:TextBox>
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
                                                                <asp:TextBox ID="txtCountry" runat="server" CssClass="txtCred" Style="width: 160px!important"
                                                                    Text="Belgium"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                    Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCountry" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div style="float: left; padding-left: 18px">
                                                            <div style="margin: 0!important">
                                                                Phone :<span class="mandet2"></span>
                                                            </div>
                                                            <div class="alt" style="margin-bottom: 5px;">
                                                                <asp:TextBox ID="txtPhone" runat="server" CssClass="txtCred" Style="width: 160px!important"
                                                                    Text="+32 59 50 91 06"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div>
                                                            Email :<span class="mandet2"></span>
                                                        </div>
                                                        <div class="alt" style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtCred" Text="vds@sofism.be"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegEmail" runat="server" ErrorMessage="Invalid Email"
                                                                Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID"
                                                                ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div>
                                                            Special Note :
                                                        </div>
                                                        <div>
                                                            <asp:TextBox ID="txtSaleNote" runat="server" CssClass="txtSaleNote" TextMode="MultiLine"
                                                                MaxLength="1024"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!--End Add/Edit product-->
                                <!--Start Payment Deatils-->
                                <div class="smallDivRight" style="width: 486px!important">
                                    <div class="pendingCont">
                                        <div id="Div1">
                                            <fieldset class="fieldAddEdit">
                                                <div class="innerSummary" style="margin: 0px!important">
                                                    <div>
                                                        <div class="accpendNew" style="width: 98%; float: left">
                                                            Payment Details</div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div style="float: left; padding-left: 5px;">
                                                        <div style="width: 100px; padding-left: 5px; font-weight: bold">
                                                            Total Amount</div>
                                                        <div style="width: 100px;">
                                                            <asp:Label ID="lblTotalAmount" runat="server" CssClass="lblAmt"></asp:Label>&nbsp;€</div>
                                                    </div>
                                                    <div style="float: right; padding-left: 5px;">
                                                        <div style="width: 100px; padding-right: 5px; font-weight: bold; text-align: right">
                                                            Product Discount</div>
                                                        <div style="padding-right: 7px; margin: 0!important; float: right">
                                                            <asp:Label ID="txtDiscount" runat="server" CssClass="lblAmt" Style="text-align: right"></asp:Label>&nbsp;€
                                                        </div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div style="float: left; padding-left: 5px;">
                                                        <div style="width: 100px; padding-left: 5px; font-weight: bold">
                                                            Gross Pay</div>
                                                        <div>
                                                            <asp:Label ID="lblTotalPay" runat="server" CssClass="lblAmt"></asp:Label>&nbsp;€</div>
                                                    </div>
                                                    <div style="float: right; padding-left: 5px;">
                                                        <div style="padding-right: 5px; font-weight: bold; text-align: right">
                                                            Final Discount</div>
                                                        <div style="padding-right: 7px; margin: 0!important; float: right">
                                                            <asp:DropDownList ID="ddlFdiscount" runat="server" onChange="ResetCalculation()">
                                                                <asp:ListItem Value="%" Text="%"></asp:ListItem>
                                                                <asp:ListItem Value="€" Text="€"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtFDiscount" runat="server" CssClass="txtCred" MaxLength="2" Style="width: 100px!important;
                                                                text-align: right" Text="0.00"  onkeyup="extractNumber(this,-1,false);CalculateFinalPay()"  onblur="extractNumber(this,-1,false);CalculateFinalPay()"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div style="float: left; padding-left: 5px;">
                                                        <div style="width: 100px; padding-left: 5px; font-weight: bold">
                                                            Final Amount</div>
                                                        <div>
                                                            <asp:Label ID="lblFinalAmount" runat="server" CssClass="lblAmt"></asp:Label>&nbsp;€</div>
                                                    </div>
                                                    <div style="float: right; padding-left: 5px;">
                                                        <div style="padding-right: 5px; font-weight: bold">
                                                            Final Payable Amount</div>
                                                        <div style="padding-right: 7px; margin: 0!important; float: right">
                                                            <asp:TextBox ID="txtFinalAmount" runat="server" CssClass="txtCred" Style="width: 100px!important;
                                                                text-align: right" onkeyup="extractNumber(this,-1,false);"  onblur="extractNumber(this,-1,false);"></asp:TextBox></div>
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
                            <!--Card Panel-->
                            <div class="smallDivRight" style="margin-top: 6px!important; width: 486px!important">
                                <div class="pendingCont">
                                    <div id="Div2">
                                        <fieldset class="fieldAddEdit">
                                            <div class="innerSummary" style="margin: -4px 5px 5px 10px!important">
                                                <!--Credit Card-->
                                                <div style="float: left; margin-right: 10px">
                                                    <div>
                                                        <div class="accpendNew">
                                                            Credit Card</div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div>
                                                        <div style="margin: 0!important">
                                                            Amount Paid :<span class="mandet2"></span>
                                                        </div>
                                                        <div class="alt" style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtAmountPaid" runat="server" MaxLength="10" CssClass="txtCred"
                                                                Style="width: 140px!important; text-align: right;" onkeyup="extractNumber(this,-1,false);CalculatePay();"
                                                                onblur="extractNumber(this,-1,false);CalculatePay();" onfocus="PopulateAmount(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--Credit Card-->
                                                <!--Bank Contact-->
                                                <div style="float: left; margin-right: 10px">
                                                    <div>
                                                        <div class="accpendNew">
                                                            Bank Contact</div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div>
                                                        <div style="margin: 0!important">
                                                            Amount Paid :<span class="mandet2"></span>
                                                        </div>
                                                        <div class="alt" style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtBCash" runat="server" MaxLength="10" CssClass="txtCred" Style="width: 140px!important;
                                                                text-align: right;" onkeyup="extractNumber(this,-1,false);CalculatePay();" onblur="extractNumber(this,-1,false);CalculatePay();"
                                                                onfocus="PopulateAmount(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--Bank Contact-->
                                                <!--Cash-->
                                                <div style="float: left;">
                                                    <div>
                                                        <div class="accpendNew">
                                                            Cash</div>
                                                    </div>
                                                    <div style="clear: both">
                                                    </div>
                                                    <div>
                                                        <div style="margin-bottom: 5px!important;">
                                                            Amount Paid :<span class="mandet2"></span>
                                                        </div>
                                                        <div class="alt" style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtCash" runat="server" MaxLength="10" CssClass="txtCred" Style="width: 140px!important;
                                                                text-align: right;" onkeyup="extractNumber(this,-1,false);CalculatePay();" onblur="extractNumber(this,-1,false);CalculatePay();"
                                                                onfocus="PopulateAmount(this);"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <!--Cash-->
                                                <div style="clear: both">
                                                </div>
                                                <div class="errorMsg">
                                                    <span id="spErrorPay" runat="server">&nbsp;</span>
                                                </div>
                                                <div id="dvAddUser" class="fl" style="float: right">
                                                    <span class="btn5" style="margin-right: -5px!important; margin-top: 5px!important;">
                                                        <asp:LinkButton ID="lnkFinalCheckout" runat="server" OnClick="lnkFinalCheckout_Click"><span class="AddCheckoutData"></span>Final Checkout</asp:LinkButton></span>
                                                </div>
                                                <div class="fl" style="float: right">
                                                    <span class="btn5" style="margin-right: -5px!important; margin-top: 5px!important;">
                                                        <asp:LinkButton ID="lnkBack" runat="server" CausesValidation="False" OnClick="lnkBack_Click">Back</asp:LinkButton>
                                                    </span>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                            <!--End Card Panel-->
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div style="clear: both">
    </div>
    <div class="push">
    </div>
    <!--Body End-->
    <!--Footer Start-->
    <div>
        <uc2:Footer ID="Footer1" runat="server" />
    </div>
    <!--Footer End-->
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

    $("input[type=text]").focus(function () {
        // Select field contents
        this.select();
    });

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

    function PopulateAmount(evThis) {

        $("#txtBCash").val("");
        $("#txtCash").val("");
        $("#txtAmountPaid").val("");

        //$("#" + evThis.id).val($("#lblTotalPay").html());

        $("#" + evThis.id).val($("#txtFinalAmount").val());
    }
</script>
