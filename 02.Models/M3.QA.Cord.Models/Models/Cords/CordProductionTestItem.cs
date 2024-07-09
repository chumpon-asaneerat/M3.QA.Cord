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
using System.Windows;

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
        // O Gets
        protected internal Func<decimal?> GetO { get; set; }

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

        protected internal void RaiseOChanges()
        {
            // Raise relelated events
            Raise(() => this.O);
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

        #region N/O

        /// <summary>Gets or sets Test Value.</summary>
        public decimal? N
        {
            get { return (null != GetN) ? GetN() : new decimal?(); }
            set { }
        }
        /// <summary>Gets or sets Test Value.</summary>
        public decimal? O
        {
            get { return (null != GetO) ? GetO() : new decimal?(); }
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
        /// <summary>Gets O Display Caption.</summary>
        public string CaptionO
        {
            get { return /* "N" + No.ToString() + */ "?"; }
            set { }
        }
        /// <summary>Gets O Visibility.</summary>
        public Visibility OVisible
        {
            get { return O.HasValue ? Visibility.Visible : Visibility.Collapsed; }
            set { }
        }

        #endregion

        #endregion
    }
}
