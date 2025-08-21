using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Core.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }

        // Relation
        public ICollection<Book>? Books { get; set; }
    }
}
