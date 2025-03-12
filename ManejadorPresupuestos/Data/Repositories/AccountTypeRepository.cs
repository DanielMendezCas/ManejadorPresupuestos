using Dapper;
using ManejadorPresupuestos.Data.Repositories.Interfaces;
using ManejadorPresupuestos.Models;
using Microsoft.Data.SqlClient;

namespace ManejadorPresupuestos.Data.Repositories
{
    public class AccountTypeRepository : IAccountTypeRepository
    {
        private readonly string connectionString;

        public AccountTypeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("Default");
        }

        public async Task Create(AccountType accountType)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>
                (@"
                INSERT INTO AccountTypes (AccountName, UserId, [Order])
                VALUES (@AccountName, @UserId, 0);
                SELECT SCOPE_IDENTITY();", accountType);
            accountType.Id = id;
        }

        public async Task<bool> Exists(string accountName, int Userid)
        {
            using var connection = new SqlConnection(connectionString);
            var exists = await connection.QueryFirstOrDefaultAsync<int>
                (@"
                SELECT 1
                FROM AccountTypes
                WHERE AccountName = @AccountName AND UserId = @UserId;",
                new { accountName, Userid });

            return exists == 1;
        }
    }
}