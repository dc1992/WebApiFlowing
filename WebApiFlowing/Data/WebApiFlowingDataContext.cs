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
        }
    }
}