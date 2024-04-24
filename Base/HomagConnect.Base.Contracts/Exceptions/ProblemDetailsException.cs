namespace HomagConnect.Base.Contracts.Exceptions
{
    /// <summary>
    /// Possible exception that can be thrown by the API.
    /// </summary>
    public class ProblemDetailsException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="ProblemDetailsException" />.
        /// </summary>
        public ProblemDetailsException() { }

        /// <summary>
        /// Creates a new instance of <see cref="ProblemDetailsException" />.
        /// </summary>
        public ProblemDetailsException(ProblemDetails problemDetails) : base(problemDetails.Detail)
        {
            Type = problemDetails.Type;
            Title = problemDetails.Title;
            Status = problemDetails.Status;
            TraceId = problemDetails.TraceId;
            Detail = problemDetails.Detail;
            Errors = problemDetails.Errors;
        }

        /// <summary>
        /// Creates a new instance of <see cref="ProblemDetailsException" />.
        /// </summary>
        public ProblemDetailsException(string message) : base(message) { }

        /// <summary>
        /// Creates a new instance of <see cref="ProblemDetailsException" />.
        /// </summary>
        public ProblemDetailsException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Gets the detail of the exception.
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Gets the errors of the exception.
        /// </summary>
        public Dictionary<string, string[]> Errors { get; set; }

        /// <summary>
        /// Gets the status of the exception.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets the title of the exception.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the trace id of the exception.
        /// </summary>
        public string TraceId { get; set; }

        /// <summary>
        /// </summary>
        public string Type { get; set; }
    }
}