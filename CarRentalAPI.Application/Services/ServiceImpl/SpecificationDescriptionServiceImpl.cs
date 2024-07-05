using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Özellik açıklaması CRUD operasyonları için servis sınıfı.
/// </summary>
public class SpecificationDescriptionServiceImpl : ISpecificationDescriptionService
{
    private readonly ISpecificationDescriptionRepository _specificationDescriptionRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// SpecificationDescriptionServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="specificationDescriptionRepository">Özellik açıklaması deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public SpecificationDescriptionServiceImpl(ISpecificationDescriptionRepository specificationDescriptionRepository, IMapper mapper)
    {
        _specificationDescriptionRepository = specificationDescriptionRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir özellik açıklaması ekler.
    /// </summary>
    /// <param name="specificationDescriptionCreateUpdateDTO">Eklenecek özellik açıklaması bilgileri.</param>
    /// <returns>Eklenen özellik açıklaması bilgileri.</returns>
    public async Task<SpecificationDescriptionCreateUpdateDTO> Add(SpecificationDescriptionCreateUpdateDTO specificationDescriptionCreateUpdateDTO)
    {
        specificationDescriptionCreateUpdateDTO.isDeleted = false;
        var specificationDescription = _mapper.Map<SpecificationDescription>(specificationDescriptionCreateUpdateDTO);
        var addedSpecificationDescription = await _specificationDescriptionRepository.Add(specificationDescription);
        return _mapper.Map<SpecificationDescriptionCreateUpdateDTO>(addedSpecificationDescription);
    }

    /// <summary>
    /// Bir özellik açıklamasını siler.
    /// </summary>
    /// <param name="specificationDescriptionId">Silinecek özellik açıklamasının ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int specificationDescriptionId)
    {
        var existingSpecificationDescription = await _specificationDescriptionRepository.GetByIdAsync(specificationDescriptionId);

        if (existingSpecificationDescription != null)
        {
            existingSpecificationDescription.isDeleted = true;
            return await _specificationDescriptionRepository.Delete(existingSpecificationDescription);
        }
        return false;
    }

    /// <summary>
    /// Tüm özellik açıklamalarını getirir.
    /// </summary>
    /// <returns>Tüm özellik açıklamalarının listesi.</returns>
    public async Task<IEnumerable<SpecificationDescriptionDTO>> GetAllAsync()
    {
        var specificationDescriptions = await _specificationDescriptionRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SpecificationDescriptionDTO>>(specificationDescriptions);
    }

    /// <summary>
    /// Belirli bir özellik açıklamasını ID ile getirir.
    /// </summary>
    /// <param name="specificationDescriptionId">Getirilecek özellik açıklamasının ID'si.</param>
    /// <returns>Belirli özellik açıklamasının bilgileri.</returns>
    public async Task<SpecificationDescriptionDTO> GetByIdAsync(int specificationDescriptionId)
    {
        var specificationDescription = await _specificationDescriptionRepository.GetByIdAsync(specificationDescriptionId);
        return _mapper.Map<SpecificationDescriptionDTO>(specificationDescription);
    }

    /// <summary>
    /// Bir özellik açıklamasını günceller.
    /// </summary>
    /// <param name="specificationDescriptionCreateUpdateDTO">Güncellenecek özellik açıklaması bilgileri.</param>
    /// <returns>Güncellenen özellik açıklaması bilgileri.</returns>
    public async Task<SpecificationDescriptionCreateUpdateDTO> Update(SpecificationDescriptionCreateUpdateDTO specificationDescriptionCreateUpdateDTO)
    {
        var specificationDescription = _mapper.Map<SpecificationDescription>(specificationDescriptionCreateUpdateDTO);

        var updatedSpecificationDescription = await _specificationDescriptionRepository.Update(specificationDescription);
        if (updatedSpecificationDescription != null)
        {
            return _mapper.Map<SpecificationDescriptionCreateUpdateDTO>(updatedSpecificationDescription);
        }
        return null;
    }
}