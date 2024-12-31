using Kafe.DAL.Repositories.Abstract;
using Kafe.Entities.Entity.Concrete;

namespace Kafe.MVC.Services
{
    public class TableService : ITableService
    {
        private readonly IRepository<Table> _tableRepository;

        public TableService(IRepository<Table> tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public Table GetTableById(int tableId)
        {
            return _tableRepository.GetById(tableId);
        }

        public IEnumerable<Table> GetAllTables()
        {
            return _tableRepository.GetAll();
        }

        public void UpdateTable(Table table)
        {
            _tableRepository.Update(table);
        }
    }
}
