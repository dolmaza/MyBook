using System.Data.Entity;
using System.Linq;

namespace Core.Repositories
{
    public interface IDictionaryRepository : IRepository<Dictionary>
    {
        IQueryable<Dictionary> GetAll(int? level, int? code);
        Dictionary Get(int? level, int? code, int intCode);
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

        public Dictionary Get(int? level, int? code, int intCode)
        {
            return GetAll().SingleOrDefault(d => d.Level == level && d.Code == code && d.IntCode == intCode);
        }
    }
}
