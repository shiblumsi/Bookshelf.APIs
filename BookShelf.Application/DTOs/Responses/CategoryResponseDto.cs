using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.Responses
{
    public class CategoryResponseDto
    {
        public Guid Id { get; set; }            
        public string Name { get; set; }         
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
