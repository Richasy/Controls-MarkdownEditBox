using MarkdownEditBox.Enums;
using MarkdownEditBox.Models;
using Richasy.Controls.UWP.Models.UI;
using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Markdown_Editor_Sample.Pages
{
    /// <summary>
    /// This scene is suitable for display mode changes
    /// </summary>
    public sealed partial class Scenario3 : RichasyPage
    {
        public Scenario3():base()
        {
            this.InitializeComponent();
        }
        private async void MyEditor_ControlLoaded(object sender, EventArgs e)
        {
            var displayOptions = DisplayOptions.CreateOptions(EditBoxDisplayMode.Editor);
            var editorOptions = EditorOptions.CreateOptions();
            editorOptions.FontFamily = "Microsoft YaHei UI";
            var languageOptions = await EditorLanguageOptions.GetDefaultEnOptionsAsync();
            editorOptions.Theme = "";
            var mdFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/test.md"));
            string md = await FileIO.ReadTextAsync(mdFile);
            await MyEditor.Initialize(md, displayOptions, editorOptions, "", languageOptions);
            ControlPanel.Visibility = Visibility.Visible;
        }

        private void MyEditor_EditorLoaded(object sender, EventArgs e)
        {
            IsInit = true;
        }
        private async void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (IsInit)
            {
                var btn = sender as RadioButton;
                string tag = btn.Tag.ToString();
                Enum.TryParse(tag, out EditBoxDisplayMode mode);
                await MyEditor.UpdateDisplayAsync(DisplayOptions.CreateOptions(mode));
                if (mode == EditBoxDisplayMode.Split)
                {
                    var cssFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/acrmd.css"));
                    string css = await FileIO.ReadTextAsync(cssFile);
                    await MyEditor.SetPreviewStyle(css);
                }
                else
                    await MyEditor.UpdateEditorLayoutAsync();
            }
        }
    }
}
