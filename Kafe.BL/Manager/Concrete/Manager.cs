using Kafe.BL.Manager.Abstract;
using Kafe.DAL.DbContexts;
using Kafe.DAL.Repositories.Concrete;
using Kafe.Entities.Entity.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace Kafe.BL.Manager.Concrete
{
    public class Manager<T> : Repository<T>, IManager<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public Manager(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(T entity)
        {
            Create(entity);  // Repository sınıfındaki Create metodunu çağırır
        }

        public void Delete(int id)
        {
            var entity = GetById(id);  // Repository sınıfındaki GetById metodunu çağırır
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();  // Tüm kayıtları döner
        }

        public object GetAllInclude(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);  // Belirli bir ID'ye sahip kaydı döner
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);  // Kaydı günceller
            _dbContext.SaveChanges();
        }
    }
}