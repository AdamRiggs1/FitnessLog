namespace FitnessLog.Models
{
    public class UserStrengthWorkout
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int StrengthWorkoutId { get; set; }
        public StrengthWorkout StrengthWorkout { get; set; }
    }
}
