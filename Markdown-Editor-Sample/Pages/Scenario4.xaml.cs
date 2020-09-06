using MarkdownEditBox.Enums;
using MarkdownEditBox.Models;
using Newtonsoft.Json;
using Richasy.Controls.UWP.Models.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace Markdown_Editor_Sample.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Scenario4 : RichasyPage
    {
        public Scenario4():base()
        {
            this.InitializeComponent();
            IsInit = false;
        }

        private async void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInit)
            {
                var item = ThemeComboBox.SelectedItem as ComboBoxItem;
                string themeName = item.Tag.ToString();
                await MyEditor.SetThemeAsync(themeName);
            }
        }

        private async void MyEditor_ControlLoaded(object sender, EventArgs e)
        {
            var displayOptions = DisplayOptions.CreateOptions(EditBoxDisplayMode.Editor);
            var editorOptions = EditorOptions.CreateOptions();
            editorOptions.FontFamily = "Microsoft YaHei UI";
            var languageOptions = await EditorLanguageOptions.GetDefaultEnOptionsAsync();
            editorOptions.Theme = "none";
            var mdFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/test.md"));
            string md = await FileIO.ReadTextAsync(mdFile);
            await MyEditor.Initialize(md, displayOptions, editorOptions, "", languageOptions);
            ControlPanel.Visibility = Visibility.Visible;
        }

        private async void MyEditor_EditorLoaded(object sender, EventArgs e)
        {
            IsInit = true;
            await InitThemes();
            await MyEditor.SetThemeAsync("dark");
        }

        private async Task InitThemes()
        {
            var lightFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/light.json"));
            string lightJson = await FileIO.ReadTextAsync(lightFile);
            await MyEditor.DefineThemeAsync("light", lightJson);
            var darkFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/dark.json"));
            string darkJson = await FileIO.ReadTextAsync(darkFile);
            await MyEditor.DefineThemeAsync("dark", darkJson);
        }

        private void MyEditor_ExcuteFailed(object sender, MarkdownEditBox.Editor.Args.EditorExcuteFailedEventArgs e)
        {
            Debug.WriteLine(e.Message);
        }
    }
}
