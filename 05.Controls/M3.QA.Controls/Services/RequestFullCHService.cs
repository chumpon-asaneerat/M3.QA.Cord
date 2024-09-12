#region Using

using System;
using System.Collections.Generic;
using NLib;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

#endregion

namespace M3.QA
{
    public class RequestFullCHService
    {
        public static bool Request(string lotNo, int spNo)
        {
            bool success = false;

            var win = M3QAApp.Windows.RequestFullCH;
            win.Setup(lotNo, spNo);
            if (win.ShowDialog() == true)
            {
                string usr = (null != M3QAApp.Current.User) ? M3QAApp.Current.User.FullName : null;
                string remark = win.Remark;

                var ret = Models.Utils.M_ReceiveFullSample.Save(
                    lotNo, null, usr, DateTime.Now, spNo, spNo, null, remark);
                
                success = null != ret && ret.Ok;
                if (success)
                    M3QAApp.Windows.SaveSuccess();
                else
                    M3QAApp.Windows.SaveFailed();

                if (success) OnRequestFullCH.Call(null, EventArgs.Empty);
            }

            return success;
        }

        public static event EventHandler OnRequestFullCH;
    }
}
