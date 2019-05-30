using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PromotionEventsApp.DAL;
using PromotionEventsApp.Models;
using PromotionEventsApp.Repositories.Abstract;

namespace PromotionEventsApp.Repositories
{
    public class SpotRepository : ISpotRepository
    {
        private readonly AppDbContext _context;



        public SpotRepository(AppDbContext context)
        {
            _context = context;
        }

        private IQueryable<Spot> Includer(IQueryable<Spot> query, params Expression<Func<Spot, object>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return query;
        }

        public async Task<Spot> GetAsync(int id)
        {
            return await GetAsync(_ => _.Id.Equals(id));
        }

        public async Task<Spot> GetAsync(int id, params Expression<Func<Spot, object>>[] includeProperties)
        {
            return await GetAsync(_ => _.Id.Equals(id), includeProperties);
        }

        public async Task<Spot> GetAsync(Expression<Func<Spot, bool>> predicate)
        {
            return await _context.Set<Spot>().FirstOrDefaultAsync(predicate);
        }

        public async Task<Spot> GetAsync(Expression<Func<Spot, bool>> predicate, params Expression<Func<Spot, object>>[] includeProperties)
        {
            var query = Includer(_context.Set<Spot>(), includeProperties);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Spot>> GetAllAsync()
        {
            IQueryable<Spot> query = _context.Set<Spot>();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Spot>> GetAllAsync(params Expression<Func<Spot, object>>[] includeProperties)
        {
            IQueryable<Spot> query = _context.Set<Spot>();
            query = Includer(query, includeProperties);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Spot>> FindByAsync(Expression<Func<Spot, bool>> predicate)
        {
            var query = _context.Set<Spot>().Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Spot>> FindByAsync(Expression<Func<Spot, bool>> predicate, params Expression<Func<Spot, object>>[] includeProperties)
        {
            var query = _context.Set<Spot>().Where(predicate);
            query = Includer(query, includeProperties);
            return await query.ToListAsync();
        }

        public void Add(Spot entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        public void Update(Spot entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(Spot entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistsAsync(int id)
        {
            return await IsExistsAsync(_ => _.Id.Equals(id));
        }

        public async Task<bool> IsExistsAsync(Expression<Func<Spot, bool>> predicate)
        {
            return await _context.Set<Spot>().AnyAsync(predicate);
        }

        public async Task<bool> AnyAsync()
        {
            return await _context.Set<Spot>().AnyAsync();
        }
        public int GetLastId() => _context.Events.Max(_ => _.Id);
        public async Task<List<VisitedSpot>> GetUserSpots(User user)
        {
            return await _context.UserSpots.Where(_ => _.UserId == user.Id).ToListAsync();
        }
    }
}
