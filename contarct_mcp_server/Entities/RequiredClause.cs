namespace ContractMcpServer.Entities;

public class RequiredClause
{
    public int Id { get; set; }
    public int TemplateId { get; set; }
    public string ClauseCode { get; set; } = string.Empty;
    public string ClauseName { get; set; } = string.Empty;
    public bool IsRequired { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }

    public ContractTemplate Template { get; set; } = null!;
    public ICollection<ClauseRequirement> ClauseRequirements { get; set; } = new List<ClauseRequirement>();
}
