using FutebolApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=futebolapi.db"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// GET Times
app.MapGet("/times", async (AppDbContext db) =>
{
    return await db.Times.AsNoTracking().ToListAsync();
});

// GET Time
app.MapGet("/times/{id}", async (int id, AppDbContext db) =>
{
    var time = await db.Times.FindAsync(id);
    return time is not null ? Results.Ok(time) : Results.NotFound("Time não encontrado");
});


//POST Time
app.MapPost("/times", async (AppDbContext db, Time novoTime) =>
{
    db.Add(novoTime);
    await db.SaveChangesAsync();
    return Results.Created($"/times/{novoTime.Id}", novoTime);
});

//PUT Times
app.MapPut("/times/{id}", async (int id, AppDbContext db, Time timeAtt) =>
{
    var time = await db.Times.FindAsync(id);
    if (time is null) return Results.NotFound("Time não encontrado");

    time.Nome = timeAtt.Nome;
    time.Cidade = timeAtt.Cidade;
    time.TitulosBrasileiros = timeAtt.TitulosBrasileiros;
    time.TitulosMundiais = timeAtt.TitulosMundiais;

    await db.SaveChangesAsync();
    return Results.Ok(time);
});

//DELETE time
app.MapDelete("/times/{id}", async (int id, AppDbContext db) =>
{
    var time = await db.Times.FindAsync(id);
    if (time is null) return Results.NotFound("Time não encontrado");

    db.Times.Remove(time);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

//GET Jogadores
app.MapGet("/jogadores", async (AppDbContext db) =>
{
    return await db.Jogadores.AsNoTracking().ToListAsync();
});

//GET Jogador
app.MapGet("/jogadores/{id}", async (int id, AppDbContext db) => {
    var jogador = await db.Jogadores.FindAsync(id);
    return jogador is not null ? Results.Ok(jogador) : Results.NotFound("Jogador não encontrado");
});

//POST Jogadores
app.MapPost("/jogadores", async (AppDbContext db, Jogador novoJogador) =>
{
    db.Add(novoJogador);
    await db.SaveChangesAsync();
    return Results.Created($"/jogadores/{novoJogador.Id}", novoJogador);
});

//PUT Jogador
app.MapPut("/jogadores/{id}", async (int id, AppDbContext db, Jogador jogadorAtt) =>
{
    var jogador = await db.Jogadores.FindAsync(id);
    if (jogador is null) return Results.NotFound("Jogador não encontrado");

    jogador.Nome = jogadorAtt.Nome;
    jogador.Idade = jogadorAtt.Idade;
    jogador.Nacionalidade = jogadorAtt.Nacionalidade;
    jogador.Time = jogadorAtt.Time;
    jogador.Salario = jogadorAtt.Salario;
    jogador.ValorMercado = jogadorAtt.ValorMercado;

    await db.SaveChangesAsync();
    return Results.Ok(jogador);
});

//DELETE Jogador
app.MapDelete("/jogadores/{id}", async (int id, AppDbContext db) =>
{
    var jogador = await db.Jogadores.FindAsync(id);
    if (jogador is null) return Results.NotFound("Jogador não encontrado");

    db.Jogadores.Remove(jogador);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
