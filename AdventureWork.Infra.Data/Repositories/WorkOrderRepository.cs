using AdventureWork.Domain.Entities;
using AdventureWork.Domain.Repositories;
using AdventureWork.Infra.Data.Context;
using System;
using System.Collections.Generic;
using AdventureWork.Infra.Data.Extensions;

namespace AdventureWork.Infra.Data.Repositories
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
    {
        public WorkOrderRepository(IDatabaseContext context) : base(context)
        {
        }

        public IEnumerable<WorkOrder> GetAll()
        {
            var sql = @"SELECT [WorkOrderID]
                      ,[ProductID]
                      ,[OrderQty]
                      ,[StockedQty]
                      ,[ScrappedQty]
                      ,[StartDate]
                      ,[EndDate]
                      ,[DueDate]
                      ,[ScrapReasonID]
                      ,[ModifiedDate]
                  FROM [AdventureWorks2019].[Production].[WorkOrder]";
            return ExecuteReader(sql).MapToList<WorkOrder>();
        }

        public WorkOrder GetById(int id)
        {
            var sql = @"SELECT [WorkOrderID]
                      ,[ProductID]
                      ,[OrderQty]
                      ,[StockedQty]
                      ,[ScrappedQty]
                      ,[StartDate]
                      ,[EndDate]
                      ,[DueDate]
                      ,[ScrapReasonID]
                      ,[ModifiedDate]
                  FROM [AdventureWorks2019].[Production].[WorkOrder]
                    WHERE @Id = @Id";
            var parameters = new Dictionary<string, object> { { "@Id", id } };
            return ExecuteReader(sql, parameters).MapToSingle<WorkOrder>();
        }
    }
}
