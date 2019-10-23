using LambdaForums.Data.Models;
using System.Threading.Tasks;

namespace LambdaForums.Data.Interfaces
{
    public interface IPostReply
    {
        Task AddReply(PostReply reply);
        PostReply GetById(int id);
        Task Edit(int id, string message);
        Task Delete(int id);
    }
}
