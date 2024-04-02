using System;

namespace HomagConnect.Base.Exceptions
{
    /// <summary>
    /// This exception is forwarded to the calling system and is then displayed to the customer in an error page
    /// </summary>
    public class ProblemException : Exception
    {
        /// <summary>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="errorCode"></param>
        public ProblemException(string title, string subTitle, string errorCode)
            : this(title, subTitle, errorCode, title, null) { }

        /// <summary>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="errorCode"></param>
        /// <param name="message">THe message for the exception</param>
        public ProblemException(string title, string subTitle, string errorCode, string message)
            : this(title, subTitle, errorCode, message, null) { }

        /// <summary>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="subTitle"></param>
        /// <param name="errorCode"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ProblemException(string title, string subTitle, string errorCode, string message, Exception innerException)
            : base(message, innerException)
        {
            Title = title;
            SubTitle = subTitle;
            ErrorCode = errorCode;
        }

        /// <summary>
        /// Localized title of the error
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Localized sub title
        /// </summary>
        public string SubTitle { get; set; }

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

       
    }
}