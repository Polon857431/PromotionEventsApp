namespace PromotionEventsApp.Models.Entities
{
    public class EventSpot
    {
        public int EventId { get; set; }
        public virtual Event Event { get; set; }
        public int SpotId { get; set; }
        public virtual Spot Spot { get; set; }
        public int Value { get; set; }
    }   
}
