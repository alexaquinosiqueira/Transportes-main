using System.Threading.Tasks;
using Transportadora.Business.Models;

namespace Transportadora.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task Add(Employee employee);
        Task Update(Employee employee);
        Task Remove(Employee employee);
    }
}
