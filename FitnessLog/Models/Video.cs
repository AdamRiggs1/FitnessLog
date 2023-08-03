namespace FitnessLog.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
