using System.Runtime.Serialization;

namespace MarkdownEditBox.Enums
{
    public enum WebScriptNotifyType
    {
        [EnumMember(Value = "appLoaded")]
        AppLoaded,

        [EnumMember(Value = "editorLoaded")]
        EditorLoaded,

        [EnumMember(Value = "excuteActionSuccess")]
        ExcuteActionSuccess,

        [EnumMember(Value = "excuteActionFailed")]
        ExcuteActionFailed,

        [EnumMember(Value = "defineThemeSuccess")]
        DefineThemeSuccess,

        [EnumMember(Value = "defineThemeFailed")]
        DefineThemeFailed,

        [EnumMember(Value = "setThemeFailed")]
        SetThemeFailed,

        [EnumMember(Value = "save")]
        Save,

        [EnumMember(Value = "contentChanged")]
        ContentChanged,

        [EnumMember(Value = "contextmenu")]
        ContextMenu,

        [EnumMember(Value = "resize")]
        Resize,
    }
}
