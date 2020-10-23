using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Dal;
using Server.Entities.Models;

namespace Server.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : Controller
    {
        ServerContext db;
        public HostController(ServerContext context)
        {
            db = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Host>>> Get()
        {
            return await db.Hosts.OrderBy(x => x.Name).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Host>> Get(int id)
        {
            Host Host = await db.Hosts.Include(v => v.Vms).Include(a => a.Apps).FirstOrDefaultAsync(x => x.Id == id);
            return Ok(Host);
        }

        [HttpPost]
        public async Task<ActionResult<Host>> Post(Host Host)
        {
            if (ModelState.IsValid)
            {
                db.Hosts.Add(Host);
                await db.SaveChangesAsync();
                return Ok(Host);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put(Host Host)
        {
            if (ModelState.IsValid)
            {
                db.Update(Host);
                await db.SaveChangesAsync();
                return Ok(Host);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Host>> Delete(int id)
        {
            Host Host = db.Hosts.FirstOrDefault(x => x.Id == id);
            if (Host != null)
            {
                db.Hosts.Remove(Host);
                await db.SaveChangesAsync();
            }
            return Ok(Host);
        }
    }
}