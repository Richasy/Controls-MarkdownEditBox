using MarkdownEditBox.Editor.Args;
using MarkdownEditBox.Enums;
using MarkdownEditBox.Models;
using Newtonsoft.Json;
using System;
using Windows.UI.Xaml.Controls;

namespace MarkdownEditBox.Editor
{
    public partial class MarkdownEditBox
    {
        /// <summary>
        /// Indicates that the editor encountered an error while performing an operation
        /// </summary>
        public event EventHandler<EditorExcuteFailedEventArgs> ExcuteFailed;
        /// <summary>
        /// Indicates that the editor successfully performed an operation
        /// </summary>
        public event EventHandler<EditorExcuteSuccessEventArgs> ExcuteSuccess;
        /// <summary>
        /// Occurs when the editor frame is loaded and ready to load the editing module
        /// </summary>
        public event EventHandler ControlLoaded;
        /// <summary>
        /// The editing module is loaded
        /// </summary>
        public event EventHandler EditorLoaded;
        /// <summary>
        /// Triggered when the content of the document changes (the event does not contain the document content)
        /// </summary>
        public event EventHandler ContentChanged;
        /// <summary>
        /// The control requests to save the content of the document
        /// </summary>
        public event EventHandler RequestSave;

        private void MainWebView_ScriptNotify(object sender, NotifyEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Value))
            {
                var notify = JsonConvert.DeserializeObject<NotifyPackage>(e.Value);
                switch (notify.Key)
                {
                    case WebScriptNotifyType.AppLoaded:
                        IsControlLoaded = true;
                        IsLoadingRingActive = false;
                        break;
                    case WebScriptNotifyType.EditorLoaded:
                        IsEditorLoaded = true;
                        break;
                    case WebScriptNotifyType.ExcuteActionSuccess:
                    case WebScriptNotifyType.DefineThemeSuccess:
                        ExcuteSuccess.Invoke(this, new EditorExcuteSuccessEventArgs(notify.Key, notify.Value));
                        break;
                    case WebScriptNotifyType.ExcuteActionFailed:
                    case WebScriptNotifyType.DefineThemeFailed:
                    case WebScriptNotifyType.SetThemeFailed:
                        ExcuteFailed.Invoke(this, new EditorExcuteFailedEventArgs(notify.Value));
                        break;
                    case WebScriptNotifyType.Save:
                        RequestSave.Invoke(this, EventArgs.Empty);
                        break;
                    case WebScriptNotifyType.ContentChanged:
                        ContentChanged?.Invoke(this, EventArgs.Empty);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
