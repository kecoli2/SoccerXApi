using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SoccerX.Common.Util
{
    public class FlexibleDictionaryConverter : Newtonsoft.Json.JsonConverter<Dictionary<string, string>>
    {

        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public override Dictionary<string, string>? ReadJson(JsonReader reader, Type objectType, Dictionary<string, string>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                return serializer.Deserialize<Dictionary<string, string>>(reader);
            }
            if (reader.TokenType == JsonToken.StartArray)
            {
                // [] gibi boş array gelirse boş dictionary dön
                JArray.Load(reader); // consume array
                return new Dictionary<string, string>();
            }

            return null;
        }

        public override void WriteJson(JsonWriter writer, Dictionary<string, string>? value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
        #endregion

        #region Private Method
        #endregion

    }
}
