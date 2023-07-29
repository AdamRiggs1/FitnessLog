namespace FitnessLog.Models
{
    public class CardioWorkout
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int Speed { get; set; }
        public int TypeId { get; set; }
        public WorkoutType WorkoutType { get; set; }
    }
}
