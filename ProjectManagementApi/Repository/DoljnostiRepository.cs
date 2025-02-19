using ProjectManagementApi.Data;
using ProjectManagementApi.Models;
using ProjectManagementApi.Repository.Interfaces;

namespace ProjectManagementApi.Repository
{
    public class DoljnostiRepository : IDoljnostiRepository
    {
        private readonly DataContext _context;

        public DoljnostiRepository(DataContext context)
        {
            _context = context;
        }

        public bool DoljnostiExists(int doljnosId)
        {
            return _context.Doljnosti.Any(p => p.id_doljnosti == doljnosId);
        }

        public Doljnosti GetDoljnostiById(int Id_doljnosti)
        {
            return _context.Doljnosti.Where(p => p.id_doljnosti == Id_doljnosti).FirstOrDefault();
        }

        public Doljnosti GetDoljnosti(string post)
        {
            return _context.Doljnosti.Where(p => p.Post == post).FirstOrDefault();
        }

       

        public ICollection<Doljnosti> GetDoljnostisList()
        {
            return _context.Doljnosti.OrderBy(p => p.id_doljnosti).ToList();
        }

        public bool CreateDoljnosti(Doljnosti doljnosti_create)
        {
            _context.Add(doljnosti_create);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDoljnosti(Doljnosti doljnosti_update)
        {
            _context.Update(doljnosti_update);
            return Save();
        }

        public bool DeleteDoljnosti(Doljnosti doljnosti_delete)
        {
            _context.Remove(doljnosti_delete);
            return Save();
        }

      
    }
}