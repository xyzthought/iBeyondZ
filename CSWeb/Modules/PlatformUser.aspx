﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlatformUser.aspx.cs" Inherits="Modules_PlatformUser" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function ClearFormFields() {
            $("select#ddlUserType").val('0');
            $('#txtFirstName').val('');
            $('#txtLastName').val('');
            $('#txtLoginID').val('');
            $('#txtPassword').val('');
            $('#txtEmailID').val('');
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
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" 
                        OnClick="lnkBtnSearch_Click" CausesValidation="False"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dvAddUser" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkAddNew" href="#" runat="server" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow1','dvInnerWindow',0)"><span class="AddNewData"></span>Add User</asp:LinkButton></span>
                </div>
                <div class="reports">
                    Manage Platform User
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
                                    Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle" SortColumn="UserType"
                                    DataKeyNames="UserID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                    ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                    CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                    EndDateCol="3" OnRowDataBound="gvGrid_RowDataBound" OnRowCommand="gvGrid_RowCommand"
                                    OnPageIndexChanging="gvGrid_PageIndexChanging" OnRowEditing="gvGrid_RowEditing" OnRowDeleting="gvGrid_RowDeleting"
                                    OnSorting="gvGrid_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="User Type" SortExpression="UserType">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUserType" runat="server" Text='<%# Eval("UserType") %>' ToolTip='<%# Eval("UserType") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>' ToolTip='<%# Eval("FirstName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Name" SortExpression="LastName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>' ToolTip='<%# Eval("LastName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email-ID" SortExpression="CommunicationEmailID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCommunicationEmailID" runat="server" Text='<%# Eval("CommunicationEmailID") %>'
                                                    ToolTip='<%# Eval("CommunicationEmailID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Logged-In" SortExpression="LastLoggedIn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastLoggedIn" runat="server" Text='<%# Eval("LastLoggedIn", "{0:dd-MMM-yyyy HH:mm:ss}") %>'
                                                    ToolTip='<%# Eval("LastLoggedIn", "{0:dd-MMM-yyyy HH:mm:ss}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("CreatedOn", "{0:dd-MMM-yyyy}") %>'
                                                    ToolTip='<%# Eval("CreatedOn", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Updated On" SortExpression="UpdatedOn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpdatedOn" runat="server" Text='<%# Eval("UpdatedOn", "{0:dd-MMM-yyyy}") %>'
                                                    ToolTip='<%# Eval("UpdatedOn", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                    CausesValidation="False" CommandArgument='<%# Eval("UserID") %>' OnClientClick="return ClearFormFields();"> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                                    CommandArgument='<%# Eval("UserID") %>' CausesValidation="False"> <img src="../Images/ico_delete.png" alt="Delete" /> </asp:LinkButton>
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
                    <!--Add/Edit Platform User-->
                    <div id="ModalWindow1" style="display: none" clientidmode="Static">
                        <div class="mainModalAddEdit" id="mainModalAddDataSource">
                            <div class="topM">
                                <h1>
                                    <span id="spTitle">Add/Edit Platform User</span><a onclick="return CloseAddDiv('ModalWindow1');"
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
                                                        User Type :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt">
                                                        <asp:DropDownList ID="ddlUserType" runat="server" CssClass="txtUpl">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div>
                                                        First Name :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtFirstName" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtFirstName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Last Name :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtSecondName" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtLastName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        LoginID :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtLoginID" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqtxtLoginID" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                            ForeColor="Red" ControlToValidate="txtLoginID" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Password :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                            ForeColor="Red" ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Communication Email-ID :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqtxtEmailID" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                            ForeColor="Red" ControlToValidate="txtEmailID" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegEmail" runat="server" ErrorMessage="Invalid Email"
                                                            Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID"
                                                            ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div class="btn-wrapper4">
                                                        <span class="btn">
                                                            <asp:LinkButton ID="lnkBtnSaveDS" runat="server" OnClick="lnkBtnSaveDS_Click">Save</asp:LinkButton></span>
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
