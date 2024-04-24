#region Using

using System.Collections.Generic;

#endregion

namespace M3.QA.Models
{
    #region YarnType

    /// <summary>
    /// The Yarn Type class.
    /// </summary>
    public class YarnType
    {
        #region Public Properties

        /// <summary>Gets or set Id.</summary>
        public string Id { get; set; }
        /// <summary>Gets or set Text.</summary>
        public string Text { get; set; }

        #endregion

        #region Static Methods

        public static List<YarnType> Gets()
        {
            var ret = new List<YarnType>()
            {
                new YarnType() { Id = "Polyester", Text = "Polyester" },
                new YarnType() { Id = "Nylon", Text = "Nylon" }
            };

            return ret;
        }

        #endregion
    }

    #endregion
}
