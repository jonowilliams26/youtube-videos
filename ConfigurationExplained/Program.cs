using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<NameSettings>(
    builder.Configuration.GetSection("NameSettings")
);

var app = builder.Build();

// Configure App
app.UseSwagger();
app.UseSwaggerUI();

// Configure Endpoints
app.MapGet("/IConfig", async (
    IOptions<NameSettings> options,
    IOptionsSnapshot<NameSettings> optionsSnapshot,
    IOptionsMonitor<NameSettings> optionsMonitor
    ) =>
{
    var config = options.Value;
    var configSnapshot = optionsSnapshot.Value;

    await Task.Delay(10 * 1000);

    var configMonitor = optionsMonitor.CurrentValue;

    return new
    {
        Config = config,
        ConfigSnapshot = configSnapshot,
        ConfigMonitor = configMonitor
    };
});

app.Run();

public class NameSettings
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> Hobbies { get; set; } = [];
}