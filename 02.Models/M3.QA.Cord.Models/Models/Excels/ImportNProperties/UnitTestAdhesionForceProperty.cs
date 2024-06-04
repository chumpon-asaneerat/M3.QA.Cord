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
using NLib.Controls;
using NLib.Data;
using NLib.Models;
using NLib.Reflection;

#endregion

namespace M3.QA.Models
{
    public class UnitTestAdhesionForceProperty : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public UnitTestAdhesionForceProperty() : base()
        {
            PeakPoint = new ImportNTestProperty();
            AdhesionForce = new ImportNTestProperty();

            // Check calculate action
            if (null == PeakPoint.ValueChanges)
            {
                PeakPoint.ValueChanges = CalculateFormula;
            }
        }

        #endregion

        #region Private Methods

        private void CalculateFormula()
        {
            if (null != PeakPoint && null != AdhesionForce)
            {
                AdhesionForce.N1 = (PeakPoint.N1.HasValue) ? PeakPoint.N1.Value / 5 : new decimal?();
                AdhesionForce.N2 = (PeakPoint.N2.HasValue) ? PeakPoint.N2.Value / 5 : new decimal?();
                // Raise events
                Raise(() => this.AdhesionForce);
            }
        }

        #endregion

        #region Public Methods

        public void UpdateProperties()
        {
            if (null == PeakPoint) PeakPoint = new ImportNTestProperty();

            PeakPoint.LotNo = LotNo;
            PeakPoint.SPNo = SPNo;
            PeakPoint.NoOfSample = NoOfSample;

            if (null == AdhesionForce) AdhesionForce = new ImportNTestProperty();
            AdhesionForce.SPNo = SPNo;
            AdhesionForce.LotNo = LotNo;
            AdhesionForce.SPNo = SPNo;
            AdhesionForce.NoOfSample = NoOfSample;

            // Check calculate action
            if (null == PeakPoint.ValueChanges)
            {
                PeakPoint.ValueChanges = CalculateFormula;
            }

            CalculateFormula(); // calculate
        }

        #endregion

        #region Public Properties

        public string LotNo { get; set; }
        public int? SPNo { get; set; }
        public int NoOfSample { get; set; } = 2;
        public string YarnType { get; set; }

        /// <summary>Gets or sets PeakPoint.</summary>
        public ImportNTestProperty PeakPoint { get; set; }
        /// <summary>Gets or sets AdhesionForce.</summary>
        public ImportNTestProperty AdhesionForce { get; set; }

        #endregion
    }
}
