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
    public class MemberRepository : IMemberRepository
    {

        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context)
        {
            _context = context;
        }

        private IQueryable<Member> Includer(IQueryable<Member> query, params Expression<Func<Member, object>>[] includeProperties)
        {
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);
            return query;
        }

  

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            IQueryable<Member> query = _context.Set<Member>();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetAllAsync(params Expression<Func<Member, object>>[] includeProperties)
        {
            IQueryable<Member> query = _context.Set<Member>();
            query = Includer(query, includeProperties);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Member>> FindByAsync(Expression<Func<Member, bool>> predicate)
        {
            var query = _context.Set<Member>().Where(predicate);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Member>> FindByAsync(Expression<Func<Member, bool>> predicate, params Expression<Func<Member, object>>[] includeProperties)
        {
            var query = _context.Set<Member>().Where(predicate);
            query = Includer(query, includeProperties);
            return await query.ToListAsync();
        }

        public void Add(Member entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Added;
        }

        public void Update(Member entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Delete(Member entity)
        {
            EntityEntry dbEntityEntry = _context.Entry(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<bool> AnyAsync()
        {
            return await _context.Set<Member>().AnyAsync();
        }
    }
}
