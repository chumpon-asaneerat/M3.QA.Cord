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
    #region UniTestImportResult

    /// <summary>
    /// The UniTestImportResult class.
    /// </summary>
    public class UniTestImportResult<T>
    {
        public bool IsValid { get; set; }
        public string ErrMsg { get; set; }
        public T Value { get; set; }
    }

    #endregion
}
