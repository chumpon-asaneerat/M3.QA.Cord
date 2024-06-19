#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
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

        protected internal void RaiseYarnTypeChanges()
        {
            // Raise relelated events
            Raise(() => this.YarnType);
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
            Raise(() => this.NOut);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR);
        }

        protected internal void RaiseRChanges()
        {
            // Raise relelated events
            Raise(() => this.R);
            Raise(() => this.ROut);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR);
        }

        protected internal void RaiseNOutChanges()
        {
            // Raise relelated events
            Raise(() => this.NOut);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR);
        }

        protected internal void RaiseROutChanges()
        {
            // Raise relelated events
            Raise(() => this.ROut);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR);
        }

        #endregion

        #region Protected Properties

        // SP Gets
        protected internal Func<int?> GetSPNo { get; set; }
        // Need SP Gets
        protected internal Func<bool> GetNeedSP { get; set; }
        // YarnType Gets
        protected internal Func<string> GetYarnType { get; set; }
        // N Gets/Sets
        protected internal Func<decimal?> GetN { get; set; }
        protected internal Action<decimal?> SetN { get; set; }
        // R Gets/Sets
        protected internal Func<decimal?> GetR { get; set; }
        protected internal Action<decimal?> SetR { get; set; }
        // NOut Gets/Sets
        protected internal Func<bool> GetNOut { get; set; }
        protected internal Action<bool> SetNOut { get; set; }
        // ROut Gets/Sets
        protected internal Func<bool> GetROut { get; set; }
        protected internal Action<bool> SetROut { get; set; }

        // CustomAllowR Gets
        protected internal Func<bool> CustomAllowR { get; set; }

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
        /// <summary>Gets or sets Yarn Type.</summary>
        public string YarnType
        {
            get { return (null != GetYarnType) ? GetYarnType() : null; }
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

                    Raise(() => this.VisibleN);
                    Raise(() => this.VisibleR);
                    Raise(() => this.ReadOnlyN);
                    Raise(() => this.ReadOnlyR);

                    Raise(() => this.N);
                    Raise(() => this.ForegroundColorN);
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

                    Raise(() => this.VisibleR);
                    Raise(() => this.ReadOnlyR);

                    Raise(() => this.ForegroundColorR);
                }
            }
        }

        #endregion

        #region NOut/ROut

        /// <summary>Gets or sets Normal Out of spec Value.</summary>
        public bool NOut
        {
            get { return (null != GetNOut) ? GetNOut() : false; }
            set
            {
                if (null != SetNOut)
                {
                    SetNOut(value);
                    // Raise events
                    Raise(() => this.NOut);
                    Raise(() => this.ForegroundColorN);
                }
            }
        }

        /// <summary>Gets or sets Re Test Out of spec Value.</summary>
        public bool ROut
        {
            get { return (null != GetROut) ? GetROut() : false; }
            set
            {
                if (null != SetROut)
                {
                    SetROut(value);
                    // Raise events
                    Raise(() => this.ROut);
                    Raise(() => this.ForegroundColorR);
                }
            }
        }

        #endregion

        #region MultiPropertyRetest

        /// <summary>Check is Enable Multi Property Retest.</summary>
        public bool MultiPropertyRetest 
        {
            get; 
            set; 
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
            get 
            {
                if (MultiPropertyRetest)
                {
                    bool ret = (NeedSP) ? SPNo.HasValue && ((N.HasValue && NOut) || R.HasValue) : ((N.HasValue && NOut) || R.HasValue);
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
                else
                {
                    bool ret = (NeedSP) ? SPNo.HasValue && ((N.HasValue && NOut) || R.HasValue) : ((N.HasValue && NOut) || R.HasValue);
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
            } 
            set { } 
        }

        /// <summary>Check is ReadOnly Normal Test.</summary>
        public bool ReadOnlyN 
        { 
            get { return (NeedSP) ? !SPNo.HasValue || R.HasValue: R.HasValue; } 
            set { } 
        }
        /// <summary>Check is ReadOnly Re Test (if no N not allow to enter R).</summary>
        public bool ReadOnlyR 
        { 
            get 
            {
                if (MultiPropertyRetest)
                {
                    bool ret = (NeedSP) ? !SPNo.HasValue && !N.HasValue : !N.HasValue;
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
                else
                {
                    bool ret = (NeedSP) ? !SPNo.HasValue && !N.HasValue : !N.HasValue;
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
            } 
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
                if (MultiPropertyRetest)
                {
                    return (EnableR || R.HasValue) ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    //return (EnableR || R.HasValue) ? Visibility.Visible : Visibility.Collapsed;

                    // Note: In some case some row has only R data so need to display it.
                    return (EnableR || R.HasValue) ? Visibility.Visible : Visibility.Collapsed;
                }
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

        #region Colors Foreground (For Runtime binding)

        /// <summary>Gets N Foreground Color.</summary>
        public SolidColorBrush ForegroundColorN
        {
            get 
            {
                if (!N.HasValue)
                    return (R.HasValue) ? ModelConsts.DimGrayColor : ModelConsts.BlackColor; // No input
                if (NOut)
                    return ModelConsts.RedColor; // Out of spec.
                return (R.HasValue) ? ModelConsts.DimGrayColor : ModelConsts.ForestGreenColor; 
            }
            set { }
        }
        /// <summary>Gets R Foreground Color.</summary>
        public SolidColorBrush ForegroundColorR
        {
            get 
            {
                if (!R.HasValue)
                    return ModelConsts.BlackColor; // No input
                if (ROut)
                    return ModelConsts.RedColor; // Out of spec.
                return ModelConsts.ForestGreenColor; // In Range
            }
            set { }
        }

        #endregion

        #endregion
    }

    #endregion
}
