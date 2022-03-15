using System;

namespace InterviewTest.Commons.Results
{
    public class ProcessResult
    {

        public bool Success { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public ProcessResult InnerProcessResult { get; set; }

        public ProcessResult(bool success, string message, Exception exception = null, ProcessResult innerProcessResult = null)
        {
            Success = success;
            Message = message;
            Exception = exception;
            Success = success;
            InnerProcessResult = innerProcessResult;
        }
    }
}
