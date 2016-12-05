using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IDictionaryRepository : IRepository<Dictionary>
    {
        IQueryable<Dictionary> GetAll(int? level, int? dictionaryCode);
    }

    public class DictionaryRepository : Repository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(DbContext context)
            : base(context)
        {
        }

        public IQueryable<Dictionary> GetAll(int? level, int? code)
        {
            return GetAll().Where(d => d.Level == level && d.Code == code);
        }
    }
}
