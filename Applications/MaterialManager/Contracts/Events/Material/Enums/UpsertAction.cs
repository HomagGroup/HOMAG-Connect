namespace HomagConnect.MaterialManager.Contracts.Events.Material.Enums
{
    /// <summary>
    /// Specifies the action performed during an upsert operation.
    /// </summary>
    public enum UpsertAction
    {
        /// <summary>
        /// Indicates that a new material was created.
        /// </summary>
        Created,

        /// <summary>
        /// Indicates that an existing material was updated.
        /// </summary>
        Updated
    }
}