using System.Collections.Generic;
using Transportadora.Business.Models;
using System.Threading.Tasks;
using System;


namespace Transportadora.Business.Interfaces
{
    public interface IFluxoCaixaRepository : IRepository<FluxoCaixa>
    {
        IEnumerable<FluxoCaixa> GetFluxoCaixa(int ano);
    }
}
