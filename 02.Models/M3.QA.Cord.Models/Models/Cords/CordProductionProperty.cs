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

#endregion

namespace M3.QA.Models
{
    #region CordProductionProperty

    /// <summary>
    /// The Cord Production Property class
    /// </summary>
    public class CordProductionProperty : NInpc
    {
        #region Public Methods

        public void CalcAverage()
        {
            if (null == Tests) return;

            decimal total = decimal.Zero;
            int iCnt = 0;
            lock (this)
            {
                if (null != this.Tests)
                {
                    foreach (var item in this.Tests)
                    {
                        if (item.Avg.HasValue)
                        {
                            // Has N value and no R value so use N to calc avg
                            total += item.Avg.Value;
                            ++iCnt;
                        }
                    }
                }
            }
            // set total sum, total test
            this.TotalSum = total;
            this.TotalTest = iCnt;

            // Calc average value.
            this.Avg = (iCnt > 0) ? (total / iCnt) : new decimal?();

            // calc deviation
            // formula:
            // stddev^2 = sum( (test - mean)^2) / (Count N - 1) )
            decimal? stddev = new decimal?();
            double sum2 = 0;
            lock (this)
            {
                if (null != this.Tests && iCnt >= 1 && this.Avg.HasValue)
                {
                    decimal avg = this.Avg.Value;
                    foreach (var item in this.Tests)
                    {
                        if (item.Avg.HasValue)
                        {
                            // Has N value and no R value so use N to calc avg
                            sum2 += Math.Pow((double)(item.Avg.Value - avg), 2);
                        }
                    }

                    stddev = Convert.ToDecimal(Math.Sqrt(sum2 / (iCnt - 1)));
                }
            }
            this.StdDev = stddev;
        }

        #endregion

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

        #region Avg

        /// <summary>Gets or sets Avg (or mean) value.</summary>
        public decimal? Avg
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Sum of all test value.</summary>
        public decimal? TotalSum
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets No. of test value.</summary>
        public decimal? TotalTest
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets standard deviation.</summary>
        public decimal? StdDev
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }

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

            spec.UnitReport = value.UnitReport;

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

                inst.CalcAverage(); // calc total avg

                results.Add(inst);
            }

            return results;
        }

        #endregion
    }

    #endregion

    #region CordProductionPropertyExtensionMethods

    public static class CordProductionPropertyExtensionMethods
    {
        #region Find By Property No

        /// <summary>
        /// Find By Property No.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="propertyNo"></param>
        /// <returns></returns>
        public static CordProductionProperty FindByPropertyNo(this List<CordProductionProperty> items,
            int propertyNo)
        {
            if (null == items || items.Count <= 0)
                return null;

            return items.Find((x) => { return x.PropertyNo == propertyNo; });
        }

        #endregion
    }

    #endregion
}
