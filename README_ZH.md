# MarkdownEditBox

[![Nuget](https://img.shields.io/nuget/v/Richasy.Controls.MarkdownEditBox)](https://www.nuget.org/packages/Richasy.Controls.MarkdownEditBox/)

[English](.\README.md)

## 简介

该控件的核心编辑器基于 [monaco-editor](https://microsoft.github.io/monaco-editor/)，markdown语法解析基于 [markdown-it](https://github.com/markdown-it/markdown-it)，基于 [Vue](https://vuejs.org/) 将这二者组织起来，然后在 UWP 中使用 WebView 呈现。

所以它不是轻量级的 Native 编辑器，相反，它很重，但功能还挺齐全。

由于是特化的Markdown编辑器，所以对moanco-editor进行了一些定制化操作，修改后的编辑器项目请查看：[Markdown-Editor-Vue](https://github.com/Richasy/Markdown-Editor-Vue)

***该控件的最低系统版本需求为 Windows10 ver 1809***

## 控件特性

- 扩展 Markdown 语法高亮
- 支持绝大多数 Markdown 语法渲染
- 提供单独编辑器的 `Editor` 模式和编辑+预览的 `Split` 模式
- 内置相对完善的快捷键定义，并提供右键菜单
- 提供丰富的主题定义接口（其实就是 monaco-editor 那一套）
- 支持 css 导入

## 简单的开始

1. 在项目中引用 nuget 包：[Richasy.Controls.MarkdownEditBox](https://www.nuget.org/packages/Richasy.Controls.MarkdownEditBox/)
2. 创建一个页面 `EditorPage.xaml`.

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

![](https://i.loli.net/2020/09/06/jqvFZrVOcY9t7fh.png)

由于自身是基于WebView的控件，所以 MarkdownEditBox 并没有尝试使用传统的 `MarkdownEditBox.Text` 之类的属性来设置文本，而是采取调用方法的形式进行配置。

关于相关配置的详细信息，以及在一些常用场景下使用 MarkdownEditBox 的方法，可以参考仓库内的示例项目。

## 已知问题

1. 内存占用很高，运行时占用约120MB
2. 快速切换应用时，使用输入法输入文本可能有乱码问题 (Chakra内核问题)
3. 选中文本后，初次使用输入法输入文本无效 (Chakra内核问题)
4. 在 `Split` 模式下，调整区域占比会有明显的卡顿 (Chakra内核问题)

monaco editor 毕竟是为 VS Code 设计，而 VS Code 又基于 Electron (Chromium核心)，所以在当前的 WebView 中运行时会产生一些问题。

该控件其实是为之后 WinUI3 的 WebView2 而准备的，以后 WebView2 for UWP 正式发布后，该控件的浏览器内核会进行替换

## 感谢

- [monaco editor](https://github.com/Microsoft/monaco-editor)
- [markdown-it](https://github.com/markdown-it/markdown-it)
- [vue](https://github.com/vuejs/vue)
- [vue-split-pane](https://github.com/PanJiaChen/vue-split-pane)