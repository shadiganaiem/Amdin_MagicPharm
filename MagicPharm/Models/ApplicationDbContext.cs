namespace MagicPharm.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WatchBrand> WatchBrands { get; set; }
        public virtual DbSet<WatchOrder> WatchOrders { get; set; }
        public virtual DbSet<WatchRepair> WatchRepairs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(e => e.WatchOrders)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.WatchRepairs)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status>()
                .HasMany(e => e.WatchRepairs)
                .WithRequired(e => e.Status)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WatchBrand>()
                .HasMany(e => e.WatchOrders)
                .WithRequired(e => e.WatchBrand)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<WatchBrand>()
            //    .HasMany(e => e.WatchRepairs)
            //    .WithRequired(e => e.WatchBrand)
            //    .WillCascadeOnDelete(false);
        }
    }
}
