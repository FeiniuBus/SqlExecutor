using System;
using System.Threading.Tasks;

namespace FeiniuBus.SqlExecutor
{
    public interface ISqlExecutor : IDisposable
    {
        Task BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();

        Task<IHashObject> SingRowAsync(string sql, IHashObject parms);
        Task<IHashObjectList> ListAsync(string sql, IHashObject parms);
        Task<object> ExecuteScalar(string sql, IHashObject parms);
        Task<int> ExecuteNonQueryAsync(string sql, IHashObject param);



    }


}
