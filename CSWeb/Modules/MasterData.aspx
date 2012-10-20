<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterData.aspx.cs" Inherits="Modules_MasterData" %>

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
                    Manage Master Data
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
                                <div class="divCenter">
                                    <div id="divSize" style="float: left; padding-right: 10px;">
                                        <div class="reports" style="margin-bottom: 10px">
                                            Manage Size
                                        </div>
                                        <br style="clear: both" />
                                        <div id="updDataSource1">
                                            <fieldset>
                                                <div class="grid_container">
                                                    <ctrl:CustomGridView ID="gvSize" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                        AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                                        SortColumn="SizeID" DataKeyNames="SizeID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                        SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ShowFooter="true" EmptyDataText="No Record Found"
                                                        OnRowCancelingEdit="gvSize_RowCancelingEdit" OnRowCommand="gvSize_RowCommand"
                                                        OnRowDataBound="gvSize_RowDataBound" OnRowDeleting="gvSize_RowDeleting" OnRowEditing="gvSize_RowEditing"
                                                        OnRowUpdating="gvSize_RowUpdating" 
                                                        onpageindexchanging="gvSize_PageIndexChanging">
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblSizeID" runat="server" Text="SizeID" Visible="false" /></b>
                                                                    </th>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblSizeName" runat="server" Text="SizeName" /></b>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSizeID1" CssClass="txtMasterData" runat="server" Visible="false"
                                                                            Text="0" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtSizeName1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDSi" ID="ReqtxtSize" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeName1"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewDSi" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SizeID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSizeID" runat="server" Text='<%# Eval("SizeID") %>' Visible="false" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSizeIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SizeID") %>'
                                                                        Visible="false" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtSizeID" CssClass="txtMasterData" Text="0" runat="server" Visible="false" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("SizeName") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtSizeNameE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SizeName") %>' />
                                                                     <asp:RequiredFieldValidator ValidationGroup="NewDSiE" ID="ReqtxtSizeE" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeNameE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtSizeName" CssClass="txtMasterData" runat="server" />
                                                                     <asp:RequiredFieldValidator ValidationGroup="NewDSi" ID="ReqtxtSize" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSizeName"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                                        CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                                        CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                                        ToolTip="Save" ValidationGroup="NewDSiE" />
                                                                    <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                        runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                </EditItemTemplate>
                                                                <FooterStyle />
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                        CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewDSi" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="tdData" />
                                                        <HeaderStyle CssClass="trHeader" />
                                                    </ctrl:CustomGridView>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div id="divCategory" style="float: left; padding-right: 10px;">
                                        <div class="reports" style="margin-bottom: 10px">
                                            Manage Category
                                        </div>
                                        <br style="clear: both" />
                                        <div id="updDataSource2">
                                            <fieldset>
                                                <div class="grid_container">
                                                    <ctrl:CustomGridView ID="grvCategory" runat="server" AutoGenerateColumns="false"
                                                        AllowPaging="True" AllowSorting="True" Width="100%" PageSize="50" GridLines="None"
                                                        CssClass="gvStyle" SortColumn="CategoryID" DataKeyNames="CategoryID" SortOrder="Ascending"
                                                        SortAscImageUrl="~/Images/GridViewCtrl/asc.png" SortDescImageUrl="~/Images/GridViewCtrl/dsc.png"
                                                        ExportTemplatePath="~/Reports/Templates/" ExcelHeaderRow="8" StartRow="10" StartColumn="2"
                                                        DBColumn="" MaxLevel="1" SheetNumber="1" CurrentDateRow="6" CurrentDateCol="3"
                                                        StartDateRow="4" StartDateCol="3" EndDateRow="5" EndDateCol="3" ShowFooter="true"
                                                        EmptyDataText="No Record Found" OnRowCancelingEdit="grvCategory_RowCancelingEdit"
                                                        OnRowCommand="grvCategory_RowCommand" OnRowDataBound="grvCategory_RowDataBound"
                                                        OnRowDeleting="grvCategory_RowDeleting" OnRowEditing="grvCategory_RowEditing"
                                                        OnRowUpdating="grvCategory_RowUpdating" 
                                                        onpageindexchanging="grvCategory_PageIndexChanging">
                                                        <EmptyDataTemplate>
                                                            <table cellpadding="0" cellspacing="0" width="100%">
                                                                <tr>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblCategoryID" runat="server" Text="CategoryID" Visible="false" /></b>
                                                                    </th>
                                                                    <th>
                                                                        <b>
                                                                            <asp:Label ID="lblCategoryName" runat="server" Text="CategoryName" /></b>
                                                                    </th>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCategoryID" CssClass="txtMasterData" runat="server" Visible="false"
                                                                            Text="0" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtCategoryName1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDC" ID="ReqtxtCat1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCategoryName1"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                     <td>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewDC" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="CategoryID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategoryID" runat="server" Text='<%# Eval("CategoryID") %>' Visible="false" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtCategoryIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("CategoryID") %>'
                                                                        Visible="false" />
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtCategoryID" CssClass="txtMasterData" Text="0" runat="server"
                                                                        Visible="false" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("CategoryName") %>' />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtCategoryNameE" CssClass="txtMasterData" runat="server" Text='<%# Eval("CategoryName") %>' />
                                                                     <asp:RequiredFieldValidator ValidationGroup="NewDCE" ID="ReqtxtCatE" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCategoryNameE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </EditItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtCategoryName" CssClass="txtMasterData" runat="server" />
                                                                     <asp:RequiredFieldValidator ValidationGroup="NewDC" ID="ReqtxtCat" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtCategoryName"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <HeaderStyle />
                                                                <ItemStyle />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                                        CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                                        CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                                        ToolTip="Save" ValidationGroup="NewDCE" />
                                                                    <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                        runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                </EditItemTemplate>
                                                                <FooterStyle />
                                                                <FooterTemplate>
                                                                    <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                        CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewDC" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <RowStyle CssClass="tdData" />
                                                        <HeaderStyle CssClass="trHeader" />
                                                    </ctrl:CustomGridView>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                    <div id="divSeason" style="float: left; padding-right: 10px;">
                                        <div class="reports" style="margin-bottom: 10px">
                                            Manage Season
                                        </div>
                                        <br style="clear: both" />
                                        <div id="updDataSource3">
                                            <div id="Div1">
                                                <fieldset>
                                                    <div class="grid_container">
                                                        <ctrl:CustomGridView ID="grvSeason" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                            AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                                            SortColumn="SeasonID" DataKeyNames="SeasonID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                                            EndDateCol="3" ShowFooter="true" EmptyDataText="No Record Found" OnRowCancelingEdit="grvSeason_RowCancelingEdit"
                                                            OnRowCommand="grvSeason_RowCommand" 
                                                            OnRowDataBound="grvSeason_RowDataBound" OnRowDeleting="grvSeason_RowDeleting"
                                                            OnRowEditing="grvSeason_RowEditing" OnRowUpdating="grvSeason_RowUpdating" 
                                                            onpageindexchanging="grvSeason_PageIndexChanging">
                                                            <EmptyDataTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblSeasonID" runat="server" Text="SeasonID" Visible="false" /></b>
                                                                        </th>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblSeason" runat="server" Text="Season" /></b>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtSeasonID1" CssClass="txtMasterData" runat="server" Visible="false"
                                                                                Text="0" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtSeason1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                             <asp:RequiredFieldValidator ValidationGroup="NewDS" ID="ReqtxtSeason1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSeason1"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                                CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewDS" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SeasonID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeasonID" runat="server" Text='<%# Eval("SeasonID") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtSeasonIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SeasonID") %>'
                                                                            Visible="false" />
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtSeasonID" CssClass="txtMasterData" Text="0" runat="server" Visible="false" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Season">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSeason" runat="server" Text='<%# Eval("SeasonName") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtSeasonE" CssClass="txtMasterData" runat="server" Text='<%# Eval("SeasonName") %>' />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDSE" ID="ReqtxtSeasoE" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSeasonE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtSeason" CssClass="txtMasterData" runat="server" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewDS" ID="ReqtxtSeason" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtSeason"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <HeaderStyle />
                                                                    <ItemStyle />
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
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divBrand" style="float: left">
                                        <div class="reports" style="margin-bottom: 10px">
                                            Manage Brand
                                        </div>
                                        <br style="clear: both" />
                                        <div id="Div6">
                                            <div id="dvInnerWindow3">
                                                <fieldset>
                                                    <div class="grid_container">
                                                        <ctrl:CustomGridView ID="grvBrand" runat="server" AutoGenerateColumns="false" AllowPaging="True"
                                                            AllowSorting="True" Width="100%" PageSize="50" GridLines="None" CssClass="gvStyle"
                                                            SortColumn="BrandID" DataKeyNames="BrandID" SortOrder="Ascending" SortAscImageUrl="~/Images/GridViewCtrl/asc.png"
                                                            SortDescImageUrl="~/Images/GridViewCtrl/dsc.png" ExportTemplatePath="~/Reports/Templates/"
                                                            ExcelHeaderRow="8" StartRow="10" StartColumn="2" DBColumn="" MaxLevel="1" SheetNumber="1"
                                                            CurrentDateRow="6" CurrentDateCol="3" StartDateRow="4" StartDateCol="3" EndDateRow="5"
                                                            EndDateCol="3" ShowFooter="true" EmptyDataText="No Record Found" OnRowCancelingEdit="grvBrand_RowCancelingEdit"
                                                            OnRowCommand="grvBrand_RowCommand" OnRowDataBound="grvBrand_RowDataBound" OnRowDeleting="grvBrand_RowDeleting"
                                                            OnRowEditing="grvBrand_RowEditing" OnRowUpdating="grvBrand_RowUpdating" 
                                                            onpageindexchanging="grvBrand_PageIndexChanging">
                                                            <EmptyDataTemplate>
                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                    <tr>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblBrandID" runat="server" Text="BrandID" Visible="false" /></b>
                                                                        </th>
                                                                        <th>
                                                                            <b>
                                                                                <asp:Label ID="lblBrand" runat="server" Text="Brand" /></b>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBrandID1" CssClass="txtMasterData" runat="server" Visible="false"
                                                                                Text="0" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtBrand1" CssClass="txtMasterData" runat="server" Visible="true" />
                                                                            <asp:RequiredFieldValidator ValidationGroup="NewD" ID="ReqtxtBrand1" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtBrand1"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                        </td>
                                                                         <td>
                                                                            <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                                CommandName="AddEmpty" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="BrandID" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrandID" runat="server" Text='<%# Eval("BrandID") %>' Visible="false" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBrandIDE" CssClass="txtMasterData" runat="server" Text='<%# Eval("BrandID") %>'
                                                                            Visible="false" />
                                                                            
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtBrandID" CssClass="txtMasterData" Text="0" runat="server" Visible="false" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Brand">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("BrandName") %>' />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBrandE" CssClass="txtMasterData" runat="server" Text='<%# Eval("BrandName") %>' />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewD" ID="ReqtxtBrandE" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtBrandE"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txtBrand" CssClass="txtMasterData" runat="server" />
                                                                        <asp:RequiredFieldValidator ValidationGroup="NewD" ID="ReqtxtBrand" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtBrand"
                                    Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Action">
                                                                    <HeaderStyle />
                                                                    <ItemStyle />
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgbtnEdit" ImageUrl="../Images/ico_Edit.png" runat="server"
                                                                            CommandName="Edit" ToolTip="Edit" ValidationGroup="Edit" />
                                                                        <asp:ImageButton ID="imgbtnDelete" ImageUrl="../Images/ico_delete.png" runat="server"
                                                                            CommandName="Delete" ToolTip="Delete" OnClientClick="return confirm('Are you sure you want to delete? ');" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton ID="imgbtSave" ImageUrl="../Images/save.png" runat="server" CommandName="Update"
                                                                            ToolTip="Save" ValidationGroup="ProductFamilyEditRow" />
                                                                        <asp:ImageButton ID="imgbtnCancel" CausesValidation="false" ImageUrl="../Images/cancel.png"
                                                                            runat="server" CommandName="Cancel" ToolTip="Cancel" />
                                                                    </EditItemTemplate>
                                                                    <FooterStyle />
                                                                    <FooterTemplate>
                                                                        <asp:ImageButton ID="imgbtnSaveNew" ImageUrl="../Images/Plusorange.png" runat="server"
                                                                            CommandName="Add" ToolTip="Add New" Visible="true" ValidationGroup="NewD" />
                                                                    </FooterTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle CssClass="tdData" />
                                                            <HeaderStyle CssClass="trHeader" />
                                                        </ctrl:CustomGridView>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
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
