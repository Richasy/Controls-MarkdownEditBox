using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace MarkdownEditBox.Editor
{
    public partial class MarkdownEditBox
    {
        private bool _isControlLoaded;
        private bool _isEditorLoaded;

        /// <summary>
        /// Indicates whether the entire editor control is loaded
        /// </summary>
        public bool IsControlLoaded
        {
            get => _isControlLoaded;
            private set
            {
                _isControlLoaded = value;
                if (value)
                    ControlLoaded?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Indicates whether the editing module of the control is loaded
        /// </summary>
        public bool IsEditorLoaded
        {
            get => _isEditorLoaded;
            private set
            {
                _isEditorLoaded = value;
                if (value)
                    EditorLoaded?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Whether to display the progress ring of the control
        /// </summary>
        public Visibility LoadingRingVisibility
        {
            get { return (Visibility)GetValue(LoadingRingVisibilityProperty); }
            set { SetValue(LoadingRingVisibilityProperty, value); }
        }

        public static readonly DependencyProperty LoadingRingVisibilityProperty =
            DependencyProperty.Register("LoadingRingVisibility", typeof(Visibility), typeof(MarkdownEditBox), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// The size of the progress ring (width and height are the same value)
        /// </summary>
        public double LoadingRingSize
        {
            get { return (double)GetValue(LoadingRingSizeProperty); }
            set { SetValue(LoadingRingSizeProperty, value); }
        }

        public static readonly DependencyProperty LoadingRingSizeProperty =
            DependencyProperty.Register("LoadingRingSize", typeof(double), typeof(MarkdownEditBox), new PropertyMetadata(40.0));

        /// <summary>
        /// The color of the progress ring
        /// </summary>
        public Brush LoadingRingForeground
        {
            get { return (Brush)GetValue(LoadingRingForegroundProperty); }
            set { SetValue(LoadingRingForegroundProperty, value); }
        }

        public static readonly DependencyProperty LoadingRingForegroundProperty =
            DependencyProperty.Register("LoadingRingForeground", typeof(Brush), typeof(MarkdownEditBox), new PropertyMetadata(DependencyProperty.UnsetValue));

        /// <summary>
        /// Whether the progress ring is activated
        /// </summary>
        public bool IsLoadingRingActive
        {
            get { return (bool)GetValue(IsLoadingRingActiveProperty); }
            set { SetValue(IsLoadingRingActiveProperty, value); }
        }

        public static readonly DependencyProperty IsLoadingRingActiveProperty =
            DependencyProperty.Register("IsLoadingRingActive", typeof(bool), typeof(MarkdownEditBox), new PropertyMetadata(false));

        public Color MainWebViewDefaultColor
        {
            get { return (Color)GetValue(MainWebViewDefaultColorProperty); }
            set { SetValue(MainWebViewDefaultColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MainWebViewDefaultColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MainWebViewDefaultColorProperty =
            DependencyProperty.Register("MainWebViewDefaultColor", typeof(Color), typeof(MarkdownEditBox), new PropertyMetadata(Colors.Transparent));


    }
}
