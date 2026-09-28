using ContractMcpServer.Data;
using ContractMcpServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContractMcpServer.Services;

public class ClauseRequirementService : IClauseRequirementService
{
    private readonly ContractMcpDbContext _db;
    private readonly ILogger<ClauseRequirementService> _logger;

    public ClauseRequirementService(ContractMcpDbContext db, ILogger<ClauseRequirementService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ClauseRequirementResponse?> GetClauseRequirementAsync(int templateId, string clauseCode, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MCP tool get_clause_requirement called for TemplateId={TemplateId}, ClauseCode={ClauseCode}",
            templateId, clauseCode);

        var clause = await _db.RequiredClauses
            .Where(rc => rc.TemplateId == templateId && rc.ClauseCode == clauseCode)
            .FirstOrDefaultAsync(cancellationToken);

        if (clause is null)
        {
            _logger.LogWarning("Clause not found: ClauseCode={ClauseCode}, TemplateId={TemplateId}", clauseCode, templateId);
            return null;
        }

        var requirements = await _db.ClauseRequirements
            .Where(cr => cr.RequiredClauseId == clause.Id)
            .Select(cr => new RequirementDto
            {
                RequirementCode = cr.RequirementCode,
                Name = cr.RequirementName,
                Description = cr.RequirementDescription,
                Mandatory = cr.IsMandatory
            })
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} requirements for ClauseCode={ClauseCode}, TemplateId={TemplateId}",
            requirements.Count, clauseCode, templateId);

        return new ClauseRequirementResponse
        {
            TemplateId = templateId,
            ClauseCode = clause.ClauseCode,
            ClauseName = clause.ClauseName,
            Requirements = requirements
        };
    }
}
