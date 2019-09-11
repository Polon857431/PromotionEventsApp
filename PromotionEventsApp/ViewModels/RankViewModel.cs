using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;

namespace PromotionEventsApp.ViewModels
{
    public class RankViewModel
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int Points { get; set; }
    }
}
