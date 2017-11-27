using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlUpdateBuilder : MysqlWhereBuilder, IUpdateBuilder
    {
        private readonly IDictionary<string, object> _pir;
        private string _tableName;

        public MysqlUpdateBuilder(ICharacterConverter characterConverter) : base(characterConverter)
        {
            _pir = new Dictionary<string, object>();
        }

        public SqlBuiderResult Build()
        {
            if (string.IsNullOrEmpty(_tableName))
                throw new Exception("TableName is Null.");
            if (!_pir.Any())
                throw new Exception("Value is Null.");
            var res = new SqlBuiderResult();
            res.SqlString.AppendLine($"UPDATE {_tableName} SET");
            var filedSb = new StringBuilder();
            foreach (var item in _pir)
            {
                var pfield = $"@UPPARM_{item}";
                filedSb.AppendFormat("`{0}` = {1},", item.Key, pfield);
            }
            res.SqlString.AppendLine(filedSb.ToString().TrimEnd(','));
            res.SqlString.AppendLine($"WHERE 1=1");

            var whereRes = BuildWhere();

            if (whereRes.SqlString.Length > 0)
                res.SqlString.AppendLine(" AND " + whereRes.SqlString);
            res.SqlString.Append(" ;");
            foreach (var item in whereRes.Params)
                res.Params.Add(item.Key, item.Value);
            return res;
        }

        public IUpdateBuilder Update(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IUpdateBuilder Set(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_pir.ContainsKey(key))
                _pir[key] = value;
            else
                _pir.Add(key, value);
            return this;
        }

        public IUpdateBuilder Set(IDictionary<string, object> values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            foreach (var item in values)
                Set(item.Key, item.Value);
            return this;
        }
    }
}