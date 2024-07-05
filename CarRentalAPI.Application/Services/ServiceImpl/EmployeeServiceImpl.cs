using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Çalışan CRUD operasyonları için servis sınıfı.
/// </summary>
public class EmployeeServiceImpl : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// EmployeeServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="employeeRepository">Çalışan deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public EmployeeServiceImpl(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir çalışan ekler.
    /// </summary>
    /// <param name="employeeCreateUpdateDTO">Eklenecek çalışan bilgileri.</param>
    /// <returns>Eklenen çalışan bilgileri.</returns>
    public async Task<EmployeeCreateUpdateDTO> Add(EmployeeCreateUpdateDTO employeeCreateUpdateDTO)
    {
        employeeCreateUpdateDTO.isDeleted = false;
        var employee = _mapper.Map<Employee>(employeeCreateUpdateDTO);
        var addedEmployee = await _employeeRepository.Add(employee);
        return _mapper.Map<EmployeeCreateUpdateDTO>(addedEmployee);
    }

    /// <summary>
    /// Bir çalışanı siler.
    /// </summary>
    /// <param name="employeeId">Silinecek çalışanın ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int employeeId)
    {
        var existingEmployee = await _employeeRepository.GetByIdAsync(employeeId);
        if (existingEmployee != null)
        {
            existingEmployee.isDeleted = true;
            return await _employeeRepository.Delete(existingEmployee);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Tüm çalışanları getirir.
    /// </summary>
    /// <returns>Tüm çalışanların listesi.</returns>
    public async Task<IEnumerable<EmployeeDTO>> GetAllAsync()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
    }

    /// <summary>
    /// Belirli bir çalışanı ID ile getirir.
    /// </summary>
    /// <param name="employeeId">Getirilecek çalışanın ID'si.</param>
    /// <returns>Belirli çalışanın bilgileri.</returns>
    public async Task<EmployeeDTO> GetByIdAsync(int employeeId)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId);
        return _mapper.Map<EmployeeDTO>(employee);
    }

    /// <summary>
    /// Bir çalışanı günceller.
    /// </summary>
    /// <param name="employeeCreateUpdateDTO">Güncellenecek çalışan bilgileri.</param>
    /// <returns>Güncellenen çalışan bilgileri.</returns>
    public async Task<EmployeeCreateUpdateDTO> Update(EmployeeCreateUpdateDTO employeeCreateUpdateDTO)
    {
        var employee = _mapper.Map<Employee>(employeeCreateUpdateDTO);

        var updatedEmployee = await _employeeRepository.Update(employee);
        if (updatedEmployee != null)
        {
            return _mapper.Map<EmployeeCreateUpdateDTO>(updatedEmployee);
        }
        return null;
    }
}