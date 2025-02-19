using ProjectManagementApi.Data;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Repository
{
    namespace ProjectManagementApp.Repositories
    {
        public class CompaniesRepository : ICompaniesRepository
        {
            private readonly DataContext _context;

            public CompaniesRepository(DataContext context)
            {
                _context = context;
            }

            public bool CompanyExists(int companyId)
            {
                return _context.Companies.Any(c => c.CompanyID == companyId);
            }

            public Companies GetCompanyById(int companyId)
            {
                return _context.Companies.FirstOrDefault(c => c.CompanyID == companyId);
            }

            public ICollection<Companies> GetCompanies()
            {
                return _context.Companies.OrderBy(c => c.CompanyID).ToList();
            }

            public bool CreateCompany(Companies company)
            {
                _context.Add(company);
                return Save();
            }

            public bool UpdateCompany(Companies company)
            {
                _context.Update(company);
                return Save();
            }

            public bool DeleteCompany(Companies company)
            {
                _context.Remove(company);
                return Save();
            }

            public bool Save()
            {
                var saved = _context.SaveChanges();
                return saved > 0;
            }
        }
    }
}
