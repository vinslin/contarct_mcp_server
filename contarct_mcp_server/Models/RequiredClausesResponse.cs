namespace ContractMcpServer.Models;

public class RequiredClausesResponse
{
    public int TemplateId { get; set; }
    public List<ClauseDto> RequiredClauses { get; set; } = new();
}

public class ClauseDto
{
    public string ClauseCode { get; set; } = string.Empty;
    public string ClauseName { get; set; } = string.Empty;
    public bool Required { get; set; }
    public string? Description { get; set; }
}
