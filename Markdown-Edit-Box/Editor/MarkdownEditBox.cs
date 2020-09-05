using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MarkdownEditBox.Editor
{
    [TemplatePart(Name = "MainWebView", Type = typeof(WebView))]
    [TemplatePart(Name = "ContextMenuFlyout", Type = typeof(CommandBarFlyout))]
    public partial class MarkdownEditBox : Control
    {
        public WebView MainWebView;
        private CommandBarFlyout _contextMenuFlyout;
        public MarkdownEditBox()
        {
            this.DefaultStyleKey = typeof(MarkdownEditBox);
        }

        protected override void OnApplyTemplate()
        {
            MainWebView = GetTemplateChild("MainWebView") as WebView;
            MainWebView.ScriptNotify += MainWebView_ScriptNotify;

            _contextMenuFlyout = GetTemplateChild("ContextMenuFlyout") as CommandBarFlyout;

            base.OnApplyTemplate();
        }

        
    }
}
