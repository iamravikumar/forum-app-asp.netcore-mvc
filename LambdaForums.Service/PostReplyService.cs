using LambdaForums.Data;
using LambdaForums.Data.Interfaces;
using LambdaForums.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LambdaForums.Service
{
    public class PostReplyService : IPostReply
    {
        private readonly ApplicationDbContext _context;

        public PostReplyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            await _context.SaveChangesAsync();
        }

        public PostReply GetById(int id)
        {
            return _context.PostReplies
                .Where(reply => reply.Id == id)
                .Include(r => r.Post)
                    .ThenInclude(post => post.Forum)
                .Include(r => r.Post)
                    .ThenInclude(post => post.User)
                .FirstOrDefault();
        }

        public async Task Delete(int id)
        {
            var reply = GetById(id);
            _context.Remove(reply);
            await _context.SaveChangesAsync();
        }

        public async Task Edit(int id, string message)
        {
            var reply = GetById(id);
            await _context.SaveChangesAsync();
            _context.Update(reply);
            await _context.SaveChangesAsync();
        }
    }
}
