using AutoMapper;
using ClaimAuthorizationApi.Model.Models;
using ClaimAuthorizationApi.Model.ViewModels.Login;
using ClaimAuthorizationApi.Model.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClaimAuthorizationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _mapper = mapper;   
        }

        // GET: api/<UserController>
        [HttpGet("GetUsers")]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            IEnumerable<UserViewModel> users = _mapper.Map<IEnumerable<UserViewModel>>(await _userManager.Users.ToListAsync());
            return users;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost("RegisterUser")]
        public async Task<ActionResult<UpsertUserViewModel>> RegisterUser([FromBody] UpsertUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = _mapper.Map<User>(model);
                user.UserName = model.Email;
                user.CreatedTime = DateTime.UtcNow;
                user.LastModifiedTime = DateTime.UtcNow;

                Task<IdentityResult> result = _userManager.CreateAsync(user, model.Password);
                model = _mapper.Map<UpsertUserViewModel>(user);

                if (result.Result.Succeeded)
                    return Ok(model);
                else
                    return BadRequest(result.Result.Errors.Select(s=> s.Description).ToArray());
            }

            return BadRequest(ModelState); 
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // POST api/<UserController>
        [HttpPost("Login")]
        public async Task<ActionResult<LoginViewModel>> Login([FromBody] LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                    return Ok(model);

                return BadRequest("Email and Password can not match, try again.");
            }

            return BadRequest(ModelState);
        }
    }
}
