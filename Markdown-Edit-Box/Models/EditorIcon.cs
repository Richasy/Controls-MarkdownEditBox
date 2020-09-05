using MarkdownEditBox.Enums;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace MarkdownEditBox.Models
{
    public class EditorIcon : FontIcon
    {
        public EditorIcon()
        {
            FontFamily = new FontFamily("/Markdown-Edit-Box/Assets/editor.ttf#editor");
        }

        public EditorIcon(EditorSymbol symbol) : this()
        {
            Symbol = symbol;
        }

        public EditorSymbol Symbol
        {
            get { return (EditorSymbol)GetValue(SymbolProperty); }
            set { SetValue(SymbolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Symbol.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SymbolProperty =
            DependencyProperty.Register("Symbol", typeof(EditorSymbol), typeof(EditorIcon), new PropertyMetadata(null, new PropertyChangedCallback(Symbol_Changed)));

        private static void Symbol_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is EditorSymbol icon)
            {
                var instance = d as EditorIcon;
                instance.Glyph = ((char)icon).ToString();
            }
        }
    }
}
