using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Araç resimleri CRUD operasyonları için servis sınıfı.
/// </summary>
public class CarImageServiceImple : ICarImageService
{
    private readonly ICarImageRepository _carImageRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// CarImageServiceImple sınıfının kurucusu.
    /// </summary>
    /// <param name="carImageRepository">Araç resimleri deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public CarImageServiceImple(ICarImageRepository carImageRepository, IMapper mapper)
    {
        _carImageRepository = carImageRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Belirli bir aracın ilk resmini getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Araç resmi.</returns>
    public async Task<string> GetFirstImageBase64(int carId)
    {
        return await _carImageRepository.GetFirstImageBase64(carId);
    }

    /// <summary>
    /// Yeni bir araç resmi ekler.
    /// </summary>
    /// <param name="carImageCreateUpdateDTO">Eklenecek araç resmi bilgileri.</param>
    /// <returns>Eklenen araç resmi bilgileri.</returns>
    public async Task<CarImageCreateUpdateDTO> Add(CarImageCreateUpdateDTO carImageCreateUpdateDTO)
    {
        carImageCreateUpdateDTO.isDeleted = false;
        var carImage = _mapper.Map<CarImage>(carImageCreateUpdateDTO);
        var addedCarImage = await _carImageRepository.Add(carImage);
        return _mapper.Map<CarImageCreateUpdateDTO>(addedCarImage);
    }

    /// <summary>
    /// Bir araç resmini siler.
    /// </summary>
    /// <param name="carImageId">Silinecek araç resminin ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int carImageId)
    {
        var existingCarImage = await _carImageRepository.GetByIdAsync(carImageId);
        if (existingCarImage != null)
        {
            existingCarImage.isDeleted = true;
            return await _carImageRepository.Delete(existingCarImage);
        }
        return false;
    }

    /// <summary>
    /// Tüm araç resimlerini getirir.
    /// </summary>
    /// <returns>Tüm araç resimlerinin listesi.</returns>
    public async Task<IEnumerable<CarImageDTO>> GetAllAsync()
    {
        var carImages = await _carImageRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CarImageDTO>>(carImages);
    }

    /// <summary>
    /// Belirli bir araç resmini ID ile getirir.
    /// </summary>
    /// <param name="carImageId">Getirilecek araç resminin ID'si.</param>
    /// <returns>Belirli araç resminin bilgileri.</returns>
    public async Task<CarImageDTO> GetByIdAsync(int carImageId)
    {
        var carImage = await _carImageRepository.GetByIdAsync(carImageId);
        return _mapper.Map<CarImageDTO>(carImage);
    }

    /// <summary>
    /// Belirli bir araca ait resimleri getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli araca ait resimlerin listesi.</returns>
    public async Task<IEnumerable<CarImageDTO>> GetImagesByCarIdAsync(int carId)
    {
        var carImages = await _carImageRepository.GetImagesByCarIdAsync(carId);
        return _mapper.Map<IEnumerable<CarImageDTO>>(carImages);
    }

    /// <summary>
    /// Bir araç resmini günceller.
    /// </summary>
    /// <param name="carImageCreateUpdateDTO">Güncellenecek araç resmi bilgileri.</param>
    /// <returns>Güncellenen araç resmi bilgileri.</returns>
    public async Task<CarImageCreateUpdateDTO> Update(CarImageCreateUpdateDTO carImageCreateUpdateDTO)
    {
        var carImage = _mapper.Map<CarImage>(carImageCreateUpdateDTO);

        var updatedCarImage = await _carImageRepository.Update(carImage);
        if (updatedCarImage != null)
        {
            return _mapper.Map<CarImageCreateUpdateDTO>(updatedCarImage);
        }
        return null;
    }
}