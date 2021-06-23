using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ParameterRepository : Repository<Parameter>, IParameterRepository
    {
        public ParameterRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<Parameter>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Id_Company == companyId);
        }

    }
}
