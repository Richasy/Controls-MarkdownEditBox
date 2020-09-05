using MarkdownEditBox.Models;
using Newtonsoft.Json;
using Richasy.Controls.UWP.Models.UI;
using Richasy.Font.UWP;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Navigation;

namespace Markdown_Editor_Sample.Pages
{
    /// <summary>
    /// This scenario is suitable for simple attribute modification
    /// </summary>
    public sealed partial class Scenario2 : RichasyPage
    {
        public ObservableCollection<SystemFont> FontCollection = new ObservableCollection<SystemFont>();
        public Scenario2():base()
        {
            this.InitializeComponent(); 
            IsInit = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var fonts = SystemFont.GetSystemFonts();
            fonts.ForEach(p => FontCollection.Add(p));
            var selectedFont = fonts.Where(p => p.Name == "Microsoft YaHei UI").FirstOrDefault();
            if (selectedFont != null)
                FontFamilyComboBox.SelectedItem = selectedFont;
            base.OnNavigatedTo(e);
        }

        private async void FontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsInit)
            {
                var font = FontFamilyComboBox.SelectedItem as SystemFont;
                await MyEditor.SetFontFamilyAsync(font.Name);
            }
        }

        private async void MyEditor_ControlLoaded(object sender, EventArgs e)
        {
            var displayOptions = DisplayOptions.CreateOptions();
            var editorOptions = EditorOptions.CreateOptions();
            editorOptions.FontFamily = "Microsoft YaHei UI";
            var languageOptions = await EditorLanguageOptions.GetDefaultEnOptionsAsync();
            editorOptions.Theme = "";
            var mdFile= await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/test.md"));
            string md = await FileIO.ReadTextAsync(mdFile);
            await MyEditor.Initialize(md, displayOptions, editorOptions, "", languageOptions);
            var cssFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/acrmd.css"));
            string css = await FileIO.ReadTextAsync(cssFile);
            await MyEditor.SetPreviewStyle(css);
            ControlPanel.Visibility = Visibility.Visible;
        }

        private async void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (IsInit)
            {
                await MyEditor.SetFontSizeAsync(e.NewValue);
            }
        }

        private async void LineNumberSwitcher_Toggled(object sender, RoutedEventArgs e)
        {
            if (IsInit)
            {
                var obj = new
                {
                    lineNumbers = LineNumberSwitcher.IsOn ? "on" : "off"
                };
                await MyEditor.UpdateEditorOptionsAsync(JsonConvert.SerializeObject(obj));
            }
        }

        private async void MinimapSwitcher_Toggled(object sender, RoutedEventArgs e)
        {
            if (IsInit)
            {
                var obj = new
                {
                    minimap=new
                    {
                        enabled=MinimapSwitcher.IsOn
                    }
                };
                await MyEditor.UpdateEditorOptionsAsync(JsonConvert.SerializeObject(obj));
            }
        }

        private void MyEditor_EditorLoaded(object sender, EventArgs e)
        {
            IsInit = true;
        }
    }
}
