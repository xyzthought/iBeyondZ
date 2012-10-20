<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Product.aspx.cs" Inherits="Modules_Product" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Products</title>
    <script type="text/javascript">
        function ClearFormFields() {

            $('#txtProductName').val('');
            $('#txtDescription').val('');
            $('#txtBuyingPrice').val('');
            $('#txtTax').val('');
            $('#txtMargin').val('');
            $('#txtCity').val('');
            $('#txtSellingPrice').val('');
            $('#txtBarCode').val('');

            $('#lblError').empty();
        }
    </script>
    <style type="text/css">
    .gvStyle tr td, .gvStyle tr th{padding:5px;vertical-align:top;}
    </style>
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
                <input type="hidden" name="ctl00$ContentPlaceHolder1$currentReportName" id="currentReportName" />
                <div class="reports">
                    Manage Products
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
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
                                <div class="inner" style="width: 700px!important; margin: 10px auto; border: 1px solid #eee">
                                    <ctrl:CustomGridView ID="gvGrid" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                        AllowSorting="True" Width="100%" PageSize="15" GridLines="None" CssClass="gvStyle"
                                        SortColumn="ProductName" DataKeyNames="ProductID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                        SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ShowFooter="true" EmptyDataText="No Record Found"
                                        OnRowCancelingEdit="gvGrid_RowCancelingEdit" OnRowCommand="gvGrid_RowCommand"
                                        OnRowDataBound="gvGrid_RowDataBound" OnRowDeleting="gvGrid_RowDeleting" OnRowEditing="gvGrid_RowEditing"
                                        OnRowUpdating="gvGrid_RowUpdating">
                                        <EmptyDataTemplate>
                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <th>
                                                        <b>
                                                            <asp:Label ID="lblProductID" runat="server" Text="ProductID" Visible="false" /></b>
                                                    </th>
                                                    <th>
                                                        <b>
                                                            <asp:Label ID="lblProductName" runat="server" Text="ProductName" /></b>
                                                    </th>
                                                    <th>
                                                        <b>
                                                            <asp:Label ID="lblDescription" runat="server" Text="Description" /></b>
                                                    </th>
                                                    <th>
                                                        <b>
                                                            <asp:Label ID="lblMargin" runat="server" Text="Margin" /></b>
                                                    </th>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtProductID1" CssClass="txtProdDesc" runat="server" Visible="false"
                                                            Text="0" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtProductName1" CssClass="txtProdDesc" runat="server" Visible="true" />
                                                        <asp:RequiredFieldValidator ValidationGroup="NewDS" ID="ReqtxtProduct" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtProductName1"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtDescription1" CssClass="txProdDescTextArea" runat="server" Visible="true"
                                                            Rows="3" />
                                                    </td>
                                                    <td valign="top">
                                                        <asp:TextBox ID="txtMargin1" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);"
                                                            CssClass="txtProdDesc" runat="server" Visible="true" Style="width: 100px; text-align: right;" />
                                                    </td>
                                                     <td>
                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                            CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewDS" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="ProductID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>' Visible="false" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProductIDE" CssClass="txtProdDesc" runat="server" Text='<%# Eval("ProductID") %>'
                                                        Visible="false" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductID" CssClass="txtProdDesc" Text="0" runat="server" Visible="false" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product">
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProductNameE" CssClass="txtProdDesc" runat="server" Text='<%# Eval("ProductName") %>' />
                                                    <asp:RequiredFieldValidator ValidationGroup="NewDSE" ID="ReqtxtProductE" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtProductNameE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>

                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtProductName" CssClass="txtProdDesc" runat="server" />
                                                    <asp:RequiredFieldValidator ValidationGroup="NewDS" ID="ReqtxtProduct" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtProductName"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDescriptionE" CssClass="txProdDescTextArea" runat="server" Text='<%# Eval("Description") %>'
                                                        TextMode="MultiLine" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtDescription" CssClass="txProdDescTextArea" runat="server" TextMode="MultiLine" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Margin">
                                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMargin" runat="server" Text='<%# Eval("Margin") %>' />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtMarginE" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);"
                                                        CssClass="txtProdDesc" Style="width: 100px; text-align: right;" runat="server"
                                                        Text='<%# Eval("Margin") %>' />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtMargin" CssClass="txtProdDesc" runat="server" Style="width: 100px;
                                                        text-align: right;" onkeyup="extractNumber(this,-1,false);" onblur="extractNumber(this,-1,false);" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action">
                                                <HeaderStyle />
                                                <ItemStyle VerticalAlign="Top" />
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                        CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                        CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                        ToolTip="Save" ValidationGroup="NewDSE" />
                                                    <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                        runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                </EditItemTemplate>
                                                <FooterStyle />
                                                <FooterTemplate>
                                                    <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                        CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewDS" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="tdData" />
                                        <HeaderStyle CssClass="trHeader" />
                                    </ctrl:CustomGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br style="clear: both" />
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
