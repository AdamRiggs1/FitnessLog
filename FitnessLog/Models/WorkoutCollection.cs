namespace FitnessLog.Models
{
    public class WorkoutCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CardioWorkoutId { get; set; }
        public int StrengthWorkoutId { get; set; }
    }
}
