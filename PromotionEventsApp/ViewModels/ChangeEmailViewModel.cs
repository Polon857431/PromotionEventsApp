using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEventsApp.ViewModels
{
    public class ChangeEmailViewModel
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string NewEmail { get; set; }
        public string NewEmailConfirm { get; set; }

    }
}
