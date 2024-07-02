#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;
using static NLib.IO.Folders;

#endregion

namespace M3.QA.Models
{
    #region NRTestProperty

    /// <summary>
    /// The NR Test Property class
    /// </summary>
    public class NRTestProperty : NInpc
    {
        #region Internal Variables

        private Func<int?> _GetSPNo;
        private Func<bool> _GetNeedSP;
        private Func<string> _GetYarnType;
        private Func<string> _GetSampleType;
        private List<Func<decimal?>> _GetNs;
        private List<Action<decimal?>> _SetNs;

        private List<Func<decimal?>> _GetR1s;
        private List<Action<decimal?>> _SetR1s;

        private List<Func<decimal?>> _GetR2s;
        private List<Action<decimal?>> _SetR2s;

        private List<Func<bool>> _GetNOuts;
        private List<Action<bool>> _SetNOuts;

        private List<Func<bool>> _GetNOOuts;
        private List<Action<bool>> _SetNOOuts;

        private List<Func<bool>> _GetR1Outs;
        private List<Action<bool>> _SetR1Outs;

        private List<Func<bool>> _GetR2Outs;
        private List<Action<bool>> _SetR2Outs;

        private List<Func<bool>> _GetR1Flags;
        private List<Action<bool>> _SetR1Flags;

        private List<Func<bool>> _GetR2Flags;
        private List<Action<bool>> _SetR2Flags;

        private Func<bool> _GetMultiPropertyRetest;
        private Action<bool> _SetMultiPropertyRetest;
        private Func<bool> _GetMultiOut;

        private Func<bool> _GetCustomAllowR;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRTestProperty() : base()
        {
            #region Init Get/Set link methods

            // Get SPNo
            _GetSPNo = () => { return this.SPNo; };
            // Get NeedSP
            _GetNeedSP = () => { return this.NeedSP; };
            // Get Yarn Type
            _GetYarnType = () => { return this.YarnType; };
            // Get Sample Type
            _GetSampleType = () => { return this.SampleType; };
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
            // Set N
            _SetNs = new List<Action<decimal?>>()
            {
                (value) => { this.N1 = value; },
                (value) => { this.N2 = value; },
                (value) => { this.N3 = value; },
                (value) => { this.N4 = value; },
                (value) => { this.N5 = value; },
                (value) => { this.N6 = value; },
                (value) => { this.N7 = value; }
            };
            // Get R1
            _GetR1s = new List<Func<decimal?>>()
            {
                () => { return this.N1R1; },
                () => { return this.N2R1; },
                () => { return this.N3R1; },
                () => { return this.N4R1; },
                () => { return this.N5R1; },
                () => { return this.N6R1; },
                () => { return this.N7R1; }
            };
            // Set R1
            _SetR1s = new List<Action<decimal?>>()
            {
                (value) => { this.N1R1 = value; },
                (value) => { this.N2R1 = value; },
                (value) => { this.N3R1 = value; },
                (value) => { this.N4R1 = value; },
                (value) => { this.N5R1 = value; },
                (value) => { this.N6R1 = value; },
                (value) => { this.N7R1 = value; }
            };
            // Get R2
            _GetR2s = new List<Func<decimal?>>()
            {
                () => { return this.N1R2; },
                () => { return this.N2R2; },
                () => { return this.N3R2; },
                () => { return this.N4R2; },
                () => { return this.N5R2; },
                () => { return this.N6R2; },
                () => { return this.N7R2; }
            };
            // Set R2
            _SetR2s = new List<Action<decimal?>>()
            {
                (value) => { this.N1R2 = value; },
                (value) => { this.N2R2 = value; },
                (value) => { this.N3R2 = value; },
                (value) => { this.N4R2 = value; },
                (value) => { this.N5R2 = value; },
                (value) => { this.N6R2 = value; },
                (value) => { this.N7R2 = value; }
            };
            // Get N Out
            _GetNOuts = new List<Func<bool>>()
            {
                () => { return this.N1Out; },
                () => { return this.N2Out; },
                () => { return this.N3Out; },
                () => { return this.N4Out; },
                () => { return this.N5Out; },
                () => { return this.N6Out; },
                () => { return this.N7Out; }
            };
            // Set N Out
            _SetNOuts = new List<Action<bool>>()
            {
                (value) => { this.N1Out = value; },
                (value) => { this.N2Out = value; },
                (value) => { this.N3Out = value; },
                (value) => { this.N4Out = value; },
                (value) => { this.N5Out = value; },
                (value) => { this.N6Out = value; },
                (value) => { this.N7Out = value; }
            };
            // Get N Overall Out
            _GetNOOuts = new List<Func<bool>>()
            {
                () => { return this.N1OOut; },
                () => { return this.N2OOut; },
                () => { return this.N3OOut; },
                () => { return this.N4OOut; },
                () => { return this.N5OOut; },
                () => { return this.N6OOut; },
                () => { return this.N7OOut; }
            };
            // Set N Overall Out
            _SetNOOuts = new List<Action<bool>>()
            {
                (value) => { this.N1OOut = value; },
                (value) => { this.N2OOut = value; },
                (value) => { this.N3OOut = value; },
                (value) => { this.N4OOut = value; },
                (value) => { this.N5OOut = value; },
                (value) => { this.N6OOut = value; },
                (value) => { this.N7OOut = value; }
            };
            // Get R1 Out
            _GetR1Outs = new List<Func<bool>>()
            {
                () => { return this.N1R1Out; },
                () => { return this.N2R1Out; },
                () => { return this.N3R1Out; },
                () => { return this.N4R1Out; },
                () => { return this.N5R1Out; },
                () => { return this.N6R1Out; },
                () => { return this.N7R1Out; }
            };
            // Set R1 Out
            _SetR1Outs = new List<Action<bool>>()
            {
                (value) => { this.N1R1Out = value; },
                (value) => { this.N2R1Out = value; },
                (value) => { this.N3R1Out = value; },
                (value) => { this.N4R1Out = value; },
                (value) => { this.N5R1Out = value; },
                (value) => { this.N6R1Out = value; },
                (value) => { this.N7R1Out = value; }
            };
            // Get R2 Out
            _GetR2Outs = new List<Func<bool>>()
            {
                () => { return this.N1R2Out; },
                () => { return this.N2R2Out; },
                () => { return this.N3R2Out; },
                () => { return this.N4R2Out; },
                () => { return this.N5R2Out; },
                () => { return this.N6R2Out; },
                () => { return this.N7R2Out; }
            };
            // Set R2 Out
            _SetR2Outs = new List<Action<bool>>()
            {
                (value) => { this.N1R2Out = value; },
                (value) => { this.N2R2Out = value; },
                (value) => { this.N3R2Out = value; },
                (value) => { this.N4R2Out = value; },
                (value) => { this.N5R2Out = value; },
                (value) => { this.N6R2Out = value; },
                (value) => { this.N7R2Out = value; }
            };

            // Get R1 Flag
            _GetR1Flags = new List<Func<bool>>()
            {
                () => { return this.N1R1Flag; },
                () => { return this.N2R1Flag; },
                () => { return this.N3R1Flag; },
                () => { return this.N4R1Flag; },
                () => { return this.N5R1Flag; },
                () => { return this.N6R1Flag; },
                () => { return this.N7R1Flag; }
            };
            // Set R1 Flag
            _SetR1Flags = new List<Action<bool>>()
            {
                (value) => { this.N1R1Flag = value; },
                (value) => { this.N2R1Flag = value; },
                (value) => { this.N3R1Flag = value; },
                (value) => { this.N4R1Flag = value; },
                (value) => { this.N5R1Flag = value; },
                (value) => { this.N6R1Flag = value; },
                (value) => { this.N7R1Flag = value; }
            };
            // Get R2 Flag
            _GetR2Flags = new List<Func<bool>>()
            {
                () => { return this.N1R2Flag; },
                () => { return this.N2R2Flag; },
                () => { return this.N3R2Flag; },
                () => { return this.N4R2Flag; },
                () => { return this.N5R2Flag; },
                () => { return this.N6R2Flag; },
                () => { return this.N7R2Flag; }
            };
            // Set R2 Flag
            _SetR2Flags = new List<Action<bool>>()
            {
                (value) => { this.N1R2Flag = value; },
                (value) => { this.N2R2Flag = value; },
                (value) => { this.N3R2Flag = value; },
                (value) => { this.N4R2Flag = value; },
                (value) => { this.N5R2Flag = value; },
                (value) => { this.N6R2Flag = value; },
                (value) => { this.N7R2Flag = value; }
            };

            // Get Custom Allow Retest
            _GetCustomAllowR = () => { return (null != AllowReTest) ? AllowReTest() : true; };

            // Get Multi Property Re test
            _GetMultiPropertyRetest = () => { return EnableMultiPropertyTest; };
            // Set Multi Property Re test
            _SetMultiPropertyRetest = (value) => { EnableMultiPropertyTest = value; };

            _GetMultiOut = () => { return (null != GetNMultiOut) ? GetNMultiOut() : false; };

            #endregion

            BuildItems(0); // create empty items.
        }

        #endregion

        #region Private Methods

        private void BuildItems(int noOfSample)
        {
            lock (this)
            {
                Items = new List<NRTestPropertyItem>();
                NRTestPropertyItem item;
                for (int i = 1; i <= 7; i++)
                {
                    if (i > noOfSample) continue; // skip if more than allow no of sample.

                    item = new NRTestPropertyItem();
                    // set Sample No.
                    item.No = i;
                    // assign method pointer to Get SPNo/Need SP
                    item.GetSPNo = (null != _GetSPNo) ? _GetSPNo : null;
                    item.GetNeedSP = (null != _GetNeedSP) ? _GetNeedSP : null;
                    item.GetYarnType = (null != _GetYarnType) ? _GetYarnType : null;
                    item.GetSampleType = (null != _GetSampleType) ? _GetSampleType : null;
                    // assign method pointer to Get/Set N
                    item.GetN = (null != _GetNs) ? _GetNs[i - 1] : null;
                    item.SetN = (null != _SetNs) ? _SetNs[i - 1] : null;
                    // assign method pointer to Get/Set R1
                    item.GetR1 = (null != _GetR1s) ? _GetR1s[i - 1] : null;
                    item.SetR1 = (null != _SetR1s) ? _SetR1s[i - 1] : null;
                    // assign method pointer to Get/Set R2
                    item.GetR2 = (null != _GetR2s) ? _GetR2s[i - 1] : null;
                    item.SetR2 = (null != _SetR2s) ? _SetR2s[i - 1] : null;
                    // assign method pointer to Get/Set O
                    item.GetNOut = (null != _GetNOuts) ? _GetNOuts[i - 1] : null;
                    item.SetNOut = (null != _SetNOuts) ? _SetNOuts[i - 1] : null;
                    // assign method pointer to Get/Set Overall Out
                    item.GetNOOut = (null != _GetNOOuts) ? _GetNOOuts[i - 1] : null;
                    item.SetNOOut = (null != _SetNOOuts) ? _SetNOOuts[i - 1] : null;
                    // assign method pointer to Get/Set R1Out
                    item.GetR1Out = (null != _GetR1Outs) ? _GetR1Outs[i - 1] : null;
                    item.SetR1Out = (null != _SetR1Outs) ? _SetR1Outs[i - 1] : null;
                    // assign method pointer to Get/Set R2Out
                    item.GetR2Out = (null != _GetR2Outs) ? _GetR2Outs[i - 1] : null;
                    item.SetR2Out = (null != _SetR2Outs) ? _SetR2Outs[i - 1] : null;

                    // assign method pointer to Get/Set R1Flag
                    item.GetR1Flag = (null != _GetR1Flags) ? _GetR1Flags[i - 1] : null;
                    item.SetR1Flag = (null != _SetR1Flags) ? _SetR1Flags[i - 1] : null;
                    // assign method pointer to Get/Set R2Flag
                    item.GetR2Flag = (null != _GetR2Flags) ? _GetR2Flags[i - 1] : null;
                    item.SetR2Flag = (null != _SetR2Flags) ? _SetR2Flags[i - 1] : null;

                    // common for all test N
                    item.CustomAllowR = (null != _GetCustomAllowR) ? _GetCustomAllowR : null;

                    item.GetMultiPropertyRetest = (null != _GetMultiPropertyRetest) ? _GetMultiPropertyRetest : null;
                    item.SetMultiPropertyRetest = (null != _SetMultiPropertyRetest) ? _SetMultiPropertyRetest : null;

                    item.GetNMultiOut = (null != _GetMultiOut) ? _GetMultiOut : null;

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

            if (propertyName.StartsWith("N") && propertyName.Length > 1)
            {
                string sIdx = propertyName.Substring(1, 1);

                // Note: N1 -> index must be 0, N2  -> index must be 1 so need decrease index by 1.
                int idx;
                if (int.TryParse(sIdx, out idx))
                {
                    idx--; // Make zero index

                    if (idx < 0 || idx >= this.Items.Count) return;
                    if (propertyName.Contains("R1"))
                    {
                        if (propertyName.Contains("Out"))
                        {
                            // R1 Out
                            this.Items[idx].RaiseR1OutChanges();
                        }
                        else if (propertyName.Contains("Flag"))
                        {
                            // R1 Flag
                            this.Items[idx].RaiseR1FlagChanges();
                            CalcAvg();
                        }
                        else
                        {
                            // R1 only
                            this.Items[idx].RaiseR1Changes();
                            CheckSpec();
                            CalcAvg();
                        }
                    }
                    else if (propertyName.Contains("R2"))
                    {
                        if (propertyName.Contains("Out"))
                        {
                            // R2 Out
                            this.Items[idx].RaiseR2OutChanges();
                        }
                        else if (propertyName.Contains("Flag"))
                        {
                            // R2 Flag
                            this.Items[idx].RaiseR2FlagChanges();
                            CalcAvg();
                        }
                        else
                        {
                            // R2 only
                            this.Items[idx].RaiseR2Changes();
                            CheckSpec();
                            CalcAvg();
                        }
                    }
                    else
                    {
                        if (propertyName.Contains("Out"))
                        {
                            // N Out only
                            this.Items[idx].RaiseNOutChanges();
                        }
                        else
                        {
                            // N only
                            this.Items[idx].RaiseNChanges();
                            CheckSpec();
                            CalcAvg();
                        }
                    }
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
            else if (propertyName.StartsWith("NeedSP"))
            {
                this.Raise(() => this.EnableTest);
                lock (this)
                {
                    foreach (var item in Items)
                    {
                        item.RaiseNeedSPChanges();
                    }
                }
            }
            else if (propertyName.StartsWith("YarnType"))
            {
                this.Raise(() => this.EnableTest);
                lock (this)
                {
                    foreach (var item in Items)
                    {
                        item.RaiseYarnTypeChanges();
                    }
                }
            }
            else if (propertyName.StartsWith("SampleType"))
            {
                this.Raise(() => this.EnableTest);
                lock (this)
                {
                    foreach (var item in Items)
                    {
                        item.RaiseSampleTypeChanges();
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
                        if (item.N.HasValue && !item.R1.HasValue && !item.R2.HasValue)
                        {
                            // Has N value and no R value so use N to calc avg
                            total += item.N.Value;
                            ++iCnt;
                        }
                        if (item.R1Flag && item.R1.HasValue)
                        {
                            // Either N has value or not but when R value exists so use R to calc avg
                            total += item.R1.Value;
                            ++iCnt;
                        }
                        if (item.R2Flag && item.R2.HasValue)
                        {
                            // Either N has value or not but when R value exists so use R to calc avg
                            total += item.R2.Value;
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

        #region Protected Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected virtual void CheckSpec()
        {

        }

        protected internal void RaiseNOutChanges()
        {
            foreach (var item in Items) 
                item.RaiseNOutChanges();
        }

        protected internal void RaiseR1OutChanges()
        {
            foreach (var item in Items) 
                item.RaiseR1OutChanges();
        }

        protected internal void RaiseR2OutChanges()
        {
            foreach (var item in Items)
                item.RaiseR2OutChanges();
        }

        protected internal void RaiseR1FlagChanges()
        {
            foreach (var item in Items)
                item.RaiseR1FlagChanges();
        }

        protected internal void RaiseR2FlagChanges()
        {
            foreach (var item in Items)
                item.RaiseR2FlagChanges();
        }

        #endregion

        #region Callback Actions

        internal Action ValueChanges { get; set; }
        internal Func<bool> GetNMultiOut { get; set; }

        #endregion

        #region Public Properties

        #region LotNo/PropertyNo/SPNo/NoOfSample/SampleType

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
        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
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
        /// <summary>Gets or sets Yarn Type.</summary>
        public string SampleType
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

        #region Spec

        /// <summary>Gets or sets CordTestSpec.</summary>
        public CordTestSpec Spec { get; set; }

        #endregion

        #region Enable Test (Normal/Re Test)

        /// <summary>Gets or sets is enable to Multi Property test data.</summary>
        public bool EnableMultiPropertyTest
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    
                });
            }
        }
        /// <summary>Gets or sets is enable to enter test data.</summary>
        public bool EnableTest
        {
            get { return (NeedSP) ? SPNo.HasValue : true; }
            set { }
        }
        /// <summary>Gets or sets custom allow retrest.</summary>
        public Func<bool> AllowReTest { get; set; }

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

        #region Re Test 1 (1-7)

        /// <summary>Gets or sets N1R1 value.</summary>
        public decimal? N1R1
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
        /// <summary>Gets or sets N2R1 value.</summary>
        public decimal? N2R1
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
        /// <summary>Gets or sets N3R1 value.</summary>
        public decimal? N3R1
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
        /// <summary>Gets or sets N4R1 value.</summary>
        public decimal? N4R1
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
        /// <summary>Gets or sets N5R1 value.</summary>
        public decimal? N5R1
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
        /// <summary>Gets or sets N6R1 value.</summary>
        public decimal? N6R1
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
        /// <summary>Gets or sets N7R1 value.</summary>
        public decimal? N7R1
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

        #region Re Test 2 (1-7)

        /// <summary>Gets or sets N1R2 value.</summary>
        public decimal? N1R2
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
        /// <summary>Gets or sets N2R2 value.</summary>
        public decimal? N2R2
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
        /// <summary>Gets or sets N3R2 value.</summary>
        public decimal? N3R2
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
        /// <summary>Gets or sets N4R2 value.</summary>
        public decimal? N4R2
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
        /// <summary>Gets or sets N5R2 value.</summary>
        public decimal? N5R2
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
        /// <summary>Gets or sets N6R2 value.</summary>
        public decimal? N6R2
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
        /// <summary>Gets or sets N7R2 value.</summary>
        public decimal? N7R2
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

        #region Normal OutSpec (1-7)

        /// <summary>Gets or sets Normal OutSpec1 value.</summary>
        public bool N1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec 2 value.</summary>
        public bool N2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec 3 value.</summary>
        public bool N3Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec 4 value.</summary>
        public bool N4Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec 5 value.</summary>
        public bool N5Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec 6 value.</summary>
        public bool N6Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Normal OutSpec 7 value.</summary>
        public bool N7Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Re-Test 1 OutSpec (1-7)

        /// <summary>Gets or sets Re-Test 1 OutSpec1 value.</summary>
        public bool N1R1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 OutSpec2 value.</summary>
        public bool N2R1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 OutSpec3 value.</summary>
        public bool N3R1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 OutSpec4 value.</summary>
        public bool N4R1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 OutSpec5 value.</summary>
        public bool N5R1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 OutSpec6 value.</summary>
        public bool N6R1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 OutSpec7 value.</summary>
        public bool N7R1Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Re-Test 2 OutSpec (1-7)

        /// <summary>Gets or sets Re-Test 2 OutSpec1 value.</summary>
        public bool N1R2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 OutSpec2 value.</summary>
        public bool N2R2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 OutSpec3 value.</summary>
        public bool N3R2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 OutSpec4 value.</summary>
        public bool N4R2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 OutSpec5 value.</summary>
        public bool N5R2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 OutSpec6 value.</summary>
        public bool N6R2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 OutSpec7 value.</summary>
        public bool N7R2Out
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Re-Test 1 Flag (1-7)

        /// <summary>Gets or sets Re-Test 1 Flag 1 value.</summary>
        public bool N1R1Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 Flag 2 value.</summary>
        public bool N2R1Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 Flag 3 value.</summary>
        public bool N3R1Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 Flag 4 value.</summary>
        public bool N4R1Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 Flag 5 value.</summary>
        public bool N5R1Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 Flag 6 value.</summary>
        public bool N6R1Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 1 Flag 7 value.</summary>
        public bool N7R1Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Re-Test 2 Flag (1-7)

        /// <summary>Gets or sets Re-Test 2 Flag 1 value.</summary>
        public bool N1R2Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 Flag 2 value.</summary>
        public bool N2R2Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 Flag 3 value.</summary>
        public bool N3R2Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 Flag 4 value.</summary>
        public bool N4R2Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 Flag 5 value.</summary>
        public bool N5R2Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 Flag 6 value.</summary>
        public bool N6R2Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        /// <summary>Gets or sets Re-Test 2 Flag 7 value.</summary>
        public bool N7R2Flag
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }

        #endregion

        #region Overall Out (for share out between all items at same N)

        // overall out.
        public bool N1OOut
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        public bool N2OOut
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        public bool N3OOut
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        public bool N4OOut
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        public bool N5OOut
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        public bool N6OOut
        {
            get { return Get<bool>(); }
            set
            {
                Set(value, () =>
                {
                    ValueChange();
                });
            }
        }
        public bool N7OOut
        {
            get { return Get<bool>(); }
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
        /// Gets NR Items.
        /// </summary>
        public List<NRTestPropertyItem> Items { get; set; }

        #endregion

        #endregion

        #region Static Methods

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(NRTestProperty src, NRTestProperty dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.YarnType = src.YarnType;
            dst.SampleType = src.SampleType;

            dst.Spec = src.Spec;

            dst.N1 = src.N1;
            dst.N2 = src.N2;
            dst.N3 = src.N3;
            dst.N4 = src.N4;
            dst.N5 = src.N5;
            dst.N6 = src.N6;
            dst.N7 = src.N7;

            dst.N1R1 = src.N1R1;
            dst.N2R1 = src.N2R1;
            dst.N3R1 = src.N3R1;
            dst.N4R1 = src.N4R1;
            dst.N5R1 = src.N5R1;
            dst.N6R1 = src.N6R1;
            dst.N7R1 = src.N7R1;

            dst.N1R2 = src.N1R2;
            dst.N2R2 = src.N2R2;
            dst.N3R2 = src.N3R2;
            dst.N4R2 = src.N4R2;
            dst.N5R2 = src.N5R2;
            dst.N6R2 = src.N6R2;
            dst.N7R2 = src.N7R2;

            dst.N1Out = src.N1Out;
            dst.N2Out = src.N2Out;
            dst.N3Out = src.N3Out;
            dst.N4Out = src.N4Out;
            dst.N5Out = src.N5Out;
            dst.N6Out = src.N6Out;
            dst.N7Out = src.N7Out;

            dst.N1R1Out = src.N1R1Out;
            dst.N2R1Out = src.N2R1Out;
            dst.N3R1Out = src.N3R1Out;
            dst.N4R1Out = src.N4R1Out;
            dst.N5R1Out = src.N5R1Out;
            dst.N6R1Out = src.N6R1Out;
            dst.N7R1Out = src.N7R1Out;

            dst.N1R2Out = src.N1R2Out;
            dst.N2R2Out = src.N2R2Out;
            dst.N3R2Out = src.N3R2Out;
            dst.N4R2Out = src.N4R2Out;
            dst.N5R2Out = src.N5R2Out;
            dst.N6R2Out = src.N6R2Out;
            dst.N7R2Out = src.N7R2Out;

            dst.N1R1Flag = src.N1R1Flag;
            dst.N2R1Flag = src.N2R1Flag;
            dst.N3R1Flag = src.N3R1Flag;
            dst.N4R1Flag = src.N4R1Flag;
            dst.N5R1Flag = src.N5R1Flag;
            dst.N6R1Flag = src.N6R1Flag;
            dst.N7R1Flag = src.N7R1Flag;

            dst.N1R2Flag = src.N1R2Flag;
            dst.N2R2Flag = src.N2R2Flag;
            dst.N3R2Flag = src.N3R2Flag;
            dst.N4R2Flag = src.N4R2Flag;
            dst.N5R2Flag = src.N5R2Flag;
            dst.N6R2Flag = src.N6R2Flag;
            dst.N7R2Flag = src.N7R2Flag;

            dst.SampleType = src.SampleType;

            dst.EnableMultiPropertyTest = src.EnableMultiPropertyTest;
        }

        #endregion
    }

    #endregion
}
