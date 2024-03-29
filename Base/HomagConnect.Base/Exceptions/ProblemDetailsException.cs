using System;
using System.Collections.Generic;

namespace HomagConnect.Base.Exceptions
{
    public class ProblemDetailsException : Exception
    {
#pragma warning disable S4004 // Collection properties should be readonly
        public ProblemDetailsException() { }

        public ProblemDetailsException(ProblemDetails problemDetails) : base(problemDetails.Detail)
        {
            Type = problemDetails.Type;
            Title = problemDetails.Title;
            Status = problemDetails.Status;
            TraceId = problemDetails.TraceId;
            Detail = problemDetails.Detail;
            Errors = problemDetails.Errors;
        }

        public ProblemDetailsException(string message) : base(message) { }

        public ProblemDetailsException(string message, Exception innerException) : base(message, innerException) { }

        public string Detail { get; set; }
        
        public Dictionary<string, string[]> Errors { get; set; }


        public int Status { get; set; }

        public string Title { get; set; }

        public string TraceId { get; set; }

        public string Type { get; set; }
    }

#pragma warning restore S4004 // Collection properties should be readonly
}