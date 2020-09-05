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
    public class EditorLanguageOptions
    {
        /// <summary>
        /// Language name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Configuration list
        /// </summary>
        public List<EditorLanguageOptionItem> Options { get; set; }

        protected EditorLanguageOptions() { }

        /// <summary>
        /// Create language configuration through defined json
        /// </summary>
        /// <param name="languageName">Language name, e.g en-US</param>
        /// <param name="optionJson">Defined language configuration item json</param>
        /// <returns></returns>
        public static EditorLanguageOptions CreateOptions(string languageName, string optionJson)
        {
            if (string.IsNullOrEmpty(languageName) || string.IsNullOrEmpty(optionJson))
                throw new ArgumentNullException("The parameter passed in cannot be empty");
            else if (!optionJson.StartsWith("["))
                throw new ArgumentNullException("The incoming OptionJson should be a JSON array");

            var options = JsonConvert.DeserializeObject<List<EditorLanguageOptionItem>>(optionJson);
            bool hasRepeat = options.Distinct().Count() != options.Count();
            if (hasRepeat)
                throw new InvalidCastException("The incoming language list contains duplicates");

            return new EditorLanguageOptions()
            {
                Name = languageName,
                Options = options
            };
        }

        /// <summary>
        /// Create language configuration by type
        /// </summary>
        /// <param name="languageName">Language name, e.g en-US</param>
        /// <param name="options">Defined language configuration items</param>
        /// <returns></returns>
        public static EditorLanguageOptions CreateOptions(string languageName, List<EditorLanguageOptionItem> options)
        {
            if (string.IsNullOrEmpty(languageName) || options == null)
                throw new ArgumentNullException("The parameter passed in cannot be empty");

            bool hasRepeat = options.Distinct().Count() != options.Count;
            if (hasRepeat)
                throw new InvalidCastException("The incoming language list contains duplicates");

            return new EditorLanguageOptions
            {
                Name = languageName,
                Options = options
            };
        }

        /// <summary>
        /// Get the default Simplified Chinese language configuration
        /// </summary>
        /// <returns></returns>
        public static async Task<EditorLanguageOptions> GetDefaultZhOptionsAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Markdown-Edit-Box/Assets/zh-CN.json"));
            string json = await FileIO.ReadTextAsync(file);
            return CreateOptions("zh-CN", json);
        }

        /// <summary>
        /// Get the default English language configuration
        /// </summary>
        /// <returns></returns>
        public static async Task<EditorLanguageOptions> GetDefaultEnOptionsAsync()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Markdown-Edit-Box/Assets/en-US.json"));
            string json = await FileIO.ReadTextAsync(file);
            return CreateOptions("en-US", json);
        }
    }

    /// <summary>
    /// Language configuration entry
    /// </summary>
    public class EditorLanguageOptionItem
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
        /// Instantiate <see cref="EditorLanguageOptionItem"/>
        /// </summary>
        public EditorLanguageOptionItem()
        {

        }

        /// <summary>
        /// Instantiate <see cref="EditorLanguageOptionItem"/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public EditorLanguageOptionItem(string id, string text)
        {
            Id = id;
            Text = text;
        }

        public override bool Equals(object obj)
        {
            return obj is EditorLanguageOptionItem item &&
                   Id == item.Id;
        }

        public override int GetHashCode()
        {
            return 2108858624 + EqualityComparer<string>.Default.GetHashCode(Id);
        }
    }
}
