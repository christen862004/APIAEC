using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIAEC.Models
{
    public class ITIContext:IdentityDbContext<ApplicationUser>
    {
        public DbSet<Department>    Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        public ITIContext(DbContextOptions<ITIContext> options):base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>().HasData(new Department() { Id=1,Name="SD",ManagerName="Ahmed"});
            modelBuilder.Entity<Department>().HasData(new Department() { Id = 2, Name = "OS", ManagerName = "Ahmed" });

            modelBuilder.Entity<Employee>().HasData(
                new Employee() { Id = 1, Name = "Ahmed",Salary=10000, Address="Alex",DepartmentId=1 });

            modelBuilder.Entity<Employee>().HasData(
               new Employee() { Id = 2, Name = "Safa", Salary = 10000, Address = "Alex", DepartmentId = 2 });
            modelBuilder.Entity<Employee>().HasData(
               new Employee() { Id = 3, Name = "Sara", Salary = 10000, Address = "Alex", DepartmentId = 2 });


            base.OnModelCreating(modelBuilder);
        }
    }
}
