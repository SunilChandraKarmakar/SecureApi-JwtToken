using AutoMapper;
using ClaimAuthorizationApi.Model.Models;
using ClaimAuthorizationApi.Model.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
                ClaimAuthorizationApi.Model.Models.User user = _mapper.Map<User>(model);
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
    }
}
