using Markdown_Editor_Sample.Enums;
using MarkdownEditBox.Models;
using Richasy.Controls.UWP.Models.UI;
using Richasy.Controls.UWP.Popups;
using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Markdown_Editor_Sample.Pages
{
    /// <summary>
    /// This scenario is suitable for simple configuration out of the box
    /// Usually the initialization operation of the control is carried out in the ControlLoaded event
    /// In order to improve usability, simple file operations are added here
    /// </summary>
    public sealed partial class Scenario1 : RichasyPage
    {
        private StorageFile _file;
        public Scenario1() : base()
        {
            this.InitializeComponent();
        }
        private async void MyEditor_ControlLoaded(object sender, EventArgs e)
        {
            var displayOptions = DisplayOptions.CreateOptions();
            var editorOptions = EditorOptions.CreateOptions();
            var languageOptions = await EditorLanguageOptions.GetDefaultEnOptionsAsync();
            editorOptions.Theme = "";
            await MyEditor.Initialize("# Hello Markdown!", displayOptions, editorOptions, "", languageOptions);
            var cssFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/acrmd.css"));
            string css = await FileIO.ReadTextAsync(cssFile);
            await MyEditor.SetPreviewStyle(css);
        }

        private void MyEditor_ExcuteSuccess(object sender, MarkdownEditBox.Editor.Args.EditorExcuteSuccessEventArgs e)
        {
            new TipPopup(App._instance, "Excute action successed").Show(ColorNames.SuccessPopBackground);
        }

        private void MyEditor_ExcuteFailed(object sender, MarkdownEditBox.Editor.Args.EditorExcuteFailedEventArgs e)
        {
            new TipPopup(App._instance, e.Message).Show(ColorNames.FailedPopBackground);
        }

        private async void MyEditor_RequestSave(object sender, EventArgs e)
        {
            await SaveFile();
        }

        private void MyEditor_ContentChanged(object sender, EventArgs e)
        {
            ChangedSign.Visibility = Visibility.Visible;
        }

        private async void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyEditor.IsEditorLoaded)
            {
                var file = await App._instance.IO.OpenLocalFileAsync(".md");
                if (file != null)
                {
                    _file = file;
                    FileNameBlock.Text = file.DisplayName;
                    SaveFileButton.IsEnabled = true;
                    string content = await FileIO.ReadTextAsync(file);
                    await MyEditor.SetMarkdownAsync(content);
                    ChangedSign.Visibility = Visibility.Collapsed;
                }
            }
        }

        private async void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveFile();
        }

        private async Task SaveFile()
        {
            string markdown = await MyEditor.GetMarkdownAsync();
            if (_file == null)
            {

                var file = await App._instance.IO.GetSaveFileAsync(".md", "My Markdown.md", "Markdown File");
                if (file != null)
                {
                    _file = file;
                    FileNameBlock.Text = file.DisplayName;
                }
                else
                    return;
            }
            await FileIO.WriteTextAsync(_file, markdown);
            ChangedSign.Visibility = Visibility.Collapsed;
        }

        private void MyEditor_EditorLoaded(object sender, EventArgs e)
        {
            OpenFileButton.IsEnabled = true;
        }
    }
}
