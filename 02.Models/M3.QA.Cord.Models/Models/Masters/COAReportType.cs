#region Using

using System.Collections.Generic;

#endregion

namespace M3.QA.Models
{
    #region COAReportType

    /// <summary>
    /// The Coa Report Type class.
    /// </summary>
    public class CoaReportType
    {
        #region Public Properties

        /// <summary>Gets or sets CoaNo.</summary>
        public int CoaNo { get; set; }
        /// <summary>Gets or sets FMQC.</summary>
        public string FMQC { get; set; }
        /// <summary>Gets Coa Report Code.</summary>
        public string CoaReportCode
        {
            get { return string.Format("{0} ({1})", CoaNo, FMQC); }
            set { }
        }

        #endregion

        #region Static Methods

        public static List<CoaReportType> Gets()
        {
            var ret = new List<CoaReportType>()
            {
                new CoaReportType() { CoaNo = 1, FMQC = "27-02" },
                new CoaReportType() { CoaNo = 2, FMQC = "27-02" },
                new CoaReportType() { CoaNo = 3, FMQC = "31-02" },
                new CoaReportType() { CoaNo = 4, FMQC = "29-02" }
            };

            return ret;
        }

        #endregion
    }

    #endregion
}
