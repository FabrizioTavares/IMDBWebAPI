namespace Repository.Abstraction;

public interface IBaseCompositeRepository<T> : IBaseRepository<T>
{
    Task<T?> GetComposite(int firstId, int secondId, CancellationToken cancellationToken);
}