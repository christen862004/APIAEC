using APIAEC.Models;

namespace APIAEC.DTO
{
    public class DeptWithEmpsDto
    {
        public int DeptId { get; set; }
        public string DeptName { get; set; }
        public List<string> Employees { get; set; }
        
    }
    public class EmpDto
    {
        public int Id { get; set; }
        public string Name { get; set; }//Property json
    }
}
