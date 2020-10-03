using Assetify.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assetify.Data
{
    public class AssetifyContext : DbContext
    {
        public AssetifyContext(DbContextOptions<AssetifyContext> options): base(options) { }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetOrientation> Orientations { get; set; }
        public DbSet<AssetImage> Images { get; set; }
        public DbSet<UserAsset> UserAsset { get; set; }
        public DbSet<Search> Searches { get; set; }
        //public DbSet<InvestorSearch> InvestorsSearches { get; set; }
        //public DbSet<ResidentialSearch> ResidentialsSearches { get; set; }
        //public DbSet<ResidentialSearchAddress> ResidentialSearchAddresses { get; set; }
    }
}
