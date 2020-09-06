using MarkdownEditBox.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarkdownEditBox.Models
{
    internal class NotifyPackage
    {
        [JsonProperty("key")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WebScriptNotifyType Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    internal class ErrorPackage
    {
        [JsonProperty("errorMessage")]
        public string ErrorMessage { get; set; }

        [JsonProperty("action")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WebScriptErrorType Action { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }
    }
}
