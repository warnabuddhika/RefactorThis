using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RefactorThis.Core.Common.ViewModels
{
    public class ProductVm
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore]
        public bool IsNew { get; }
        

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public int? CreatedUserId { get; set; } = 0;

        public int? UpdatedUserId { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
