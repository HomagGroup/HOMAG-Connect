using System;
using System.Collections.Generic;

namespace HomagConnect.Base.Exceptions
{
    [Serializable]
    public class ProblemDetails
    {
        public string Detail { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }

        public int Status { get; set; }

        public string Title { get; set; }

        public string TraceId { get; set; }

        public string Type { get; set; }
    }
}