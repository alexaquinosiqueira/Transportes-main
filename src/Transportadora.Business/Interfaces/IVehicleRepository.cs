using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetByCompanyId(Guid companyId);
    }
}
