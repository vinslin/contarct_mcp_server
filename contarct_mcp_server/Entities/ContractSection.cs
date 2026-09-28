namespace ContractMcpServer.Entities;

public class ContractSection
{
    public int Id { get; set; }
    public int TemplateId { get; set; }
    public string SectionCode { get; set; } = string.Empty;
    public string SectionName { get; set; } = string.Empty;
    public int SectionOrder { get; set; }
    public bool IsRequired { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }

    public ContractTemplate Template { get; set; } = null!;
}
