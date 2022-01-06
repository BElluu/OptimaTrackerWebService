using OptimaTrackerWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimaTrackerWebService.Services
{
    public class DatabaseService
    {
        public void Insert(CompanyModel company)
        {
            Console.WriteLine(company.SerialKey);
            Console.WriteLine(company.TIN);
            Console.WriteLine(company.Events);
        }
    }
}
