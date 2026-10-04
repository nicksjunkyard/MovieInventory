using Microsoft.EntityFrameworkCore;
using MovieInventoryApi.Data;
using MovieInventoryAPI.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MovieDb>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
}
else
{
    builder.Services.AddDbContext<MovieDb>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("ProductionConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ProductionConnection")))
    );
}

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.SerializerOptions.WriteIndented = true;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

var app = builder.Build();

app.MapGet("/movies", async (MovieDb db) =>
    await db.Movies.ToListAsync());
app.MapGet("/movies/{id}", async (int id, MovieDb db) =>
    await db.Movies.FindAsync(id)
        is Movie movie
            ? Results.Ok(movie)
            : Results.NotFound());
app.MapPost("/movies", async (Movie movie, MovieDb db) =>
{
    // Handle nested entity creation
    if (movie.Directors?.Count > 0)
    {
        foreach (var director in movie.Directors)
        {
            if (director.Id == 0) // New director
            {
                db.Directors.Add(director);
            }
        }
    }
    if (movie.Actors?.Count > 0)
    {
        foreach (var actor in movie.Actors)
        {
            if (actor.Id == 0) // New actor
            {
                db.Actors.Add(actor);
            }
        }
    }
    if (movie.Actresses?.Count > 0)
    {
        foreach (var actress in movie.Actresses)
        {
            if (actress.Id == 0) // New actress
            {
                db.Actresses.Add(actress);
            }
        }
    }
    if (movie.Tags?.Count > 0)
    {
        foreach (var tag in movie.Tags)
        {
            if (tag.Id == 0) // New tag
            {
                db.Tags.Add(tag);
            }
        }
    }
    db.Movies.Add(movie);
    await db.SaveChangesAsync();
    return Results.Created($"/movies/{movie.Id}", movie);
});
app.MapPut("/movies/{id}", async (int id, Movie updatedMovie, MovieDb db) =>
{
    var movie = await db.Movies.FindAsync(id);
    if (movie is null) return Results.NotFound();
    movie.Title = updatedMovie.Title;
    movie.Description = updatedMovie.Description;
    movie.FileName = updatedMovie.FileName;
    movie.ReleaseDate = updatedMovie.ReleaseDate;
    movie.DateModified = DateTime.UtcNow;
    movie.ApiCode = updatedMovie.ApiCode;
    movie.StudioId = updatedMovie.StudioId;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/movies/{id}", async (int id, MovieDb db) =>
{
    var movie = await db.Movies.FindAsync(id);
    if (movie is null) return Results.NotFound();
    db.Movies.Remove(movie);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Studios
app.MapGet("/studios", async (MovieDb db) =>
    await db.Studios.ToListAsync());
app.MapGet("/studios/{id}", async (int id, MovieDb db) =>
    await db.Studios.FindAsync(id)
        is Studio studio
            ? Results.Ok(studio)
            : Results.NotFound());
app.MapPost("/studios", async (Studio studio, MovieDb db) =>
{
    db.Studios.Add(studio);
    await db.SaveChangesAsync();
    return Results.Created($"/studios/{studio.Id}", studio);
});
app.MapPut("/studios/{id}", async (int id, Studio updatedStudio, MovieDb db) =>
{
    var studio = await db.Studios.FindAsync(id);
    if (studio is null) return Results.NotFound();
    studio.Name = updatedStudio.Name;
    studio.URL = updatedStudio.URL;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/studios/{id}", async (int id, MovieDb db) =>
{
    var studio = await db.Studios.FindAsync(id);
    if (studio is null) return Results.NotFound();
    db.Studios.Remove(studio);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Directors
app.MapGet("/directors", async (MovieDb db) =>
    await db.Directors.ToListAsync());
app.MapGet("/directors/{id}", async (int id, MovieDb db) =>
    await db.Directors.FindAsync(id)
        is Director director
            ? Results.Ok(director)
            : Results.NotFound());
app.MapPost("/directors", async (Director director, MovieDb db) =>
{
    db.Directors.Add(director);
    await db.SaveChangesAsync();
    return Results.Created($"/directors/{director.Id}", director);
});
app.MapPut("/directors/{id}", async (int id, Director updatedDirector, MovieDb db) =>
{
    var director = await db.Directors.FindAsync(id);
    if (director is null) return Results.NotFound();
    director.FirstName = updatedDirector.FirstName;
    director.LastName = updatedDirector.LastName;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/directors/{id}", async (int id, MovieDb db) =>
{
    var director = await db.Directors.FindAsync(id);
    if (director is null) return Results.NotFound();
    db.Directors.Remove(director);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Actors
app.MapGet("/actors", async (MovieDb db) =>
    await db.Actors.ToListAsync());
app.MapGet("/actors/{id}", async (int id, MovieDb db) =>
    await db.Actors.FindAsync(id)
        is Actor actor
            ? Results.Ok(actor)
            : Results.NotFound());
app.MapPost("/actors", async (Actor actor, MovieDb db) =>
{
    db.Actors.Add(actor);
    await db.SaveChangesAsync();
    return Results.Created($"/actors/{actor.Id}", actor);
});
app.MapPut("/actors/{id}", async (int id, Actor updatedActor, MovieDb db) =>
{
    var actor = await db.Actors.FindAsync(id);
    if (actor is null) return Results.NotFound();
    actor.FirstName = updatedActor.FirstName;
    actor.LastName = updatedActor.LastName;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/actors/{id}", async (int id, MovieDb db) =>
{
    var actor = await db.Actors.FindAsync(id);
    if (actor is null) return Results.NotFound();
    db.Actors.Remove(actor);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Actresses
app.MapGet("/actresses", async (MovieDb db) =>
    await db.Actresses.ToListAsync());
app.MapGet("/actresses/{id}", async (int id, MovieDb db) =>
    await db.Actresses.FindAsync(id)
        is Actress actress
            ? Results.Ok(actress)
            : Results.NotFound());
app.MapPost("/actresses", async (Actress actress, MovieDb db) =>
{
    db.Actresses.Add(actress);
    await db.SaveChangesAsync();
    return Results.Created($"/actresses/{actress.Id}", actress);
});
app.MapPut("/actresses/{id}", async (int id, Actress updatedActress, MovieDb db) =>
{
    var actress = await db.Actresses.FindAsync(id);
    if (actress is null) return Results.NotFound();
    actress.FirstName = updatedActress.FirstName;
    actress.LastName = updatedActress.LastName;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/actresses/{id}", async (int id, MovieDb db) =>
{
    var actress = await db.Actresses.FindAsync(id);
    if (actress is null) return Results.NotFound();
    db.Actresses.Remove(actress);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Tags
app.MapGet("/tags", async (MovieDb db) =>
    await db.Tags.ToListAsync());
app.MapGet("/tags/{id}", async (int id, MovieDb db) =>
    await db.Tags.FindAsync(id)
        is Tag tag
            ? Results.Ok(tag)
            : Results.NotFound());
app.MapPost("/tags", async (Tag tag, MovieDb db) =>
{
    db.Tags.Add(tag);
    await db.SaveChangesAsync();
    return Results.Created($"/tags/{tag.Id}", tag);
});
app.MapPut("/tags/{id}", async (int id, Tag updatedTag, MovieDb db) =>
{
    var tag = await db.Tags.FindAsync(id);
    if (tag is null) return Results.NotFound();
    tag.Name = updatedTag.Name;
    tag.Synonyms = updatedTag.Synonyms;
    tag.ParentTagId = updatedTag.ParentTagId;
    await db.SaveChangesAsync();
    return Results.NoContent();
});
app.MapDelete("/tags/{id}", async (int id, MovieDb db) =>
{
    var tag = await db.Tags.FindAsync(id);
    if (tag is null) return Results.NotFound();
    db.Tags.Remove(tag);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();
