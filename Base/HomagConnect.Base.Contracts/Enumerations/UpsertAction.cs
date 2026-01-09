namespace HomagConnect.Base.Contracts.Enumerations
{
    /// <summary>
    /// Specifies the action performed during an upsert operation.
    /// </summary>
    public enum UpsertAction
    {
        /// <summary>
        /// Indicates that a new object was created.
        /// </summary>
        Created,

        /// <summary>
        /// Indicates that an existing object was updated.
        /// </summary>
        Updated
    }
}