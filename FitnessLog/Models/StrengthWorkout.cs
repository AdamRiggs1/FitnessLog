using System.Security;

namespace FitnessLog.Models
{
    public class StrengthWorkout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public int TypeId { get; set; }
        public WorkoutType WorkoutType { get; set; }
    }
}
