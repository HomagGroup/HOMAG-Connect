namespace HomagConnect.IntelliDivide.Contracts.Request
{
    public enum OptimizationRequestAction
    {
        /// <summary>
        /// Creates a new optimization in intelliDivide using the provided data.
        /// </summary>
        ImportOnly,

        /// <summary>
        /// Creates a new optimization in intelliDivide using the provided data and executes a optimization just for estimation. A
        /// reduced number of optimizations is performed. No export data is generated.
        /// </summary>
        Estimate,

        /// <summary>
        /// Creates a new optimization in intelliDivide using the provided data and executes the optimization.
        /// </summary>
        Optimize,

        /// <summary>
        /// Creates a new optimization in intelliDivide using the provided data, executes the optimization and sends the balanced
        /// solution to the machine.
        /// </summary>
        Send
    }
}