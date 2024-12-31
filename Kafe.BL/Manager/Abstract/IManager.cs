using Kafe.DAL.Repositories.Abstract;
using Kafe.Entities.Entity.Abstract;
using System.Collections.Generic;

namespace Kafe.BL.Manager.Abstract
{

    public interface IManager<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
        object GetAllInclude(Func<object, bool> value);
    }
}