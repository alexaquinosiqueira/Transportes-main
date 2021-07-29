using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IRequisicaoCompraRepository : IRepository<RequisicaoCompra>
    {
        Task<IEnumerable<RequisicaoCompra>> GetByCompanyId(Guid companyId);
    }
}
