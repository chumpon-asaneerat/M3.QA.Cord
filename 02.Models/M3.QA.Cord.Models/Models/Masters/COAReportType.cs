#region Using

using System.Collections.Generic;

#endregion

namespace M3.QA.Models
{
    #region COAReportType

    /// <summary>
    /// The COA Report Type class.
    /// </summary>
    public class COAReportType
    {
        #region Public Properties

        /// <summary>Gets or sets CoaNo.</summary>
        public int CoaNo { get; set; }
        /// <summary>Gets or sets FMQC.</summary>
        public string FMQC { get; set; }
        /// <summary>Gets Text.</summary>
        public string Text 
        {
            get { return string.Format("{0} ({1})", CoaNo, FMQC); }
            set { }
        }

        #endregion

        #region Static Methods

        public static List<COAReportType> Gets()
        {
            var ret = new List<COAReportType>()
            {
                new COAReportType() { CoaNo = 1, FMQC = "27-02" },
                new COAReportType() { CoaNo = 2, FMQC = "27-02" },
                new COAReportType() { CoaNo = 3, FMQC = "31-02" },
                new COAReportType() { CoaNo = 4, FMQC = "29-02" }
            };

            return ret;
        }

        #endregion
    }

    #endregion
}
