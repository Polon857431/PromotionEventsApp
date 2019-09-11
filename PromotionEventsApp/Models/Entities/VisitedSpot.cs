namespace PromotionEventsApp.Models.Entities
{
    public class VisitedSpot
    {
        public int SpotId { get; set; }
        public Spot Spot { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int Value { get; set; }
    }
}
