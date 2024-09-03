using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class StockCreateDto
    {
        [Required]
        [MinLength(5,ErrorMessage ="Symbol must be 5 characters at least")]
        [MaxLength(280,ErrorMessage ="Symbol can not be over 10 characters")]
        public string  Symbol { get; set; } = string.Empty;
        [Required]
        [MinLength(5,ErrorMessage ="CompanyName must be 5 characters at least")]
        [MaxLength(280,ErrorMessage ="CompanyName can not be over 10 characters")]
        public string  CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,1000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(0.005,100)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(10,ErrorMessage ="Industry cannt be over 10 characters")]
        public string Industry { get; set; }=string.Empty;
        [Range(1,1000000000000)]
        public long MarketCap {get; set;} 
    }
}