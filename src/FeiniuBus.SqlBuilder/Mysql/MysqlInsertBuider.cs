using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlInsertBuider : IInsertBuider
    {
        private readonly IDictionary<string, object> _pir;
        private string _tableName;

        public MysqlInsertBuider()
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
            var fieldSb = new StringBuilder();
            var valueSb = new StringBuilder();
            foreach (var item in _pir)
            {
                fieldSb.Append($"`{item.Key}`,");
                var valfild = $"@INPARM_{item.Key}";
                valueSb.Append(valfild + ",");
                res.Params.Add(valfild, item.Value);
            }
            res.SqlString.AppendFormat("INSERT INTO {0} (", _tableName);
            res.SqlString.AppendLine(fieldSb.ToString().TrimEnd(','));

            res.SqlString.AppendLine(")");
            res.SqlString.AppendLine("VALUE(");
            res.SqlString.AppendLine(valueSb.ToString().TrimEnd(','));
            res.SqlString.AppendLine(");");
            return res;
        }

        public IInsertBuider InsertInto(string tableName)
        {
            _tableName = tableName;
            return this;
        }

        public IInsertBuider Values(string key, object value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            if (_pir.ContainsKey(key))
                _pir[key] = value;
            else
                _pir.Add(key, value);
            return this;
        }

        public IInsertBuider Values(IDictionary<string, object> keyvalues)
        {
            if (keyvalues == null)
                throw new ArgumentNullException(nameof(keyvalues));
            foreach (var item in keyvalues)
                Values(item.Key, item.Value);
            return this;
        }
    }
}