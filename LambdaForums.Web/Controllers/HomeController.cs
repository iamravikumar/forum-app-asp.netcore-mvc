using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LambdaForums.Web.Models;
using LambdaForums.Web.Models.Home;
using LambdaForums.Data.Interfaces;
using LambdaForums.Web.Models.Post;
using Microsoft.AspNetCore.Identity;
using LambdaForums.Data.Models;

namespace LambdaForums.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPost _postService;
        private readonly IApplicationUser _applicationUser;
        private static UserManager<ApplicationUser> _userManager;

        public HomeController(IPost postService, UserManager<ApplicationUser> userManager, IApplicationUser applicationUser)
        {
            _postService = postService;
            _userManager = userManager;
            _applicationUser = applicationUser;
        }

        public IActionResult Index()
        {
            var model = BuildHomeIndexModel();
            return View(model);
        }

        private HomeIndexModel BuildHomeIndexModel()
        {
            var latest = _postService.GetLatestPosts(10);

            var posts = latest.Select(post => new PostListingModel
            {
                Id = post.Id,
                Title = post.Title,
                Author = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                DatePosted = post.Created.ToString(),
                //RepliesCount = _postService.GetReplyCount(post.Id),
                RepliesCount = post.Replies.Count(),
                ForumName = post.Forum.Title,
                //ForumImageUrl = _postService.GetForumImageUrl(post.Id),
                ForumImageUrl = post.Forum.ImageUrl,
                ForumId = post.Forum.Id
            });

            return new HomeIndexModel()
            {
                LatestPosts = posts
            };

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
