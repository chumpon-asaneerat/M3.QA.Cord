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
    #region NRTestPropertyItem

    /// <summary>
    /// The NR Test Property Item class.
    /// </summary>
    public class NRTestPropertyItem : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public NRTestPropertyItem() : base() { }

        #endregion

        #region virtual methods

        protected virtual void CheckRange() { }

        protected internal void RaiseSPNoChanges()
        {
            // Raise relelated events
            Raise(() => this.SPNo);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);
        }

        protected internal void RaiseNeedSPChanges()
        {
            // Raise relelated events
            Raise(() => this.NeedSP);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);
        }

        protected internal void RaiseNChanges()
        {
            // Raise relelated events
            Raise(() => this.N);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);
        }

        protected internal void RaiseRChanges()
        {
            // Raise relelated events
            Raise(() => this.R);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);
        }

        #endregion

        #region Protected Properties

        // SP Gets
        protected internal Func<int?> GetSPNo { get; set; }
        // Need SP Gets
        protected internal Func<bool> GetNeedSP { get; set; }
        // N Gets/Sets
        protected internal Func<decimal?> GetN { get; set; }
        protected internal Action<decimal?> SetN { get; set; }
        // R Gets/Sets
        protected internal Func<decimal?> GetR { get; set; }
        protected internal Action<decimal?> SetR { get; set; }

        #endregion

        #region Public Properties

        #region SP/NeedSP/No

        /// <summary>
        /// Gets or sets SP No.
        /// </summary>
        public int? SPNo
        {
            get { return (null != GetSPNo) ? GetSPNo() : new int?(); }
            set { }
        }
        /// <summary>Gets or sets Need SP to Enable Test.</summary>
        public bool NeedSP
        {
            get { return (null != GetNeedSP) ? GetNeedSP() : true; }
            set { }
        }
        /// <summary>Gets or sets Test No. (N1, N2, N3)</summary>
        public int No { get; set; }

        #endregion

        #region N/R

        /// <summary>Gets or sets Test Value.</summary>
        public decimal? N
        {
            get { return (null != GetN) ? GetN() : new decimal?(); }
            set
            {
                if (null != SetN)
                {
                    SetN(value);
                    // Raise events
                    Raise(() => this.EnableN);
                    Raise(() => this.EnableR);
                    Raise(() => this.N);
                }
            }
        }
        /// <summary>Gets or sets Re Test Value.</summary>
        public decimal? R
        {
            get { return (null != GetR) ? GetR() : new decimal?(); }
            set
            {
                if (null != SetR)
                {
                    SetR(value);
                    // Raise events
                    Raise(() => this.R);
                }
            }
        }

        #endregion

        #region EnableN/EnableR/CaptionN/CaptionR (For Runtime binding)

        /// <summary>Check is Enable Normal Test.</summary>
        public bool EnableN 
        { 
            get 
            {
                // Note: In some case some row has only R data so need to enable it.
                return (NeedSP) ? (SPNo.HasValue && !R.HasValue) : !R.HasValue;
            } 
            set { }  
        }
        /// <summary>Check is Enable Re Test (requird N value first).</summary>
        public bool EnableR 
        { 
            get { return (NeedSP) ? SPNo.HasValue && (N.HasValue || R.HasValue) : (N.HasValue || R.HasValue); } 
            set { } 
        }

        /// <summary>Check is ReadOnly Normal Test.</summary>
        public bool ReadOnlyN 
        { 
            get { return (NeedSP) ? !SPNo.HasValue || R.HasValue: R.HasValue; } 
            set { } 
        }
        /// <summary>Check is ReadOnly Re Test (requird N value first).</summary>
        public bool ReadOnlyR 
        { 
            get { return (NeedSP) ? !SPNo.HasValue && N.HasValue : N.HasValue; } 
            set { } 
        }

        /// <summary>Gets Visibility of Normal Test.</summary>
        public Visibility VisibleN 
        { 
            get { return Visibility.Visible; } 
            set { } 
        }
        /// <summary>Gets Visibility of Re Test.</summary>
        public Visibility VisibleR 
        { 
            get 
            {
                //return (EnableR || R.HasValue) ? Visibility.Visible : Visibility.Collapsed;

                // Note: In some case some row has only R data so need to display it.
                return (EnableR || R.HasValue) ? Visibility.Visible : Visibility.Collapsed; 
            } 
            set { } 
        }

        /// <summary>Gets N Display Caption.</summary>
        public string CaptionN 
        { 
            get { return "N" + No.ToString(); } 
            set { } 
        }
        /// <summary>Gets R Display Caption.</summary>
        public string CaptionR 
        { 
            get { return "R" + No.ToString(); } 
            set { } 
        }

        #endregion

        #endregion
    }

    #endregion
}
