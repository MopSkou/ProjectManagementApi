using ProjectManagementApi.Data;
using ProjectManagementApi.Models;

namespace ProjectManagementApi.Repository.Interfaces
{
    public interface IDoljnostiRepository
    {
        ICollection<Doljnosti> GetDoljnostisList();

        Doljnosti GetDoljnostiById(int Id_doljnosti);

        Doljnosti GetDoljnosti(string post);
        bool DoljnostiExists(int doljnosId);
        bool CreateDoljnosti(Doljnosti doljnosti_create);
        bool UpdateDoljnosti(Doljnosti doljnosti_update);
        bool DeleteDoljnosti(Doljnosti doljnosti_delete);
        bool Save();
    }
}

