using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HomagConnect.Base.Exceptions
{
    [Serializable]
    public class ProblemDetailsException : Exception
    {
        public ProblemDetailsException() { }

        public ProblemDetailsException(ProblemDetails problemDetails)
        {
            Type = problemDetails.Type;
            Title = problemDetails.Title;
            Status = problemDetails.Status;
            TraceId = problemDetails.TraceId;
            Detail = problemDetails.Detail;
            Errors = problemDetails.Errors;
        }

        protected ProblemDetailsException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public ProblemDetailsException(string message) : base(message) { }

        public ProblemDetailsException(string message, Exception innerException) : base(message, innerException) { }

        public string Detail { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }

        public string TraceId { get; set; }

        public string Type { get; set; }
    }
}