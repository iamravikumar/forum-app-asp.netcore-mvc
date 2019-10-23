using LambdaForums.Web.Models.Post;
using System.Collections.Generic;

namespace LambdaForums.Web.Models.Forum
{
    public class ForumTopicModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        //public bool EmptySearchResults { get; set; }
    }
}
