<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuyingInterface.aspx.cs"
    Inherits="BuyingInterface" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Product Purchase</title>
    <script type="text/javascript">
        function ClearFormFields() {
            $('#ddlManufacturer option:eq(0)').attr('selected', 'selected')
            $('#ddlProduct option:eq(0)').attr('selected', 'selected')
            $('#ddlSize option:eq(0)').attr('selected', 'selected')

            $('#txtQuantity').val('');
            $('#txtPrice').val('');
            $('#txtDateOfPurchase').val('');

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
                <div class="searchBox">
                    <asp:TextBox ID="txtSearch" value="Search" runat="server" class="searchBoxTxt" onkeypress="return SetDefaultButton(event,1);"
                        onfocus="if (this.value==&#39;Search&#39;) this.value=&#39;&#39;" onblur="if (this.value==&#39;&#39;) this.value=&#39;Search&#39;" />
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" OnClick="lnkBtnSearch_Click"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dv3" class="fl">
                    <asp:HiddenField ID="hdnManufacturer" runat="server" ClientIDMode="Static" />
                    <asp:HiddenField ID="txtSearchManufacturerId" runat="server" ClientIDMode="Static" />
                    <span class="btn5">
                        <asp:LinkButton ID="lnkRefresh" runat="server" OnClick="lnkRefresh_Click">Refresh</asp:LinkButton>
                    </span>
                </div>
                <div id="Div1" class="fl">
                    <div class="demo" style="font-size: 9px!important;">
                        <p>
                            <input type="text" id="toDate" runat="server" class="txtCred" style="width: 80px!important" /></p>
                    </div>
                </div>
                <div id="dv2" class="fl">
                    <div class="demo" style="font-size: 9px!important;">
                        <p>
                            <input type="text" id="fromDate" runat="server" class="txtCred" style="width: 80px!important" /></p>
                    </div>
                </div>
                <div id="Div2" class="fl">
                    <div class="demo" style="font-size: 9px!important;">
                        <p>
                            <input type="text" id="txtSearchManufacturer" runat="server" class="txtCred" value="Manufacturer Name"
                                style="width: 160px!important" /></p>
                    </div>
                </div>
                <div id="dvAddUser" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click"><span class="PrintPDF"></span>Print</asp:LinkButton></span>
                    <span class="btn5">
                        <asp:LinkButton ID="lnkAddNew" runat="server" OnClick="lnkAddNew_Click"><span class="AddNewData"></span>Add
                        New</asp:LinkButton></span>
                </div>
                <div class="reports">
                    Manage Product Purchase
                </div>
                <div class="clear">
                </div>
            </div>
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
                                <ctrl:CustomGridView ID="gvGrid" EmptyDataText="<span class='noDataSelected'>No Data Available</span>"
                                    runat="server" AutoGenerateColumns="false" AllowPaging="True" AllowSorting="True"
                                    Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle" SortColumn="CompanyName"
                                    DataKeyNames="ManufacturerID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                    ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                    CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                    EndDateCol="3" OnRowDataBound="gvGrid_RowDataBound" OnRowCommand="gvGrid_RowCommand"
                                    OnPageIndexChanging="gvGrid_PageIndexChanging" OnRowEditing="gvGrid_RowEditing"
                                    OnRowDeleting="gvGrid_RowDeleting" OnSorting="gvGrid_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Purchase Date" SortExpression="PurchaseDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPurchaseDate" runat="server" Text='<%# Eval("PurchaseDate") %>'
                                                    ToolTip='<%# Eval("PurchaseDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manufacturer" SortExpression="Manufacturer">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManufacturer" runat="server" Text='<%# Eval("ManufacturerName") %>'
                                                    ToolTip='<%# Eval("ManufacturerName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product" SortExpression="Product">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("ProductName") %>' ToolTip='<%# Eval("ProductName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size/Quantity" SortExpression="Sizes">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Sizes") %>' ToolTip='<%# Eval("Sizes") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Quantity" SortExpression="Quantity">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>' ToolTip='<%# Eval("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Buying Price" SortExpression="BuyingPrice">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" Text='<%# String.Format("{0:C}", Eval("BuyingPrice")) %>'
                                                    ToolTip='<%# String.Format("{0:C}", Eval("BuyingPrice")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Margin(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMargin" runat="server" Text='<%# String.Format("{0:C}", Eval("Margin")) %>'
                                                    ToolTip='<%# String.Format("{0:C}", Eval("Margin")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="VAT(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTax" runat="server" Text='<%# String.Format("{0:C}", Eval("Tax")) %>'
                                                    ToolTip='<%# String.Format("{0:C}", Eval("Tax")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Selling Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSPrice" runat="server" Text='<%# String.Format("{0:C}", Eval("SellingPrice")) %>'
                                                    ToolTip='<%# String.Format("{0:C}", Eval("SellingPrice")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Right" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print">
                                            <ItemTemplate>
                                            <asp:Label id="lblPPDID" runat="server" Visible="false" Text='<%# Eval("ProductPurchaseDetailID") %>'></asp:Label>
                                                <asp:CheckBox ID="chkPrint" runat="server" CausesValidation="False" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" CssClass="al" />
                                            <HeaderStyle HorizontalAlign="Center" Font-Underline="false" CssClass="alH" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <%--<asp:LinkButton ID="lnkPrint" runat="server" CommandName="Print" ToolTip="Click to print"
                                                    CausesValidation="False" CommandArgument='<%# Eval("ProductPurchaseDetailID") %>'> <img src="../Images/PrintBarcode.png" alt="Print" /> </asp:LinkButton>--%>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                    CausesValidation="False" CommandArgument='<%# Eval("ProductPurchaseID") %>'> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                                    CommandArgument='<%# Eval("ProductPurchaseID") %>' CausesValidation="False"> <img src="../Images/ico_delete.png" alt="Delete" /> </asp:LinkButton>
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
                    <!--Add/Edit Product Purchase -->
                    <div id="ModalWindow1" style="display: none" clientidmode="Static">
                        <div class="mainModalAddEdit" id="mainModalAddDataSource">
                            <div class="topM">
                                <h1>
                                    <span id="spTitle">Add/Edit Purchase</span><a onclick="return CloseAddDiv('ModalWindow1');"
                                        id="lnkCloseAddDiv" title="Close"> </a>
                                </h1>
                            </div>
                            <div id="MidM2" class="MidM">
                                <div class="addNew" id="addNew2">
                                    <div id="updDataSource">
                                        <div id="dvInnerWindow" class="modalContent">
                                            <fieldset class="fieldAddEdit">
                                                <div class="inner">
                                                    <div class="mandet">
                                                        <span id="lblMessage">* Fields are mandatory</span></div>
                                                    <div class="errorMsg">
                                                        <span id="lblError" runat="server"></span>
                                                    </div>
                                                    <div>
                                                        Manufacturer :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="txtUpl">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="reqManufacturer" runat="server" ValidationGroup="frm"
                                                            ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" ControlToValidate="ddlManufacturer"
                                                            Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Product :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:DropDownList ID="ddlProduct" runat="server" CssClass="txtUpl">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="reqProduct" runat="server" ValidationGroup="frm"
                                                            ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" ControlToValidate="ddlProduct"
                                                            Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Size:<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:DropDownList ID="ddlSize" runat="server" CssClass="txtUpl">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="reqddlSize" runat="server" ValidationGroup="frm"
                                                            ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" ControlToValidate="ddlSize"
                                                            Display="Dynamic" InitialValue="-1"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Quantity :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="txtCred" onkeyup="extractNumber(this,0,false);"
                                                            onblur="extractNumber(this,0,false);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtQuantity" runat="server" ValidationGroup="frm"
                                                            ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtQuantity"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Price :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtPrice" runat="server" CssClass="txtCred" onkeyup="extractNumber(this,2,false);"
                                                            onblur="extractNumber(this,2,false);"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqTxtPrice" runat="server" ValidationGroup="frm"
                                                            ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtPrice"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Date of Purchase :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <input type="text" id="txtDateOfPurchase" name="txtDateOfPurchase" runat="server"
                                                            class="txtCred" />
                                                        <asp:RequiredFieldValidator ID="reqtxtDateOfPurchase" runat="server" ValidationGroup="frm"
                                                            ErrorMessage="*" Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtDateOfPurchase"
                                                            Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="btn-wrapper4">
                                                        <span class="btn">
                                                            <asp:LinkButton ID="lnkBtnSaveDS" runat="server" OnClick="lnkBtnSaveDS_Click" ValidationGroup="frm">Save</asp:LinkButton></span>
                                                        <span class="btn">
                                                            <asp:LinkButton ID="lnkCancel" runat="server" OnClientClick="return CloseAddDiv('ModalWindow1');">Cancel</asp:LinkButton>
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
<!--Date Picker-->
<script src="../Scripts/jquery.ui.core.js" type="text/javascript"></script>
<script src="../Scripts/jquery.ui.datepicker.js" type="text/javascript"></script>
<script src="../Scripts/jquery.ui.widget.js" type="text/javascript"></script>
<link href="../Styles/jquery.ui.datepicker.css" rel="stylesheet" type="text/css" />
<link href="../Styles/jquery.ui.theme.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    $(function () {
        $("#fromDate").datepicker({
            showOn: "button",
            buttonImage: "../images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0d"
        });
    });
    $(function () {
        $("#toDate").datepicker({
            showOn: "button",
            buttonImage: "../images/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            maxDate: "+0d"
        });
    });
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

    // Auto complete Manufacturer
    $(function () {
        Populate();
    });

    function Populate() {
        var projects = [];
        var MyKeys = ["value", "label"];


        dimArrayValue = $("#hdnManufacturer").val();

        var DataArr = dimArrayValue.split("@@");

        for (i = 0; i < DataArr.length; i++) {
            var DataArr2 = DataArr[i].split("##");
            var obj = {};
            for (j = 0; j < DataArr2.length; j++) {
                obj[MyKeys[j]] = DataArr2[j];
            }
            projects.push(obj);
        }


        $("#txtSearchManufacturer").autocomplete({
            minLength: 0,
            source: projects,
            focus: function (event, ui) {
                $("#txtSearchManufacturer").val(ui.item.label);
                return false;
            },
            select: function (event, ui) {
                $("#txtSearchManufacturer").val(ui.item.label);
                $("#txtSearchManufacturerId").val(ui.item.value);
                return false;
            }
        })
		.data("autocomplete")._renderItem = function (ul, item) {
		    return $("<li></li>")
				.data("item.autocomplete", item)
				.append("<a>" + item.label + "</a>")
				.appendTo(ul);
		};
    }

    $(function () {
        /*if ($('#txtSearchManufacturer').val() == "") {
        $('#txtSearchManufacturer').val('Manufacturer Name')
        }*/
        var tbval = $('#txtSearchManufacturer').val();
        $('#txtSearchManufacturer').focus(function () { if ($(this).val() == tbval) $(this).val(''); });
        $('#txtSearchManufacturer').blur(function () { if ($(this).val() == tbval) $(this).val(tbval); });
    });

</script>
<!--Date Picker-->
