using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Araç sınıfı CRUD operasyonları için servis sınıfı.
/// </summary>
public class CarClassServiceImpl : ICarClassService
{
    private readonly ICarClassRepository _carClassRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// CarClassServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="carClassRepository">Araç sınıfı deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public CarClassServiceImpl(ICarClassRepository carClassRepository, IMapper mapper)
    {
        _carClassRepository = carClassRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir araç sınıfı ekler.
    /// </summary>
    /// <param name="carClassCreateUpdateDTO">Eklenecek araç sınıfı bilgileri.</param>
    /// <returns>Eklenen araç sınıfı bilgileri.</returns>
    public async Task<CarClassCreateUpdateDTO> Add(CarClassCreateUpdateDTO carClassCreateUpdateDTO)
    {
        carClassCreateUpdateDTO.isDeleted = false;
        var carClass = _mapper.Map<CarClass>(carClassCreateUpdateDTO);
        var addedCarClass = await _carClassRepository.Add(carClass);
        return _mapper.Map<CarClassCreateUpdateDTO>(addedCarClass);
    }

    /// <summary>
    /// Bir araç sınıfını siler.
    /// </summary>
    /// <param name="carClassId">Silinecek araç sınıfının ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int carClassId)
    {
        var existingCarClass = await _carClassRepository.GetByIdAsync(carClassId);
        if (existingCarClass != null)
        {
            existingCarClass.isDeleted = true;
            return await _carClassRepository.Delete(existingCarClass);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Tüm araç sınıflarını getirir.
    /// </summary>
    /// <returns>Tüm araç sınıflarının listesi.</returns>
    public async Task<IEnumerable<CarClassDTO>> GetAllAsync()
    {
        var carClasses = await _carClassRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CarClassDTO>>(carClasses);
    }

    /// <summary>
    /// Belirli bir araç sınıfını ID ile getirir.
    /// </summary>
    /// <param name="carClassId">Getirilecek araç sınıfının ID'si.</param>
    /// <returns>Belirli araç sınıfının bilgileri.</returns>
    public async Task<CarClassDTO> GetByIdAsync(int carClassId)
    {
        var carClass = await _carClassRepository.GetByIdAsync(carClassId);
        return _mapper.Map<CarClassDTO>(carClass);
    }

    /// <summary>
    /// Bir araç sınıfını günceller.
    /// </summary>
    /// <param name="carClassCreateUpdateDTO">Güncellenecek araç sınıfı bilgileri.</param>
    /// <returns>Güncellenen araç sınıfı bilgileri.</returns>
    public async Task<CarClassCreateUpdateDTO> Update(CarClassCreateUpdateDTO carClassCreateUpdateDTO)
    {
        //var existingCarClass = await _carClassRepository.GetByIdAsync(carClass.CarClassId);
        //if (existingCarClass == null) return null;

        var carClass = _mapper.Map<CarClass>(carClassCreateUpdateDTO);
        var updatedCarClass = await _carClassRepository.Update(carClass);
        if (updatedCarClass != null)
        {
            return _mapper.Map<CarClassCreateUpdateDTO>(updatedCarClass);
        }
        return null;
    }
}
