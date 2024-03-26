#region Using

using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Media;

using NLib;
using NLib.Models;

using Dapper;
using Newtonsoft.Json;

#endregion

namespace M3.QA.Models
{
    public class DIPMC
    {
        #region Public Properties

        public string MCCode { get; set; }
        public string MCName { get; set; }

        #endregion

        #region Static Methods

        public static List<DIPMC> Gets()
        {
            return new List<DIPMC>() 
            { 
                new DIPMC() { MCCode = "S-8-1", MCName = "S-8-1" },
                new DIPMC() { MCCode = "S-8-2", MCName = "S-8-2" }
            };
        }

        #endregion
    }
}
