using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Araç hasar detayları CRUD operasyonları için servis sınıfı.
/// </summary>
public class CarDamageDetailServiceImpl : ICarDamageDetailService
{
    private readonly ICarDamageDetailRepository _carDamageDetailRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// CarDamageDetailServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="carDamageDetailRepository">Araç hasar detayları deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public CarDamageDetailServiceImpl(ICarDamageDetailRepository carDamageDetailRepository, IMapper mapper)
    {
        _carDamageDetailRepository = carDamageDetailRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir araç hasar detayı ekler.
    /// </summary>
    /// <param name="carDamageDetailCreateUpdateDTO">Eklenecek araç hasar detayı bilgileri.</param>
    /// <returns>Eklenen araç hasar detayı bilgileri.</returns>
    public async Task<CarDamageDetailCreateUpdateDTO> Add(CarDamageDetailCreateUpdateDTO carDamageDetailCreateUpdateDTO)
    {
        carDamageDetailCreateUpdateDTO.isDeleted = false;
        var carDamageDetail = _mapper.Map<CarDamageDetail>(carDamageDetailCreateUpdateDTO);
        var addedCarDamageDetail = await _carDamageDetailRepository.Add(carDamageDetail);
        return _mapper.Map<CarDamageDetailCreateUpdateDTO>(addedCarDamageDetail);
    }

    /// <summary>
    /// Bir araç hasar detayını siler.
    /// </summary>
    /// <param name="carDamageDetaiId">Silinecek araç hasar detayının ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>

    public async Task<bool> Delete(int carDamageDetaiId)
    {
        var existingCarDamageDetail = await _carDamageDetailRepository.GetByIdAsync(carDamageDetaiId);
        if (existingCarDamageDetail != null)
        {
            existingCarDamageDetail.isDeleted = true;
            return await _carDamageDetailRepository.Delete(existingCarDamageDetail);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Tüm araç hasar detaylarını getirir.
    /// </summary>
    /// <returns>Tüm araç hasar detaylarının listesi.</returns>
    public async Task<IEnumerable<CarDamageDetailDTO>> GetAllAsync()
    {
        var carDamageDetails = await _carDamageDetailRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CarDamageDetailDTO>>(carDamageDetails);
    }

    /// <summary>
    /// Belirli bir araç hasar detayını ID ile getirir.
    /// </summary>
    /// <param name="carDamageDetailId">Getirilecek araç hasar detayının ID'si.</param>
    /// <returns>Belirli araç hasar detayının bilgileri.</returns>
    public async Task<CarDamageDetailDTO> GetByIdAsync(int carDamageDetailId)
    {
        var carDamageDetail = await _carDamageDetailRepository.GetByIdAsync(carDamageDetailId);
        return _mapper.Map<CarDamageDetailDTO>(carDamageDetail);
    }

    /// <summary>
    /// Belirli bir araca ait hasar detaylarını getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli araca ait hasar detaylarının listesi.</returns>
    public async Task<IEnumerable<CarDamageDetailDTO>> GetDetailsByCarIdAsync(int carId)
    {
        var carDamageDetails = await _carDamageDetailRepository.GetDetailsByCarIdAsync(carId);
        return _mapper.Map<IEnumerable<CarDamageDetailDTO>>(carDamageDetails);
    }

    /// <summary>
    /// Bir araç hasar detayını günceller.
    /// </summary>
    /// <param name="carDamageDetailCreateUpdateDTO">Güncellenecek araç hasar detayı bilgileri.</param>
    /// <returns>Güncellenen araç hasar detayı bilgileri.</returns>
    public async Task<CarDamageDetailCreateUpdateDTO> Update(CarDamageDetailCreateUpdateDTO carDamageDetailCreateUpdateDTO)
    {
        var carDamageDetail = _mapper.Map<CarDamageDetail>(carDamageDetailCreateUpdateDTO);
        var updatedCarDamageDetail = await _carDamageDetailRepository.Update(carDamageDetail);
        if (updatedCarDamageDetail != null)
        {
            return _mapper.Map<CarDamageDetailCreateUpdateDTO>(updatedCarDamageDetail);
        }
        return null;
    }
}
