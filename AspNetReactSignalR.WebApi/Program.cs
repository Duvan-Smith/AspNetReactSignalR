using AspNetReactSignalR.WebApi.Hubs;

string MiCors = "MiCords";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => options.AddPolicy(name: MiCors, builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .WithOrigins(new[]
        {
            "http://localhost:3000",
        })
        .SetIsOriginAllowedToAllowWildcardSubdomains();
}));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

app.UseCors(MiCors);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
