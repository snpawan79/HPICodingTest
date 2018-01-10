using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPI.BusinessEntities
{
    public class Product
    {
       [Key]
        public string ProductCode { get; set; }
        public int Price { get; set; }
    }
}
