namespace CarRentalAPI.Application.Services;

/// <summary>
/// CRUD operasyonları için genel servis arayüzü.
/// </summary>
public interface IService<TCreateUpdateDTO, TReadDTO>
{
    Task<IEnumerable<TReadDTO>> GetAllAsync();
    Task<TReadDTO> GetByIdAsync(int id);
    Task<TCreateUpdateDTO> Add(TCreateUpdateDTO entity);
    Task<TCreateUpdateDTO> Update(TCreateUpdateDTO entity);
    Task<bool> Delete(int id);
}
