namespace FitnessLog.Models
{
    public class UserCardioWorkout
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int CardioWorkoutId { get; set; }
        public CardioWorkout CardioWorkout { get; set; }
    }
}
