using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyectoEF;
using proyectoEF.Models;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB")); // creamos el servicio para usar la BBDD en memoria
builder.Services.AddSqlServer<TareasContext>(builder.Configuration.GetConnectionString("cnTareas"));// creamos el servicio para usar la BBDD en sqlServer y le pasamos el nombre de la coneccion


var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) =>
{
    dbContext.Database.EnsureCreated(); // Este medoto nos asegura que la BBDD este creada y si no esta creada la crea
    return Results.Ok($"Base de datos en memoria {dbContext.Database.IsInMemory()}");

}); // vereficamoS la coneccion a la BBDD


app.MapGet("/api/tareas", async ([FromServices] TareasContext dbContext) =>
{
    return Results.Ok(dbContext.Tareas.Include(p=>p.Categoria));

});

app.MapPost("/api/tareas", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea) =>
{
    tarea.TareaId = Guid.NewGuid();
    tarea.CreatedDate = DateTime.Now;
    await dbContext.AddAsync(tarea);

    //inciamos un metod
    await dbContext.SaveChangesAsync();
    return Results.Ok();

});

app.MapPut("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromBody] Tarea tarea, [FromRoute] Guid id) =>
{

    var tareaActual = dbContext.Tareas.Find(id);

    if (tareaActual != null)
    {
      
        tareaActual.Title = tarea.Title;
        tareaActual.Descripcion = tarea.Descripcion;
        tareaActual.PriorityTask = tarea.PriorityTask;


        await dbContext.SaveChangesAsync();
        return Results.Ok();
    }

    return Results.NotFound();
});

app.MapDelete("/api/tareas/{id}", async ([FromServices] TareasContext dbContext, [FromRoute] Guid id) =>
{
    var tareaActual = dbContext.Tareas.Find(id);
    if (tareaActual != null)
    {
        dbContext.Remove(tareaActual);
        await dbContext.SaveChangesAsync();

        return Results.Ok();
    }

    return Results.NotFound();
});
app.Run();


