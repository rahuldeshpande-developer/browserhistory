using BrowserNavigationHistory.Models;
using Microsoft.AspNetCore.Mvc;

namespace BrowserNavigationHistory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrowserHistoryController : ControllerBase
    {
        const int FIXED_SIZE = 1000;
        private readonly IHistoryItemRepository _repository;

        public BrowserHistoryController(IHistoryItemRepository navHistory)
        {
            _repository = navHistory;
        }

        // [controller]/
        [HttpGet]
        public ActionResult<IEnumerable<HistoryItem>> Get()
        {
            return Ok(_repository.Get());
        }

        // [controller]/id/1
        [HttpGet("{id:int}")]
        public ActionResult<HistoryItem> GetById(int id)
        {
            HistoryItem historyItem = _repository.GetById(id);

            if(historyItem == null)
            {
                return NotFound($"Item of id = {id} does not exist.");
            }

            return Ok(historyItem);
        }

        // [controller]/batch?page=4&size=3
        [HttpGet]
        [Route("batch")]
        public ActionResult<IEnumerable<HistoryItem>>
            GetByPage([FromQuery(Name = "page")] int pageNumber = 1,
                    [FromQuery(Name = "size")] int pageSize = FIXED_SIZE)
        {
            using(var context = new BrowserHistoryContext())
            {
                return Ok(_repository.GetByPage(pageNumber, pageSize));
            }
        }

        [HttpPost]
        public ActionResult<HistoryItem> Create(HistoryItem item)
        {
            _repository.Create(item);
            return Created(string.Empty, item);
        }

        // [controller]/2
        [HttpPut]
        public ActionResult Update(HistoryItem historyItem)
        {
            var itemToUpdate = _repository.GetById(historyItem.Id);
            if(itemToUpdate == null)
            {
                return NotFound($"History item of id = {historyItem.Id} not found");
            }
            _repository.Update(historyItem);
            return StatusCode(StatusCodes.Status202Accepted);
        }

        // [controller]/1
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
