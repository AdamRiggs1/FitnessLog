using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IUserStrengthWorkoutRepository
    {
        List<UserStrengthWorkout> GetAll();
        UserStrengthWorkout GetByFirebaseUserId(string firebaseUserId);
        List<UserStrengthWorkout> CurrentUsersStrengthWorkouts(string firebaseUserId);
        void Add(UserStrengthWorkout userStrengthWorkout);

        void Update(UserStrengthWorkout userStrengthWorkout);

        void Delete(int id);
    }
}