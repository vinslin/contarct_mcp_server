using ContractMcpServer.Data;
using ContractMcpServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContractMcpServer.Services;

public class ContractStructureService : IContractStructureService
{
    private readonly ContractMcpDbContext _db;
    private readonly ILogger<ContractStructureService> _logger;

    public ContractStructureService(ContractMcpDbContext db, ILogger<ContractStructureService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<ContractStructureResponse?> GetContractStructureAsync(int templateId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MCP tool get_contract_structure called for TemplateId={TemplateId}", templateId);

        var templateExists = await _db.ContractTemplates.AnyAsync(t => t.Id == templateId, cancellationToken);
        if (!templateExists)
        {
            _logger.LogWarning("Template not found: TemplateId={TemplateId}", templateId);
            return null;
        }

        var sections = await _db.ContractSections
            .Where(s => s.TemplateId == templateId)
            .OrderBy(s => s.SectionOrder)
            .Select(s => new SectionDto
            {
                SectionCode = s.SectionCode,
                SectionName = s.SectionName,
                Order = s.SectionOrder,
                Required = s.IsRequired,
                Description = s.Description
            })
            .ToListAsync(cancellationToken);

        _logger.LogInformation("Found {Count} sections for TemplateId={TemplateId}", sections.Count, templateId);

        return new ContractStructureResponse
        {
            TemplateId = templateId,
            Sections = sections
        };
    }
}
