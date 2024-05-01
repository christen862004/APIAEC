
using APIAEC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APIAEC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                    options.SuppressModelStateInvalidFilter = true); 
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<ITIContext>(options =>
                options.UseSqlServer("Data Source=.;Initial Catalog=ITI_WebAPI_AEC44;Integrated Security=True;Encrypt=False")
            );

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
                AddEntityFrameworkStores<ITIContext>();

            builder.Services.AddCors(options => {
                options.AddPolicy("MyPolicy", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();//extension wwwroot
            
            app.UseCors("MyPolicy");//custim
            
            app.UseAuthorization();

            app.MapControllers();//controller ==> Route []

            app.Run();
        }
    }
}
