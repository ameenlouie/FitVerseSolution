using System;
using System.Collections.Generic;

public class DailyEntry
{
    public DateTime Date { get; set; }
    public int ProductivityScore { get; set; }
    public string? Notes { get; set; }
    public int TasksDone { get; set; }
    public int WaterIntakeML { get; set; }
    public string? Mood { get; set; }
}

public static class AppData
{
    public static List<DailyEntry> Entries { get; } = new();
}
