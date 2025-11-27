using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// ✅ Allow browser JS from anywhere (phone/Kali HTML page)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// Use CORS
app.UseCors("AllowAll");

// Root
app.MapGet("/", () => "Fitverse .NET Productivity Tracker is Live!");

// Get all entries
app.MapGet("/entries", () => AppData.Entries);

// Get entry by date
app.MapGet("/entries/{date}", (string date) =>
{
    if (!DateTime.TryParse(date, out var parsed))
        return Results.BadRequest("Invalid date format. Use yyyy-MM-dd.");

    var entry = AppData.Entries.FirstOrDefault(e => e.Date.Date == parsed.Date);
    return entry is null ? Results.NotFound("No entry found for that date.") : Results.Ok(entry);
});

// Add or update entry
app.MapPost("/entries", (DailyEntry entry) =>
{
    entry.Date = entry.Date.Date;
    AppData.Entries.RemoveAll(e => e.Date == entry.Date);
    AppData.Entries.Add(entry);

    return Results.Ok("Entry saved/updated.");
});

app.Run();
