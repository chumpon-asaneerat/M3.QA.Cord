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

#endregion

namespace M3.QA.Models
{
    #region UnitTestAdhesionForce

    /// <summary>
    /// The UnitTestAdhesionForce class.
    /// </summary>
    public class UnitTestAdhesionForce : NInpc
    {
        #region Private Methods

        private void ParseComment1()
        {
            var p = UniTestSPParser.Parse(Comment1);
            NoOfSP = (null != p) ? p.Items.Count : 0;

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

            int NCnt = 2;
            for (int i = 0; i < NoOfSP; i++)
            {
                switch (i)
                {
                    case 0:
                        {
                            SP1 = p.Items[i].SP;
                            NCnt1 = NCnt;
                            RCnt1 = p.Items[i].RCnt;
                            break;
                        }
                    case 1:
                        {
                            SP2 = p.Items[i].SP;
                            NCnt2 = NCnt;
                            RCnt2 = p.Items[i].RCnt;
                            break;
                        }
                    case 2:
                        {
                            SP3 = p.Items[i].SP;
                            NCnt3 = NCnt;
                            RCnt3 = p.Items[i].RCnt;
                            break;
                        }
                    case 3:
                        {
                            SP4 = p.Items[i].SP;
                            NCnt4 = NCnt;
                            RCnt4 = p.Items[i].RCnt;
                            break;
                        }
                    case 4:
                        {
                            SP5 = p.Items[i].SP;
                            NCnt5 = NCnt;
                            RCnt5 = p.Items[i].RCnt;
                            break;
                        }
                    case 5:
                        {
                            SP6 = p.Items[i].SP;
                            NCnt6 = NCnt;
                            RCnt6 = p.Items[i].RCnt;
                            break;
                        }
                    case 6:
                        {
                            SP7 = p.Items[i].SP;
                            NCnt7 = NCnt;
                            RCnt7 = p.Items[i].RCnt;
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

            Raise(() => this.NCnt1);
            Raise(() => this.NCnt2);
            Raise(() => this.NCnt3);
            Raise(() => this.NCnt4);
            Raise(() => this.NCnt5);
            Raise(() => this.NCnt6);
            Raise(() => this.NCnt7);

            Raise(() => this.RCnt1);
            Raise(() => this.RCnt2);
            Raise(() => this.RCnt3);
            Raise(() => this.RCnt4);
            Raise(() => this.RCnt5);
            Raise(() => this.RCnt6);
            Raise(() => this.RCnt7);
        }

        private void PrepareAdhesionForces()
        {
            Items = new List<UnitTestAdhesionForceProperty>();
            for (int i = 0; i < NoOfSP; i++)
            {
                var inst = new UnitTestAdhesionForceProperty();

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
                Items.Add(inst);
            }
        }

        public void PrepareProperties()
        {
            PrepareAdhesionForces();
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

        public int NoOfSample { get; set; } = 2; // Fixed
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

        #region Test Cnt

        public int NCnt1 { get; set; } = 3;
        public int NCnt2 { get; set; } = 3;
        public int NCnt3 { get; set; } = 3;
        public int NCnt4 { get; set; } = 3;
        public int NCnt5 { get; set; } = 3;
        public int NCnt6 { get; set; } = 3;
        public int NCnt7 { get; set; } = 3;

        #endregion

        #region Retest Cnt

        public int RCnt1 { get; set; }
        public int RCnt2 { get; set; }
        public int RCnt3 { get; set; }
        public int RCnt4 { get; set; }
        public int RCnt5 { get; set; }
        public int RCnt6 { get; set; }
        public int RCnt7 { get; set; }

        #endregion

        #region Test Properties

        #region Items

        /// <summary>
        /// Gets or sets each Items Properties
        /// </summary>
        public List<UnitTestAdhesionForceProperty> Items { get; set; }

        #endregion

        #endregion

        #endregion

        #region Static Methods

        public static UniTestImportResult<UnitTestAdhesionForce> Import(string fileName)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            UniTestImportResult<UnitTestAdhesionForce> result = new UniTestImportResult<UnitTestAdhesionForce>();
            result.IsValid = false;
            result.ErrMsg = "unknown";
            result.Value = null;

            UnitTestAdhesionForce inst = null;

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
                        inst = new UnitTestAdhesionForce();
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

                    if (string.IsNullOrWhiteSpace(inst.TestType) || inst.TestType != "Peeling test")
                    {
                        // 
                        result.IsValid = false;
                        result.ErrMsg = "Invalid file , Not Peeling test file.";
                        result.Value = null;
                        return result;
                    }

                    #region Data
                    
                    string sheetName2 = "Data";
                    var table2 = conn.Query("Select * from [" + sheetName2 + "$]").Result;
                    if (null != table2)
                    {
                        inst.PrepareProperties(); // prepare properties

                        // Loop data row.
                        int iSP = 0;
                        int iCnt = 1;
                        int iMax = 2;
                        decimal? N;
                        decimal d;
                        for (int iRow = 3; iRow < table2.Rows.Count; iRow++)
                        {
                            DataRow row = table2.Rows[iRow];

                            #region Find Max Test + Retest

                            switch (iSP)
                            {
                                case 0:
                                    iMax = inst.NCnt1 + inst.RCnt1;
                                    break;
                                case 1:
                                    iMax = inst.NCnt2 + inst.RCnt2;
                                    break;
                                case 2:
                                    iMax = inst.NCnt3 + inst.RCnt3;
                                    break;
                                case 3:
                                    iMax = inst.NCnt4 + inst.RCnt4;
                                    break;
                                case 4:
                                    iMax = inst.NCnt5 + inst.RCnt5;
                                    break;
                                case 5:
                                    iMax = inst.NCnt6 + inst.RCnt6;
                                    break;
                                case 6:
                                    iMax = inst.NCnt7 + inst.RCnt7;
                                    break;
                                default:
                                    iMax = 3;
                                    break;
                            }

                            #endregion

                            #region Items

                            if (null != inst.Items &&
                                inst.Items.Count > 0 && iSP < inst.Items.Count)
                            {
                                N = decimal.TryParse(row["F4"].ToString(), out d) ? d : new decimal?();

                                switch (iCnt)
                                {
                                    case 1:
                                        {
                                            inst.Items[iSP].PeakPoint.N1 = N;
                                            break;
                                        }
                                    case 2:
                                        {
                                            inst.Items[iSP].PeakPoint.N2 = N;
                                            break;
                                        }
                                    case 3:
                                        {
                                            inst.Items[iSP].PeakPoint.R1 = N;
                                            break;
                                        }
                                    case 4:
                                        {
                                            inst.Items[iSP].PeakPoint.R2 = N;
                                            break;
                                        }
                                }
                            }

                            #endregion

                            iCnt++;

                            if (iCnt > iMax)
                            {
                                iCnt = 1; // Reset to N1

                                inst.Items[iSP].UpdateProperties(); // calculate formula

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

        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The UnitTestAdhesionForce item to save.</param>
        /// <returns></returns>
        public static NDbResult Save(UnitTestAdhesionForce value, UserInfo user)
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
                cnn.Execute("Ex_SaveAdhesionForceHead", p, commandType: CommandType.StoredProcedure);
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

            // Save Adhesion Force
            //if (ret.ErrNum != 0) return ret;

            if (null != value.Items && value.Items.Count > 0)
            {
                int iCnt = 0;
                foreach (var r in value.Items)
                {
                    p = new DynamicParameters();
                    p.Add("@LotNo", r.LotNo);
                    p.Add("@spno", r.SPNo);
                    p.Add("@peakn1", (null != r.PeakPoint) ? r.PeakPoint.N1 : new decimal?());
                    p.Add("@peakn2", (null != r.PeakPoint) ? r.PeakPoint.N2 : new decimal?());
                    p.Add("@adhesionn1", (null != r.AdhesionForce) ? r.AdhesionForce.N1 : new decimal?());
                    p.Add("@adhesionn2", (null != r.AdhesionForce) ? r.AdhesionForce.N2 : new decimal?());
                    p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

                    try
                    {
                        cnn.Execute("Ex_SaveAdhesionForce", p, commandType: CommandType.StoredProcedure);
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
                if (iCnt == value.Items.Count)
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
