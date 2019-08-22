using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;

namespace PromotionEventsApp.Services.Abstract
{
    public interface IMobileService
    {
        Task CheckCode(string code, User user);
    }
}
