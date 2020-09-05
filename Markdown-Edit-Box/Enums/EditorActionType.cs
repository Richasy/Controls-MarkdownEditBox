using System.Runtime.Serialization;

namespace MarkdownEditBox.Enums
{
    public enum EditorActionType
    {
        [EnumMember(Value = "markdown-bold")]
        Bold,

        [EnumMember(Value = "markdown-italic")]
        Italic,

        [EnumMember(Value = "markdown-underline")]
        Underline,

        [EnumMember(Value = "markdown-strikethrough")]
        Strikethrough,

        [EnumMember(Value = "markdown-supscript")]
        Supscript,

        [EnumMember(Value = "markdown-subscript")]
        Subscript,

        [EnumMember(Value = "markdown-codeline")]
        InlineCode,

        [EnumMember(Value = "markdown-pre")]
        CodeBlock,

        [EnumMember(Value = "markdown-quote")]
        Quote,

        [EnumMember(Value = "markdown-header1")]
        Header1,

        [EnumMember(Value = "markdown-header2")]
        Header2,

        [EnumMember(Value = "markdown-header3")]
        Header3,

        [EnumMember(Value = "markdown-header4")]
        Header4,

        [EnumMember(Value = "markdown-header5")]
        Header5,

        [EnumMember(Value = "markdown-header6")]
        Header6,

        [EnumMember(Value = "editor-save")]
        Save,

        [EnumMember(Value = "editor-copy")]
        Copy,

        [EnumMember(Value = "editor-cut")]
        Cut,

        [EnumMember(Value = "editor-paste")]
        Paste
    }
}
