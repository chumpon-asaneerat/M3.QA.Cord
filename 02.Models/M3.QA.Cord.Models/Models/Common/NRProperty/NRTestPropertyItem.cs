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
        public NRTestPropertyItem() : base() 
        {
            MultiPropertyRetest = false; // default is false
        }

        #endregion

        #region virtual methods

        protected internal void RaiseSPNoChanges()
        {
            // Raise relelated events
            Raise(() => this.SPNo);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.EnableR2);

            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.VisibleR2);

            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);
            Raise(() => this.ReadOnlyR2);
        }

        protected internal void RaiseNeedSPChanges()
        {
            // Raise relelated events
            Raise(() => this.NeedSP);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.EnableR2);

            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.VisibleR2);

            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);
            Raise(() => this.ReadOnlyR2);
        }

        protected internal void RaiseYarnTypeChanges()
        {
            // Raise relelated events
            Raise(() => this.YarnType);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.EnableR2);

            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.VisibleR2);

            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);
            Raise(() => this.ReadOnlyR2);
        }

        protected internal void RaiseSampleTypeChanges()
        {
            // Raise relelated events
            Raise(() => this.SampleType);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.EnableR2);

            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.VisibleR2);

            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);
            Raise(() => this.ReadOnlyR2);
        }

        protected internal void RaiseNChanges()
        {
            // Raise relelated events
            Raise(() => this.N);
            Raise(() => this.NOut);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.EnableR2);

            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.VisibleR2);

            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);
            Raise(() => this.ReadOnlyR2);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR1);
            Raise(() => this.ForegroundColorR2);
        }

        protected internal void RaiseR1Changes()
        {
            // Raise relelated events
            Raise(() => this.R1);
            Raise(() => this.R1Out);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR1);
        }

        protected internal void RaiseR2Changes()
        {
            // Raise relelated events
            Raise(() => this.R2);
            Raise(() => this.R2Out);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR2);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR2);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR2);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR2);
        }

        protected internal void RaiseNOutChanges()
        {
            // Raise relelated events
            Raise(() => this.NOut);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.EnableR2);

            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.VisibleR2);

            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);
            Raise(() => this.ReadOnlyR2);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR1);
            Raise(() => this.ForegroundColorR2);
        }

        protected internal void RaiseR1OutChanges()
        {
            // Raise relelated events
            Raise(() => this.R1Out);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR1);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR1);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR1);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR1);
        }

        protected internal void RaiseR2OutChanges()
        {
            // Raise relelated events
            Raise(() => this.R2Out);
            Raise(() => this.EnableN);
            Raise(() => this.EnableR2);
            Raise(() => this.VisibleN);
            Raise(() => this.VisibleR2);
            Raise(() => this.ReadOnlyN);
            Raise(() => this.ReadOnlyR2);

            Raise(() => this.ForegroundColorN);
            Raise(() => this.ForegroundColorR2);
        }

        protected internal void RaiseR1FlagChanges()
        {
            // Raise relelated events
            Raise(() => this.R1Flag);
        }

        protected internal void RaiseR2FlagChanges()
        {
            // Raise relelated events
            Raise(() => this.R2Flag);
        }

        #endregion

        #region Protected Properties

        // SP Gets
        protected internal Func<int?> GetSPNo { get; set; }
        // Need SP Gets
        protected internal Func<bool> GetNeedSP { get; set; }
        // YarnType Gets
        protected internal Func<string> GetYarnType { get; set; }
        // SampleType Gets
        protected internal Func<string> GetSampleType { get; set; }
        // N Gets/Sets
        protected internal Func<decimal?> GetN { get; set; }
        protected internal Action<decimal?> SetN { get; set; }
        // R1 Gets/Sets
        protected internal Func<decimal?> GetR1 { get; set; }
        protected internal Action<decimal?> SetR1 { get; set; }
        // R2 Gets/Sets
        protected internal Func<decimal?> GetR2 { get; set; }
        protected internal Action<decimal?> SetR2 { get; set; }
        // NOut Gets/Sets
        protected internal Func<bool> GetNOut { get; set; }
        protected internal Action<bool> SetNOut { get; set; }
        // R1Out Gets/Sets
        protected internal Func<bool> GetR1Out { get; set; }
        protected internal Action<bool> SetR1Out { get; set; }
        // R2Out Gets/Sets
        protected internal Func<bool> GetR2Out { get; set; }
        protected internal Action<bool> SetR2Out { get; set; }

        // R1Flag Gets/Sets
        protected internal Func<bool> GetR1Flag { get; set; }
        protected internal Action<bool> SetR1Flag { get; set; }
        // R2Flag Gets/Sets
        protected internal Func<bool> GetR2Flag { get; set; }
        protected internal Action<bool> SetR2Flag { get; set; }

        // CustomAllowR Gets
        protected internal Func<bool> CustomAllowR { get; set; }

        // MultiPropertyRetest Gets/Sets
        protected internal Func<bool> GetMultiPropertyRetest { get; set; }
        protected internal Action<bool> SetMultiPropertyRetest { get; set; }
        // NMultiOut Gets
        protected internal Func<bool> GetNMultiOut { get; set; }

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
        /// <summary>Gets or sets Sample Type.</summary>
        public string SampleType
        {
            get { return (null != GetSampleType) ? GetSampleType() : null; }
            set { }
        }

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
                    Raise(() => this.EnableR1);
                    Raise(() => this.EnableR2);

                    Raise(() => this.VisibleN);
                    Raise(() => this.VisibleR1);
                    Raise(() => this.VisibleR2);

                    Raise(() => this.ReadOnlyN);
                    Raise(() => this.ReadOnlyR1);
                    Raise(() => this.ReadOnlyR2);

                    Raise(() => this.N);
                    Raise(() => this.ForegroundColorN);
                }
            }
        }
        /// <summary>Gets or sets Re Test 1 Value.</summary>
        public decimal? R1
        {
            get { return (null != GetR1) ? GetR1() : new decimal?(); }
            set
            {
                if (null != SetR1)
                {
                    SetR1(value);
                    // Raise events
                    Raise(() => this.R1);

                    Raise(() => this.VisibleR1);
                    Raise(() => this.ReadOnlyR1);

                    Raise(() => this.ForegroundColorR1);
                }
            }
        }
        /// <summary>Gets or sets Re Test 2 Value.</summary>
        public decimal? R2
        {
            get { return (null != GetR2) ? GetR2() : new decimal?(); }
            set
            {
                if (null != SetR2)
                {
                    SetR2(value);
                    // Raise events
                    Raise(() => this.R2);

                    Raise(() => this.VisibleR2);
                    Raise(() => this.ReadOnlyR2);

                    Raise(() => this.ForegroundColorR2);
                }
            }
        }

        #endregion

        #region NOut/ROut/RFlag

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
        /// <summary>Gets or sets Normal (Multi) Out of spec Value.</summary>
        public bool NMultiOut
        {
            get { return (null != GetNMultiOut) ? GetNMultiOut() : false; }
            set { }
        }
        /// <summary>Gets or sets Re Test Out of spec Value.</summary>
        public bool R1Out
        {
            get { return (null != GetR1Out) ? GetR1Out() : false; }
            set
            {
                if (null != SetR1Out)
                {
                    SetR1Out(value);
                    // Raise events
                    Raise(() => this.R1Out);
                    Raise(() => this.ForegroundColorR1);
                }
            }
        }
        /// <summary>Gets or sets Re Test Out of spec Value.</summary>
        public bool R2Out
        {
            get { return (null != GetR2Out) ? GetR2Out() : false; }
            set
            {
                if (null != SetR2Out)
                {
                    SetR2Out(value);
                    // Raise events
                    Raise(() => this.R2Out);
                    Raise(() => this.ForegroundColorR2);
                }
            }
        }
        /// <summary>Gets or sets Re Test Flag Value.</summary>
        public bool R1Flag
        {
            get { return (null != GetR1Flag) ? GetR1Flag() : false; }
            set
            {
                if (null != SetR1Flag)
                {
                    SetR1Flag(value);
                    // Raise events
                    Raise(() => this.R1Flag);
                }
            }
        }
        /// <summary>Gets or sets Re Test Flag Value.</summary>
        public bool R2Flag
        {
            get { return (null != GetR2Flag) ? GetR2Flag() : false; }
            set
            {
                if (null != SetR2Flag)
                {
                    SetR2Flag(value);
                    // Raise events
                    Raise(() => this.R2Flag);
                }
            }
        }

        #endregion

        #region MultiPropertyRetest

        /// <summary>Check is Enable Multi Property Retest.</summary>
        public bool MultiPropertyRetest 
        {
            get { return (null != GetMultiPropertyRetest) ? GetMultiPropertyRetest() : false; }
            set
            {
                if (null != SetMultiPropertyRetest)
                {
                    SetMultiPropertyRetest(value);
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
                return (NeedSP) ? (SPNo.HasValue && !R1.HasValue && !R2.HasValue) : !R1.HasValue && !R2.HasValue;
            } 
            set { }  
        }
        /// <summary>Check is Enable Re Test 1 (requird N value first).</summary>
        public bool EnableR1 
        { 
            get 
            {
                if (MultiPropertyRetest)
                {
                    bool ret = (NeedSP) ? SPNo.HasValue && GetNMultiOut() : (null != GetNMultiOut) ? GetNMultiOut() : false;
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
                else
                {
                    bool ret = (NeedSP) ? SPNo.HasValue && ((N.HasValue && NOut) || R1.HasValue) : ((N.HasValue && NOut) || R1.HasValue);
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
            } 
            set { } 
        }
        /// <summary>Check is Enable Re Test 2 (requird N value first).</summary>
        public bool EnableR2
        {
            get
            {
                if (MultiPropertyRetest)
                {
                    bool ret = (NeedSP) ? SPNo.HasValue && GetNMultiOut() : (null != GetNMultiOut) ? GetNMultiOut() : false;
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
                else
                {
                    bool ret = (NeedSP) ? SPNo.HasValue && ((N.HasValue && NOut) || R2.HasValue) : ((N.HasValue && NOut) || R2.HasValue);
                    bool allowR = (null != CustomAllowR) ? CustomAllowR() : true;
                    return ret && allowR;
                }
            }
            set { }
        }

        /// <summary>Check is ReadOnly Normal Test.</summary>
        public bool ReadOnlyN 
        { 
            get { return (NeedSP) ? !SPNo.HasValue || R1.HasValue || R2.HasValue : R1.HasValue || R2.HasValue; } 
            set { } 
        }
        /// <summary>Check is ReadOnly Re Test 1 (if no N not allow to enter R).</summary>
        public bool ReadOnlyR1
        { 
            get 
            {
                if (MultiPropertyRetest)
                {
                    bool ret = (NeedSP) ? !SPNo.HasValue && !GetNMultiOut() : (null != GetNMultiOut) ? (!GetNMultiOut()) : true;
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
        /// <summary>Check is ReadOnly Re Test 2 (if no N not allow to enter R).</summary>
        public bool ReadOnlyR2
        {
            get
            {
                if (MultiPropertyRetest)
                {
                    bool ret = (NeedSP) ? !SPNo.HasValue && !GetNMultiOut() : (null != GetNMultiOut) ? (!GetNMultiOut()) : true;
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
        /// <summary>Gets Visibility of Re Test 1.</summary>
        public Visibility VisibleR1 
        { 
            get 
            {
                if (MultiPropertyRetest)
                {
                    if (!EnableR1)
                    {
                        Console.WriteLine("disable");
                    }
                    return (EnableR1 || R1.HasValue) ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    //return (EnableR || R.HasValue) ? Visibility.Visible : Visibility.Collapsed;

                    // Note: In some case some row has only R data so need to display it.
                    return (EnableR1 || R1.HasValue) ? Visibility.Visible : Visibility.Collapsed;
                }
            } 
            set { } 
        }
        /// <summary>Gets Visibility of Re Test 2.</summary>
        public Visibility VisibleR2
        {
            get
            {
                if (MultiPropertyRetest)
                {
                    if (!EnableR2)
                    {
                        Console.WriteLine("disable");
                    }
                    return (EnableR2 || R2.HasValue) ? Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    //return (EnableR || R.HasValue) ? Visibility.Visible : Visibility.Collapsed;

                    // Note: In some case some row has only R data so need to display it.
                    return (EnableR2 || R2.HasValue) ? Visibility.Visible : Visibility.Collapsed;
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
        /// <summary>Gets R1 Display Caption.</summary>
        public string CaptionR 
        {
            get { return /* "N" + No.ToString() + */ "R1"; }
            set { } 
        }
        /// <summary>Gets R2 Display Caption.</summary>
        public string CaptionR2
        {
            get { return /* "N" + No.ToString() + */ "R2"; } 
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
                    return (R1.HasValue || R2.HasValue) ? ModelConsts.DimGrayColor : ModelConsts.BlackColor; // No input
                if (NOut)
                    return ModelConsts.RedColor; // Out of spec.
                return (R1.HasValue || R2.HasValue) ? ModelConsts.DimGrayColor : ModelConsts.ForestGreenColor; 
            }
            set { }
        }
        /// <summary>Gets R1 Foreground Color.</summary>
        public SolidColorBrush ForegroundColorR1
        {
            get 
            {
                if (!R1.HasValue)
                    return ModelConsts.BlackColor; // No input
                if (R1Out)
                    return ModelConsts.RedColor; // Out of spec.
                return ModelConsts.ForestGreenColor; // In Range
            }
            set { }
        }
        /// <summary>Gets R2 Foreground Color.</summary>
        public SolidColorBrush ForegroundColorR2
        {
            get
            {
                if (!R2.HasValue)
                    return ModelConsts.BlackColor; // No input
                if (R2Out)
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
