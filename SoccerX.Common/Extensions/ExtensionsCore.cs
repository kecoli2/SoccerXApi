using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Common.Extensions
{
    public static class ExtensionsCore
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public static TValue? GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            if (type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
            {
                return valueSelector(att);
            }
            return default;
        }
        #endregion

        #region Private Method
        #endregion
    }
}
