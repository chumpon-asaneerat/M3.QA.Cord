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

#endregion

namespace M3.QA.Models
{
    /// <summary>
    /// The Cord Production Test Item class.
    /// </summary>
    public class CordProductionTestItem : NInpc
    {
        #region Protected Properties

        // SP Gets
        protected internal Func<int?> GetSPNo { get; set; }
        // N Gets
        protected internal Func<decimal?> GetN { get; set; }
        // R1 Gets
        protected internal Func<decimal?> GetR1 { get; set; }
        // R2 Gets
        protected internal Func<decimal?> GetR2 { get; set; }

        #endregion

        #region virtual methods

        protected internal void RaiseSPNoChanges()
        {
            // Raise relelated events
            Raise(() => this.SPNo);
        }

        protected internal void RaiseNChanges()
        {
            // Raise relelated events
            Raise(() => this.N);
        }

        #endregion

        #region Public Properties

        #region SP/No

        /// <summary>
        /// Gets or sets SP No.
        /// </summary>
        public int? SPNo
        {
            get { return (null != GetSPNo) ? GetSPNo() : new int?(); }
            set { }
        }
        /// <summary>Gets or sets Test No. (N1, N2, N3)</summary>
        public int No { get; set; }

        #endregion

        #region N/R1/R2

        /// <summary>Gets or sets Test Value.</summary>
        public decimal? N
        {
            get { return (null != GetN) ? GetN() : new decimal?(); }
            set { }
        }
        /// <summary>Gets or sets Test Value.</summary>
        public decimal? R1
        {
            get { return (null != GetR1) ? GetR1() : new decimal?(); }
            set { }
        }
        /// <summary>Gets or sets Test Value.</summary>
        public decimal? R2
        {
            get { return (null != GetR2) ? GetR2() : new decimal?(); }
            set { }
        }

        #endregion

        #region CaptionN (For Runtime binding)

        /// <summary>Gets N Display Caption.</summary>
        public string CaptionN
        {
            get { return "N" + No.ToString(); }
            set { }
        }

        #endregion

        #endregion
    }
}
