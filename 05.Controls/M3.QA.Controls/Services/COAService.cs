#region Using

using System;
using System.Reflection;
using M3.QA.Models;
using NLib;
using OfficeOpenXml;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

#endregion

namespace M3.QA
{
    public class COAService
    {
        #region COA 1

        public class COA1
        {
            private static void WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                if (null != ws && null != p && null != p.Spec)
                {
                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = "(" + p.Spec.UnitReport + ")";
                    // SPEC
                    ws.Cells["D" + iRow.ToString()].Value = p.Spec.ReportSpec;
                    // RESULT
                    ws.Cells["E" + iRow.ToString()].Value = p.Avg;
                    // JUDGE
                    ws.Cells["G" + iRow.ToString()].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                    // Test Method
                    ws.Cells["H" + iRow.ToString()].Value = p.Spec.TestMethod;
                }
            }

            public static void Export(CordProduction value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;

                if (null == value)
                    return;

                if (null != value)
                {
                    value.LoadProperties();
                }

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

                            #region Write each properties

                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            WriteProperty(ws, 20, p);
                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            WriteProperty(ws, 21, p);
                            // ELONG AT LOAD (PropertyNo = 3)
                            p = value.Properties.FindByPropertyNo(3);
                            WriteProperty(ws, 22, p);
                            // NO OF TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            WriteProperty(ws, 23, p);
                            // CORD GAUGE (PropertyNo = 9)
                            p = value.Properties.FindByPropertyNo(9);
                            WriteProperty(ws, 24, p);
                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            WriteProperty(ws, 25, p);
                            // CORD SIZE (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            WriteProperty(ws, 26, p);
                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            WriteProperty(ws, 27, p);
                            // RPU (PropertyNo = 12)
                            p = value.Properties.FindByPropertyNo(12);
                            WriteProperty(ws, 28, p);
                            // ADHESION FORCE (PEEL) (PropertyNo = 4)
                            p = value.Properties.FindByPropertyNo(4);
                            WriteProperty(ws, 29, p);

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

        #region COA 2

        public class COA2
        {
            private static void WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                if (null != ws && null != p && null != p.Spec)
                {
                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = "(" + p.Spec.UnitReport + ")";
                    // SPEC
                    ws.Cells["D" + iRow.ToString()].Value = p.Spec.ReportSpec;

                    // RESULT 1-5
                    int iCnt = 1;
                    foreach (var test in p.Tests)
                    {
                        if (iCnt == 1)
                        {
                            ws.Cells["G" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 2)
                        {
                            ws.Cells["H" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 3)
                        {
                            ws.Cells["I" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 4)
                        {
                            ws.Cells["J" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 5)
                        {
                            ws.Cells["K" + iRow.ToString()].Value = test.Avg;
                        }
                        iCnt++;
                    }
                }
            }

            public static void Export(CordProduction value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;

                if (null == value)
                    return;

                if (null != value)
                {
                    value.LoadProperties();
                }

                if (!ExcelExportUtils.CreateCOA2File(outputFile, true))
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
                            /*
                            // DATE
                            ws.Cells["M7"].Value = (value.InputDate.HasValue) ?
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B10"].Value = value.UserName;
                            // ITEM CODE
                            ws.Cells["I10"].Value = value.ItemCode;
                            // LOT NO
                            ws.Cells["N10"].Value = value.LotNo;
                            */
                            #endregion

                            #region Write Lot 1-5
                            /*
                            ws.Cells["G14"].Value = string.Format("{0}-1", value.LotNo);
                            ws.Cells["H14"].Value = string.Format("{0}-2", value.LotNo);
                            ws.Cells["I14"].Value = string.Format("{0}-3", value.LotNo);
                            ws.Cells["J14"].Value = string.Format("{0}-4", value.LotNo);
                            ws.Cells["K14"].Value = string.Format("{0}-5", value.LotNo);
                            */
                            #endregion

                            #region Write each properties
                            /*
                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            WriteProperty(ws, 15, p);
                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            WriteProperty(ws, 16, p);
                            // ELONG AT LOAD (PropertyNo = 3)
                            p = value.Properties.FindByPropertyNo(3);
                            WriteProperty(ws, 17, p);
                            // FIRST TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            WriteProperty(ws, 18, p);
                            // SECOND TWIST (PropertyNo = 8)
                            p = value.Properties.FindByPropertyNo(8);
                            WriteProperty(ws, 19, p);
                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            WriteProperty(ws, 20, p);
                            // SHRINKAGE FORCE (PropertyNo = 5)
                            p = value.Properties.FindByPropertyNo(5);
                            WriteProperty(ws, 21, p);
                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            WriteProperty(ws, 22, p);
                            // FINENESS (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            WriteProperty(ws, 23, p);
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

        #region COA 3

        public class COA3
        {
            private static void WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                if (null != ws && null != p && null != p.Spec)
                {
                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = "(" + p.Spec.UnitReport + ")";
                    // SPEC
                    ws.Cells["D" + iRow.ToString()].Value = p.Spec.ReportSpec;

                    // RESULT 1-5
                    int iCnt = 1;
                    foreach (var test in p.Tests)
                    {
                        if (iCnt == 1)
                        {
                            ws.Cells["G" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 2)
                        {
                            ws.Cells["H" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 3)
                        {
                            ws.Cells["I" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 4)
                        {
                            ws.Cells["J" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 5)
                        {
                            ws.Cells["K" + iRow.ToString()].Value = test.Avg;
                        }
                        iCnt++;
                    }
                }
            }

            public static void Export(CordProduction value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;

                if (null == value)
                    return;

                if (null != value)
                {
                    value.LoadProperties();
                }

                if (!ExcelExportUtils.CreateCOA3File(outputFile, true))
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
                            /*
                            // DATE
                            ws.Cells["M7"].Value = (value.InputDate.HasValue) ?
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B10"].Value = value.UserName;
                            // ITEM CODE
                            ws.Cells["I10"].Value = value.ItemCode;
                            // LOT NO
                            ws.Cells["N10"].Value = value.LotNo;
                            */
                            #endregion

                            #region Write Lot 1-5
                            /*
                            ws.Cells["G14"].Value = string.Format("{0}-1", value.LotNo);
                            ws.Cells["H14"].Value = string.Format("{0}-2", value.LotNo);
                            ws.Cells["I14"].Value = string.Format("{0}-3", value.LotNo);
                            ws.Cells["J14"].Value = string.Format("{0}-4", value.LotNo);
                            ws.Cells["K14"].Value = string.Format("{0}-5", value.LotNo);
                            */
                            #endregion

                            #region Write each properties
                            /*
                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            WriteProperty(ws, 15, p);
                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            WriteProperty(ws, 16, p);
                            // ELONG AT LOAD (PropertyNo = 3)
                            p = value.Properties.FindByPropertyNo(3);
                            WriteProperty(ws, 17, p);
                            // FIRST TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            WriteProperty(ws, 18, p);
                            // SECOND TWIST (PropertyNo = 8)
                            p = value.Properties.FindByPropertyNo(8);
                            WriteProperty(ws, 19, p);
                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            WriteProperty(ws, 20, p);
                            // SHRINKAGE FORCE (PropertyNo = 5)
                            p = value.Properties.FindByPropertyNo(5);
                            WriteProperty(ws, 21, p);
                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            WriteProperty(ws, 22, p);
                            // FINENESS (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            WriteProperty(ws, 23, p);
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
            private static void WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                if (null != ws && null != p && null != p.Spec)
                {
                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = "(" + p.Spec.UnitReport + ")";
                    // SPEC
                    ws.Cells["D" + iRow.ToString()].Value = p.Spec.ReportSpec;

                    // RESULT 1-5
                    int iCnt = 1;
                    foreach (var test in p.Tests)
                    {
                        if (iCnt == 1)
                        {
                            ws.Cells["G" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 2)
                        {
                            ws.Cells["H" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 3)
                        {
                            ws.Cells["I" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 4)
                        {
                            ws.Cells["J" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 5)
                        {
                            ws.Cells["K" + iRow.ToString()].Value = test.Avg;
                        }
                        iCnt++;
                    }
                }
            }

            public static void Export(CordProduction value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;

                if (null == value)
                    return;

                if (null != value)
                {
                    value.LoadProperties();
                }

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
                            #region Write Cells

                            // DATE
                            ws.Cells["M7"].Value = (value.InputDate.HasValue) ?
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B10"].Value = value.UserName;
                            // ITEM CODE
                            ws.Cells["I10"].Value = value.ItemCode;
                            // LOT NO
                            ws.Cells["N10"].Value = value.LotNo;

                            #endregion

                            #region Write Lot 1-5

                            ws.Cells["G14"].Value = string.Format("{0}-1", value.LotNo);
                            ws.Cells["H14"].Value = string.Format("{0}-2", value.LotNo);
                            ws.Cells["I14"].Value = string.Format("{0}-3", value.LotNo);
                            ws.Cells["J14"].Value = string.Format("{0}-4", value.LotNo);
                            ws.Cells["K14"].Value = string.Format("{0}-5", value.LotNo);

                            #endregion

                            #region Write each properties

                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            WriteProperty(ws, 15, p);
                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            WriteProperty(ws, 16, p);
                            // ELONG AT LOAD (PropertyNo = 3)
                            p = value.Properties.FindByPropertyNo(3);
                            WriteProperty(ws, 17, p);
                            // FIRST TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            WriteProperty(ws, 18, p);
                            // SECOND TWIST (PropertyNo = 8)
                            p = value.Properties.FindByPropertyNo(8);
                            WriteProperty(ws, 19, p);
                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            WriteProperty(ws, 20, p);
                            // SHRINKAGE FORCE (PropertyNo = 5)
                            p = value.Properties.FindByPropertyNo(5);
                            WriteProperty(ws, 21, p);
                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            WriteProperty(ws, 22, p);
                            // FINENESS (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            WriteProperty(ws, 23, p);

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

        #region COA 5

        public class COA5
        {
            private static void WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                if (null != ws && null != p && null != p.Spec)
                {
                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = "(" + p.Spec.UnitReport + ")";
                    // SPEC
                    ws.Cells["D" + iRow.ToString()].Value = p.Spec.ReportSpec;

                    // RESULT 1-5
                    int iCnt = 1;
                    foreach (var test in p.Tests)
                    {
                        if (iCnt == 1)
                        {
                            ws.Cells["G" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 2)
                        {
                            ws.Cells["H" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 3)
                        {
                            ws.Cells["I" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 4)
                        {
                            ws.Cells["J" + iRow.ToString()].Value = test.Avg;
                        }
                        else if (iCnt == 5)
                        {
                            ws.Cells["K" + iRow.ToString()].Value = test.Avg;
                        }
                        iCnt++;
                    }
                }
            }

            public static void Export(CordProduction value)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                string outputFile = ExcelModel.Dialogs.SaveDialog();
                if (string.IsNullOrEmpty(outputFile))
                    return;

                if (null == value)
                    return;

                if (null != value)
                {
                    value.LoadProperties();
                }

                if (!ExcelExportUtils.CreateCOA5File(outputFile, true))
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
                            /*
                            // DATE
                            ws.Cells["M7"].Value = (value.InputDate.HasValue) ?
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B10"].Value = value.UserName;
                            // ITEM CODE
                            ws.Cells["I10"].Value = value.ItemCode;
                            // LOT NO
                            ws.Cells["N10"].Value = value.LotNo;
                            */
                            #endregion

                            #region Write Lot 1-5
                            /*
                            ws.Cells["G14"].Value = string.Format("{0}-1", value.LotNo);
                            ws.Cells["H14"].Value = string.Format("{0}-2", value.LotNo);
                            ws.Cells["I14"].Value = string.Format("{0}-3", value.LotNo);
                            ws.Cells["J14"].Value = string.Format("{0}-4", value.LotNo);
                            ws.Cells["K14"].Value = string.Format("{0}-5", value.LotNo);
                            */
                            #endregion

                            #region Write each properties
                            /*
                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            WriteProperty(ws, 15, p);
                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            WriteProperty(ws, 16, p);
                            // ELONG AT LOAD (PropertyNo = 3)
                            p = value.Properties.FindByPropertyNo(3);
                            WriteProperty(ws, 17, p);
                            // FIRST TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            WriteProperty(ws, 18, p);
                            // SECOND TWIST (PropertyNo = 8)
                            p = value.Properties.FindByPropertyNo(8);
                            WriteProperty(ws, 19, p);
                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            WriteProperty(ws, 20, p);
                            // SHRINKAGE FORCE (PropertyNo = 5)
                            p = value.Properties.FindByPropertyNo(5);
                            WriteProperty(ws, 21, p);
                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            WriteProperty(ws, 22, p);
                            // FINENESS (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            WriteProperty(ws, 23, p);
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
