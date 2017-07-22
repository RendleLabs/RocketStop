using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketStop.DockingService.Data;

namespace RocketStop.DockingService.Controllers
{
    [Route("api/bays")]
    public class BaysController : Controller
    {
        private readonly DockingContext _context;

        public BaysController(DockingContext context)
        {
            _context = context;
        }

        [HttpGet("available")]
        public async Task<IActionResult> Available([FromQuery]int width, [FromQuery]int height, [FromQuery]int depth)
        {
            var results = await (from bay in _context.Bays
                                 join docking in _context.Dockings on bay.BayId equals docking.BayId into dd
                                 from docking in dd.DefaultIfEmpty()
                                 where bay.Width >= width && bay.Height >= height && bay.Depth >= depth && docking == null
                                 select bay).ToListAsync();

            return Ok(results);
        }

        [HttpGet("available-count")]
        public async Task<IActionResult> AvailableCount([FromQuery]int width, [FromQuery]int height, [FromQuery]int depth)
        {
            var count = await (from bay in _context.Bays
                               join docking in _context.Dockings on bay.BayId equals docking.BayId into dd
                               from docking in dd.DefaultIfEmpty()
                               where bay.Width >= width && bay.Height >= height && bay.Depth >= depth && docking == null
                               select bay).CountAsync();

            return Ok(new { count });
        }
    }
}