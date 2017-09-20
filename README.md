# SqlExecutor
FeiniuBus Database Operating Competents.Offering DynamicQuery, Object Mapper, Linq Builder, Sql Builder and others.

# Support
 Framework | Support 
----|----
 netstandard1.6 | +Support
 netstandard2.0 | +Support
 netcoreapp1.1 | +Support
 netcoreapp2.0 | +Support
 golang | +[Client Only](https://github.com/FeiniuBus/SqlExecutor-Go)

# Pipeline
Branch | Status | CI
----|----|----
master | 2.0.0 | [![](https://travis-ci.org/FeiniuBus/SqlExecutor.svg?branch=master)](https://travis-ci.org/FeiniuBus/SqlExecutor)[![Build status](https://ci.appveyor.com/api/projects/status/w49ddl7ydevg4kl5?svg=true)](https://ci.appveyor.com/project/standardcore/sqlexecutor)
milestone/2.0.0 | RELEASED | [![](https://travis-ci.org/FeiniuBus/SqlExecutor.svg?branch=milestone/2.0.0)](https://travis-ci.org/FeiniuBus/SqlExecutor)

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
DynamicQuery-SQLStatement Converter.

## SqlExecuter
An encapsulation layer of Ado.NET.

## SqlExecuter.Mysql
An MySQL dialect implemention of SqlExecuter.
