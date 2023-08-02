using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IUserCardioWorkoutRepository
    {
        List<UserCardioWorkout> GetAll();

       UserCardioWorkout GetByFirebaseUserId(string firebaseUserId);

        List<UserCardioWorkout> CurrentUsersCardioWorkouts(string firebaseUserId);
        void Add(UserCardioWorkout userCardioWorkout);

        void Update(UserCardioWorkout userCardioWorkout);

        void Delete(int id);
    }
}
