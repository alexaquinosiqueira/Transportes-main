using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IVehicleTypeRepository : IRepository<VehicleType>
    {
        Task<IEnumerable<VehicleType>> GetByCompanyId(Guid companyId);
    }
}
