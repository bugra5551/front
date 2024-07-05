namespace CarRentalAPI.Persistence.Repositories;

/// <summary>
/// Genel CRUD operasyonları için depo arayüzü.
/// </summary>
/// <typeparam name="TEntity">Entity tipi.</typeparam>
public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> Add(TEntity entity);
    Task<TEntity> Update(TEntity entity);
    Task<bool> Delete(TEntity entity);
}