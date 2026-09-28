using ContractMcpServer.Models;

namespace ContractMcpServer.Services;

public interface IContractStructureService
{
    Task<ContractStructureResponse?> GetContractStructureAsync(int templateId, CancellationToken cancellationToken = default);
}
