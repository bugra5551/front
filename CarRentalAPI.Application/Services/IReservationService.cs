using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Rezervasyon CRUD operasyonları için servis arayüzü.
/// </summary>
public interface IReservationService : IService<ReservationCreateUpdateDTO,ReservationDTO>
{
    Task<IEnumerable<ReservationDTO>> GetReservationsByUserId(int userId);
}
