using Newtonsoft.Json;
using System.Text;
using System.Xml;

namespace SoccerX.Common.Extensions
{
    public static class JsonExtensionsNewtonsoft
    {

        #region Public Method
        public static string ToJsonNewton<T>(this T obj, bool indented = false)
        {
            var formatting = indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None;
            return JsonConvert.SerializeObject(obj, formatting);
        }

        public static T? FromJsonNewton<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string PrettifyJsonNewton(this string json)
        {
            var parsed = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsed, Newtonsoft.Json.Formatting.Indented);
        }

        public static string MinifyJsonNewton(this string json)
        {
            var parsed = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(parsed, Newtonsoft.Json.Formatting.None);
        }

        public static byte[] ToJsonBytesNewton(this string json)
        {
            return Encoding.UTF8.GetBytes(json);
        }

        public static string FromJsonBytesNewton(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        #endregion

        #region Private Method
        #endregion
    }
}
