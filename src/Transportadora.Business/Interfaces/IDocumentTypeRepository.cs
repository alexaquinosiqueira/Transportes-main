using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IDocumentTypeRepository : IRepository<DocumentType>
    {
        Task<IEnumerable<DocumentType>> GetByCompanyId(Guid companyId);
    }
}
