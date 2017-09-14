using System.Linq;

namespace FeiniuBus
{
    public class DynamicQueryBuilder
    {
        private readonly DynamicQuery _query;

        public DynamicQueryBuilder() : this(true)
        {
        }

        public DynamicQueryBuilder(bool pager)
        {
            _query = new DynamicQuery(pager);
            ParamGroupBuilder = new DynamicQueryParamGroupBuilder(_query.ParamGroup);
        }

        public DynamicQueryParamGroupBuilder ParamGroupBuilder { get; }

        public DynamicQueryBuilder Skip(int skip)
        {
            _query.Skip = skip;
            return this;
        }

        public DynamicQueryBuilder Take(int take)
        {
            _query.Take = take;
            return this;
        }

        public DynamicQueryBuilder Pager(bool pager)
        {
            _query.Pager = pager;
            return this;
        }

        public DynamicQueryBuilder Select(string select)
        {
            _query.Select = select;
            return this;
        }

        public DynamicQueryBuilder OrderBy(string name, ListSortDirection sort = ListSortDirection.Ascending)
        {
            if (!_query.Order.Any(i => (i.Name == name) && (i.Sort == sort)))
                _query.Order.Add(new DynamicQueryOrder { Name = name, Sort = sort });
            return this;
        }

        public DynamicQuery Build()
        {
            return _query;
        }

        public static DynamicQueryBuilder Create(bool pager = true)
        {
            return new DynamicQueryBuilder(pager);
        }


    }
}