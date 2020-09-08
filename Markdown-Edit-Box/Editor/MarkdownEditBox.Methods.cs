using MarkdownEditBox.Enums;
using MarkdownEditBox.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace MarkdownEditBox.Editor
{
    public partial class MarkdownEditBox
    {
        /// <summary>
        /// Initialize the control
        /// </summary>
        /// <param name="markdown">Initial Markdown text</param>
        /// <param name="displayOptions">Control layout configuration</param>
        /// <param name="editorOptions">Edit module configuration</param>
        /// <param name="themeData">Initialized theme data (JSON)</param>
        /// <param name="languages">Language configuration items passed in during initialization</param>
        /// <returns></returns>
        public async Task Initialize(string markdown, DisplayOptions displayOptions, EditorOptions editorOptions, string themeData = "", EditorLocaleOptions languages = null)
        {
            if (displayOptions == null || editorOptions == null)
            {
                throw new ArgumentNullException("The incoming configuration cannot be null");
            }
            if (languages != null)
                CurrentLocaleOptions = languages;
            markdown = GetSafeMarkdownString(markdown);
            await UpdateDisplayAsync(displayOptions);

            string editorJson = JsonConvert.SerializeObject(editorOptions);
            EditorLoaded += Initialize_EditorLoaded;
            await InvokeScriptAsync(InvokeScriptTargetType.Editor, "initialization", markdown, JsonConvert.SerializeObject(editorOptions), themeData);
        }

        /// <summary>
        /// Update the layout mode of the control
        /// </summary>
        /// <param name="displayOptions">Layout configuration</param>
        /// <returns></returns>
        public async Task UpdateDisplayAsync(DisplayOptions displayOptions)
        {
            if (displayOptions == null)
            {
                throw new ArgumentNullException("The incoming configuration cannot be null");
            }

            CurrentDisplayMode = displayOptions.DisplayType;
            await InvokeScriptAsync(InvokeScriptTargetType.App, "updateDisplay", JsonConvert.SerializeObject(displayOptions));
        }

        /// <summary>
        /// Update locale configuration items
        /// </summary>
        /// <param name="options">Language configuration</param>
        public void UpdateLocaleOptions(EditorLocaleOptions options)
        {
            if (_contextMenuFlyout == null)
                throw new KeyNotFoundException("No popup menu item found");
            var btns = _contextMenuFlyout.PrimaryCommands.Union(_contextMenuFlyout.SecondaryCommands).OfType<AppBarButton>();
            foreach (var btn in btns)
            {
                string id = btn.Tag.ToString();
                var lan = options.Options.Where(p => p.Id == id).FirstOrDefault();
                if (lan != null)
                {
                    btn.Label = lan.Text;
                }
            }
        }

        /// <summary>
        /// Inject Markdown text into the editing module
        /// </summary>
        /// <param name="markdown">Markdown text</param>
        /// <returns></returns>
        public async Task SetMarkdownAsync(string markdown)
        {
            if (markdown == null)
                markdown = "";
            markdown = GetSafeMarkdownString(markdown);
            await InvokeScriptAsync(InvokeScriptTargetType.Editor, "setContent", markdown);
        }

        /// <summary>
        /// Get the markdown content in the current editing module
        /// </summary>
        /// <returns>Markdown text</returns>
        public async Task<string> GetMarkdownAsync()
        {
            string result = await InvokeScriptAsync(InvokeScriptTargetType.Editor, "getContent");
            return result;
        }

        /// <summary>
        /// Define editor theme
        /// </summary>
        /// <param name="themeName">Theme name</param>
        /// <param name="themeJson">Theme data (JSON)</param>
        /// <returns></returns>
        public async Task DefineThemeAsync(string themeName, string themeJson)
        {
            if (string.IsNullOrEmpty(themeName) || string.IsNullOrEmpty(themeJson))
                throw new ArgumentNullException("The parameter passed in cannot be empty");

            themeJson = themeJson.Replace("\n", "").Replace("\r", "");
            await InvokeScriptAsync(InvokeScriptTargetType.Editor, "defineTheme", themeName, themeJson);
        }

        /// <summary>
        /// Set the theme of the current editing module
        /// </summary>
        /// <param name="themeName">Theme name</param>
        /// <returns></returns>
        public async Task SetThemeAsync(string themeName)
        {
            if (string.IsNullOrEmpty(themeName))
                throw new ArgumentNullException("The parameter passed in cannot be empty");

            await InvokeScriptAsync(InvokeScriptTargetType.Editor, "setTheme", themeName);
        }

        /// <summary>
        /// Update editor configuration items
        /// </summary>
        /// <param name="optionJson">Pass in the JSON text of the configuration item object</param>
        /// <returns></returns>
        public async Task UpdateEditorOptionsAsync(string optionJson)
        {
            if (string.IsNullOrEmpty(optionJson))
                throw new ArgumentNullException("The parameter passed in cannot be empty");

            optionJson = optionJson.Replace("\n", "").Replace("\r", "");
            await InvokeScriptAsync(InvokeScriptTargetType.Editor, "updateOptions", optionJson);
        }

        /// <summary>
        /// Set edit module font
        /// </summary>
        /// <param name="fontFamily">Font name</param>
        /// <returns></returns>
        public async Task SetFontFamilyAsync(string fontFamily)
        {
            if (string.IsNullOrEmpty(fontFamily))
                throw new ArgumentNullException("The parameter passed in cannot be empty");

            var opt = new
            {
                fontFamily
            };
            await UpdateEditorOptionsAsync(JsonConvert.SerializeObject(opt));
        }

        /// <summary>
        /// Set edit module font size
        /// </summary>
        /// <param name="fontSize">Font size</param>
        /// <returns></returns>
        public async Task SetFontSizeAsync(double fontSize)
        {
            if (fontSize <= 0)
                throw new ArgumentOutOfRangeException("Font size should be greater than 0");

            var opt = new
            {
                fontSize
            };
            await UpdateEditorOptionsAsync(JsonConvert.SerializeObject(opt));
        }

        /// <summary>
        /// Set edit module line height
        /// </summary>
        /// <param name="lineHeight">Line height</param>
        /// <returns></returns>
        public async Task SetLineHeightAsync(double lineHeight)
        {
            if (lineHeight <= 0)
                throw new ArgumentOutOfRangeException("Line height should be greater than 0");

            var opt = new
            {
                lineHeight
            };
            await UpdateEditorOptionsAsync(JsonConvert.SerializeObject(opt));
        }

        /// <summary>
        /// Inject Markdown text into the preview module
        /// </summary>
        /// <param name="markdown">Markdown text</param>
        /// <returns></returns>
        public async Task SetRenderHTMLAsync(string markdown)
        {
            if (markdown == null)
                markdown = "";
            markdown = GetSafeMarkdownString(markdown);
            await InvokeScriptAsync(InvokeScriptTargetType.Preview, "setContent", markdown);
        }

        /// <summary>
        /// Get the rendered HTML content from the preview module
        /// </summary>
        /// <returns>HTML</returns>
        public async Task<string> GetRenderHTMLAsync()
        {
            string result = await InvokeScriptAsync(InvokeScriptTargetType.Preview, "getContent");
            return result;
        }

        /// <summary>
        /// Set the style of the preview module (inject CSS)
        /// </summary>
        /// <param name="css">CSS text</param>
        /// <returns></returns>
        public async Task SetPreviewStyle(string css)
        {
            css = css.Replace("\n", "").Replace("\r", "");
            await InvokeScriptAsync(InvokeScriptTargetType.Preview, "setCss", css);
        }

        /// <summary>
        /// Execute preset editor commands
        /// </summary>
        /// <param name="action">Action type</param>
        /// <returns></returns>
        public async Task ExcuteActionAsync(EditorActionType action)
        {
            await ExcuteActionAsync(action.ToEnumString());
        }

        /// <summary>
        /// Execute editor commands (can be some commands that are not preset)
        /// </summary>
        /// <param name="actionId">Action id</param>
        /// <returns></returns>
        public async Task ExcuteActionAsync(string actionId)
        {
            if (string.IsNullOrEmpty(actionId))
                throw new ArgumentNullException("The parameter passed in cannot be empty");

            await InvokeScriptAsync(InvokeScriptTargetType.Editor, "excuteAction", actionId);
        }

        /// <summary>
        /// Asynchronously inject Javascript to execute scripts
        /// </summary>
        /// <param name="target">Editor target</param>
        /// <param name="functionName">Function name</param>
        /// <param name="parameters">Incoming parameters</param>
        /// <returns></returns>
        public async Task<string> InvokeScriptAsync(InvokeScriptTargetType target, string functionName, params string[] parameters)
        {
            if (string.IsNullOrEmpty(functionName))
                throw new ArgumentNullException("Function name cannot be empty");
            if (parameters == null)
                parameters = new string[] { };
            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = $"`{parameters[i]}`";
            }
            string prefix = target.ToEnumString();
            string eval = $"{prefix}.{functionName}({string.Join(',', parameters)})";
            string result = await MainWebView.InvokeScriptAsync("eval", new string[] { eval });
            return result;
        }

        /// <summary>
        /// Escape some characters that cause problems in Markdown
        /// </summary>
        /// <param name="markdown">Markdown text</param>
        /// <returns></returns>
        private string GetSafeMarkdownString(string markdown)
        {
            if (!string.IsNullOrEmpty(markdown))
            {
                markdown = markdown.Replace("\\", "\\\\");
                var reg = new Regex("`");
                markdown = reg.Replace(markdown, "\\`");
                markdown = markdown.Replace("${", "\\$\\{");
            }
            return markdown;
        }

        /// <summary>
        /// Check and edit module layout (forced to refresh layout)
        /// </summary>
        /// <returns></returns>
        public async Task UpdateEditorLayoutAsync()
        {
            await InvokeScriptAsync(InvokeScriptTargetType.Monaco, "layout");
        }

        private EditorIcon GetIconFromActionId(EditorActionType action)
        {
            EditorSymbol symbol = EditorSymbol.Text;
            switch (action)
            {
                case EditorActionType.Bold:
                    symbol = EditorSymbol.Bold;
                    break;
                case EditorActionType.Italic:
                    symbol = EditorSymbol.Italic;
                    break;
                case EditorActionType.Underline:
                    symbol = EditorSymbol.Underline;
                    break;
                case EditorActionType.Strikethrough:
                    symbol = EditorSymbol.Strikethrough;
                    break;
                case EditorActionType.Supscript:
                    symbol = EditorSymbol.Supscript;
                    break;
                case EditorActionType.Subscript:
                    symbol = EditorSymbol.Subscript;
                    break;
                case EditorActionType.InlineCode:
                case EditorActionType.CodeBlock:
                    symbol = EditorSymbol.Code;
                    break;
                case EditorActionType.Quote:
                    symbol = EditorSymbol.Quote;
                    break;
                case EditorActionType.Header1:
                case EditorActionType.Header2:
                case EditorActionType.Header3:
                case EditorActionType.Header4:
                case EditorActionType.Header5:
                case EditorActionType.Header6:
                    symbol = EditorSymbol.Header;
                    break;
                case EditorActionType.Save:
                    symbol = EditorSymbol.Save;
                    break;
                case EditorActionType.Copy:
                    symbol = EditorSymbol.Copy;
                    break;
                case EditorActionType.Cut:
                    symbol = EditorSymbol.Cut;
                    break;
                case EditorActionType.Paste:
                    symbol = EditorSymbol.Paste;
                    break;
                default:
                    break;
            }
            return new EditorIcon(symbol);
        }

        private void ShowContextMenuFlyout(string positionJson)
        {
            var jobj = JObject.Parse(positionJson);
            double x = Convert.ToDouble(jobj["x"].ToString());
            double y = Convert.ToDouble(jobj["y"].ToString());
            var options = new FlyoutShowOptions();
            options.Position = new Windows.Foundation.Point(x, y);
            if (_contextMenuFlyout != null)
            {
                _contextMenuFlyout.ShowAt(MainWebView, options);
            }
        }
    }
}
