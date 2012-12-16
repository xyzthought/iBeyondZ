<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageCustomer.aspx.cs" Inherits="Modules_ManageCustomer" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function ClearFormFields() {
            $('#txtFirstName').val('');
            $('#txtLastName').val('');
            $('#txtEmailID').val('');
            $('#txtAddress').val('');
            $('#txtZIP').val('');
            $('#txtCity').val('');
            $('#txtCountry').val('');
            $('#txtPhone').val('');
            $('#lblError').empty();
            $('#txtFirstName').focus();
        }

        function SetFocusOnName() {
            $('#txtFirstName').focus();
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
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" OnClick="lnkBtnSearch_Click"
                        CausesValidation="False"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dvAddUser" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkAddNew" href="#" runat="server" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow1','dvInnerWindow',0)"><span class="AddNewData"></span>Add Customer</asp:LinkButton></span>
                </div>
                <div class="reports">
                    Manage Customer
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
                                    Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle" SortColumn="FirstName"
                                    DataKeyNames="CustomerID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                    ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                    CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                    EndDateCol="3" OnRowDataBound="gvGrid_RowDataBound" OnRowCommand="gvGrid_RowCommand"
                                    OnPageIndexChanging="gvGrid_PageIndexChanging" OnRowEditing="gvGrid_RowEditing"
                                    OnRowDeleting="gvGrid_RowDeleting" OnSorting="gvGrid_Sorting">
                                    <Columns>
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
                                        <asp:TemplateField HeaderText="Email-ID" SortExpression="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>' ToolTip='<%# Eval("Email") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tele-Number">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTeleNumber" runat="server" Text='<%# Eval("TeleNumber") %>' ToolTip='<%# Eval("TeleNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCreatedOn" runat="server" Text='<%# Eval("CreatedOn", "{0:dd-MMM-yy}") %>'
                                                    ToolTip='<%# Eval("CreatedOn", "{0:dd-MMM-yy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Updated On" SortExpression="UpdatedOn">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpdatedOn" runat="server" Text='<%# Eval("UpdatedOn", "{0:dd-MMM-yy}") %>'
                                                    ToolTip='<%# Eval("UpdatedOn", "{0:dd-MMM-yy}") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                    CausesValidation="False" CommandArgument='<%# Eval("CustomerID") %>' OnClientClick="return ClearFormFields();SetFocusOnName();"> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                                    CommandArgument='<%# Eval("CustomerID") %>' CausesValidation="False"> <img src="../Images/ico_delete.png" alt="Delete" /> </asp:LinkButton>
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
                                    <span id="spTitle">Add/Edit Customer</span><a onclick="return CloseAddDiv('ModalWindow1');"
                                        id="lnkCloseAddDiv" title="Close"> </a>
                                </h1>
                            </div>
                            <div id="MidM2" class="MidM">
                                <div class="addNew" id="addNew2">
                                    <div id="updDataSource">
                                        <div id="dvInnerWindow" class="modalContent">
                                            <fieldset class="fieldAddEdit">
                                                <div class="inner" style="margin: 0 5px 0px!important;">
                                                    <div class="mandet">
                                                        <span id="lblMessage">* Fields are mandatory</span></div>
                                                    <div class="errorMsg">
                                                        <span id="lblError" runat="server"></span>
                                                    </div>
                                                    <div>
                                                        First Name :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt">
                                                        <div class="demo">
                                                            <asp:TextBox ID="txtFirstName" runat="server" ClientIDMode="Static" CssClass="txtCred" />
                                                        </div>
                                                    </div>
                                                    <div>
                                                        Last Name :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt">
                                                        <div class="demo">
                                                            <asp:TextBox ID="txtLastName" runat="server" ClientIDMode="Static" CssClass="txtCred" />
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
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtCred" Text="vds@sofism.be"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegEmail" runat="server" ErrorMessage="Invalid Email"
                                                            Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID"
                                                            ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div style="clear: both">
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
