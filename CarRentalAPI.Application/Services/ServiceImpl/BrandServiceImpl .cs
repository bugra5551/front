using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Marka CRUD operasyonları için servis sınıfı.
/// </summary>
public class BrandServiceImpl : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// BrandServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="brandRepository">Marka deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public BrandServiceImpl(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir marka ekler.
    /// </summary>
    /// <param name="brandCreateUpdateDto">Eklenecek marka bilgileri.</param>
    /// <returns>Eklenen marka bilgileri.</returns>
    public async Task<BrandCreateUpdateDTO> Add(BrandCreateUpdateDTO brandCreateUpdateDTO)
    {
        brandCreateUpdateDTO.isDeleted = false;
        var brand = _mapper.Map<Brand>(brandCreateUpdateDTO);
        var addedBrand = await _brandRepository.Add(brand);
        return _mapper.Map<BrandCreateUpdateDTO>(addedBrand);
    }

    /// <summary>
    /// Bir markayı siler.
    /// </summary>
    /// <param name="brandId">Silinecek markanın ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int brandId)
    {
        var existingBrand = await _brandRepository.GetByIdAsync(brandId);
        if (existingBrand != null)
        {
            existingBrand.isDeleted = true;
            return await _brandRepository.Delete(existingBrand);
        }
        return false;
    }

    /// <summary>
    /// Tüm markaları getirir.
    /// </summary>
    /// <returns>Tüm markaların listesi.</returns>
    public async Task<IEnumerable<BrandDTO>> GetAllAsync()
    {
        var brands = await _brandRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BrandDTO>>(brands);
    }

    /// <summary>
    /// Belirli bir markayı ID ile getirir.
    /// </summary>
    /// <param name="brandId">Getirilecek markanın ID'si.</param>
    /// <returns>Belirli markanın bilgileri.</returns>
    public async Task<BrandDTO> GetByIdAsync(int brandId)
    {
        var brand = await _brandRepository.GetByIdAsync(brandId);
        return _mapper.Map<BrandDTO>(brand);
    }

    /// <summary>
    /// Bir markayı günceller.
    /// </summary>
    /// <param name="brandCreateUpdateDto">Güncellenecek marka bilgileri.</param>
    /// <returns>Güncellenen marka bilgileri.</returns>
    public async Task<BrandCreateUpdateDTO> Update(BrandCreateUpdateDTO brandCreateUpdateDTO)
    {
        var brand = _mapper.Map<Brand>(brandCreateUpdateDTO);

        var updatedBrand = await _brandRepository.Update(brand);
        if (updatedBrand != null)
        {
            return _mapper.Map<BrandCreateUpdateDTO>(updatedBrand);
        }
        return null;
    }
}
