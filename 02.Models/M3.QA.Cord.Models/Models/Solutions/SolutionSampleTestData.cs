#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region SolutionSampleTestData

    /// <summary>
    /// The Solution Sample Test Data class.
    /// </summary>
    public class SolutionSampleTestData
    {
        #region Public Properties

        #region Common

        public string LotNo { get; set; }
        public string ItemCode { get; set; }

        public int? MasterId { get; set; }

        #endregion

        #endregion
    }

    #endregion
}
