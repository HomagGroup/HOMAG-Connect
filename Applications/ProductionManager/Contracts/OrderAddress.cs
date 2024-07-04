using System.Runtime.Serialization;

namespace HomagConnect.ProductionManager.Contracts;

public class Address: IExtensibleDataObject
{
    #region IExtensibleDataObject Members

    public ExtensionDataObject? ExtensionData { get; set; }

    #endregion

    public string? Street { get; set; }
    public string? HouseNumber { get; set; }
    public string? Town { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
}