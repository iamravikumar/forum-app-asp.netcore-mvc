using LambdaForums.Data.Interfaces;
using LambdaForums.Data.Models;
using LambdaForums.Web.Models.Post;
using LambdaForums.Web.Models.Reply;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaForums.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        //private readonly IPostFormatter _postFormatter;
        private readonly IApplicationUser _userService;
        private static UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, IForum forumService, IApplicationUser userService,
            UserManager<ApplicationUser> userManager/*, IPostFormatter postFormatter*/)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
            //_postFormatter = postFormatter;
            _userService = userService;
        }

        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var replies = BuildPostReplies(post.Replies).OrderByDescending(reply => reply.Created);

            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                IsAuthorAdmin = IsAuthorAdmin(post.User),
                Created = post.Created,
                //PostContent = _postFormatter.Prettify(post.Content),
                PostContent = post.Content,
                Replies = replies,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title
            };

            return View(model);
        }

        [Authorize]
        public IActionResult Create(int id)
        {
            // note id here - is Forum.Id
            var forum = _forumService.GetById(id);

            var model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                AuthorName = User.Identity.Name,
                ForumImageUrl = forum.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var post = BuildPost(model, user);

            await _postService.Add(post);
            await _userService.UpdateUserRating(userId, typeof(Post));

            return RedirectToAction("Index", "Forum", model.ForumId);
            //return RedirectToAction("Index", "Post", post.Id);
        }

        private bool IsAuthorAdmin(ApplicationUser user)
        {
            return _userManager.GetRolesAsync(user)
                .Result.Contains("Admin");
        }

        private Post BuildPost(NewPostModel post, ApplicationUser user)
        {
            var now = DateTime.Now;
            var forum = _forumService.GetById(post.ForumId);

            return new Post
            {
                Title = post.Title,
                Content = post.Content,
                Created = now,
                Forum = forum,
                User = user,
                //IsArchived = false
            };
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content,
                //ReplyContent = _postFormatter.Prettify(reply.Content),
                IsAuthorAdmin = IsAuthorAdmin(reply.User)
            });
        }
    }
}