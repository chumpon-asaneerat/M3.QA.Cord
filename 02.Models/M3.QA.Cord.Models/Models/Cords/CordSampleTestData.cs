#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;
using NLib.Models;

using Dapper;
using Newtonsoft.Json;
using System.Windows.Markup;
using EPPlus.DataExtractor;

#endregion

namespace M3.QA.Models
{
    public class CordSampleTestData : NInpc
    {
        #region Public Properties

        public string LotNo { get; set; }

        public string ItemCode { get; set; }

        public int? MasterId { get; set; }
        public int? TotalSP { get; set; }
        public DateTime? StartTestDate { get; set; }

        public string Spindle { get; set; }
        public string ELongLoadN { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets CordSampleTestData by Lot No.
        /// </summary>
        /// <param name="value">The CordSampleTestData item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordSampleTestData> GetByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordSampleTestData> ret = new NDbResult<CordSampleTestData>();

            if (string.IsNullOrWhiteSpace(lotNo))
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

            var p = new DynamicParameters();

            p.Add("@LotNo", lotNo);

            try
            {
                var items = cnn.Query<CordSampleTestData>("M_CheckLotReceive", p, commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList().FirstOrDefault() : null;
                ret.Success(data);
                // Set error number/message
                ret.ErrNum = 0;
                ret.ErrMsg = "Success";
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        #endregion
    }

    public class CordTensileStrengthProperty : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordTensileStrengthProperty() : base() 
        {
            BuildData(true);
        }

        #endregion

        #region Private Methods

        private void BuildData(bool onCreate = false)
        {
            Items = new List<CordTensileStrengthData>()
            {
                new CordTensileStrengthData() 
                { 
                    No = 1,
                    N = (!onCreate) ? this.N1 : new decimal?(),
                    R = (!onCreate) ? this.R1 : new decimal?()
                },
                new CordTensileStrengthData() 
                {
                    No = 2,
                    N = (!onCreate) ? this.N2 : new decimal?(),
                    R = (!onCreate) ? this.R2 : new decimal?()
                },
                new CordTensileStrengthData() 
                { 
                    No = 3,
                    N = (!onCreate) ? this.N3 : new decimal?(),
                    R = (!onCreate) ? this.R3 : new decimal?()
                }
            };
        }

        private void UpdateDataToProperties()
        {
            if (null != Items) 
            {
                foreach (var item in Items)
                {
                    if (null == item) continue;
                    if (item.No == 1) 
                    {
                        this.N1 = item.N;
                        this.R1 = item.R;
                    }
                    else if (item.No == 2)
                    {
                        this.N2 = item.N;
                        this.R2 = item.R;
                    }
                    else if (item.No == 3)
                    {
                        this.N3 = item.N;
                        this.R3 = item.R;
                    }
                }
            }
        }

        #endregion

        #region Public Properties

        public string LotNo { get; set; }
        public int SPNo { get; set; }
        public decimal? N1 { get; set; }
        public decimal? N2 { get; set; }
        public decimal? N3 { get; set; }
        public decimal? R1 { get; set; }
        public decimal? R2 { get; set; }
        public decimal? R3 { get; set; }

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        public List<CordTensileStrengthData> Items { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets CordSampleTestData by Lot No.
        /// </summary>
        /// <param name="value">The CordSampleTestData item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordTensileStrengthProperty> GetByLotNo(string lotNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTensileStrengthProperty> ret = new NDbResult<CordTensileStrengthProperty>();

            if (string.IsNullOrWhiteSpace(lotNo))
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

            var p = new DynamicParameters();

            p.Add("@LotNo", lotNo);

            try
            {
                var items = cnn.Query<CordTensileStrengthProperty>("P_GetTensileDataByLot", p, commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList().FirstOrDefault() : null;
                if (null != data)
                {
                    data.BuildData();
                }
                ret.Success(data);
                // Set error number/message
                ret.ErrNum = 0;
                ret.ErrMsg = "Success";
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        public static NDbResult<CordTensileStrengthProperty> Save(CordTensileStrengthProperty value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTensileStrengthProperty> ret = new NDbResult<CordTensileStrengthProperty>();

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

            // sync all items to properties
            value.UpdateDataToProperties();

            var p = new DynamicParameters();

            p.Add("@LotNo", value.LotNo);
            p.Add("@spno", value.SPNo);

            p.Add("@n1", value.N1);
            p.Add("@n2", value.N2);
            p.Add("@n3", value.N3);
            p.Add("@r1", value.R1);
            p.Add("@r2", value.R2);
            p.Add("@r3", value.R3);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveTensile", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
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

            return ret;
        }

        #endregion
    }

    public class CordTensileStrengthData : NInpc
    {
        #region Private Methods

        private void CheckRange()
        {

        }

        #endregion

        #region Public Properties

        public int No { get; set; }

        public decimal? N
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () => { CheckRange(); });
            }
        }
        public decimal? R
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () => { });
            }
        }

        public bool EnableReTest { get; set; } = true;

        #endregion
    }
}
