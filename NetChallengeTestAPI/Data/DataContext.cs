﻿using Microsoft.EntityFrameworkCore;
using NetChallengeTest.Core.Models;

namespace NetChallengeTestAPI.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
