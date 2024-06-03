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
    /// <summary>
    /// The UnitTest Adhesion Force Property class
    /// </summary>
    public class UnitTestAdhesionForceProperty : NInpc
    {
        #region Public Properties

        #region LotNo/SPNo/NoOfSample/YarnType

        public string LotNo { get; set; }
        public int? SPNo { get; set; }
        public int NoOfSample { get; set; } = 3;
        public string YarnType { get; set; }

        #endregion

        #region PeakPoint/AdhesionForce

        /// <summary>Gets or sets PeakPoint.</summary>
        public ImportNTestProperty PeakPoint { get; set; }
        /// <summary>Gets or sets AdhesionForce.</summary>
        public ImportNTestProperty AdhesionForce { get; set; }

        #endregion
    }
}
