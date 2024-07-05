using AutoMapper;
using CarRentalAPI.Application.DTOs.Read;
using CarRentalAPI.Domain.Entities.Entity;
using CarRentalAPI.Persistence.Repositories;

namespace CarRentalAPI.Application.Services.ServiceImpl;

/// <summary>
/// Yardım masası işlemleri için servis sınıfı.
/// </summary>
public class HelpDeskServiceImpl : IHelpDeskService
{
    private readonly IHelpDeskRepository _helpDeskRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// HelpDeskServiceImpl sınıfının kurucusu.
    /// </summary>
    /// <param name="helpDeskRepository">Yardım masası deposu.</param>
    /// <param name="mapper">AutoMapper nesnesi.</param>
    public HelpDeskServiceImpl(IHelpDeskRepository helpDeskRepository, IMapper mapper)
    {
        _helpDeskRepository = helpDeskRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Yeni bir yardım masası mesajı ekler.
    /// </summary>
    /// <param name="messageDTO">Eklenecek mesaj bilgileri.</param>
    /// <returns>Eklenen mesaj bilgileri.</returns>
    public async Task<HelpDeskCreateUpdateDTO> AddMessage(HelpDeskCreateUpdateDTO messageDTO)
    {
        var message = _mapper.Map<HelpDesk>(messageDTO);
        var addedMessage = await _helpDeskRepository.AddMessage(message);
        return _mapper.Map<HelpDeskCreateUpdateDTO>(addedMessage);
    }

    /// <summary>
    /// Belirli bir araç ve kullanıcı ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <param name="userId">Kullanıcı ID'si.</param>
    /// <returns>Belirli araç ve kullanıcıya ait mesajların listesi.</returns>
    public async Task<IEnumerable<HelpDeskDTO>> GetMessagesByCarIdAndUserId(int carId, int userId)
    {
        var messages = await _helpDeskRepository.GetMessagesByCarIdAndUserId(carId, userId);
        return _mapper.Map<IEnumerable<HelpDeskDTO>>(messages);
    }

    /// <summary>
    /// Belirli bir araç ID'sine göre mesajları getirir.
    /// </summary>
    /// <param name="carId">Araç ID'si.</param>
    /// <returns>Belirli araca ait mesajların listesi.</returns>
    public async Task<IEnumerable<HelpDeskDTO>> GetMessagesByCarId(int carId)
    {
        var messages = await _helpDeskRepository.GetMessagesByCarId(carId);
        return _mapper.Map<IEnumerable<HelpDeskDTO>>(messages);
    }
}
