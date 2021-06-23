using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IPaymentMethodRepository : IRepository<PaymentMethod>
    {
        Task<IEnumerable<PaymentMethod>> GetByCompanyId(Guid companyId);
    }
}
