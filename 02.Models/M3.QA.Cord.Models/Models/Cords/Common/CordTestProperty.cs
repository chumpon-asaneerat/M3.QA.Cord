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
    #region CordTestProperty

    /// <summary>
    /// The CordTestProperty class.
    /// </summary>
    public class CordTestProperty : NInpc
    {
        #region Internal Variables

        private List<Func<decimal?>> _GetNs;
        private List<Action<decimal?>> _SetNs;
        private List<Func<decimal?>> _GetRs;
        private List<Action<decimal?>> _SetRs;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordTestProperty() : base()
        {
            #region Init Get/Set link methods

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
            // Get R
            _GetRs = new List<Func<decimal?>>()
            {
                () => { return this.R1; },
                () => { return this.R2; },
                () => { return this.R3; },
                () => { return this.R4; },
                () => { return this.R5; },
                () => { return this.R6; },
                () => { return this.R7; }
            };
            // Set R
            _SetRs = new List<Action<decimal?>>()
            {
                (value) => { this.R1 = value; },
                (value) => { this.R2 = value; },
                (value) => { this.R3 = value; },
                (value) => { this.R4 = value; },
                (value) => { this.R5 = value; },
                (value) => { this.R6 = value; },
                (value) => { this.R7 = value; }
            };

            #endregion

            BuildItems(0); // create empty items.
        }

        #endregion

        #region Private Methods

        private void CalcAvg()
        {

        }

        protected internal void BuildItems(int noOfSample)
        {
            Items = new List<CordTestPropertyItem>();
            CordTestPropertyItem item;
            for (int i = 1; i < 7; i++)
            {
                if (i > noOfSample) continue; // skip if more than allow no of sample.

                item = new CordTestPropertyItem();
                item.No = i;
                item.SPNo = SPNo; // assign SPNo
                item.NeedSP = NeedSP;

                // Link get/set methods.
                item.GetN = (null != _GetNs) ? _GetNs[i - 1] : null;
                item.SetN = (null != _SetNs) ? _SetNs[i - 1] : null;
                item.GetR = (null != _GetRs) ? _GetRs[i - 1] : null;
                item.SetR = (null != _SetRs) ? _SetRs[i - 1] : null;

                Items.Add(item);
            }
        }

        #endregion

        #region Public Properties

        #region LotNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo { get; set; }
        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo { get; set; }
        /// <summary>Gets or sets SP No.</summary>
        public int? SPNo { get; set; }
        /// <summary>Gets Max No of Test/Retest.</summary>
        public int NoOfSample
        {
            get
            {
                return (null != Items) ? Items.Count : 0;
            }
            set
            {
                BuildItems(value);
                // Raise events
                this.Raise(() => this.NoOfSample);
                this.Raise(() => this.Items);
            }
        }

        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP { get; set; }

        #endregion

        #region Enable Test (Normal/Re Test)

        public bool EnableTest
        {
            get { return (NeedSP) ? SPNo.HasValue : false; }
            set { }
        }

        #endregion

        #region Normal Test (1-7)

        public decimal? N1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N6
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? N7
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }

        #endregion

        #region Re Test (1-7)

        public decimal? R1
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R2
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R3
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R4
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R5
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R6
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }
        public decimal? R7
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {
                    CalcAvg();
                });
            }
        }

        #endregion

        #region Avg

        public decimal? Avg
        {
            get { return Get<decimal?>(); }
            set
            {
                Set(value, () =>
                {

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

        #region Items

        /// <summary>
        /// Gets Items.
        /// </summary>
        public List<CordTestPropertyItem> Items { get; set; }

        #endregion

        #endregion
    }

    #endregion

    #region CordTestPropertyItem

    /// <summary>
    /// The CordTestPropertyItem class.
    /// </summary>
    public class CordTestPropertyItem : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordTestPropertyItem() : base() { }

        #endregion

        #region virtual methods

        protected virtual void CheckRange() { }

        #endregion

        #region Protected Properties

        protected internal Func<decimal?> GetN { get; set; }
        protected internal Action<decimal?> SetN { get; set; }

        protected internal Func<decimal?> GetR { get; set; }
        protected internal Action<decimal?> SetR { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets SP No.
        /// </summary>
        public int? SPNo
        {
            get { return Get<int?>(); }
            set
            {
                Set(value, () =>
                {
                    // Raise events
                    Raise(() => this.EnableNormalTest);
                    Raise(() => this.EnableReTest);
                });
            }
        }

        /// <summary>
        /// Gets or sets Test No. (N1, N2, N3)
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// Gets or sets Test Value.
        /// </summary>
        public decimal? N
        {
            get
            {
                return (null != GetN) ? GetN() : new decimal?();
            }
            set
            {
                if (null != SetN)
                {
                    SetN(value);
                    // Raise events
                    Raise(() => this.EnableNormalTest);
                    Raise(() => this.EnableReTest);
                }
            }
        }
        /// <summary>
        /// Gets or sets Re Test Value.
        /// </summary>
        public decimal? R
        {
            get
            {
                return (null != GetR) ? GetR() : new decimal?();
            }
            set
            {
                if (null != SetR)
                {
                    SetR(value);
                }
            }
        }
        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP { get; set; }
        /// <summary>
        /// Check is Enable Normal Test. 
        /// </summary>
        public bool EnableNormalTest
        {
            get { return (NeedSP) ? SPNo.HasValue : false; }
            set { }
        }
        /// <summary>
        /// Check is Enable Re Test. 
        /// </summary>
        public bool EnableReTest
        {
            get { return (NeedSP) ? SPNo.HasValue && N.HasValue : N.HasValue; }
            set { }
        }
        /// <summary>Gets N Display Caption.</summary>
        public string NCaption { get { return "N" + No.ToString(); } set { } }
        /// <summary>Gets R Display Caption.</summary>
        public string RCaption { get { return "R" + No.ToString(); } set { } }

        #endregion
    }

    #endregion
}
