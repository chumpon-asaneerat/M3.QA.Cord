#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows;
using Dapper;

using NLib;
using NLib.Models;

#endregion

namespace M3.QA.Models
{
    #region Utils class

    public static class Utils
    {
        #region M_GetPropertyTotalNByItem

        public class M_GetPropertyTotalNByItem
        {
            #region Public Properties

            public int MasterId { get; set; }
            public int PropertyNo { get; set; }
            public int NoSample { get; set; }

            #endregion

            #region Static Methods

            public static NDbResult<M_GetPropertyTotalNByItem> GetByItem(int masterId, int propertyNo)
            {
                MethodBase med = MethodBase.GetCurrentMethod();

                NDbResult<M_GetPropertyTotalNByItem> ret = new NDbResult<M_GetPropertyTotalNByItem>();

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

                p.Add("@MasterId", masterId);
                p.Add("@PropertyNo", propertyNo);

                try
                {
                    var items = cnn.Query<M_GetPropertyTotalNByItem>("M_GetPropertyTotalNByItem", p, commandType: CommandType.StoredProcedure);
                    var data = (null != items) ? items.ToList().FirstOrDefault() : null;

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
    }

    #endregion
}
