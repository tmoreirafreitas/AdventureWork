using AdventureWork.Domain.Entities;
using AdventureWork.Domain.Repositories;
using AdventureWork.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using AdventureWork.Infra.Data.Extensions;

namespace AdventureWork.Infra.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IDatabaseContext context) : base(context)
        {
        }

        public IEnumerable<Product> GetAll()
        {
            const string sql = @"SELECT [ProductID]
                                      ,[Name]
                                      ,[ProductNumber]
                                      ,[MakeFlag]
                                      ,[FinishedGoodsFlag]
                                      ,[Color]
                                      ,[SafetyStockLevel]
                                      ,[ReorderPoint]
                                      ,[StandardCost]
                                      ,[ListPrice]
                                      ,[Size]
                                      ,[SizeUnitMeasureCode]
                                      ,[WeightUnitMeasureCode]
                                      ,[Weight]
                                      ,[DaysToManufacture]
                                      ,[ProductLine]
                                      ,[Class]
                                      ,[Style]
                                      ,[ProductSubcategoryID]
                                      ,[ProductModelID]
                                      ,[SellStartDate]
                                      ,[SellEndDate]
                                      ,[DiscontinuedDate]
                                      ,[rowguid]
                                      ,[ModifiedDate]
                                  FROM [AdventureWorks2019].[Production].[Product]";

            var dataReader = ExecuteReader(sql);
            var products = dataReader.MapToList<Product>();
            return products;
        }

        public Product GetById(int id)
        {
            const string sql = @"SELECT [ProductID]
                                      ,[Name]
                                      ,[ProductNumber]
                                      ,[MakeFlag]
                                      ,[FinishedGoodsFlag]
                                      ,[Color]
                                      ,[SafetyStockLevel]
                                      ,[ReorderPoint]
                                      ,[StandardCost]
                                      ,[ListPrice]
                                      ,[Size]
                                      ,[SizeUnitMeasureCode]
                                      ,[WeightUnitMeasureCode]
                                      ,[Weight]
                                      ,[DaysToManufacture]
                                      ,[ProductLine]
                                      ,[Class]
                                      ,[Style]
                                      ,[ProductSubcategoryID]
                                      ,[ProductModelID]
                                      ,[SellStartDate]
                                      ,[SellEndDate]
                                      ,[DiscontinuedDate]
                                      ,[rowguid]
                                      ,[ModifiedDate]
                                  FROM [AdventureWorks2019].[Production].[Product]
                                  WHERE @Id = @Id";

            var parameters = new Dictionary<string, object> { { "@Id", id } };
            var dataReader = ExecuteReader(sql, parameters);
            var product = dataReader.MapToSingle<Product>();
            return product;
        }
    }
}
