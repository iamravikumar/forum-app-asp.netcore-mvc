using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaForums.Web.Models.Forum
{
    public class ForumListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPosts { get; set; }
        public int NumberOfUsers { get; set; }
        public string ImageUrl { get; set; }
        public bool HasRecentPost { get; set; }

        //public PostListingModel Latest { get; set; }
        //public IEnumerable<PostListingModel> AllPosts { get; set; }
    }
}
