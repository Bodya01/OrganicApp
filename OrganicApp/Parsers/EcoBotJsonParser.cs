using Newtonsoft.Json.Linq;

namespace OrganicApp.Parsers
{
    public static class EcoBotJsonParser
    {
        private const string HISTORY = "history";

        public static Dictionary<DateTime, float> ParseHistory(string json)
        {
            var result = new Dictionary<DateTime, float>();

            var jsonObject = JObject.Parse(json);

            var historyObject = (JObject)jsonObject[HISTORY]!;

            foreach (var property in historyObject.Properties())
            {
                var dateTime = DateTime.Parse(property.Name);
                var value = float.Parse(property.Value.ToString());

                result.Add(dateTime, value);
            }

            return result;
        }

        public static string GetDeviceId(string content)
        {
            var contentObject = JObject.Parse(content);

            var markerDataObject = (JObject)contentObject["marker_data"];

            var deviceId = markerDataObject["id"].ToString();

            return deviceId;
        }
    }
}