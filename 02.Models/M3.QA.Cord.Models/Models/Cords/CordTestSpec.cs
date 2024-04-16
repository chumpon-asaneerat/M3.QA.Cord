#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordTestSpec

    /// <summary>
    /// The Cord Test Spec class.
    /// </summary>
    public class CordTestSpec : NInpc
    {
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public CordTestSpec() : base()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Check is out of spec.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns></returns>
        public bool IsOutOfSpec(decimal? value)
        {
            bool ret = false;
            MethodBase med = MethodBase.GetCurrentMethod();

            if (!value.HasValue) 
            {
                //med.Info("Value is null.");
                return ret;
            }

            var dVal = value.Value;
            switch (SpecId)
            {
                case 0: // No Checking
                    {
                        ret = true;
                        break;
                    }
                case 1: // Plus-Minus
                    {
                        bool checkMin = VMin.HasValue;
                        bool checkMax = VMax.HasValue;

                        decimal vMin, vMax;
                        if (VCenter.HasValue)
                        {
                            vMin = VCenter.Value + (VMin.HasValue ? VMin.Value : decimal.Zero);
                            vMax = VCenter.Value + (VMax.HasValue ? VMax.Value : decimal.Zero);
                        }
                        else
                        {
                            vMin = (VMin.HasValue ? VMin.Value : decimal.Zero);
                            vMax = (VMax.HasValue ? VMax.Value : decimal.Zero);
                        }

                        bool inRange;
                        if (checkMin && checkMax)
                            inRange = dVal >= vMin && dVal <= vMax; // Check Min-Max
                        else if (checkMin && !checkMax)
                            inRange = dVal >= vMin; // Check Min Only
                        else if (!checkMin && checkMax)
                            inRange = dVal <= vMax; // Check Max Only
                        else inRange = true; // No Min-Max

                        ret = !inRange;

                        break;
                    }
                case 2: // Min-Max
                    {
                        bool checkMin = VMin.HasValue;
                        bool checkMax = VMax.HasValue;

                        decimal vMin, vMax;
                        vMin = VMin.HasValue ? VMin.Value : decimal.Zero;
                        vMax = VMax.HasValue ? VMax.Value : decimal.Zero;

                        bool inRange;
                        if (checkMin && checkMax)
                            inRange = dVal >= vMin && dVal <= vMax; // Check Min-Max
                        else if (checkMin && !checkMax)
                            inRange = dVal >= vMin; // Check Min Only
                        else if (!checkMin && checkMax)
                            inRange = dVal <= vMax; // Check Max Only
                        else inRange = true; // No Min-Max

                        ret = !inRange;

                        break;
                    }
                default: 
                    {
                        ret = true; // not found SpecId so assume value is valid.
                        break;
                    }
            }

            return ret;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets Product Code.</summary>
        public string ProductCode 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets MasterId.</summary>
        public int? MasterId
        {
            get { return Get<int?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets PropertyNo.</summary>
        public int PropertyNo
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }


        /// <summary>Gets or sets SpecId.</summary>
        public int SpecId
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Spec description.</summary>
        public string SpecDesc
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or sets UnitId.</summary>
        public int UnitId
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Unit description.</summary>
        public string UnitDesc
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or sets Option.</summary>
        public int Option
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Option description.</summary>
        public string OptionDesc
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or sets VCenter.</summary>
        public decimal? VCenter
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets VMin.</summary>
        public decimal? VMin
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets VMax.</summary>
        public decimal? VMax
        {
            get { return Get<decimal?>(); }
            set { Set(value, () => { }); }
        }


        #endregion
    }

    #endregion
}
