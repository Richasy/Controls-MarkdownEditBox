using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MarkdownEditBox.Enums
{
    /// <summary>
    /// Mini map location
    /// </summary>
    public enum MinimapSideMode
    {
        /// <summary>
        /// On left
        /// </summary>
        [EnumMember(Value = "left")]
        Left,

        /// <summary>
        /// On right
        /// </summary>
        [EnumMember(Value = "right")]
        Right
    }
}
