var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicionei o cors aqui, se no front-end mudar o url tem que alterar aqui também
builder.Services.AddCors(options => {
    options.AddPolicy("MinhaPoliticaCORS", builder => {
        builder.WithOrigins("http://localhost:5173")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("MinhaPoliticaCORS");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
