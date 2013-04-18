using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sunshine.Business.Core;

namespace Sunshine.Business.Agencies
{
    public interface ICommerceAgency
    {
        Company RegisterCompany(Company company);
        Company UpdateCompany(Company company);
    }
}
