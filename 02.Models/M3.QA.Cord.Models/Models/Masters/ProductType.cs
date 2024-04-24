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
    public class ProductType
    {
        #region Public Properties

        public string Id { get; set; }
        public string Text { get; set; }

        #endregion

        #region Static Methods

        public static List<ProductType> Gets()
        {
            var ret = new List<ProductType>() 
            { 
                new ProductType() { Id = "Twist", Text = "Twist" },
                new ProductType() { Id = "Dip", Text = "Dip" }
            };

            return ret;
        }

        #endregion
    }
}
