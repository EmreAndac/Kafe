using Kafe.Entities.Entity.Concrete;

namespace Kafe.MVC.Services
{
    public interface ITableService
    {
        Table GetTableById(int tableId);
        IEnumerable<Table> GetAllTables();// Tüm masaları getir
        void UpdateTable(Table table);
    }
}
