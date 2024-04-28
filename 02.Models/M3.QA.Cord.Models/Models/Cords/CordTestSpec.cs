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
using static M3.QA.Models.Utils;

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

        #region Private Methods

        private string GetNoneSpec()
        {
            string ret = string.Empty;
            // No Check.
            ret += "None";
            return ret;
        }

        private string GetPlusMinusSpec()
        {
            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(UnitDesc))
            {
                ret += string.Format("[ {0} ] - ", UnitDesc);
            }

            string part1 = string.Empty;
            string part2 = string.Empty;

            // Plus/Minus
            decimal dCenter = VCenter.HasValue ? VCenter.Value : 0;

            if (VMin.HasValue && VMax.HasValue)
            {
                // has both min/max
                if (VMin.Value == VMax.Value)
                {
                    part1 += string.Format("N: {0:#,##0.###} ± {1:#,##0.###}", dCenter,  VMin.Value);
                }
                else
                {
                    part1 += string.Format("N: {0:#,##0.###} + {1:#,##0.###}, {0:#,##0.###} - {2:#,##0.###}", 
                        dCenter, VMax.Value, VMin.Value);
                }

                part2 += string.Format(" ( {0:#,##0.###} ≤ N ≤ {1:#,##0.###} ) ", dCenter - VMin.Value, dCenter + VMax.Value);

                ret += part1 + part2;
            }
            else if (VMin.HasValue && !VMax.HasValue)
            {
                // has min only
                part1 += string.Format("N: {0:#,##0.###} - {1:#,##0.###}", dCenter, VMin.Value);
                part2 += string.Format(" ( N ≥ {0:#,##0.###} )", dCenter - VMin.Value);

                ret += part1 + part2;
            }
            else if (!VMin.HasValue && VMax.HasValue)
            {
                // has max only
                part1 += string.Format("N: {0:#,##0.###} + {1:#,##0.###}", dCenter, VMax.Value);
                part2 += string.Format(" ( N ≤ {0:#,##0.###} )", dCenter + VMax.Value);

                ret += part1 + part2;
            }
            else
            {
                ret += "Plus/Minus Specification is not set.";
            }

            return ret;
        }

        private string GetMinMaxSpec()
        {
            string ret = string.Empty;

            if (!string.IsNullOrWhiteSpace(UnitDesc))
            {
                ret += string.Format("[ {0} ] - ", UnitDesc);
            }

            // Min/Max
            if (VMin.HasValue && VMax.HasValue)
            {
                // Has both value
                ret += string.Format("{0:#,##0.###} ≤ N ≤ {1:#,##0.###}", VMin.Value, VMax.Value);
            }
            else if (VMin.HasValue && !VMax.HasValue)
            {
                // Has Min value only
                ret += string.Format("N ≥ {0:#,##0.###}", VMin.Value);
            }
            else if (!VMin.HasValue && VMax.HasValue)
            {
                // Has Max value only
                ret += string.Format("N ≤ {0:#,##0.###}", VMax.Value);
            }
            else
            {
                // No min-max assign
                ret += "Min/Max Specification is not set.";
            }

            return ret;
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
                        ret = false;
                        break;
                    }
                case 1: // Plus-Minus
                    {
                        bool checkMin = VMin.HasValue;
                        bool checkMax = VMax.HasValue;

                        decimal vMin, vMax;
                        if (VCenter.HasValue)
                        {
                            vMin = VCenter.Value - (VMin.HasValue ? VMin.Value : decimal.Zero);
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
                        ret = false; // not found SpecId so assume value is valid.
                        break;
                    }
            }

            return ret;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets Item Code.</summary>
        public string ItemCode 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Product Name.</summary>
        public string ProductName
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
        public string UnitId
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or sets Unit description.</summary>
        public string UnitDesc
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or sets OptionId.</summary>
        public string OptionId
        {
            get { return Get<string>(); }
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

        /// <summary>Gets or sets Unit Report.</summary>
        public string UnitReport
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        public string SpecInfo
        {
            get 
            {
                string ret = string.Empty;
                if (SpecId == 0) ret = GetNoneSpec();
                else if (SpecId == 1) ret = GetPlusMinusSpec();
                else if (SpecId == 2) ret = GetMinMaxSpec();
                return ret;
            }
            set { }
        }

        /// <summary>Gets Spec Visibility.</summary>
        public Visibility SpecVisibility
        {
            get { return (SpecId > 0) ? Visibility.Visible : Visibility.Collapsed; }
            set { }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets Specification.
        /// </summary>
        /// <param name="masterId"></param>
        /// <param name="propertyNo"></param>
        /// <returns></returns>
        public static NDbResult<List<CordTestSpec>> Gets(int? masterId = new int?(), 
            int? propertyNo = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordTestSpec>> ret = new NDbResult<List<CordTestSpec>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();

            p.Add("@masterId", masterId);
            p.Add("@propertyNo", propertyNo);

            try
            {
                var items = cnn.Query<CordTestSpec>("M_GetTestSpecificationByItem", p, 
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;

                ret.Success(data);
                // Set error number/message
                ret.ErrNum = 0;
                ret.ErrMsg = "Success";
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }
        /// <summary>
        /// Gets Specification By MasterId.
        /// </summary>
        /// <param name="masterId"></param>
        /// <returns></returns>
        public static NDbResult<List<CordTestSpec>> GetsByMasterId(int? masterId = new int?())
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordTestSpec>> ret = new NDbResult<List<CordTestSpec>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                ret.ErrNum = 8000;
                ret.ErrMsg = msg;

                return ret;
            }

            var p = new DynamicParameters();

            p.Add("@masterId", masterId);

            try
            {
                var items = cnn.Query<CordTestSpec>("M_GetTestSpecByMasterid", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;

                ret.Success(data);
                // Set error number/message
                ret.ErrNum = 0;
                ret.ErrMsg = "Success";
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                ret.ErrNum = 9999;
                ret.ErrMsg = ex.Message;
            }

            return ret;
        }

        #endregion
    }

    #endregion

    #region CordTestSpecExtensionMethods

    public static class CordTestSpecExtensionMethods
    {
        #region Find By Property No

        /// <summary>
        /// Find By Property No.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="propertyNo"></param>
        /// <param name="elongId"></param>
        /// <returns></returns>
        public static CordTestSpec FindByPropertyNo(this List<CordTestSpec> items,  
            int propertyNo, string elongId = null)
        {
            if (null == items || items.Count <= 0)
                return null;

            CordTestSpec spec;
            spec = items.Find((x) =>
            {
                bool ret = false;

                if (string.IsNullOrEmpty(elongId))
                {
                    // No Elong
                    if (!string.IsNullOrEmpty(x.UnitId))
                    {
                        // Has UnitId then OptionId required
                        if (!string.IsNullOrEmpty(x.OptionId))
                        {
                            // Has OptionId so return first OptionId = '1'
                            ret = x.PropertyNo == propertyNo && x.OptionId == "1";
                        }
                    }
                    else
                    {
                        // No Unit id
                        ret = x.PropertyNo == propertyNo;
                    }
                }
                else
                {
                    // Need Elong to check.
                    if (!string.IsNullOrEmpty(x.UnitId))
                    {
                        // Has UnitId then OptionId required
                        if (!string.IsNullOrEmpty(x.OptionId))
                        {
                            // Has OptionId so return first OptionId = '1'
                            ret = x.PropertyNo == propertyNo && 
                                string.Compare(elongId, x.UnitId, true) == 0 && 
                                x.OptionId == "1";
                        }
                    }
                    else
                    {
                        // No Unit id
                        ret = x.PropertyNo == propertyNo;
                    }
                }
                return ret;
            });

            if (null == spec)
            {
                spec = new CordTestSpec()
                {
                    PropertyNo = propertyNo,
                    SpecId = 0,
                    UnitId = null,
                    OptionId = null
                };
            }

            return spec;
        }
        /// <summary>
        /// Find List By Property No.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="propertyNo"></param>
        /// <returns></returns>
        public static List<CordTestSpec> FindListByPropertyNo(this List<CordTestSpec> items, 
            int propertyNo, string elongId = null)
        {
            if (null == items || items.Count <= 0)
                return null;

            var specs = items.FindAll((x) =>
            {
                bool ret = false;
                if (!string.IsNullOrEmpty(x.UnitId))
                {
                    // Has UnitId then OptionId required
                    if (!string.IsNullOrEmpty(x.OptionId))
                    {
                        // Has OptionId so return first OptionId = '1'
                        ret = x.PropertyNo == propertyNo && x.OptionId == "1";
                    }
                }
                else
                {
                    // No Unit id
                    ret = x.PropertyNo == propertyNo;
                }
                return ret;
            });

            return specs;
        }

        #endregion
    }

    #endregion
}
