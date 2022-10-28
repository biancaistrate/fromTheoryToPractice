using CheckoutApp.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class DbContextMock : DbContext
    {
        public DbContextMock(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Basket> Baskets { get; set; }
       
    }
}
