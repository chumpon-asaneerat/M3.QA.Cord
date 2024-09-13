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

using Dapper;
using System.Security.Cryptography;
using System.Windows.Documents;

#endregion

namespace M3.QA.Models
{
    #region UnitTestTensileElongation

    public class UnitTestTensileElongation : NInpc
    {
        #region Private Methods

        private void ParseComment1()
        {
            Parser = UniTestSPParser.Parse(Comment1);
            NoOfSP = (null != Parser) ? Parser.Items.Count : 0;

            // Clear SPs.
            SP1 = new int?();
            SP2 = new int?();
            SP3 = new int?();
            SP4 = new int?();
            SP5 = new int?();
            SP6 = new int?();
            SP7 = new int?();

            if (NoOfSP <= 0) return;

            for (int i = 0; i < NoOfSP; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            SP1 = Parser.Items[i].SP;
                            break;
                        }
                    case 1:
                        {
                            SP2 = Parser.Items[i].SP;
                            break;
                        }
                    case 2:
                        {
                            SP3 = Parser.Items[i].SP;
                            break;
                        }
                    case 3:
                        {
                            SP4 = Parser.Items[i].SP;
                            break;
                        }
                    case 4:
                        {
                            SP5 = Parser.Items[i].SP;
                            break;
                        }
                    case 5:
                        {
                            SP6 = Parser.Items[i].SP;
                            break;
                        }
                    case 6:
                        {
                            SP7 = Parser.Items[i].SP;
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

        private void ParseComment2()
        {
            if (string.IsNullOrWhiteSpace(Comment2))
            {
                SampleType = "S";
            }
            else
            {
                string comment = Comment2.Trim().ToUpper();
                if (comment.StartsWith("F"))
                    SampleType = "F";
                else SampleType = "S";
            }
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
                var item = Parser.Items[i];

                var inst = new UniTestTensileStrength();

                inst.LotNo = this.LotNo;
                inst.YarnType = this.YarnType;
                inst.NoOfSample = this.NoOfSample;
                inst.SPNo = item.SP;
                inst.NoOfSample = 3;

                // Append to List
                TensileStrengths.Add(inst);
            }
        }

        private void PrepareUniTestElongations()
        {
            Elongations = new List<UniTestElongation>();
            for (int i = 0; i < NoOfSP; i++)
            {
                var item = Parser.Items[i];

                var inst = new UniTestElongation();

                inst.LotNo = this.LotNo;
                inst.YarnType = this.YarnType;
                inst.NoOfSample = this.NoOfSample;
                inst.SPNo = item.SP;
                inst.NoOfSample = 3;

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

        #endregion

        #region Public Properties

        #region Parser

        public UniTestSPParser Parser { get; set; }

        #endregion

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

        public string Comment2
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    ParseComment2();
                });
            }
        }

        public string TestType { get; set; }

        public string SampleType { get; set; }

        public int NoOfSample { get; set; } = 3; // Fixed
        public List<string> ElongNs { get; set; } = new List<string>();

        public string YarnType { get; set; }

        public int TestById { get; set; }

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

        public static UniTestImportResult<UnitTestTensileElongation> Import(string fileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            UniTestImportResult<UnitTestTensileElongation> result = new UniTestImportResult<UnitTestTensileElongation>();
            result.IsValid = false;
            result.ErrMsg = "unknown";
            result.Value = null;

            UnitTestTensileElongation inst = null;

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
                        inst = new UnitTestTensileElongation();
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
                            else if (pName == "Comment 2")
                            {
                                inst.Comment2 = (null != pValue) ?
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

                    #region Check can load data

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

                    #endregion

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
                        decimal? N;
                        decimal d;

                        if (null != inst.Parser && null != inst.Parser.Items &&
                            inst.Parser.Items.Count > 0)
                        {
                            int startRow = 3; // Excel start row.
                            int readRow = 0;

                            var items = inst.Parser.Items;
                            for (int iSP = 0; iSP < items.Count; iSP++)
                            {
                                var item = items[iSP];

                                var tensile = inst.TensileStrengths[iSP]; // Tensile Strengths
                                var elong = inst.Elongations[iSP]; // Elongations
                                var subProps = inst.Elongations[iSP].SubProperties;
                                var atBreak = (null != subProps && subProps.Count > 0) ? subProps[0] : null;

                                int totalRow = 3; // default 3 N
                                totalRow += (item.RetestN1) ? 2 : 0; // Has Retest N1 need 2 value
                                totalRow += (item.RetestN2) ? 2 : 0; // Has Retest N2 need 2 value
                                totalRow += (item.RetestN3) ? 2 : 0; // Has Retest N3 need 2 value
                                int currRow = 0;

                                // Read require rows for current SP (in parser)
                                while (currRow < totalRow)
                                {
                                    int idx = startRow + readRow + currRow;
                                    if (idx >= table2.Rows.Count) 
                                        break; // reach end of rows

                                    // Read value on current row
                                    DataRow row = table2.Rows[idx];

                                    #region Tensile Strengths

                                    N = decimal.TryParse(row["F2"].ToString(), out d) ? d : new decimal?();

                                    switch (currRow)
                                    {
                                        case 0:
                                            tensile.N1 = N; // N1
                                            break;
                                        case 1:
                                            tensile.N2 = N; // N2
                                            break;
                                        case 2:
                                            tensile.N3 = N; // N3
                                            break;
                                        case 3:
                                            if (item.RetestN1 && !tensile.N1R1.HasValue)
                                            {
                                                tensile.N1R1 = N; // N1R1
                                            }
                                            else if (item.RetestN2 && !tensile.N2R1.HasValue)
                                            {
                                                tensile.N2R1 = N; // N2R1
                                            }
                                            else if (item.RetestN3 && !tensile.N3R1.HasValue)
                                            {
                                                tensile.N3R1 = N; // N3R1
                                            }
                                            break;
                                        case 4:
                                            if (item.RetestN1 && !tensile.N1R2.HasValue)
                                            {
                                                tensile.N1R2 = N; // N1R2
                                            }
                                            else if (item.RetestN2 && !tensile.N2R2.HasValue)
                                            {
                                                tensile.N2R2 = N; // N2R2
                                            }
                                            else if (item.RetestN3 && !tensile.N3R2.HasValue)
                                            {
                                                tensile.N3R2 = N; // N3R2
                                            }
                                            break;
                                        case 5:
                                            if (item.RetestN2 && !tensile.N2R1.HasValue)
                                            {
                                                tensile.N2R1 = N; // N2R1
                                            }
                                            else if (item.RetestN3 && !tensile.N3R1.HasValue)
                                            {
                                                tensile.N3R1 = N; // N3R1
                                            }
                                            break;
                                        case 6:
                                            if (item.RetestN2 && !tensile.N2R2.HasValue)
                                            {
                                                tensile.N2R2 = N; // N2R2
                                            }
                                            else if (item.RetestN3 && !tensile.N3R2.HasValue)
                                            {
                                                tensile.N3R2 = N; // N3R2
                                            }
                                            break;
                                        case 7:
                                            tensile.N3R1 = N; // N3R1
                                            break;
                                        case 8:
                                            tensile.N3R2 = N; // N3R2
                                            break;
                                    }

                                    #endregion

                                    #region Elongations AtBreak

                                    N = decimal.TryParse(row["F3"].ToString(), out d) ? d : new decimal?();

                                    switch (currRow)
                                    {
                                        case 0:
                                            atBreak.N1 = N; // N1
                                            break;
                                        case 1:
                                            atBreak.N2 = N; // N2
                                            break;
                                        case 2:
                                            atBreak.N3 = N; // N3
                                            break;
                                        case 3:
                                            if (item.RetestN1 && !atBreak.N1R1.HasValue)
                                            {
                                                atBreak.N1R1 = N; // N1R1
                                            }
                                            else if (item.RetestN2 && !atBreak.N2R1.HasValue)
                                            {
                                                atBreak.N2R1 = N; // N2R1
                                            }
                                            else if (item.RetestN3 && !atBreak.N3R1.HasValue)
                                            {
                                                atBreak.N3R1 = N; // N3R1
                                            }
                                            break;
                                        case 4:
                                            if (item.RetestN1 && !atBreak.N1R2.HasValue)
                                            {
                                                atBreak.N1R2 = N; // N1R2
                                            }
                                            else if (item.RetestN2 && !atBreak.N2R2.HasValue)
                                            {
                                                atBreak.N2R2 = N; // N2R2
                                            }
                                            else if (item.RetestN3 && !atBreak.N3R2.HasValue)
                                            {
                                                atBreak.N3R2 = N; // N3R2
                                            }
                                            break;
                                        case 5:
                                            if (item.RetestN2 && !atBreak.N2R1.HasValue)
                                            {
                                                atBreak.N2R1 = N; // N2R1
                                            }
                                            else if (item.RetestN3 && !atBreak.N3R1.HasValue)
                                            {
                                                atBreak.N3R1 = N; // N3R1
                                            }
                                            break;
                                        case 6:
                                            if (item.RetestN2 && !atBreak.N2R2.HasValue)
                                            {
                                                atBreak.N2R2 = N; // N2R2
                                            }
                                            else if (item.RetestN3 && !atBreak.N3R2.HasValue)
                                            {
                                                atBreak.N3R2 = N; // N3R2
                                            }
                                            break;
                                        case 7:
                                            atBreak.N3R1 = N; // N3R1
                                            break;
                                        case 8:
                                            atBreak.N3R2 = N; // N3R2
                                            break;
                                    }

                                    #endregion

                                    #region Elongations AtLoad

                                    int iCol = 0;
                                    foreach (DataColumn col in elongCols)
                                    {
                                        var atLoad = subProps.FindByElong(elongNs[iCol]);

                                        N = decimal.TryParse(row[col].ToString(), out d) ? d : new decimal?();

                                        switch (currRow)
                                        {
                                            case 0:
                                                atLoad.N1 = N; // N1
                                                break;
                                            case 1:
                                                atLoad.N2 = N; // N2
                                                break;
                                            case 2:
                                                atLoad.N3 = N; // N3
                                                break;
                                            case 3:
                                                if (item.RetestN1 && !atLoad.N1R1.HasValue)
                                                {
                                                    atLoad.N1R1 = N; // N1R1
                                                }
                                                else if (item.RetestN2 && !atLoad.N2R1.HasValue)
                                                {
                                                    atLoad.N2R1 = N; // N2R1
                                                }
                                                else if (item.RetestN3 && !atLoad.N3R1.HasValue)
                                                {
                                                    atLoad.N3R1 = N; // N3R1
                                                }
                                                break;
                                            case 4:
                                                if (item.RetestN1 && !atLoad.N1R2.HasValue)
                                                {
                                                    atLoad.N1R2 = N; // N1R2
                                                }
                                                else if (item.RetestN2 && !atLoad.N2R2.HasValue)
                                                {
                                                    atLoad.N2R2 = N; // N2R2
                                                }
                                                else if (item.RetestN3 && !atLoad.N3R2.HasValue)
                                                {
                                                    atLoad.N3R2 = N; // N3R2
                                                }
                                                break;
                                            case 5:
                                                if (item.RetestN2 && !atLoad.N2R1.HasValue)
                                                {
                                                    atLoad.N2R1 = N; // N2R1
                                                }
                                                else if (item.RetestN3 && !atLoad.N3R1.HasValue)
                                                {
                                                    atLoad.N3R1 = N; // N3R1
                                                }
                                                break;
                                            case 6:
                                                if (item.RetestN2 && !atLoad.N2R2.HasValue)
                                                {
                                                    atLoad.N2R2 = N; // N2R2
                                                }
                                                else if (item.RetestN3 && !atLoad.N3R2.HasValue)
                                                {
                                                    atLoad.N3R2 = N; // N3R2
                                                }
                                                break;
                                            case 7:
                                                atLoad.N3R1 = N; // N3R1
                                                break;
                                            case 8:
                                                atLoad.N3R2 = N; // N3R2
                                                break;
                                        }
                                    }

                                    #endregion

                                    currRow++; // step to next row
                                }

                                readRow += totalRow; // update readed row.
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

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The UniTestTensileElongation item to save.</param>
        /// <returns></returns>
        public static NDbResult Save(UnitTestTensileElongation value, UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            int totalSP = 0;
            if (value.SP1.HasValue) totalSP++;
            if (value.SP2.HasValue) totalSP++;
            if (value.SP3.HasValue) totalSP++;
            if (value.SP4.HasValue) totalSP++;
            if (value.SP5.HasValue) totalSP++;
            if (value.SP6.HasValue) totalSP++;
            if (value.SP7.HasValue) totalSP++;

            var p = new DynamicParameters();

            p.Add("@LotNo", value.LotNo);
            p.Add("@itemcode", value.SampleName);

            p.Add("@SP1", value.SP1);
            p.Add("@SP2", value.SP2);
            p.Add("@SP3", value.SP3);
            p.Add("@SP4", value.SP4);
            p.Add("@SP5", value.SP5);
            p.Add("@SP6", value.SP6);
            p.Add("@SP7", value.SP7);

            p.Add("@TotalSP", totalSP);

            p.Add("@testdate", value.TestDate);

            p.Add("@testbyid", value.TestById);
            p.Add("@testbyname", value.Operator);

            p.Add("@loaddate", DateTime.Now);
            p.Add("@loadby", (null != user) ? user.FullName : null);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("Ex_SaveTensileElongationHead", p, commandType: CommandType.StoredProcedure);
                ret.Success();
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }
            
            // Save Tensile Strength
            //if (ret.ErrNum != 0) return ret;

            if (null != value.TensileStrengths  &&  value.TensileStrengths.Count > 0)
            {
                int iCnt = 0;
                foreach (var r in value.TensileStrengths) 
                {
                    p = new DynamicParameters();
                    p.Add("@LotNo", r.LotNo);
                    p.Add("@spno", r.SPNo);
                    p.Add("@n1", r.N1);
                    p.Add("@n2", r.N2);
                    p.Add("@n3", r.N3);

                    p.Add("@n1r1", r.N1R1);
                    p.Add("@n1r2", r.N1R2);
                    p.Add("@n2r1", r.N2R1);
                    p.Add("@n2r2", r.N2R2);
                    p.Add("@n3r1", r.N3R1);
                    p.Add("@n3r2", r.N3R2);

                    p.Add("@sampletype", value.SampleType);

                    p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                    try
                    {
                        cnn.Execute("Ex_SaveTensile", p, commandType: CommandType.StoredProcedure);
                        ret.Success();
                        // Set error number/message
                        ret.ErrNum = p.Get<int>("@errNum");
                        ret.ErrMsg = p.Get<string>("@errMsg");

                        if (ret.ErrNum == 0) iCnt++;
                    }
                    catch (Exception ex)
                    {
                        med.Err(ex);
                        // Set error number/message
                        ret.ErrNum = 9999;
                        ret.ErrMsg = ex.Message;
                    }
                }
                if (iCnt == value.TensileStrengths.Count)
                {
                    ret.Success();
                }
            }

            // Save Elongation
            //if (ret.ErrNum != 0) return ret;
            
            if (null != value.Elongations && value.Elongations.Count > 0)
            {
                int iCnt = 0;
                foreach (var r in value.Elongations)
                {
                    foreach (var s in r.SubProperties)
                    {
                        p = new DynamicParameters();

                        p.Add("@LotNo", s.LotNo);
                        // Elongation Break Proepty No = 2, Elongation Load Proepty No = 3
                        p.Add("@propertyno", s.PropertyNo);
                        p.Add("@spno", s.SPNo);
                        p.Add("@loadn", s.LoadN);

                        p.Add("@n1", s.N1);
                        p.Add("@n2", s.N2);
                        p.Add("@n3", s.N3);

                        p.Add("@n1r1", s.N1R1);
                        p.Add("@n1r2", s.N1R2);
                        p.Add("@n2r1", s.N2R1);
                        p.Add("@n2r2", s.N2R2);
                        p.Add("@n3r1", s.N3R1);
                        p.Add("@n3r2", s.N3R2);

                        p.Add("@sampletype", value.SampleType);

                        p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
                        p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                        try
                        {
                            cnn.Execute("Ex_SaveElongation", p, commandType: CommandType.StoredProcedure);
                            ret.Success();
                            // Set error number/message
                            ret.ErrNum = p.Get<int>("@errNum");
                            ret.ErrMsg = p.Get<string>("@errMsg");

                            if (ret.ErrNum == 0) iCnt++;
                        }
                        catch (Exception ex)
                        {
                            med.Err(ex);
                            // Set error number/message
                            ret.ErrNum = 9999;
                            ret.ErrMsg = ex.Message;
                        }
                    }
                }
                if (iCnt == value.Elongations.Count)
                {
                    ret.Success();
                }
            }

            return ret;
        }

        #endregion
    }

    #endregion
}
