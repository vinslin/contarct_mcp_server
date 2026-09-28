using ContractMcpServer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContractMcpServer.Data;

public class ContractMcpDbContext : DbContext
{
    public ContractMcpDbContext(DbContextOptions<ContractMcpDbContext> options) : base(options) { }

    public DbSet<ContractTemplate> ContractTemplates => Set<ContractTemplate>();
    public DbSet<ContractSection> ContractSections => Set<ContractSection>();
    public DbSet<RequiredClause> RequiredClauses => Set<RequiredClause>();
    public DbSet<ClauseRequirement> ClauseRequirements => Set<ClauseRequirement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContractTemplate>(e =>
        {
            e.ToTable("ContractTemplates");
            e.HasKey(x => x.Id);
            e.Property(x => x.TemplateCode).HasColumnType("VARCHAR(100)").IsRequired();
            e.Property(x => x.TemplateName).HasColumnType("VARCHAR(200)").IsRequired();
            e.Property(x => x.Version).HasColumnType("VARCHAR(50)").IsRequired();
            e.Property(x => x.Description).HasColumnType("VARCHAR(1000)");
            e.Property(x => x.IsActive).HasColumnType("BIT").IsRequired();
            e.Property(x => x.EffectiveDate).HasColumnType("DATE").IsRequired();
            e.Property(x => x.CreatedDate).HasColumnType("DATETIME2").IsRequired();
        });

        modelBuilder.Entity<ContractSection>(e =>
        {
            e.ToTable("ContractSections");
            e.HasKey(x => x.Id);
            e.Property(x => x.SectionCode).HasColumnType("VARCHAR(100)").IsRequired();
            e.Property(x => x.SectionName).HasColumnType("VARCHAR(200)").IsRequired();
            e.Property(x => x.SectionOrder).HasColumnType("INT").IsRequired();
            e.Property(x => x.IsRequired).HasColumnType("BIT").IsRequired();
            e.Property(x => x.Description).HasColumnType("VARCHAR(1000)");
            e.Property(x => x.CreatedDate).HasColumnType("DATETIME2").IsRequired();
            e.HasOne(x => x.Template)
             .WithMany(t => t.Sections)
             .HasForeignKey(x => x.TemplateId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RequiredClause>(e =>
        {
            e.ToTable("RequiredClauses");
            e.HasKey(x => x.Id);
            e.Property(x => x.ClauseCode).HasColumnType("VARCHAR(100)").IsRequired();
            e.Property(x => x.ClauseName).HasColumnType("VARCHAR(200)").IsRequired();
            e.Property(x => x.IsRequired).HasColumnType("BIT").IsRequired();
            e.Property(x => x.Description).HasColumnType("VARCHAR(1000)");
            e.Property(x => x.CreatedDate).HasColumnType("DATETIME2").IsRequired();
            e.HasOne(x => x.Template)
             .WithMany(t => t.RequiredClauses)
             .HasForeignKey(x => x.TemplateId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<ClauseRequirement>(e =>
        {
            e.ToTable("ClauseRequirements");
            e.HasKey(x => x.Id);
            e.Property(x => x.RequirementCode).HasColumnType("VARCHAR(100)").IsRequired();
            e.Property(x => x.RequirementName).HasColumnType("VARCHAR(200)").IsRequired();
            e.Property(x => x.RequirementDescription).HasColumnType("VARCHAR(2000)").IsRequired();
            e.Property(x => x.IsMandatory).HasColumnType("BIT").IsRequired();
            e.Property(x => x.CreatedDate).HasColumnType("DATETIME2").IsRequired();
            e.HasOne(x => x.RequiredClause)
             .WithMany(rc => rc.ClauseRequirements)
             .HasForeignKey(x => x.RequiredClauseId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
