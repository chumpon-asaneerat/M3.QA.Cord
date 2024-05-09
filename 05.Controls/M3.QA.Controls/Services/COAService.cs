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

                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C20"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D20"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E20"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G20"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H20"].Value = p.Spec.TestMethod;
                            }

                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C21"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D21"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E21"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G21"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H21"].Value = p.Spec.TestMethod;
                            }

                            // ELONG AT LOAD (PropertyNo = 3)
                            p = value.Properties.FindByPropertyNo(3);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C22"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D22"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E22"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G22"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H22"].Value = p.Spec.TestMethod;
                            }

                            // NO OF TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C23"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D23"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E23"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G23"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H23"].Value = p.Spec.TestMethod;
                            }

                            // CORD GAUGE (PropertyNo = 9)
                            p = value.Properties.FindByPropertyNo(9);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C24"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D24"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E24"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G24"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H24"].Value = p.Spec.TestMethod;
                            }

                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C25"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D25"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E25"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G25"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H25"].Value = p.Spec.TestMethod;
                            }

                            // CORD SIZE (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C26"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D26"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E26"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G26"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H26"].Value = p.Spec.TestMethod;
                            }

                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C27"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D27"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E27"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G27"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H27"].Value = p.Spec.TestMethod;
                            }

                            // RPU (PropertyNo = 12)
                            p = value.Properties.FindByPropertyNo(12);
                            if (null != p && null != p.Spec)
                            {
                                // Unit Report
                                ws.Cells["C28"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D28"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E28"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G28"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H28"].Value = p.Spec.TestMethod;
                            }

                            // ADHESION FORCE (PEEL) (PropertyNo = 4)
                            p = value.Properties.FindByPropertyNo(4);
                            if (null != p) 
                            {
                                // Unit Report
                                ws.Cells["C29"].Value = "(" + p.Spec.UnitReport + ")";
                                // SPEC
                                ws.Cells["D29"].Value = p.Spec.ReportSpec;
                                // RESULT
                                ws.Cells["E29"].Value = p.Avg;
                                // JUDGE
                                ws.Cells["G29"].Value = p.Spec.IsOutOfSpec(p.Avg) ? "NG" : "OK";
                                // Test Method
                                ws.Cells["H29"].Value = p.Spec.TestMethod;
                            }

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
