namespace HomagConnect.MaterialAssist.Contracts.Update
{
    public class MaterialAssistUpdateBoardEntity
    {
        public string? Comments { get; set; }

        public string? ExternalId { get; set; }

        /// <summary>
        /// Only available for single boards
        /// </summary>
        public double? Length { get; set; }

        /// <summary>
        /// Only available for stacks or goods in stock
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Only available for single boards
        /// </summary>
        public double? Width { get; set; }
    }
}