using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

public class RecipeContext : DbContext
    {
    public DbSet<RecipesData> RecipesData { get; set; }

    public string DbPath { get; }

    public RecipeContext ()
        {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Combine(path, "recipes.db");
        }

    // The following configures EF to create a SQLite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring ( DbContextOptionsBuilder optionsBuilder )
        {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
        optionsBuilder.UseLazyLoadingProxies();
        }
    }

public class RecipesData
    {
    [Key] // Marking Id as the primary key
    public Guid Id { get; set; }
    public string? Country { get; set; }
    public string? RecipeName { get; set; }
    public string? ImagePath { get; set; }
    public string? Instructions { get; set; }
    public string? Category { get; set; }
    }

