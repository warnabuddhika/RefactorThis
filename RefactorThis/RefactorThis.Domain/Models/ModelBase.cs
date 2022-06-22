using System.ComponentModel.DataAnnotations;

namespace RefactorThis.Domain.Models
{
    public class ModelBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public int? CreatedUserId { get; set; } = 0;

        public int? UpdatedUserId { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
