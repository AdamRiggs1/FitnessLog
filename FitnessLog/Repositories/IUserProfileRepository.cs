using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IUserProfileRepository
    {
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        List<UserProfile> GetAllUsers();
        void Add(UserProfile userProfile);

        void Update(UserProfile userProfile);

        void Delete(int id);
    }
}