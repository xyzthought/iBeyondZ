<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEditSaleOrder.aspx.cs"
    Inherits="Modules_AddEditSaleOrder" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
                        <!--Outer Div-->
                        <div class="accPend">
                            <!--Start Add/Edit product-->
                            <div class="smallDivLeft">
                                <div class="acceptedCont">
                                    <div id="MidM2">
                                        <fieldset class="fieldAddEdit">
                                            <div class="inner">
                                                <div class="mandet">
                                                    <span id="lblMessage">* Fields are mandatory</span></div>
                                                <div class="errorMsg">
                                                    <span id="lblError" runat="server"></span>
                                                </div>
                                                <div>
                                                    Product Bar Code :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt">
                                                    <asp:TextBox ID="txtProductBarCode" runat="server" CssClass="txtCred"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtProductBarCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                                <div>
                                                    Product :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="txtProduct" runat="server" CssClass="txtCred"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="reqtxtProduct" runat="server" ErrorMessage="*"
                                                        Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtProduct" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                                <div>
                                                    Size :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:DropDownList ID="ddlSize" runat="server" CssClass="txtUpl">
                                                        </asp:DropDownList>
                                                </div>
                                                <div>
                                                    Quantity :<span class="mandet2">* </span>
                                                </div>
                                                <div class="alt" style="margin-bottom: 5px;">
                                                    <asp:TextBox ID="txtQuantity" runat="server" MaxLength="4" CssClass="txtCred"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="ReqQuantity" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                        ForeColor="Red" ControlToValidate="txtQuantity" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                                <div>
                                                    Price :&nbsp;<asp:Label ID="lblPrice" runat="server" CssClass="txtCred"></asp:Label>
                                                </div>
                                                
                                                <div class="btn-wrapper4">
                                                    <span class="btn">
                                                        <asp:LinkButton ID="lnkBtnSaveDS" runat="server">Save</asp:LinkButton></span>
                                                    <span class="btn">
                                                        <asp:LinkButton ID="lnkCancel" runat="server">Cancel</asp:LinkButton>
                                                    </span>
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
                                    dddddd</div>
                            </div>
                            <!--End Payment Deatils-->
                            <div style="clear: both">
                            </div>
                            <!-- Start Product Deatil Grid-->
                            <div>
                            </div>
                            <!--End Product Deatil Grid-->
                        </div>
                    </div>
                </div>
            </div>
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
