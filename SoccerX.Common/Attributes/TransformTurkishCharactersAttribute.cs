using System.Reflection;

namespace SoccerX.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class TransformTurkishCharactersAttribute: Attribute
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public static void ApplyTransformations(object target)
        {
            if (target == null) return;

            var properties = target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                if (!prop.CanRead || !prop.CanWrite) continue;

                var attr = prop.GetCustomAttribute<TransformTurkishCharactersAttribute>();
                if (attr == null) continue;

                var value = prop.GetValue(target) as string;
                if (value == null) continue;

                var transformed = ReplaceTurkishCharacters(value);
                prop.SetValue(target, transformed);
            }
        }
        #endregion

        #region Private Method
        private static string ReplaceTurkishCharacters(string input)
        {
            return input
                .Replace("ç", "c").Replace("Ç", "C")
                .Replace("ğ", "g").Replace("Ğ", "G")
                .Replace("ı", "i").Replace("İ", "I")
                .Replace("ö", "o").Replace("Ö", "O")
                .Replace("ş", "s").Replace("Ş", "S")
                .Replace("ü", "u").Replace("Ü", "U");
        }
        #endregion

    }
}
