using ContractMcpServer.Models;

namespace ContractMcpServer.Services;

public interface IRequiredClauseService
{
    Task<RequiredClausesResponse?> GetRequiredClausesAsync(int templateId, CancellationToken cancellationToken = default);
}
