using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IInsuranceSituationRepository : IRepository<InsuranceSituation>
    {
        Task<IEnumerable<InsuranceSituation>> GetByCompanyId(Guid companyId);
    }
}
