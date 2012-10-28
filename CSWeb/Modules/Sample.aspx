<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample.aspx.cs" Inherits="Modules_Sample" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                <input type="hidden" name="hdnData" id="hdnData" />
                <div class="searchBox">
                    <asp:TextBox ID="txtSearch" value="Search" runat="server" class="searchBoxTxt" onkeypress="return SetDefaultButton(event,1);"
                        onfocus="if (this.value==&#39;Search&#39;) this.value=&#39;&#39;" onblur="if (this.value==&#39;&#39;) this.value=&#39;Search&#39;" />
                    <a id="lnkBtnSearch" class="searchBoxBtn" href="#"></a>
                    <div class="clear">
                    </div>
                </div>
                <div id="dvAdd" class="fl">
                    <span class="btn5"><asp:LinkButton id="lnkAddNew" href="#" runat="server"><span class="AddNewData"></span>Add Data 1</asp:LinkButton></span>
                </div>
                <div id="divAdd1" class="fl">
                    <span class="btn5"><asp:LinkButton id="lnkAddNew2" href="#" runat="server"><span class="AddNewData"></span>Add Data 2</asp:LinkButton> </span>
                </div>
                <div class="reports">
                    Home | Top Information
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
            <div id="updMain">
            <asp:TextBox ID="txtTest" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DropDownList1" runat="server" 
                    onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="RegularExpressionValidator" ControlToValidate="txtTest" ValidationExpression="\\w+([-+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"></asp:RegularExpressionValidator>

                <div id="dvgridcontainer" class="grid_container">
                    <!--Start GRID-->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                        <ContentTemplate>
                            <div style="margin: 0px auto; padding: 0px; text-align: center;">
                                <div id="divMess" runat="server" visible="false">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <br style="clear: both" />
                            <div class="grid_container">
                                <ctrl:CustomGridView id="gvGrid" emptydatatext="<span class='noDataSelected'>No Data Available</span>"
                                    runat="server" autogeneratecolumns="false" allowpaging="True" allowsorting="True"
                                    width="100%" pagesize="20" gridlines="None" cssclass="gvStyle" sortcolumn="UserType" DataKeyNames="UserID"
                                    sortorder="Ascending" sortascimageurl="~/Images/GridViewCtrl/asc.png" sortdescimageurl="~/Images/GridViewCtrl/dsc.png"
                                    exporttemplatepath="~/Reports/Templates/" excelheaderrow="8" startrow="10" startcolumn="2"
                                    dbcolumn="" maxlevel="1" sheetnumber="1" currentdaterow="6" currentdatecol="3"
                                    startdaterow="4" startdatecol="3" enddaterow="5" enddatecol="3">
                    <Columns>
                        <asp:TemplateField HeaderText="User Type" SortExpression="UserType">
                            <ItemTemplate>
                                <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("UserType") %>' ToolTip='<%# Eval("UserType") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <a id="aEdit" runat="server" href="#" onclick="CallManageLoader();" title="Click to edit">
                                    <img src="../../Images/iconEditAction.png" alt="Edit" /></a>
                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                    CommandArgument='<%# Eval("dd") +"|"+ Eval("dd") %>'> <img src="../../Images/iconDeleteAction.png" alt="Delete" /> </asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" CssClass="al" />
                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" CssClass="alH" />
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="tdData" />
                    <HeaderStyle CssClass="trHeader" />
                </ctrl:CustomGridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--End Grid-->
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
