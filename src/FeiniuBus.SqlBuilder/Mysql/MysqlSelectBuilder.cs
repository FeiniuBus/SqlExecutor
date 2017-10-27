using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlSelectBuilder : MysqlWhereBuilder, ISelectBuilder
    {
        private readonly ICharacterConverter _characterConverter;
        public MysqlSelectBuilder(ICharacterConverter characterConverter) : base(characterConverter)
        {
            _characterConverter = characterConverter;
        }

        [Obsolete("Unsupported.")]
        public SqlBuiderResult Build()
        {
            throw new System.NotImplementedException("暂时不支持SelectBuilder.");
        }

        public string OrderBy(IEnumerable<DynamicQueryOrder> orders, bool fieldConverter = true)
        {
            if (orders == null || !orders.Any()) return string.Empty;

            return string.Join(",", orders.Select(item =>
            {
                var name = item.Name;

                if (fieldConverter)
                {
                    name = _characterConverter.FieldConverter(item.Name);
                }

                if (SqlFieldMappings.Any(x=>x.Key == item.Name))
                {
                    name = SqlFieldMappings.First(x => x.Key == item.Name).SqlField;
                }
               
                var order = item.Sort == ListSortDirection.Ascending
                  ? "ASC"
                  : "DESC";
               
                return $" {name} {order} ";

            }));
        }
    }
}
