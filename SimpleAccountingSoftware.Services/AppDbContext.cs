using Microsoft.EntityFrameworkCore;
using SimpleAccountingSoftware.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAccountingSoftware.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
