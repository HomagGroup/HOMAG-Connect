using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders {
  /// <summary>
  /// Holds the overview per month of the usage of the licenses
  /// </summary>
  public class UsageOverview
  {
      /// <summary>
      /// The month of the overview
      /// </summary>
      [JsonProperty(Order = 1)]
      public DateTime Period { get; set; }
  
      /// <summary>
      /// The licenses bought by the customer for the current period
      /// </summary>
      [JsonProperty(Order = 2)]
      public Collection<License> Licenses { get; set; }
  
      /// <summary>
      /// The quantity of parts that can be released for the current period
      /// </summary>
      [JsonProperty(Order = 3)]
      public int QuantityOfPartsCoveredByTheLicense { get; set; }
  
      /// <summary>
      /// The quantity of parts that have been released for the current period
      /// </summary>
      [JsonProperty(Order = 4)]
      public int QuantityOfReleasedParts { get; set; }
}
