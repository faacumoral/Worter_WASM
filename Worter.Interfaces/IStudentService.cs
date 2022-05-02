using FMCW.Common.Results;

namespace Worter.Interfaces
{
    public interface IStudentService 
    {
        IntResult LoginStudent(string username, string password);
    }
}
