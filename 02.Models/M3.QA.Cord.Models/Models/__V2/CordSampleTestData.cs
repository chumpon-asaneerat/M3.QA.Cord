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

namespace M3.QA.V2.Models
{
    #region CordSampleTestData

    /// <summary>
    /// The CordSampleTestData class.
    /// </summary>
    public class CordSampleTestData : NInpc
    {
        #region Public Properties

        public string LotNo { get; set; }
        public string ItemCode { get; set; }

        public int? MasterId { get; set; }

        public int? SP1 { get; set; }
        public int? SP2 { get; set; }
        public int? SP3 { get; set; }
        public int? SP4 { get; set; }
        public int? SP5 { get; set; }
        public int? SP6 { get; set; }
        public int? SP7 { get; set; }

        public int? TotalSP { get; set; }
        public DateTime? StartTestDate { get; set; }

        public string Spindle { get; set; }
        public string ELongLoadN { get; set; }

        public bool CanEditStartDate { get; set; }

        /// <summary>The Tensile Strengths Items.</summary>
        public List<CordTensileStrength> TensileStrengths { get; set; }

        #endregion

        #region Private Methods

        private void InitTestProperties()
        {
            TensileStrengths = CordTensileStrength.Create(this);
        }

        #endregion
    }

    #endregion
}
