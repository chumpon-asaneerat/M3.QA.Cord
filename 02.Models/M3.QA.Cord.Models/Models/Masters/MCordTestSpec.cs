#region Using

using System;
using System.Collections.Generic;
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
    /// The MCordTestSpec class.
    /// </summary>
    public class MCordTestSpec
    {
        #region Public Properties

        /// <summary>Gets or sets Property No.</summary>
        public int PropertyNo { get; set; }
        /// <summary>Gets or sets Property Name. (Note: on db column is misspelling)</summary>
        public string PropertName { get; set; }
        /// <summary>Gets or sets Property Type.</summary>
        public string PropertyType { get; set; }

        #endregion

        #region Static Methods

        /// <summary>
        /// Gets.
        /// </summary>
        /// <returns></returns>
        public static NDbResult<List<MCordTestSpec>> Gets()
        {
            MethodBase med = MethodBase.GetCurrentMethod();

            NDbResult<List<MCordTestSpec>> ret = new NDbResult<List<MCordTestSpec>>();

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

            string product = "Cord";
            var p = new DynamicParameters();

            p.Add("@product", product);

            try
            {
                var items = cnn.Query<MCordTestSpec>("M_GetPropertyListByProduct", p,
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
}
