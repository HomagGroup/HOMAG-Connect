namespace HomagConnect.MaterialManager.Contracts.Request
{
    /// <summary>
    /// Filter payload for storage requests.
    /// </summary>
    public class StorageImportFilter
    {
        /// <summary>
        /// Filter by board type codes.
        /// </summary>
        public string[] BoardTypeCodes { get; set; } = [];

        /// <summary>
        /// Name of the storage System.
        /// </summary>
        public string StorageSystemName { get; set; } = string.Empty;
    }
}