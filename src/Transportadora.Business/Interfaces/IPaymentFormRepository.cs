using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IPaymentFormRepository : IRepository<PaymentForm>
    {
        Task<IEnumerable<PaymentForm>> GetByCompanyId(Guid companyId);
    }
}
