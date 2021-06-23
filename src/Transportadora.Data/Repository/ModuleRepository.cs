using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ModuleRepository : Repository<Module>, IModuleRepository
    {
        public ModuleRepository(TruckDbContext context) : base(context) { }



    }
}