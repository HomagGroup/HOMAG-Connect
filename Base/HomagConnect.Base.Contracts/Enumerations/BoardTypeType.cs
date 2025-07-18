using HomagConnect.Base.Contracts.Attributes;
using HomagConnect.Base.Contracts.Converter;

using Newtonsoft.Json;

namespace HomagConnect.Base.Contracts.Enumerations
{
    /// <summary>
    /// The type of the board type.
    /// </summary>
    [ResourceManager(typeof(BoardTypeTypeDisplayNames))]
    [JsonConverter(typeof(TolerantEnumConverter))]
    public enum BoardTypeType
    {
        /// <summary>
        /// The board is a whole board.
        /// </summary>
        Board,

        /// <summary>
        /// The board is an offcut.
        /// </summary>
        Offcut,

        /// <summary>
        /// The board is a Template.
        /// </summary>
        Template
    }
}