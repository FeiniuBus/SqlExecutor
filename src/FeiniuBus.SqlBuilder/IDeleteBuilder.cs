namespace FeiniuBus.SqlBuilder
{
    public interface IDeleteBuilder : IWhereBuilder, ISqlBuilder
    {
        IDeleteBuilder Delete(string tableName);
    }
}