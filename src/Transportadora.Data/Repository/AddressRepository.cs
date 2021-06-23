using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(TruckDbContext context) : base(context) { }



    }
}
