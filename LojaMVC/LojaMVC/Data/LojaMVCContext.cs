using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using LojaMVC.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LojaMVC.Data
{
    public class LojaMVCContext : DbContext
    {
        public LojaMVCContext() : base("LojaMVC")
        {

        }

        public DbSet<Produto> Produto { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}