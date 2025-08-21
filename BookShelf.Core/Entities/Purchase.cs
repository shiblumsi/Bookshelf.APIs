using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class Purchase
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }

        public Guid BookId { get; set; }
        public Book? Book { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public required decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public required string PaymentMethod { get; set; }
        public required string TransactionId { get; set; }
    }

}
