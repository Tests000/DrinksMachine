using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinksMachine
{
    public class DrinksMachineDBContext:DbContext
    {
        public DrinksMachineDBContext(DbContextOptions<DrinksMachineDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Models.Coin> Coins { get; set; }
        public DbSet<Models.Drink> Drinks { get; set; }
    }
}
