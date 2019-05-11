using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Core.Entities.BatTrangModel;

namespace Infrastructure.Data.BatTrang
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<FeedBack> FeedBack { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<ProductNews> ProductNews { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductBill> ProductBill { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<RelatedProduct> RelatedProduct { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
