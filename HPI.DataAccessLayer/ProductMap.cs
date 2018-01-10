using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HPI.BusinessEntities;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPI.DataAccessLayer
{
    public class ProductMap: EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // ....
           
            Property(x => x.ProductCode).IsRequired().HasColumnAnnotation("Index_PCode", new IndexAnnotation(new[] { new IndexAttribute("Index_PCode") { IsUnique = true } }));
        }
    
    }
}
