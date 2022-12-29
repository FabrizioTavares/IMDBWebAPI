namespace Repository.Repositories.Abstract
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll();
        Task<T?> Get(int id, CancellationToken cancellationToken);
        Task<T> Insert(T entity, CancellationToken cancellationToken);
        Task Update(T entity, CancellationToken cancellationToken);
        Task Remove(T entity, CancellationToken cancellationToken);
        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
