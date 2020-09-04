# MarkdownEditBox

该控件的核心编辑器基于 [monaco-editor](https://microsoft.github.io/monaco-editor/)，markdown语法解析基于 [markdown-it](https://github.com/markdown-it/markdown-it)，基于 [Vue](https://vuejs.org/) 将这二者组织起来，然后在UWP中使用WebView呈现。

*好吧，请叫我终极缝合怪*

事实上，除了组合它们，我也进行了一些简单的工作：

- 对 monaco-editor 中的 markdown 语法规则进行扩展，适配大多数扩展语法，并修改 markdown 语法解析后的 token，使之更为易懂。
- 集成 markdown-it 的大多数可用插件，极大扩展了 markdown 的解析内容
- 由于用 vue 包了一层，所以对外暴露了一些接口，根据这些接口，我得以在 WebView 中与其进行互操作

## 使用说明

### 简单的开始

(未完待续...)