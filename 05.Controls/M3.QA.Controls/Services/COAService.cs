#region Using

using System;
using System.Reflection;
using M3.QA.Models;
using NLib;
using OfficeOpenXml;

#endregion

namespace M3.QA
{
    public class COAService
    {
        #region COA 1

        public class COA1
        {
            public static void Export()
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;
                /*
                if (null == pcCard)
                    return;
                if (null == sheet || null == items)
                    return;
                */
                if (!ExcelExportUtils.CreateCOA1File(outputFile, true))
                {
                    M3QAApp.Windows.ExportFailed();
                    return;
                }

                try
                {
                    using (var package = new ExcelPackage(outputFile))
                    {
                        var ws = package.Workbook.Worksheets[0]; // check exists
                        if (null != ws)
                        {
                            #region Header
                            /*
                            string hdr = "Cord  production  appearance  check  sheet ( ใบตรวจเช็คเส้นด้ายของ S-9 ) ";
                            hdr += " Item Code : " + pcCard.ProductCode;
                            hdr += " Lot :  " + pcCard.DIPLotNo;
                            ws.Cells["A2"].Value = hdr;
                            // Date
                            string sDate = sheet.CheckDate.ToString("dd/MM/yyyy");
                            ws.Cells["AS2"].Value = " Date : " + sDate;
                            */
                            #endregion
                        }

                        package.Save();
                    }
                    M3QAApp.Windows.ExportSuccess();
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    M3QAApp.Windows.ExportFailed(ex.Message);
                }
            }
        }

        #endregion

        #region COA 4

        public class COA4
        {
            public static void Export()
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;
                /*
                if (null == pcCard)
                    return;
                if (null == sheet || null == items)
                    return;
                */
                if (!ExcelExportUtils.CreateCOA4File(outputFile, true))
                {
                    M3QAApp.Windows.ExportFailed();
                    return;
                }

                try
                {
                    using (var package = new ExcelPackage(outputFile))
                    {
                        var ws = package.Workbook.Worksheets[0]; // check exists
                        if (null != ws)
                        {
                            #region Header
                            /*
                            string hdr = "Cord  production  appearance  check  sheet ( ใบตรวจเช็คเส้นด้ายของ S-9 ) ";
                            hdr += " Item Code : " + pcCard.ProductCode;
                            hdr += " Lot :  " + pcCard.DIPLotNo;
                            ws.Cells["A2"].Value = hdr;
                            // Date
                            string sDate = sheet.CheckDate.ToString("dd/MM/yyyy");
                            ws.Cells["AS2"].Value = " Date : " + sDate;
                            */
                            #endregion
                        }

                        package.Save();
                    }
                    M3QAApp.Windows.ExportSuccess();
                }
                catch (Exception ex)
                {
                    med.Err(ex);
                    M3QAApp.Windows.ExportFailed(ex.Message);
                }
            }
        }

        #endregion
    }
}
