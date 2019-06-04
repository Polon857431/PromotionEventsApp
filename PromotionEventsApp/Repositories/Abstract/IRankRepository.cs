using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.Repositories.Abstract
{
    public  interface IRankRepository
    {
        Task<IEnumerable<VisitedSpot>> GetAllAsync();
        Task<IEnumerable<VisitedSpot>> GetAllAsync(params Expression<Func<VisitedSpot, object>>[] includeProperties);
    }
}
