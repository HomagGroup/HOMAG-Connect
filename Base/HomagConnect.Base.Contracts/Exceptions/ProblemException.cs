namespace HomagConnect.Base.Contracts.Exceptions
{
    /// <summary>
    /// This exception is forwarded to the calling system and is then displayed to the customer in an error page
    /// </summary>
    public class ProblemException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="ProblemException" />.
        /// </summary>
        public ProblemException(string title, string subTitle, string errorCode)
            : this(title, subTitle, errorCode, title, null) { }

        /// <summary>
        /// Creates a new instance of <see cref="ProblemException" />.
        /// </summary>
        public ProblemException(string title, string subTitle, string errorCode, string message)
            : this(title, subTitle, errorCode, message, null) { }

        /// <summary>
        /// Creates a new instance of <see cref="ProblemException" />.
        /// </summary>
        public ProblemException(string title, string subTitle, string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            Title = title;
            SubTitle = subTitle;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Code to identify the error
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// The details type for the ProblemDetails
        /// </summary>
        public string ProblemDetailsType => ProblemDetailsTypePrefix + ErrorCode;

        /// <summary>
        /// And prefix for the details type
        /// </summary>
        public virtual string ProblemDetailsTypePrefix { get; } = "hgconnect:";

        /// <summary>
        /// Localized sub title
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Localized title of the error
        /// </summary>
        public string Title { get; set; }
    }
}