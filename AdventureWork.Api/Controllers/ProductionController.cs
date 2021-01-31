using AdventureWork.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace AdventureWork.Api.Controllers
{
    [ApiController]
    [Route("production")]
    public class ProductionController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IWorkOrderRepository _workOrderRepository;

        public ProductionController(IProductRepository productRepository, IWorkOrderRepository workOrderRepository)
        {
            _productRepository = productRepository;
            _workOrderRepository = workOrderRepository;
        }

        [HttpGet("product")]
        public IActionResult GetAllProduct()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var products = _productRepository.GetAll();
            stopwatch.Stop();
            var time = stopwatch.ElapsedMilliseconds;

            var pageNumber = 1;
            int numberOfObjectsPerPage = 10;
            var queryResultPage = products
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);

            return Ok(new
            {
                TempoConsultaEPopulacaoObjeto = $"{time} ms",
                totalItensRetornado = products.Count(),
                DezPrimeirosItens = queryResultPage,
            });
        }

        [HttpGet("product/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetById(id);
            return Ok(product);
        }

        [HttpGet("work-order")]
        public IActionResult GetAllWorkOrder()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var worksOrders = _workOrderRepository.GetAll();
            stopwatch.Stop();
            var time = stopwatch.ElapsedMilliseconds;

            var pageNumber = 1;
            int numberOfObjectsPerPage = 10;
            var queryResultPage = worksOrders
              .Skip(numberOfObjectsPerPage * (pageNumber - 1))
              .Take(numberOfObjectsPerPage);

            return Ok(new
            {
                TempoConsultaEPopulacaoObjeto = $"{time} ms",
                totalItensRetornado = worksOrders.Count(),
                DezPrimeirosItens = queryResultPage,
            });
        }
    }
}
