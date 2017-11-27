# SqlExecutor
FeiniuBus Database Operating Competents.Offering DynamicQuery, Object Mapper, Linq Builder, Sql Builder and others.

# Support
 Framework | Support 
----|----
 netstandard2.0 | +Support
 netcoreapp2.0 | +Support
 golang | +[Client Only](https://github.com/FeiniuBus/SqlExecutor-Go)

# Pipeline
Branch | Status | CI
----|----|----
master | 2.0.0 | [![](https://travis-ci.org/FeiniuBus/SqlExecutor.svg?branch=master)](https://travis-ci.org/FeiniuBus/SqlExecutor)[![Build status](https://ci.appveyor.com/api/projects/status/w49ddl7ydevg4kl5?svg=true)](https://ci.appveyor.com/project/standardcore/sqlexecutor)
milestone/2.0.0 | RELEASED | [![](https://travis-ci.org/FeiniuBus/SqlExecutor.svg?branch=milestone/2.0.0)](https://travis-ci.org/FeiniuBus/SqlExecutor)
milestone/2.1.0| PRE | [![](https://travis-ci.org/FeiniuBus/SqlExecutor.svg?branch=milestone%2F2.1.0)](https://travis-ci.org/FeiniuBus/SqlExecutor)

# Competents
## Dynamic Query
Utility library for build data query descriptor or convert it to Linq Expression or SQL Statements.
***Here is a sample***

```cs
DynamicQueryBuilder builder = DynamicQueryBuilder.Create(true);
var child1 = builder.ParamGroupBuilder.CreateChildAndGroup();
child1.ParamBuilder.Any("Extra", sub =>
{
    sub.ParamBuilder.Equal("Guest", "Andy");
});
var child2 = builder.ParamGroupBuilder.CreateChildOrGroup()
            .ParamBuilder
                .Contains("Address", "chengdu")
                .EndsWith("Address", "lnk")
                .Equal("Disabled", false)
                .GreaterThan("Amout", 10)
                .GreaterThanOrEqual("Price", 100)
                .In("Drink", "mileshake,coffee")
                .LessThan("Count", 10)
                .LessThanOrEqual("Total", 100)
                .StartsWith("Url", "Http://");
builder.OrderBy("Amout", ListSortDirection.Ascending)
    .Select("Guest").Take(10).Skip(10);
var dynamicQuery = builder.Build();
```

## HashObject
HashObject,HashList

## LinqBuilder
DynamicQuery-LinqExpression Converter.

## SqlBuilder
### STEP 1 : Add SqlBuilder Services in `StartUp.cs`

```cs
 public void ConfigureServices(IServiceCollection services)
{
   services.AddSQLBuilder(opts =>
   {
      opts.UseMySQL();
   });
}
```

### STEP 2 : Builder mapping data with fluent style api
```cs
var mappings = new SqlFieldMappings();
mappings.Map("Guest", "t1.Guest")
        .Map("Address", "t1.Address")
        .Map("Disabled", "t1.Disabled")
        .Map("Amout", "t1.Amout")
        .Map("Price", "t1.Price")
        .Map("Drink", "t1.Drink")
        .Map("Count", "t1.Count")
        .Map("Total", "t1.Total");
// Native api also be supported
mappings.Add("Url", "t1.Url");
```

### STEP 3 : Builder SQL clauses
```cs
// `selectBuilder` should be injected from DI. Notice it's lifetime and create service scope any time if	necessary.
selectBuilder.Mapping(mappings);
selectBuilder.Where(dynamicQuery.ParamGroup);
var whereClause = selectBuilder.BuildWhere();
var orderbyClause = selectBuilder.OrderBy(dynamicQuery.Order);
```

** In this case, value of `whereClause` is :
```sql
((Extra.Guest = @PARAM_0)) AND (t1.Address like @PARAM_1 OR t1.Address like @PARAM_2 OR t1.Disabled = @PARAM_3 OR t1.Amout > @PARAM_4 OR t1.Price >= @PARAM_5 OR t1.Drink IN (mileshake,coffee) OR t1.Count < @PARAM_6 OR t1.Total <= @PARAM_7 OR t1.Url like @PARAM_8)
```


## SqlExecuter
An encapsulation layer of Ado.NET.

## SqlExecuter.Mysql
An MySQL dialect implemention of SqlExecuter.
