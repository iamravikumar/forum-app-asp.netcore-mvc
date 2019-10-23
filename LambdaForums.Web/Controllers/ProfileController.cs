using System.Linq;
using System.Threading.Tasks;
using LambdaForums.Data.Interfaces;
using LambdaForums.Data.Models;
using LambdaForums.Service.ImageService;
using LambdaForums.Web.Models.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LambdaForums.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService,
            IUploadService uploadService, IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = _userService.GetById(id);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = new ProfileModel()
            {
                Id = user.Id,
                Username = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                DateJoined = user.MemberSince,
                IsActive = user.IsActive,
                IsAdmin = userRoles.Contains("Admin")
            };

            return View(model);
        }

        /*
         * Files uploaded using the IFormFile technique are buffered in memory or on disk on the web server 
         * before being processed. Inside the action method, the IFormFile contents are accessible as a stream. 
         * In addition to the local file system, files can be streamed to Azure Blob storage or Entity Framework.
         */
        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            var connectionString = _configuration.GetConnectionString("AzureStorageAccountConnectionString");
            var containerName = _configuration.GetSection("ContainerName").Value;

            var imageUri = await _uploadService.UploadImageToStorage(file, connectionString, containerName);

            // Set immageUri to the User's profile
            await _userService.SetProfileImage(userId, imageUri);

            return RedirectToAction("Detail", "Profile", new { id = userId });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var profiles = _userService.GetAll()
                .OrderByDescending(user => user.Rating)
                .Select(u => new ProfileModel
                {
                    Id = u.Id,
                    Email = u.Email,
                    Username = u.UserName,
                    ProfileImageUrl = u.ProfileImageUrl,
                    UserRating = u.Rating.ToString(),
                    DateJoined = u.MemberSince,
                    IsActive = u.IsActive
                });

            var model = new ProfileListModel
            {
                Profiles = profiles
            };

            return View(model);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deactivate(string id)
        {
            var user = _userService.GetById(id);
            await _userService.DeactivateUser(user);
            return RedirectToAction("Detail", "Profile", new { id = id });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Activate(string id)
        {
            var user = _userService.GetById(id);
            await _userService.ActivateUser(user);
            return RedirectToAction("Detail", "Profile", new { id = id });
        }
    }
}