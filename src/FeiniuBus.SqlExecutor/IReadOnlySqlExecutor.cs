using System;
using System.Threading.Tasks;

namespace FeiniuBus.SqlExecutor
{
    public interface IReadOnlySqlExecutor : IDisposable
    {
        Task<IHashObject> SingRowAsync(string sql, IHashObject parms);
        Task<IHashObjectList> ListAsync(string sql, IHashObject parms);
        Task<object> ExecuteScalar(string sql, IHashObject parms);
    }
}