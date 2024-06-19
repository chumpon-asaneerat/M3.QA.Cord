#region Using

using System.Collections.Generic;

#endregion

namespace M3.QA.Models
{
    #region ProductType

    /// <summary>
    /// The Product Type class.
    /// </summary>
    public class ProductType
    {
        #region Public Properties

        /// <summary>Gets or set Id.</summary>
        public string Id { get; set; }
        /// <summary>Gets or set Text.</summary>
        public string Text { get; set; }

        #endregion

        #region Static Methods

        public static List<ProductType> Gets()
        {
            var ret = new List<ProductType>() 
            { 
                new ProductType() { Id = "Twist", Text = "Twist" },
                new ProductType() { Id = "Dip", Text = "Dip" },
                new ProductType() { Id = "Solution", Text = "Solution" }
            };

            return ret;
        }

        #endregion
    }

    #endregion
}
