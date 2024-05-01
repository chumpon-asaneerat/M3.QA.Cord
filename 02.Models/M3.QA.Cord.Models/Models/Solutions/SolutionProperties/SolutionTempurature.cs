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
    #region SolutionTempurature

    /// <summary>
    /// The Solution Tempurature class.
    /// </summary>
    public class SolutionTempurature : NRTestProperty
    {
        #region Protected Methods

        /// <summary>
        /// Check Spec.
        /// </summary>
        protected override void CheckSpec()
        {
            if (null == Spec || Spec.SpecId <= 0)
                return;

            this.NOut1 = (N1.HasValue) ? Spec.IsOutOfSpec(N1.Value) : false;
            this.NOut2 = (N2.HasValue) ? Spec.IsOutOfSpec(N2.Value) : false;
            this.NOut3 = (N3.HasValue) ? Spec.IsOutOfSpec(N3.Value) : false;
            this.NOut4 = (N4.HasValue) ? Spec.IsOutOfSpec(N4.Value) : false;
            this.NOut5 = (N5.HasValue) ? Spec.IsOutOfSpec(N5.Value) : false;
            this.NOut6 = (N6.HasValue) ? Spec.IsOutOfSpec(N6.Value) : false;
            this.NOut7 = (N7.HasValue) ? Spec.IsOutOfSpec(N7.Value) : false;

            this.ROut1 = (R1.HasValue) ? Spec.IsOutOfSpec(R1.Value) : false;
            this.ROut2 = (R2.HasValue) ? Spec.IsOutOfSpec(R2.Value) : false;
            this.ROut3 = (R3.HasValue) ? Spec.IsOutOfSpec(R3.Value) : false;
            this.ROut4 = (R4.HasValue) ? Spec.IsOutOfSpec(R4.Value) : false;
            this.ROut5 = (R5.HasValue) ? Spec.IsOutOfSpec(R5.Value) : false;
            this.ROut6 = (R6.HasValue) ? Spec.IsOutOfSpec(R6.Value) : false;
            this.ROut7 = (R7.HasValue) ? Spec.IsOutOfSpec(R7.Value) : false;

            // Raise items events
            this.RaiseNOutChanges();
            this.RaiseROutChanges();
        }

        #endregion

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
                        dst.Items[i].R = src.Items[i].R;
                    }
                }
            }
        }

        #endregion

        #endregion
    }

    #endregion
}
