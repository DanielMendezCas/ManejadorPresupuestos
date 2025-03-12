using ManejadorPresupuestos.Models;

namespace ManejadorPresupuestos.Data.Repositories.Interfaces
{
    public interface IAccountTypeRepository
    {
        Task Create(AccountType accountType);
        Task<bool> Exists(string accountName, int UserId);
    }
}
