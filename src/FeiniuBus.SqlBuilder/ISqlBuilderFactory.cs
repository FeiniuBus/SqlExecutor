namespace FeiniuBus.SqlBuilder
{
    /// <summary>
    /// </summary>
    public interface ISqlBuilderFactory
    {
        IInsertBuider InsertBuider();
        IUpdateBuilder UpdateBuilder();
        IDeleteBuilder DeleteBuilder();
    }
}