using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FirebaseAdmin.Auth;
using Hangfire;
using iVertion.Application.DTOs;
using iVertion.Application.Interfaces;
using iVertion.Domain.Account;
using iVertion.Domain.FiltersDb;
using iVertion.Infra.Data.Identity;
using iVertion.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace iVertion.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IGoogleAuthService<GoogleUserInfo> _googleAuthService;
        private readonly IUserInterface<ApplicationUser> _userService;
        private readonly IAuthenticate _authentication;
        private readonly IUserProfileService _userProfileService;
        private readonly IRoleProfileService _roleProfileService;
        private readonly IAdditionalUserRoleService _additionalUserRoleService;
        private readonly ITemporaryUserRoleService _temporaryUserRoleService;
        private readonly IPersonService _personService;
        private readonly IConfiguration _configuration;

        public AccountController(IGoogleAuthService<GoogleUserInfo> googleAuthService,
                                IUserInterface<ApplicationUser> userService,
                                IAuthenticate authentication,
                               IUserProfileService userProfileService,
                               IRoleProfileService roleProfileService,
                               IAdditionalUserRoleService additionalUserRoleService,
                               ITemporaryUserRoleService temporaryUserRoleService,
                               IPersonService personService,
                               IConfiguration configuration)
        {
            _googleAuthService = googleAuthService;
            _userService = userService;
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
            _userProfileService = userProfileService ??
                throw new ArgumentNullException(nameof(userProfileService));
            _roleProfileService = roleProfileService ??
                throw new ArgumentNullException(nameof(roleProfileService));
            _additionalUserRoleService = additionalUserRoleService ??
                throw new ArgumentNullException(nameof(additionalUserRoleService));
            _temporaryUserRoleService = temporaryUserRoleService ??
                throw new ArgumentNullException(nameof(temporaryUserRoleService));
            _personService = personService ??
                throw new ArgumentNullException(nameof(personService));
            _configuration = configuration;
        }

        [HttpPost]
        [Route("google-login")]
        public async Task<ActionResult> GoogleLogin([FromBody] string idToken)
        {
            try
            {
            FirebaseToken decodedToken = await FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string uid = decodedToken.Uid;

            ApplicationUser user = await _userService.FindOrCreateUserAsync(decodedToken.Claims["email"].ToString());

            if (user.IsEnabled)
            {
                UserToken token = await GenerateTokenAsync(decodedToken.Claims["email"].ToString());
                return Ok(token);

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt.");
                return BadRequest(ModelState);
            }
            } catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        private async Task<List<string>> GetAllUserRolesAsync(int userProfileId, string targetUserId){
            var roleProfileFilterdb = new RoleProfileFilterDb(){
                UserProfileId = userProfileId,
                PageSize = 10000, 
                OrderByProperty = "Id", 
                Page=1, 
                Role= null, 
                UserId=null
            };
            var rolesProfiles = await _roleProfileService.GetRoleProfilesAsync(roleProfileFilterdb);
            var additionalUserRolesFilterDb = new AdditionalUserRoleFilterDb(){
                TargetUserId = targetUserId,
                PageSize = 10000, 
                OrderByProperty = "Id", 
                Page=1, 
                Role=null, 
                UserId=null
            };
            var additionalUserRoles = await _additionalUserRoleService.GetAdditionalUserRolesAsync(additionalUserRolesFilterDb);
            var temporaryUserRoleFilterDb = new TemporaryUserRoleFilterDb(){
                TargetUserId = targetUserId,
                PageSize = 10000, 
                OrderByProperty = "Id", 
                Page=1, 
                Role=null, 
                UserId=null,
                StartDate=DateTime.Now,
                ExpirationDate=DateTime.Now
            };
            
            var temporaryUserRoles = await _temporaryUserRoleService.GetTemporaryUserRolesAsync(temporaryUserRoleFilterDb);
            var roleModel = new List<string>();
            foreach(var role in rolesProfiles.Data.Data){
                roleModel.Add(role.Role);
            }
            foreach(var role in additionalUserRoles.Data.Data){
                roleModel.Add(role.Role);
            }
            foreach(var role in temporaryUserRoles.Data.Data){
                roleModel.Add(role.Role);
            }
            return roleModel;
            
        }

        private async Task<bool> UpdateUserRolesAsync(IList<string> oldRoles, List<string> newRoles, ApplicationUser user){
            foreach(var role in oldRoles){
                if (!newRoles.Contains(role)){
                    await _userService.RemoveFromRoleAsync(user, role);
                }
                
            }
            foreach (var role in newRoles){
                if (!oldRoles.Contains(role)){
                    await _userService.AddUserToRoleAsync(user, role);
                }
            }
            return true;
        }
        private async Task<UserToken> GenerateTokenAsync(string email)
        {
            var user = await _userService.GetUserByNameAsync(email);
            var newRoles = await GetAllUserRolesAsync(user.UserProfileId, user.Id);
            var oldRoles = await _userService.GetUserRolesAsync(email);

            await UpdateUserRolesAsync(oldRoles, newRoles, user);
        
            var roles = await _userService.GetUserRolesAsync(email);

            try
            {
                Console.WriteLine($"Email: {email}");
                var claims = new List<Claim>
                {
                    new Claim("email", email),
                    new Claim("UId", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                foreach (var role in roles) 
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                
                // Generate the Private Key
                var privateKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])
                );
                // Generate the Digital Signature
                var credentials = new SigningCredentials(
                    privateKey, SecurityAlgorithms.HmacSha256
                );
                // Set Expiration Time
                _ = DateTime.UtcNow;
                DateTime expiration;
                if (roles.Contains("TotemPanel") && roles.Count() == 1)
                {
                    expiration = DateTime.UtcNow.AddYears(1);
                }
                else
                {
                    expiration = DateTime.UtcNow.AddMinutes(120);
                }
                // Generate the Token
                JwtSecurityToken token = new(
                    // issuer
                    issuer: _configuration["Jwt:Issuer"],
                    // audience
                    audience: _configuration["Jwt:Audience"],
                    // claims
                    claims: claims,
                    // Expiration Time
                    expires: expiration,
                    // Digital Signature
                    signingCredentials: credentials
                );
                return new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };
            } catch (Exception e) {
                Console.WriteLine(e);
                throw new Exception();

            }
            

        }
    }
}