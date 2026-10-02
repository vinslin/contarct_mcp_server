using System.ComponentModel;
using System.Text.Json;
using ContractMcpServer.Services;
using ModelContextProtocol.Server;

namespace ContractMcpServer.Tools;

[McpServerToolType]
public class ContractTools
{
    private readonly IContractTemplateService _templateService;
    private readonly IContractStructureService _structureService;
    private readonly IRequiredClauseService _clauseService;
    private readonly IClauseRequirementService _requirementService;
    private readonly ILogger<ContractTools> _logger;

    public ContractTools(
        IContractTemplateService templateService,
        IContractStructureService structureService,
        IRequiredClauseService clauseService,
        IClauseRequirementService requirementService,
        ILogger<ContractTools> logger)
    {
        _templateService = templateService;
        _structureService = structureService;
        _clauseService = clauseService;
        _requirementService = requirementService;
        _logger = logger;
    }

    [McpServerTool(Name = "get_latest_contract_template")]
    [Description("Returns the currently active company contract template and its version. Use this tool when you need to determine which contract template/version is currently approved by the company.")]
    public async Task<string> GetLatestContractTemplate(CancellationToken cancellationToken)
    {
        _logger.LogInformation("MCP tool get_latest_contract_template called");

        var result = await _templateService.GetLatestContractTemplateAsync(cancellationToken);

        if (result is null)
            return JsonSerializer.Serialize(new { error = "No active contract template was found." });

        return JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }

    [McpServerTool(Name = "get_contract_structure")]
    [Description("Returns the expected sections, ordering, and required status for a specific contract template by default it returns latest tempate one. Use this when checking whether a contract follows the company's expected document structure.")]
    public async Task<string> GetLatestContractStructure(
        [Description("The unique ID of the contract template to retrieve sections for. Defaults to 1.")] int templateId = 1,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MCP tool get_contract_structure called for TemplateId={TemplateId}", templateId);

        var result = await _structureService.GetContractStructureAsync(templateId, cancellationToken);

        if (result is null)
            return JsonSerializer.Serialize(new { error = $"Contract template with ID {templateId} was not found." });

        return JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }

    [McpServerTool(Name = "get_required_clauses")]
    [Description("Returns the clauses that the company requires for a specific contract template by default it returns latest template one. Use this when checking whether a contract contains all company-required clauses.")]
    public async Task<string> GetLatestContractRequiredClauses(
        [Description("The unique ID of the contract template to retrieve required clauses for. Defaults to 1.")] int templateId = 1,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MCP tool get_required_clauses called for TemplateId={TemplateId}", templateId);

        var result = await _clauseService.GetRequiredClausesAsync(templateId, cancellationToken);

        if (result is null)
            return JsonSerializer.Serialize(new { error = $"Contract template with ID {templateId} was not found." });

        return JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }

    [McpServerTool(Name = "get_clause_requirement")]
    [Description("Returns the detailed company-defined requirements for a specific clause. Use this when evaluating whether the content of a clause satisfies the company's requirements.")]
    public async Task<string> GetClauseRequirement(
        [Description("The unique ID of the contract template. Defaults to 1.")] int templateId = 1,
        [Description("The clause code identifier (e.g. CONFIDENTIALITY, TERMINATION). Defaults to TERMINATION.")] string clauseCode = "TERMINATION",
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("MCP tool get_clause_requirement called for TemplateId={TemplateId}, ClauseCode={ClauseCode}",
            templateId, clauseCode);

        var result = await _requirementService.GetClauseRequirementAsync(templateId, clauseCode, cancellationToken);

        if (result is null)
            return JsonSerializer.Serialize(new { error = $"Clause '{clauseCode}' was not found for template {templateId}." });

        return JsonSerializer.Serialize(result, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
