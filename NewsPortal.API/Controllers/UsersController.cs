using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsPortal.Application.Helpers;
using NewsPortal.Application.Services;
using NewsPortal.Domain.Entities;
using NewsPortal.Domain.ViewModel;
using Newtonsoft.Json.Linq;
using System.Diagnostics.Metrics;
using System.Net.Sockets;
using static NewsPortal.Application.Helpers.GlobalEnums;

namespace NewsPortal.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly EncryptionHelper _encryptionHelper;
        
        public UsersController(UserService userService,JwtTokenGenerator jwtTokenGenerator, EncryptionHelper encryptionHelper) 
        {
            _userService = userService;
            _tokenGenerator = jwtTokenGenerator;
            _encryptionHelper = encryptionHelper;
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

       public  class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int DeptId { get; set; }
            public double Salary { get; set; }
        }
        class Department //test
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [AllowAnonymous]
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "Ravi", DeptId = 1, Salary = 30000 },
                new Employee { Id = 2, Name = "Anu", DeptId = 2, Salary = 60000 },
                new Employee { Id = 3, Name = "Kumar", DeptId = 1, Salary = 80000 },
                new Employee { Id = 4, Name = "Meena", DeptId = 3, Salary = 40000 }
            };

            var result = employees.Where(x => x.Salary > 50000);
            var highpaid = employees.OrderByDescending(x => x.Salary).Take(3);
            bool hr = employees.Any(x => x.Id == 2);
            var tot = employees.GroupBy(x => x.Id).Select(q => new
            {
                Id = q.Key,
                TotalSal = q.Count(),
            });


            //var nameresult = employees.Select(x => x.Name).ToList();

            var res = employees.Max(x => x.Salary);
            var res1 = employees.OrderByDescending(x => x.Salary).FirstOrDefault();
            
           
            var page = employees.OrderBy(x => x.Id).Take(10).Skip(0);
            var dublicatename = employees.GroupBy(a => a.Name).Select(x => x.FirstOrDefault());
                 
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
       
        [AllowAnonymous]
        [HttpPost("CreateNewUser")]
        public async Task<IActionResult> CreateNewUser([FromBody] User user)
        {
            var Existuser = await _userService.UserValidateByEmail(user.Email);

            if (Existuser == null) 
            {
                user.Password = _encryptionHelper.Encrypt(user.Password);
                user.IsActive = true;
                await _userService.AddUserAsync(user);
                return CreatedAtAction(nameof(GetUser), new { Id = user.UserId }, user);
            } else
            {
                return BadRequest(new { Message = "The email ID you entered is already registered. Please try a different one." });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<Response<object>>> LoginUser([FromBody] LoginUser LoginObj)
        {
            User user = new User
            {
                Email = LoginObj.Email,
                UserType = LoginObj.UserType,
                Password = LoginObj.Password
                //Password = _encryptionHelper.Encrypt(LoginObj.Password)
            };

            User objUser = await _userService.LoginUser(user);

            if (objUser != null)
            {
                var role = objUser.UserType == 1 ? "Admin" : "";
                var token = _tokenGenerator.GenerateToken(user.Email, role);
                var results = new
                {
                    Token = token,
                    User = objUser
                };
                return Ok(Response<object>.Success(results));
            }

            return Unauthorized(Response<object>.Failure(new List<string> { "Invalid credentials" }));
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword([FromBody] LoginUser LoginObj)
        {
            User user = new User
            {
                Email = LoginObj.Email,
                UserType = LoginObj.UserType,
                Password = _encryptionHelper.Encrypt(LoginObj.Password)
            };

            User objUser = await _userService.LoginUser(user);

            if (objUser != null)
            {
                //ToDo mail and Message
                return Ok(new { IsSuccess = true, Message = "Password send successfully in your email" });
            }
            return BadRequest(new { Message = "Invalid login credentials." });
        }

        //[HttpGet("test-error")]
        //[AllowAnonymous]
        //public IActionResult TestError()
        //{
        //    throw new Exception("This is a test exception.");
        //}

    }
}
