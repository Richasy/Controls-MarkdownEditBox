using System.Runtime.Serialization;

namespace MarkdownEditBox.Enums
{
    public enum EditorDisplayMode
    {
        /// <summary>
        /// Simultaneous display of editor and preview interface
        /// </summary>
        [EnumMember(Value = "split")]
        Split,

        /// <summary>
        /// Only Editor
        /// </summary>
        [EnumMember(Value = "editor")]
        Editor,

        /// <summary>
        /// Only Preview
        /// </summary>
        [EnumMember(Value = "preview")]
        Preview
    }
}
