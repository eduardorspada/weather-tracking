
using iVertion.Domain.Account;
using iVertion.Infra.Data.Identity;
using iVertion.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using iVertion.Application.Interfaces;
using iVertion.Domain.FiltersDb;
using iVertion.Application.DTOs;
using System.Xml.Schema;

namespace iVertion.WebApi.Controllers
{
    /// <summary>
    /// User
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticate _authentication;
        private readonly IRoleInterface<IdentityRole> _roleService;
        private readonly IUserInterface<ApplicationUser> _userService;
        private readonly IUserProfileService _userProfileService;
        private readonly IRoleProfileService _roleProfileService;
        private readonly IAdditionalUserRoleService _additionalUserRoleService;
        private readonly ITemporaryUserRoleService _temporaryUserRoleService;
        private readonly IPersonService _personService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authentication"></param>
        /// <param name="userService"></param>
        /// <param name="roleService"></param>
        /// <param name="userProfileService"></param>
        /// <param name="roleProfileService"></param>
        /// <param name="additionalUserRoleService"></param>
        /// <param name="temporaryUserRoleService"></param>
        /// <param name="personService"></param>
        public UserController(IAuthenticate authentication,
                              IUserInterface<ApplicationUser> userService,
                              IRoleInterface<IdentityRole> roleService,
                              IUserProfileService userProfileService,
                              IRoleProfileService roleProfileService,
                              IAdditionalUserRoleService additionalUserRoleService,
                              ITemporaryUserRoleService temporaryUserRoleService,
                              IPersonService personService
                               )
        {
            _authentication = authentication ??
                throw new ArgumentNullException(nameof(authentication));
            _userService = userService ??
                throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ??
                throw new ArgumentNullException(nameof(roleService));
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
        }
        /// <summary>
        /// Returns a list of users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "GetUsers")]
        public async Task<ActionResult> Get()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }
        /// <summary>
        /// Returns a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "GetUsers")]
        public async Task<ActionResult> GetUserByIdAsync(string id){
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null){
                return NotFound();
            } else {
                return Ok(user);
            }
        }
        /// <summary>
        /// Creates a new user from the "userInfo" properties.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        
        [HttpPost("CreateUser")]
        [Authorize(Roles = "AddUser")]
        public async Task<ActionResult> CreateUser ([FromBody] RegisterModel userInfo)
        {
            var result = await _authentication.RegisterUser(userInfo.Email,
                                                            userInfo.Password,
                                                            userInfo.IsEnabled,
                                                            userInfo.UserProfileId,
                                                            userInfo.PersonId
                                                            );
            if (result)
            {
                return Ok($"User {userInfo.Email} was created successfully.");
            }
            else
            {
                ModelState.AddModelError("error", "We had a problem compiling the data.");
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Retuns user profile information
        /// </summary>
        /// <param name="userProfileFilterDb"></param>
        /// <returns></returns>
        [HttpGet("UsersProfile")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetUserProfileAsync([FromQuery] UserProfileFilterDb userProfileFilterDb){
            var result = await _userProfileService.GetUserProfilesAsync(userProfileFilterDb);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        /// <summary>
        /// Returns a user profile information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("UsersProfile/{id}")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetUserProfileByIdAsync(int id){
            var result = await _userProfileService.GetUserProfileByIdAsync(id);
            if (result.Data == null)
                return NotFound();
            if (result.IsSuccess){
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Returns a roles of user profile by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("RolesUsersProfile/{id}")]
        [Authorize(Roles = "AddToRole")]
        public async Task<List<string>> GetRolesProfileAsync(int id){
            var roleProfileFilterdb = new RoleProfileFilterDb(){
            UserProfileId = id,
            PageSize = 10000, 
            OrderByProperty = "Id", 
            Page=1, 
            Role= null, 
            UserId=null
            };
            var rolesProfiles = await _roleProfileService.GetRoleProfilesAsync(roleProfileFilterdb);
            

            var roleModel = new List<string>();
            foreach(var role in rolesProfiles.Data.Data){
                roleModel.Add(role.Role);
            }
            return roleModel;
            
        }
        /// <summary>
        /// Adds a Role to a User Profile by User Profile Id.
        /// </summary>
        /// <param name="roleFromUserProfileIdModel"></param>
        /// <returns></returns>
        [HttpPost("AddRoleToUserProfile")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> AddRoleToUserProfileAsync([FromBody] RoleFromUserProfileIdModel roleFromUserProfileIdModel) {
            if (!String.IsNullOrEmpty(roleFromUserProfileIdModel.Role) && roleFromUserProfileIdModel.UserProfileId > 0){
                var roleExists = await _roleService.RoleExistsAsync(roleFromUserProfileIdModel.Role);
                if (roleExists){
                    var userProfile = await _userProfileService.GetUserProfileByIdAsync(roleFromUserProfileIdModel.UserProfileId);
                    if (userProfile.Data == null)
                        return NotFound("This Id does not correspond to an existing User Profile.");
                    if (userProfile.IsSuccess){
                        var roleProfileFilterdb = new RoleProfileFilterDb(){
                        UserProfileId = roleFromUserProfileIdModel.UserProfileId,
                        PageSize = 10000, 
                        OrderByProperty = "Id", 
                        Page=1, 
                        Role= roleFromUserProfileIdModel.Role, 
                        UserId=null
                        };
                        var rolesProfiles = await _roleProfileService.GetRoleProfilesAsync(roleProfileFilterdb);
                        

                        var roleModel = new List<string>();
                        var roleProfileId = 0;
                        foreach(var role in rolesProfiles.Data.Data){
                            roleModel.Add(role.Role);
                            roleProfileId = role.Id;
                        }
                        if (!roleModel.Contains(roleFromUserProfileIdModel.Role)){
                            var roleProfileDto = new RoleProfileDTO();
                            var userId = User.FindFirst("UId").Value;
                            var dateNow = DateTime.UtcNow;
                            roleProfileDto.Role = roleFromUserProfileIdModel.Role;
                            roleProfileDto.UserProfileId = roleFromUserProfileIdModel.UserProfileId;
                            roleProfileDto.Active = true;
                            roleProfileDto.UserId = userId;
                            roleProfileDto.CreatedAt = dateNow;
                            roleProfileDto.UpdatedAt = dateNow;
                            await _roleProfileService.CreateRoleProfileAsync(roleProfileDto);
                            return Ok($@"{roleFromUserProfileIdModel.Role} has been successfully added.");
                        }
                        return Conflict($@"{roleFromUserProfileIdModel.Role} already exists in Role Profile");
                    }
                    return BadRequest(userProfile);

                }
                return NotFound(@"The specified role does not exist in the system!");
            }

            
            return BadRequest("Role is not be null or empty and UserProfileId must be greater than zero");
            

        }
        /// <summary>
        /// Removes a Role from a User Profile by the Role name and the User Profile Id.
        /// </summary>
        /// <param name="roleFromUserProfileIdModel"></param>
        /// <returns></returns>
        [HttpDelete("RemoveRoleFromUserProfileId")]
        [Authorize(Roles = "RemoveFromRole")]
        public async Task<ActionResult> RemoveRoleFromUserProfileId([FromBody] RoleFromUserProfileIdModel roleFromUserProfileIdModel){
            if (!String.IsNullOrEmpty(roleFromUserProfileIdModel.Role) && roleFromUserProfileIdModel.UserProfileId > 0){
                var roleExists = await _roleService.RoleExistsAsync(roleFromUserProfileIdModel.Role);
                if (roleExists){
                    var userProfile = await _userProfileService.GetUserProfileByIdAsync(roleFromUserProfileIdModel.UserProfileId);
                    if (userProfile.Data == null)
                        return NotFound("This Id does not correspond to an existing User Profile.");
                    if (userProfile.IsSuccess){
                        var roleProfileFilterdb = new RoleProfileFilterDb(){
                        UserProfileId = roleFromUserProfileIdModel.UserProfileId,
                        PageSize = 10000, 
                        OrderByProperty = "Id", 
                        Page=1, 
                        Role= roleFromUserProfileIdModel.Role, 
                        UserId=null
                        };
                        var rolesProfiles = await _roleProfileService.GetRoleProfilesAsync(roleProfileFilterdb);
                        

                        var roleModel = new List<string>();
                        var roleProfileId = 0;
                        foreach(var role in rolesProfiles.Data.Data){
                            roleModel.Add(role.Role);
                            roleProfileId = role.Id;
                        }
                        if (!roleModel.Contains(roleFromUserProfileIdModel.Role)){
                            return Conflict($@"{roleFromUserProfileIdModel.Role} does not exist in Role Profile");
                        }
                        await _roleProfileService.RemoveRoleProfileAsync(roleProfileId);
                        return Ok($@"{roleFromUserProfileIdModel.Role} has been successfully removed.");
                    }
                    return BadRequest(userProfile);

                }
                return NotFound(@"The specified role does not exist in the system!");
            }

            
            return BadRequest("Role is not be null or empty and UserProfileId must be greater than zero");
        }

        /// <summary>
        /// Retuns role information
        /// </summary>
        /// <param name="roleProfileFilterDb"></param>
        /// <returns></returns>
        [HttpGet("RolesProfile")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetRoleProfileAsync([FromQuery] RoleProfileFilterDb roleProfileFilterDb){
            var result = await _roleProfileService.GetRoleProfilesAsync(roleProfileFilterDb);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        /// <summary>
        /// Returns an array of additional user roles.
        /// </summary>
        /// <param name="additionalUserRoleFilterDb"></param>
        /// <returns></returns>
        [HttpGet("AdditionalUserRole")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetAdditionalUserRolesAsync([FromQuery] AdditionalUserRoleFilterDb additionalUserRoleFilterDb){
            var result = await _additionalUserRoleService.GetAdditionalUserRolesAsync(additionalUserRoleFilterDb);
            if (result.Data == null)
                return NotFound();
            if (result.IsSuccess){
                return Ok(result);
            }
            return BadRequest(result);
        }
        /// <summary>
        /// Returns an additional user role by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("AdditionalUserRole/{id}")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetAdditionalUserRoleByIdAsync(int id){
            var additionalUserRole = await _additionalUserRoleService.GetAdditionalUserRoleByIdAsync(id);
            if (additionalUserRole.Data == null){
                return NotFound($@"Unable to find additional user role with id {id}.");
            } else {
                return Ok(additionalUserRole);
            }
        }
        /// <summary>
        /// Adds an additional role to a user beyond the role profile they belong to.
        /// </summary>
        /// <param name="additionalUserRoleModel"></param>
        /// <returns></returns>
        [HttpPost("AdditionalUserRole")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> AddAddtionalUserRoleAsync([FromBody] AdditionalUserRoleModel additionalUserRoleModel){
            if (!String.IsNullOrEmpty(additionalUserRoleModel.Role)) {
                if (!String.IsNullOrEmpty(additionalUserRoleModel.UserName)){
                    var roleExists = await _roleService.RoleExistsAsync(additionalUserRoleModel.Role);
                    if (roleExists) {
                        var targetUser = await _userService.GetUserByNameAsync(additionalUserRoleModel.UserName);
                        try {
                            var targetUserId = targetUser.Id;
                            var person = await _personService.GetPersonByIdAsync(targetUser.PersonId);
                            var userProfileId = targetUser.UserProfileId;

                            var userProfile = await _userProfileService.GetUserProfileByIdAsync(userProfileId);
                            if (userProfile.Data == null)
                                return NotFound("This Id does not correspond to an existing User Profile.");
                            if (userProfile.IsSuccess){
                                var roleProfileFilterdb = new RoleProfileFilterDb(){
                                UserProfileId = userProfileId,
                                PageSize = 10000, 
                                OrderByProperty = "Id", 
                                Page=1, 
                                Role= additionalUserRoleModel.Role, 
                                UserId=null
                                };
                                var rolesProfiles = await _roleProfileService.GetRoleProfilesAsync(roleProfileFilterdb);
                                

                                var roleModel = new List<string>();
                                var roleProfileId = 0;
                                foreach(var role in rolesProfiles.Data.Data){
                                    roleModel.Add(role.Role);
                                    roleProfileId = role.Id;
                                }
                                if (!roleModel.Contains(additionalUserRoleModel.Role)){
                                    var additionalUserRolesFilterDb = new AdditionalUserRoleFilterDb(){
                                        TargetUserId = targetUserId,
                                        PageSize = 10000, 
                                        OrderByProperty = "Id", 
                                        Page=1, 
                                        Role=additionalUserRoleModel.Role, 
                                        UserId=null
                                        };
                                    var additionalUserRoles = await _additionalUserRoleService.GetAdditionalUserRolesAsync(additionalUserRolesFilterDb);
                                    var additionalRoles = new List<string>();
                                    var additionalUserRoleId = 0;
                                    foreach(var role in additionalUserRoles.Data.Data){
                                        additionalRoles.Add(role.Role);
                                        additionalUserRoleId = role.Id;
                                    }
                                    if (!additionalRoles.Contains(additionalUserRoleModel.Role)){
                                        var addtionalUserRoleDto = new AdditionalUserRoleDTO();
                                        var userId = User.FindFirst("UId").Value;
                                        var dateNow = DateTime.UtcNow;
                                        addtionalUserRoleDto.Role = additionalUserRoleModel.Role;
                                        addtionalUserRoleDto.TargetUserId = targetUserId;
                                        addtionalUserRoleDto.UserId = userId;
                                        addtionalUserRoleDto.Active = true;
                                        addtionalUserRoleDto.CreatedAt = dateNow;
                                        addtionalUserRoleDto.UpdatedAt = dateNow;
                                        
                                        await _additionalUserRoleService.CreateAdditionalUserRoleAsync(addtionalUserRoleDto);
                                        return Ok($@"The {additionalUserRoleModel.Role} has been successfully assigned to the {person.Data.FullName}.");
                                    }
                                    return Conflict($@"The {additionalUserRoleModel.Role} already exists in this {person.Data.FullName}'s additional roles.");

                                }
                                return Conflict($@"The {additionalUserRoleModel.Role} already exists in this {person.Data.FullName}'s role profile.");
                            }
                            return BadRequest(userProfile);
                        } catch {
                            return NotFound($@"The specified user '{additionalUserRoleModel.UserName}', does not exist in the system!");
                        }
                    }
                    return NotFound($@"The specified role '{additionalUserRoleModel.Role}', does not exist in the system!");
                }
                return BadRequest("UserName is not be null or empty");
            }
            return BadRequest("Role is not be null or empty");
        }
        /// <summary>
        /// Remove an additional role to a user beyond the role profile they belong to.
        /// </summary>
        /// <param name="additionalUserRoleModel"></param>
        /// <returns></returns>
        [HttpDelete("AdditionalUserRole")]
        [Authorize(Roles = "RemoveFromRole")]
        public async Task<ActionResult> RemoveAddtionalUserRoleAsync([FromBody] AdditionalUserRoleModel additionalUserRoleModel){
            if (!String.IsNullOrEmpty(additionalUserRoleModel.Role)) {
                if (!String.IsNullOrEmpty(additionalUserRoleModel.UserName)){
                    var roleExists = await _roleService.RoleExistsAsync(additionalUserRoleModel.Role);
                    if (roleExists) {
                        var targetUser = await _userService.GetUserByNameAsync(additionalUserRoleModel.UserName);
                        var person = await _personService.GetPersonByIdAsync(targetUser.PersonId);
                        try {
                            var targetUserId = targetUser.Id;

                            var additionalUserRolesFilterDb = new AdditionalUserRoleFilterDb(){
                                TargetUserId = targetUserId,
                                PageSize = 10000, 
                                OrderByProperty = "Id", 
                                Page=1, 
                                Role=additionalUserRoleModel.Role, 
                                UserId=null
                                };
                            var additionalUserRoles = await _additionalUserRoleService.GetAdditionalUserRolesAsync(additionalUserRolesFilterDb);

                                var roleModel = new List<string>();
                                var additionalUserRoleId = 0;
                                foreach(var role in additionalUserRoles.Data.Data){
                                    roleModel.Add(role.Role);
                                    additionalUserRoleId = role.Id;
                                }
                                if (roleModel.Contains(additionalUserRoleModel.Role)){
                                    await _additionalUserRoleService.RemoveAdditionalUserRoleAsync(additionalUserRoleId);
                                    return Ok($@"The {additionalUserRoleModel.Role} has been successfully removed from the {person.Data.FullName}.");
                                }
                                return Conflict($@"The {additionalUserRoleModel.Role} not exists in this {person.Data.FullName}'s additional roles.");

                        } catch {
                            return NotFound($@"The specified user '{additionalUserRoleModel.UserName}', does not exist in the system!");
                        }
                    }
                    return NotFound($@"The specified role '{additionalUserRoleModel.Role}', does not exist in the system!");
                }
                return BadRequest("UserName is not be null or empty");
            }
            return BadRequest("Role is not be null or empty");
        }
        /// <summary>
        /// Returns an array of temporary user roles.
        /// </summary>
        /// <param name="temporaryUserRoleFilterDb"></param>
        /// <returns></returns>
        [HttpGet("TemporaryUserRole")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetTemporaryUserRolesAsync([FromQuery] TemporaryUserRoleFilterDb temporaryUserRoleFilterDb){
            var result = await _temporaryUserRoleService.GetTemporaryUserRolesAsync(temporaryUserRoleFilterDb);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
        /// <summary>
        /// Returns a temporary user role by its id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("TemporaryUserRole/{id}")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetTemporaryUserRoleByIdAsync(int id){
            var temporaryUserRole = await _temporaryUserRoleService.GetTemporaryUserRoleByIdAsync(id);
            if (temporaryUserRole.Data == null){
                return NotFound($@"Unable to find temporary user role with id {id}.");
            } else {
                return Ok(temporaryUserRole);
            }
        }
        /// <summary>
        /// Adds a new temporary user role.
        /// </summary>
        /// <param name="temporaryUserRoleModel"></param>
        /// <returns></returns>
        [HttpPost("TemporaryUserRole")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> AddTemporaryUserRoleAsync([FromBody] TemporaryUserRoleModel temporaryUserRoleModel)
        {
            if (!String.IsNullOrEmpty(temporaryUserRoleModel.Role)){
                if (!String.IsNullOrEmpty(temporaryUserRoleModel.UserName)){
                    if (temporaryUserRoleModel.StartDate >= DateTime.Now){
                        if (temporaryUserRoleModel.ExpirationDate > temporaryUserRoleModel.StartDate){
                            var roleExists = await _roleService.RoleExistsAsync(temporaryUserRoleModel.Role);
                            if (roleExists){
                                var targetUser = await _userService.GetUserByNameAsync(temporaryUserRoleModel.UserName);
                                try
                                {

                                    var targetUserId = targetUser.Id;
                                    var person = await _personService.GetPersonByIdAsync(targetUser.PersonId);
                                    var userProfileId = targetUser.UserProfileId;
                                    var userProfile = await _userProfileService.GetUserProfileByIdAsync(userProfileId);
                                    if (userProfile.Data == null)
                                        return NotFound("This Id does not correspond to an existing User Profile.");
                                    if (userProfile.IsSuccess){
                                        var roleProfileFilterDb = new RoleProfileFilterDb(){
                                                                        UserProfileId = userProfileId,
                                                                        PageSize = 10000, 
                                                                        OrderByProperty = "Id", 
                                                                        Page=1, 
                                                                        Role= temporaryUserRoleModel.Role, 
                                                                        UserId=null
                                                                        };

                                        var rolesProfiles = await _roleProfileService.GetRoleProfilesAsync(roleProfileFilterDb);
                                        var roleModel = new List<string>();
                                        var roleProfileId = 0;
                                        foreach(var role in rolesProfiles.Data.Data){
                                            roleModel.Add(role.Role);
                                            roleProfileId = role.Id;
                                        }
                                        if (!roleModel.Contains(temporaryUserRoleModel.Role)){
                                            var additionalUserRolesFilterDb = new AdditionalUserRoleFilterDb(){
                                                TargetUserId = targetUserId,
                                                PageSize = 10000, 
                                                OrderByProperty = "Id", 
                                                Page=1, 
                                                Role=temporaryUserRoleModel.Role, 
                                                UserId=null
                                                };
                                            var additionalUserRoles = await _additionalUserRoleService.GetAdditionalUserRolesAsync(additionalUserRolesFilterDb);
                                            var additionalRoles = new List<string>();
                                            var additionalUserRoleId = 0;
                                            foreach(var role in additionalUserRoles.Data.Data){
                                                additionalRoles.Add(role.Role);
                                                additionalUserRoleId = role.Id;
                                            }
                                            if (!additionalRoles.Contains(temporaryUserRoleModel.Role)){

                                                var temporaryUserRoleFilterDb = new TemporaryUserRoleFilterDb(){
                                                    TargetUserId = targetUserId,
                                                    PageSize = 10000, 
                                                    OrderByProperty = "Id", 
                                                    Page=1, 
                                                    Role=temporaryUserRoleModel.Role, 
                                                    UserId=null,
                                                    StartDate=null,
                                                    ExpirationDate=DateTime.Now
                                                };
                                                var temporaryUserRoles = await _temporaryUserRoleService.GetTemporaryUserRolesAsync(temporaryUserRoleFilterDb);
                                                var temporaryRoles = new List<string>();
                                                foreach(var role in temporaryUserRoles.Data.Data){
                                                    temporaryRoles.Add(role.Role);
                                                }
                                                if (!temporaryRoles.Contains(temporaryUserRoleModel.Role)){

                                                    // Action
                                                    var temporaryUserRoleDto = new TemporaryUserRoleDTO();
                                                    var dateNow = DateTime.Now;
                                                    var userId = User.FindFirst("UId").Value;
                                                    temporaryUserRoleDto.UserId = userId;
                                                    temporaryUserRoleDto.Active = true;
                                                    temporaryUserRoleDto.Role = temporaryUserRoleModel.Role;
                                                    temporaryUserRoleDto.TargetUserId = targetUserId;
                                                    temporaryUserRoleDto.StartDate = temporaryUserRoleModel.StartDate;
                                                    temporaryUserRoleDto.ExpirationDate = temporaryUserRoleModel.ExpirationDate;
                                                    temporaryUserRoleDto.CreatedAt = dateNow;
                                                    temporaryUserRoleDto.UpdatedAt = dateNow;

                                                    await _temporaryUserRoleService.CreateTemporaryUserRoleAsync(temporaryUserRoleDto);

                                                    return Ok($@"The {temporaryUserRoleModel.Role} has been successfully assigned to the {person.Data.FullName}.");
                                                    // End Action
                                                }

                                                return Conflict($@"The {temporaryUserRoleModel.Role} already exists in this {person.Data.FullName}'s temporary roles.");
                                            }
                                            return Conflict($@"The {temporaryUserRoleModel.Role} already exists in this {person.Data.FullName}'s additional roles.");
                                        }
                                        return Conflict($@"The {temporaryUserRoleModel.Role} already exists in this {person.Data.FullName}'s User Profile.");
                                    }
                                    return BadRequest(userProfile);
                                } 
                                catch (Exception e)
                                {
                                    return BadRequest($@"An error occurred in the request. - {e.Message}");
                                    // return NotFound($@"The specified user '{temporaryUserRoleModel.UserName}', does not exist in the system!");
                                }
                            }
                            return NotFound($@"The specified role '{temporaryUserRoleModel.Role}', does not exist in the system!");
                        }
                        return BadRequest("The expiration date must be greater than the start date.");
                    }
                    return BadRequest("The start date cannot be retroactive.");
                }
                return BadRequest("Username cannot be null or empty.");
            }
            return BadRequest("Role cannot be null or empty.");
        }
        /// <summary>
        /// Updates the start and expiration dates of a temporary user role.
        /// </summary>
        /// <param name="editTemporaryUserRoleModel"></param>
        /// <returns></returns>
        [HttpPatch("TemporaryUserRole")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> UpdateTemporaryUserRoleAsync([FromBody] EditTemporaryUserRoleModel editTemporaryUserRoleModel){
            var temporaryUserRole = await _temporaryUserRoleService.GetTemporaryUserRoleByIdAsync(editTemporaryUserRoleModel.Id);
            try
            {
                var result = temporaryUserRole.IsSuccess;
                if (editTemporaryUserRoleModel.StartDate > temporaryUserRole.Data.CreatedAt){
                    if (editTemporaryUserRoleModel.ExpirationDate > editTemporaryUserRoleModel.StartDate){
                        var temporaryUserRoleDto = new TemporaryUserRoleDTO(){
                            Id = temporaryUserRole.Data.Id,
                            Active = temporaryUserRole.Data.Active,
                            CreatedAt = temporaryUserRole.Data.CreatedAt,
                            UpdatedAt = DateTime.Now,
                            Role = temporaryUserRole.Data.Role,
                            TargetUserId = temporaryUserRole.Data.TargetUserId,
                            StartDate = editTemporaryUserRoleModel.StartDate,
                            ExpirationDate = editTemporaryUserRoleModel.ExpirationDate
                        };
                        // Action
                        await _temporaryUserRoleService.UpdateTemporaryUserRoleAsync(temporaryUserRoleDto);
                        return Ok(temporaryUserRoleDto);
                        // Action End
                    }
                    return BadRequest("The expiration date must be greater than the start date.");
                }
                return BadRequest("The start date must be greater than the created date");
            }
            catch
            {
                return NotFound($@"We did not find the temporary user role with id {editTemporaryUserRoleModel.Id}.");
            }
        }
        /// <summary>
        /// Removes a temporary role from a user.
        /// </summary>
        /// <param name="removeTemporaryUserRoleModel"></param>
        /// <returns></returns>
        [HttpDelete("TemporaryUserRole")]
        [Authorize(Roles = "RemoveFromRole")]
        public async Task<ActionResult> RemoveTemporaryUserRoleAsync([FromBody] RemoveTemporaryUserRoleModel removeTemporaryUserRoleModel){
            if (!String.IsNullOrEmpty(removeTemporaryUserRoleModel.Role)) {
                if (!String.IsNullOrEmpty(removeTemporaryUserRoleModel.UserName)){
                    var roleExists = await _roleService.RoleExistsAsync(removeTemporaryUserRoleModel.Role);
                    if (roleExists) {
                        var targetUser = await _userService.GetUserByNameAsync(removeTemporaryUserRoleModel.UserName);
                        try
                        {
                            var person = await _personService.GetPersonByIdAsync(targetUser.PersonId);
                            var targetUserId = targetUser.Id;
                            var temporaryUserRoleFilterDb = new TemporaryUserRoleFilterDb(){
                                TargetUserId = targetUserId,
                                PageSize = 10000, 
                                OrderByProperty = "Id", 
                                Page=1, 
                                Role=removeTemporaryUserRoleModel.Role, 
                                UserId=null,
                                StartDate=null,
                                ExpirationDate=DateTime.Now
                            };
                            var temporaryUserRoles = await _temporaryUserRoleService.GetTemporaryUserRolesAsync(temporaryUserRoleFilterDb);
                            var temporaryRoles = new List<string>();
                            var temporaryRoleId = 0;
                            foreach(var role in temporaryUserRoles.Data.Data){
                                temporaryRoles.Add(role.Role);
                                temporaryRoleId = role.Id;
                            }
                            if (temporaryRoles.Contains(removeTemporaryUserRoleModel.Role)){
                                try
                                {
                                    await _temporaryUserRoleService.RemoveTemporaryUserRoleAsync(temporaryRoleId);
                                    return Ok($@"The '{removeTemporaryUserRoleModel.Role}' has been successfully removed from the {person.Data.FullName}.");
                                }
                                catch
                                {
                                    return BadRequest($@"An error occurred while removing the temporary role '{removeTemporaryUserRoleModel.Role}' for {person.Data.FullName}.");
                                }
                            }
                            return Conflict($@"The {removeTemporaryUserRoleModel.Role} not exists in this {person.Data.FullName}'s temporary roles.");
                        }
                        catch
                        {
                            return NotFound($@"The specified user '{removeTemporaryUserRoleModel.UserName}', does not exist in the system!");
                        }
                    }
                    return NotFound($@"The specified role '{removeTemporaryUserRoleModel.Role}', does not exist in the system!");
                }
                return BadRequest("Username cannot be null or empty.");
            }
            return BadRequest("Role cannot be null or empty.");
        }
        /// <summary>
        /// Returns a list of roles.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetRolesAsync(){
            var roles = await _roleService.GetRolesAsync();
            return Ok(roles);
        }
        /// <summary>
        /// Returns a list of roles off some username.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet("GetUserRoles/{userName}")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> GetUserRolesAsync(string userName){
            var roles = await _userService.GetUserRolesAsync(userName);
            return Ok(roles);
        }
        /// <summary>
        /// Creates a new user role profile
        /// </summary>
        /// <param name="userProfileModel"></param>
        /// <returns></returns>
        [HttpPost("AddUserRoleProfile")]
        [Authorize(Roles = "AddToRole")]
        public async Task<ActionResult> AddUserRoleAsync([FromBody] UserProfileModel userProfileModel){
            if (userProfileModel == null)
                return BadRequest("The user profile model not be null!");
            var userProfileDto = new UserProfileDTO();
            var userId = User.FindFirst("UId").Value;
            var dateNow = DateTime.UtcNow;
            userProfileDto.Name = userProfileModel.Name;
            userProfileDto.Description = userProfileModel.Description;
            userProfileDto.Active = userProfileModel.Active;
            userProfileDto.CreatedAt = dateNow;
            userProfileDto.UpdatedAt = dateNow;
            userProfileDto.UserId = userId;
            await _userProfileService.CreateUserProfileAsync(userProfileDto);
            return Ok(userProfileDto);
            
        }
        /// <summary>
        /// Change a user's password from the "passwordModel" properties.
        /// </summary>
        /// <param name="passwordModel"></param>
        /// <returns></returns>
        [HttpPatch("ResetUserPassword")]
        [Authorize(Roles = "EditUser")]
        public async Task<ActionResult> ResetUserPassword([FromBody] ResetPasswordModel passwordModel)
        {
            var user = await _userService.GetUserByIdAsync(passwordModel.Id.ToString());
            if (user != null)
            {
                var result = await _userService.UpdatePasswordHashAsync(user, passwordModel.Password);
                if (result)
                {
                    return Ok(user);
                }
                return BadRequest("There was an error updating the pasword!");
                
            }
            return NotFound("User not found in de system!");
        }
        /// <summary>
        /// Change the authenticated user's password from the "myPasswordModel" properties.
        /// </summary>
        /// <param name="myPasswordModel"></param>
        /// <returns></returns>
        [HttpPatch("ResetMyUserPassword")]
        [Authorize]
        public async Task<ActionResult> ResetMyUserPassword([FromBody] ResetMyPasswordModel myPasswordModel)
        {
            var userId = User.FindFirst("UId").Value;
            var user = await _userService.GetUserByIdAsync(userId);
            if (user != null)
            {
                var result = await _userService.UpdatePasswordHashAsync(user, myPasswordModel.Password);
                if (result)
                {
                    return Ok(user);
                }
                return BadRequest("There was an error updating the pasword!");
                
            }
            return NotFound("User not found in de system!");
        }
        /// <summary>
        /// Changes a user's username from the "userNameModel" properties.
        /// </summary>
        /// <param name="userNameModel"></param>
        /// <returns></returns>
        [HttpPatch("EditUserName")]
        [Authorize(Roles = "EditUser")]
        public async Task<ActionResult> EditeUserName([FromBody] EditUserNameModel userNameModel)
        {
            var user = await _userService.GetUserByIdAsync(userNameModel.Id.ToString());
            if (user != null)
            {
                user.UserName = userNameModel.Email;
                user.NormalizedUserName = userNameModel.Email.ToUpper();
                user.Email = userNameModel.Email;
                user.NormalizedEmail = userNameModel.Email.ToUpper();
                var result = await _userService.UpdateUserAsync(user);
                if (result)
                {
                    return Ok(userNameModel);
                }
                return BadRequest("There was an error updating the username!");
                
            }
            return NotFound("User not found in de system!");
        }
        /// <summary>
        /// Changes the authenticated user's username from the "myUserNameModel" properties.
        /// </summary>
        /// <param name="myUserNameModel"></param>
        /// <returns></returns>
        [HttpPatch("EditMyUserName")]
        [Authorize]
        public async Task<ActionResult> EditeMyUserName([FromBody] EditMyUserNameModel myUserNameModel)
        {
            var userId = User.FindFirst("UId").Value;
            var user = await _userService.GetUserByIdAsync(userId);
            if (user != null)
            {
                user.UserName = myUserNameModel.Email;
                user.NormalizedUserName = myUserNameModel.Email.ToUpper();
                user.Email = myUserNameModel.Email;
                user.NormalizedEmail = myUserNameModel.Email.ToUpper();
                var result = await _userService.UpdateUserAsync(user);
                if (result)
                {
                    return Ok(myUserNameModel);
                }
                return BadRequest("There was an error updating the username!");
                
            }
            return NotFound("User not found in de system!");
        }
    }
}