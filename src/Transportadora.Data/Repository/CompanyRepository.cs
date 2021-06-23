using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(TruckDbContext context) : base(context)
        {
        }
    }
}
