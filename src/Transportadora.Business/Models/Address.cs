using System.Collections.Generic;

namespace Transportadora.Business.Models
{
    public class Address : Entity
    {
        public string AddressLine { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string PostalCode { get; set; }

        public int City_Id { get; set; }
        public virtual City City { get; set; }
        public int State_id { get; set; }
        public virtual State State { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
    }
}
