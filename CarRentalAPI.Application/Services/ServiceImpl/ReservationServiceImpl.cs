using AutoMapper;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Rezervasyon CRUD operasyonları için servis sınıfı.
/// </summary>
public class ReservationServiceImpl : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// ReservationServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="reservationRepository">Rezervasyon deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public ReservationServiceImpl(IReservationRepository reservationRepository, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir rezervasyon ekler.
    /// </summary>
    /// <param name="reservationDTO">Eklenecek rezervasyon bilgileri.</param>
    /// <returns>Eklenen rezervasyon bilgileri.</returns>
    public async Task<ReservationCreateUpdateDTO> Add(ReservationCreateUpdateDTO reservationDTO)
    {
        var reservation = _mapper.Map<Reservation>(reservationDTO);
        var addedReservation = await _reservationRepository.Add(reservation);
        return _mapper.Map<ReservationCreateUpdateDTO>(addedReservation);    
    }

    /// <summary>
    /// Bir rezervasyonu siler.
    /// </summary>
    /// <param name="reservationId">Silinecek rezervasyonun ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int reservationId)
    {
        var existingReservation = await _reservationRepository.GetByIdAsync(reservationId);
        if (existingReservation != null)
        {
            existingReservation.isDeleted = true;
            return await _reservationRepository.Delete(existingReservation);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Tüm rezervasyonları getirir.
    /// </summary>
    /// <returns>Tüm rezervasyonların listesi.</returns>
    public async Task<IEnumerable<ReservationDTO>> GetAllAsync()
    {
        var reservations = await _reservationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
    }

    /// <summary>
    /// Belirli bir rezervasyonu ID ile getirir.
    /// </summary>
    /// <param name="reservationId">Getirilecek rezervasyonun ID'si.</param>
    /// <returns>Belirli rezervasyonun bilgileri.</returns>
    public async Task<ReservationDTO> GetByIdAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId);
        return _mapper.Map<ReservationDTO>(reservation);
    }

    /// <summary>
    /// Belirli bir kullanıcının rezervasyonlarını getirir.
    /// </summary>
    /// <param name="userId">Kullanıcının ID'si.</param>
    /// <returns>Kullanıcının rezervasyonlarının listesi.</returns>
    public async Task<IEnumerable<ReservationDTO>> GetReservationsByUserId(int userId)
    {
        var reservations = await _reservationRepository.GetReservationsByUserId(userId);
        return _mapper.Map<IEnumerable<ReservationDTO>>(reservations);
    }

    /// <summary>
    /// Bir rezervasyonu günceller.
    /// </summary>
    /// <param name="reservationDTO">Güncellenecek rezervasyon bilgileri.</param>
    /// <returns>Güncellenen rezervasyon bilgileri.</returns>
    public async Task<ReservationCreateUpdateDTO> Update(ReservationCreateUpdateDTO reservationDTO)
    {
        var reservation = _mapper.Map<Reservation>(reservationDTO);

        var existingReservation = await _reservationRepository.GetByIdAsync(reservation.ReservationId);
        if (existingReservation == null) return null;

        var updatedEmployee = await _reservationRepository.Update(reservation);
        return _mapper.Map<ReservationCreateUpdateDTO>(updatedEmployee);
    }
}