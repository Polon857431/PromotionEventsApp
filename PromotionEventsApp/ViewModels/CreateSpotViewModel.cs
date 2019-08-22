using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PromotionEventsApp.ViewModels
{
    public class CreateSpotViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile SpotImage { get; set; }
        public string Coords { get; set; }

    }
    public class FormFileWrapper
    {
        public IFormFile File { get; set; }
    }
}
