namespace HomagConnect.MaterialManager.Contracts.Material.Base
{
    /// <summary>
    /// Image types
    /// </summary>
    public enum ImageType
    {
        /// <summary>
        /// The main picture.
        /// </summary>
        Picture,

        /// <summary>
        /// A technical drawing.
        /// </summary>
        Drawing,

        /// <summary>
        /// A technical picture used for measurement that also shows identifier for measurement values.
        /// </summary>
        Measurement,

        /// <summary>
        /// A specification sheet usually taken by the user from the manufacturer and uploaded
        /// </summary>
        SpecSheet,

        /// <summary>
        /// A pdf containing a label used for printing
        /// </summary>
        Label,

        /// <summary>
        /// Image used for 3D rendering of f.e. cabinets
        /// </summary>
        Texture
    }
}