using ContractMcpServer.Data;
using ContractMcpServer.Services;
using ContractMcpServer.Tools;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ContractMcpDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContractMcpDb")));

// Services
builder.Services.AddScoped<IContractTemplateService, ContractTemplateService>();
builder.Services.AddScoped<IContractStructureService, ContractStructureService>();
builder.Services.AddScoped<IRequiredClauseService, RequiredClauseService>();
builder.Services.AddScoped<IClauseRequirementService, ClauseRequirementService>();

// MCP Server with HTTP transport
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithToolsFromAssembly();

builder.Logging.AddConsole();

var app = builder.Build();

app.MapMcp("/mcp");

app.Run();
