using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SoccerX.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with JSON serialization and deserialization.
    /// </summary>
    public static class JsonExtensions
    {
        private static readonly JsonSerializerOptions DefaultOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        private static readonly JsonSerializerOptions PrettyPrintOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>
        /// Converts an object to a JSON string.
        /// </summary>
        /// <typeparam name="T">Type of the object.</typeparam>
        /// <param name="obj">The object to serialize.</param>
        /// <param name="indented">If true, formats the JSON with indentation.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public static string ToJson<T>(this T obj, bool indented = false)
        {
            var options = indented ? PrettyPrintOptions : DefaultOptions;
            return JsonSerializer.Serialize(obj, options);
        }

        /// <summary>
        /// Converts a JSON string back to an object of type T.
        /// </summary>
        /// <typeparam name="T">Target object type.</typeparam>
        /// <param name="json">JSON string to deserialize.</param>
        /// <returns>The deserialized object of type T.</returns>
        public static T? FromJson<T>(this string json)
        {
            return JsonSerializer.Deserialize<T>(json, DefaultOptions);
        }

        /// <summary>
        /// Prettifies (formats) a JSON string for better readability.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        /// <returns>A formatted JSON string.</returns>
        public static string PrettifyJson(this string json)
        {
            using var jsonDoc = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(jsonDoc, PrettyPrintOptions);
        }

        /// <summary>
        /// Minifies a JSON string by removing unnecessary spaces and new lines.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        /// <returns>A minified JSON string.</returns>
        public static string MinifyJson(this string json)
        {
            using var jsonDoc = JsonDocument.Parse(json);
            return JsonSerializer.Serialize(jsonDoc, DefaultOptions);
        }

        /// <summary>
        /// Converts a JSON string to a UTF-8 byte array.
        /// </summary>
        /// <param name="json">The JSON string.</param>
        /// <returns>A UTF-8 encoded byte array.</returns>
        public static byte[] ToJsonBytes(this string json)
        {
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// Converts a UTF-8 byte array to a JSON string.
        /// </summary>
        /// <param name="bytes">The byte array containing JSON data.</param>
        /// <returns>A JSON string.</returns>
        public static string FromJsonBytes(this byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
