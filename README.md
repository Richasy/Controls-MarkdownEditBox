# MarkdownEditBox

[![Nuget](https://img.shields.io/nuget/v/Richasy.Controls.MarkdownEditBox)](https://www.nuget.org/packages/Richasy.Controls.MarkdownEditBox/)

## Introduction

The core editor of the control is based on [monaco-editor](https://microsoft.github.io/monaco-editor/), Markdown syntax analysis is based on [markdown-it](https://github.com/markdown-it/markdown-it)，Organize the two based on [Vue](https://vuejs.org/), and then use WebView to present them in UWP.

So it is not a lightweight Native editor, on the contrary, it is very heavy, but it has quite complete functions.

***The minimum system version requirement of this control is Windows10 ver 1809***

## Control characteristics

- Expanded Markdown syntax highlighting
- Support most Markdown syntax rendering
- Provide separate editor `Editor` mode and edit+preview `Split` mode
- Built-in relatively complete shortcut key definition, and provide right-click menu
- Provide a rich theme definition interface (actually the set of monaco-editor)
- Support css import

## Simple start

1. Reference the nuget package in the project:[Richasy.Controls.MarkdownEditBox](https://www.nuget.org/packages/Richasy.Controls.MarkdownEditBox/)
2. Create a page `EditorPage.xaml`.

**EditorPage.xaml**

```xml
<Page ...
    xmlns:editor="using:MarkdownEditBox.Editor"
    >
    <editor:MarkdownEditBox x:Name="MyEditor"
                        IsLoadingRingActive="True"
                        LoadingRingForeground="{ThemeResource SystemColorControlAccentBrush}"
                        ControlLoaded="MyEditor_ControlLoaded"
                        />
</Page>
```

**EditorPage.xaml.cs**

```csharp
private async void MyEditor_ControlLoaded(object sender, EventArgs e)
{
    var displayOptions = DisplayOptions.CreateOptions();
    var editorOptions = EditorOptions.CreateOptions();
    var languageOptions = await EditorLanguageOptions.GetDefaultEnOptionsAsync();
    editorOptions.Theme = "";
    await MyEditor.Initialize("# Hello Markdown!", displayOptions, editorOptions, "", languageOptions);
    var cssFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri("你的自定义CSS文件路径（需要在项目内）"));
    string css = await FileIO.ReadTextAsync(cssFile);
    await MyEditor.SetPreviewStyle(css);
}
```

Since it is a WebView-based control, MarkdownEditBox does not try to use traditional `MarkdownEditBox.Text` property to set the text, but takes the form of calling methods for configuration.

For detailed information about related configurations, and how to use MarkdownEditBox in some common scenarios, you can refer to the sample project in the repository.

## Known issues

1. Use a lot of memory, about 120MB when running
2. When switching applications quickly, there may be garbled characters when using the input method to input text (Chakra kernel issue)
3. After selecting the text, using the input method to input text for the first time is invalid (Chakra kernel issue)
4. In `Split` mode, there will be obvious lag when adjusting the area ratio (Chakra kernel issue)

The monaco editor is designed for VS Code after all, and VS Code is based on Electron (Chromium core), so some problems will arise when running in the current WebView.

This control is actually prepared for the WebView2 of WinUI3. After WebView2 for UWP is officially released, the browser kernel of the control will be replaced.

## Thanks

- [monaco editor](https://github.com/Microsoft/monaco-editor)
- [markdown-it](https://github.com/markdown-it/markdown-it)
- [vue](https://github.com/vuejs/vue)
- [vue-split-pane](https://github.com/PanJiaChen/vue-split-pane)