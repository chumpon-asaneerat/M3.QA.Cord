#region Using

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Data;
using System.Linq;
using System.Reflection;

using Dapper;
using NLib;
using NLib.Models;
using static M3.QA.Models.Utils;
using System.Runtime.ExceptionServices;

#endregion

namespace M3.QA.Models
{
    /// <summary>
    /// The Cord Production Test class.
    /// </summary>
    public class CordProductionTest : NInpc
    {
        #region Internal Variables

        private Func<int?> _GetSPNo;
        private List<Func<decimal?>> _GetNs;
        private List<Func<decimal?>> _GetOs;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordProductionTest() : base()
        {
            #region Init Get/Set link methods

            // Get SPNo
            _GetSPNo = () => { return this.SPNo; };
            // Get N
            _GetNs = new List<Func<decimal?>>()
            {
                () => { return this.N1; },
                () => { return this.N2; },
                () => { return this.N3; },
                () => { return this.N4; },
                () => { return this.N5; },
                () => { return this.N6; },
                () => { return this.N7; }
            };
            // Get R1
            _GetOs = new List<Func<decimal?>>()
            {
                () => { return this.O1; },
                () => { return this.O2; },
                () => { return this.O3; },
                () => { return this.O4; },
                () => { return this.O5; },
                () => { return this.O6; },
                () => { return this.O7; }
            };

            #endregion

            BuildItems(0); // create empty items.
        }

        #endregion

        #region Private Methods

        private void BuildItems(int noOfSample)
        {
            lock (this)
            {
                Items = new List<CordProductionTestItem>();
                CordProductionTestItem item;
                for (int i = 1; i <= 7; i++)
                {
                    if (i > noOfSample) continue; // skip if more than allow no of sample.

                    item = new CordProductionTestItem();
                    // set Sample No.
                    item.No = i;
                    // assign method pointer to Get SPNo/Need SP
                    item.GetSPNo = (null != _GetSPNo) ? _GetSPNo : null;
                    // assign method pointer to Get/Set N
                    item.GetN = (null != _GetNs) ? _GetNs[i - 1] : null;
                    // assign method pointer to Get/Set O
                    item.GetO = (null != _GetOs) ? _GetOs[i - 1] : null;

                    Items.Add(item);
                }
            }
        }

        private void ValueChange([CallerMemberName] string propertyName = "")
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return;
            if (null == this.Items)
                return; // No items.

            if (propertyName.StartsWith("N"))
            {
                string sIdx = propertyName.Substring(1, 1);

                // Note: N1 -> index must be 0, N2  -> index must be 1 so need decrease index by 1.
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    idx--; // Make zero index

                    if (idx < 0 || idx >= this.Items.Count) return;
                    // N only
                    this.Items[idx].RaiseNChanges();
                    CheckSpec();
                    CalcAvg();
                }
            }
            else if (propertyName.StartsWith("O"))
            {
                string sIdx = propertyName.Substring(1, 1);

                // Note: O1 -> index must be 0, O2  -> index must be 1 so need decrease index by 1.
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    idx--; // Make zero index

                    if (idx < 0 || idx >= this.Items.Count) return;
                    // N only
                    this.Items[idx].RaiseOChanges();
                    //CheckSpec();
                    //CalcAvg();
                }
            }
            else if (propertyName.StartsWith("SPNo"))
            {
                lock (this)
                {
                    foreach (var item in Items)
                    {
                        item.RaiseSPNoChanges();
                    }
                }
            }
        }

        private void CalcAvg()
        {
            decimal total = decimal.Zero;
            int iCnt = 0;
            lock (this)
            {
                if (null != this.Items)
                {
                    foreach (var item in this.Items)
                    {
                        if (item.N.HasValue)
                        {
                            // Has N value and no R value so use N to calc avg
                            total += item.N.Value;
                            ++iCnt;
                        }
                    }
                }
            }
            // Calc average value.
            this.Avg = (iCnt > 0) ? (total / iCnt) : new decimal?();

            if (null != ValueChanges) ValueChanges();
        }

        #endregion

        #region Virtual Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected virtual void CheckSpec()
        {

        }

        #endregion

        #region Callback Actions

        internal Action ValueChanges { get; set; }

        #endregion

        #region Public Properties

        #region LotNo/PropertyNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo { get; set; }
        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo { get; set; }
        /// <summary>Gets or sets SP No.</summary>
        public int? SPNo
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets Max No of Test/Retest.</summary>
        public int NoOfSample
        {
            get { return (null != Items) ? Items.Count : 0; }
            set
            {
                BuildItems(value);
                // Raise events
                this.Raise(() => this.NoOfSample);
                this.Raise(() => this.Items);
            }
        }
        /// <summary>Gets or sets Yarn Type.</summary>
        public string YarnType
        {
            get { return Get<string>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region ItemCode/PropertyName/MasterId/LoadN/SampleType

        public string ItemCode { get; set; }
        public string PropertyName { get; set; }
        public int? MasterId { get; set; }

        public string LoadN { get; set; }
        public string SampleType { get; set; }

        #endregion

        #region Spec

        /// <summary>Gets or sets CordTestSpec.</summary>
        public CordTestSpec Spec { get; set; }

        #endregion

        #region Normal Test (1-7)

        /// <summary>Gets or sets N1 value.</summary>
        public decimal? N1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N2 value.</summary>
        public decimal? N2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N3 value.</summary>
        public decimal? N3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N4 value.</summary>
        public decimal? N4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N5 value.</summary>
        public decimal? N5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N6 value.</summary>
        public decimal? N6
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets N7 value.</summary>
        public decimal? N7
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Normal (Over) Test (1-7)

        /// <summary>Gets or sets O1 value.</summary>
        public decimal? O1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets O2 value.</summary>
        public decimal? O2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets O3 value.</summary>
        public decimal? O3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets O4 value.</summary>
        public decimal? O4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets O5 value.</summary>
        public decimal? O5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets O6 value.</summary>
        public decimal? O6
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets O7 value.</summary>
        public decimal? O7
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Avg

        /// <summary>Gets or sets Avg value.</summary>
        public decimal? Avg
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }

        #endregion

        #region Items

        /// <summary>
        /// Gets N Items.
        /// </summary>
        public List<CordProductionTestItem> Items { get; set; }

        #endregion

        #endregion

        #region Static Methods

        public static List<CordProductionTest> Create(CordProductionProperty value)
        {
            List<CordProductionTest> results = new List<CordProductionTest>();
            if (null == value)
                return results;

            string unitId = (null != value.Spec) ? value.Spec.UnitId : null;
            var tests =  P_GetTestDetailByProperty.Gets(value.LotNo, value.PropertyNo, unitId).Value();
            if (null != tests)
            {
                foreach (var item in tests)
                {
                    var inst = new CordProductionTest();

                    inst.LotNo = value.LotNo;
                    inst.PropertyNo = value.PropertyNo;
                    inst.ItemCode = value.ItemCode;
                    inst.NoOfSample = value.NoOfSample;
                    inst.Spec = value.Spec;

                    inst.SPNo = item.SPNo;
                    inst.LoadN = item.LoadN;
                    inst.SampleType = item.SampleType;

                    List<decimal?> values = new List<decimal?>()
                    {
                        item.N1, item.N1R1, item.N1R2,
                        item.N2, item.N2R1, item.N2R2,
                        item.N3, item.N3R1, item.N3R2
                    };

                    int iCnt = 1;
                    for (int i = 0; i < values.Count; i++) 
                    {
                        decimal? val = values[i];
                        if (iCnt <= 3)
                        {
                            if (val.HasValue)
                            {
                                switch (iCnt)
                                {
                                    case 1:
                                        {
                                            inst.N1 = val;
                                            iCnt++;
                                        }
                                        break;
                                    case 2:
                                        {
                                            inst.N2 = val;
                                            iCnt++;
                                        }
                                        break;
                                    case 3:
                                        {
                                            inst.N3 = val;
                                            iCnt++;
                                        }
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (val.HasValue)
                            {
                                int oCnt = iCnt - 3;
                                switch (oCnt)
                                {
                                    case 1:
                                        {
                                            inst.O1 = val;
                                            iCnt++;
                                        }
                                        break;
                                    case 2:
                                        {
                                            inst.O2 = val;
                                            iCnt++;
                                        }
                                        break;
                                    case 3:
                                        {
                                            inst.O3 = val;
                                            iCnt++;
                                        }
                                        break;
                                }
                            }
                        }
                    }

                    results.Add(inst);
                }
            }

            return results;
        }

        #endregion
    }
}
