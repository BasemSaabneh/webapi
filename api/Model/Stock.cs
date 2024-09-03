using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
//https://www.youtube.com/watch?v=F_b5wjkpg8M&list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc&index=20

namespace api.Model
{
    public class Stock
    {
        public int Id { get; set; }
        public string  Symbol { get; set; } = string.Empty;
        public string  CompanyName { get; set; } = string.Empty;
        [Column(TypeName ="decimal(18,2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName ="decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; }=string.Empty;
        public long MarketCap {get; set;}

        public List<Comment> Comments {get; set;} //= new List<Comment>();
    }
}