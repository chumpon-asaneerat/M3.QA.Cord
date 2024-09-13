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
            
        }

        #endregion

        #region Public Properties

        /// <summary>Gets or sets SP No.</summary>
        public int SP { get; set; }
        /// <summary>Gets or sets Has Retest N1.</summary>
        public bool RetestN1 { get; set; }
        /// <summary>Gets or sets Has Retest N2.</summary>
        public bool RetestN2 { get; set; }
        /// <summary>Gets or sets Has Retest N3.</summary>
        public bool RetestN3 { get; set; }

        #endregion

        #region Static Methods

        private static string Between(string source, string startString, string endString)
        {
            string ret = string.Empty;
            if (!string.IsNullOrWhiteSpace(source))
            {
                int Pos1 = source.IndexOf(startString) + startString.Length;
                int Pos2 = source.IndexOf(endString);
                ret = source.Substring(Pos1, Pos2 - Pos1);
            }
            return ret;
        }

        public static UniTestSPParserItem Parse(string value)
        {
            UniTestSPParserItem inst = null;
            if (string.IsNullOrWhiteSpace(value))
                return inst;

            string sSP = string.Empty;
            List<string> retestNs = new List<string>();
            if (value.Contains("+"))
            {
                var lines = value.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);
                if (null != lines && lines.Length > 0) 
                { 
                    sSP = lines[0]; // first element is SP No.

                    if (lines.Length > 1)
                    {
                        // Has retest
                        string retestValues = Between(lines[1], "(", ")");

                        var retests = retestValues.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        if (null != retests && retests.Length > 0)
                        {
                            retestNs.AddRange(retests);
                        }
                    }
                }
            }
            else
            {
                sSP = value;
            }

            int sp, rN;
            if (int.TryParse(sSP, out sp))
            {
                // parse success.
                inst = new UniTestSPParserItem();
                inst.SP = sp;

                inst.RetestN1 = false;
                inst.RetestN2 = false;
                inst.RetestN3 = false;

                foreach (var retestN in retestNs)
                {
                    if (int.TryParse(retestN, out rN))
                    {
                        if (rN == 1) inst.RetestN1 = true;
                        else if (rN == 2) inst.RetestN2 = true;
                        else if (rN == 3) inst.RetestN3 = true;
                    }
                }
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
