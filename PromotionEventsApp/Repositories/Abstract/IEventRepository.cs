using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.Repositories.Abstract
{
    public interface IEventRepository
    {

        Task<Event> GetAsync(int id);
        Task<Event> GetAsync(int id, params Expression<Func<Event, object>>[] includeProperties);
        Task<Event> GetAsync(Expression<Func<Event, bool>> predicate);

        Task<Event> GetAsync(Expression<Func<Event, bool>> predicate,
            params Expression<Func<Event, object>>[] includeProperties);

        Task<IEnumerable<Event>> GetAllAsync();
        Task<IEnumerable<Event>> GetAllAsync(params Expression<Func<Event, object>>[] includeProperties);

        Task<IEnumerable<Event>> FindByAsync(Expression<Func<Event, bool>> predicate);

        Task<IEnumerable<Event>> FindByAsync(Expression<Func<Event, bool>> predicate,
            params Expression<Func<Event, object>>[] includeProperties);

        void Add(Event e);
        void Update(Event e);
        void Delete(Event e);
        Task CommitAsync();

        Task<bool> IsExistsAsync(Expression<Func<Event, bool>> predicate);
        Task<bool> IsExistsAsync(int id);
        Task<bool> AnyAsync();
        int GetLastId();
        Task<List<Member>> GetUserEvents(User user);
        Task<List<Member>> GetEventMembers(int id);
        Task<Member> GetMember(int eventId, User user);
        Task<List<EventSpot>> GetEventSpots(int eventId);

    }
}