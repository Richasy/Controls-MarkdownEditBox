using System;

namespace MarkdownEditBox.Editor.Args
{
    /// <summary>
    /// Parameters when the editor boundary scale changes
    /// </summary>
    public class EditorResizeEventArgs:EventArgs
    {
        /// <summary>
        /// Proportion of editing module
        /// </summary>
        public double Percent { get; set; }

        public EditorResizeEventArgs()
        {
            Percent = 0;
        }
        public EditorResizeEventArgs(double percent)
        {
            Percent = percent;
        }
    }
}
