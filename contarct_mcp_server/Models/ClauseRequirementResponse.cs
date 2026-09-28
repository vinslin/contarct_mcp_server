namespace ContractMcpServer.Models;

public class ClauseRequirementResponse
{
    public int TemplateId { get; set; }
    public string ClauseCode { get; set; } = string.Empty;
    public string ClauseName { get; set; } = string.Empty;
    public List<RequirementDto> Requirements { get; set; } = new();
}

public class RequirementDto
{
    public string RequirementCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Mandatory { get; set; }
}
