using LambdaForums.Web.Models.Post;
using System.Collections.Generic;

namespace LambdaForums.Web.Models.Search
{
    public class SearchResultModel
    {
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string SearchQuery { get; set; }
        public bool EmptySearchResults { get; set; }
    }
}
