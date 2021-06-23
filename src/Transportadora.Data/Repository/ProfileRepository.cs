using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class ProfileRepository : Repository<ProfileT>, IProfileRepository
    {
        public ProfileRepository(TruckDbContext context) : base(context) { }



    }
}