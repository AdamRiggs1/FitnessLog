using FitnessLog.Models;
using System.Collections.Generic;

namespace FitnessLog.Repositories
{
    public interface IWorkoutTypeRepository
    {
        List<WorkoutType> GetAll();
    }
}