var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Autoriser le front à appeler l'API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS 
app.UseCors("AllowFrontend");

app.UseAuthorization();

// Permet de servir mon front depuis wwwroot
app.UseStaticFiles();

// Si aucune route API ne correspond, renvoyer index.html (SPA)
app.MapFallbackToFile("index.html");

// Mappe les API
app.MapControllers();

app.Run();
