using AutoMapper;
using ClaimAuthorizationApi.Model.Models;
using ClaimAuthorizationApi.Model.ResponseModel;
using ClaimAuthorizationApi.Model.ViewModels.Login;
using ClaimAuthorizationApi.Model.ViewModels.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

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
        private readonly JwtConfig _jwtConfig;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper,
                             IOptions<JwtConfig> jwtConfig)
        {
            _userManager = userManager;
            _signInManager = signInManager; 
            _mapper = mapper;
            _jwtConfig = jwtConfig.Value;
        }

        // GET: api/<UserController>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public ActionResult<UpsertUserViewModel> RegisterUser([FromBody] UpsertUserViewModel model)
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
                    return Ok(new ResponseStatusModel(ResponseCode.Ok, "User has been registered successfull.", model));
                else
                    return BadRequest(new ResponseStatusModel(ResponseCode.Error, "User registration failed.", result.Result.Errors.Select(s => s.Description).ToArray()));
            }

            return BadRequest(new ResponseStatusModel(ResponseCode.FormValidateError, "Regsitration form validate error.", ModelState)); 
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
        public async Task<ActionResult<UserViewModel>> Login([FromBody] LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if (result.Succeeded)
                {
                    UserViewModel existUser = _mapper.Map<UserViewModel>(await _userManager.FindByEmailAsync(model.Email));
                    existUser.Token = GenerateToken(existUser);
                    return Ok(new ResponseStatusModel(ResponseCode.Ok, "Login successfull", existUser));
                }

                return BadRequest(new ResponseStatusModel(ResponseCode.FormValidateError, "Email and Password can not match, try again.", null));
            }

            return BadRequest(new ResponseStatusModel(ResponseCode.FormValidateError, "Regsitration form validate error.", ModelState));
        }

        private string GenerateToken(UserViewModel user)
        {
            JwtSecurityTokenHandler jwtTokenHendler = new JwtSecurityTokenHandler();
            byte[] key = System.Text.Encoding.ASCII.GetBytes(_jwtConfig.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),

                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience
            };

            var token = jwtTokenHendler.CreateToken(tokenDescriptor);
            return jwtTokenHendler.WriteToken(token);
        }
    }
}
