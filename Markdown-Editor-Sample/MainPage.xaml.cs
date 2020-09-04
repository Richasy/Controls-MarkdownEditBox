using MarkdownEditBox.Models;
using Richasy.Helper.UWP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Markdown_Editor_Sample
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private StorageFile _file;
        private Instance _instance = new Instance("MarkdownSample");
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void MyEditor_ControlLoaded(object sender, EventArgs e)
        {
            var displayOptions = DisplayOptions.CreateOptions();
            var editorOptions = EditorOptions.CreateOptions();
            editorOptions.Theme = "";
            await MyEditor.Initialize("# Hello Markdown!", displayOptions, editorOptions);
            var cssFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/acrmd.css"));
            string css = await FileIO.ReadTextAsync(cssFile);
            await MyEditor.SetPreviewStyle(css);
        }

        private void MyEditor_ExcuteSuccess(object sender, MarkdownEditBox.Editor.Args.EditorExcuteSuccessEventArgs e)
        {

        }

        private void MyEditor_ExcuteFailed(object sender, MarkdownEditBox.Editor.Args.EditorExcuteFailedEventArgs e)
        {

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
                var file = await _instance.IO.OpenLocalFileAsync(".md");
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

                var file = await _instance.IO.GetSaveFileAsync(".md", "My Markdown.md", "Markdown File");
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
