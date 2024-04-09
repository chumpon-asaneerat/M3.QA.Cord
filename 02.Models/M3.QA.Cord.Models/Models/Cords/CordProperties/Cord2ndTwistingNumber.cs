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
    #region Cord2ndTwistingNumber

    /// <summary>
    /// The Cord 2nd Twisting Number class.
    /// </summary>
    public class Cord2ndTwistingNumber
    {
        #region Public Properties

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #endregion

        #region Static Methods

        #endregion
    }

    #endregion
}
