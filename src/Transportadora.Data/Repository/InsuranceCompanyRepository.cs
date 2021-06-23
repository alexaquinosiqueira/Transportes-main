﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transportadora.Business.Interfaces;
using Transportadora.Business.Models;
using Transportadora.Data.Context;

namespace Transportadora.Data.Repository
{
    public class InsuranceCompanyRepository : Repository<InsuranceCompany>, IInsuranceCompanyRepository
    {
        public InsuranceCompanyRepository(TruckDbContext context) : base(context) { }

        public Task<IEnumerable<InsuranceCompany>> GetByCompanyId(Guid companyId)
        {
            return Search(x => x.Company_Id == companyId);
        }

    }
}
