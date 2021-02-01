using AdventureWork.Domain.Entities;
using AdventureWork.Domain.Repositories;
using AdventureWork.Infra.Data.Context;
using AdventureWork.Infra.Data.Dto;
using AdventureWork.Infra.Data.Extensions;
using AutoMapper;
using System.Collections.Generic;

namespace AdventureWork.Infra.Data.Repositories
{
    public class WorkOrderRepository : Repository<WorkOrder>, IWorkOrderRepository
    {
        private readonly IMapper _mapper;
        public WorkOrderRepository(IDatabaseContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public IEnumerable<WorkOrder> GetAll()
        {
            var sql = @"SELECT wo.[WorkOrderID]
                          ,wo.[ProductID]
	                      ,p.[Name]
	                      ,p.[ProductNumber]
	                      ,p.[Color]
                          ,wo.[OrderQty]
                          ,wo.[StockedQty]
                          ,wo.[ScrappedQty]
                          ,wo.[StartDate]
                          ,wo.[EndDate]
                          ,wo.[DueDate]
                          ,wo.[ScrapReasonID]
                          ,wo.[ModifiedDate]
                      FROM [AdventureWorks2019].[Production].[WorkOrder] wo
                      Inner join [AdventureWorks2019].[Production].[Product] p on wo.ProductID = p.ProductID";
            var listDto = ExecuteReader(sql).MapToList<WorkOrdersProductDto>();
            return _mapper.Map<IEnumerable<WorkOrder>>(listDto);
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
            var itemDto = ExecuteReader(sql, parameters).MapToSingle<WorkOrder>();
            return _mapper.Map<WorkOrder>(itemDto);
        }
    }
}
