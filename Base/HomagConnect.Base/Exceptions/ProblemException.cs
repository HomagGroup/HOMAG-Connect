using System;
using System.Runtime.Serialization;

namespace HomagConnect.Base.Exceptions
{
    /// <summary>
    /// This exception is forwarded to the calling system and is then displayed to the customer in an error page
    /// </summary>
    [Serializable]
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
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ProblemException(SerializationInfo info, StreamingContext context) : base(info, context) { }

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

        /// <summary>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(SubTitle), SubTitle);
            info.AddValue(nameof(ErrorCode), ErrorCode);

            base.GetObjectData(info, context);
        }
    }
}