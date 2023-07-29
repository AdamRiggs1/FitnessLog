using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IStrengthWorkoutRepository
    {
        List<StrengthWorkout> GetAll();
        void Add(StrengthWorkout strengthWorkout);

        void Update(StrengthWorkout strengthWorkout);

        void Delete(int id);
    }
}