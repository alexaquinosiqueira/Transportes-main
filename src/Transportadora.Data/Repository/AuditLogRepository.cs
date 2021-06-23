using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class AuditLogRepository : Repository<AuditLog>, IAuditLogRepository
    {
        public AuditLogRepository(TruckDbContext context) : base(context) { }



    }
}
