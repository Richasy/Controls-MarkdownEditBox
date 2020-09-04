using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MarkdownEditBox.Editor
{
    [TemplatePart(Name = "MainWebView", Type = typeof(WebView))]
    public partial class MarkdownEditBox : Control
    {
        public WebView MainWebView;
        public MarkdownEditBox()
        {
            this.DefaultStyleKey = typeof(MarkdownEditBox);
        }

        protected override void OnApplyTemplate()
        {
            MainWebView = GetTemplateChild("MainWebView") as WebView;
            MainWebView.ScriptNotify += MainWebView_ScriptNotify;

            base.OnApplyTemplate();
        }

        
    }
}
