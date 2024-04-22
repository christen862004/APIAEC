using System.ComponentModel.DataAnnotations;

namespace APIAEC.Models
{
    public class Department
    {
        public int Id { get; set; }
        [MaxLength(20)]
        [MinLength(3)]
        public string Name { get; set; }

        public string  ManagerName { get; set; }

        public List<Employee>? Employees { get; set; }//Cycle Emp<==>dept
    }
}
