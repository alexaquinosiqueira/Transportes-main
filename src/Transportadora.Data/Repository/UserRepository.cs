using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(TruckDbContext context) : base(context) { }

    }
}