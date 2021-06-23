using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class DocumentTypeRepository : Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<DocumentType>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }
    }
}
