#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordTestSpec

    /// <summary>
    /// The Cord Test Spec class.
    /// </summary>
    public class CordTestSpec : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordTestSpec() : base()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets Product Code.</summary>
        public string ProductCode 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets MasterId.</summary>
        public int? MasterId
        {
            get { return Get<int?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets PropertyNo.</summary>
        public int PropertyNo
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }


        /// <summary>Gets or sets SpecId.</summary>
        public int SpecId
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Spec description.</summary>
        public string SpecDesc
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or sets UnitId.</summary>
        public int UnitId
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Unit description.</summary>
        public string UnitDesc
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        #endregion
    }

    #endregion
}
