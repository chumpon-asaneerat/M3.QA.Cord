#region Using

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region DIPSolutionBreaker

    /// <summary>
    /// The DIP Solution Breaker class.
    /// </summary>
    public class DIPSolutionBreaker : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public DIPSolutionBreaker() : base()
        {
            BreakerWeight = new NRTestProperty();
            // Set calc formula
            BreakerWeight.ValueChanges = CalculateFormula;

            BreakerWeightBeforeHeat = new NRTestProperty();
            // Set calc formula
            BreakerWeight.ValueChanges = CalculateFormula;

            BreakerWeightAfterHeat = new NRTestProperty();
            // Set calc formula
            BreakerWeight.ValueChanges = CalculateFormula;
        }

        #endregion

        #region Private Methods

        private void CalculateFormula()
        {

        }

        #endregion

        #region Public Properties

        #region BreakerW/BreakerWBH/BreakerWAH

        /// <summary>Gets or sets BreakerWeight.</summary>
        public NRTestProperty BreakerWeight { get; set; }
        /// <summary>Gets or sets BreakerWeightBeforeHeat.</summary>
        public NRTestProperty BreakerWeightBeforeHeat { get; set; }
        /// <summary>Gets or sets BreakerWeightAfterHeat.</summary>
        public NRTestProperty BreakerWeightAfterHeat { get; set; }

        #endregion

        #endregion
    }

    #endregion
}
