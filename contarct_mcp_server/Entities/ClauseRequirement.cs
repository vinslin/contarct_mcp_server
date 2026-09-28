namespace ContractMcpServer.Entities;

public class ClauseRequirement
{
    public int Id { get; set; }
    public int RequiredClauseId { get; set; }
    public string RequirementCode { get; set; } = string.Empty;
    public string RequirementName { get; set; } = string.Empty;
    public string RequirementDescription { get; set; } = string.Empty;
    public bool IsMandatory { get; set; }
    public DateTime CreatedDate { get; set; }

    public RequiredClause RequiredClause { get; set; } = null!;
}
