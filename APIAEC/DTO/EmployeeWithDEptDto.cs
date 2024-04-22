using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace APIAEC.DTO
{
    public class EmployeeWithDEptDto
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public string DeptName { get; set; }
    }
}
