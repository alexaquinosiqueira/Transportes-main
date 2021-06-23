using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transportadora.Business.Models;

namespace Transportadora.UI.Site.Data
{
    public class TransportadoraUISiteContext : DbContext
    {
        public TransportadoraUISiteContext (DbContextOptions<TransportadoraUISiteContext> options)
            : base(options)
        {
        }

        public DbSet<Transportadora.Business.Models.Bank> Bank { get; set; }

        public DbSet<Transportadora.Business.Models.BankAccount> BankAccount { get; set; }

        public DbSet<Transportadora.Business.Models.PaymentMethod> PaymentMethod { get; set; }


        
    }
}
