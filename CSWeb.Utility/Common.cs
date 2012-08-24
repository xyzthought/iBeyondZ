using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL.BusinessObject;

namespace CSWeb.Utility
{
    public class Common
    {
        public DataTable  GetConfigFileData(string vstrSourceTableName)
        {
            DataTable dtData = new DataTable();
            string strMailServerInfo = string.Empty;
            strMailServerInfo = @"xml\Config.xml";
            DataSet dsData = new DataSet();
            dsData.ReadXml(strMailServerInfo);
            if (null != dsData)
            {
                if (null != dsData.Tables[vstrSourceTableName])
                {
                    dtData = dsData.Tables[vstrSourceTableName];
                }
            }
            return dtData;
        }

       
    }
}
