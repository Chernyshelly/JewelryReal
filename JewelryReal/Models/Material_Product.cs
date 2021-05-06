using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryReal.Models
{
    public class Material_Product
    {
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public Material Product { get; set; }
        public int MaterialID { get; set; }
        [ForeignKey("MaterialID")]
        public Material Material { get; set; }
    }
}
