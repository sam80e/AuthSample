using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Tokens
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
