using FutebolApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=futebolapi.db"));
var app = builder.Build();

// GET Times
app.MapGet("/times", async (AppDbContext db) =>
{
    return await db.Times.ToListAsync();
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
    return Results.Created($"O time {novoTime.Nome} foi criado com sucesso!", novoTime);
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

//GET Jogadores
app.MapGet("/jogadores", async (AppDbContext db) =>
{
    return await db.Jogadores.ToListAsync();
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
    return Results.Created($"O jogador {novoJogador.Nome} foi criado com sucesso!", novoJogador);
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

app.Run();
