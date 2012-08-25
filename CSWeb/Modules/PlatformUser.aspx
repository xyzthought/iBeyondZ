<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlatformUser.aspx.cs" Inherits="Modules_PlatformUser" %>

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
                <div class="searchBox">
                    <asp:TextBox ID="txtSearch" value="Search" runat="server" class="searchBoxTxt" onkeypress="return SetDefaultButton(event,1);"
                        onfocus="if (this.value==&#39;Search&#39;) this.value=&#39;&#39;" onblur="if (this.value==&#39;&#39;) this.value=&#39;Search&#39;" />
                    <a id="lnkBtnSearch" class="searchBoxBtn" href="#"></a>
                    <div class="clear">
                    </div>
                </div>
                <div id="ContentPlaceHolder1_dvAddReport" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkAddNew" href="#" runat="server"><span class="AddNewData"></span>Add Data 1</asp:LinkButton></span>
                </div>
                <div id="div1" class="fl">
                    <span class="btn5">
                        <asp:LinkButton ID="lnkAddNew2" href="#" runat="server"><span class="AddNewData"></span>Add Data 2</asp:LinkButton>
                    </span>
                </div>
                <div class="reports">
                    Manage Platform User
                </div>
                <div class="clear">
                </div>
            </div>
            <div id="updMain">
                <div id="dvgridcontainer" class="grid_container">
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

                        <asp:TemplateField HeaderText="First Name" SortExpression="FirstName">
                            <ItemTemplate>
                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>' ToolTip='<%# Eval("FirstName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Second Name" SortExpression="SecondName">
                            <ItemTemplate>
                                <asp:Label ID="lblSecondName" runat="server" Text='<%# Eval("SecondName") %>' ToolTip='<%# Eval("SecondName") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Email-ID" SortExpression="CommunicationEmailID">
                            <ItemTemplate>
                                <asp:Label ID="lblCommunicationEmailID" runat="server" Text='<%# Eval("CommunicationEmailID") %>' ToolTip='<%# Eval("CommunicationEmailID") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Last Logged-In" SortExpression="LastLoggedIn">
                            <ItemTemplate>
                                <asp:Label ID="lblCommunicationEmailID" runat="server" Text='<%# Eval("LastLoggedIn") %>' ToolTip='<%# Eval("LastLoggedIn") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Created On" SortExpression="CreatedOn">
                            <ItemTemplate>
                                <asp:Label ID="lblCommunicationEmailID" runat="server" Text='<%# Eval("CreatedOn") %>' ToolTip='<%# Eval("CreatedOn") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Font-Underline="false" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Updated On" SortExpression="UpdatedOn">
                            <ItemTemplate>
                                <asp:Label ID="lblCommunicationEmailID" runat="server" Text='<%# Eval("UpdatedOn") %>' ToolTip='<%# Eval("UpdatedOn") %>'></asp:Label>
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
