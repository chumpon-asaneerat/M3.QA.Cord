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
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        private UniTestSPParserItem() : base() 
        {
            RCnt = 0;
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets SP No.</summary>
        public int SP { get; set; }
        /// <summary>Gets or sets Retest Count.</summary>
        public int RCnt { get; set; }

        #endregion

        #region Static Methods

        public static UniTestSPParserItem Parse(string value)
        {
            UniTestSPParserItem inst = null;
            if (string.IsNullOrWhiteSpace(value))
                return inst;

            string sSP = string.Empty;
            string sRCnt = string.Empty;
            if (value.Contains("+") || value.Contains("/"))
            {
                var lines = value.Split(new string[] { "+", "/" }, StringSplitOptions.RemoveEmptyEntries);
                if (null != lines && lines.Length > 0) 
                { 
                    sSP = lines[0]; // first element is SP No.
                    if (lines.Length > 1)
                    {
                        // Has retest
                        sRCnt = lines[1];
                    }
                    else
                    {
                        sRCnt = "0";
                    }
                }
            }
            else
            {
                sSP = value;
                sRCnt = "0";
            }
            int sp, rcnt;
            if (int.TryParse(sSP, out sp))
            {
                // parse success.
                inst = new UniTestSPParserItem();
                inst.SP = sp;
                if (int.TryParse(sRCnt, out rcnt))
                {
                    inst.RCnt = rcnt;
                }
                else inst.RCnt = 0;
            }

            return inst;
        }

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

            string[] sps = value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            int noOfSP = (null != sps) ? sps.Length : 0;

            inst = new UniTestSPParser();

            UniTestSPParserItem item;
            for (int i = 0; i < noOfSP; i++)
            {
                item = UniTestSPParserItem.Parse(sps[i]);
                if (null != item) 
                    inst.Items.Add(item); // append to items list.
            }

            return inst;
        }

        #endregion
    }

    #endregion
}
