using MarkdownEditBox.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MarkdownEditBox.Models
{
    internal class NotifyPackage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public WebScriptNotifyType Key { get; set; }
        public string Value { get; set; }
    }

    internal class ErrorPackage
    {
        public string ErrorMessage { get; set; }
        
        [JsonConverter(typeof(StringEnumConverter))]
        public WebScriptErrorType Action { get; set; }
        public string Sign { get; set; }
    }
}
