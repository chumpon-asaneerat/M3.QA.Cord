#region Using

using System;
using System.Reflection;
using System.Collections.Generic;
using M3.QA.Models;
using NLib;
using NLib.Models;

using OfficeOpenXml;
using System.Drawing;
using System.Windows;
using System.Linq;

#endregion

namespace M3.QA
{
    public enum JudgeStatus
    {
        NoSpec,
        OK,
        NG
    }

    public class COAService
    {
        private static List<string> Codes = new List<string>()
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M"
        };

        public static decimal ToFloor(decimal i, double decimalPlaces)
        {
            var power = Convert.ToDecimal(Math.Pow(10, decimalPlaces));
            return Math.Floor(i * power) / power;
        }

        public static DateTime? GetDateFromLot(string productLotNo)
        {
            DateTime? ret = new DateTime?();
            if (!string.IsNullOrEmpty(productLotNo))
            {
                var sLot = productLotNo.Trim();
                if (sLot.Length >= 5) 
                { 
                    string sYear = "20" + sLot.Substring(0, 2);
                    string sMn = sLot.Substring(2, 1);
                    string sDt = sLot.Substring(3, 2);

                    int iYr;
                    int iDt;
                    int iMn = Codes.IndexOf(sMn) + 1;
                    if (int.TryParse(sYear, out iYr) && int.TryParse(sDt, out iDt) && (iMn >= 1 && iMn <= 12))
                    {
                        ret = new DateTime(iYr, iMn, iDt);
                    }
                }
            }
            return ret;
        }

        #region COA 1

        public class COA1
        {
            private static JudgeStatus WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                JudgeStatus ret = JudgeStatus.NoSpec;

                if (null != ws && null != p)
                {
                    if (p.PropertyNo == 3)
                    {
                        // Replace ELONG AT Load (like 44 N)
                        string loadN = (null != p.Spec) ? p.Spec.UnitDesc : " N";
                        ws.Cells["A" + iRow.ToString()].Value = "ELONG. AT " + loadN + "";
                    }

                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = "(" + p.Spec.UnitReport + ")";

                    // SPEC
                    if (p.PropertyNo == 2)
                    {
                        // ELONG AT BREAK
                        string sVal = (null != p.Spec) ? p.Spec.ReportSpec : "(%)";
                        sVal = sVal.Replace("MIN.", "Min.");
                        ws.Cells["D" + iRow.ToString()].Value = sVal;
                    }
                    else if (p.PropertyNo == 10)
                    {
                        // DENIER
                        ws.Cells["D" + iRow.ToString()].Value = (null != p.Spec) ? p.Spec.ReportSpecInt : "(%)";
                    }
                    else
                    {
                        ws.Cells["D" + iRow.ToString()].Value = (null != p.Spec) ? p.Spec.ReportSpec : "(%)";
                    }

                    // RESULT
                    if (p.PropertyNo == 10)
                    {
                        // DENIER
                        //ws.Cells["E" + iRow.ToString()].Value = p.Avg;
                        ws.Cells["E" + iRow.ToString()].Value = (p.Avg.HasValue) ? ToFloor(p.Avg.Value, 0) : 0;
                        ws.Cells["E" + iRow.ToString()].Style.Numberformat.Format = "######0";
                    }
                    else if (p.PropertyNo == 12)
                    {
                        // RPU
                        ws.Cells["E" + iRow.ToString()].Value = (p.Avg.HasValue) ? ToFloor(p.Avg.Value, 2) : 0;
                        ws.Cells["E" + iRow.ToString()].Style.Numberformat.Format = "#,##0.0";
                    }
                    else
                    {
                        ws.Cells["E" + iRow.ToString()].Value = p.Avg;
                        ws.Cells["E" + iRow.ToString()].Style.Numberformat.Format = "#,##0.0";
                    }

                    // Judge
                    ret = (null != p.Spec) ? (p.Spec.IsOutOfSpec(p.Avg) ? JudgeStatus.NG : JudgeStatus.OK) : JudgeStatus.NoSpec;
                    string sJudge;
                    switch (ret)
                    {
                        case JudgeStatus.OK:
                            sJudge = "OK";
                            break;
                        case JudgeStatus.NG:
                            sJudge = "NO GOOD";
                            break;
                        default:
                            sJudge = "-";
                            break;
                    }
                    // JUDGE
                    ws.Cells["G" + iRow.ToString()].Value = sJudge;
                    if (ret == JudgeStatus.OK)
                    {
                        ws.Cells["G" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }
                    else if (ret == JudgeStatus.NG)
                    {
                        ws.Cells["G" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.WhiteSmoke);
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }

                    // Test Method
                    ws.Cells["H" + iRow.ToString()].Value = p.Spec.TestMethod;
                }

                return ret;
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
                            // PRODUCT LOT NO
                            ws.Cells["B15"].Value = value.ProductionLot;
                            // PI NO 
                            ws.Cells["B16"].Value = value.PiNoSL;

                            #endregion

                            #region Write each properties

                            int iNG = 0;
                            CordProductionProperty p;

                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            if (WriteProperty(ws, 20, p) == JudgeStatus.NG) iNG++;

                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            if (WriteProperty(ws, 21, p) == JudgeStatus.NG) iNG++;

                            // ELONG AT LOAD (PropertyNo = 3)

                            // Need to find ELongN in report spec first
                            var rpts = Models.Utils.M_GetReportTestSpecByMasterid.Gets(value.MasterId).Value();
                            var rpt = (null != rpts) ? rpts.Find(x => x.PropertyNo == 3) : null;
                            string elongN = (null != rpt) ? rpt.UnitId : null;

                            p = value.Properties.FindByPropertyNo(3, elongN);
                            if (WriteProperty(ws, 22, p) == JudgeStatus.NG) iNG++;

                            // NO OF TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            if (WriteProperty(ws, 23, p) == JudgeStatus.NG) iNG++;

                            // CORD GAUGE (PropertyNo = 9)
                            p = value.Properties.FindByPropertyNo(9);
                            if (WriteProperty(ws, 24, p) == JudgeStatus.NG) iNG++;

                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            if (WriteProperty(ws, 25, p) == JudgeStatus.NG) iNG++;

                            // DENIER (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            if (WriteProperty(ws, 26, p) == JudgeStatus.NG) iNG++;

                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            if (WriteProperty(ws, 27, p) == JudgeStatus.NG) iNG++;

                            // RPU (PropertyNo = 12)
                            p = value.Properties.FindByPropertyNo(12);
                            if (WriteProperty(ws, 28, p) == JudgeStatus.NG) iNG++;

                            // ADHESION FORCE (PEEL) (PropertyNo = 4)
                            p = value.Properties.FindByPropertyNo(4);
                            if (WriteProperty(ws, 29, p) == JudgeStatus.NG) iNG++;

                            #endregion

                            // Update overall judge
                            ws.Cells["E12"].Value = (iNG > 0) ? "NO PASSED" : "PASSED";
                            ws.Cells["E12"].Style.Font.Size = 50;
                            ws.Cells["E12"].Style.Font.Bold = true;
                            if (iNG > 0)
                            {
                                ws.Cells["E12"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                //ws.Cells["E12"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                //ws.Cells["E12"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
                            else
                            {
                                ws.Cells["E12"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                //ws.Cells["E12"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                //ws.Cells["E12"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
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

        #region COA 1a

        public class COA1a
        {
            private static JudgeStatus WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                JudgeStatus ret = JudgeStatus.NoSpec;

                if (null != ws && null != p)
                {
                    if (p.PropertyNo == 3)
                    {
                        // Replace ELONG AT Load (like 44 N)
                        string loadN = (null != p.Spec) ? p.Spec.UnitDesc : " N";
                        ws.Cells["A" + iRow.ToString()].Value = "ELONG. AT " + loadN + "";
                    }

                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = "(" + p.Spec.UnitReport + ")";

                    // SPEC
                    if (p.PropertyNo == 2)
                    {
                        // ELONG AT BREAK
                        string sVal = (null != p.Spec) ? p.Spec.ReportSpec : "(%)";
                        sVal = sVal.Replace("MIN.", "Min.");
                        ws.Cells["D" + iRow.ToString()].Value = sVal;
                    }
                    else if (p.PropertyNo == 10)
                    {
                        // DENIER
                        ws.Cells["D" + iRow.ToString()].Value = (null != p.Spec) ? p.Spec.ReportSpecInt : "(%)";
                    }
                    else
                    {
                        ws.Cells["D" + iRow.ToString()].Value = (null != p.Spec) ? p.Spec.ReportSpec : "(%)";
                    }

                    // RESULT
                    if (p.PropertyNo == 10)
                    {
                        // DENIER                        
                        ws.Cells["E" + iRow.ToString()].Value = (p.Avg.HasValue) ? ToFloor(p.Avg.Value, 0) : 0;
                        ws.Cells["E" + iRow.ToString()].Style.Numberformat.Format = "######0";
                    }
                    else if (p.PropertyNo == 12)
                    {
                        // RPU
                        ws.Cells["E" + iRow.ToString()].Value = (p.Avg.HasValue) ? ToFloor(p.Avg.Value, 2) : 0;
                        ws.Cells["E" + iRow.ToString()].Style.Numberformat.Format = "#,##0.0";
                    }
                    else
                    {
                        ws.Cells["E" + iRow.ToString()].Value = p.Avg;
                        ws.Cells["E" + iRow.ToString()].Style.Numberformat.Format = "#,##0.0";
                    }

                    // Judge
                    ret = (null != p.Spec) ? (p.Spec.IsOutOfSpec(p.Avg) ? JudgeStatus.NG : JudgeStatus.OK) : JudgeStatus.NoSpec;
                    string sJudge;
                    switch (ret)
                    {
                        case JudgeStatus.OK:
                            sJudge = "OK";
                            break;
                        case JudgeStatus.NG:
                            sJudge = "NO GOOD";
                            break;
                        default:
                            sJudge = "-";
                            break;
                    }
                    // JUDGE
                    ws.Cells["G" + iRow.ToString()].Value = sJudge;
                    if (ret == JudgeStatus.OK)
                    {
                        ws.Cells["G" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }
                    else if (ret == JudgeStatus.NG)
                    {
                        ws.Cells["G" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.WhiteSmoke);
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["G" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }

                    // Test Method
                    ws.Cells["H" + iRow.ToString()].Value = p.Spec.TestMethod;
                }

                return ret;
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

                if (!ExcelExportUtils.CreateCOA1aFile(outputFile, true))
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
                            // PRODUCT LOT NO
                            ws.Cells["B15"].Value = value.ProductionLot;
                            // PI NO 
                            ws.Cells["B16"].Value = value.PiNoSL;

                            #endregion

                            #region Write each properties

                            int iNG = 0;
                            CordProductionProperty p;

                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            if (WriteProperty(ws, 20, p) == JudgeStatus.NG) iNG++;

                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            if (WriteProperty(ws, 21, p) == JudgeStatus.NG) iNG++;

                            // ELONG AT LOAD (PropertyNo = 3)

                            // Need to find ELongN in report spec first
                            var rpts = Models.Utils.M_GetReportTestSpecByMasterid.Gets(value.MasterId).Value();
                            var rpt = (null != rpts) ? rpts.Find(x => x.PropertyNo == 3) : null;
                            string elongN = (null != rpt) ? rpt.UnitId : null;

                            p = value.Properties.FindByPropertyNo(3, elongN);
                            if (WriteProperty(ws, 22, p) == JudgeStatus.NG) iNG++;

                            // NO OF TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            if (WriteProperty(ws, 23, p) == JudgeStatus.NG) iNG++;

                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            if (WriteProperty(ws, 24, p) == JudgeStatus.NG) iNG++;

                            // DENIER (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            if (WriteProperty(ws, 25, p) == JudgeStatus.NG) iNG++;

                            // RPU (PropertyNo = 12)
                            p = value.Properties.FindByPropertyNo(12);
                            if (WriteProperty(ws, 26, p) == JudgeStatus.NG) iNG++;

                            #endregion

                            // Update overall judge
                            ws.Cells["E12"].Value = (iNG > 0) ? "NO PASSED" : "PASSED";
                            ws.Cells["E12"].Style.Font.Size = 50;
                            ws.Cells["E12"].Style.Font.Bold = true;
                            if (iNG > 0)
                            {
                                ws.Cells["E12"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                //ws.Cells["E12"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                //ws.Cells["E12"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
                            else
                            {
                                ws.Cells["E12"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                //ws.Cells["E12"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                //ws.Cells["E12"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
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
            private static JudgeStatus WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                JudgeStatus ret = JudgeStatus.NoSpec;
                if (null != ws && null != p)
                {
                    string spec;
                    if (p.PropertyNo == 3 && p.UnitId == "10" || p.UnitId == "20")
                    {
                        spec = "For Information";
                    }
                    else
                    {
                        spec = (null != p.Spec) ? p.Spec.ReportSpec : null;
                    }

                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = (null != p.Spec) ? "(" + p.Spec.UnitReport + ")" : "(%)";
                    // SPEC
                    ws.Cells["D" + iRow.ToString()].Value = spec;

                    // MIN
                    ws.Cells["E" + iRow.ToString()].Value = p.MinTestValue;
                    // MAX
                    ws.Cells["F" + iRow.ToString()].Value = p.MaxTestValue;
                    // AVG
                    ws.Cells["G" + iRow.ToString()].Value = p.Avg;
                    // STDDEV
                    ws.Cells["H" + iRow.ToString()].Value = p.StdDev;
                    // CPK
                    ws.Cells["I" + iRow.ToString()].Value = p.Cpk;
                    // Judge
                    ret = (null != p.Spec) ? (p.Spec.IsOutOfSpec(p.Avg) ? JudgeStatus.NG : JudgeStatus.OK) : JudgeStatus.NoSpec;
                    string sJudge;
                    switch (ret)
                    {
                        case JudgeStatus.OK:
                            sJudge = "OK";
                            break;
                        case JudgeStatus.NG:
                            sJudge = "NO GOOD";
                            break;
                        default:
                            sJudge = "-";
                            break;
                    }
                    ws.Cells["J" + iRow.ToString()].Value = sJudge;
                    if (ret == JudgeStatus.OK)
                    {
                        ws.Cells["J" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        //ws.Cells["J" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["J" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }
                    else if (ret == JudgeStatus.NG)
                    {
                        ws.Cells["J" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                        //ws.Cells["J" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["J" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }

                    // Test Method
                    ws.Cells["K" + iRow.ToString()].Value = (null != p.Spec) ? p.Spec.TestMethod : null;
                }

                return ret;
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

                            // DATE
                            ws.Cells["L7"].Value = (value.InputDate.HasValue) ?
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B11"].Value = value.UserName;
                            // PRODUCT
                            ws.Cells["B12"].Value = value.ProductName;
                            // ITEM CODE
                            ws.Cells["B13"].Value = value.ItemCode;
                            // SAP CODE
                            ws.Cells["B14"].Value = "7 179 172 90";
                            // YARN TYPE
                            ws.Cells["B15"].Value = value.YarnCode;
                            // PRODUCT LOT NO
                            ws.Cells["B16"].Value = value.ProductionLot;
                            // VALID DATE
                            var dt = COAService.GetDateFromLot(value.ProductionLot);
                            ws.Cells["B17"].Value = (dt.HasValue) ? dt.Value.AddYears(1) : new DateTime?();
                            // PI No SL
                            ws.Cells["B18"].Value = value.PiNoSL;

                            #endregion

                            #region Write each properties

                            int iCnt = 0, iOk = 0;
                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            if (WriteProperty(ws, 22, p) == JudgeStatus.OK) iOk++;
                            iCnt++;
                            
                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            if (WriteProperty(ws, 23, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // Need to find ELongN in report spec first
                            var rpts = Models.Utils.M_GetReportTestSpecByMasterid.Gets(value.MasterId).Value();
                            var matchs = (null != rpts) ? rpts.FindAll(x => x.PropertyNo == 3) : null;
                            var rpt1 = (null != matchs && matchs.Count > 0) ? matchs[0] : null;
                            var rpt2 = (null != matchs && matchs.Count > 1) ? matchs[1] : null;
                            var rpt3 = (null != matchs && matchs.Count > 2) ? matchs[2] : null;

                            string elongN1 = (null != rpt1) ? rpt1.UnitId : null;
                            string elongN2 = (null != rpt2) ? rpt2.UnitId : null;
                            string elongN3 = (null != rpt3) ? rpt3.UnitId : null;

                            // ELONG AT LOAD (PropertyNo = 3 AT 10 N)
                            p = value.Properties.FindByPropertyNo(3, elongN1);
                            if (WriteProperty(ws, 24, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // ELONG AT LOAD (PropertyNo = 3 AT 20 N)
                            p = value.Properties.FindByPropertyNo(3, elongN2);
                            if (WriteProperty(ws, 25, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // ELONG AT LOAD (PropertyNo = 3 AT 50 N)
                            p = value.Properties.FindByPropertyNo(3, elongN3);
                            if (WriteProperty(ws, 26, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // FIRST TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            if (WriteProperty(ws, 27, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            if (WriteProperty(ws, 28, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // FINENESS (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            if (WriteProperty(ws, 29, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // DPU (PropertyNo = 12)
                            p = value.Properties.FindByPropertyNo(12);
                            if (WriteProperty(ws, 30, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            #endregion

                            // Update overall judge
                            ws.Cells["H13"].Value = (iCnt == iOk) ? "PASSED" : "NO PASSED";
                            ws.Cells["H13"].Style.Font.Size = 50;
                            ws.Cells["H13"].Style.Font.Bold = true;
                            if (iCnt == iOk)
                            {
                                ws.Cells["H13"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                ws.Cells["H13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                ws.Cells["H13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
                            else
                            {
                                ws.Cells["H13"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                ws.Cells["H13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                ws.Cells["H13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
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
            private static JudgeStatus WriteProperty(ExcelWorksheet ws, int iRow, CordProductionProperty p)
            {
                JudgeStatus ret = JudgeStatus.NoSpec;
                if (null != ws && null != p)
                {
                    string spec = (null != p.Spec && p.Spec.SpecId > 0) ? p.Spec.ReportSpec : "-";

                    if (p.PropertyNo == 3)
                    {
                        // Replace ELONG AT Load (like 44 N)
                        string loadN = (null != p.Spec) ? p.Spec.UnitDesc : " N";
                        ws.Cells["A" + iRow.ToString()].Value = "ELONG. AT " + loadN + "";
                    }

                    // Unit Report
                    ws.Cells["C" + iRow.ToString()].Value = (null != p.Spec) ? "(" + p.Spec.UnitReport + ")" : "(%)";
                    if (iRow == 25)
                    {
                        // Custom for TWIST ONLY
                        ws.Cells["A" + iRow.ToString()].Value = (null != p.Spec) ? "(" + p.Spec.UnitReport + ")" : "(%)";
                    }
                    // SPEC
                    ws.Cells["D" + iRow.ToString()].Value = spec;

                    // RESULT (AVG)
                    ws.Cells["E" + iRow.ToString()].Value = p.Avg;
                    // MAX
                    ws.Cells["F" + iRow.ToString()].Value = p.MaxTestValue;
                    // MIN
                    ws.Cells["G" + iRow.ToString()].Value = p.MinTestValue;
                    // R
                    ws.Cells["H" + iRow.ToString()].Value = p.MaxTestValue - p.MinTestValue;
                    // Judge
                    ret = (null != p.Spec) ? (p.Spec.IsOutOfSpec(p.Avg) ? JudgeStatus.NG : JudgeStatus.OK) : JudgeStatus.NoSpec;
                    string sJudge;
                    switch (ret)
                    {
                        case JudgeStatus.OK:
                            sJudge = "OK";
                            break;
                        case JudgeStatus.NG:
                            sJudge = "NO GOOD";
                            break;
                        default:
                            sJudge = "-";
                            break;
                    }
                    ws.Cells["I" + iRow.ToString()].Value = sJudge;
                    if (ret == JudgeStatus.OK)
                    {
                        ws.Cells["I" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                        //ws.Cells["I" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["I" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }
                    else if (ret == JudgeStatus.NG)
                    {
                        ws.Cells["I" + iRow.ToString()].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                        //ws.Cells["I" + iRow.ToString()].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        //ws.Cells["I" + iRow.ToString()].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                    }
                    // Test Method
                    ws.Cells["J" + iRow.ToString()].Value = (null != p.Spec) ? p.Spec.TestMethod : null;
                }

                return ret;
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

                            // DATE
                            ws.Cells["J8"].Value = (value.InputDate.HasValue) ?
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B12"].Value = value.UserName;
                            // PRODUCT
                            ws.Cells["B13"].Value = value.ProductName;
                            // ITEM CODE
                            ws.Cells["B14"].Value = value.ItemCode;
                            // YARN TYPE
                            ws.Cells["B15"].Value = value.YarnCode;
                            // LOT NO
                            ws.Cells["B16"].Value = value.ProductionLot;
                            // PRODUCT LOT NO
                            ws.Cells["D17"].Value = value.ReportProductLot;

                            #endregion

                            #region Write each properties

                            int iCnt = 0, iOk = 0;
                            CordProductionProperty p;
                            // TENSILE STRENGTH (PropertyNo = 1)
                            p = value.Properties.FindByPropertyNo(1);
                            if (WriteProperty(ws, 21, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            if (WriteProperty(ws, 22, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // ELONG AT LOAD (PropertyNo = 3 AT 100 N)

                            // Need to find ELongN in report spec first
                            var rpts = Models.Utils.M_GetReportTestSpecByMasterid.Gets(value.MasterId).Value();
                            var rpt = (null != rpts) ? rpts.Find(x => x.PropertyNo == 3) : null;
                            string elongN = (null != rpt) ? rpt.UnitId : null;

                            p = value.Properties.FindByPropertyNo(3, elongN);
                            if (WriteProperty(ws, 23, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // FIRST TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            if (WriteProperty(ws, 24, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // SECOND TWIST (PropertyNo = 8)
                            p = value.Properties.FindByPropertyNo(8);
                            if (WriteProperty(ws, 25, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            if (WriteProperty(ws, 26, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // WEIGHT (PropertyNo = 14)
                            p = value.Properties.FindByPropertyNo(14);
                            if (WriteProperty(ws, 27, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            // DENIER (PropertyNo = 10)
                            p = value.Properties.FindByPropertyNo(10);
                            if (WriteProperty(ws, 28, p) == JudgeStatus.OK) iOk++;
                            iCnt++;

                            #endregion

                            // Update overall judge
                            ws.Cells["F13"].Value = (iCnt == iOk) ? "PASSED" : "NO PASSED";
                            if ((iCnt == iOk))
                            {
                                ws.Cells["F13"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                //ws.Cells["F13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                //ws.Cells["F13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
                            else
                            {
                                ws.Cells["F13"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                //ws.Cells["F13"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                //ws.Cells["F13"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.WhiteSmoke);
                            }
                            ws.Cells["F13"].Style.Font.Size = 50;
                            ws.Cells["F13"].Style.Font.Bold = true;
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

                    // Write AVG, STDDEV
                    ws.Cells["L" + iRow.ToString()].Value = (p.Avg.HasValue) ? p.Avg : decimal.Zero;
                    ws.Cells["M" + iRow.ToString()].Value = (p.StdDev.HasValue) ? p.StdDev : decimal.Zero;

                    // CP/CPK
                    if (p.Cp.HasValue)
                        ws.Cells["N" + iRow.ToString()].Value = p.Cp.Value;
                    else ws.Cells["N" + iRow.ToString()].Value = "-";

                    if (p.Cpk.HasValue)
                        ws.Cells["O" + iRow.ToString()].Value = p.Cpk.Value;
                    else ws.Cells["O" + iRow.ToString()].Value = "-";
                }
            }

            private static void WriteJudge(ExcelWorksheet ws, string sCell, CordProductionProperty p)
            {
                if (null != ws && null != p && null != p.Spec)
                {
                    if (p.Spec.IsOutOfSpec(p.Avg))
                    {
                        ws.Cells[sCell].Value = "NO PASSED";
                        ws.Cells[sCell].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Cells[sCell].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        ws.Cells[sCell].Style.Font.Bold = true;
                        ws.Cells[sCell].Style.Font.Size = 12;
                        ws.Cells[sCell].Style.Font.Color.SetColor(System.Drawing.Color.WhiteSmoke);
                        ws.Cells[sCell].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[sCell].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkRed);
                    }
                    else
                    {
                        ws.Cells[sCell].Value = "OK"; // or PASSED
                        ws.Cells[sCell].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Cells[sCell].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                        ws.Cells[sCell].Style.Font.Bold = true;
                        ws.Cells[sCell].Style.Font.Size = 12;
                        ws.Cells[sCell].Style.Font.Color.SetColor(System.Drawing.Color.WhiteSmoke);
                        ws.Cells[sCell].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[sCell].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.ForestGreen);
                    }
                }
                else
                {
                    ws.Cells[sCell].Value = "";
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
                            // PRODUCT LOT NO
                            ws.Cells["N10"].Value = value.ProductionLot;

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
                            WriteJudge(ws, "J26", p); // Judge

                            // ELONG AT BREAK (PropertyNo = 2)
                            p = value.Properties.FindByPropertyNo(2);
                            WriteProperty(ws, 16, p);
                            WriteJudge(ws, "J27", p); // Judge

                            // ELONG AT LOAD (PropertyNo = 3)

                            // Need to find ELongN in report spec first
                            var rpts = Models.Utils.M_GetReportTestSpecByMasterid.Gets(value.MasterId).Value();
                            var rpt = (null != rpts) ? rpts.Find(x => x.PropertyNo == 3) : null;
                            string elongN = (null != rpt) ? rpt.UnitId : null;

                            p = value.Properties.FindByPropertyNo(3, elongN);
                            WriteProperty(ws, 17, p);
                            WriteJudge(ws, "K26", p); // Judge

                            // FIRST TWIST (PropertyNo = 7)
                            p = value.Properties.FindByPropertyNo(7);
                            WriteProperty(ws, 18, p);
                            WriteJudge(ws, "K27", p); // Judge

                            // SECOND TWIST (PropertyNo = 8)
                            p = value.Properties.FindByPropertyNo(8);
                            WriteProperty(ws, 19, p);
                            WriteJudge(ws, "L26", p); // Judge

                            // THERMAL SHRINKAGE (PropertyNo = 6)
                            p = value.Properties.FindByPropertyNo(6);
                            WriteProperty(ws, 20, p);
                            WriteJudge(ws, "L27", p); // Judge

                            // SHRINKAGE FORCE (PropertyNo = 5)
                            p = value.Properties.FindByPropertyNo(5);
                            WriteProperty(ws, 21, p);
                            WriteJudge(ws, "M26", p); // Judge

                            // MOISTURE REGAIN (PropertyNo = 11)
                            p = value.Properties.FindByPropertyNo(11);
                            WriteProperty(ws, 22, p);
                            WriteJudge(ws, "M27", p); // Judge

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
            public static void Export(DIPSolutionProduction value)
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

                            // DATE
                            ws.Cells["G7"].Value = (value.InputDate.HasValue) ?
                                value.InputDate.Value.Date : new DateTime?();
                            // USER
                            ws.Cells["B11"].Value = value.UserName;
                            // ITEM CODE
                            ws.Cells["B12"].Value = value.ItemCode;
                            // LOT NO
                            ws.Cells["B13"].Value = value.LotNo;
                            // MANUFACTURING DATE
                            ws.Cells["C14"].Value = value.ManufacturingDate;
                            // VALID DATE
                            ws.Cells["B15"].Value = value.ValidDate;

                            int iErr = 0;
                            string judge = string.Empty;

                            #endregion

                            #region Properties

                            // PH
                            ws.Cells["C20"].Value = (null != value.PHSpec) ? value.PHSpec.UnitReport : string.Empty;
                            ws.Cells["D20"].Value = (null != value.PHSpec) ? value.PHSpec.ReportSpecEx : string.Empty;
                            ws.Cells["E20"].Value = value.PH;
                            if (null != value.PHSpec && !value.PHSpec.IsOutOfSpec(value.PH))
                            {
                                ws.Cells["F20"].Value =  "OK"; // or PASSED
                            }
                            else
                            {
                                ws.Cells["F20"].Value =  "NO GOOD"; // or NO PASSED
                                ws.Cells["F20"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                iErr++;
                            }

                            // TEMP
                            ws.Cells["C21"].Value = (null != value.TempturatureSpec) ? value.TempturatureSpec.UnitReport : string.Empty;
                            ws.Cells["D21"].Value = (null != value.TempturatureSpec) ? value.TempturatureSpec.ReportSpecEx : string.Empty;
                            ws.Cells["E21"].Value = value.Tempturature;
                            if (null != value.TempturatureSpec && !value.TempturatureSpec.IsOutOfSpec(value.Tempturature))
                            {
                                ws.Cells["F21"].Value = "OK"; // or PASSED
                            }
                            else
                            {
                                ws.Cells["F21"].Value = "NO GOOD"; // or NO PASSED
                                ws.Cells["F21"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                iErr++;
                            }

                            // VISCOSITY
                            ws.Cells["C22"].Value = (null != value.ViscositySpec) ? value.ViscositySpec.UnitReport : string.Empty;
                            ws.Cells["D22"].Value = (null != value.ViscositySpec) ? value.ViscositySpec.ReportSpecEx : string.Empty;
                            ws.Cells["E22"].Value = value.Viscosity;
                            if (null != value.ViscositySpec && !value.ViscositySpec.IsOutOfSpec(value.Viscosity))
                            {
                                ws.Cells["F22"].Value = "OK"; // or PASSED
                            }
                            else
                            {
                                ws.Cells["F22"].Value = "NO GOOD"; // or NO PASSED
                                ws.Cells["F22"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                iErr++;
                            }

                            // TSC
                            ws.Cells["C23"].Value = (null != value.TSCSpec) ? value.TSCSpec.UnitReport : string.Empty;
                            ws.Cells["D23"].Value = (null != value.TSCSpec) ? value.TSCSpec.ReportSpecEx : string.Empty;
                            ws.Cells["E23"].Value = value.TSCAvg;
                            if (null != value.TSCSpec && !value.TSCSpec.IsOutOfSpec(value.TSCAvg))
                            {
                                ws.Cells["F23"].Value = "OK"; // or PASSED
                            }
                            else
                            {
                                ws.Cells["F23"].Value = "NO GOOD"; // or NO PASSED
                                ws.Cells["F23"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                iErr++;
                            }

                            // JUDGE - OVERALL
                            ws.Cells["E12"].Value = (iErr > 0) ? "NO PASSED" : "PASSED";
                            if (iErr > 0)
                            {
                                ws.Cells["E12"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
                            }
                            else
                            {
                                ws.Cells["E12"].Style.Font.Color.SetColor(System.Drawing.Color.Black);
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
    }
}
