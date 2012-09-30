<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditSaleOrder.aspx.cs"
    Inherits="Modules_AddEditSaleOrder" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        function CalculatePay() {


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
                                <!--Outer Div-->
                                <div class="accPend">
                                    <!--Start Add/Edit product-->
                                    <div class="smallDivLeft">
                                        <div class="acceptedCont">
                                            <div id="MidM2">
                                                <fieldset class="fieldAddEdit">
                                                    <div class="inner">
                                                        <asp:HiddenField ID="hdnByBarCode" runat="server" ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hdnByProductName" runat="server" ClientIDMode="Static" />
                                                        <div class="mandet">
                                                            <span id="lblMessage">* Fields are mandatory</span></div>
                                                        <div class="errorMsg">
                                                            <span id="lblError" runat="server"></span>
                                                        </div>
                                                        <div style="background: url('../Images/dot.png') repeat-x scroll center bottom #FFFFFF;
                                                            padding-bottom: 6px;">
                                                            <strong>Search By</strong>&nbsp;<input type="radio" id="rdoBarCode" name="rdoSelection"
                                                                value="Bar Code" checked="checked" onchange="ChangeMe(1)" />&nbsp;Bar Code<input
                                                                    type="radio" id="rdoPName" name="rdoSelection" value="Product Name" onchange="ChangeMe(2)" />&nbsp;Product
                                                            Name
                                                        </div>
                                                        <div>
                                                            Product <span id="spType">Bar Code</span> :<span class="mandet2">* </span>
                                                        </div>
                                                        <div class="alt">
                                                            <asp:TextBox ID="txtProductBarCode" runat="server" CssClass="txtCred" ClientIDMode="Static"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                                Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtProductBarCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            <asp:HiddenField ID="Productid" runat="server" ClientIDMode="Static" />
                                                            <p id="Product-description">
                                                            </p>
                                                        </div>
                                                        <div style="float: left">
                                                            <div>
                                                                Quantity :<span class="mandet2">* </span>
                                                            </div>
                                                            <div class="alt" style="margin-bottom: 5px;">
                                                                <asp:TextBox ID="txtQuantity" ClientIDMode="Static" runat="server" MaxLength="4"
                                                                    CssClass="txtCred" Style="width: 100px!important; text-align: right" onkeyup="extractNumber(this,0,false);"
                                                                    onblur="extractNumber(this,0,false);"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="ReqQuantity" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                                    ForeColor="Red" ControlToValidate="txtQuantity" Display="Dynamic"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div style="float: right; margin-top: 25px;">
                                                            <span class="btn5">
                                                                <asp:LinkButton ID="lnkAddMore" runat="server" OnClick="lnkAddMore_Click"><span class="AddNewData"></span>Add</asp:LinkButton>
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
                                                            <div class="accpendNew">
                                                                Payment Details</div>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div>
                                                            <div>
                                                                <div style="width: 100px; float: left; padding-left: 5px; font-weight: bold">
                                                                    Total Amount</div>
                                                                <div style="width: 100px; float: left;">
                                                                    <asp:Label ID="lblTotalAmount" runat="server" CssClass="txtCred" Text="0.00"></asp:Label>&nbsp;€</div>
                                                            </div>
                                                            <div>
                                                                <div style="width: 70px; float: left; padding-left: 5px; font-weight: bold">
                                                                    Discount</div>
                                                                <div style="width: 100px; float: left; display: ">
                                                                    <asp:Label ID="txtDiscount" runat="server" CssClass="txtCred" Text="0.00"></asp:Label>&nbsp;€
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div style="width: 100px; float: left; padding-left: 5px; font-weight: bold">
                                                            Total Pay</div>
                                                        <div style="width: 100px; float: left;">
                                                            <asp:Label ID="lblTotalPay" runat="server" CssClass="txtCred" Text="0.00"></asp:Label>&nbsp;€
                                                        </div>
                                                        <div style="clear: both">
                                                        </div>
                                                        <div style="float: right; margin-bottom: 19px;">
                                                            <span class="btn5">
                                                                <asp:LinkButton ID="lnkFinalChekout" runat="server" CausesValidation="False" OnClick="lnkFinalChekout_Click"><span class="AddCheckoutData"></span>Checkout</asp:LinkButton>
                                                            </span>
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
                                                <asp:TemplateField HeaderText="Discount(%)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPDiscount" Text='<%# Eval("PDiscount") %>' runat="server" CssClass="txtCred"
                                                            MaxLength="2" Style="width: 100px!important; text-align: right" onkeyup="extractNumber(this,-1,false);CalculatePay();"
                                                            onblur="extractNumber(this,-1,false);CalculatePay();"></asp:TextBox>
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
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%# String.Format("{0:0.00}",Eval("Price")) %>'
                                                            ToolTip='<%# String.Format("{0:0.00}",Eval("Price")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                            CausesValidation="False" CommandArgument='<%# Eval("ProductID") %>'> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>
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
<script type="text/javascript">

    function ChangeMe(callfrom) {
        Populate(callfrom);
    }


    $(function () {
        Populate(1);
    });

    function Populate(callfrom) {
        var projects = [];
        var MyKeys = ["value", "label", "desc"];

        if (callfrom == "1")
            dimArrayValue = $("#hdnByBarCode").val();
        else if (callfrom == "2")
            dimArrayValue = $("#hdnByProductName").val();

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
                $("#Product-description").html(ui.item.desc);
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
</script>
