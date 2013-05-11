using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Sunshine.Business.Account;

namespace Sunshine.Business.Core
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<UserInternal> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductPriceType> ProductPriceTypes { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<PriceInterval> PriceIntervals { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }

        public DbSet<Company> Companys { get; set; }


        public DbSet<SecurityGroup> SecurityGroups { get; set; }
        public DbSet<RoleSecurityGroup> RoleSecurityGroups { get; set; }
    }
}
