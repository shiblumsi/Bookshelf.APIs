using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelf.Application.Common
{
    public class APIServiceResponse
    {
        public string? ResponseDateTime { get; set; }
        public bool? ResponseStatus { get; set; }
        public string? SuccessMsg { get; set; }
        public int? ResponseCode { get; set; }
        public string? ErrMsg { get; set; }
        public string? RequestDateTime { get; set; }
        public object? ResponseBusinessData { get; set; }
        public int TotalRecords {  get; set; }
    }
}
