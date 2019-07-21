using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using PromotionEventsApp.Models;

namespace PromotionEventsApp.Repositories.Abstract
{
    public interface IMemberRepository
    {
    

        Task<IEnumerable<Member>> GetAllAsync();
        Task<IEnumerable<Member>> GetAllAsync(params Expression<Func<Member, object>>[] includeProperties);

        Task<IEnumerable<Member>> FindByAsync(Expression<Func<Member, bool>> predicate);

        Task<IEnumerable<Member>> FindByAsync(Expression<Func<Member, bool>> predicate,
            params Expression<Func<Member, object>>[] includeProperties);

        void Add(Member member);
        void Update(Member member);
        void Delete(Member member);
        Task CommitAsync();
        Task<bool> AnyAsync();
    }
}
