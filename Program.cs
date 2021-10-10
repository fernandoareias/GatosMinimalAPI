using GatosMinimal.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("v1/gatos", (Context context) => {
    var resultado = context.Gatos.ToList();
    if (resultado == null) return Results.NotFound();
    return Results.Ok(resultado);
}).Produces<List<Gato>>();

app.MapGet("v1/gatos/{idGato}", (Guid idGato, Context context) => {
    var resultado = context.Gatos.FirstOrDefault(g => g.Id.Equals(idGato));
    if (resultado == null) return Results.NotFound();
    return Results.Ok(resultado);
}).Produces<Gato>();

app.MapPost("v1/gatos", (Context context, Gato gato) => {
    context.Gatos.Add(gato);
    context.SaveChangesAsync();

    return Results.Created($"/v1/gatos/{gato.Id}", gato);
}).Produces<Gato>();

app.MapPut("v1/gatos/{idGato}", (Guid idGato, Context context, Gato gato) => {
    context.Gatos.Update(gato);
    context.SaveChangesAsync();
    return Results.Ok(gato);
}).Produces<Gato>();

app.MapDelete("v1/gatos/{idGato}", (Guid idGato, Context context) => {
    var entity = context.Gatos.FirstOrDefault(g => g.Id.Equals(idGato));
    if (entity == null) return Results.NotFound();

    context.Gatos.Remove(entity);
    context.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
