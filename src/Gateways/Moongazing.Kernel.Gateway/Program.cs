var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
var app = builder.Build();


app.MapGet("/", () => "Allio Gateway is up and running.");

app.Run();
app.MapReverseProxy();