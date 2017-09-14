using System;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlDeleteBuilder : MysqlWhereBuilder, IDeleteBuilder
    {
        private string _tableName;
        public MysqlDeleteBuilder(ICharacterConverter characterConverter) : base(characterConverter)
        {
        }

        public SqlBuiderResult Buid()
        {
            if (string.IsNullOrEmpty(_tableName))
                throw new Exception("TableName is Null.");
            var res = new SqlBuiderResult();
            res.SqlString.AppendLine($"DELETE FROM `{_tableName}` WHERE 1=1 ");
            var whereres = BuildWhere();
            if (whereres.SqlString.Length > 0)
            {
                res.SqlString.AppendLine(" AND " + whereres.SqlString.ToString());
            }
            res.SqlString.Append(" ;");
            foreach (var item in whereres.Params)
            {
                res.Params.Add(item.Key, item.Value);
            }
            return res;
        }

        public IDeleteBuilder Delete(string tableName)
        {
            _tableName = tableName;
            return this;
        }
    }
}
