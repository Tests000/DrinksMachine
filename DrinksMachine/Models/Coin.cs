using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinksMachine.Models
{
    public class Coin
    {
        public int Id { get; set; }
        public int Dignity { get; set; }
        public int Count { get; set; }
        public bool Enabled { get; set; }
    }
}
