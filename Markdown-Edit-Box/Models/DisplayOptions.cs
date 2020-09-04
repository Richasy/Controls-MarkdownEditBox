using MarkdownEditBox.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownEditBox.Models
{
    /// <summary>
    /// Control display configuration
    /// </summary>
    public class DisplayOptions
    {
        /// <summary>
        /// Control how the control is initialized
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty("displayType")]
        public EditorDisplayMode DisplayType { get; set; }

        /// <summary>
        /// The minimum percentage of the left panel (0 to 100)
        /// </summary>
        [JsonProperty("minPercent")]
        public double MinPercent { get; set; }

        /// <summary>
        /// The maxinum percentage of the left panel (0 to 100)
        /// </summary>
        [JsonProperty("maxPercent")]
        public double MaxPercent { get; set; }

        /// <summary>
        /// The default percentage of the left panel (0 to 100)
        /// </summary>
        [JsonProperty("defaultPercent")]
        public double DefaultPercent { get; set; }

        protected DisplayOptions()
        {
            DisplayType = EditorDisplayMode.Split;
            MinPercent = 30;
            MaxPercent = 80;
            DefaultPercent = 50;
        }

        /// <summary>
        /// Create a display configuration
        /// with <c>Split</c> mode, the minimum value is 30, the maximum value is 80, and the default value is 50
        /// </summary>
        /// <returns></returns>
        public static DisplayOptions CreateOptions()
        {
            return new DisplayOptions();
        }

        /// <summary>
        /// Create a display configuration through <c>DisplayType</c>
        /// In <c>Split</c> mode, the minimum value is 30, the maximum value is 80, and the default value is 50
        /// In <c>Editor</c> mode, the minimum value is 100, the maximum value is 100, and the default value is 100
        /// In <c>Preview</c> mode, the minimum value is 0, the maximum value is 0, and the default value is 0
        /// </summary>
        /// <param name="displayType">Control display mode</param>
        /// <returns>Display configuration</returns>
        public static DisplayOptions CreateOptions(EditorDisplayMode displayType)
        {
            DisplayOptions options = null;
            switch (displayType)
            {
                case EditorDisplayMode.Split:
                    options = new DisplayOptions();
                    break;
                case EditorDisplayMode.Editor:
                    options = new DisplayOptions
                    {
                        DisplayType = EditorDisplayMode.Editor,
                        MinPercent = 100,
                        MaxPercent = 100,
                        DefaultPercent = 100
                    };
                    break;
                case EditorDisplayMode.Preview:
                    options = new DisplayOptions
                    {
                        DisplayType = EditorDisplayMode.Preview,
                        MinPercent = 0,
                        MaxPercent = 0,
                        DefaultPercent = 0
                    };
                    break;
                default:
                    break;
            }
            return options;
        }

        /// <summary>
        /// Create a display configuration
        /// </summary>
        /// <param name="displayType">Control display mode</param>
        /// <param name="minPercent">The minimum percentage of the left panel (0 to 100)</param>
        /// <param name="maxPercent">The maxinum percentage of the left panel (0 to 100)</param>
        /// <param name="defaultPercent">The default percentage of the left panel (0 to 100)</param>
        /// <returns>Display configuration</returns>
        public static DisplayOptions CreateOptions(EditorDisplayMode displayType, double minPercent, double maxPercent, double defaultPercent)
        {
            if (minPercent > maxPercent)
                throw new ArgumentException("The minimum value should be less than or equal to the maximum value");
            else if (minPercent < 0 || minPercent > 100)
                throw new ArgumentOutOfRangeException("The minimum value is outside the preset range and should be between 0 and 100");
            else if (maxPercent > 100)
                throw new ArgumentOutOfRangeException("The maxinum value is outside the preset range and should be between 0 and 100");
            else if(defaultPercent<minPercent || defaultPercent>maxPercent)
                throw new ArgumentOutOfRangeException("The default value should be between the minimum and maximum");
            else
            {
                return new DisplayOptions
                {
                    DisplayType = displayType,
                    MinPercent = minPercent,
                    MaxPercent = maxPercent,
                    DefaultPercent = defaultPercent
                };
            }
        }
    }
}
