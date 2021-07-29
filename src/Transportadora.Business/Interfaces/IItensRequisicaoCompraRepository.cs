using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IItensRequicaoCompraRepository : IRepository<ItensRequisicaoCompra>
    {
        Task<IEnumerable<ItensRequisicaoCompra>> GetByCompanyId(Guid companyId);
    }
}
