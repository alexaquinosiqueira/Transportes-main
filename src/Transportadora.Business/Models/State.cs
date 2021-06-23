using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class State : Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Uf { get; set; }
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
