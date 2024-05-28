#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.Models;
using NLib.Reflection;

#endregion

namespace M3.QA.Models
{
    #region UniTestImportResult

    /// <summary>
    /// The UniTestImportResult class.
    /// </summary>
    public class UniTestImportResult
    {
        public bool IsValid { get; set; }
        public string ErrMsg { get; set; }
        public UniTestTensileElongation Value { get; set; }
    }

    #endregion

    #region UniTestTensileElongation

    public class UniTestTensileElongation : NInpc
    {
        #region Private Methods

        private void ParseComment1()
        {
            string[] sps = Comment1.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            NoOfSP = (null != sps) ? sps.Length : 0;

            // Clear SPs.
            SP1 = new int?();
            SP2 = new int?();
            SP3 = new int?();
            SP4 = new int?();
            SP5 = new int?();
            SP6 = new int?();
            SP7 = new int?();

            // Raise NoOfSP Change Events
            Raise(() => this.NoOfSP);

            if (NoOfSP <= 0) return;

            int sp;
            for (int i = 0; i < NoOfSP; i++) 
            { 
                switch (i)
                {
                    case 0: 
                        {
                            SP1 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 1:
                        {
                            SP2 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 2:
                        {
                            SP3 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 3:
                        {
                            SP4 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 4:
                        {
                            SP5 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 5:
                        {
                            SP6 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                    case 6:
                        {
                            SP7 = int.TryParse(sps[i], out sp) ? sp : new int?();
                            break;
                        }
                }
            }
            // Raise SP? Change Events
            Raise(() => this.SP1);
            Raise(() => this.SP2);
            Raise(() => this.SP3);
            Raise(() => this.SP4);
            Raise(() => this.SP5);
            Raise(() => this.SP6);
            Raise(() => this.SP7);
        }

        public void PrepareProperties()
        {
            PrepareTensileStrengths();
            PrepareUniTestElongations();
        }

        private void PrepareTensileStrengths()
        {
            TensileStrengths = new List<UniTestTensileStrength>();
            for (int i = 0; i < NoOfSP; i++) 
            {
                var inst = new UniTestTensileStrength();

                inst.LotNo = this.LotNo;
                inst.YarnType = this.YarnType;
                inst.NoOfSample = this.NoOfSample;
                
                switch (i)
                {
                    case 0: 
                        {
                            inst.SPNo = this.SP1;
                            break;
                        }
                    case 1:
                        {
                            inst.SPNo = this.SP2;
                            break;
                        }
                    case 2:
                        {
                            inst.SPNo = this.SP3;
                            break;
                        }
                    case 3:
                        {
                            inst.SPNo = this.SP4;
                            break;
                        }
                    case 4:
                        {
                            inst.SPNo = this.SP5;
                            break;
                        }
                    case 5:
                        {
                            inst.SPNo = this.SP6;
                            break;
                        }
                    case 6:
                        {
                            inst.SPNo = this.SP7;
                            break;
                        }
                }

                // Append to List
                TensileStrengths.Add(inst);
            }
        }

        private void PrepareUniTestElongations()
        {
            Elongations = new List<UniTestElongation>();
            for (int i = 0; i < NoOfSP; i++)
            {
                bool valid = false;
                var inst = new UniTestElongation();

                inst.LotNo = this.LotNo;
                inst.YarnType = this.YarnType;
                inst.NoOfSample = this.NoOfSample;

                switch (i)
                {
                    case 0:
                        {
                            inst.SPNo = this.SP1;
                            valid = true;
                            break;
                        }
                    case 1:
                        {
                            inst.SPNo = this.SP2;
                            valid = true;
                            break;
                        }
                    case 2:
                        {
                            inst.SPNo = this.SP3;
                            valid = true;
                            break;
                        }
                    case 3:
                        {
                            inst.SPNo = this.SP4;
                            valid = true;
                            break;
                        }
                    case 4:
                        {
                            inst.SPNo = this.SP5;
                            valid = true;
                            break;
                        }
                    case 5:
                        {
                            inst.SPNo = this.SP6;
                            valid = true;
                            break;
                        }
                    case 6:
                        {
                            inst.SPNo = this.SP7;
                            valid = true;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                if (valid)
                {
                    var elongBreak = new UniTestElongationBreakProperty();
                    elongBreak.SPNo = inst.SPNo;
                    elongBreak.LotNo = inst.LotNo;
                    elongBreak.NoOfSample = inst.NoOfSample;
                    inst.SubProperties.Add(elongBreak);
                    foreach (string elong in this.ElongNs)
                    {
                        var elongLoad = new UniTestElongationLoadProperty();
                        elongLoad.LoadN = elong;
                        elongLoad.SPNo = inst.SPNo;
                        elongLoad.LotNo = inst.LotNo;
                        elongLoad.NoOfSample = inst.NoOfSample;
                        inst.SubProperties.Add(elongLoad);
                    }
                    // Append to List
                    Elongations.Add(inst);
                }
            }
        }

        #endregion

        #region Public Properties

        #region Common

        /// <summary>Gets or sets Test Date.</summary>
        public DateTime? TestDate { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? Humidity { get; set; }
        /// <summary>Gets or sets Sample Name (aka. Product Name).</summary>
        public string SampleName { get; set; }
        public string LotNo { get; set; }

        public decimal? Preparation { get; set; }

        public string Operator { get; set; }

        public string Comment1 
        { 
            get { return Get<string>(); }
            set 
            {
                Set(value, () => 
                {
                    ParseComment1();
                });
            }
        }

        public string TestType { get; set; }

        public int NoOfSample { get; set; } = 3; // Fixed
        public List<string> ElongNs { get; set; } = new List<string>();

        public string YarnType { get; set; }

        #endregion

        #region SP

        public int NoOfSP { get; set; }
        public int? SP1 { get; set; }
        public int? SP2 { get; set; }
        public int? SP3 { get; set; }
        public int? SP4 { get; set; }
        public int? SP5 { get; set; }
        public int? SP6 { get; set; }
        public int? SP7 { get; set; }

        #endregion

        #region Test Properties

        public List<UniTestTensileStrength> TensileStrengths { get; set; }
        public List<UniTestElongation> Elongations { get; set; }

        #endregion

        #endregion

        #region Static Methods

        public static UniTestImportResult Import(string fileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            UniTestImportResult result = new UniTestImportResult();
            result.IsValid = false;
            result.ErrMsg = "unknown";
            result.Value = null;

            UniTestTensileElongation inst = null;

            var cfg = new ExcelConfig();
            cfg.DataSource.FileName = fileName;
            cfg.DataSource.HeaderInFirstRow = false;
            cfg.DataSource.Driver = ExcelDriver.Jet;
            cfg.DataSource.IMexMode = IMex.ImportMode;

            var conn = new NDbConnection();
            conn.Config = cfg;
            if (conn.Connect())
            {
                try
                {
                    #region UniTest.TensileCond

                    string sheetName1 = "UniTest.TensileCond";
                    var table1 = conn.Query("Select * from [" + sheetName1 + "$]").Result;
                    if (null != table1)
                    {
                        inst = new UniTestTensileElongation();
                        foreach (DataRow row in table1.Rows)
                        {
                            if (null == row) continue;
                            string pName = null != row["F2"] ? row["F2"].ToString() : null;
                            object pValue = null != row["F3"] ? row["F3"] : null;
                            if (string.IsNullOrEmpty(pName)) continue;

                            if (pName == "Test date")
                            {
                                inst.TestDate = (null != pValue) ? 
                                    DateTime.ParseExact(pValue.ToString(), "yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo) : new DateTime?();
                            }
                            else if (pName == "Temperature")
                            {
                                inst.Temperature = (null != pValue) ?
                                    decimal.Parse(pValue.ToString()) : new decimal?();
                            }
                            else if (pName == "Humidity")
                            {
                                inst.Humidity = (null != pValue) ?
                                    decimal.Parse(pValue.ToString()) : new decimal?();
                            }
                            else if (pName == "Sample name")
                            {
                                inst.SampleName = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Lot No.")
                            {
                                inst.LotNo = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Preparation")
                            {
                                inst.Preparation = (null != pValue) ?
                                    decimal.Parse(pValue.ToString()) : new decimal?();
                            }
                            else if (pName == "Operator")
                            {
                                inst.Operator = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Comment 1")
                            {
                                inst.Comment1 = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                            else if (pName == "Test type")
                            {
                                inst.TestType = (null != pValue) ?
                                    pValue.ToString() : null;
                            }
                        }
                        table1.Dispose();
                    }
                    else
                    {
                        result.IsValid = false;
                        result.ErrMsg = "Cannot Access Sheet " + sheetName1;
                        result.Value = null;
                    }

                    table1 = null;

                    #endregion

                    // Check can load data
                    if (null == inst)
                    {
                        return result;
                    }

                    if (string.IsNullOrWhiteSpace(inst.TestType) || inst.TestType != "Tension test")
                    {
                        // 
                        result.IsValid = false;
                        result.ErrMsg = "Invalid file , Not Tension test file.";
                        result.Value = null;
                        return result;
                    }

                    #region Data

                    string sheetName2 = "Data";
                    var table2 = conn.Query("Select * from [" + sheetName2 + "$]").Result;
                    if (null != table2)
                    {
                        List<DataColumn> elongCols = new List<DataColumn>();
                        List<string> elongNs = new List<string>();

                        DataRow headerRow = table2.Rows[0];
                        foreach (DataColumn col in table2.Columns)
                        {
                            string hdrTxt = headerRow[col].ToString();
                            if (hdrTxt.StartsWith("At-Load-"))
                            {
                                string elongN = hdrTxt.Replace("At-Load-", "");
                                elongCols.Add(col);
                                elongNs.Add(elongN);
                            }
                        }

                        // update elongNs to inst
                        inst.ElongNs = elongNs;
                        inst.PrepareProperties(); // prepare properties

                        // Loop data row.
                        int iSP = 0;
                        int iCnt = 1;
                        decimal? N;
                        decimal d;
                        for (int iRow = 3; iRow < table2.Rows.Count; iRow++)
                        {
                            DataRow row = table2.Rows[iRow];

                            #region Tensile Strengths

                            if (null != inst.TensileStrengths &&
                                inst.TensileStrengths.Count > 0 && iSP < inst.TensileStrengths.Count &&
                                null != inst.Elongations[iSP])
                            {
                                N = decimal.TryParse(row["F2"].ToString(), out d) ? d : new decimal?();

                                switch (iCnt)
                                {
                                    case 1:
                                        {
                                            inst.TensileStrengths[iSP].N1 = N;
                                            break;
                                        }
                                    case 2:
                                        {
                                            inst.TensileStrengths[iSP].N2 = N;
                                            break;
                                        }
                                    case 3:
                                        {
                                            inst.TensileStrengths[iSP].N3 = N;
                                            break;
                                        }
                                }
                            }

                            #endregion

                            #region Elongation

                            #region At-Break

                            if (null != inst.Elongations &&
                                inst.Elongations.Count > 0 && iSP < inst.Elongations.Count &&
                                null != inst.Elongations[iSP])
                            {
                                var subProps = inst.Elongations[iSP].SubProperties;
                                var atBreak = (null != subProps && subProps.Count > 0) ? subProps[0] : null;

                                N = decimal.TryParse(row["F3"].ToString(), out d) ? d : new decimal?();
                                switch (iCnt)
                                {
                                    case 1:
                                        {
                                            if (null != atBreak)
                                            {
                                                atBreak.N1 = N;
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (null != atBreak)
                                            {
                                                atBreak.N2 = N;
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (null != atBreak)
                                            {
                                                // At Break
                                                atBreak.N3 = N;
                                            }
                                            break;
                                        }
                                }
                            }

                            #endregion

                            #region At-Load

                            if (null != inst.Elongations &&
                                inst.Elongations.Count > 0 && iSP < inst.Elongations.Count &&
                                null != inst.Elongations[iSP])
                            {
                                var subProps = inst.Elongations[iSP].SubProperties;

                                int iCol = 0;
                                foreach (DataColumn col in elongCols)
                                {
                                    var atLoad = subProps.FindByElong(elongNs[iCol]);

                                    if (null != atLoad)
                                    {
                                        N = decimal.TryParse(row[col].ToString(), out d) ? d : new decimal?();

                                        switch (iCnt)
                                        {
                                            case 1:
                                                {
                                                    if (null != atLoad)
                                                    {
                                                        atLoad.N1 = N;
                                                    }
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    if (null != atLoad)
                                                    {
                                                        atLoad.N2 = N;
                                                    }
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    if (null != atLoad)
                                                    {
                                                        atLoad.N3 = N;
                                                    }
                                                    break;
                                                }
                                        }
                                    }

                                    iCol++;
                                }
                            }

                            #endregion

                            #endregion

                            iCnt++;

                            if (iCnt > 3)
                            {
                                iCnt = 1; // Reset to N1

                                iSP++; // next sp
                            }
                        }

                        // Success
                        result.IsValid = true;
                        result.ErrMsg = "Success";
                        result.Value = inst;
                    }
                    else
                    {
                        result.IsValid = false;
                        result.ErrMsg = "Cannot Access Sheet " + sheetName2;
                        result.Value = null;
                    }
                    table2 = null;

                    #endregion
                }
                catch (Exception ex)
                {
                    med.Err(ex);

                    result.IsValid = false;
                    result.ErrMsg = ex.Message;
                    result.Value = null;
                }
                finally
                {

                }
            }
            else
            {
                result.IsValid = false;
                result.ErrMsg = "Cannot Open Excel File.";
                result.Value = null;
            }

            conn.Disconnect();
            conn.Dispose();
            conn = null;

            return result;
        }

        #endregion
    }

    #endregion
}
