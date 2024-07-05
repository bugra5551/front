using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Lokasyon CRUD operasyonları için servis sınıfı.
/// </summary>
public class LocationServiceImpl : ILocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// LocationServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="locationRepository">Lokasyon deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public LocationServiceImpl(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir lokasyon ekler.
    /// </summary>
    /// <param name="locationDTO">Eklenecek lokasyon bilgileri.</param>
    /// <returns>Eklenen lokasyon bilgileri.</returns>
    public async Task<LocationCreateUpdateDTO> Add(LocationCreateUpdateDTO locationDTO)
    {
        var location = _mapper.Map<Location>(locationDTO);
        var addedLocation = await _locationRepository.Add(location);
        return _mapper.Map<LocationCreateUpdateDTO>(addedLocation);
    }

    /// <summary>
    /// Bir lokasyonu siler.
    /// </summary>
    /// <param name="locationId">Silinecek lokasyonun ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int locationId)
    {
        var existingLocation = await _locationRepository.GetByIdAsync(locationId);
        if (existingLocation == null) return false;
        else
        {
            existingLocation.isDeleted = true;
            return await _locationRepository.Delete(existingLocation);
        }         
    }

    /// <summary>
    /// Tüm lokasyonları getirir.
    /// </summary>
    /// <returns>Tüm lokasyonların listesi.</returns>
    public async Task<IEnumerable<LocationDTO>> GetAllAsync()
    {
        var locations = await _locationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LocationDTO>>(locations);
    }

    /// <summary>
    /// Belirli bir lokasyonu ID ile getirir.
    /// </summary>
    /// <param name="id">Getirilecek lokasyonun ID'si.</param>
    /// <returns>Belirli lokasyonun bilgileri.</returns>
    public async  Task<LocationDTO> GetByIdAsync(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        return _mapper.Map<LocationDTO>(location);
    }

    /// <summary>
    /// Bir lokasyonu günceller.
    /// </summary>
    /// <param name="locationDTO">Güncellenecek lokasyon bilgileri.</param>
    /// <returns>Güncellenen lokasyon bilgileri.</returns>
    public async Task<LocationCreateUpdateDTO> Update(LocationCreateUpdateDTO locationDTO)
    {

        var location = _mapper.Map<Location>(locationDTO);

        var existingLocation = await _locationRepository.GetByIdAsync(location.LocationId);
        if (existingLocation == null) return null;

        var updatedLocation = await _locationRepository.Update(location);
        return _mapper.Map<LocationCreateUpdateDTO>(updatedLocation);
    }
}