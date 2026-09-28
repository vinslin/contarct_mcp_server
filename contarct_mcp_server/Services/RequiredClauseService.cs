using ContractMcpServer.Data;
using ContractMcpServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContractMcpServer.Services;

public class RequiredClauseService : IRequiredClauseService
{
    private readonly ContractMcpDbContext _db;
    private readonly ILogger<RequiredClauseService> _logger;

    public RequiredClauseService(ContractMcpDbContext db, ILogger<RequiredClauseService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<RequiredClausesResponse?> GetRequiredClausesAsync(int templateId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MCP tool get_required_clauses called for TemplateId={TemplateId}", templateId);

        var templateExists = await _db.ContractTemplates.AnyAsync(t => t.Id == templateId, cancellationToken);
        if (!templateExists)
        {
            _logger.LogWarning("Template not found: TemplateId={TemplateId}", templateId);
            return null;
        }

        var clauses = await _db.RequiredClauses
            .Where(rc => rc.TemplateId == templateId && rc.IsRequired)
            .OrderBy(rc => rc.ClauseName)
            .Select(rc => new ClauseDto
            {
                ClauseCode = rc.ClauseCode,
                ClauseName = rc.ClauseName,
                Required = rc.IsRequired,
                Description = rc.Description
            })
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} required clauses for TemplateId={TemplateId}", clauses.Count, templateId);

        return new RequiredClausesResponse
        {
            TemplateId = templateId,
            RequiredClauses = clauses
        };
    }
}
