using NHibernate.Mapping.ByCode;

namespace StyleShareWebsite.DataAccess.Interfaces.Common
{
    public interface IAsyncService<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> AddAsync(T entity);

    }
}
