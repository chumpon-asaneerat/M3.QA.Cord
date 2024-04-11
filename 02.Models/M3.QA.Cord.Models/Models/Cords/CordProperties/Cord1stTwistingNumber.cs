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
    #region Cord1stTwistingNumber

    /// <summary>
    /// The Cord 1st Twisting Number class.
    /// </summary>
    public class Cord1stTwistingNumber : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public Cord1stTwistingNumber() : base()
        {
            Item = new NRTestProperty();
            // only Item change need to calc formula for TM and TM10cm
            Item.ValueChanges = CalculateFormulaFromItem;

            TM = new NRTestProperty();
            // only TM change need to calc formula for Item and TM10cm
            TM.ValueChanges = CalculateFormulaFromTM;

            TM10cm = new NRTestProperty();
            // only TCM change need to calc formula for Item and TM
            TM10cm.ValueChanges = CalculateFormulaFromTCM;
        }

        #endregion

        #region Private Methods

        private bool onCalc = false;

        private void CalculateFormulaFromItem()
        {
            if (onCalc) return; // lock circular reference dead lock
            onCalc = true;

            if (null != Item && null != TM && null != TM10cm)
            {
                // TM = Item / 2
                TM.N1 = (Item.N1.HasValue) ? Item.N1.Value * 2 : new decimal?();
                TM.N2 = (Item.N2.HasValue) ? Item.N2.Value * 2 : new decimal?();
                TM.N3 = (Item.N3.HasValue) ? Item.N3.Value * 2 : new decimal?();
                TM.R1 = (Item.R1.HasValue) ? Item.R1.Value * 2 : new decimal?();
                TM.R2 = (Item.R2.HasValue) ? Item.R2.Value * 2 : new decimal?();
                TM.R3 = (Item.R3.HasValue) ? Item.R3.Value * 2 : new decimal?();
                // TCM = TM / 10
                TM10cm.N1 = (TM.N1.HasValue) ? TM.N1.Value / 10 : new decimal?();
                TM10cm.N2 = (TM.N2.HasValue) ? TM.N2.Value / 10 : new decimal?();
                TM10cm.N3 = (TM.N3.HasValue) ? TM.N3.Value / 10 : new decimal?();
                TM10cm.R1 = (TM.R1.HasValue) ? TM.R1.Value / 10 : new decimal?();
                TM10cm.R2 = (TM.R2.HasValue) ? TM.R2.Value / 10 : new decimal?();
                TM10cm.R3 = (TM.R3.HasValue) ? TM.R3.Value / 10 : new decimal?();

                // Raise events
                Raise(() => this.TM);
                Raise(() => this.TM10cm);
            }

            onCalc = false;
        }

        private void CalculateFormulaFromTM()
        {
            if (onCalc) return; // lock circular reference dead lock
            onCalc = true;

            if (null != Item && null != TM && null != TM10cm)
            {
                // Item = TM / 2
                Item.N1 = (TM.N1.HasValue) ? TM.N1.Value / 2 : new decimal?();
                Item.N2 = (TM.N2.HasValue) ? TM.N2.Value / 2 : new decimal?();
                Item.N3 = (TM.N3.HasValue) ? TM.N3.Value / 2 : new decimal?();
                Item.R1 = (TM.R1.HasValue) ? TM.R1.Value / 2 : new decimal?();
                Item.R2 = (TM.R2.HasValue) ? TM.R2.Value / 2 : new decimal?();
                Item.R3 = (TM.R3.HasValue) ? TM.R3.Value / 2 : new decimal?();
                // TCM = TM / 10
                TM10cm.N1 = (TM.N1.HasValue) ? TM.N1.Value / 10 : new decimal?();
                TM10cm.N2 = (TM.N2.HasValue) ? TM.N2.Value / 10 : new decimal?();
                TM10cm.N3 = (TM.N3.HasValue) ? TM.N3.Value / 10 : new decimal?();
                TM10cm.R1 = (TM.R1.HasValue) ? TM.R1.Value / 10 : new decimal?();
                TM10cm.R2 = (TM.R2.HasValue) ? TM.R2.Value / 10 : new decimal?();
                TM10cm.R3 = (TM.R3.HasValue) ? TM.R3.Value / 10 : new decimal?();

                // Raise events
                Raise(() => this.Item);
                Raise(() => this.TM10cm);
            }

            onCalc = false;
        }

        private void CalculateFormulaFromTCM()
        {
            if (onCalc) return; // lock circular reference dead lock
            onCalc = true;

            if (null != Item && null != TM && null != TM10cm)
            {
                // TM = TCM 8 10
                TM.N1 = (TM10cm.N1.HasValue) ? TM10cm.N1.Value * 10 : new decimal?();
                TM.N2 = (TM10cm.N2.HasValue) ? TM10cm.N2.Value * 10 : new decimal?();
                TM.N3 = (TM10cm.N3.HasValue) ? TM10cm.N3.Value * 10 : new decimal?();
                TM.R1 = (TM10cm.R1.HasValue) ? TM10cm.R1.Value * 10 : new decimal?();
                TM.R2 = (TM10cm.R2.HasValue) ? TM10cm.R2.Value * 10 : new decimal?();
                TM.R3 = (TM10cm.R3.HasValue) ? TM10cm.R3.Value * 10 : new decimal?();
                // Item = TM / 2
                Item.N1 = (TM.N1.HasValue) ? TM.N1.Value / 2 : new decimal?();
                Item.N2 = (TM.N2.HasValue) ? TM.N2.Value / 2 : new decimal?();
                Item.N3 = (TM.N3.HasValue) ? TM.N3.Value / 2 : new decimal?();
                Item.R1 = (TM.R1.HasValue) ? TM.R1.Value / 2 : new decimal?();
                Item.R2 = (TM.R2.HasValue) ? TM.R2.Value / 2 : new decimal?();
                Item.R3 = (TM.R3.HasValue) ? TM.R3.Value / 2 : new decimal?();

                // Raise events
                Raise(() => this.Item);
                Raise(() => this.TM);
            }

            onCalc = false;
        }

        private void UpdateProperties()
        {
            if (null == Item) Item = new NRTestProperty();

            Item.LotNo = LotNo;
            Item.PropertyNo = PropertyNo;
            Item.SPNo = SPNo;
            Item.NoOfSample = NoOfSample;
            Item.NeedSP = NeedSP;
            // Check calculate action
            if (null == Item.ValueChanges)
            {
                Item.ValueChanges = CalculateFormulaFromItem;
            }

            if (null == TM) TM = new NRTestProperty();
            TM.SPNo = SPNo;
            TM.LotNo = LotNo;
            TM.PropertyNo = PropertyNo;
            TM.SPNo = SPNo;
            TM.NoOfSample = NoOfSample;
            TM.NeedSP = NeedSP;
            // Check calculate action
            if (null == TM.ValueChanges)
            {
                TM.ValueChanges = CalculateFormulaFromTM;
            }

            if (null == TM10cm) TM10cm = new NRTestProperty();
            TM10cm.SPNo = SPNo;
            TM10cm.LotNo = LotNo;
            TM10cm.PropertyNo = PropertyNo;
            TM10cm.SPNo = SPNo;
            TM10cm.NoOfSample = NoOfSample;
            TM10cm.NeedSP = NeedSP;
            // Check calculate action
            if (null == TM10cm.ValueChanges)
            {
                TM10cm.ValueChanges = CalculateFormulaFromTCM;
            }

            CalculateFormulaFromItem(); // calculate

            this.Raise(() => this.EnableTest);
        }

        #endregion

        #region Public Properties

        #region LotNo/PropertyNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo
        {
            get { return Get<int>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets or sets SP No.</summary>
        public int? SPNo
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets Max No of Test/Retest.</summary>
        public int NoOfSample
        {
            get { return Get<int>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }
        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    UpdateProperties();
                });
            }
        }

        #endregion

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #region Enable Test (Normal/Re Test)

        public bool EnableTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
        }

        #endregion

        #region Item/TM/TM10cm

        public NRTestProperty Item { get; set; }
        public NRTestProperty TM { get; set; }
        public NRTestProperty TM10cm { get; set; }

        #endregion

        #endregion

        #region Static Methods

        #region Create

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="totalN"></param>
        /// <returns></returns>
        internal static List<Cord1stTwistingNumber> Create(CordSampleTestData value,
            Utils.M_GetPropertyTotalNByItem totalN)
        {
            List<Cord1stTwistingNumber> results = new List<Cord1stTwistingNumber>();
            if (null == value)
                return results;

            // For 1st Twisting Number Proepty No = 7
            int noOfSample = (null != totalN) ? totalN.NoSample : 0;
            int alllowSP = (value.TotalSP.HasValue) ? value.TotalSP.Value : 0;

            int i = 1;
            int iMaxLimitSP = 7;
            while (i <= iMaxLimitSP)
            {
                if (results.Count >= alllowSP)
                    break; // already reach max allow SP

                int? SP;
                switch (i)
                {
                    case 1: SP = value.SP1; break;
                    case 2: SP = value.SP2; break;
                    case 3: SP = value.SP3; break;
                    case 4: SP = value.SP4; break;
                    case 5: SP = value.SP5; break;
                    case 6: SP = value.SP6; break;
                    case 7: SP = value.SP7; break;
                    default: SP = new int?(); break;
                }
                // Skip SP is null
                if (!SP.HasValue)
                {
                    i++; // increase index and skip to next loop.
                    continue;
                }

                var inst = new Cord1stTwistingNumber()
                {
                    LotNo = value.LotNo,
                    PropertyNo = 7, // 1st Twisting Number = 7
                    SPNo = SP,
                    NeedSP = true,
                    NoOfSample = noOfSample
                };

                results.Add(inst);

                i++; // increase index
            }

            // For 1st Twisting Number Proepty No = 7
            var existItems = (value.MasterId.HasValue) ? GetsByLotNo(value.LotNo, 7).Value() : null;
            if (null != existItems && null != results)
            {
                int idx = -1;
                // loop trought all initial results and fill data with the exists on database
                foreach (var item in results)
                {
                    idx = existItems.FindIndex((x) => 
                    { 
                        return x.SPNo == item.SPNo && x.PropertyNo == item.PropertyNo;
                    });
                    if (idx != -1)
                    {
                        // need to set because not return from db.
                        existItems[idx].NoOfSample = item.NoOfSample;
                        // Clone anther properties
                        Clone(existItems[idx], item);
                    }
                }
            }

            return results;
        }

        #endregion

        #region Clone

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(Cord1stTwistingNumber src, Cord1stTwistingNumber dst)
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

            NRTestProperty.Clone(src.Item, dst.Item);
            NRTestProperty.Clone(src.TM, dst.TM);
            NRTestProperty.Clone(src.TM10cm, dst.TM10cm);
        }

        #endregion

        #region GetsByLotNo

        /// <summary>
        /// Gets Cord1stTwistingNumber by Lot No.
        /// </summary>
        /// <param name="lotNo">The Lot No.</param>
        /// <param name="propertyNo">The Property No.</param>
        /// <returns></returns>
        public static NDbResult<List<Cord1stTwistingNumber>> GetsByLotNo(string lotNo, int propertyNo)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<Cord1stTwistingNumber>> ret = new NDbResult<List<Cord1stTwistingNumber>>();

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

            try
            {
                List<Cord1stTwistingNumber> results = new List<Cord1stTwistingNumber>();

                var items = Utils.P_GetTwistNumberByLot.GetByLot(lotNo, propertyNo).Value();
                if (null != items)
                {
                    foreach (var item in items)
                    {
                        var inst = new Cord1stTwistingNumber();
                        inst.LotNo = item.LotNo;
                        inst.PropertyNo = 7; // 1st Twisting Number = 7
                        inst.SPNo = item.SPNo;

                        inst.NeedSP = true;
                        //inst.NoOfSample = 2; // ???

                        if (null != inst.Item)
                        {
                            inst.Item.N1 = item.N1;
                            inst.Item.N2 = item.N2;
                            inst.Item.N3 = item.N3;
                            inst.Item.R1 = item.R1;
                            inst.Item.R2 = item.R2;
                            inst.Item.R3 = item.R3;
                        }
                        if (null != inst.TM)
                        {
                            inst.TM.N1 = item.TMN1;
                            inst.TM.N2 = item.TMN2;
                            inst.TM.N3 = item.TMN3;
                            inst.TM.R1 = item.TMR1;
                            inst.TM.R2 = item.TMR2;
                            inst.TM.R3 = item.TMR3;
                        }
                        if (null != inst.TM10cm)
                        {
                            inst.TM10cm.N1 = item.TCMN1;
                            inst.TM10cm.N2 = item.TCMN2;
                            inst.TM10cm.N3 = item.TCMN3;
                            inst.TM10cm.R1 = item.TCMR1;
                            inst.TM10cm.R2 = item.TCMR2;
                            inst.TM10cm.R3 = item.TCMR3;
                        }

                        inst.InputBy = item.InputBy;
                        inst.InputDate = item.InputDate;
                        inst.EditBy = item.EditBy;
                        inst.EditDate = item.EditDate;

                        results.Add(inst);
                    }
                }

                ret.Success(results);
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

        /// <summary>
        /// Save.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static NDbResult<Cord1stTwistingNumber> Save(Cord1stTwistingNumber value)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<Cord1stTwistingNumber> ret = new NDbResult<Cord1stTwistingNumber>();

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
            p.Add("@propertyno", value.PropertyNo);
            p.Add("@spno", value.SPNo);

            p.Add("@n1", (null != value.Item) ? value.Item.N1 : new decimal?());
            p.Add("@n2", (null != value.Item) ? value.Item.N2 : new decimal?());
            p.Add("@n3", (null != value.Item) ? value.Item.N3 : new decimal?());
            p.Add("@r1", (null != value.Item) ? value.Item.R1 : new decimal?());
            p.Add("@r2", (null != value.Item) ? value.Item.R2 : new decimal?());
            p.Add("@r3", (null != value.Item) ? value.Item.R3 : new decimal?());

            p.Add("@tmn1", (null != value.TM) ? value.TM.N1 : new decimal?());
            p.Add("@tmn2", (null != value.TM) ? value.TM.N2 : new decimal?());
            p.Add("@tmn3", (null != value.TM) ? value.TM.N3 : new decimal?());
            p.Add("@tmr1", (null != value.TM) ? value.TM.R1 : new decimal?());
            p.Add("@tmr2", (null != value.TM) ? value.TM.R2 : new decimal?());
            p.Add("@tmr3", (null != value.TM) ? value.TM.R3 : new decimal?());

            p.Add("@tcmn1", (null != value.TM10cm) ? value.TM10cm.N1 : new decimal?());
            p.Add("@tcmn2", (null != value.TM10cm) ? value.TM10cm.N2 : new decimal?());
            p.Add("@tcmn3", (null != value.TM10cm) ? value.TM10cm.N3 : new decimal?());
            p.Add("@tcmr1", (null != value.TM10cm) ? value.TM10cm.R1 : new decimal?());
            p.Add("@tcmr2", (null != value.TM10cm) ? value.TM10cm.R2 : new decimal?());
            p.Add("@tcmr3", (null != value.TM10cm) ? value.TM10cm.R3 : new decimal?());

            p.Add("@user", value.EditBy);
            p.Add("@savedate", value.EditDate);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("P_SaveTwistingNumber", p, commandType: CommandType.StoredProcedure);
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

        #endregion
    }

    #endregion
}
