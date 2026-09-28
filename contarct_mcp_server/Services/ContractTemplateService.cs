using ContractMcpServer.Data;
using ContractMcpServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContractMcpServer.Services;

public class ContractTemplateService : IContractTemplateService
{
    private readonly ContractMcpDbContext _db;
    private readonly ILogger<ContractTemplateService> _logger;

    public ContractTemplateService(ContractMcpDbContext db, ILogger<ContractTemplateService> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task<LatestContractTemplateResponse?> GetLatestContractTemplateAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Querying latest active contract template");

        var template = await _db.ContractTemplates
            .Where(t => t.IsActive)
            .OrderByDescending(t => t.EffectiveDate)
            .ThenByDescending(t => t.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if (template is null)
        {
            _logger.LogWarning("No active contract template found");
            return null;
        }

        _logger.LogInformation("Found active template: Id={Id}, Code={Code}, Version={Version}",
            template.Id, template.TemplateCode, template.Version);

        return new LatestContractTemplateResponse
        {
            TemplateId = template.Id,
            TemplateCode = template.TemplateCode,
            TemplateName = template.TemplateName,
            Version = template.Version,
            Description = template.Description,
            EffectiveDate = template.EffectiveDate.ToString("yyyy-MM-dd")
        };
    }
}
