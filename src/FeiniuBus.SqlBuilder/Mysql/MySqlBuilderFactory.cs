namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MySqlBuilderFactory : ISqlBuilderFactory
    {
        public IInsertBuider InsertBuider()
        {
            return new MysqlInsertBuider();
        }

        public IUpdateBuilder UpdateBuilder()
        {
            return new MysqlUpdateBuilder(new DefaultCharacterConverter());
        }

        public IDeleteBuilder DeleteBuilder()
        {
            return new MysqlDeleteBuilder(new DefaultCharacterConverter());
        }
    }
}