using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Araç özellikleri CRUD operasyonları için servis sınıfı.
/// </summary>
public class CarSpecificationServiceImpl : ICarSpecificationService
{
    private readonly ICarSpecificationRepository _carSpecificationRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// CarSpecificationServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="carSpecificationRepository">Araç özellikleri deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public CarSpecificationServiceImpl(ICarSpecificationRepository carSpecificationRepository, IMapper mapper)
    {
        _carSpecificationRepository = carSpecificationRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir araç özelliği ekler.
    /// </summary>
    /// <param name="carSpecificationCreateUpdateDTO">Eklenecek araç özelliği bilgileri.</param>
    /// <returns>Eklenen araç özelliği bilgileri.</returns>
    public async Task<CarSpecificationCreateUpdateDTO> Add(CarSpecificationCreateUpdateDTO carSpecificationCreateUpdateDTO)
    {
        carSpecificationCreateUpdateDTO.isDeleted = false;
        var carSpecification = _mapper.Map<CarSpecification>(carSpecificationCreateUpdateDTO);
        var addedCarSpecification = await _carSpecificationRepository.Add(carSpecification);
        return _mapper.Map<CarSpecificationCreateUpdateDTO>(addedCarSpecification);
    }

    /// <summary>
    /// Bir araç özelliğini siler.
    /// </summary>
    /// <param name="carSpecificationId">Silinecek araç özelliğinin ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int carSpecificationId)
    {
        var existingCarSpecification = await _carSpecificationRepository.GetByIdAsync(carSpecificationId);

        if (existingCarSpecification != null)
        {
            existingCarSpecification.isDeleted = true;
            return await _carSpecificationRepository.Delete(existingCarSpecification);
        }
        return false;
    }

    /// <summary>
    /// Tüm araç özelliklerini getirir.
    /// </summary>
    /// <returns>Tüm araç özelliklerinin listesi.</returns>
    public async Task<IEnumerable<CarSpecificationDTO>> GetAllAsync()
    {
        var carSpecifications = await _carSpecificationRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CarSpecificationDTO>>(carSpecifications);
    }

    /// <summary>
    /// Belirli bir araç özelliğini ID ile getirir.
    /// </summary>
    /// <param name="carSpecificationId">Getirilecek araç özelliğinin ID'si.</param>
    /// <returns>Belirli araç özelliğinin bilgileri.</returns>
    public async Task<CarSpecificationDTO> GetByIdAsync(int carSpecificationId)
    {
        var carSpecification = await _carSpecificationRepository.GetByIdAsync(carSpecificationId);
        return _mapper.Map<CarSpecificationDTO>(carSpecification);
    }

    /// <summary>
    /// Bir araç özelliğini günceller.
    /// </summary>
    /// <param name="carSpecificationCreateUpdateDTO">Güncellenecek araç özelliği bilgileri.</param>
    /// <returns>Güncellenen araç özelliği bilgileri.</returns>
    public async Task<CarSpecificationCreateUpdateDTO> Update(CarSpecificationCreateUpdateDTO carSpecificationCreateUpdateDTO)
    {
        var carSpecification = _mapper.Map<CarSpecification>(carSpecificationCreateUpdateDTO);

        var updatedCarSpecification = await _carSpecificationRepository.Update(carSpecification);
        if (updatedCarSpecification != null)
        {
            return _mapper.Map<CarSpecificationCreateUpdateDTO>(updatedCarSpecification);
        }
        return null;
    }
}