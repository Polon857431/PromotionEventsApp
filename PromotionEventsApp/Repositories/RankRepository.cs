using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PromotionEventsApp.DAL;
using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories.Abstract;

namespace PromotionEventsApp.Repositories
{
    public class RankRepository : IRankRepository
    {
        private readonly AppDbContext _context;

        public RankRepository(AppDbContext context)
        {
            _context = context;
        }

        private IQueryable<VisitedSpot> Includer(IQueryable<VisitedSpot> query,
            params Expression<Func<VisitedSpot, object>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return query;
        }




        public async Task<IEnumerable<VisitedSpot>> GetAllAsync()
        {
            IQueryable<VisitedSpot> query = _context.Set<VisitedSpot>();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<VisitedSpot>> GetAllAsync(params Expression<Func<VisitedSpot, object>>[] includeProperties)
        {
            IQueryable<VisitedSpot> query = _context.Set<VisitedSpot>();
            query = Includer(query, includeProperties);
            return await query.ToListAsync();
        }
    }
}
