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
            public static void Export(CordProduction value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;

                if (null == value)
                    return;

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
                            #region Write Cells

                            // DATE
                            ws.Cells["I7"].Value = (value.InputDate.HasValue) ? 
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B11"].Value = value.UserName;
                            // PRODUCT
                            ws.Cells["B12"].Value = value.ProductName;
                            // ITEM CODE
                            ws.Cells["B13"].Value = value.ItemCode;
                            // YARN TYPE
                            ws.Cells["B14"].Value = value.YarnCode;
                            // LOT NO
                            ws.Cells["B15"].Value = value.LotNo;
                            // PI NO 
                            ws.Cells["B16"].Value = value.PiNoSL;

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
            public static void Export(CordProduction value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;

                if (null == value)
                    return;

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
