#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
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

        public List<Utils.M_GetPropertyTotalNByItem> TotalNs { get; set; }

        /// <summary>The Tensile Strengths Items.</summary>
        public List<CordTensileStrength> TensileStrengths { get; set; }
        /// <summary>The Elongations Items.</summary>
        public List<CordElongation> Elongations { get; set; }
        /// <summary>The AdhesionForces Items.</summary>
        public List<CordAdhesionForce> AdhesionForces { get; set; }
        /// <summary>The ShrinkageForce Items.</summary>
        public List<CordShrinkageForce> ShrinkageForces { get; set; }
        /// <summary>The Thickness Items.</summary>
        public List<CordThickness> Thicknesses { get; set; }

        /// <summary>The 1st Twisting Number Items.</summary>
        public List<Cord1stTwistingNumber> Cord1stTwistingNumbers { get; set; }
        /// <summary>The 2nd Twisting Number Items.</summary>
        public List<Cord2ndTwistingNumber> Cord2ndTwistingNumbers { get; set; }

        #endregion

        #region Private Methods

        private void InitTestProperties()
        {
            // Get Total N(s)
            if (MasterId.HasValue)
            {
                TotalNs = Utils.M_GetPropertyTotalNByItem.Gets(MasterId.Value).Value();
                if (null != TotalNs && TotalNs.Count > 0)
                {
                    // Tensile Strengths PropertyNo = 1
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 1; });
                        TensileStrengths = CordTensileStrength.Create(this, item);
                    }
                    // Elongation PropertyNo = 2 (break), 3 (load)
                    {
                        var breakItem = TotalNs.Find((x) => { return x.PropertyNo == 2; });
                        var loadItem = TotalNs.Find((x) => { return x.PropertyNo == 3; });
                        Elongations = CordElongation.Create(this, breakItem, loadItem);
                    }
                    // Adhesion Force PropertyNo = 4
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 4; });
                        AdhesionForces = CordAdhesionForce.Create(this, item);
                    }
                    // Shrinkage Force PropertyNo = 5
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 5; });
                        ShrinkageForces = CordShrinkageForce.Create(this, item);
                    }
                    // Thickness PropertyNo = 9
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 9; });
                        Thicknesses = CordThickness.Create(this, item);
                    }
                    // 1st Twisting Number PropertyNo = 7
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 7; });
                        Cord1stTwistingNumbers = Cord1stTwistingNumber.Create(this, item);
                    }
                    // 2nd Twisting Number PropertyNo = 8
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 8; });
                        Cord2ndTwistingNumbers = Cord2ndTwistingNumber.Create(this, item);
                    }
                }
            }
        }

        #endregion

        #region Static Methods

        #region GetByLotNo

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
                    // Init relatedd properties
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

        #endregion

        #region Save

        public static NDbResult<CordSampleTestData> Save(CordSampleTestData value,
            QA.Models.UserInfo user)
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
                    res = CordTensileStrength.Save(x);
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
                    res = CordAdhesionForce.Save(x);
                    if (null == res || !res.Ok) return;
                });

                value.ShrinkageForces.ForEach(x =>
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = CordShrinkageForce.Save(x);
                    if (null == res || !res.Ok) return;
                });

                value.Thicknesses.ForEach(x =>
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = CordThickness.Save(x);
                    if (null == res || !res.Ok) return;
                });

                value.Cord1stTwistingNumbers.ForEach(x =>
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = Cord1stTwistingNumber.Save(x);
                    if (null == res || !res.Ok) return;
                });

                value.Cord2ndTwistingNumbers.ForEach(x =>
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = Cord2ndTwistingNumber.Save(x);
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

        #endregion
    }

    #endregion
}
