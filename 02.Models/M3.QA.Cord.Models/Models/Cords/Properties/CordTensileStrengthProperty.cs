#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordTensileStrengthProperty

    /// <summary>
    /// The CordTensileStrengthProperty class.
    /// </summary>
    public class CordTensileStrengthProperty : CordTestProperty
    {
        #region Static Methods

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="lotNo"></param>
        /// <param name="maxSP"></param>
        /// <param name="noOfSample"></param>
        /// <param name="sp1"></param>
        /// <param name="sp2"></param>
        /// <param name="sp3"></param>
        /// <param name="sp4"></param>
        /// <param name="sp5"></param>
        /// <param name="sp6"></param>
        /// <param name="sp7"></param>
        /// <returns></returns>
        internal static List<CordTensileStrengthProperty> Create(string lotNo, int maxSP, int noOfSample,
            int? sp1, int? sp2, int? sp3, int? sp4, int? sp5, int? sp6, int? sp7)
        {
            List<CordTensileStrengthProperty> results = new List<CordTensileStrengthProperty>();
            for (int i = 1; i <= maxSP; i++)
            {
                if (i > 7) continue;

                int? SP;
                switch (i)
                {
                    case 1: SP = sp1; break;
                    case 2: SP = sp2; break;
                    case 3: SP = sp3; break;
                    case 4: SP = sp4; break;
                    case 5: SP = sp5; break;
                    case 6: SP = sp6; break;
                    case 7: SP = sp7; break;
                    default: SP = new int?(); break;
                }
                var inst = new CordTensileStrengthProperty()
                {
                    LotNo = lotNo,
                    PropertyNo = 1, // TensileStrength = 1
                    SPNo = SP,
                    NeedSP = true,
                    NoOfSample = noOfSample
                };

                results.Add(inst);
            }
            return results;
        }

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordTensileStrengthProperty src, CordTensileStrengthProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            dst.N1 = src.N1;
            dst.N2 = src.N2;
            dst.N3 = src.N3;
            dst.N4 = src.N4;
            dst.N5 = src.N5;
            dst.N6 = src.N6;
            dst.N7 = src.N7;

            dst.R1 = src.R1;
            dst.R2 = src.R2;
            dst.R3 = src.R3;
            dst.R4 = src.R4;
            dst.R5 = src.R5;
            dst.R6 = src.R6;
            dst.R7 = src.R7;

            /*
            dst.AllowN1 = src.AllowN1;
            dst.AllowN2 = src.AllowN2;
            dst.AllowN3 = src.AllowN3;
            dst.AllowN4 = src.AllowN4;
            dst.AllowN5 = src.AllowN5;
            dst.AllowN6 = src.AllowN6;
            dst.AllowN7 = src.AllowN7;

            dst.AllowR1 = src.AllowR1;
            dst.AllowR2 = src.AllowR2;
            dst.AllowR3 = src.AllowR3;
            dst.AllowR4 = src.AllowR4;
            dst.AllowR5 = src.AllowR5;
            dst.AllowR6 = src.AllowR6;
            dst.AllowR7 = src.AllowR7;

            dst.Avg = src.Avg;
            */
        }

        /// <summary>
        /// Gets CordTensileStrengthProperty by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <param name="masterId">The MasterId.</param>
        /// <returns></returns>
        public static NDbResult<List<CordTensileStrengthProperty>> GetsByLotNo(string lotNo, int masterId)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordTensileStrengthProperty>> ret = new NDbResult<List<CordTensileStrengthProperty>>();

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
                var items = cnn.Query<CordTensileStrengthProperty>("P_GetTensileDataByLot",
                    p, commandType: CommandType.StoredProcedure).ToList();
                ret.Success(items);
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
        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

    #endregion
}
