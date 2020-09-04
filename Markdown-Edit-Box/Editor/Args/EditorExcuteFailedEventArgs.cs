using MarkdownEditBox.Enums;
using MarkdownEditBox.Models;
using Newtonsoft.Json;
using System;

namespace MarkdownEditBox.Editor.Args
{
    public class EditorExcuteFailedEventArgs:EventArgs
    {
        public WebScriptErrorType Type { get; private set; }
        public string Message { get; private set; }
        public string ExcuteParameter { get; private set; }

        public EditorExcuteFailedEventArgs()
        {

        }

        public EditorExcuteFailedEventArgs(string msg)
        {
            var errorPack = JsonConvert.DeserializeObject<ErrorPackage>(msg);
            Type = errorPack.Action;
            Message = errorPack.ErrorMessage;
            ExcuteParameter = errorPack.Sign;
        }
    }
}
