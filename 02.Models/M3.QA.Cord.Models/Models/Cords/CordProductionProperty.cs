﻿#region Using

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
    /// The Cord Production Property class
    /// </summary>
    public class CordProductionProperty : NInpc
    {
        #region Public Properties

        #region LotNo/PropertyNo/SPNo/NoOfSample

        /// <summary>Gets or sets Lot No.</summary>
        public string LotNo { get; set; }
        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo { get; set; }
        /// <summary>Gets Max No of Test/Retest.</summary>
        public int NoOfSample { get; set; }
        /// <summary>Gets or sets Yarn Type.</summary>
        public string YarnType { get; set; }

        #endregion

        #region ItemCode/PropertyName/MasterId

        public string ItemCode { get; set; }
        public string PropertyName { get; set; }
        public int? MasterId { get; set; }

        #endregion

        #region Spec

        /// <summary>Gets or sets CordTestSpec.</summary>
        public CordTestSpec Spec { get; set; }

        #endregion

        #region Tests

        /// <summary>
        /// Gets Test Items.
        /// </summary>
        public List<CordProductionTest> Tests { get; set; }

        #endregion

        #endregion

        #region Static Methods

        private static CordTestSpec GetSpec(Utils.M_GetReportTestSpecByMasterid value)
        {
            CordTestSpec spec = null;
            if (null == value)
                return spec;

            // Setup Spec
            spec = new CordTestSpec();
            spec.ItemCode = value.ItemCode;
            spec.MasterId = value.MasterId;

            spec.PropertyNo = value.PropertyNo;
            spec.PropertyName = value.PropertName;
            spec.NoSample = value.NoSample;

            spec.SpecId = value.SpecId;
            spec.SpecDesc = value.SpecDesc;

            spec.UnitId = value.UnitId;
            spec.UnitDesc = value.UnitDesc;

            spec.OptionId = value.OptionId;
            spec.OptionDesc = value.OptionDesc;

            spec.VCenter = value.VCenter;
            spec.VMin = value.VMin;
            spec.VMax = value.VMax;

            spec.TestMethod = value.TestMethod;

            return spec;
        }

        public static List<CordProductionProperty> Create(CordProduction value)
        {
            var results = new List<CordProductionProperty>();

            if (null == value)
                return results;

            var rpts = Utils.M_GetReportTestSpecByMasterid.Gets(value.MasterId).Value();
            if (null == rpts)
                return results;

            foreach (var rpt in rpts)
            {
                var inst = new CordProductionProperty();

                inst.LotNo = value.LotNo; // Assign Lot from parent source

                inst.ItemCode = rpt.ItemCode;
                inst.MasterId = rpt.MasterId;

                inst.PropertyNo = rpt.PropertyNo;
                inst.PropertyName = (string.IsNullOrEmpty(rpt.PropertName)) ? null : rpt.PropertName.Trim();

                inst.NoOfSample = rpt.NoSample;

                inst.Spec = GetSpec(rpt); // Setup Spec.

                inst.Tests = CordProductionTest.Create(inst); // Load Tests

                results.Add(inst);
            }

            return results;
        }

        #endregion
    }
}
