namespace HomagConnect.ProductionManager.Contracts
{
    /// <summary>
    /// Relevant Info to produce a part on a workplace of type edgeband
    /// Hint: will be replaced by a more generic productionEntity Model
    /// </summary>
    public class EdgebandPredictPart
    {
        /// <summary>
        /// 
        /// </summary>
        public string? Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? EdgeFront { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? EdgeBack { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? EdgeLeft { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? EdgeRight { get; set; }
    }
}
