using AdventureWork.Domain.Entities;
using System.Collections.Generic;

namespace AdventureWork.Domain.Repositories
{
    public interface IWorkOrderRepository : IRepository<WorkOrder>
    {
        IEnumerable<WorkOrder> GetAll();
        WorkOrder GetById(int id);
    }
}
