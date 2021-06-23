using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class PaymentMethodRepository : Repository<PaymentMethod>, IPaymentMethodRepository
    {
        public PaymentMethodRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<PaymentMethod>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }
    }
}
