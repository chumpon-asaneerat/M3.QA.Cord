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
    #region Helper classes

    #region CordSpecUnit

    /// <summary>
    /// The CordSpecUnit class. Use for PropertyNo 3,7,8,10
    /// </summary>
    public class CordSpecUnit
    {
        #region Override

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return UnitId.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CordSpecUnit)) return false;
            return (obj as CordSpecUnit).UnitId == UnitId;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets Unit Id.</summary>
        public string UnitId { get; set; }
        /// <summary>Gets or sets Unit Desc.</summary>
        public string UnitDesc { get; set; }

        #endregion
    }

    #endregion

    #region CordSpecType

    /// <summary>
    /// The CordSpecType class. Use for Spec Type
    /// </summary>
    public class CordSpecType
    {
        #region Override

        /// <summary>
        /// GetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return SpecId.GetHashCode();
        }
        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CordSpecType)) return false;
            return (obj as CordSpecType).SpecId == SpecId;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets Spec Id.</summary>
        public int SpecId { get; set; }
        /// <summary>Gets or sets Spec Desc.</summary>
        public string SpecDesc { get; set; }

        #endregion
    }

    #endregion

    #endregion

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
            // Init Spec Types.
            SpecTypes = new List<CordSpecType>()
            {
                new CordSpecType() { SpecId = 0, SpecDesc = "No Check" },
                new CordSpecType() { SpecId = 1, SpecDesc = "Plus/Minus" },
                new CordSpecType() { SpecId = 2, SpecDesc = "Min/Max" },
                new CordSpecType() { SpecId = 3, SpecDesc = "Range" }
            };
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

        private string GetRangeSpec()
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
                ret += "Range Specification is not set.";
            }

            return ret;
        }

        private void ApplySpecId()
        {
            if (!IsSettingMode)
                return; // not in setting mode.

            if (null != SelectionSpecType && SelectionSpecType.SpecId == SpecId)
                return; // Same SpecId so ignore

            // in setting page required sync items.
            if (null == SpecTypes || SpecTypes.Count <= 0)
                return;
            int idx = SpecTypes.FindIndex(x => { return x.SpecId == SpecId; });
            SelectionSpecType = (idx != -1) ? SpecTypes[idx] : null;

            // Update OptionId
            if (null != SelectionSpecType)
            {
                OptionId = SelectionSpecType.SpecId != 0 ? "1" : null;
            }
        }

        private void ApplySelectionSpecType()
        {
            if (!IsSettingMode)
                return; // not in setting mode.

            if (null != SelectionSpecType)
            {
                if (SpecId != SelectionSpecType.SpecId)
                {
                    SpecId = SelectionSpecType.SpecId;
                    SpecDesc = SelectionSpecType.SpecDesc;
                }
            }
            else
            {
                if (SpecId != 0)
                {
                    SpecId = 0;
                    SpecDesc = "No Check";
                }
            }
            // Raise events.
            Raise(() => SelectionSpecType);
        }

        private void ApplyUnitId()
        {
            if (!IsSettingMode)
                return; // not in setting mode.

            if (null != SelectionUnit && SelectionUnit.UnitId == UnitId)
                return; // Same SpecId so ignore

            // in setting page required sync items.
            if (null == Units || Units.Count <= 0)
                return;
            int idx = Units.FindIndex(x => { return x.UnitId == UnitId; });
            SelectionUnit = (idx != -1) ? Units[idx] : null;
        }

        private void ApplySelectionUnit()
        {
            if (!IsSettingMode)
                return; // not in setting mode.

            if (null != SelectionUnit)
            {
                if (UnitId != SelectionUnit.UnitId)
                {
                    UnitId = SelectionUnit.UnitId;
                    UnitDesc = SelectionUnit.UnitDesc;
                }
            }
            else
            {
                if (UnitId != null)
                {
                    UnitId = null;
                    UnitDesc = null;
                }
            }
            // Raise events.
            Raise(() => SelectionUnit);
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
                case 3: // Range
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

        #region For Test Entry UI

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
        /// <summary>Gets or sets Property Name.</summary>
        public string PropertyName
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or sets SpecId.</summary>
        public int SpecId
        {
            get { return Get<int>(); }
            set 
            { 
                Set(value, () => 
                {
                    ApplySpecId();
                    // Raise events
                    Raise(() => SpecInfo);
                }); 
            }
        }
        /// <summary>Gets Spec description.</summary>
        public string SpecDesc
        {
            get { return (null != SelectionSpecType) ? SelectionSpecType.SpecDesc : null; }
            set { }
        }

        /// <summary>Gets or sets UnitId.</summary>
        public string UnitId
        {
            get { return Get<string>(); }
            set 
            { 
                Set(value, () => 
                {
                    ApplyUnitId();
                }); 
            }
        }
        /// <summary>Gets Unit description.</summary>
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
        /// <summary>Gets Option description.</summary>
        public string OptionDesc
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or sets VCenter.</summary>
        public decimal? VCenter
        {
            get { return Get<decimal?>(); }
            set 
            { 
                Set(value, () => 
                {
                    // Raise events
                    Raise(() => SpecInfo);
                }); 
            }
        }
        /// <summary>Gets or sets VMin.</summary>
        public decimal? VMin
        {
            get { return Get<decimal?>(); }
            set 
            { 
                Set(value, () => 
                {
                    // Raise events
                    Raise(() => SpecInfo);
                }); 
            }
        }
        /// <summary>Gets or sets VMax.</summary>
        public decimal? VMax
        {
            get { return Get<decimal?>(); }
            set 
            { 
                Set(value, () => 
                {
                    // Raise events
                    Raise(() => SpecInfo);
                }); 
            }
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
                else if (SpecId == 3) ret = GetRangeSpec();
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

        #region For Setting UI

        /// <summary>Gets or sets is setting mode.</summary>
        public bool IsSettingMode { get; set; } = false;

        /// <summary>Gets or sets is property is enable for test.</summary>
        public bool EnableProperty
        {
            get { return Get<bool>(); }
            set 
            { 
                Set(value, () => 
                {
                    // Raise events.
                    Raise(() => PropertyCaptionColor);
                    Raise(() => PropertyPanelBrush);
                }); 
            }
        }

        /// <summary>Gets Property Caption Color.</summary>
        public Color PropertyCaptionColor
        {
            get { return (EnableProperty) ? Colors.ForestGreen : Colors.DimGray; }
            set { }
        }
        /// <summary>Gets Property Panel Entry Color.</summary>
        public SolidColorBrush PropertyPanelBrush
        {
            get { return (EnableProperty) ? ModelConsts.TransparentColor : ModelConsts.WhiteSmokeColor; }
        }

        /// <summary>Gets or sets No of Sample.</summary>
        public int NoSample
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets SpecTypes.</summary>
        public List<CordSpecType> SpecTypes { get; private set; }

        private CordSpecType _SelectionSpec;
        /// <summary>Gets Selection SpecType.</summary>
        public CordSpecType SelectionSpecType
        {
            get { return _SelectionSpec; }
            set
            {
                if (_SelectionSpec != value)
                {
                    _SelectionSpec = value;
                    ApplySelectionSpecType();
                }
            }
        }


        /// <summary>Gets Unit Visibility.</summary>
        public Visibility UnitVisibility
        {
            get 
            { 
                return (PropertyNo == 3 || PropertyNo == 7 || PropertyNo == 8 || PropertyNo == 10) ? 
                    Visibility.Visible : Visibility.Collapsed; 
            }
            set { }
        }

        private List<CordSpecUnit> _units;
        public List<CordSpecUnit> Units
        {
            get 
            {
                if (PropertyNo == 7 || PropertyNo == 8)
                {
                    if (null == _units)
                    {
                        _units = new List<CordSpecUnit>()
                        {
                            new CordSpecUnit() { UnitId = "t/10cm", UnitDesc = "t/10cm" },
                            new CordSpecUnit() { UnitId = "t/m", UnitDesc = "t/m" }
                        };
                    }
                }
                else if (PropertyNo == 10)
                {
                    if (null == _units)
                    {
                        _units = new List<CordSpecUnit>()
                        {
                            new CordSpecUnit() { UnitId = "D", UnitDesc = "Denier (D)" },
                            new CordSpecUnit() { UnitId = "dtex", UnitDesc = "dtex" }
                        };
                    }
                }
                return _units;
            }
            set { }
        }

        private CordSpecUnit _SelectionUnit;
        public CordSpecUnit SelectionUnit
        {
            get { return _SelectionUnit; }
            set
            {
                if (_SelectionUnit != value)
                {
                    _SelectionUnit = value;
                    ApplySelectionUnit();
                }
            }
        }

        public Visibility ComboBoxUnitVisible
        {
            get
            {
                return (PropertyNo == 7 || PropertyNo == 8 || PropertyNo == 10) ?
                    Visibility.Visible : Visibility.Collapsed;
            }
            set { }
        }

        public Visibility TextBoxUnitVisible
        {
            get
            {
                return (PropertyNo == 3) ?
                    Visibility.Visible : Visibility.Collapsed;
            }
            set { }
        }

        #endregion

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

        /// <summary>
        /// Gets Setting specification.
        /// </summary>
        /// <param name="cordCode"></param>
        /// <returns></returns>
        public static List<CordTestSpec> GetSettings(CordCode cordCode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            var ret = new List<CordTestSpec>();

            // Parse all specification
            var specs = MCordTestSpec.Gets().Value();
            if (null != specs) 
            {
                foreach (var item in specs)
                {
                    var inst = new CordTestSpec()
                    {
                        PropertyNo = item.PropertyNo,
                        PropertyName = item.PropertName,
                        // Setting Mode
                        IsSettingMode = true,
                        // Assign default spect type = 0
                        SpecId = 0
                    };
                    // Add to list.
                    ret.Add(inst);
                }
            }

            try
            {
                var exists = (null != ret) ? GetsByMasterId(cordCode.MasterId).Value() : null;
                if (null != exists)
                {
                    int idx;
                    foreach (var item in ret)
                    {
                        // init required property
                        item.ItemCode = cordCode.ItemCode;
                        item.ProductName = cordCode.ProductName;
                        item.MasterId = cordCode.MasterId;

                        idx = exists.FindIndex(x =>  x.PropertyNo == item.PropertyNo);
                        if (idx != -1 && null != exists[idx])
                        {
                            // Copy from exist setting.
                            var exist = exists[idx];
                            item.EnableProperty = true; // set property is enable
                            item.NoSample = exist.NoSample;
                            item.SpecId = exist.SpecId;
                            item.SpecDesc = exist.SpecDesc;
                            item.UnitId = exist.UnitId;
                            item.UnitDesc = exist.UnitDesc;
                            item.OptionId = exist.OptionId;
                            item.OptionDesc = exist.OptionDesc;
                            item.VCenter = exist.VCenter;
                            item.VMin = exist.VMin;
                            item.VMax = exist.VMax;
                            item.UnitReport = exist.UnitReport;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                med.Err(ex);
            }

            return ret;
        }
        /// <summary>
        /// Save Setting
        /// </summary>
        /// <param name="value">The CordTestSpec item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordTestSpec> SaveSetting(CordTestSpec value, Models.UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTestSpec> ret = new NDbResult<CordTestSpec>();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

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

            p.Add("@masterid", value.MasterId);
            p.Add("@propertyno", value.PropertyNo);
            p.Add("@specid", value.SpecId);
            p.Add("@specdesc", value.SpecDesc);
            p.Add("@unitid", value.UnitId);
            p.Add("@optionid", value.OptionId);

            p.Add("@vcenter", value.VCenter);
            p.Add("@vmin", value.VMin);
            p.Add("@vmax", value.VMax);

            p.Add("@unitreport", value.UnitReport);
            p.Add("@nosample", value.NoSample);

            p.Add("@operator", (null != user) ? user.FullName : null);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("M_SaveTestSpec", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
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
        /// Delete Setting
        /// </summary>
        /// <param name="value">The CordTestSpec item to delete.</param>
        /// <returns></returns>
        public static NDbResult<CordTestSpec> DeleteSetting(CordTestSpec value, Models.UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordTestSpec> ret = new NDbResult<CordTestSpec>();

            if (null == value)
            {
                ret.ParameterIsNull();
                return ret;
            }

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

            p.Add("@masterid", value.MasterId);
            p.Add("@propertyno", value.PropertyNo);
            p.Add("@unitid", value.UnitId);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("M_DeleteSpecByMasterid", p, commandType: CommandType.StoredProcedure);
                ret.Success(value);
                // Set error number/message
                ret.ErrNum = p.Get<int>("@errNum");
                ret.ErrMsg = p.Get<string>("@errMsg");
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
        /// Save Settings
        /// </summary>
        /// <param name="values"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static NDbResult SaveSettings(List<CordTestSpec> values, Models.UserInfo user)
        {
            //MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult ret = new NDbResult();

            if (null == values || values.Count <= 0)
            {
                ret.ParameterIsNull();
                return ret;
            }

            foreach (var item in values)
            {
                NDbResult ritem;

                if (item.EnableProperty)
                    ritem = SaveSetting(item, user);
                else 
                    ritem = DeleteSetting(item, user);

                if (null == ritem || ritem.HasError)
                {
                    var ex = (null != ritem) ? new Exception("Item is null.") : new Exception(ritem.ErrMsg);
                    ret.Error(ex);
                    break;
                }
            }

            ret.Success();

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
            int propertyNo)
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
