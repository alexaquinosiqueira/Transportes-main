using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ActionFuncionalityRepository : Repository<ActionFuncionality>, IActionFuncionalityRepository
    {
        public ActionFuncionalityRepository(TruckDbContext context) : base(context) { }



    }
}