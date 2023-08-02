namespace FitnessLog.Models
{
    public class UserFood
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
