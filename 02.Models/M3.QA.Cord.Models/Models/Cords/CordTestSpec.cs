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

        /// <summary>Gets or sets OptionId.</summary>
        public int OptionId
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

        /// <summary>Gets or sets Unit Report.</summary>
        public string UnitReport
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
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
        /// <returns></returns>
        public static CordTestSpec FindByPropertyNo(this List<CordTestSpec> items,  int propertyNo)
        {
            if (null == items || items.Count <= 0)
                return null;

            return items.Find((x) => x.PropertyNo == propertyNo);
        }

        #endregion
    }

    #endregion
}
