#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
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
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordSampleTestData() : base()
        {
            ShowTensileStrengths = false;
            ShowElongations = false;
            ShowAdhesionForces = false;
            ShowShrinkageForces = false;
            ShowThicknesses = false;
            ShowCord1stTwistingNumbers = false;
            ShowCord2ndTwistingNumbers = false;
            ShowRPUs = false;
            ShowShrinkagePcts = false;
            ShowDenierMoistureWeights = false;
        }

        #endregion

        #region Public Properties

        #region Common

        public string LotNo { get; set; }
        public string ItemCode { get; set; }

        public int? MasterId { get; set; }

        public decimal? PiNoSL { get; set; }

        public int? SP1 { get; set; }
        public int? SP2 { get; set; }
        public int? SP3 { get; set; }
        public int? SP4 { get; set; }
        public int? SP5 { get; set; }
        public int? SP6 { get; set; }
        public int? SP7 { get; set; }

        public int? TotalSP { get; set; }
        public DateTime? StartTestDate { get; set; }

        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        public string Spindle { get; set; }
        public string ELongLoadN { get; set; }

        public string YarnType { get; set; } // Polyester, Nylon

        public bool CanEditStartDate { get; set; }

        public string ProductionLotNo { get; set; }
        public string ReceiveBy { get; set; }
        public DateTime? ReceiveDate { get; set; }
        public string Customer { get; set; }

        #endregion

        #region Test Tab Visibility

        public bool ShowTensileStrengths 
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleTensileStrengths); }); }
        }
        public Visibility VisibleTensileStrengths { get { return (ShowTensileStrengths) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowElongations
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleElongations); }); }
        }
        public Visibility VisibleElongations { get { return (ShowElongations) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowAdhesionForces
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleAdhesionForces); }); }
        }
        public Visibility VisibleAdhesionForces { get { return (ShowAdhesionForces) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowShrinkageForces
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleShrinkageForces); }); }
        }
        public Visibility VisibleShrinkageForces { get { return (ShowShrinkageForces) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowThicknesses
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleThicknesses); }); }
        }
        public Visibility VisibleThicknesses { get { return (ShowThicknesses) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowCord1stTwistingNumbers
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleCord1stTwistingNumbers); }); }
        }
        public Visibility VisibleCord1stTwistingNumbers { get { return (ShowCord1stTwistingNumbers) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowCord2ndTwistingNumbers
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleCord2ndTwistingNumbers); }); }
        }
        public Visibility VisibleCord2ndTwistingNumbers { get { return (ShowCord2ndTwistingNumbers) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowRPUs {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleRPUs); }); }
        }
        public Visibility VisibleRPUs { get { return (ShowRPUs) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowShrinkagePcts
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleShrinkagePcts); }); }
        }
        public Visibility VisibleShrinkagePcts { get { return (ShowShrinkagePcts) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        public bool ShowDenierMoistureWeights
        {
            get { return Get<bool>(); }
            set { Set(value, () => { Raise(() => this.VisibleDenierMoistureWeights); }); }
        }
        public Visibility VisibleDenierMoistureWeights { get { return (ShowDenierMoistureWeights) ? Visibility.Visible : Visibility.Collapsed; } set { } }

        #endregion

        #region Spec

        /// <summary>The Spec.</summary>
        public List<CordTestSpec> Specs { get; set; }

        #endregion

        #region Test Properties

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
        /// <summary>The RPU Items.</summary>
        public List<CordRPU> RPUs { get; set; }
        /// <summary>The Shrinkage % Items.</summary>
        public List<CordShrinkagePct> ShrinkagePcts { get; set; }
        /// <summary>The Denier, Moisture regain, Weight Items.</summary>
        public List<CordDenierMoistureWeight> DenierMoistureWeights { get; set; }

        #endregion

        #endregion

        #region Private Methods

        private void InitSpecs()
        {
            var specs = CordTestSpec.Gets(this.MasterId).Value();
            Specs = (null != specs) ? specs : new List<CordTestSpec>();
        }

        public void InitSPs()
        {
            // clear sp
            this.SP1 = new int?();
            this.SP2 = new int?();
            this.SP3 = new int?();
            this.SP4 = new int?();
            this.SP5 = new int?();
            this.SP6 = new int?();
            this.SP7 = new int?();

            var activeSPs = Utils.P_GetActiveSPByLot.Gets(this.LotNo).Value();
            if (null == activeSPs || activeSPs.Count <= 0) return;
            int iCnt = 1;
            foreach (var sp in activeSPs) 
            {
                if (iCnt == 1)
                {
                    this.SP1 = sp.SP;
                }
                else if (iCnt == 2)
                {
                    this.SP2 = sp.SP;
                }
                else if (iCnt == 3)
                {
                    this.SP3 = sp.SP;
                }
                else if (iCnt == 4)
                {
                    this.SP4 = sp.SP;
                }
                else if (iCnt == 5)
                {
                    this.SP5 = sp.SP;
                }
                else if (iCnt == 6)
                {
                    this.SP6 = sp.SP;
                }
                else if (iCnt == 7)
                {
                    this.SP7 = sp.SP;
                }
                iCnt++;
            }
        }

        private void InitTestProperties()
        {
            ShowTensileStrengths = false;
            ShowElongations = false;
            ShowAdhesionForces = false;
            ShowShrinkageForces = false;
            ShowThicknesses = false;
            ShowCord1stTwistingNumbers = false;
            ShowCord2ndTwistingNumbers = false;
            ShowRPUs = false;
            ShowShrinkagePcts = false;
            ShowDenierMoistureWeights = false;

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

                        ShowTensileStrengths = (null != item && item.NoSample > 0);
                    }
                    // Elongation PropertyNo = 2 (break), 3 (load)
                    {
                        var breakItem = TotalNs.Find((x) => { return x.PropertyNo == 2; });
                        var loadItem = TotalNs.Find((x) => { return x.PropertyNo == 3; });
                        Elongations = CordElongation.Create(this, breakItem, loadItem);

                        ShowElongations = ((null != breakItem && breakItem.NoSample > 0) || 
                            (null != loadItem && loadItem.NoSample > 0));
                    }
                    // Adhesion Force PropertyNo = 4
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 4; });
                        AdhesionForces = CordAdhesionForce.Create(this, item);

                        ShowAdhesionForces = (null != item && item.NoSample > 0);
                    }
                    // Shrinkage Force PropertyNo = 5
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 5; });
                        ShrinkageForces = CordShrinkageForce.Create(this, item);

                        ShowShrinkageForces = (null != item && item.NoSample > 0);
                    }
                    // Thickness PropertyNo = 9
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 9; });
                        Thicknesses = CordThickness.Create(this, item);

                        ShowThicknesses = (null != item && item.NoSample > 0);
                    }
                    // 1st Twisting Number PropertyNo = 7
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 7; });
                        Cord1stTwistingNumbers = Cord1stTwistingNumber.Create(this, item);

                        ShowCord1stTwistingNumbers = (null != item && item.NoSample > 0);
                    }
                    // 2nd Twisting Number PropertyNo = 8
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 8; });
                        Cord2ndTwistingNumbers = Cord2ndTwistingNumber.Create(this, item);

                        ShowCord2ndTwistingNumbers = (null != item && item.NoSample > 0);
                    }
                    // RPU PropertyNo = 12
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 12; });
                        RPUs = CordRPU.Create(this, item);

                        ShowRPUs = (null != item && item.NoSample > 0);
                    }
                    // Shrinkage% PropertyNo = 6
                    {
                        var item = TotalNs.Find((x) => { return x.PropertyNo == 6; });
                        ShrinkagePcts = CordShrinkagePct.Create(this, item);

                        ShowShrinkagePcts = (null != item && item.NoSample > 0);
                    }
                    // DenierMoistureWeight
                    // - Denier PropertyNo = 10
                    // - Moisture regain PropertyNo = 11
                    // - Weight PropertyNo = 14
                    {
                        var itemDenier = TotalNs.Find((x) => { return x.PropertyNo == 10; });
                        var itemMoisture = TotalNs.Find((x) => { return x.PropertyNo == 11; });
                        var itemWeight = TotalNs.Find((x) => { return x.PropertyNo == 14; });

                        DenierMoistureWeights = CordDenierMoistureWeight.Create(this, 
                            itemDenier, itemMoisture, itemWeight);

                        ShowDenierMoistureWeights = ((null != itemDenier && itemDenier.NoSample > 0) ||
                            (null != itemMoisture && itemMoisture.NoSample > 0) || 
                            (null != itemWeight && itemWeight.NoSample > 0));
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
                    // Init spec
                    data.InitSpecs();
                    // Init SPs
                    data.InitSPs();
                    // Init related properties
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

        private static NDbResult<CordSampleTestData> SaveHead(CordSampleTestData value, QA.Models.UserInfo user)
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

            // set user
            value.EditBy = (null != user) ? user.FullName : null;
            value.EditDate = DateTime.Now;

            var p = new DynamicParameters();

            p.Add("@LotNo", value.LotNo);
            p.Add("@masterid", value.MasterId);

            p.Add("@pino", value.PiNoSL);
            p.Add("@testdate", value.StartTestDate);

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveHeadTesingCord", p, commandType: CommandType.StoredProcedure);
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
                // Save head
                var hres = SaveHead(value, user);
                if (null == hres || !hres.Ok)
                {
                    return hres;
                }

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

                value.RPUs.ForEach(x =>
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = CordRPU.Save(x);
                    if (null == res || !res.Ok) return;
                });

                value.ShrinkagePcts.ForEach(x =>
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = CordShrinkagePct.Save(x);
                    if (null == res || !res.Ok) return;
                });

                value.DenierMoistureWeights.ForEach(x =>
                {
                    x.EditBy = (null != user) ? user.FullName : null;
                    x.EditDate = DateTime.Now;
                    res = CordDenierMoistureWeight.Save(x);
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
