using LambdaForums.Data.Interfaces;
using LambdaForums.Data.Models;
using LambdaForums.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaForums.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string content)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            var posts = _context.Posts
                .Include(post => post.Forum)
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(reply => reply.User);
            
            return posts;
        }

        public Post GetById(int id)
        {
            return _context.Posts
                .Where(post => post.Id == id)
                .Include(post => post.Forum)
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(reply => reply.User)
                .FirstOrDefault();
        }

        public IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery)
        {
            var query = (searchQuery != null) ? searchQuery.ToLower() : null;

            return string.IsNullOrEmpty(query)
                ? forum.Posts : forum.Posts.Where(post =>
                    post.Title.ToLower().Contains(query) || post.Content.ToLower().Contains(query));

            //return _context.Posts
            //    .Include(post => post.Forum)
            //    .Include(post => post.User)
            //    .Include(post => post.Replies)
            //    .Where(post =>
            //        post.Title.ToLower().Contains(query)
            //     || post.Content.ToLower().Contains(query));
        }

        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            var query = searchQuery.ToLower();

            return GetAll()
                .Where(post => post.Title.ToLower().Contains(query)
                 || post.Content.ToLower().Contains(query));
        }

        public IEnumerable<Post> GetLatestPosts(int count)
        {
            var allPosts = GetAll().OrderByDescending(post => post.Created);
            return allPosts.Take(count);
        }

        public IEnumerable<Post> GetPostsByForumId(int id)
        {
            return _context.Forums
                .Where(forum => forum.Id == id)
                .First().Posts;
        }

        public IEnumerable<ApplicationUser> GetAllUsersByForum(IEnumerable<Post> posts)
        {
            var users = new List<ApplicationUser>();

            foreach (var post in posts)
            {
                users.Add(post.User);

                if (!post.Replies.Any()) continue;

                users.AddRange(post.Replies.Select(reply => reply.User));
            }

            return users.Distinct();
        }
    }
}
