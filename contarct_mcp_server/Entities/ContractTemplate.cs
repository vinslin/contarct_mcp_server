namespace ContractMcpServer.Entities;

public class ContractTemplate
{
    public int Id { get; set; }
    public string TemplateCode { get; set; } = string.Empty;
    public string TemplateName { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateOnly EffectiveDate { get; set; }
    public DateTime CreatedDate { get; set; }

    public ICollection<ContractSection> Sections { get; set; } = new List<ContractSection>();
    public ICollection<RequiredClause> RequiredClauses { get; set; } = new List<RequiredClause>();
}
