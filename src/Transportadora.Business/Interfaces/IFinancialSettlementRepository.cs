using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IFinancialSettlementRepository : IRepository<FinancialSettlement>
    {
        Task<IEnumerable<FinancialSettlement>> GetByCompanyId(Guid companyId);

    }
}
