namespace FeiniuBus
{
    public class DynamicQueryParamGroupBuilder
    {
        private readonly DynamicQueryParamGroup _paramGroup;

        public DynamicQueryParamGroupBuilder() : this(new DynamicQueryParamGroup())
        {
        }

        public DynamicQueryParamGroupBuilder(DynamicQueryParamGroup paramGroup)
        {
            _paramGroup = paramGroup;
            ParamBuilder = new DynamicQueryParamBuilder(_paramGroup.Params);
        }

        public DynamicQueryParamBuilder ParamBuilder { get; }

        public DynamicQueryParamGroupBuilder RelationAnd()
        {
            return Relation(QueryRelation.And);
        }

        public DynamicQueryParamGroupBuilder RelationOr()
        {
            return Relation(QueryRelation.Or);
        }

        public DynamicQueryParamGroupBuilder Relation(QueryRelation relation)
        {
            _paramGroup.Relation = relation;
            return this;
        }

        public DynamicQueryParamGroup Build()
        {
            return _paramGroup;
        }

        #region CreateChildGroup

        public DynamicQueryParamGroupBuilder CreateChildGroup(QueryRelation relation)
        {
            _paramGroup.CheckDynamicQueryParamGroup();
            var childGroup = new DynamicQueryParamGroup {Relation = relation};
            _paramGroup.ChildGroups.Add(childGroup);
            return new DynamicQueryParamGroupBuilder(childGroup);
        }

        public DynamicQueryParamGroupBuilder CreateChildAndGroup()
        {
            return CreateChildGroup(QueryRelation.And);
        }

        public DynamicQueryParamGroupBuilder CreateChildOrGroup()
        {
            return CreateChildGroup(QueryRelation.Or);
        }

        public DynamicQueryParamGroupBuilder CreateChildAndNotGroup()
        {
            return CreateChildGroup(QueryRelation.AndNot);
        }

        public DynamicQueryParamGroupBuilder CreateChildOrNotGroup()
        {
            return CreateChildGroup(QueryRelation.OrNot);
        }

        #endregion
    }
}