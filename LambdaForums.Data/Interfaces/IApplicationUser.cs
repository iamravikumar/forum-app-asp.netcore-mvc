using LambdaForums.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaForums.Data.Interfaces
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task UpdateUserRating(string id, Type type);
        //Task Add(ApplicationUser user);
        Task DeactivateUser(ApplicationUser user);
        Task ActivateUser(ApplicationUser user);
        Task SetProfileImage(string id, string uri);
        //Task BumpRating(string userId, Type type);
    }
}
