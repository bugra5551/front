using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Kiralama koşulları CRUD operasyonları için servis sınıfı.
/// </summary>
public class RentingConditionServiceImpl : IRentingConditionService
{
    private readonly IRentingConditionRepository _rentingConditionRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// RentingConditionServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="rentingConditionRepository">Kiralama koşulu deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public RentingConditionServiceImpl(IRentingConditionRepository rentingConditionRepository, IMapper mapper)
    {
        _rentingConditionRepository = rentingConditionRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir kiralama koşulu ekler.
    /// </summary>
    /// <param name="rentingConditionCreateUpdateDTO">Eklenecek kiralama koşulu bilgileri.</param>
    /// <returns>Eklenen kiralama koşulu bilgileri.</returns>
    public async Task<RentingConditionCreateUpdateDTO> Add(RentingConditionCreateUpdateDTO rentingConditionCreateUpdate)
    {
        rentingConditionCreateUpdate.isDeleted = false;
        var rentingCondition = _mapper.Map<RentingCondition>(rentingConditionCreateUpdate);
        var addedRentingCondition = await _rentingConditionRepository.Add(rentingCondition);
        return _mapper.Map<RentingConditionCreateUpdateDTO>(addedRentingCondition);
    }

    /// <summary>
    /// Bir kiralama koşulunu siler.
    /// </summary>
    /// <param name="rentingConditionId">Silinecek kiralama koşulunun ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int rentingConditionId)
    {
        var existingRentingCondition = await _rentingConditionRepository.GetByIdAsync(rentingConditionId);
        if (existingRentingCondition != null)
        {
            existingRentingCondition.isDeleted = true;
            return await _rentingConditionRepository.Delete(existingRentingCondition);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Tüm kiralama koşullarını getirir.
    /// </summary>
    /// <returns>Tüm kiralama koşullarının listesi.</returns>
    public async Task<IEnumerable<RentingConditionDTO>> GetAllAsync()
    {
        var rentingConditions = await _rentingConditionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RentingConditionDTO>>(rentingConditions);
    }

    /// <summary>
    /// Belirli bir kiralama koşulunu ID ile getirir.
    /// </summary>
    /// <param name="rentingConditionId">Getirilecek kiralama koşulunun ID'si.</param>
    /// <returns>Belirli kiralama koşulunun bilgileri.</returns>
    public async Task<RentingConditionDTO> GetByIdAsync(int rentingConditionId)
    {
        var rentingCondition = await _rentingConditionRepository.GetByIdAsync(rentingConditionId);
        return _mapper.Map<RentingConditionDTO>(rentingCondition);
    }

    /// <summary>
    /// Bir kiralama koşulunu günceller.
    /// </summary>
    /// <param name="rentingConditionCreateUpdateDTO">Güncellenecek kiralama koşulu bilgileri.</param>
    /// <returns>Güncellenen kiralama koşulu bilgileri.</returns>
    public async Task<RentingConditionCreateUpdateDTO> Update(RentingConditionCreateUpdateDTO rentingConditionCreateUpdateDTO)
    {
        var rentingCondition = _mapper.Map<RentingCondition>(rentingConditionCreateUpdateDTO);

        var updatedRentingCondition = await _rentingConditionRepository.Update(rentingCondition);
        if (rentingCondition != null)
        {
            return _mapper.Map<RentingConditionCreateUpdateDTO>(updatedRentingCondition);
        }
        return null;
    }
}