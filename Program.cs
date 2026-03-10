using Aspnet_Crud;

// Modify as desired
const string AllowedOrigin = "http://localhost:8080";

const string CorsPolicy = "CorsPolicy";
var builder = WebApplication.CreateBuilder(args);

// CORS settings
builder.Services.AddCors(opts =>
{
    opts.AddPolicy(CorsPolicy, policy =>
    {
        policy.WithOrigins(AllowedOrigin)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors();

// ***** MAPPINGS *****

app.MapGet("/", EmployeeList.FetchPage)
    .RequireCors(CorsPolicy);

// ***** END MAPPINGS *****

app.Run();
