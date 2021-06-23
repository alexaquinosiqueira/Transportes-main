using System;
using System.Collections.Generic;
using System.Text;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ProfileActionFuncionalityRepository : Repository<ProfileActionFuncionality>, IProfileActionFuncionalityRepository
    {
        public ProfileActionFuncionalityRepository(TruckDbContext context) : base(context) { }



    }
}
