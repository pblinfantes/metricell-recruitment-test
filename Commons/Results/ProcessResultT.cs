using System;

namespace InterviewTest.Commons.Results
{
    public class ProcessResult<T> : ProcessResult
    {

        public T Result { get; set; }

        public ProcessResult(bool success, string message, T result, Exception exception = null, ProcessResult innerProcessResult = null) : base(success, message, exception, innerProcessResult)
        {
            Result = result;
        }
    }
}
