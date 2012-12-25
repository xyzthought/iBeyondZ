<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Manufacturer.aspx.cs" Inherits="Modules_Manufacturer" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Manufacturer</title>
    <script type="text/javascript">
        function ClearFormFields() {
            $('#txtCompanyName').val('');
            $('#txtContactFirstName').val('');
            $('#txtContactLastName').val('');
            $('#txtAddress').val('');
            $('#txtZIP').val('');
            $('#txtCity').val('');
            $('#txtCountry').val('');
            $('#txtPhone').val('');
            $('#txtEmailID').val('');
            $('#chkIsActive').prop("checked", false);
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
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" ValidationGroup="abc" OnClick="lnkBtnSearch_Click"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dvAddUser" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkAddNew" href="#" runat="server" OnClientClick="ClearFormFields();ShowModalDiv('ModalWindow1','dvInnerWindow',0)"><span class="AddNewData"></span>Add Manufacturer</asp:LinkButton></span>
                </div>
                <div class="reports">
                    Manage Manufacturer
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
                                    Width="100%" PageSize="20" GridLines="None" CssClass="gvStyle" SortColumn="CompanyName"
                                    DataKeyNames="ManufacturerID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                    SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                    ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                    CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                    EndDateCol="3" OnRowDataBound="gvGrid_RowDataBound" OnRowCommand="gvGrid_RowCommand"
                                    OnPageIndexChanging="gvGrid_PageIndexChanging" OnRowEditing="gvGrid_RowEditing"
                                    OnRowDeleting="gvGrid_RowDeleting" OnSorting="gvGrid_Sorting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' ToolTip='<%# Eval("CompanyName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact First Name" SortExpression="ContactFirstName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactFirstName" runat="server" Text='<%# Eval("ContactFirstName") %>'
                                                    ToolTip='<%# Eval("ContactFirstName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact Last Name" SortExpression="ContactLastName">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactLastName" runat="server" Text='<%# Eval("ContactLastName") %>'
                                                    ToolTip='<%# Eval("ContactLastName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City" SortExpression="City">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("City") %>' ToolTip='<%# Eval("City") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Country" SortExpression="Country">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>' ToolTip='<%# Eval("Country") %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Phone" SortExpression="Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>' ToolTip='<%# Eval("Phone") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" ToolTip="Click to edit"
                                                    CausesValidation="False" CommandArgument='<%# Eval("ManufacturerID") %>' OnClientClick="return ClearFormFields();"> <img src="../Images/ico_edit.png" alt="Edit" /> </asp:LinkButton>
                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" ToolTip="Click to delete"
                                                    CommandArgument='<%# Eval("ManufacturerID") %>' CausesValidation="False"> <img src="../Images/ico_delete.png" alt="Delete" /> </asp:LinkButton>
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
                    <!--Add/Edit Manufacturer -->
                    <div id="ModalWindow1" style="display: none" clientidmode="Static">
                        <div class="mainModalAddEdit" id="mainModalAddDataSource">
                            <div class="topM">
                                <h1>
                                    <span id="spTitle">Add/Edit Manufacturer</span><a onclick="return CloseAddDiv('ModalWindow1');"
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
                                                        Company Name :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtCompanyName" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtFirstName" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCompanyName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Contact First Name :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtContactFirstName" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtSecondName" runat="server" ErrorMessage="*"
                                                            Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtContactFirstName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Contact Last Name:<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtContactLastName" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="ReqtxtLoginID" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                            ForeColor="Red" ControlToValidate="txtContactLastName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div>
                                                        Address :<span class="mandet2">* </span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="reqtxtPassword" runat="server" ErrorMessage="*" Font-Size="X-Small"
                                                            ForeColor="Red" ControlToValidate="txtAddress" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                                    <div style="float: left;padding-left:18px">
                                                        <div style="margin: 0!important">
                                                            City :<span class="mandet2">* </span>
                                                        </div>
                                                        <div class="alt" style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                                Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCity" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div style="clear:both"></div>
                                                    <div  style="float: left">
                                                        <div style="margin: 0!important">
                                                            Country :<span class="mandet2">* </span>
                                                        </div>
                                                        <div class="alt" style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtCountry" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                                                                Font-Size="X-Small" ForeColor="Red" ControlToValidate="txtCountry" Display="Dynamic"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                    <div style="float: left;padding-left:18px">
                                                        <div style="margin: 0!important">
                                                            Phone :<span class="mandet2"></span>
                                                        </div>
                                                        <div class="alt" style="margin-bottom: 5px;">
                                                            <asp:TextBox ID="txtPhone" runat="server" CssClass="txtCred" Style="width: 160px!important"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div style="clear:both"></div>
                                                    <div>
                                                        Email :<span class="mandet2"></span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="txtCred"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegEmail" runat="server" ErrorMessage="<br>Invalid Email"
                                                            Font-Size="X-Small" ForeColor="Red" Display="Dynamic" ControlToValidate="txtEmailID"
                                                            ValidationExpression="^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$"></asp:RegularExpressionValidator>
                                                    </div>
                                                    <div>
                                                        Is Active :<span class="mandet2"></span>
                                                    </div>
                                                    <div class="alt" style="margin-bottom: 5px;">
                                                        <asp:CheckBox ID="chkIsActive" runat="server" CssClass="txtCred"></asp:CheckBox>
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
