namespace ContractMcpServer.Models;

public class ContractStructureResponse
{
    public int TemplateId { get; set; }
    public List<SectionDto> Sections { get; set; } = new();
}

public class SectionDto
{
    public string SectionCode { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public int Order { get; set; }
    public bool Required { get; set; }
    public string? Description { get; set; }
}
