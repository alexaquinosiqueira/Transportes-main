using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class FinancialSettlementRepository : Repository<FinancialSettlement>, IFinancialSettlementRepository
    {
        public FinancialSettlementRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<FinancialSettlement>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }

        public Task<IEnumerable<FinancialSettlement>> GetByCode(string code)
        {
            return Search(x => x.Code.Contains(code));
        }
    }
}
