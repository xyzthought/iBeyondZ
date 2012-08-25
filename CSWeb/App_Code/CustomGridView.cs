using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Globalization;
using System.Data;
using BLL.BusinessObject;
using CustomExcel.Entity;
using CSWeb.Utility;


namespace Custom.WebGridViewControls
{

    /// <summary>
    /// Summary description for AugmeGridView
    /// </summary>
    public class CustomGridView : GridView
    {
        #region Properties

        #region Sort Column
        /// <summary>
        /// Get or Set Sort Column for the grid.
        /// </summary>
        [
        Description("Sort Expression the grid will be sorted on"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string SortColumn
        {
            get
            {
                object o = ViewState["SortColumn"];

                if (null == o)
                {
                    return "0";
                }
                else
                {
                    return (string)o;
                }
            }
            set
            {
                ViewState["SortColumn"] = value;
            }
        }
        #endregion

        #region Sort Direction
        /// <summary>
        /// Get or Set Sort Direction.
        /// </summary>
        [
        Description("Sort Direction the grid will be sorted on"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public SortDirection SortOrder
        {
            get
            {
                object o = ViewState["SortOrder"];

                if (null == o)
                {
                    return SortDirection.Ascending;
                }
                else
                {
                    return (SortDirection)o;
                }


            }
            set
            {
                ViewState["SortOrder"] = value;
            }
        }
        #endregion

        #region Sort Direction Text
        /// <summary>
        /// Get or Set Sort Direction.
        /// </summary>
        [
        Description("Sort Direction the grid will be sorted on"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string SortOrderText
        {
            get
            {
                object o = ViewState["SortOrder"];

                if (null == o)
                {
                    return "ASC";
                }
                else
                {
                    if ((SortDirection)o == SortDirection.Ascending)
                    {
                        return "ASC";
                    }
                    else
                    {
                        return "DESC";
                    }
                }


            }
        }
        #endregion

        #region Sort Image Asc
        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Image to display for Ascending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),

        ]
        public string SortAscImageUrl
        {
            get
            {
                object o = ViewState["SortImageAsc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageAsc"] = value;
            }
        }
        #endregion

        #region Sort Image Desc
        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Image to display for Descending Sort"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string SortDescImageUrl
        {
            get
            {
                object o = ViewState["SortImageDesc"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["SortImageDesc"] = value;
            }
        }
        #endregion

        #region Export related

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Export Caption"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string ExportCaption
        {
            get
            {
                object o = ViewState["ExportCaption"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["ExportCaption"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Export Data Base Columns"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string DBColumn
        {
            get
            {
                object o = ViewState["DBColumn"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["DBColumn"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Export Data Base Columns"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string ExcelColumn
        {
            get
            {
                object o = ViewState["ExcelColumn"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["ExcelColumn"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Export Data Base Columns"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string ExportTemplate
        {
            get
            {
                object o = ViewState["ExportTemplate"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["ExportTemplate"] = value;
            }
        }

        /// <summary>
        /// Get or Set Image location to be used to display Ascending Sort order.
        /// </summary>
        [
        Description("Export Data Base Columns"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(""),
        ]
        public string ExportTemplatePath
        {
            get
            {
                object o = ViewState["ExportTemplatePath"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["ExportTemplatePath"] = value;
            }
        }

        [
        Description("Export Caption"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(0),
        ]
        public int ExcelHeaderRow
        {
            get
            {
                object o = ViewState["mintHeaderRow"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintHeaderRow"] = value;
            }
        }


        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int StartRow
        {
            get
            {
                object o = ViewState["mintStartRow"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintStartRow"] = value;
            }
        }


        [
        Description("Export Caption"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(0),
        ]
        public int StartColumn
        {
            get
            {
                object o = ViewState["mintStartColumn"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintStartColumn"] = value;
            }
        }



        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int MaxLevel
        {
            get
            {
                object o = ViewState["mintMaxLevel"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintMaxLevel"] = value;
            }
        }

        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int SheetNumber
        {
            get
            {
                object o = ViewState["mintSheetNumber"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintSheetNumber"] = value;
            }
        }

        [
        Description("Export Caption"),
        Category("Misc"),
        Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
        DefaultValue(0),
        ]
        public int CurrentDateRow
        {
            get
            {
                object o = ViewState["mintCurrentDateRow"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintCurrentDateRow"] = value;
            }
        }


        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int CurrentDateCol
        {
            get
            {
                object o = ViewState["mintCurrentDateCol"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintCurrentDateCol"] = value;
            }
        }

        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int StartDateRow
        {
            get
            {
                object o = ViewState["mintStartDateRow"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintStartDateRow"] = value;
            }
        }

        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int StartDateCol
        {
            get
            {
                object o = ViewState["mintStartDateCol"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintStartDateCol"] = value;
            }
        }

        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int EndDateRow
        {
            get
            {
                object o = ViewState["mintEndDateRow"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintEndDateRow"] = value;
            }
        }


        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(0),
       ]
        public int EndDateCol
        {
            get
            {
                object o = ViewState["mintEndDateCol"];
                if (null != o)
                {
                    return Convert.ToInt32(o);
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                ViewState["mintEndDateCol"] = value;
            }
        }

        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(""),
       ]
        public string ReportStartDate
        {
            get
            {
                object o = ViewState["mstrReportStartDate"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["mstrReportStartDate"] = value;
            }
        }

        [
       Description("Export Caption"),
       Category("Misc"),
       Editor("System.Web.UI.Design.UrlEditor", typeof(System.Drawing.Design.UITypeEditor)),
       DefaultValue(""),
       ]
        public string ReportEndDate
        {
            get
            {
                object o = ViewState["mstrReportEndDate"];
                return (o != null ? o.ToString() : "");
            }
            set
            {
                ViewState["mstrReportEndDate"] = value;
            }
        }

        #endregion

        #endregion



        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
            base.OnRowDataBound(e);
        }

        protected override void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {

            CreateCustomPager(row, columnSpan, pagedDataSource);

        }
        protected override void OnDataBound(EventArgs e)
        {
            if ((string.IsNullOrEmpty(ExportTemplatePath) == false) && (string.IsNullOrEmpty(ExportTemplate) == false) && (string.IsNullOrEmpty(DBColumn) == false) & (string.IsNullOrEmpty(ExcelColumn) == false))
            {
                DataTable dt = this.DataSource as DataTable;

                if (null == dt)
                {
                    DataView dv = this.DataSource as DataView;

                    if (null != dv)
                    {
                        dt = dv.ToTable();
                    }
                }

                Random randObj = new Random();
                string strRand = randObj.Next().ToString();

                this.Page.Session[strRand] = dt;

                ViewState["DataId"] = strRand;
            }

            base.OnDataBound(e);
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnSorting(GridViewSortEventArgs e)
        {

            if (SortColumn.Equals(e.SortExpression))
            {
                if (SortOrder == SortDirection.Ascending)
                {
                    SortOrder = SortDirection.Descending;
                }
                else
                {
                    SortOrder = SortDirection.Ascending;
                }
            }
            else
            {
                SortOrder = SortDirection.Ascending;
            }

            SortColumn = e.SortExpression;
            e.SortDirection = SortOrder;

            base.OnSorting(e);
        }

        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int sortColumnIndex = GetSortColumnIndex();

                if (sortColumnIndex != -1)
                {
                    AddSortImage(sortColumnIndex, e.Row);
                }


                if (e.Row.RowType == DataControlRowType.Header)
                {
                    /*
                    GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

                    TableCell left = new TableHeaderCell();
                    left.ColumnSpan = 3;
                    row.Cells.Add(left);

                    TableCell totals = new TableHeaderCell();
                    totals.ColumnSpan = this.Columns.Count - 3;
                    totals.Text = "Totals";
                    row.Cells.Add(totals);

                    this.InnerTable.Rows.AddAt(0, row);

                    */

                    GridViewRow row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

                    TableCell export = new TableHeaderCell();
                    if (this.AutoGenerateColumns == true)
                    {
                        DataTable dt = this.DataSource as DataTable;

                        if (null == dt)
                        {
                            DataView dv = this.DataSource as DataView;

                            if (null != dv)
                            {
                                dt = dv.ToTable();
                            }
                        }

                        if (null != dt)
                        {
                            export.ColumnSpan = dt.Columns.Count;
                        }

                    }
                    else
                    {
                        export.ColumnSpan = this.Columns.Count;
                    }
                    export.CssClass = "rightAlignHeader";
                    export.HorizontalAlign = HorizontalAlign.Right;

                    LinkButton lnkCSV = new LinkButton();
                    lnkCSV.Text = "";
                    lnkCSV.CommandName = "CSV";
                    lnkCSV.CommandArgument = "CSV";
                    lnkCSV.CssClass = "exportCSVButton";
                    lnkCSV.ToolTip = "Export to CSV";
                    lnkCSV.Click += new EventHandler(ExportCSVButton_Click);
                    export.Controls.Add(lnkCSV);

                    LinkButton lnkXL = new LinkButton();
                    lnkXL.Text = "";
                    lnkXL.CommandName = "Xl";
                    lnkXL.CommandArgument = "Xl";
                    lnkXL.CssClass = "exportExcelButton";
                    lnkXL.ToolTip = "Export to Excel";
                    lnkXL.Click += new EventHandler(ExportButton_Click);
                    export.Controls.Add(lnkXL);

                    try
                    {
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lnkXL);
                        ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lnkCSV);
                    }
                    catch (Exception ex)
                    {
                        //throw;
                        SendMail.MailMessage("ExcelPackage > Error > OnRowCreated(GridViewRowEventArgs e)", ex.ToString());
                    }


                    row.Cells.Add(export);

                    Table tbl = (Table)this.Controls[0];

                    if ((string.IsNullOrEmpty(ExportTemplatePath) == false) && (string.IsNullOrEmpty(ExportTemplate) == false) && (string.IsNullOrEmpty(DBColumn) == false) & (string.IsNullOrEmpty(ExcelColumn) == false))
                    {
                        tbl.Rows.AddAt(0, row);
                    }
                }




            }

            base.OnRowCreated(e);
        }



        #region Gets the column index for current sorted column
        /// <summary>
        /// Gets the column index for current sorted column
        /// </summary>
        /// <returns></returns>
        private int GetSortColumnIndex()
        {
            foreach (DataControlField field in Columns)
            {
                if (string.Equals(field.SortExpression, SortColumn))
                {
                    return Columns.IndexOf(field);
                }
            }
            return -1;
        }
        #endregion

        #region Create the sorting image based on the sort direction.
        /// <summary>
        /// Create the sorting image based on the sort direction.
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="headerRow"></param>
        private void AddSortImage(int columnIndex, GridViewRow headerRow)
        {

            // Create the sorting image based on the sort direction.

            Image sortImage = new Image();
            Literal padding = new Literal();
            padding.Text = "&nbsp;";

            if (SortOrder == SortDirection.Descending)
            {
                sortImage.ImageUrl = SortDescImageUrl;

                sortImage.AlternateText = "";
            }
            else
            {
                sortImage.ImageUrl = SortAscImageUrl;

                sortImage.AlternateText = "";
            }

            // Add the image to the appropriate header cell with a space as padding.
            headerRow.Cells[columnIndex].Controls.Add(padding);
            headerRow.Cells[columnIndex].Controls.Add(sortImage);
        }
        #endregion

        protected virtual void CreateCustomPager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
        {
            string strPagerClass = PagerStyle.CssClass.ToString();

            int pageCount = pagedDataSource.PageCount;
            int pageIndex = pagedDataSource.CurrentPageIndex + 1;
            int pageButtonCount = 5; // PagerSettings.PageButtonCount;

            TableCell cell = new TableCell();
            row.Cells.Add(cell);

            if (string.IsNullOrEmpty(strPagerClass) == false)
            {
                cell.Attributes["class"] = strPagerClass;

            }

            if (columnSpan > 1)
            {
                cell.Style.Add("padding", "0");
                cell.ColumnSpan = columnSpan;
            }

            if (pageCount > 1)
            {
                //gridFooter

                //<table cellpadding="0" cellspacing="0"  width="100%" class="gridFooterWrapper">
                //<tr>
                //    <td class="gridFooterLeft">&nbsp;</td>
                //    <td class="gridFooterNewCell">

                // grid footer

                //</td>
                //    <td class="gridFooterRight">&nbsp;</td>
                //</tr>
                //</table>

                HtmlGenericControl gridFooterWrapper = new HtmlGenericControl("table");
                gridFooterWrapper.Attributes["cellpadding"] = "0";
                gridFooterWrapper.Attributes["cellspacing"] = "0";
                gridFooterWrapper.Attributes["width"] = "100%";
                gridFooterWrapper.Attributes["class"] = "gridFooterWrapper";
                cell.Controls.Add(gridFooterWrapper);

                HtmlGenericControl gridFooterWrapper_tr = new HtmlGenericControl("tr");
                gridFooterWrapper.Controls.Add(gridFooterWrapper_tr);


                HtmlGenericControl gridFooterLeft = new HtmlGenericControl("td");
                gridFooterLeft.InnerHtml = "&nbsp;";
                gridFooterLeft.Attributes["class"] = "gridFooterLeft";
                //gridFooterWrapper_tr.Controls.Add(gridFooterLeft);

                HtmlGenericControl gridFooterNewCell = new HtmlGenericControl("td");
                gridFooterNewCell.Attributes["class"] = "gridFooterNewCell";
                gridFooterWrapper_tr.Controls.Add(gridFooterNewCell);

                HtmlGenericControl gridFooterRight = new HtmlGenericControl("td");
                gridFooterRight.InnerHtml = "&nbsp;";
                gridFooterRight.Attributes["class"] = "gridFooterRight";
                //gridFooterWrapper_tr.Controls.Add(gridFooterRight);

                HtmlGenericControl gridFooter = new HtmlGenericControl("div");
                gridFooter.Attributes["class"] = "gridFooterNew";
                //cell.Controls.Add(gridFooter);             
                gridFooterNewCell.Controls.Add(gridFooter);

                HtmlGenericControl pageInfo = new HtmlGenericControl("div");
                pageInfo.Attributes["class"] = "itemNo";
                pageInfo.InnerHtml = PageInfo(pagedDataSource.DataSourceCount);
                gridFooter.Controls.Add(pageInfo);

                /*
                #region Create Export
                lnk = new LinkButton();
                lnk.Text = "Export";
                lnk.CommandName = "Export";
                lnk.CommandArgument = "Excel";
                lnk.Click += new EventHandler(ExportButton_Click);
                //pageNumbers.Controls.Add(lnk);
                gridFooter.Controls.Add(lnk);
                #endregion

                try
                {
                    ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lnk);
                }
                catch
                {
                }
                */

                HtmlGenericControl pager = new HtmlGenericControl("div");
                pager.Attributes["class"] = "gridPagination";
                gridFooter.Controls.Add(pager);

                int min = pageIndex - pageButtonCount;
                int max = pageIndex + pageButtonCount;

                if (max > pageCount)
                {
                    // min -= max - pageCount;

                    min = min - (max - pageCount);


                }
                else if (min < 1)
                {
                    // max += 1 - min;
                    max = max + (1 - min);
                }

                if (pageCount > 1)
                {
                    HtmlGenericControl pageNumbers = new HtmlGenericControl("ul");
                    pager.Controls.Add(pageNumbers);

                    #region Create previous button
                    HtmlGenericControl previousContiner = new HtmlGenericControl("li");
                    pageNumbers.Controls.Add(previousContiner);

                    Control previousButton = null;
                    if (pageIndex > 1)
                    {
                        previousContiner.Attributes["class"] = "previousPageEnabled";
                        previousButton = BuildLinkButton(pageIndex - 2, "&nbsp;", "Page", "Prev");
                        previousContiner.Controls.Add(previousButton);
                    }
                    else
                    {
                        previousContiner.Attributes["class"] = "previousPageDisabled";
                        //previousButton = BuildSpan(PagerSettings.PreviousPageText, "disabled");
                    }
                    #endregion


                    #region Create page buttons

                    Control page = null;
                    bool needDiv = false;
                    for (int i = 1; i <= pageCount; i++)
                    {
                        if (i <= 2 || i > pageCount - 2 || (min <= i && i <= max))
                        {
                            HtmlGenericControl pageNumberContiner = new HtmlGenericControl("li");
                            pageNumbers.Controls.Add(pageNumberContiner);

                            string text = i.ToString(NumberFormatInfo.InvariantInfo);
                            if (i == pageIndex)
                            {
                                pageNumberContiner.Attributes["class"] = "activePage";
                                page = BuildSpan(text, "activePage");
                            }
                            else
                            {
                                pageNumberContiner.Attributes["class"] = "pages";
                                page = BuildLinkButton(i - 1, text, "Page", text);
                            }

                            pageNumberContiner.Controls.Add(page);

                            needDiv = true;
                        }
                        else if (needDiv)
                        {

                            HtmlGenericControl pageNumberContiner = new HtmlGenericControl("li");
                            pageNumbers.Controls.Add(pageNumberContiner);

                            pageNumberContiner.Attributes["class"] = "";
                            page = BuildSpan("...", "");
                            pageNumberContiner.Controls.Add(page);

                            //page = BuildSpan("&hellip;", null);
                            //pageNumbers.Controls.Add(page);
                            needDiv = false;
                        }
                    }
                    #endregion

                    #region Create next button
                    HtmlGenericControl nextContiner = new HtmlGenericControl("li");
                    pageNumbers.Controls.Add(nextContiner);

                    Control nextButton = null;
                    if (pageIndex < pageCount)
                    {
                        nextContiner.Attributes["class"] = "nextPageEnabled";
                        nextButton = BuildLinkButton(pageIndex, "&nbsp;", "Page", "Next");
                        nextContiner.Controls.Add(nextButton);
                    }
                    else
                    {
                        nextContiner.Attributes["class"] = "nextPageDisabled";
                        //previousButton = BuildSpan(PagerSettings.PreviousPageText, "disabled");
                    }
                    /*

                    page = pageIndex < pageCount
                            ? BuildLinkButton(pageIndex, PagerSettings.NextPageText, "Page", "Next")
                            : BuildSpan(PagerSettings.NextPageText, "disabled");
                    pageNumbers.Controls.Add(page);
                    */
                    #endregion

                }
            }
        }

        private Control BuildLinkButton(int pageIndex, string text, string commandName, string commandArgument)
        {
            PagerLinkButton link = new PagerLinkButton(this);
            link.Text = text;
            link.EnableCallback(ParentBuildCallbackArgument(pageIndex));
            link.CommandName = commandName;
            link.CommandArgument = commandArgument;
            return link;
        }

        private Control BuildSpan(string text, string cssClass)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            if (!String.IsNullOrEmpty(cssClass)) span.Attributes["class"] = cssClass;
            span.InnerHtml = text;
            return span;
        }

        private string ParentBuildCallbackArgument(int pageIndex)
        {
            MethodInfo m =
                typeof(GridView).GetMethod("BuildCallbackArgument", BindingFlags.NonPublic | BindingFlags.Instance, null,
                                            new Type[] { typeof(int) }, null);
            return (string)m.Invoke(this, new object[] { pageIndex });
        }

        protected string PageInfo(int rowCount)
        {
            string strRetVal = string.Empty;

            int currentPageFirstRow = ((PageIndex * PageSize) + 1);
            int currentPageLastRow = 0;
            int lastPageRemainder = (rowCount % PageSize);

            // if you're on the last page the currentPageLastRow
            // may be different to the other pages
            currentPageLastRow = (PageCount == (PageIndex + 1) ?

            (currentPageFirstRow + lastPageRemainder - 1) : (currentPageFirstRow +
            PageSize - 1));

            strRetVal = String.Format("Displaying {0} to {1} of {2}", currentPageFirstRow, currentPageLastRow, rowCount);

            return strRetVal;

        }

        protected void ExportButton_Click(object sender, EventArgs e)
        {
            string strReportHtml = string.Empty;

            try
            {
                ExcelManager objExcelManager = new ExcelManager();
                //string strParametersPath = this.Page.Server.MapPath("../Reports/Templates/");

                // Pass the data and get the path for generated temporary file.

                // DataTable vdtData, string vstrfilePath, string vstrExportCaption, string vstrDBColumn, string vstrExcelColumn)


                if (null != ViewState["DataId"])
                {
                    string strRand = ViewState["DataId"].ToString();

                    if (null != this.Page.Session[strRand])
                    {

                        DataTable dt = this.Page.Session[strRand] as DataTable;

                        if ((null != dt) & (string.IsNullOrEmpty(ExportTemplatePath) == false) && (string.IsNullOrEmpty(ExportTemplate) == false) && (string.IsNullOrEmpty(DBColumn) == false) & (string.IsNullOrEmpty(ExcelColumn) == false))
                        {
                            string strFilePath = objExcelManager.GenerateExcelDataFileFromGrid(dt, this.Page.Server.MapPath(ExportTemplatePath), ExportTemplate, ExportCaption, DBColumn, ExcelColumn,
                                ExcelHeaderRow, StartRow, StartColumn, MaxLevel, SheetNumber, CurrentDateRow, CurrentDateCol, StartDateRow, StartDateCol, EndDateRow, EndDateCol, ReportStartDate, ReportEndDate);


                            System.IO.FileInfo objfInfo = new System.IO.FileInfo(strFilePath);
                            if (objfInfo.Exists)
                            {
                                this.Page.Response.Clear();
                                string attachment = "attachment; filename=" + objfInfo.Name;

                                this.Page.Response.AddHeader("content-disposition", attachment);
                                //if (vExportType == Constants.ExportType.Excel)
                                //{
                                this.Page.Response.ContentType = "application/vnd.ms-excel";
                                //}
                                //else
                                //{
                                //    Response.ContentType = "text/csv";
                                //}

                                this.Page.Response.BinaryWrite(System.IO.File.ReadAllBytes(strFilePath));

                                objfInfo.Delete(); // Once the file content is written to response stream delete it.
                                //Response.End();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                SendMail.MailMessage("ExcelPackage > Error > ExportButton_Click(object sender, EventArgs e)", ex.ToString());
            }
            finally
            {
                this.Page.Response.End();
            }

        }

        protected void ExportCSVButton_Click(object sender, EventArgs e)
        {
            string strReportHtml = string.Empty;

            try
            {
                ExcelManager objExcelManager = new ExcelManager();
                //string strParametersPath = this.Page.Server.MapPath("../Reports/Templates/");

                // Pass the data and get the path for generated temporary file.

                // DataTable vdtData, string vstrfilePath, string vstrExportCaption, string vstrDBColumn, string vstrExcelColumn)


                if (null != ViewState["DataId"])
                {
                    string strRand = ViewState["DataId"].ToString();

                    if (null != this.Page.Session[strRand])
                    {

                        DataTable dt = this.Page.Session[strRand] as DataTable;

                        if ((null != dt) & (string.IsNullOrEmpty(ExportTemplatePath) == false) && (string.IsNullOrEmpty(ExportTemplate) == false) && (string.IsNullOrEmpty(DBColumn) == false) & (string.IsNullOrEmpty(ExcelColumn) == false))
                        {
                            /*string strFilePath = objExcelManager.GenerateExcelDataFileFromGrid(dt, this.Page.Server.MapPath(ExportTemplatePath), ExportTemplate, ExportCaption, DBColumn, ExcelColumn,
                                ExcelHeaderRow, StartRow, StartColumn, MaxLevel, SheetNumber, CurrentDateRow, CurrentDateCol, StartDateRow, StartDateCol, EndDateRow, EndDateCol, ReportStartDate, ReportEndDate);*/

                            string strFilePath = objExcelManager.GenerateCSVFileFromGrid(dt, this.Page.Server.MapPath(ExportTemplatePath), ExportTemplate, ExportCaption, DBColumn, ExcelColumn,
                                ExcelHeaderRow, StartRow, StartColumn, MaxLevel, SheetNumber, CurrentDateRow, CurrentDateCol, StartDateRow, StartDateCol, EndDateRow, EndDateCol, ReportStartDate, ReportEndDate);
                            System.IO.FileInfo objfInfo = new System.IO.FileInfo(strFilePath);
                            if (objfInfo.Exists)
                            {
                                this.Page.Response.Clear();
                                string attachment = "attachment; filename=" + objfInfo.Name;

                                this.Page.Response.AddHeader("content-disposition", attachment);
                                //if (vExportType == Constants.ExportType.Excel)
                                //{
                                this.Page.Response.ContentType = "text/csv";
                                //}
                                //else
                                //{
                                //    Response.ContentType = "text/csv";
                                //}

                                this.Page.Response.BinaryWrite(System.IO.File.ReadAllBytes(strFilePath));

                                objfInfo.Delete(); // Once the file content is written to response stream delete it.
                                //Response.End();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //throw;
                SendMail.MailMessage("ExcelPackage > Error > ExportCSVButton_Click(object sender, EventArgs e)", ex.ToString());
            }
            finally
            {
                this.Page.Response.End();
            }

        }

    }
}