using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.DTOs.Requests
{
    public class AddCategoryRequestDto
    {
        public required string Name { get; set; }
    }
}
