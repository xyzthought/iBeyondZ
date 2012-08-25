using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.BusinessObject
{
    [Serializable]
    public class PageInfo
    {
        #region Private Members

        string mstrSearchText;
        string mstrSortColumnName;
        string mstrSortDirection;
        int mintPageNo;
        int mintPageSize;
        int mintTotalRecord;
        string mstrDisplayingRecords;
        int mintPageButtonCount;
        string mstrRelativeImagePath;
        string mstrFilter;
        string mstrAlphaSearch;

        #endregion Private Members

        #region Public Properties

        public string SearchText
        {
            get { return mstrSearchText; }
            set { mstrSearchText = value; }
        }

        public string SortColumnName
        {
            get { return mstrSortColumnName; }
            set { mstrSortColumnName = value; }
        }

        public string SortDirection
        {
            get { return mstrSortDirection; }
            set { mstrSortDirection = value; }
        }

        public int PageNo
        {
            get { return mintPageNo; }
            set { mintPageNo = value; }
        }

        public int PageSize
        {
            get { return mintPageSize; }
            set { mintPageSize = value; }
        }

        public string DisplayingRecords
        {
            get { return mstrDisplayingRecords; }
            set { mstrDisplayingRecords = value; }
        }

        public int TotalRecord
        {
            get { return mintTotalRecord; }
            set { mintTotalRecord = value; }
        }

        public string RelativeImagePath
        {
            get { return mstrRelativeImagePath; }
            set { mstrRelativeImagePath = value; }
        }

        public int PageButtonCount
        {
            get { return mintPageButtonCount; }
            set { mintPageButtonCount = value; }
        }

        public string Filter
        {
            get { return mstrFilter; }
            set { mstrFilter = value; }
        }


        public string AlphaSearch
        {
            get { return mstrAlphaSearch; }
            set { mstrAlphaSearch = value; }
        }



        #endregion Private Properties
    }
}
