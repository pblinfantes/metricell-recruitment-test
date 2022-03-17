using InterviewTest.Model;

namespace InterviewTest.Commons.Requests
{
    public class UpdateEmployeeRequest
    {

        public Employee OriginalEmployee { get; set; }

        public Employee UpdatedEmployee { get; set; }

    }
}
