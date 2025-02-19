using ProjectManagementApi.Models;

namespace ProjectManagementApi.Repository.Interfaces
{
    public interface ICompaniesRepository
    {
        ICollection<Companies> GetCompanies();
        Companies GetCompanyById(int companyId);
        bool CompanyExists(int companyId);
        bool CreateCompany(Companies company);
        bool UpdateCompany(Companies company);
        bool DeleteCompany(Companies company);
        bool Save();
    }
}