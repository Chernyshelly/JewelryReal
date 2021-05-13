using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JewelryReal.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product_type> Product_types { get; set; }
        //public DbSet<Product_view> Product_viewss { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>()
                    .HasMany(c => c.Products)
                    .WithMany(s => s.Materials)
                    .UsingEntity(j => j.ToTable("Materials_Products"));
        }
        /*public virtual int newSale(Nullable<int> productID, Nullable<int> number_of_regular_customers_card)
        {
            var productIDParameter = productID.HasValue ?
                new ObjectParameter("ProductID", productID) :
                new ObjectParameter("ProductID", typeof(int));

            var number_of_regular_customers_cardParameter = number_of_regular_customers_card.HasValue ?
                new ObjectParameter("Number_of_regular_customers_card", number_of_regular_customers_card) :
                new ObjectParameter("Number_of_regular_customers_card", typeof(int));

            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("newSale", productIDParameter, number_of_regular_customers_cardParameter);
        }*/

    }
}
