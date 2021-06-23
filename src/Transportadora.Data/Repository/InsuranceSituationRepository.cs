using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class InsuranceSituationRepository : Repository<InsuranceSituation>, IInsuranceSituationRepository
    {
        public InsuranceSituationRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<InsuranceSituation>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }

    }
}
