
using System;
using System.IO;
using System.Web;
using System.Configuration;
using System.Xml.Serialization;
using System.Collections.Generic;
using OfficeOpenXml;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Text;

namespace CustomExcel.Entity
{
    /// <summary>
    /// This class handles the functionality required to produce excel and csv exports.
    /// </summary>
    public class ExcelManager
    {
        # region Private Variables
        private string strTemplateFile;
        private int[,] styleIDs;

        private int mintHeaderRow;
        private int mintStartRow;
        private int mintStartColumn;
        private int mintEndColumn;
        private int mintMaxLevel;
        private int mintSheetNumber;
        private int mintTotalColumns;
        private int mintCurrentRow;
        private int mintCurrentColumn;
        private int mintDateResolution;
        private int mintCurrentDateRow;
        private int mintCurrentDateCol;

        private int mintStartDateRow;
        private int mintStartDateCol;
        private int mintEndDateRow;
        private int mintEndDateCol;

        private string mstrReportStartDate;
        private string mstrReportEndDate;

        # endregion

        # region Constructors
        public ExcelManager()
        {
        }
        # endregion

        # region Supporting Report

        /// <summary>
        /// Generates the excel files and returns the path of the generated file.
        /// </summary>
        /// <param name="vobjDR"></param>
        /// <param name="vstrfilePath"></param>
        /// <param name="vstrAdditionalParameters"></param>
        /// <returns> Path to the excel file</returns>
        public string GenerateXlFile(SqlDataReader vobjDR, string vstrfilePath, string vstrAdditionalParameters)
        {
            string strReportPath = vstrfilePath;
            string strfilePath = strReportPath + "ExportedFiles/" + DateTime.Now.Ticks + ".xlsx";

            string strTemplatePath = vstrfilePath + "export_template.xlsx";

            // Populate the essential member variables.
            GenerateReportObject(vstrfilePath);

            // Populate the additional member variables.
            PopulateAdditionalParameters(vstrAdditionalParameters);

            using (ExcelPackage objXlPackage = new ExcelPackage(new FileInfo(strfilePath), new FileInfo(strTemplatePath)))
            {
                ExcelWorksheet objSheet = objXlPackage.Workbook.Worksheets[mintSheetNumber];
                InitializeStyles(objSheet);
                string strTemp = string.Empty;

                if (vobjDR != null)
                {
                    mintTotalColumns = mintStartColumn + vobjDR.FieldCount;

                    InitializeStyles(objSheet);

                    // Generate report headers
                    string strColName = string.Empty;
                    for (int intColIdx = 0; intColIdx < vobjDR.FieldCount; intColIdx++)
                    {
                        strColName = vobjDR.GetName(intColIdx);
                        objSheet.Cell(mintHeaderRow, mintCurrentColumn).Value = strColName;

                        mintCurrentColumn++;
                    }

                    mintCurrentColumn = mintStartColumn;

                    // Generate report rows
                    while (vobjDR.Read())
                    {
                        strTemp = string.Empty;

                        for (int intIndex = 0; intIndex < vobjDR.FieldCount; intIndex++)
                        {
                            strTemp = string.Empty;
                            strTemp = vobjDR[intIndex].ToString();

                            strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.@/:_""])+", " ");

                            objSheet.Cell(mintCurrentRow, mintCurrentColumn).Value = strTemp;

                            mintCurrentColumn++;

                        }

                        SetColumnStyle(objSheet, 1);

                        mintCurrentRow++;
                        mintCurrentColumn = mintStartColumn;
                    }

                }


                //Populate Custom Values
                if (!string.IsNullOrEmpty(mstrReportStartDate))
                {
                    objSheet.Cell(mintStartDateRow, mintStartDateCol).Value = mstrReportStartDate;
                }
                if (!string.IsNullOrEmpty(mstrReportEndDate))
                {
                    objSheet.Cell(mintEndDateRow, mintEndDateCol).Value = mstrReportEndDate;
                }

                objSheet.Cell(mintCurrentDateRow, mintCurrentDateCol).Value = System.DateTime.Now.ToString("MM/dd/yyyy");

                objXlPackage.Save();
            }

            return strfilePath;
        }



        public string GenerateExcelDataFile(DataTable vdtData, string vstrfilePath, string[] vstrReportParameters, string[] vstrAdditionalParameters)
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.Load(vstrfilePath + "parameters.xml");
            string strReportName = string.Empty;
            string strTemplateName = string.Empty;
            string[] strDBColumn = null;
            string[] strExcelColumn = null;
            if (null != objXmlDoc)
            {
                XmlNode objNode = null;
                objNode = objXmlDoc.SelectSingleNode("//parameters/parameter[@rid='" + vstrReportParameters[0] + "']");
                if (null != objNode)
                {
                    strReportName = objNode.Attributes["caption"].Value.ToString();
                    strDBColumn = objNode.Attributes["DBColumn"].Value.ToString().Split(';');
                    strExcelColumn = objNode.Attributes["ExcelColumn"].Value.ToString().Split(';');
                    strTemplateName = objNode.Attributes["template"].Value.ToString();
                }
            }



            string strReportPath = vstrfilePath;
            string strfilePath = strReportPath + "ExportedFiles/" + DateTime.Now.Ticks + ".xlsx";

            string strTemplatePath = vstrfilePath + strTemplateName;

            // Populate the essential member variables.
            GenerateReportObject(vstrfilePath);

            // Populate the additional member variables.
            PopulateAdditionalParameters2(vstrAdditionalParameters);

            using (ExcelPackage objXlPackage = new ExcelPackage(new FileInfo(strfilePath), new FileInfo(strTemplatePath)))
            {
                ExcelWorksheet objSheet = objXlPackage.Workbook.Worksheets[mintSheetNumber];
                InitializeStyles(objSheet);
                string strTemp = string.Empty;

                if (vdtData != null)
                {
                    mintTotalColumns = mintStartColumn + strDBColumn.Length;

                    InitializeStyles(objSheet);

                    // Generate report headers
                    string strColName = string.Empty;
                    for (int intColIdx = 0; intColIdx < strDBColumn.Length; intColIdx++)
                    {
                        objSheet.Cell(mintHeaderRow, mintCurrentColumn).Value = strExcelColumn[intColIdx];
                        mintCurrentColumn++;
                    }

                    mintCurrentColumn = mintStartColumn;

                    // Generate report rows
                    for (int i = 0; i < vdtData.Rows.Count; i++)
                    {
                        strTemp = string.Empty;
                        DataRow drData = vdtData.Rows[i];
                        for (int intIndex = 0; intIndex < strDBColumn.Length; intIndex++)
                        {
                            strTemp = string.Empty;
                            strTemp = drData[strDBColumn[intIndex].ToString()].ToString();

                            strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.@/:_""])+", " ");

                            objSheet.Cell(mintCurrentRow, mintCurrentColumn).Value = strTemp;

                            mintCurrentColumn++;

                        }

                        SetColumnStyle(objSheet, 1);

                        mintCurrentRow++;
                        mintCurrentColumn = mintStartColumn;
                    }

                }

                //Populate Custom Values
                objSheet.Cell(2, 2).Value = strReportName;
                objSheet.Cell(2, 3).Value = vstrReportParameters[1].ToString();


                if (!string.IsNullOrEmpty(mstrReportStartDate))
                {
                    objSheet.Cell(mintStartDateRow, mintStartDateCol).Value = mstrReportStartDate;
                }
                if (!string.IsNullOrEmpty(mstrReportEndDate))
                {
                    objSheet.Cell(mintEndDateRow, mintEndDateCol).Value = mstrReportEndDate;
                }

                objSheet.Cell(mintCurrentDateRow, mintCurrentDateCol).Value = System.DateTime.Now.ToString("MM/dd/yyyy");

                objXlPackage.Save();
            }

            return strfilePath;
        }

        public string GenerateExcelDataFileFromGrid(DataTable vdtData, string vstrfilePath, string vstrExportTemplate, string vstrExportCaption, string vstrDBColumn, string vstrExcelColumn,
            int vintHeaderRow,
            int vintStartRow,
            int vintStartColumn,
            int vintMaxLevel,
            int vintSheetNumber,
            int vintCurrentDateRow,
            int vintCurrentDateCol,
            int vintStartDateRow,
            int vintStartDateCol,
            int vintEndDateRow,
            int vintEndDateCol,
            string vstrReportStartDate,
            string vstrReportEndDate
            )
        {
            string strReportName = string.Empty;
            string[] strDBColumn = null;
            string[] strExcelColumn = null;

            string strReportPath = vstrfilePath;
            string strfilePath = strReportPath + "ExportedFiles/" + vstrExportCaption + "_" + DateTime.Now.Ticks + ".xlsx";

            string strTemplatePath = vstrfilePath + vstrExportTemplate;

            strReportName = vstrExportCaption;
            strDBColumn = vstrDBColumn.Split(';');
            strExcelColumn = vstrExcelColumn.Split(';');

            /*
            mintHeaderRow = 8;
            mintStartRow =10;
            mintStartColumn =2;
            mintMaxLevel =1;
            mintSheetNumber =1;
            mintCurrentDateRow = 6;
            mintCurrentDateCol = 3;
            mintStartDateRow = 4;
            mintStartDateCol = 3;
            mintEndDateRow = 5;
            mintEndDateCol = 3;
            mstrReportStartDate = "01/01/2010";
            mstrReportEndDate = "01/01/2020";
            */

            mintHeaderRow = vintHeaderRow;
            mintStartRow = vintStartRow;
            mintStartColumn = vintStartColumn;
            mintMaxLevel = vintMaxLevel;
            mintSheetNumber = vintSheetNumber;
            mintCurrentDateRow = vintCurrentDateRow;
            mintCurrentDateCol = vintCurrentDateCol;
            mintStartDateRow = vintStartDateRow;
            mintStartDateCol = vintStartDateCol;
            mintEndDateRow = vintEndDateRow;
            mintEndDateCol = vintEndDateCol;
            mstrReportStartDate = vstrReportStartDate;
            mstrReportEndDate = vstrReportEndDate;

            mintCurrentRow = mintStartRow;
            mintCurrentColumn = mintStartColumn;

            using (ExcelPackage objXlPackage = new ExcelPackage(new FileInfo(strfilePath), new FileInfo(strTemplatePath)))
            {
                ExcelWorksheet objSheet = objXlPackage.Workbook.Worksheets[mintSheetNumber];
                InitializeStyles(objSheet);
                string strTemp = string.Empty;

                if (vdtData != null)
                {
                    mintTotalColumns = mintStartColumn + strDBColumn.Length;

                    InitializeStyles(objSheet);

                    // Generate report headers
                    string strColName = string.Empty;
                    for (int intColIdx = 0; intColIdx < strDBColumn.Length; intColIdx++)
                    {
                        objSheet.Cell(mintHeaderRow, mintCurrentColumn).Value = strExcelColumn[intColIdx];
                        mintCurrentColumn++;
                    }

                    mintCurrentColumn = mintStartColumn;

                    // Generate report rows
                    for (int i = 0; i < vdtData.Rows.Count; i++)
                    {
                        strTemp = string.Empty;
                        DataRow drData = vdtData.Rows[i];
                        for (int intIndex = 0; intIndex < strDBColumn.Length; intIndex++)
                        {
                            strTemp = string.Empty;
                            strTemp = drData[strDBColumn[intIndex].ToString()].ToString();

                            strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.@/:_""])+", " ");

                            objSheet.Cell(mintCurrentRow, mintCurrentColumn).Value = strTemp;

                            mintCurrentColumn++;

                        }

                        SetColumnStyle(objSheet, 1);

                        mintCurrentRow++;
                        mintCurrentColumn = mintStartColumn;
                    }

                }

                //Populate Custom Values
                if (strReportName.IndexOf("-") > -1)
                {
                    string[] arrReportName = strReportName.Split('-');

                    objSheet.Cell(2, 2).Value = arrReportName[0];
                    objSheet.Cell(2, 3).Value = arrReportName[1];
                }
                else
                {
                    objSheet.Cell(2, 2).Value = strReportName;
                }


                if (!string.IsNullOrEmpty(mstrReportStartDate))
                {
                    objSheet.Cell(mintStartDateRow, mintStartDateCol).Value = mstrReportStartDate;
                }
                if (!string.IsNullOrEmpty(mstrReportEndDate))
                {
                    objSheet.Cell(mintEndDateRow, mintEndDateCol).Value = mstrReportEndDate;
                }

                objSheet.Cell(mintCurrentDateRow, mintCurrentDateCol).Value = System.DateTime.Now.ToString("MM/dd/yyyy");

                objXlPackage.Save();
            }

            return strfilePath;
        }



        /// <summary>
        /// Populate the member variables related to additional parameters line report start and end date etc...
        /// </summary>
        /// <param name="vstrAdditionalParameters"></param>
        private void PopulateAdditionalParameters(string vstrAdditionalParameters)
        {
            XmlDocument objXmlDoc = new XmlDocument();
            objXmlDoc.LoadXml(vstrAdditionalParameters);

            if (null != objXmlDoc)
            {
                XmlNode objNode = null;
                objNode = objXmlDoc.SelectSingleNode("//parameters/parameter[@name='StartDate']");
                if (null != objNode)
                {
                    mstrReportStartDate = objNode.Attributes["value"].Value.ToString();
                }

                objNode = objXmlDoc.SelectSingleNode("//parameters/parameter[@name='EndDate']");
                if (null != objNode)
                {
                    mstrReportEndDate = objNode.Attributes["value"].Value.ToString();
                }
            }
        }

        private void PopulateAdditionalParameters2(string[] vstrAdditionalParameters)
        {
            mstrReportStartDate = vstrAdditionalParameters[0];
            mstrReportEndDate = vstrAdditionalParameters[1];
        }


        /// <summary>
        /// This function generates the CSV file and returns the path for the same.
        /// </summary>
        /// <param name="vobjDR"></param>
        /// <param name="vstrParametersPath"></param>
        /// <param name="vstrAdditionalParameters"></param>
        /// <returns></returns>
        public string GenerateCSVFile(SqlDataReader vobjDR, string vstrParametersPath, string vstrAdditionalParameters)
        {
            StringBuilder objSbContent = new StringBuilder();
            string strRow = string.Empty;

            string strReportPath = vstrParametersPath;
            string strfilePath = strReportPath + "ExportedFiles/" + DateTime.Now.Ticks + ".csv";

            StreamWriter objWriter = null;
            FileStream objStream = null;

            try
            {
                objStream = File.Create(strfilePath);
                objWriter = new StreamWriter(objStream, Encoding.Default);

                string strTemp = string.Empty;

                if (vobjDR != null)
                {
                    // Generate report headers
                    string strColName = string.Empty;
                    strRow = string.Empty;
                    for (int intColIdx = 0; intColIdx < vobjDR.FieldCount; intColIdx++)
                    {
                        strColName = vobjDR.GetName(intColIdx);
                        strRow += strColName + ",";
                    }

                    objSbContent.AppendLine(strRow);

                    // Generate report rows
                    while (vobjDR.Read())
                    {
                        strTemp = string.Empty;
                        strRow = string.Empty;

                        for (int intIndex = 0; intIndex < vobjDR.FieldCount; intIndex++)
                        {
                            strTemp = string.Empty;
                            strTemp = vobjDR[intIndex].ToString();

                            //strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.""])+", " ");
                            strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.@/:_""])+", " ");

                            strRow += strTemp + ",";
                        }

                        objSbContent.AppendLine(strRow);
                    }
                }

                objWriter.Write(objSbContent.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objWriter.Flush();
                objWriter.Close();
                objStream.Close();
            }

            return strfilePath;
        }

        public string GenerateCSVFileFromGrid(DataTable vdtData, string vstrfilePath, string vstrExportTemplate, string vstrExportCaption, string vstrDBColumn, string vstrExcelColumn,
            int vintHeaderRow,
            int vintStartRow,
            int vintStartColumn,
            int vintMaxLevel,
            int vintSheetNumber,
            int vintCurrentDateRow,
            int vintCurrentDateCol,
            int vintStartDateRow,
            int vintStartDateCol,
            int vintEndDateRow,
            int vintEndDateCol,
            string vstrReportStartDate,
            string vstrReportEndDate)
        {
            StringBuilder objSbContent = new StringBuilder();
            string strRow = string.Empty;

            string strReportName = string.Empty;
            string[] strDBColumn = null;
            string[] strExcelColumn = null;

            string strReportPath = vstrfilePath;
            string strfilePath = strReportPath + "ExportedFiles/" + vstrExportCaption + "_" + DateTime.Now.Ticks + ".csv";

            strReportName = vstrExportCaption;
            strDBColumn = vstrDBColumn.Split(';');
            strExcelColumn = vstrExcelColumn.Split(';');

            StreamWriter objWriter = null;
            FileStream objStream = null;

            try
            {
                objStream = File.Create(strfilePath);
                objWriter = new StreamWriter(objStream, Encoding.Default);

                string strTemp = string.Empty;

                /*if (vobjDR != null)
                {
                    // Generate report headers
                    string strColName = string.Empty;
                    strRow = string.Empty;
                    for (int intColIdx = 0; intColIdx < vobjDR.FieldCount; intColIdx++)
                    {
                        strColName = vobjDR.GetName(intColIdx);
                        strRow += strColName + ",";
                    }

                    objSbContent.AppendLine(strRow);

                    // Generate report rows
                    while (vobjDR.Read())
                    {
                        strTemp = string.Empty;
                        strRow = string.Empty;

                        for (int intIndex = 0; intIndex < vobjDR.FieldCount; intIndex++)
                        {
                            strTemp = string.Empty;
                            strTemp = vobjDR[intIndex].ToString();

                            //strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.""])+", " ");
                            strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.@/:_""])+", " ");

                            strRow += strTemp + ",";
                        }

                        objSbContent.AppendLine(strRow);
                    }
                }*/

                if (vdtData != null)
                {
                    // Generate report headers
                    string strColName = string.Empty;
                    for (int intColIdx = 0; intColIdx < strDBColumn.Length; intColIdx++)
                    {
                        strColName = strExcelColumn[intColIdx];
                        //strRow += strColName + ",";
                        strRow += strColName;
                        if (intColIdx < (strDBColumn.Length - 1))
                        {
                            strRow += ",";
                        }
                    }
                    objSbContent.AppendLine(strRow);

                    // Generate report rows
                    for (int i = 0; i < vdtData.Rows.Count; i++)
                    {
                        strTemp = string.Empty;
                        strRow = string.Empty;

                        DataRow drData = vdtData.Rows[i];
                        for (int intIndex = 0; intIndex < strDBColumn.Length; intIndex++)
                        {
                            strTemp = string.Empty;
                            strTemp = drData[strDBColumn[intIndex].ToString()].ToString();

                            strTemp = System.Text.RegularExpressions.Regex.Replace(strTemp, @"([^a-zA-Z0-9\s.@/:_""])+", " ");

                            //strRow += strTemp + ",";
                            strRow += strTemp;
                            if (intIndex < (strDBColumn.Length - 1))
                            {
                                strRow += ",";
                            }
                        }

                        objSbContent.AppendLine(strRow);

                    }

                }

                objWriter.Write(objSbContent.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objWriter.Flush();
                objWriter.Close();
                objStream.Close();
            }

            return strfilePath;
        }
        # endregion

        # region Excel Properties
        /// <summary>
        /// Reads the passed xml file and populates member variables.
        /// </summary>
        /// <param name="vstrfilePath"></param>
        /// <returns>Returns true if successfully read. </returns>
        private bool GenerateReportObject(string vstrfilePath)
        {
            bool blnRetVal = true;

            string strTemplateParametersPath = vstrfilePath + "ExcelExportParameters.xml";

            XmlDocument objXDoc = new XmlDocument();
            XmlNode objNode = null;

            objXDoc.Load(strTemplateParametersPath);

            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='headerRow']");
            if (null != objNode)
            {
                mintHeaderRow = Convert.ToInt32(objNode.InnerText);
            }
            else
            {
                blnRetVal = false;
            }

            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='startRow']");
            if (null != objNode)
            {
                mintStartRow = Convert.ToInt32(objNode.InnerText);
                //startRow = 10;
            }
            else
            {
                blnRetVal = false;
            }

            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='startColumn']");
            if (null != objNode)
            {
                mintStartColumn = Convert.ToInt32(objNode.InnerText);
                //startColumn = 2;
            }
            else
            {
                blnRetVal = false;
            }

            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='maxLevel']");
            if (null != objNode)
            {
                mintMaxLevel = Convert.ToInt32(objNode.InnerText);
                //maxLevel = 1;    
            }
            else
            {
                blnRetVal = false;
            }

            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='currentDateRow']");
            if (null != objNode)
            {
                mintCurrentDateRow = Convert.ToInt32(objNode.InnerText);
            }
            else
            {
                blnRetVal = false;
            }

            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='currentDateCol']");
            if (null != objNode)
            {
                mintCurrentDateCol = Convert.ToInt32(objNode.InnerText);
            }
            else
            {
                blnRetVal = false;
            }

            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='sheetNumber']");
            if (null != objNode)
            {
                mintSheetNumber = Convert.ToInt32(objNode.InnerText);
                //sheetNumber = 1;
            }
            else
            {
                blnRetVal = false;
            }

            #region Additional Parameters
            //startDateRow
            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='startDateRow']");
            if (null != objNode)
            {
                mintStartDateRow = Convert.ToInt32(objNode.InnerText);
            }
            else
            {
                mintStartDateRow = -1;
            }

            //startDateCol
            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='startDateCol']");
            if (null != objNode)
            {
                mintStartDateCol = Convert.ToInt32(objNode.InnerText);
            }
            else
            {
                mintStartDateCol = -1;
            }

            //endDateRow
            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='endDateRow']");
            if (null != objNode)
            {
                mintEndDateRow = Convert.ToInt32(objNode.InnerText);
            }
            else
            {
                mintEndDateRow = -1;
            }

            //endDateCol
            objNode = objXDoc.SelectSingleNode("//parameters/parameter[@name='endDateCol']");
            if (null != objNode)
            {
                mintEndDateCol = Convert.ToInt32(objNode.InnerText);
            }
            else
            {
                mintEndDateCol = -1;
            }

            #endregion


            //totalColumns = endColumn - startColumn + 1;
            mintCurrentRow = mintStartRow;
            mintCurrentColumn = mintStartColumn;

            return blnRetVal;
        }


        /// <summary>
        /// Initialises the styles to be applied to this excel sheet.
        /// </summary>
        /// <param name="sheet"></param>
        private void InitializeStyles(ExcelWorksheet sheet)
        {
            styleIDs = new int[mintMaxLevel, mintTotalColumns];
            for (int intRow = 0; intRow < mintMaxLevel; intRow++)
            {
                for (int intCol = 0; intCol < mintTotalColumns; intCol++)
                    styleIDs[intRow, intCol] = sheet.Cell(mintStartRow + intRow, mintStartColumn + intCol).StyleID;
            }
        }

        /// <summary>
        /// Set style to columns.
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="level"></param>
        private void SetColumnStyle(ExcelWorksheet sheet, int level)
        {
            for (int intIdx = 0; intIdx < mintTotalColumns; intIdx++)
                sheet.Cell(mintCurrentRow, mintStartColumn + intIdx).StyleID = styleIDs[level - 1, intIdx];
        }
        # endregion
    }
}
