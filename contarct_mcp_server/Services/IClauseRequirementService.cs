using ContractMcpServer.Models;

namespace ContractMcpServer.Services;

public interface IClauseRequirementService
{
    Task<ClauseRequirementResponse?> GetClauseRequirementAsync(int templateId, string clauseCode, CancellationToken cancellationToken = default);
}
