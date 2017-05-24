using System.Web.Script.Serialization;

namespace FlightsApp.Lib.Utils
{
    public class JsonSerializer
    {
        public static string ToJson(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static T FromJson<T>(string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
        }
    }
}
