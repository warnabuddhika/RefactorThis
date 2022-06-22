using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefactorThis.Domain.Models.Products
{
    public class ProductOption: ModelBase
    {    

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
       
        public bool IsNew { get; }

        public Product Product { get; set; }
    }
}
