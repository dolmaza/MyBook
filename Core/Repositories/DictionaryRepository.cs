using System.Data.Entity;

namespace Core.Repositories
{
    public interface IDictionaryRepository : IRepository<Dictionary>
    {

    }

    public class DictionaryRepository : Repository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(DbContext context)
            : base(context)
        {
        }
    }
}
