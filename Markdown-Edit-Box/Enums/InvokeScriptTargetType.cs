using System.Runtime.Serialization;

namespace MarkdownEditBox.Enums
{
    public enum InvokeScriptTargetType
    {
        /// <summary>
        /// window.app
        /// </summary>
        [EnumMember(Value = "window.app")]
        App,

        /// <summary>
        /// window.Editor
        /// </summary>
        [EnumMember(Value = "window.Editor")]
        Editor,

        /// <summary>
        /// window.Preview
        /// </summary>
        [EnumMember(Value = "window.Preview")]
        Preview,

        /// <summary>
        /// window.mdEditor
        /// </summary>
        [EnumMember(Value = "window.mdEditor")]
        Monaco
    }
}
