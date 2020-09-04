using MarkdownEditBox.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace MarkdownEditBox.Models
{
    /// <summary>
    /// Editor configuration
    /// </summary>
    public class EditorOptions
    {
        /// <summary>
        /// Selected theme name. Default to <c>vs-dark</c>
        /// </summary>
        [JsonProperty("theme")]
        public string Theme { get; set; } = "vs-dark";

        /// <summary>
        /// Control the rendering of line numbers. 
        /// If it is a function, it will be invoked when rendering a line number and the return value will be rendered. 
        /// Otherwise, if it is a truey, line numbers will be rendered normally (equivalent of using an identity function). 
        /// Otherwise, line numbers will not be rendered. Defaults to <c>on</c>.
        /// </summary>
        [JsonProperty("lineNumbers")]
        [JsonConverter(typeof(StringEnumConverter))]
        public LineNumberMode LineNumbers { get; set; } = LineNumberMode.On;

        /// <summary>
        /// Control the width of line numbers, by reserving horizontal space for rendering at least an amount of digits. Defaults to 3.
        /// </summary>
        [JsonProperty("lineNumbersMinChars")]
        public int LineNumbersMinChars { get; set; } = 3;

        /// <summary>
        /// The width reserved for line decorations (in px). Line decorations are placed between line numbers and the editor content. You can pass in a string in the format floating point followed by "ch". 
        /// e.g. 1.3ch. Defaults to 10.
        /// </summary>
        [JsonProperty("lineDecorationsWidth")]
        public int LineDecorationsWidth { get; set; } = 10;

        /// <summary>
        /// Control the behavior and rendering of the minimap.
        /// </summary>
        [JsonProperty("minimap")]
        public MinimapOptions Minimap { get; set; } = new MinimapOptions();

        /// <summary>
        /// Enable rendering of current line highlight. Defaults to none.
        /// </summary>
        [JsonProperty("renderLineHighlight")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RenderLineHighlightMode RenderLineHighlight { get; set; } = RenderLineHighlightMode.None;

        /// <summary>
        /// Control the behavior and rendering of the scrollbars.
        /// </summary>
        [JsonProperty("scrollbar")]
        public ScrollBarOptions ScrollBar { get; set; } = new ScrollBarOptions();

        /// <summary>
        /// The font size
        /// </summary>
        [JsonProperty("fontSize")]
        public double FontSize { get; set; } = 18;

        /// <summary>
        /// The line height
        /// </summary>
        [JsonProperty("lineHeight")]
        public double LineHeight { get; set; } = 30;

        /// <summary>
        /// The font family
        /// </summary>
        [JsonProperty("fontFamily")]
        public string FontFamily { get; set; } = "\'Microsoft YaHei UI\', sans-serif";

        /// <summary>
        /// Should the editor be read only. Defaults to false.
        /// </summary>
        [JsonProperty("readOnly")]
        public bool ReadOnly { get; set; } = false;

        protected EditorOptions() { }

        /// <summary>
        /// Get the default editor configuration
        /// </summary>
        /// <returns></returns>
        public static EditorOptions CreateOptions()
        {
            return new EditorOptions();
        }

        /// <summary>
        /// Create editor configuration with basic text style
        /// </summary>
        /// <param name="theme">Theme name</param>
        /// <param name="fontFamily">Font family</param>
        /// <param name="fontSize">Font size</param>
        /// <param name="lineHeight">line height</param>
        /// <returns></returns>
        public static EditorOptions CreateOptions(string theme, string fontFamily, double fontSize, double lineHeight)
        {
            if (string.IsNullOrEmpty(theme))
                theme = "vs-dark";
            if (string.IsNullOrEmpty(fontFamily))
                throw new ArgumentException("The font name cannot be empty");
            else if (fontSize <= 0)
                throw new ArgumentOutOfRangeException("Invalid font size");
            else if (lineHeight <= 0)
                throw new ArgumentOutOfRangeException("Invalid line height");
            return new EditorOptions()
            {
                Theme = theme,
                FontFamily = fontFamily,
                LineHeight = lineHeight,
                FontSize = fontSize
            };
        }
    }

    /// <summary>
    /// Mini map configuration
    /// </summary>
    public class MinimapOptions
    {
        /// <summary>
        /// Enable the rendering of the minimap. Defaults to <c>false</c>.
        /// </summary>
        [JsonProperty("enabled")]
        public bool Enabled { get; set; } = false;

        /// <summary>
        /// Limit the width of the minimap to render at most a certain number of columns. Defaults to 120.
        /// </summary>
        [JsonProperty("maxColumn")]
        public int MaxColumn { get; set; } = 120;

        /// <summary>
        /// Render the actual text on a line (as opposed to color blocks). Defaults to <c>True</c>.
        /// </summary>
        [JsonProperty("renderCharacters")]
        public bool RenderCharacters { get; set; } = true;

        /// <summary>
        /// Relative size of the font in the minimap. Defaults to 1.
        /// </summary>
        [JsonProperty("scale")]
        public double Scale { get; set; } = 1;

        /// <summary>
        /// Control the rendering of the minimap slider. Defaults to 'mouseover'.
        /// </summary>
        [JsonProperty("showSlider")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MinimapShowMode ShowSlider { get; set; } = MinimapShowMode.MouseOver;

        /// <summary>
        /// Control the side of the minimap in editor. Defaults to 'right'.
        /// </summary>
        [JsonProperty("side")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MinimapSideMode Side { get; set; } = MinimapSideMode.Right;
    }

    /// <summary>
    /// Scroll bar configuration
    /// </summary>
    public class ScrollBarOptions
    {
        /// <summary>
        /// Always consume mouse wheel events. Defaults to true.
        /// </summary>
        [JsonProperty("alwaysConsumeMouseWheel")]
        public bool AlwaysConsumeMouseWheel { get; set; } = true;

        /// <summary>
        /// The size of arrows (if displayed). Defaults to 8.
        /// </summary>
        [JsonProperty("arrowSize")]
        public int ArrowSize { get; set; } = 8;

        /// <summary>
        /// Listen to mouse wheel events and react to them by scrolling. Defaults to true.
        /// </summary>
        [JsonProperty("handleMouseWheel")]
        public bool HandleMouseWheel { get; set; } = true;

        /// <summary>
        /// Render horizontal scrollbar. Defaults to 'hidden'.
        /// </summary>
        [JsonProperty("horizontal")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ScrollbarDisplayMode Horizontal { get; set; } = ScrollbarDisplayMode.Hidden;

        /// <summary>
        /// Render arrows at the left and right of the horizontal scrollbar. Defaults to false.
        /// </summary>
        [JsonProperty("horizontalHasArrows")]
        public bool HorizontalHasArrows { get; set; } = false;

        /// <summary>
        /// Height in pixels for the horizontal scrollbar. Defaults to 8 (px).
        /// </summary>
        [JsonProperty("horizontalScrollbarSize")]
        public int HorizontalScrollbarSize { get; set; } = 8;

        /// <summary>
        /// Height in pixels for the horizontal slider. Defaults to 8 (px).
        /// </summary>
        [JsonProperty("horizontalSliderSize")]
        public int HorizontalSliderSize { get; set; } = 8;

        /// <summary>
        /// Cast horizontal and vertical shadows when the content is scrolled. Defaults to <c>true</c>.
        /// </summary>
        [JsonProperty("useShadows")]
        public bool UseShadows { get; set; } = true;

        /// <summary>
        /// Render vertical scrollbar. Defaults to 'hidden'.
        /// </summary>
        [JsonProperty("vertical")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ScrollbarDisplayMode Vertical { get; set; } = ScrollbarDisplayMode.Auto;

        /// <summary>
        /// Render arrows at the left and right of the vertical scrollbar. Defaults to false.
        /// </summary>
        [JsonProperty("verticalHasArrows")]
        public bool VerticalHasArrows { get; set; } = false;

        /// <summary>
        /// Height in pixels for the vertical scrollbar. Defaults to 8 (px).
        /// </summary>
        [JsonProperty("verticalScrollbarSize")]
        public int VerticalScrollbarSize { get; set; } = 8;

        /// <summary>
        /// Height in pixels for the vertical slider. Defaults to 8 (px).
        /// </summary>
        [JsonProperty("verticalSliderSize")]
        public int VerticalSliderSize { get; set; } = 8;
    }
}
