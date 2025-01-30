using HomagConnect.Base.Contracts.Converter;
using Newtonsoft.Json;

namespace HomagConnect.IntelliDivide.Contracts.Usage
{
    /// <summary>
    /// Contains the type of the license
    /// </summary>
	[JsonConverter(typeof(TolerantEnumConverter))]
    public enum LicenseType
    {
		/// <summary>
		/// License of type Cutting
		/// </summary>
		Cutting,

		/// <summary>
		/// License of type Nesting
		/// </summary>
		Nesting
    }
}
