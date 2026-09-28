using ContractMcpServer.Models;

namespace ContractMcpServer.Services;

public interface IContractTemplateService
{
    Task<LatestContractTemplateResponse?> GetLatestContractTemplateAsync(CancellationToken cancellationToken = default);
}
