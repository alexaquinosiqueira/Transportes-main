using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IInsuranceCompanyRepository : IRepository<InsuranceCompany>
    {
        Task<IEnumerable<InsuranceCompany>> GetByCompanyId(Guid companyId);
    }
}
