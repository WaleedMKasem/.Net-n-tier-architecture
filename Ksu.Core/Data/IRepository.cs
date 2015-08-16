using System.Linq;
using System.Collections.Generic;

namespace Arabia.Core.Data
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {
        T GetById(object id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        IQueryable<T> Table { get; }
    
    }
}
