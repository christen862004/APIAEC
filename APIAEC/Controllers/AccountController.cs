using APIAEC.DTO;
using APIAEC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace APIAEC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        [HttpPost("register")]
        //api/Account/Register:post body
        public async Task<IActionResult> register(RegisterUserDTO userFromReq)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser userModel = new ApplicationUser() { 
                    UserName=userFromReq.UserName,
                };

               IdentityResult result=  await userManager.CreateAsync(userModel, userFromReq.Passwrod);
                if(result.Succeeded)
                {
                    //await userManager.AddToRoleAsync(userModel, "Admin");
                    return Ok("User Create Success");
                }
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]//username ,passwor
        public async Task<IActionResult> Login(LoginUserDto userFromReq)
        {
            if (ModelState.IsValid)
            {
                 ApplicationUser userModel=
                    await userManager.FindByNameAsync(userFromReq.UserName);
                if (userModel!=null)
                {
                    //check password
                    bool found=await userManager.CheckPasswordAsync(userModel, userFromReq.Password);
                    if (found == true)
                    {
                        List<Claim> myclaim = new List<Claim>();
                        
                        myclaim.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id.ToString()));
                        myclaim.Add(new Claim(ClaimTypes.Name, userModel.UserName));
                        myclaim.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));

                        var roles=await userManager.GetRolesAsync(userModel);
                        if (roles != null)
                        {
                            foreach (var role in roles)
                            {
                                myclaim.Add(new Claim(ClaimTypes.Role, role));
                            }
                        }
                        SymmetricSecurityKey securityKey = 
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes("asdqeqeoi989865hhvbvfdf"));

                        var mysigningCredentials = 
                            new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        //create toke
                        var mytoken = new JwtSecurityToken(
                            issuer: "http://localhost:5091/",
                            audience: "http://localhost:4200/",
                            claims: myclaim,
                            expires:DateTime.Now.AddHours(3),
                            signingCredentials: mysigningCredentials

                            );
                        //respose token to client
                        return Ok(new
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration=mytoken.ValidTo
                        });
                    }
                }
            }
            return Unauthorized("Invalid Account");
        }
    }
}
