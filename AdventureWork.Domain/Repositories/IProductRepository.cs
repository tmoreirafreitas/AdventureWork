using AdventureWork.Domain.Entities;
using System.Collections.Generic;

namespace AdventureWork.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
    }
}
