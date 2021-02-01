using AdventureWork.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Data;
using System.Threading.Tasks;

namespace AdventureWork.Infra.CrossCutting.MiddlewareFilterNotification
{
    public class TransactionFilter : IAsyncActionFilter
    {
        private readonly IDatabaseContext _context;

        public TransactionFilter(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.Request.Method.Equals("Post", StringComparison.OrdinalIgnoreCase)
                    && !context.HttpContext.Request.Method.Equals("Put", StringComparison.OrdinalIgnoreCase)
                    && !context.HttpContext.Request.Method.Equals("Delete", StringComparison.OrdinalIgnoreCase))
            {
                _context.BeginTransaction(IsolationLevel.ReadCommitted);
                var executedContext = await next.Invoke();
                _context.EndTransaction(executedContext.Exception);
            }
            else
            {
                _context.BeginTransaction(IsolationLevel.Snapshot);
                var executedContext = await next.Invoke();
                _context.EndTransaction(executedContext.Exception);
            }
        }
    }
}
