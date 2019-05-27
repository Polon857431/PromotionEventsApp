using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public string Image { get; set; }
        public List<Spot> Spots { get; set; }
        public List<User> Members { get; set; }
        public IFormFile EventImage { get; set; }
    }
}
