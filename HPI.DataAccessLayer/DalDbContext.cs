using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.Entity;
using System.Data.Common;
using System.Data.Entity;
using HPI.BusinessEntities;
namespace HPI.DataAccessLayer
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DalDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DalDbContext()
            : base()
        {

        }

        // Constructor to use on a DbConnection that is already opened
        public DalDbContext(DbConnection existingConnection, bool contextOwnsConnection)
              : base(existingConnection, contextOwnsConnection)
            {

            }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new ProductMap());
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
