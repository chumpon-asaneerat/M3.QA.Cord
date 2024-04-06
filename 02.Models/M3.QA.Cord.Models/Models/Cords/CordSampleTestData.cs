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
    #region CordSampleTestData

    /// <summary>
    /// The CordSampleTestData class.
    /// </summary>
    public class CordSampleTestData : NInpc
    {
        #region Public Properties

        public string LotNo { get; set; }

        public string ItemCode { get; set; }

        public int? MasterId { get; set; }

        public int? SP1 { get; set; }
        public int? SP2 { get; set; }
        public int? SP3 { get; set; }
        public int? SP4 { get; set; }
        public int? SP5 { get; set; }
        public int? SP6 { get; set; }
        public int? SP7 { get; set; }

        public int? TotalSP { get; set; }
        public DateTime? StartTestDate { get; set; }

        public string Spindle { get; set; }
        public string ELongLoadN { get; set; }

        public bool CanEditStartDate { get; set; }

        /// <summary>The Tensile Strengths Items.</summary>
        public List<CordTensileStrengthProperty> TensileStrengths { get; set; }
        /// <summary>The Elongations Items.</summary>
        public List<CordElongationProperty> Elongations { get; set; }
        /// <summary>The AdhesionForces Items.</summary>
        public List<CordAdhesionForceProperty> AdhesionForces { get; set; }

        #endregion

        #region Private Methods

        private void InitTensileStrengths()
        {
            TensileStrengths = CordTensileStrengthProperty.Create(this);
        }

        private void InitElongations()
        {
            Elongations = CordElongationProperty.Create(this);
        }

        private void InitAdhesionForces()
        {
            AdhesionForces = CordAdhesionForceProperty.Create(this);
        }

        private void InitTestProperties()
        {
            if (TotalSP.HasValue && MasterId.HasValue)
            {
                InitTensileStrengths();
                InitElongations();
                InitAdhesionForces();
            }
        }

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

                if (null != data)
                {
                    // already has date so cannot edit.
                    data.CanEditStartDate = (data.StartTestDate.HasValue) ? false : true;
                    data.InitTestProperties();
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

        public static NDbResult<CordSampleTestData> Save(CordSampleTestData value, 
            UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordSampleTestData> ret = new NDbResult<CordSampleTestData>();

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

            try
            {
                NDbResult res = null;
                value.TensileStrengths.ForEach(x => 
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = CordTensileStrengthProperty.Save(x);
                    if (null == res || !res.Ok) return;
                });

                value.Elongations.ForEach(x =>
                {
                    foreach (var item in x.SubProperties)
                    {
                        item.EditBy = (null != user) ? user.FullName : null;
                        item.EditDate = DateTime.Now;
                        res = CordElongationSubProperty.Save(item);
                        if (null == res || !res.Ok) return;
                    }
                });

                value.AdhesionForces.ForEach(x => 
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = CordAdhesionForceProperty.Save(x);
                    if (null == res || !res.Ok) return;
                });


                if (null == res || !res.Ok)
                {
                    if (null == res)
                    {
                        ret.ErrNum = -1;
                        ret.ErrMsg = "Unknown Error.";
                        ret.data = value;
                    }
                    else
                    {
                        ret.ErrNum = res.ErrNum;
                        ret.ErrMsg = res.ErrMsg;
                        ret.data = value;
                    }
                }
                else
                {
                    ret.Success(value);

                    // Set error number/message
                    ret.ErrNum = 0;
                    ret.ErrMsg = "Success";
                }
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