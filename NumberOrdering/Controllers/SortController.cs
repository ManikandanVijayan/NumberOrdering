using BusinessService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NumberOrdering.Controllers
{
    /// <summary>
    /// SortController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private INumberOrderingService _numOrderService = null;

        /// <summary>
        /// SortController constructor
        /// </summary>
        /// <param name="numOrderingService">numOrderingService
        public SortController(INumberOrderingService numOrderingService)
        {
            _numOrderService = numOrderingService;
        }

        /// <summary>
        /// Load content of latest saved file 
        /// </summary>
        /// <returns>Sorted List</returns>
        [HttpGet]
        public ActionResult GetLatestFileContent()
        {
            var sortedNumberList = _numOrderService.GetLatestFileContent();
            return Ok(sortedNumberList);
        }

        /// <summary>
        /// Save sorted numbers to file.
        /// </summary>
        /// <param name="numberList">Number list to be sorted</param>
        /// <returns>Saved or not</returns>
        [HttpPost]
        public ActionResult SortNumbers([FromBody] List<int> numberList)
        {
            var isFileSaved = _numOrderService.SortNumbers(numberList);
            return Ok(isFileSaved);
        }
    }
}
