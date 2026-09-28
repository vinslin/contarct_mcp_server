namespace ContractMcpServer.Models;

public class LatestContractTemplateResponse
{
    public int TemplateId { get; set; }
    public string TemplateCode { get; set; } = string.Empty;
    public string TemplateName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string EffectiveDate { get; set; } = string.Empty;
}
