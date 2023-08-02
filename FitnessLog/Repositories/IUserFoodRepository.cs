using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IUserFoodRepository
    {
        void Add(UserFood userFood);
        List<UserFood> CurrentUsersFood(string firebaseUserId);
        void Delete(int id);
        List<UserFood> GetAll();
        UserFood GetByFirebaseUserId(string firebaseUserId);
        void Update(UserFood userFood);
    }
}