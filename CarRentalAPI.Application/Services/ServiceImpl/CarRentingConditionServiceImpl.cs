using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Araç kiralama koşulları CRUD operasyonları için servis sınıfı.
/// </summary>
public class CarRentingConditionServiceImpl : ICarRentingConditionService
{
    private readonly ICarRentingConditionRepository _carRentingConditionRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// CarRentingConditionServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="carRentingConditionRepository">Araç kiralama koşulları deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public CarRentingConditionServiceImpl(ICarRentingConditionRepository carRentingConditionRepository, IMapper mapper)
    {
        _carRentingConditionRepository = carRentingConditionRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir araç kiralama koşulu ekler.
    /// </summary>
    /// <param name="carRentingConditionCreateUpdateDTO">Eklenecek araç kiralama koşulu bilgileri.</param>
    /// <returns>Eklenen araç kiralama koşulu bilgileri.</returns>
    public async Task<CarRentingConditionCreateUpdateDTO> Add(CarRentingConditionCreateUpdateDTO carRentingConditionCreateUpdateDTO)
    {
        carRentingConditionCreateUpdateDTO.isDeleted = false;
        var carRentingCondition = _mapper.Map<CarRentingCondition>(carRentingConditionCreateUpdateDTO);
        var addedCarRentingCondition = await _carRentingConditionRepository.Add(carRentingCondition);
        return _mapper.Map<CarRentingConditionCreateUpdateDTO>(addedCarRentingCondition);
    }

    /// <summary>
    /// Bir araç kiralama koşulunu siler.
    /// </summary>
    /// <param name="carRentingConditionId">Silinecek araç kiralama koşulunun ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int carRentingConditionId)
    {
        var existingCarRentingCondition = await _carRentingConditionRepository.GetByIdAsync(carRentingConditionId);

        if (existingCarRentingCondition != null)
        {
            existingCarRentingCondition.isDeleted = true;
            return await _carRentingConditionRepository.Delete(existingCarRentingCondition);
        }
        return false;
    }

    /// <summary>
    /// Tüm araç kiralama koşullarını getirir.
    /// </summary>
    /// <returns>Tüm araç kiralama koşullarının listesi.</returns>
    public async Task<IEnumerable<CarRentingConditionDTO>> GetAllAsync()
    {
        var carRentingConditions = await _carRentingConditionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CarRentingConditionDTO>>(carRentingConditions);
    }

    /// <summary>
    /// Belirli bir araç kiralama koşulunu ID ile getirir.
    /// </summary>
    /// <param name="carRentingConditionId">Getirilecek araç kiralama koşulunun ID'si.</param>
    /// <returns>Belirli araç kiralama koşulunun bilgileri.</returns>
    public async Task<CarRentingConditionDTO> GetByIdAsync(int carRentingConditionId)
    {
        var carRentingCondition = await _carRentingConditionRepository.GetByIdAsync(carRentingConditionId);
        return _mapper.Map<CarRentingConditionDTO>(carRentingCondition);
    }

    /// <summary>
    /// Belirli bir araca ait kiralama koşullarını getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli araca ait kiralama koşullarının listesi.</returns>
    public async Task<IEnumerable<CarRentingConditionDTO>> GetCarRentingConditionsByCarIdAsync(int carId)
    {
        var carRentingConditionsByCar = await _carRentingConditionRepository.GetCarRentingConditionsByCarId(carId);
        return _mapper.Map<List<CarRentingConditionDTO>>(carRentingConditionsByCar);
    }

    /// <summary>
    /// Bir araç kiralama koşulunu günceller.
    /// </summary>
    /// <param name="carRentingConditionCreateUpdateDTO">Güncellenecek araç kiralama koşulu bilgileri.</param>
    /// <returns>Güncellenen araç kiralama koşulu bilgileri.</returns>
    public async Task<CarRentingConditionCreateUpdateDTO> Update(CarRentingConditionCreateUpdateDTO carRentingConditionCreateUpdateDTO)
    {
        var carRentingCondition = _mapper.Map<CarRentingCondition>(carRentingConditionCreateUpdateDTO);

        var updatedCarRentingCondition = await _carRentingConditionRepository.Update(carRentingCondition);
        if (updatedCarRentingCondition != null)
        {
            return _mapper.Map<CarRentingConditionCreateUpdateDTO>(updatedCarRentingCondition);
        }
        return null;
    }
}