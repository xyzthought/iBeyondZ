using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Custom.WebGridViewControls.Pager
{
    /// <summary>
    /// Summary description for Pagination
    /// </summary>
    [ToolboxData("<{0}:Pagination runat=server></{0}:Pagination>")]
    public class Pagination : WebControl, IPostBackEventHandler
    {
        private int mintPageSize = 5;
        private int mintPageButtonCount = 5;
        private int mintCurrentPageIndex = 0;
        private int mintTotalResult = 10;
        private string mstrRelImagePath = "images/";
        private string mstrDisplayingRecords = String.Empty;

        public event PaginationClickedEventHandler PaginationClick;
        public delegate void PaginationClickedEventHandler(object sender, PaginationItemClickEventArgs e);

        protected virtual void OnClick(PaginationItemClickEventArgs e)
        {
            if (PaginationClick != null)
            {
                PaginationClick(this, e);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadFromViewState();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            int pageCount = (int)Math.Ceiling(TotalResult / (PageSize * 1.0));
            int midPageCount = (int)Math.Floor(PageButtonCount / (2 * 1.0));

            int startPage = CurrentPageNo - midPageCount;
            if (startPage <= 0)
                startPage = 1;

            int endPage = startPage + PageButtonCount - 1;
            if (endPage > pageCount)
                endPage = pageCount;

            startPage = endPage - PageButtonCount + 1;
            if (startPage <= 0)
                startPage = 1;

            int totalPage = endPage - startPage + 1;

            writer.AddAttribute(HtmlTextWriterAttribute.Class, "itemNo");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write(DisplayingRecords);
            writer.RenderEndTag();

            if (totalPage > 1)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Class, "gridPagination");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);

                writer.RenderBeginTag(HtmlTextWriterTag.Ul);

                #region To show the previous image link
                if (CurrentPageNo > 1)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "previousPageEnabled");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    int prevPageNo = CurrentPageNo - 1;
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, prevPageNo.ToString()));
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "prevNextPage");
                    writer.RenderBeginTag(HtmlTextWriterTag.A); //<a>
                    writer.Write("&nbsp;");
                    writer.RenderEndTag(); // </a>
                    writer.RenderEndTag(); // Li
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "previousPageDisabled");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.RenderEndTag();
                }
                #endregion To show the previous image link

                #region show page numbers with link
                for (int i = 0; i < totalPage; i++)
                {
                    int pageNo = startPage + i;
                    if (CurrentPageNo != pageNo)
                    {
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "pages");
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);

                        writer.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, pageNo.ToString()));
                        writer.RenderBeginTag(HtmlTextWriterTag.A); //<a>
                        writer.Write(pageNo.ToString());
                        writer.RenderEndTag();  //</a>

                        writer.RenderEndTag();
                    }
                    else
                    {
                        // current page
                        writer.AddAttribute(HtmlTextWriterAttribute.Class, "activePage");
                        writer.RenderBeginTag(HtmlTextWriterTag.Li);
                        writer.Write(pageNo.ToString());
                        writer.RenderEndTag();

                    }
                }
                #endregion show page numbers with link

                #region To show the next image link
                if (CurrentPageNo != pageCount)
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "nextPageEnabled");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    int nextPageNo = CurrentPageNo + 1;
                    writer.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackClientHyperlink(this, nextPageNo.ToString()));
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "prevNextPage");
                    writer.RenderBeginTag(HtmlTextWriterTag.A); //<a>
                    writer.Write("&nbsp;");
                    writer.RenderEndTag(); // </a>
                    writer.RenderEndTag(); // Li
                }
                else
                {
                    writer.AddAttribute(HtmlTextWriterAttribute.Class, "nextPageDisabled");
                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.RenderEndTag();
                }
                #endregion To show the next image link

                writer.RenderEndTag(); // Ul
                writer.RenderEndTag(); // Div

                /* SAMPLE OUTPUT
                    <div class="itemNo">
                        Displaying 1 to 2 of 100
                    </div>
                    <div class="gridPagination">
                        <ul>
                            <li class="previousPageDisabled" />
                            <li class="activePage">1</li>
                            <li class="pages">
                                <a href="javascript:__doPostBack('pgnCampaign','2')">2</a>
                            </li>
                            <li class="pages">
                                <a href="javascript:__doPostBack('pgnCampaign','3')">3</a>
                            </li>
                            <li class="nextPageEnabled" />
                        </ul>
                    </div>
                END SAMPLE OUTPUT */
            }
            else
            {
                this.Visible = false;
            }
        }

        private void LoadFromViewState()
        {
            object objData = ViewState["TotalResult"];
            if (!object.Equals(objData, null))
            {
                mintTotalResult = (int)objData;
            }

            objData = ViewState["PageSize"];
            if (!object.Equals(objData, null))
            {
                mintPageSize = (int)objData;
            }

            objData = ViewState["PageButtonCount"];
            if (!object.Equals(objData, null))
            {
                mintPageButtonCount = (int)objData;
            }

            objData = ViewState["CurrentPageIndex"];
            if (!object.Equals(objData, null))
            {
                mintCurrentPageIndex = (int)objData;
            }

            objData = ViewState["RelativeImagePath"];
            if (!object.Equals(objData, null))
            {
                mstrRelImagePath = (string)objData;
            }

            objData = ViewState["DisplayingRecords"];
            if (!object.Equals(objData, null))
            {
                mstrDisplayingRecords = (string)objData;
            }
        }

        # region Properties
        /// <summary>
        /// Gets or sets the total result.
        /// </summary>
        /// <value>The total result.</value>
        public int TotalResult
        {
            get { return mintTotalResult; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Value must be a positive number.");
                }

                mintTotalResult = value;
                if (this.EnableViewState)
                {
                    ViewState["TotalResult"] = mintTotalResult;
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public int PageSize
        {
            get { return mintPageSize; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("Value must be a positive number and greater than 0.");
                }

                mintPageSize = value;
                if (this.EnableViewState)
                {
                    ViewState["PageSize"] = mintPageSize;
                }
            }
        }

        /// <summary>
        /// Gets or sets the page button count.
        /// </summary>
        /// <value>The page button count.</value>
        public int PageButtonCount
        {
            get { return mintPageButtonCount; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Value must be a positive number and greater than 0.");
                }

                mintPageButtonCount = value;
                if (this.EnableViewState)
                {
                    ViewState["PageButtonCount"] = mintPageButtonCount;
                }
            }
        }

        /// <summary>
        /// Gets the current page no.
        /// </summary>
        /// <value>The current page no.</value>
        private int CurrentPageNo
        {
            get { return CurrentPageIndex + 1; }
        }

        /// <summary>
        /// Gets or sets the index of the current page.
        /// </summary>
        /// <value>The index of the current page.</value>
        public int CurrentPageIndex
        {
            get { return mintCurrentPageIndex; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Value must be a positive number.");
                }

                mintCurrentPageIndex = value;
                if (this.EnableViewState)
                {
                    ViewState["CurrentPageIndex"] = mintCurrentPageIndex;
                }
            }
        }

        /// <summary>
        /// Gets or sets the relative image path.
        /// </summary>
        /// <value>The relative image path.</value>
        public string RelativeImagePath
        {
            get { return mstrRelImagePath; }
            set
            {
                mstrRelImagePath = value;
                if (this.EnableViewState)
                {
                    ViewState["RelativeImagePath"] = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the disply record string (Ex: Displaying 11 to 20 of 50)
        /// </summary>
        /// <value>The displaying records details</value>
        public string DisplayingRecords
        {
            get { return mstrDisplayingRecords; }
            set
            {
                mstrDisplayingRecords = value;
                if (this.EnableViewState)
                {
                    ViewState["DisplayingRecords"] = value;
                }
            }
        }
        # endregion

        # region IPostBackEventHandler Members
        public void RaisePostBackEvent(string eventArgument)
        {
            OnClick(new PaginationItemClickEventArgs(Convert.ToInt32(eventArgument)));
        }
        # endregion
    }

    public class PaginationItemClickEventArgs : EventArgs
    {
        private int pageIndex = 0;

        public PaginationItemClickEventArgs(int pageNo)
        {
            pageIndex = pageNo - 1;
        }

        /// <summary>
        /// Gets the index of the page.
        /// </summary>
        /// <value>The index of the page.</value>
        public int PageIndex
        {
            get { return pageIndex; }
        }
    }
}
