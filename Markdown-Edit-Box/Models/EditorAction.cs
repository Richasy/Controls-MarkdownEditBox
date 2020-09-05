using MarkdownEditBox.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownEditBox.Models
{
    /// <summary>
    /// Editor Action Instructions
    /// </summary>
    public class EditorAction
    {
        /// <summary>
        /// Action identifier
        /// </summary>
        [JsonProperty("id")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EditorActionType Id { get; set; }

        /// <summary>
        /// Default action name
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// hot key
        /// </summary>
        [JsonProperty("attachKeys")]
        public string[] AttachKeys { get; set; }

        /// <summary>
        /// Group ID
        /// </summary>
        [JsonProperty("contextMenuGroupId")]
        public string ContextMenuGroupId { get; set; }

        /// <summary>
        /// Position in the menu
        /// </summary>
        [JsonProperty("contextMenuOrder")]
        public double ContextMenuOrder { get; set; }

        public override bool Equals(object obj)
        {
            return obj is EditorAction action &&
                   Id == action.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<EditorActionType>.Default.GetHashCode(Id);
        }
    }
}
