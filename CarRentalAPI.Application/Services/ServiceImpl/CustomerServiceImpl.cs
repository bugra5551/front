using AutoMapper;
using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Müşteri CRUD operasyonları için servis sınıfı.
/// </summary>
public class CustomerServiceImpl : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// CustomerServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="customerRepository">Müşteri deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public CustomerServiceImpl(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir müşteri ekler.
    /// </summary>
    /// <param name="customerCreateUpdateDTO">Eklenecek müşteri bilgileri.</param>
    /// <returns>Eklenen müşteri bilgileri.</returns>
    public async Task<CustomerCreateUpdateDTO> Add(CustomerCreateUpdateDTO customerCreateUpdateDTO)
    {
        customerCreateUpdateDTO.isDeleted = false;
        var customer = _mapper.Map<Customer>(customerCreateUpdateDTO);
        var addedCustomer = await _customerRepository.Add(customer);
        return _mapper.Map<CustomerCreateUpdateDTO>(addedCustomer);
    }

    /// <summary>
    /// Bir müşteriyi siler.
    /// </summary>
    /// <param name="customerId">Silinecek müşterinin ID'si.</param>
    /// <returns>Silme işleminin başarılı olup olmadığını belirten boolean değer.</returns>
    public async Task<bool> Delete(int customerId)
    {
        var existingCustomer = await _customerRepository.GetByIdAsync(customerId);
        if (existingCustomer != null)
        {
            existingCustomer.isDeleted = true;
            return await _customerRepository.Delete(existingCustomer);
        }
        return false;
    }

    /// <summary>
    /// Tüm müşterileri getirir.
    /// </summary>
    /// <returns>Tüm müşterilerin listesi.</returns>
    public async Task<IEnumerable<CustomerDTO>> GetAllAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
    }

    /// <summary>
    /// Belirli bir müşteriyi ID ile getirir.
    /// </summary>
    /// <param name="customerId">Getirilecek müşterinin ID'si.</param>
    /// <returns>Belirli müşterinin bilgileri.</returns>
    public async Task<CustomerDTO> GetByIdAsync(int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        return _mapper.Map<CustomerDTO>(customer);
    }

    /// <summary>
    /// Bir müşteriyi günceller.
    /// </summary>
    /// <param name="customerCreateUpdateDTO">Güncellenecek müşteri bilgileri.</param>
    /// <returns>Güncellenen müşteri bilgileri.</returns>
    public async Task<CustomerCreateUpdateDTO> Update(CustomerCreateUpdateDTO customerCreateUpdateDTO)
    {
        var customer = _mapper.Map<Customer>(customerCreateUpdateDTO);

        var updatedCustomer = await _customerRepository.Update(customer);

        if (updatedCustomer != null)
        {
            return _mapper.Map<CustomerCreateUpdateDTO>(updatedCustomer);
        }
        return null;
    }
}