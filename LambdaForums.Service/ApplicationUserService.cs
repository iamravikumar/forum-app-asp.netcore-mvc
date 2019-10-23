using LambdaForums.Data;
using LambdaForums.Data.Interfaces;
using LambdaForums.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaForums.Service
{
    public class ApplicationUserService : IApplicationUser
    {
        private readonly ApplicationDbContext _context;

        public ApplicationUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public ApplicationUser GetById(string id)
        {
            return GetAll()
                .FirstOrDefault(user => user.Id == id);
        }

        public async Task UpdateUserRating(string userId, Type type)
        {
            var user = GetById(userId);
            user.Rating = CalculateUserIncrement(type, user.Rating);
            await _context.SaveChangesAsync();
        }

        public async Task SetProfileImage(string id, string uri)
        {
            var user = GetById(id);
            user.ProfileImageUrl = uri;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        private int CalculateUserIncrement(Type type, int userRating)
        {
            var bump = 0;

            if (type == typeof(Post))
            {
                bump = 3;
            }

            if (type == typeof(PostReply))
            {
                bump = 2;
            }

            return userRating + bump;
        }

        public async Task DeactivateUser(ApplicationUser user)
        {
            user.IsActive = false;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task ActivateUser(ApplicationUser user)
        {
            user.IsActive = true;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
