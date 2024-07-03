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
    #region DIPSolutionPH

    /// <summary>
    /// The DIP Solution PH class.
    /// </summary>
    public class DIPSolutionPH : NRTestProperty
    {
        #region Protected Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected override void CheckSpec()
        {
            if (null == Spec || Spec.SpecId <= 0)
                return;

            this.N1Out = (N1.HasValue) ? Spec.IsOutOfSpec(N1.Value) : false;
            this.N2Out = (N2.HasValue) ? Spec.IsOutOfSpec(N2.Value) : false;
            this.N3Out = (N3.HasValue) ? Spec.IsOutOfSpec(N3.Value) : false;
            this.N4Out = (N4.HasValue) ? Spec.IsOutOfSpec(N4.Value) : false;
            this.N5Out = (N5.HasValue) ? Spec.IsOutOfSpec(N5.Value) : false;
            this.N6Out = (N6.HasValue) ? Spec.IsOutOfSpec(N6.Value) : false;
            this.N7Out = (N7.HasValue) ? Spec.IsOutOfSpec(N7.Value) : false;

            this.N1R1Out = (N1R1.HasValue) ? Spec.IsOutOfSpec(N1R1.Value) : false;
            this.N1R2Out = (N1R2.HasValue) ? Spec.IsOutOfSpec(N1R2.Value) : false;
            this.N2R1Out = (N2R1.HasValue) ? Spec.IsOutOfSpec(N2R1.Value) : false;
            this.N2R2Out = (N2R2.HasValue) ? Spec.IsOutOfSpec(N2R2.Value) : false;
            this.N3R1Out = (N3R1.HasValue) ? Spec.IsOutOfSpec(N3R1.Value) : false;
            this.N3R2Out = (N3R2.HasValue) ? Spec.IsOutOfSpec(N3R2.Value) : false;
            this.N4R1Out = (N4R1.HasValue) ? Spec.IsOutOfSpec(N4R1.Value) : false;
            this.N4R2Out = (N4R2.HasValue) ? Spec.IsOutOfSpec(N4R2.Value) : false;
            this.N5R1Out = (N5R1.HasValue) ? Spec.IsOutOfSpec(N5R1.Value) : false;
            this.N5R2Out = (N5R2.HasValue) ? Spec.IsOutOfSpec(N5R2.Value) : false;
            this.N6R1Out = (N6R1.HasValue) ? Spec.IsOutOfSpec(N6R1.Value) : false;
            this.N6R2Out = (N6R2.HasValue) ? Spec.IsOutOfSpec(N6R2.Value) : false;
            this.N7R1Out = (N7R1.HasValue) ? Spec.IsOutOfSpec(N7R1.Value) : false;
            this.N7R2Out = (N7R2.HasValue) ? Spec.IsOutOfSpec(N7R2.Value) : false;

            // Raise items events
            this.RaiseNOutChanges();
            this.RaiseR1OutChanges();
            this.RaiseR2OutChanges();

            if (null != CalcAvgCallback) CalcAvgCallback("PH");
        }

        #endregion

        internal Action<string> CalcAvgCallback { get; set; }

        #region Public Properties

        #region User/EditDate

        public string InputBy { get; set; }
        public DateTime? InputDate { get; set; }
        public string EditBy { get; set; }
        public DateTime? EditDate { get; set; }

        #endregion

        #endregion

        #region Static Methods

        #region Clone

        /// <summary>
        /// Clone.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void Clone(CordThickness src, CordThickness dst)
        {
            if (null == src || null == dst)
                return;

            dst.LotNo = src.LotNo;
            dst.PropertyNo = src.PropertyNo;
            dst.SPNo = src.SPNo;
            dst.NoOfSample = src.NoOfSample;
            dst.YarnType = src.YarnType;

            dst.EditBy = src.EditBy;
            dst.EditDate = src.EditDate;
            dst.InputBy = src.InputBy;
            dst.InputDate = src.InputDate;

            if (null != dst.Items && null != src.Items)
            {
                int iMax = (dst.Items.Count > src.Items.Count) ? dst.Items.Count : src.Items.Count;
                for (int i = 0; i < iMax; i++)
                {
                    if (i < dst.Items.Count && i < src.Items.Count)
                    {
                        dst.Items[i].N = src.Items[i].N;
                        dst.Items[i].R1 = src.Items[i].R1;
                        dst.Items[i].R2 = src.Items[i].R2;

                        dst.Items[i].R1Flag = src.Items[i].R1Flag;
                        dst.Items[i].R2Flag = src.Items[i].R2Flag;
                        dst.Items[i].SampleType = src.Items[i].SampleType;
                    }
                }
            }
        }

        #endregion

        #region Create

        /// <summary>
        /// Create
        /// </summary>
        /// <param name="value"></param>
        /// <param name="PhN"></param>
        /// <param name="PhR"></param>
        /// <returns></returns>
        internal static DIPSolutionPH Create(DIPSolutionSampleTestData value,
            decimal? PhN, decimal? PhR)
        {
            DIPSolutionPH result = null;
            if (null == value)
                return result;

            // Ph Proepty No = 16
            var spec = value.Specs.FindByPropertyNo(16);
            // For Ph Proepty No = 16
            int noOfSample = (null != spec) ? spec.NoSample : 1; // default 1

            result = new DIPSolutionPH();
            result.LotNo = value.LotNo;
            result.PropertyNo = 16; // Ph = 16
            result.NeedSP = false;
            result.Spec = spec;
            result.NoOfSample = noOfSample;
            result.N1 = PhN;
            result.N1R1 = PhR;
            result.N1R2 = PhR;

            return result;
        }

        #endregion

        #endregion
    }

    #endregion
}
