using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HeliumHealthMonitor.Presentation.UI.Components
{
    public class CamelCaseSerializer : ISerializer
    {
        public string ContentType { get; set; }
        public CamelCaseSerializer()
        {
            ContentType = "application/json";
        }

        public string Serialize(object obj)
        {
            var camelCaseSetting = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string json = JsonConvert.SerializeObject(obj, camelCaseSetting);
            return json;
        }
    }
}
