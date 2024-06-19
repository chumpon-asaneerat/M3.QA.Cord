#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region CordCodeDetail

    /// <summary>
    /// The Cord Code Detail class.
    /// </summary>
    public class CordCodeDetail : NInpc
    {
        #region Public Method
        /*
        /// <summary>
        /// Clone.
        /// </summary>
        /// <returns></returns>
        public CordCodeDetail Clone()
        {
            var inst = new CordCodeDetail();

            inst.MasterId = MasterId;
            inst.Customer = Customer;
            inst.ItemCode = ItemCode;
            inst.UserName = UserName;
            inst.CoaNo = CoaNo;
            inst.FMQC = FMQC;
            inst.ProductType = ProductType;
            inst.ProductName = ProductName;
            inst.YarnType = YarnType;
            inst.YarnCode = YarnCode;
            inst.ELongLoadN = ELongLoadN;
            inst.NoTestCH = NoTestCH;

            //inst.NewItemCode = NewItemCode;
            //inst.NewCustomer = NewCustomer;

            return inst;
        }
        */
        #endregion

        #region Public Properties

        /// <summary>Gets or set MasterId.</summary>
        public int? MasterId 
        {
            get { return Get<int?>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set Customer.</summary>
        public string Customer 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set ItemCode.</summary>
        public string ItemCode 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set UserName.</summary>
        public string UserName 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or set CoaNo.</summary>
        public int CoaNo 
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set FMQC.</summary>
        public string FMQC 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets Coa Report Code.</summary>
        public string CoaReportCode
        {
            get { return string.Format("{0} ({1})", CoaNo, FMQC); }
            set { }
        }

        /// <summary>Gets or set ProductType.</summary>
        public string ProductType 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set ProductName.</summary>
        public string ProductName 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set YarnType.</summary>
        public string YarnType 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set YarnCode.</summary>
        public string YarnCode 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }

        /// <summary>Gets or set ELongLoadN.</summary>
        public string ELongLoadN 
        {
            get { return Get<string>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set NoTestCH.</summary>
        public int NoTestCH 
        {
            get { return Get<int>(); }
            set { Set(value, () => { }); }
        }
        /// <summary>Gets or set ValidDays.</summary>
        public int? ValidDays
        {
            get { return Get<int?>(); }
            set { Set(value, () => { }); }
        }



        /// <summary>Check is new record (MasterId is null or less than zero).</summary>
        public bool IsNew
        {
            get { return MasterId.HasValue && MasterId.Value > 0 ? false : true; }
            set { }
        }

        /// <summary>Gets Visibility for input that allow only New item.</summary>
        public Visibility NewVisible
        {
            get { return (IsNew) ? Visibility.Visible : Visibility.Collapsed; }
            set { }
        }
        /// <summary>Gets Visibility for input that allow only Exist item.</summary>
        public Visibility ExistVisible
        {
            get { return (!IsNew) ? Visibility.Visible : Visibility.Collapsed; }
            set { }
        }

        /// <summary>Gets or sets New Item Code.</summary>
        public string NewItemCode
        {
            get { return Get<string>(); }
            set
            {
                if (!IsNew)
                    return; // not allow if already exist.
                Set(value, () =>
                {
                    // when set new customer the old Customer should be reset
                    ItemCode = null;
                });
            }
        }
        /// <summary>Gets or sets New customer.</summary>
        public string NewCustomer 
        {
            get { return Get<string>();  }
            set 
            {
                if (!IsNew) 
                    return; // not allow if already exist.
                Set(value, () => 
                {
                    // when set new customer the old Customer should be reset
                    Customer = null;
                });
            }
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets
        /// </summary>
        /// <param name="cordcode"></param>
        /// <returns></returns>
        public static NDbResult<List<CordCodeDetail>> Gets(string cordcode)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordCodeDetail>> rets = new NDbResult<List<CordCodeDetail>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@cordcode", cordcode);

            try
            {
                var items = cnn.Query<CordCodeDetail>("M_GetCordCodeDetail", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<CordCodeDetail>();
            }

            return rets;
        }
        /// <summary>
        /// Search
        /// </summary>
        /// <param name="cordcode"></param>
        /// <param name="customer"></param>
        /// <param name="producttype"></param>
        /// <returns></returns>
        public static NDbResult<List<CordCodeDetail>> Search(string cordcode, string customer, 
            string producttype)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<CordCodeDetail>> rets = new NDbResult<List<CordCodeDetail>>();

            IDbConnection cnn = DbServer.Instance.Db;
            if (null == cnn || !DbServer.Instance.Connected)
            {
                string msg = "Connection is null or cannot connect to database server.";
                med.Err(msg);
                // Set error number/message
                rets.ErrNum = 8000;
                rets.ErrMsg = msg;

                return rets;
            }

            var p = new DynamicParameters();
            p.Add("@cordcode", (string.IsNullOrEmpty(cordcode)) ? null : cordcode);
            p.Add("@customer", (string.IsNullOrEmpty(customer)) ? null : customer);
            p.Add("@producttype", (string.IsNullOrEmpty(producttype)) ? null : producttype);

            try
            {
                var items = cnn.Query<CordCodeDetail>("M_SearchCordCodeDetail", p,
                    commandType: CommandType.StoredProcedure);
                var data = (null != items) ? items.ToList() : null;
                rets.Success(data);
            }
            catch (Exception ex)
            {
                med.Err(ex);
                // Set error number/message
                rets.ErrNum = 9999;
                rets.ErrMsg = ex.Message;
            }

            if (null == rets.data)
            {
                // create empty list.
                rets.data = new List<CordCodeDetail>();
            }

            return rets;
        }
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="value">The CordCodeDetail item to save.</param>
        /// <returns></returns>
        public static NDbResult<CordCodeDetail> Save(CordCodeDetail value, Models.UserInfo user)
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<CordCodeDetail> ret = new NDbResult<CordCodeDetail>();

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

            p.Add("@cordcode", (value.IsNew && !string.IsNullOrWhiteSpace(value.NewItemCode)) ? 
                value.NewItemCode : value.ItemCode);
            p.Add("@customer", (value.IsNew && !string.IsNullOrWhiteSpace(value.NewCustomer)) ? 
                value.NewCustomer : value.Customer);
            p.Add("@Username", value.UserName);
            p.Add("@coano", value.CoaNo);
            p.Add("@fmqc", value.FMQC);
            p.Add("@producttype", value.ProductType);
            p.Add("@productname", value.ProductName);
            p.Add("@yarntype", value.YarnType);
            p.Add("@elonglondn", value.ELongLoadN);
            p.Add("@notestch", value.NoTestCH);
            p.Add("@yarncode", value.YarnCode);

            p.Add("@operator", (null != user) ? user.FullName : null);

            p.Add("@errNum", dbType: DbType.Int32, direction: ParameterDirection.Output);
            p.Add("@errMsg", dbType: DbType.String, direction: ParameterDirection.Output, size: -1);

            try
            {
                cnn.Execute("M_SaveCordCode", p, commandType: CommandType.StoredProcedure);
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

        #endregion
    }

    #endregion
}
