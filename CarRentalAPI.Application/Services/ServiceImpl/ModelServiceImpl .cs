using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Araç modeli CRUD operasyonları için servis sınıfı.
/// </summary>
public class ModelServiceImpl : IModelService
{
    private readonly IModelRepository _modelRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// ModelServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="modelRepository">Araç modeli deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public ModelServiceImpl(IModelRepository modelRepository, IMapper mapper)
    {
        _modelRepository = modelRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir araç modeli ekler.
    /// </summary>
    /// <param name="modelCreateUpdateDTO">Eklenecek araç modeli bilgileri.</param>
    /// <returns>Eklenen araç modeli bilgileri.</returns>
    public async Task<ModelCreateUpdateDTO> Add(ModelCreateUpdateDTO modelCreateUpdateDTO)
    {
        modelCreateUpdateDTO.isDeleted = false;
        var model = _mapper.Map<Model>(modelCreateUpdateDTO);
        var addedModel = await _modelRepository.Add(model);
        return _mapper.Map<ModelCreateUpdateDTO>(addedModel);
    }

    /// <summary>
    /// Bir araç modelini siler.
    /// </summary>
    /// <param name="modelId">Silinecek araç modelinin ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int modelId)
    {
        var existingModel = await _modelRepository.GetByIdAsync(modelId);
        if (existingModel != null)
        {
            existingModel.isDeleted = true;
            return await _modelRepository.Delete(existingModel);
        }
        else { return false; }
    }

    /// <summary>
    /// Tüm araç modellerini getirir.
    /// </summary>
    /// <returns>Tüm araç modellerinin listesi.</returns>
    public async Task<IEnumerable<ModelDTO>> GetAllAsync()
    {
        var models = await _modelRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ModelDTO>>(models);
    }

    /// <summary>
    /// Belirli bir araç modelini ID ile getirir.
    /// </summary>
    /// <param name="modelId">Getirilecek araç modelinin ID'si.</param>
    /// <returns>Belirli araç modelinin bilgileri.</returns>
    public async Task<ModelDTO> GetByIdAsync(int modelId)
    {
        var model = await _modelRepository.GetByIdAsync(modelId);
        return _mapper.Map<ModelDTO>(model);
    }

    /// <summary>
    /// Belirli bir markaya ait araç modellerini getirir.
    /// </summary>
    /// <param name="brandId">Marka ID'si.</param>
    /// <returns>Belirli markaya ait araç modellerinin listesi.</returns>
    public async Task<IEnumerable<ModelDTO>> GetModelsByBrandIdAsync(int brandId)
    {
        var models = await _modelRepository.GetModelsByBrandIdAsync(brandId);
        return _mapper.Map<IEnumerable<ModelDTO>>(models);
    }

    /// <summary>
    /// Bir araç modelini günceller.
    /// </summary>
    /// <param name="modelCreateUpdateDTO">Güncellenecek araç modeli bilgileri.</param>
    /// <returns>Güncellenen araç modeli bilgileri.</returns>
    public async Task<ModelCreateUpdateDTO> Update(ModelCreateUpdateDTO modelCreateUpdateDTO)
    {
        var model = _mapper.Map<Model>(modelCreateUpdateDTO);

        var updatedModel = await _modelRepository.Update(model);
        if (updatedModel != null)
        {
            return _mapper.Map<ModelCreateUpdateDTO>(updatedModel);
        }
        return null;
    }
}
