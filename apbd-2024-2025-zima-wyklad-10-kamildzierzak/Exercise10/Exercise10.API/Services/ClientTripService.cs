using Exercise10.API.DTO;
using Exercise10.API.Models;
using Exercise10.API.Repositories;

namespace Exercise10.API.Services;

public interface IClientTripService
{
    Task<(bool Success, string Message)> AssignClientToTripAsync(int idTrip, AssignClientToTripDto assignClientToTripDto);
}

public class ClientTripService : IClientTripService
{
    private readonly IClientRepository _clientRepository;
    private readonly IClientTripRepository _clientTripRepository;
    private readonly ITripRepository _tripRepository;

    public ClientTripService(IClientRepository clientRepository, IClientTripRepository clientTripRepository, ITripRepository tripRepository)
    {
        _clientRepository = clientRepository;
        _clientTripRepository = clientTripRepository;
        _tripRepository = tripRepository;
    }

    public async Task<(bool Success, string Message)> AssignClientToTripAsync(int idTrip, AssignClientToTripDto assignClientToTripDto)
    {
        // 1. Let's check if trip exists
        var trip = await _tripRepository.GetTripByIdAsync(idTrip);
        if (trip == null) return (false, "Trip not found.");

        // 2. Now, we need to check if trip already started or is in the past
        if (trip.DateFrom <= DateTime.Now)
        {
            return (false, "The trip has already started or is in the past.");
        }

        // 3. Does the client exists?
        var client = await _clientRepository.GetClientByPeselAsync(assignClientToTripDto.Pesel);

        if (client == null)
        {
            client = new Client
            {
                FirstName = assignClientToTripDto.FirstName,
                LastName = assignClientToTripDto.LastName,
                Email = assignClientToTripDto.Email,
                Telephone = assignClientToTripDto.Telephone,
                Pesel = assignClientToTripDto.Pesel
            };
            client.IdClient = await _clientRepository.AddClientAsync(client);
        }

        // 4. Check if client is already assigned to the trip
        if (await _clientTripRepository.IsClientAssignedToTripAsync(client.IdClient, idTrip))
        {
            return (false, "Client is already assigned to this trip.");
        }

        // 5. Finally assign client to trip
        var registeredAt = DateTime.Now;
        await _clientTripRepository.AssignClientToTripAsync(client.IdClient, trip.IdTrip, registeredAt, assignClientToTripDto.PaymentDate);

        return (true, "Client successfully assigned to the trip.");
    }
}

