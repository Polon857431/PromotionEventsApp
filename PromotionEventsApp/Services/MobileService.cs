using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEventsApp.Models;
using PromotionEventsApp.Models.Entities;
using PromotionEventsApp.Services.Abstract;

namespace PromotionEventsApp.Services
{
    public class MobileService : IMobileService
    {
        public async Task CheckCode(string code, User user)
        {
            var x = GetIdFromCode(code);
        }

        private string GetIdFromCode(string code) => code.Split('_')[0];
       

        
    }
}
