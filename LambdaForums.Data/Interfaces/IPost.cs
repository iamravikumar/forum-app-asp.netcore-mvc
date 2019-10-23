using LambdaForums.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForums.Data.Interfaces
{
    public interface IPost
    {
        Task Add(Post post);
        //Task Archive(int id);
        Task Delete(int id);
        Task EditPostContent(int id, string content);

        //int GetReplyCount(int id);

        Post GetById(int id);
        IEnumerable<Post> GetAll();
        //IEnumerable<Post> GetPostsByUserId(int id);
        IEnumerable<Post> GetPostsByForumId(int id);
        //IEnumerable<Post> GetPostsBetween(DateTime start, DateTime end);
        IEnumerable<Post> GetFilteredPosts(Forum forum, string searchQuery);
        IEnumerable<Post> GetFilteredPosts(string searchQuery);
        IEnumerable<ApplicationUser> GetAllUsersByForum(IEnumerable<Post> posts);
        IEnumerable<Post> GetLatestPosts(int count);
        //string GetForumImageUrl(int id);
    }
}
