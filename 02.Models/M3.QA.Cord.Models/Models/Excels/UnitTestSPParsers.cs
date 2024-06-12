#region Using

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using NLib;
using NLib.Reflection;
using OfficeOpenXml;

#endregion

namespace M3.QA.Models
{
    #region UniTestSPParserItem

    /// <summary>
    /// The UniTestSPParserItem class.
    /// </summary>
    public class UniTestSPParserItem
    {
        #region Public Properties

        /// <summary>Gets or sets SP No.</summary>
        public int SP { get; set; }
        /// <summary>Gets or sets Retest Count.</summary>
        public int RCnt { get; set; }

        #endregion
    }

    #endregion

    #region UniTestSPParser

    /// <summary>
    /// The UniTestSPParser class.
    /// </summary>
    public class UniTestSPParser
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        private UniTestSPParser() : base()
        {
            Items = new List<UniTestSPParserItem>();
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets Parser Items.</summary>
        public List<UniTestSPParserItem> Items { get; private set; }

        #endregion

        #region Static Methods

        public static UniTestSPParser Parse(string value)
        {
            UniTestSPParser inst = null;
            if (string.IsNullOrWhiteSpace(value)) 
                return inst;

            inst = new UniTestSPParser();

            return inst;
        }

        #endregion
    }

    #endregion
}
