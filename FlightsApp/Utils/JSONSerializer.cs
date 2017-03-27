using System.Web.Script.Serialization;

namespace FlightsApp
{
    public class JSONSerializer
    {
        public static string ToJSON(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        public static T FromJSON<T>(string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
        }
    }
}
