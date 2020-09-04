using MarkdownEditBox.Enums;
using System;

namespace MarkdownEditBox.Editor.Args
{
    public class EditorExcuteSuccessEventArgs:EventArgs
    {
        public WebScriptNotifyType Type { get; private set; }
        public string ExcuteParameter { get; private set; }
        internal EditorExcuteSuccessEventArgs()
        {

        }
        internal EditorExcuteSuccessEventArgs(WebScriptNotifyType type,string param)
        {
            Type = type;
            ExcuteParameter = param;
        }
    }
}
