using LambdaForums.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForums.Data.Interfaces
{
    public interface IForum
    {
        Forum GetById(int id);
        IEnumerable<Forum> GetAll();
        Task Create(Forum forum);
        Task Delete(int id);
        //Task UpdateForumTitle(int id, string title);
        //Task UpdateForumDescription(int id, string description);
        //Post GetLatestPost(int forumId);
        IEnumerable<ApplicationUser> GetActiveUsers(int forumId);
        bool HasRecentPost(int id);
        //Task SetForumImage(int id, Uri uri);
    }
}
