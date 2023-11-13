
using ReservaHotel.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();


//adiciona um contexto --- obejto que fornece conexao com o banco
builder.Services.AddDbContext<BDContext>();



var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opcoes => opcoes
    .WithOrigins("http://localhost:4200")
    .AllowAnyHeader().AllowAnyMethod().AllowCredentials());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
