using MarkdownEditBox.Editor.Args;
using MarkdownEditBox.Enums;
using MarkdownEditBox.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

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
        /// <summary>
        /// Occurs when the proportion of editing module changes
        /// </summary>
        public event EventHandler<EditorResizeEventArgs> Resize;

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
                        ExcuteSuccess?.Invoke(this, new EditorExcuteSuccessEventArgs(notify.Key, notify.Value));
                        break;
                    case WebScriptNotifyType.ExcuteActionFailed:
                    case WebScriptNotifyType.DefineThemeFailed:
                    case WebScriptNotifyType.SetThemeFailed:
                        ExcuteFailed?.Invoke(this, new EditorExcuteFailedEventArgs(notify.Value));
                        break;
                    case WebScriptNotifyType.Save:
                        RequestSave?.Invoke(this, EventArgs.Empty);
                        break;
                    case WebScriptNotifyType.ContentChanged:
                        ContentChanged?.Invoke(this, EventArgs.Empty);
                        break;
                    case WebScriptNotifyType.ContextMenu:
                        ShowContextMenuFlyout(notify.Value);
                        break;
                    case WebScriptNotifyType.Resize:
                        Resize?.Invoke(this, new EditorResizeEventArgs(Convert.ToDouble(notify.Value)));
                        break;
                    default:
                        break;
                }
            }
        }

        private async void ContextMenuButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as AppBarButton;
            string actionId = btn.Tag.ToString();
            await ExcuteActionAsync(actionId);
        }

        /// <summary>
        /// Perform some configuration work after the editing module is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Initialize_EditorLoaded(object sender, EventArgs e)
        {
            var actionJson = await InvokeScriptAsync(InvokeScriptTargetType.Editor, "getActions");
            if (!string.IsNullOrEmpty(actionJson))
            {
                var actions = JsonConvert.DeserializeObject<List<EditorAction>>(actionJson);
                actions = actions.Where(p => p.Id.ToEnumString().Contains("markdown-")).ToList();
                if (_contextMenuFlyout != null)
                {
                    _contextMenuFlyout.SecondaryCommands.Clear();
                    var groups = actions.GroupBy(p => p.ContextMenuGroupId);
                    foreach (var group in groups)
                    {
                        foreach (var action in group)
                        {
                            var btn = new AppBarButton();
                            btn.Tag = action.Id.ToEnumString();
                            btn.Label = action.Label;
                            btn.Icon = GetIconFromActionId(action.Id);
                            btn.Click += ContextMenuButton_Click;

                            // Add shortcut
                            if (action.AttachKeys.Length > 0)
                            {
                                var acc = new KeyboardAccelerator();
                                var keyString = action.AttachKeys.Last();
                                if (action.AttachKeys.Length == 3)
                                    acc.Modifiers = VirtualKeyModifiers.Control | VirtualKeyModifiers.Shift;
                                else
                                    acc.Modifiers = VirtualKeyModifiers.Control;
                                if (Regex.IsMatch(keyString, @"[1-9]"))
                                    keyString = "Number" + keyString;
                                Enum.TryParse(keyString, out VirtualKey key);
                                acc.Key = key;
                                btn.KeyboardAccelerators.Add(acc);
                            }
                            
                            _contextMenuFlyout.SecondaryCommands.Add(btn);
                        }
                        _contextMenuFlyout.SecondaryCommands.Add(new AppBarSeparator());
                    }
                    if (_contextMenuFlyout.SecondaryCommands.Last() is AppBarSeparator)
                        _contextMenuFlyout.SecondaryCommands.RemoveAt(_contextMenuFlyout.SecondaryCommands.Count - 1);
                    foreach (var btn in _contextMenuFlyout.PrimaryCommands.OfType<AppBarButton>())
                    {
                        btn.Click -= ContextMenuButton_Click;
                        btn.Click += ContextMenuButton_Click;
                    }

                    // Change menu display language
                    if (CurrentLanguageOptions != null)
                        UpdateLanguageOptions(CurrentLanguageOptions);
                }
            }
            EditorLoaded -= Initialize_EditorLoaded;
        }
    }
}
