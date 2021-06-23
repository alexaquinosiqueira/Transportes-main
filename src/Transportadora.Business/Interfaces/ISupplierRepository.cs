using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<IEnumerable<Supplier>> GetByCompanyId(Guid companyId);
    }
}
