using System.Data;
using System.Threading.Tasks;

namespace FeiniuBus.SqlExecutor
{
    public interface ISqlExecutor : IReadOnlySqlExecutor
    {
        Task BeginTransactionAsync();
        Task BeginTransactionAsync(IsolationLevel isolationLevel);
        void CommitTransaction();
        void RollbackTransaction();

        Task<int> ExecuteNonQueryAsync(string sql, IHashObject param);
    }
}