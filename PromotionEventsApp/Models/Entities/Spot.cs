using System.Collections.Generic;

namespace PromotionEventsApp.Models.Entities
{
    public class Spot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string QrCode { get; set; }
      //  public string Coords { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Image { get; set; }
        public ICollection<EventSpot> Events { get; set; }
        public ICollection<VisitedSpot> Visitors { get; set; }
    }
}
