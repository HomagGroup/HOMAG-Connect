using System;

namespace HomagConnect.Base.Exceptions
{
    /// <summary>
    /// This exception is forwarded to the calling system and is then displayed to the customer in an error page
    /// </summary>
    [Serializable]
    public class ProblemException : Exception
    {
        /// <summary />
        public ProblemException(string title, string subTitle, string errorCode)
            : this(title, subTitle, errorCode, title, null) { }

        /// <summary />
        public ProblemException(string title, string subTitle, string errorCode, string message)
            : this(title, subTitle, errorCode, message, null) { }

        /// <summary />
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
        /// Sub title
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// Title of the error
        /// </summary>
        public string Title { get; set; }
    }
}