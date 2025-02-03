using Exercise10.API.Repositories;

namespace Exercise10.API.Services;

public interface IClientService
{
    Task<(bool Success, string Message)> DeleteClientAsync(int idClient);
}

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<(bool Success, string Message)> DeleteClientAsync(int idClient)
    {
        var hasAssignedTrips = await _clientRepository.HasTripsAsync(idClient);

        if (hasAssignedTrips) return (false, "Cannot delete client. The client has assigned trips.");

        var hasBeenDeleted = await _clientRepository.DeleteClientAsync(idClient);

        return hasBeenDeleted ? (true, "Client deleted successfully.") : (false, "Client not found.");
    }
}
