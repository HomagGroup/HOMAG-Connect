using Newtonsoft.Json;

namespace HomagConnect.ProductionManager.Contracts.Orders 
{
  /// <summary>
  /// Describes the overview per period of the usage of the owned licenses 
  /// </summary>
  public class UsageOverview
  {
      /// <summary>
      /// The month during which the licenses are active
      /// </summary>
      [JsonProperty(Order = 1)]
      public DateTime Period { get; set; }
  
      /// <summary>
      /// The licenses owned for the current period
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
