using demoLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace demoLibrary.Data
{
    public class dataContext : DbContext
    {
        public dataContext() : base("name=libraryDBConnectionString")
        {
        }
        public DbSet<registration> registrations { get; set; }
        public DbSet<assetLibrary> assetLibraries { get; set; }
        public DbSet<assetLibraryManagement> assetLibraryManagements { get; set; }
    }
}