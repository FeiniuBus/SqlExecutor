using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;

namespace FeiniuBus.SqlExecutor.Mysql
{
    public class MySqlExecutor : ISqlExecutor
    {
        protected readonly MySqlConnection _connection;
        protected MySqlTransaction _transaction;
        protected readonly SqlExecutorOptions _options;
        protected readonly ILoggerFactory _loggerFactory;
        private readonly ILogger<MySqlExecutor> _logger;
        protected readonly bool EnableLogger;

        //
        public MySqlExecutor(SqlExecutorOptions options, ILoggerFactory loggerFactory = null)
        {
            _options = options;
            _connection = new MySqlConnection(_options.ConnectionString);
            if (loggerFactory != null && _options.EnableLogger)
            {
                _logger = loggerFactory.CreateLogger<MySqlExecutor>();
                EnableLogger = true;
            }
            else
            {
                EnableLogger = false;
            }
        }

        #region Query
        public virtual async Task<IHashObject> SingRowAsync(string sql, IHashObject parms)
        {
            var cmd = GetCommand(sql, false, parms);
            using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
            {
                if (await reader.ReadAsync())
                {
                    return ReaderTohashObj(reader);
                }
                return null;
            }
        }

        public virtual async Task<IHashObjectList> ListAsync(string sql, IHashObject parms)
        {
            IHashObjectList list = new HashObjectList();
            var cmd = GetCommand(sql, false, parms);
            using (var reader = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
            {
                while (await reader.ReadAsync())
                {
                    list.Add(ReaderTohashObj(reader));
                }
            }
            return list;
        }

        public virtual async Task<object> ExecuteScalar(string sql, IHashObject parms)
        {
            var cmd = GetCommand(sql, false, parms);
            return await cmd.ExecuteScalarAsync();
        }

        protected virtual IHashObject ReaderTohashObj(DbDataReader reader)
        {
            IHashObject res = new HashObject();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var key = reader.GetName(i);
                var val = reader.IsDBNull(i) ? null : reader[i];
                res.Add(key, val);
            }
            return res;
        }

        #endregion

        #region Transaction

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new Exception("已经开始了一个事物!");
            }
            _transaction = await _connection.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            if (_transaction == null)
            {
                throw new Exception("无开始的事物!");
            }
            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void RollbackTransaction()
        {
            if (_transaction == null)
            {
                throw new Exception("无开始的事物!");
            }
            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        #endregion

        public async Task<int> ExecuteNonQueryAsync(string sql, IHashObject param)
        {
            using (var cmd = GetCommand(sql, true, param))
            {
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        private MySqlCommand GetCommand(string sql, bool hasTr, IDictionary<string, object> param)
        {
            var cmd = new MySqlCommand(sql, _connection);
            if (_transaction != null && hasTr)
            {
                cmd.Transaction = _transaction;
            }
            if (param != null)
            {
                foreach (var o in param)
                {
                    cmd.Parameters.AddWithValue(o.Key, o.Value);
                }
            }

            return cmd;
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _connection?.Dispose();
        }


    }
}
