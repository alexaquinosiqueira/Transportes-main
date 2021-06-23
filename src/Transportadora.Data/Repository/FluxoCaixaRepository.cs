using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class FluxoCaixaRepository : Repository<FluxoCaixa>, IFluxoCaixaRepository
    {
        public FluxoCaixaRepository(TruckDbContext context) : base(context) { }


        public IEnumerable<FluxoCaixa> GetFluxoCaixa(int ano)
        {
            return Db.FluxoCaixas.FromSqlRaw("exec ObtemFluxoDeCaixaPorAno @p0", ano).ToList();
        }
    }
}
