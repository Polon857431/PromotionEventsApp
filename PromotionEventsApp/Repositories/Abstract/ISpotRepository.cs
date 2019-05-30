
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.Repositories.Abstract
{
    public interface ISpotRepository
    {

        Task<Spot> GetAsync(int id);
        Task<Spot> GetAsync(int id, params Expression<Func<Spot, object>>[] includeProperties);
        Task<Spot> GetAsync(Expression<Func<Spot, bool>> predicate);
        Task<Spot> GetAsync(Expression<Func<Spot, bool>> predicate, params Expression<Func<Spot, object>>[] includeProperties);

        Task<IEnumerable<Spot>> GetAllAsync();
        Task<IEnumerable<Spot>> GetAllAsync(params Expression<Func<Spot, object>>[] includeProperties);

        Task<IEnumerable<Spot>> FindByAsync(Expression<Func<Spot, bool>> predicate);
        Task<IEnumerable<Spot>> FindByAsync(Expression<Func<Spot, bool>> predicate, params Expression<Func<Spot, object>>[] includeProperties);

        void Add(Spot s);
        void Update(Spot s);
        void Delete(Spot s);
        Task CommitAsync();

        Task<bool> IsExistsAsync(Expression<Func<Spot, bool>> predicate);
        Task<bool> IsExistsAsync(int id);
        Task<bool> AnyAsync();

        int GetLastId();

        Task<List<VisitedSpot>> GetUserSpots(User user);

    }
}
