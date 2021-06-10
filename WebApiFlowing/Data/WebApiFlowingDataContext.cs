using System;
using Microsoft.EntityFrameworkCore;
using WebApiFlowing.Data.Interfaces;
using WebApiFlowing.Data.Models;

namespace WebApiFlowing.Data
{
    public class WebApiFlowingDataContext : DbContext, IDataContext
    {
        public Guid SessionId { get; }

        public virtual DbSet<User> Users { get; set; }

        public WebApiFlowingDataContext(DbContextOptions<WebApiFlowingDataContext> options) : base(options)
        {
            SessionId = Guid.NewGuid();
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<User>()
                .HasIndex(x => x.Guid)
                .HasDatabaseName("IX_Guid")
                .IsUnique();

            mb.Entity<WeightHistory>(entity =>
            {
                entity.HasIndex(e => new { e.DateOfMeasurement, e.UserId })
                    .HasDatabaseName("UQ_DateAndUserId")
                    .IsUnique();
            });
        }
    }
}