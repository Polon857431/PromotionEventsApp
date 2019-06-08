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
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        private IQueryable<Event> Includer(IQueryable<Event> query,
            params Expression<Func<Event, object>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return query;
        }

        public async Task<Event> GetAsync(int id)
        {
            return await GetAsync(_ => _.Id.Equals(id));
        }

        public async Task<Event> GetAsync(int id, params Expression<Func<Event, object>>[] includeProperties)
        {
            return await GetAsync(_ => _.Id.Equals(id), includeProperties);
        }

        public async Task<Event> GetAsync(Expression<Func<Event, bool>> predicate)
        {
            return await _context.Set<Event>().FirstOrDefaultAsync(predicate);
        }

        public async Task<Event> GetAsync(Expression<Func<Event, bool>> predicate,
            params Expression<Func<Event, object>>[] includeProperties)
        {
            var query = Includer(_context.Set<Event>(), includeProperties);
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            IQueryable<Event> query = _context.Set<Event>();
            var res = await query.ToListAsync();
            return res;
        }

        public async Task<IEnumerable<Event>> GetAllAsync(params Expression<Func<Event, object>>[] includeProperties)
        {
            IQueryable<Event> query = _context.Set<Event>();
            query = Includer(query, includeProperties);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Event>> FindByAsync(Expression<Func<Event, bool>> predicate)
        {
            var query = _context.Set<Event>().Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Event>> FindByAsync(Expression<Func<Event, bool>> predicate,
            params Expression<Func<Event, object>>[] includeProperties)
        {
            var query = _context.Set<Event>().Where(predicate);
            query = Includer(query, includeProperties);
            return await query.ToListAsync();
        }

        public void Add(Event entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        public void Update(Event entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(Event entity)
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

        public async Task<bool> IsExistsAsync(Expression<Func<Event, bool>> predicate)
        {
            return await _context.Set<Event>().AnyAsync(predicate);
        }

        public async Task<bool> AnyAsync()
        {
            return await _context.Set<Event>().AnyAsync();
        }

        public int GetLastId()
        {
            if (_context.Events.Count() != 0)
            {
                return _context.Events.Max(_ => _.Id);
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<Member>> GetUserEvents(User user)
        {
            return await _context.Members.Include(_ => _.Event).Include(_ => _.User).Where(_ => _.UserId == user.Id)
                .ToListAsync();
        }

        public async Task<List<Member>> GetEventMembers(int eventId)
        {
            return await _context.Members.Include(_ => _.Event).Include(_ => _.User).Where(_ => _.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Member> GetMember(int eventId, User user)
        {
            return await _context.Members.SingleAsync(_ => _.EventId == eventId && _.UserId == user.Id);
        }

        public async Task<List<EventSpot>> GetEventSpots(int eventId)
        {
            return await _context.EventSpots.Where(_ => _.EventId == eventId).ToListAsync();
        }
    }
}