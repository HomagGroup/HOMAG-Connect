using HomagConnect.ProductionManager.Contracts;

namespace HomagConnect.ProductionManager.Samples.Lots.Actions
{
    /// <summary>
    /// Sample class which shows how to delete or decompose lots.
    /// </summary>
    public static class DeleteOrDecomposeLotSamples
    {
        /// <summary>
        /// DeleteOrDecomposeLotByLotId Sample
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task DeleteOrDecomposeLotByLotIdSample(IProductionManagerClient productionManager)
        {
            var lotId = Guid.Parse("PUT_LOT_ID_HERE");

            await productionManager.DeleteOrDecomposeLotByLotId(lotId);
        }

        /// <summary>
        /// DeleteOrDecomposeLotsByLotIds Sample
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task DeleteOrDecomposeLotsByLotIdsSample(IProductionManagerClient productionManager)
        {
            var lotId = new[] 
            {
                Guid.Parse("PUT_LOT_ID1_HERE"),
                Guid.Parse("PUT_LOT_ID2_HERE")
            };

            await productionManager.DeleteOrDecomposeLotsByLotIds(lotId);
        }

        /// <summary>
        /// DeleteOrDecomposeLotByLotName Sample
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task DeleteOrDecomposeLotByLotNameSample(IProductionManagerClient productionManager)
        {
            var lotName = "PUT_LOT1_Name_HERE";

            await productionManager.DeleteOrDecomposeLotByLotName(lotName);
        }

        /// <summary>
        /// DeleteOrDecomposeLotByLotNames Sample
        /// </summary>
        /// <param name="productionManager"></param>
        public static async Task DeleteOrDecomposeLotByLotNamesSample(IProductionManagerClient productionManager)
        {
            var lotNames = new[]
            {
                "PUT_LOT1_Name_HERE",
                "PUT_LOT2_Name_HERE"
            };

            await productionManager.DeleteOrDecomposeLotsByLotNames(lotNames);
        }
    }
}
