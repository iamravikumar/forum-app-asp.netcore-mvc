using System.Collections.Generic;

namespace LambdaForums.Web.Models.Forum
{
    public class ForumIndexModel
    {
        //public int NumberOfForums { get; set; }
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}
