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
using OfficeOpenXml;

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

        public void CalcStat()
        {
            if (null == Tests) return;

            decimal min = decimal.MaxValue; // set min value to Maximum
            decimal max = decimal.MinValue; // set max value to Minimum

            decimal total = decimal.Zero;
            int iCnt = 0;

            #region Calc Avg

            lock (this)
            {
                if (null != this.Tests)
                {
                    foreach (var item in this.Tests)
                    {
                        if (PropertyNo == 3 && !string.IsNullOrEmpty(UnitId))
                        {
                            // Property 3 and Has load N
                            if (string.Compare(UnitId, item.LoadN, true) != 0)
                                continue; // not match so skip.
                        }

                        if (item.N1.HasValue)
                        {
                            // update min/max
                            min = Math.Min(min, item.N1.Value);
                            max = Math.Max(max, item.N1.Value);

                            // use N to calc avg
                            total += item.Avg.Value;
                            ++iCnt;
                        }
                        if (item.N2.HasValue)
                        {
                            // update min/max
                            min = Math.Min(min, item.N2.Value);
                            max = Math.Max(max, item.N2.Value);

                            // use N to calc avg
                            total += item.Avg.Value;
                            ++iCnt;
                        }
                        if (item.N3.HasValue)
                        {
                            // update min/max
                            min = Math.Min(min, item.N3.Value);
                            max = Math.Max(max, item.N3.Value);

                            // use N to calc avg
                            total += item.Avg.Value;
                            ++iCnt;
                        }

                        /*
                        if (item.N4.HasValue)
                        {
                            // update min/max
                            min = Math.Min(min, item.N4.Value);
                            max = Math.Max(max, item.N4.Value);

                            // use N to calc avg
                            total += item.Avg.Value;
                            ++iCnt;
                        }
                        if (item.N5.HasValue)
                        {
                            // update min/max
                            min = Math.Min(min, item.N5.Value);
                            max = Math.Max(max, item.N5.Value);

                            // use N to calc avg
                            total += item.Avg.Value;
                            ++iCnt;
                        }
                        */

                        /*
                        if (item.Avg.HasValue)
                        {
                            // use N to calc avg
                            total += item.Avg.Value;
                            ++iCnt;
                        }
                        */
                    }
                }
            }
            // set total sum, total test
            this.TotalSum = total;
            this.TotalTest = iCnt;

            // Calc average value.
            this.Avg = (iCnt > 0) ? (total / iCnt) : new decimal?();
            this.MinTestValue = (iCnt > 0) ? min : new decimal?();
            this.MaxTestValue = (iCnt > 0) ? max : new decimal?();

            #endregion

            #region Calc Standard Deviation

            // formula:
            // stddev^2 = sum( (test - mean)^2) / (Count N - 1) )
            decimal? stddev = new decimal?();
            double sum2 = 0;
            lock (this)
            {
                if (null != this.Tests && this.Avg.HasValue)
                {
                    decimal avg = this.Avg.Value;
                    iCnt = 0;
                    foreach (var item in this.Tests)
                    {
                        if (PropertyNo == 3 && !string.IsNullOrEmpty(UnitId))
                        {
                            // Property 3 and Has load N
                            if (string.Compare(UnitId, item.LoadN, true) != 0)
                                continue; // not match so skip.
                        }

                        if (item.N1.HasValue)
                        {
                            // calc sum squar
                            sum2 += Math.Pow((double)(item.N1.Value - avg), 2);
                            iCnt++;
                        }
                        if (item.N2.HasValue)
                        {
                            // calc sum squar
                            sum2 += Math.Pow((double)(item.N2.Value - avg), 2);
                            iCnt++;
                        }
                        if (item.N3.HasValue)
                        {
                            // calc sum squar
                            sum2 += Math.Pow((double)(item.N3.Value - avg), 2);
                            iCnt++;
                        }

                        /*
                        if (item.N4.HasValue)
                        {
                            // calc sum squar
                            sum2 += Math.Pow((double)(item.N4.Value - avg), 2);
                            iCnt++;
                        }
                        if (item.N5.HasValue)
                        {
                            // calc sum squar
                            sum2 += Math.Pow((double)(item.N5.Value - avg), 2);
                            iCnt++;
                        }
                        */

                        /*
                        if (item.Avg.HasValue)
                        {
                            sum2 += Math.Pow((double)(item.Avg.Value - avg), 2);
                            iCnt++;
                        }
                        */
                    }

                    if (iCnt - 1 > 0)
                    {
                        stddev = Convert.ToDecimal(Math.Sqrt(sum2 / (iCnt - 1)));
                    }
                }
            }
            this.StdDev = stddev;

            #endregion

            #region Calc Cp/Cpk

            decimal? cp = new decimal?();
            decimal? cpk = new decimal?();

            if (null != Spec && 
                stddev.HasValue && stddev != 0)
            {
                decimal avg = this.Avg.Value;
                if (Spec.USL.HasValue && Spec.LSL.HasValue)
                {
                    cp = Spec.Delta.Value / (6 * stddev);
                    decimal? cpu = (Spec.USL.Value - avg) / (3 * stddev);
                    decimal? cpl = (avg - Spec.LSL.Value) / (3 * stddev);
                    cpk = (cpu < cpl) ? cpu : cpl; // use min as cpk
                }
                else if (Spec.USL.HasValue && !Spec.LSL.HasValue)
                {
                    cpk = (Spec.USL.Value - avg) / (3 * stddev);
                }
                else if (!Spec.USL.HasValue && Spec.LSL.HasValue)
                {
                    cpk = (avg - Spec.LSL.Value) / (3 * stddev);
                }
            }

            this.Cp = cp;
            this.Cpk = cpk;

            #endregion
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

        /// <summary>Gets or sets Min Test value.</summary>
        public decimal? MinTestValue
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Max Test value.</summary>
        public decimal? MaxTestValue
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
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
        /// <summary>Gets or sets Cp.</summary>
        public decimal? Cp
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Cpk.</summary>
        public decimal? Cpk
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }

        #endregion

        #region Optional

        /// <summary>Gets or sets Unit Id.</summary>
        public string UnitId { get; set; }

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
                inst.UnitId = rpt.UnitId; // keep Unit id in case Spec is null (especially for Elong at Load)

                inst.Tests = CordProductionTest.Create(inst); // Load Tests

                inst.CalcStat(); // calc related stat

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
            int propertyNo, string elongN = null)
        {
            if (null == items || items.Count <= 0)
                return null;
            if (string.IsNullOrEmpty(elongN))
            {
                return items.Find((x) => { return x.PropertyNo == propertyNo; });
            }
            else
            {
                return items.Find((x) => 
                { 
                    return x.PropertyNo == propertyNo && string.Compare(x.UnitId, elongN, true) == 0; 
                });
            }
        }

        #endregion
    }

    #endregion
}
