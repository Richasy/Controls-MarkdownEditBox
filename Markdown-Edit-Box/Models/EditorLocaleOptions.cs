using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MarkdownEditBox.Models
{
    /// <summary>
    /// Language configuration table, suitable for pop-up menu
    /// </summary>
    public class EditorLocaleOptions
    {
        /// <summary>
        /// Locale name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Configuration list
        /// </summary>
        public List<EditorLocaleOptionItem> Options { get; set; }

        protected EditorLocaleOptions() { }

        /// <summary>
        /// Create locale configuration through defined json
        /// </summary>
        /// <param name="localeName">Language name, e.g en-US</param>
        /// <param name="optionJson">Defined language configuration item json</param>
        /// <returns></returns>
        public static EditorLocaleOptions CreateOptions(string localeName, string optionJson)
        {
            if (string.IsNullOrEmpty(localeName) || string.IsNullOrEmpty(optionJson))
                throw new ArgumentNullException("The parameter passed in cannot be empty");
            else if (!optionJson.StartsWith("["))
                throw new ArgumentNullException("The incoming OptionJson should be a JSON array");

            var options = JsonConvert.DeserializeObject<List<EditorLocaleOptionItem>>(optionJson);
            bool hasRepeat = options.Distinct().Count() != options.Count();
            if (hasRepeat)
                throw new InvalidCastException("The incoming language list contains duplicates");

            return new EditorLocaleOptions()
            {
                Name = localeName,
                Options = options
            };
        }

        /// <summary>
        /// Create locale configuration by type
        /// </summary>
        /// <param name="localeName">Language name, e.g en-US</param>
        /// <param name="options">Defined language configuration items</param>
        /// <returns></returns>
        public static EditorLocaleOptions CreateOptions(string localeName, List<EditorLocaleOptionItem> options)
        {
            if (string.IsNullOrEmpty(localeName) || options == null)
                throw new ArgumentNullException("The parameter passed in cannot be empty");

            bool hasRepeat = options.Distinct().Count() != options.Count;
            if (hasRepeat)
                throw new InvalidCastException("The incoming language list contains duplicates");

            return new EditorLocaleOptions
            {
                Name = localeName,
                Options = options
            };
        }

        /// <summary>
        /// Get the default Simplified Chinese language configuration
        /// </summary>
        /// <returns></returns>
        public static async Task<EditorLocaleOptions> GetDefaultZhOptionsAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Markdown-Edit-Box/Assets/zh-CN.json"));
            string json = await FileIO.ReadTextAsync(file);
            return CreateOptions("zh-CN", json);
        }

        /// <summary>
        /// Get the default English language configuration
        /// </summary>
        /// <returns></returns>
        public static async Task<EditorLocaleOptions> GetDefaultEnOptionsAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Markdown-Edit-Box/Assets/en-US.json"));
            string json = await FileIO.ReadTextAsync(file);
            return CreateOptions("en-US", json);
        }
    }

    /// <summary>
    /// Language configuration entry
    /// </summary>
    public class EditorLocaleOptionItem
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Entry content
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Instantiate <see cref="EditorLocaleOptionItem"/>
        /// </summary>
        public EditorLocaleOptionItem()
        {

        }

        /// <summary>
        /// Instantiate <see cref="EditorLocaleOptionItem"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public EditorLocaleOptionItem(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public override bool Equals(object obj)
        {
            return obj is EditorLocaleOptionItem item &&
                   Id == item.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
        }
    }
}
