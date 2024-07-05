using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Araç CRUD operasyonları için servis sınıfı.
/// </summary>
public class CarServiceImpl : ICarService
{
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// CarServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="carRepository">Araç deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public CarServiceImpl(ICarRepository carRepository, IMapper mapper)
    {
        _carRepository = carRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir araç ekler.
    /// </summary>
    /// <param name="carCreateUpdateDTO">Eklenecek araç bilgileri.</param>
    /// <returns>Eklenen araç bilgileri.</returns>
    public async Task<CarCreateUpdateDTO> Add(CarCreateUpdateDTO carCreateUpdateDTO)
    {
        carCreateUpdateDTO.isDeleted = false;
        var car = _mapper.Map<Car>(carCreateUpdateDTO);
        var addedCar = await _carRepository.Add(car);
        return _mapper.Map<CarCreateUpdateDTO>(addedCar);
    }

    /// <summary>
    /// Bir aracı siler.
    /// </summary>
    /// <param name="carId">Silinecek aracın ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int carId)
    {
        var existingCar = await _carRepository.GetByIdAsync(carId);

        if (existingCar != null)
        {
            existingCar.isDeleted = true;
            return await _carRepository.Delete(existingCar);
        }
        return false;
    }

    /// <summary>
    /// Tüm araçları getirir.
    /// </summary>
    /// <returns>Tüm araçların listesi.</returns>
    public async Task<IEnumerable<CarDTO>> GetAllAsync()
    {
        var cars = await _carRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CarDTO>>(cars);
    }

    /// <summary>
    /// Belirli bir aracı ID ile getirir.
    /// </summary>
    /// <param name="carId">Getirilecek aracın ID'si.</param>
    /// <returns>Belirli aracın bilgileri.</returns>
    public async Task<CarDTO> GetByIdAsync(int carId)
    {
        var car = await _carRepository.GetByIdAsync(carId);
        return _mapper.Map<CarDTO>(car);
    }

    /// <summary>
    /// Bir aracı günceller.
    /// </summary>
    /// <param name="carCreateUpdateDTO">Güncellenecek araç bilgileri.</param>
    /// <returns>Güncellenen araç bilgileri.</returns>
    public async Task<CarCreateUpdateDTO> Update(CarCreateUpdateDTO carCreateUpdateDTO)
    {
        var car = _mapper.Map<Car>(carCreateUpdateDTO);

        var updatedCar = await _carRepository.Update(car);
        if (updatedCar != null)
        {
            return _mapper.Map<CarCreateUpdateDTO>(updatedCar);
        }
        return null;
    }
}