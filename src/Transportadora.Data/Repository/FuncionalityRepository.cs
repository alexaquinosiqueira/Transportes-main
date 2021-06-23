using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class FuncionalityRepository : Repository<Funcionality>, IFuncionalityRepository
    {
        public FuncionalityRepository(TruckDbContext context) : base(context) { }



    }
}