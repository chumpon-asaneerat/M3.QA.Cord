#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;

using NLib;
using NLib.Components;
using NLib.Data;
using NLib.Models;
using NLib.Reflection;

#endregion

namespace M3.QA.Models
{
    #region UniTestElongationSubProperty

    /// <summary>
    /// The UniTestElongationSubProperty class.
    /// </summary>
    public class UniTestElongationSubProperty : ImportNTestProperty
    {
        #region Public Properties

        /// <summary>Gets is Need Eload.</summary>
        public virtual bool NeedEload { get; set; } = true;
        /// <summary>Gets is show Eload.</summary>
        public Visibility ShowEload
        {
            get { return NeedEload ? Visibility.Visible : Visibility.Hidden; }
            set { }
        }
        /// <summary>Gets Proeprty No.</summary>
        public virtual int PropertyNo { get; set; }
        /// <summary>Gets Property Text.</summary>
        public virtual string PropertyText { get { return "unknown"; } set { } }
        /// <summary>Gets or sets LoadN.</summary>
        public string LoadN { get; set; }

        #endregion
    }

    #endregion

    #region UniTestElongationSubProperty Extension Methods

    public static class UniTestElongationSubPropertyExtensionMethods
    {
        public static UniTestElongationSubProperty FindByElong(this List<UniTestElongationSubProperty> values, string loadN)
        {
            if (null == values || values.Count <= 0)
                return null;

            return values.Find(x => string.Compare(x.LoadN, loadN, true) == 0);
        }
    }

    #endregion

    #region UniTestElongationBreakProperty

    /// <summary>
    /// The UniTestElongationBreakProperty class.
    /// </summary>
    public class UniTestElongationBreakProperty : UniTestElongationSubProperty
    {
        #region Public Properties

        /// <summary>Gets is Need Eload.</summary>
        public override bool NeedEload
        {
            get { return false; }
            set { }
        }
        /// <summary>Gets Proeprty No.</summary>
        public override int PropertyNo { get { return 2; } set { } }

        /// <summary>Gets Property Text.</summary>
        public override string PropertyText { get { return "at Break"; } set { } }

        #endregion
    }

    #endregion

    #region UniTestElongationLoadProperty

    /// <summary>
    /// The UniTestElongationLoadProperty class.
    /// </summary>
    public class UniTestElongationLoadProperty : UniTestElongationSubProperty
    {
        #region Public Properties

        /// <summary>Gets is Need Eload.</summary>
        public override bool NeedEload
        {
            get { return true; }
            set { }
        }
        /// <summary>Gets Proeprty No.</summary>
        public override int PropertyNo { get { return 3; } set { } }

        /// <summary>Gets Property Text.</summary>
        public override string PropertyText { get { return "at Load"; } set { } }

        #endregion
    }

    #endregion

    #region UniTestElongation

    /// <summary>
    /// The UniTestElongation class.
    /// </summary>
    public class UniTestElongation : NInpc
    {
        #region Public Properties

        public string LotNo { get; set; }
        public int? SPNo { get; set; }
        public int NoOfSample { get; set; } = 3;
        public string ELongLoadN { get; set; }
        public string YarnType { get; set; }

        public List<UniTestElongationSubProperty> SubProperties { get; set; } = new List<UniTestElongationSubProperty>();

        #endregion
    }

    #endregion
}
